using System.Collections;
using System.Resources;
using UnityEngine;
namespace Defend.Interactive{
 
    public class Resources : MonoBehaviour
    {
        public int health = 50;  // ���� ü��
        public int resourceAmount = 10;  //����ڿ�


        public GameObject resourcePickupEffect;  // �ڿ��� ������ �� ȿ��
        private bool isDamaged = false;  // �������� �޴� ������ ����

        private void OnTriggerEnter(Collider other)
        {
            // �հ� ������
            if ((other.CompareTag("LeftHand") || other.CompareTag("RightHand")) && !isDamaged)
            {
                StartCoroutine(TakeDamage(10));  // ������ ���� ������ 10 ������
            }
        }

        //1�ʿ� �Ѵ뾿������
        private IEnumerator TakeDamage(int damage)
        {
            isDamaged = true; 
            health -= damage;
            Debug.Log($"health = {health}");
            // �ڿ� ���
            GiveResource();
            
            if (health <= 0)
            {
                Destroy(gameObject);
            }

            yield return new WaitForSeconds(1f);  

            isDamaged = false;  
        }
        void GiveResource()
        {
            // �ڿ� ���� ȿ������Ʈ(���׸��� �ڿ��� �������´���?) �׸��� 1�ʵ� ����?
            if (resourcePickupEffect != null)
            {
               GameObject effectGo= Instantiate(resourcePickupEffect, transform.position, Quaternion.identity);
                Destroy(effectGo,1f);
            }

            // �÷��̾�� ����
            ResourceManager.Instance.AddResources(resourceAmount);
        }
    }

}