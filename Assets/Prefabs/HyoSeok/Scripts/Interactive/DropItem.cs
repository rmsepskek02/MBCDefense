using Defend.Player;
using Defend.Interactive;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

namespace Defend.item
{
    public class DropItem : MonoBehaviour
    {
        #region Variables
        private PlayerState playerState;
        private GameObject targetObject;
        public string resourceName; // �ڿ��̸�
        public float amount; // �ڿ� ��

        //������ ŉ�� ����
        public AudioClip GetItemSound;
        private AudioSource audioSource;

        //�ڿ� �ߺ�ŉ�����
        private bool isCollected = false;
        #endregion

        private void Awake()
        {
            Collider collider = GetComponent<Collider>();
            playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState�� ã��
            audioSource = gameObject.AddComponent<AudioSource>();
            targetObject = GameObject.Find("PlayerBody");

        }
       

        private void Update()
        {
            MagnetItem();
        }


        //�÷��̾ �Ÿ��� ������ �÷��̾�� �̵�
        void MagnetItem()
        {
            if (playerState != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, targetObject.transform.position);
                if (distanceToPlayer < ResourceManager.distance)
                {
                    Vector3 direction = (targetObject.transform.position - transform.position).normalized;
            
                    transform.position += direction * ResourceManager.speed * Time.deltaTime;

                    if (distanceToPlayer < 0.5f && !isCollected) 
                    {
                        GetResource();
                        audioSource.clip = GetItemSound;
                        audioSource.Play();
                        Destroy(gameObject, 0.1f);
                    }
                }
            }
        }

        void GetResource()
        {
            // �÷��̾�� ����
            ResourceManager.Instance.AddResources(amount, resourceName);
            isCollected = true;
        }

        //������ ����
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.gameObject);

            if (other.CompareTag("Player") && !isCollected)
            {
                GetResource();
                audioSource.clip = GetItemSound;
                audioSource.Play();
                Destroy(gameObject, 0.1f);

            }

        }

    }


}