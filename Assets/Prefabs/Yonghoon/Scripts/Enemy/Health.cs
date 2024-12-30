using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using Defend.Enemy;
namespace Defend.TestScript
{
    /// <summary>
    /// ü���� �����ϴ� Ŭ����
    /// </summary>
    public class Health : MonoBehaviour
    {
        #region Variables

        //ü�°���
        public float maxHealth = 100f;    //�ִ� Hp
        public float CurrentHealth { get; set; }    //���� Hp

        public Image CastleHealthBar;
        //�Ƹ� ����
        public float baseArmor = 5f;
        public float CurrentArmor { get; set; }

        private bool isDeath = false;                       //���� üũ
        //ü�� ����
        public float RgAmount;          //ü����
        public float Rginterval;        //ü�� ����
        [SerializeField] private bool isHpTime = false;                      //ü�� üũ
        //UnityAction
        public UnityAction<float> OnDamaged;
        public UnityAction OnDie;
        public UnityAction<float> OnHeal;
        public UnityAction<float> Armorchange;

        public float GetRatio() => CurrentHealth / maxHealth;

        private ListSpawnManager listSpawnManager;

        #endregion
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            listSpawnManager = FindAnyObjectByType<ListSpawnManager>();
            maxHealth *= (listSpawnManager.waveCount + 1);
            CurrentHealth = maxHealth;
            CurrentArmor = baseArmor;
            HPTime(RgAmount, Rginterval);//1�ʸ��� 1�� ü���� ȸ��
        }

        public void chakeC()
        {
            if(transform.name == "Castle")
            {
                CastleHealthBar.fillAmount = CurrentHealth/maxHealth;
            }
        }

        //�ƽ� ü�� �ø���
        public void IncreaseMaxHealth(float amount)
        {
            maxHealth += amount;
            //Debug.Log("Max Health up " + maxHealth);
        }

        //ü�� ����
        public void HPTime(float amount, float interval)
        {
            StartCoroutine(RegenerateHealth(amount, interval));
        }

        public IEnumerator RegenerateHealth(float amount, float interval)
        {
            if (isHpTime == true)
            {


                while (true)
                {
                    amount = RgAmount;

                    CurrentHealth += amount;
                    // �ִ� ü���� �ʰ����� �ʵ��� ����
                    CurrentHealth = Mathf.Min(CurrentHealth, maxHealth);

                    // ������ ���ݸ�ŭ ���
                    yield return new WaitForSeconds(interval);
                }
            }
        }
        //�� Real Method
        /*
        public void Heal(float amount)
        {
            //�׾����� �������� ����
            if (isDeath) return;

            // �� ���� �� ü�� ����
            float beforeHealth = CurrentHealth;

            // �� ����
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, CurrentHealth, maxHealth);

            // ���� ���� ���
            float realHeal = CurrentHealth - beforeHealth;

            //**************************************************
            // �������� ��� => ex) ���� ������ �ִ�ü�º��� �������� ���尪�� �شٰų�, �߰����� ������ �شٰų� �ʿ�� ���! 
            //float overheal = Mathf.Max(0f, amount - realHeal);
            //**************************************************

            // ����� �Ǵ� �α� ���
            //Debug.Log($"Healed: {realHeal}, Current Health: {CurrentHealth}");
            if (realHeal > 0f)
            {
                //������Ʈ ����
                OnHeal?.Invoke(realHeal);
            }
        }
        */
        //�׽�Ʈ�� heal
        public void Heal(float amount)
        {
            //�׾����� �������� ����
            if (isDeath) return;

            // �� ���� �� ü�� ����
            float beforeHealth = CurrentHealth;

            // �� ����
            amount = amount / 100;
            CurrentHealth = Mathf.Clamp(CurrentHealth + (maxHealth * amount), CurrentHealth, maxHealth); 

            // ���� ���� ���
            float realHeal = CurrentHealth - beforeHealth;

            //**************************************************
            // �������� ��� => ex) ���� ������ �ִ�ü�º��� �������� ���尪�� �شٰų�, �߰����� ������ �شٰų� �ʿ�� ���! 
            //float overheal = Mathf.Max(0f, amount - realHeal);
            //**************************************************

            // ����� �Ǵ� �α� ���
            //Debug.Log($"Healed: {realHeal}, Current Health: {CurrentHealth}");
            if (realHeal > 0f)
            {
                //������Ʈ ����
                OnHeal?.Invoke(realHeal);
            }
        }

        //damageSource: �������� �ִ� ��ü
       
        public void TakeDamage(float damage)
        {
            //�׾����� �������� ����
            if (isDeath) return;

            // ���� ���� �� ���� ������ ���
            float mitigatedDamage = Mathf.Max(damage - CurrentArmor, 0); // Clamp ��� Max ��� (�� ����)

            // ���������� ���� ������ ���
            float realDamage = Mathf.Min(CurrentHealth, mitigatedDamage);

            if (realDamage > 0f)
            {
                // ü�� ����, ����������Ʈ ����
                CurrentHealth -= realDamage;
                OnDamaged?.Invoke(-realDamage);
            }
            //Debug.Log($"Damage Taken: {realDamage}, Remaining Health: {CurrentHealth}");
            // ü���� 0 ���϶�� ���� ó��
            if (CurrentHealth <= 0f)
            {
                HandleDeath();
            }
        }
        //�ƸӰ� ����� ȣ��� �޼���
        public void ChangedArmor(float amount)
        {
            CurrentArmor += amount;
            Armorchange?.Invoke(amount);
        }

        //���� ó�� ����
        void HandleDeath()
        {
            //���� üũ
            if (isDeath)
                return;

            if (CurrentHealth <= 0f)
            {
                isDeath = true;
                //���� ����
                OnDie?.Invoke();
            }
        }
    }
}