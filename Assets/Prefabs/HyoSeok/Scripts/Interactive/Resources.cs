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
            Tree
        }

        [System.Serializable]
        public class ResourceType
        {
            public ResourceTypeEnum name;        // �ڿ� �̸� 
            public float amount;       // �ڿ� ��
            public float health;       // �ڿ� �� ü��
            public GameObject resourcePickupEffect;  // �ڿ��� ������ �� ȿ��
        }
        #region

        private bool isDamaged = false;  // �������� �޴� ������ ����
        public ResourceType[] resourceTypes;  // �ڿ� Ÿ�� �迭
        private ResourceType currentResourceType;    // ���� �ڿ� Ÿ��

        //Ÿ�� ����
        public AudioClip  hitSound;
        private AudioSource audioSource;
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
            // �հ� ������ // ������Ʈ �̸����� �����Ұ� !!
            //����ý��� ����������� �������� ������
            if ((other.CompareTag("LeftHand") || other.CompareTag("RightHand")) && !isDamaged)
            {
                // �浹�� ������Ʈ�� �̸��� ������� �ڿ� Ÿ�� ����
                SetCurrentResourceType(gameObject.name);
                StartCoroutine(Shake());
                StartCoroutine(TakeDamage(10));  // ������ ���� ������ 10 ������

            
            }
        }

        //1�ʿ� �Ѵ뾿������
        private IEnumerator TakeDamage(float damage)
        {
            isDamaged = true;
            currentResourceType.health -= damage;
            Debug.Log($"{currentResourceType.name} health = {currentResourceType.health}");
            //���� ���
            PlayHitSound();

            // �ڿ� ���
            GiveResource();

            if (currentResourceType.health <= 0)
            {
                Destroy(gameObject);
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

        void GiveResource()
        {
            // �ڿ� ���� ȿ������Ʈ(���׸��� �ڿ��� �������´���?) �׸��� 1�ʵ� ����?
            if (currentResourceType.resourcePickupEffect != null)
            {
                GameObject effectGo = Instantiate(currentResourceType.resourcePickupEffect, transform.position, Quaternion.identity);
                Destroy(effectGo, 1f);
            }

            // �÷��̾�� ����
            ResourceManager.Instance.AddResources(currentResourceType.amount, currentResourceType.name.ToString());
        }

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