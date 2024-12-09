using Defend.item;
using MyVrSample;
using System.Collections;
using System.Resources;
using UnityEngine;
namespace Defend.Interactive
{

    public class Resources : MonoBehaviour
    {
        public enum ResourceTypeEnum
        {
            Rock,
            Tree,
            Money
                
        }

        [System.Serializable]
        public class ResourceType
        {
            public ResourceTypeEnum name;        // �ڿ� �̸� 
            public float amount;       // �ڿ� ��
            public float health;       // �ڿ� �� ü��
            public GameObject resourceItem;  // �ڿ� ���������
           
        }
        #region
      

        private bool isDamaged = false;  // �������� �޴� ������ ����
        public ResourceType[] resourceTypes;  // �ڿ� Ÿ�� �迭
        private ResourceType currentResourceType;    // ���� �ڿ� Ÿ��

        //Ÿ�� ����
        public AudioClip  hitSound;
        private AudioSource audioSource;

        //���� ����Ʈ
        public GameObject destroyEffect;


        #endregion

        private void Awake()
        {
            // ù ��° �ڿ� Ÿ���� �⺻���� ����
            if (resourceTypes.Length > 0)
            {
                currentResourceType = resourceTypes[0];
            }
        }
        private void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
           
        }

        private void OnTriggerEnter(Collider other)
        {
            // �浹�� ������Ʈ�� �̸��� ���� ������Ʈ�� �̸��� ������� �ڿ� Ÿ�� ����
            string otherName = other.gameObject.name;
            string currentName = gameObject.name;

            if (!isDamaged &&
                ((otherName == "Axe" && currentName == "Tree") ||
                 (otherName == "PickAxe" && currentName == "Rock")))
            {
                SetCurrentResourceType(currentName);
                StartCoroutine(Shake());
                StartCoroutine(TakeDamage(10)); 
            }
        }

        //1�ʿ� �Ѵ뾿������
        private IEnumerator TakeDamage(float damage)
        {
            isDamaged = true;
            currentResourceType.health -= damage;
            Debug.Log($"{currentResourceType.name} health = {currentResourceType.health}");

            // �ڿ� ���� (�̴ϻ�����)
            if (currentResourceType.resourceItem != null)
            {
                //��ġ �ٽ���ƾ߉� ======================================================================================================
                GameObject dropitem = Instantiate(currentResourceType.resourceItem, transform.position, Quaternion.identity);
                DropItem item = dropitem.GetComponent<DropItem>();
                item.amount = currentResourceType.amount;
                item.resourceName = currentResourceType.name.ToString();

            }

            PlayHitSound();
            if (currentResourceType.health <= 0)
            {

                // ��鸲 ȿ���� ���尡 ����� �ð��� �ֱ� ���� ���
                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
                //���� ����Ʈ
               GameObject detheffect= Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(detheffect,1f);
            }

            yield return new WaitForSeconds(1f);

            isDamaged = false;
        }

        //������Ʈ ����
        IEnumerator Shake()
        {
            float t = 1f;
            float shakePower = 1f;
            Vector3 origin = transform.position;
           
            while (t > 0f)
            {
                t -= 0.05f;
                transform.position = origin + (Vector3)Random.insideUnitCircle * shakePower * t;
                yield return null;
            }

            transform.position = origin;
        }
       // //�ڿ�ŉ��
       //public void GiveResource()
       // {
       //     // �÷��̾�� ����
       //     ResourceManager.Instance.AddResources(currentResourceType.amount, currentResourceType.name.ToString());
       // }

        // �ڿ� Ÿ�Կ� ���� ���� �ڿ� ����
        public void SetCurrentResourceType(string resourceName)
        {
            foreach (var resourceType in resourceTypes)
            {
                if (resourceType.name.ToString().Equals(resourceName, System.StringComparison.OrdinalIgnoreCase))
                {
                    currentResourceType = resourceType;
                    return;  // Ÿ���� ã������ �ٷ� ����
                }
            }
        }

        //��Ʈ ����
        public void PlayHitSound()
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }

}