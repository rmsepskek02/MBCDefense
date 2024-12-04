using Defend.Tower;
using UnityEngine;
namespace Defend.Utillity
{
    public class Status : MonoBehaviour
    {
        #region Variables
        // �ִ� ü��
        [SerializeField] private float maxHealth;
        public float MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }
        // ���� ü��
        [SerializeField] private float currentHealth;
        public float CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                currentHealth = value;

                //���� ó��
                if (currentHealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }
        // ��������
        private bool isDeath = false;
        public bool IsDeath
        {
            get { return isDeath; }
            private set
            {
                isDeath = value;
                //�ִϸ��̼�
                //animator.SetBool(AnimationString.IsDeath, value);
            }
        }

        // ����
        [SerializeField] private float maxMana;
        public float MaxMana
        {
            get { return maxMana; }
            private set { maxMana = value; }
        }

        [SerializeField] private float currentMana;
        public float CurrentMana
        {
            get { return currentMana; }
            private set
            {
                currentMana = value;

                //���� ó��
                if (currentMana <= 0)
                {

                }
            }
        }

        // ���� ����
        [SerializeField] private float currentArmor;
        public float CurrentArmor
        {
            get { return currentArmor; }
            private set
            {
                currentArmor = value;
            }
        }

        #endregion

        private void Start()
        {

        }

        private void Update()
        {
            ChargeMana(0.5f);
        }
        // �ʱ�ȭ
        public void Init(TowerInfo towerInfo)
        {
            SetMaxHealth(towerInfo.maxHealth);
            SetMaxMana(towerInfo.maxMana);
            SetCurrentArmor(towerInfo.armor);
        }

        // �ִ�ü�� ����
        public void SetMaxHealth(float amount)
        {
            maxHealth = amount;
            CurrentHealth = maxHealth;
        }

        // �ִ븶�� ����
        public void SetMaxMana(float amount)
        {
            maxMana = amount;
            CurrentMana = maxMana;
        }

        // �Ƹ� ����
        public void SetCurrentArmor(float amount)
        {
            currentArmor = amount;
        }

        // �Ƹ� ����
        public void ReduceArmor(float amount)
        {
            //CurrentArmor -= amount;
        }

        // ������ ����
        public void TakeDamage(float damage)
        {
            // ���� ���� �� ���� ������ ��� 
            float mitigatedDamage = Mathf.Clamp(damage - CurrentArmor, 0, Mathf.Infinity);

            // ���������� ���� ������ ��� �� ��ȿ�� �˻�
            float realDamage = Mathf.Min(CurrentHealth, mitigatedDamage);

            // ü�� ����
            CurrentHealth -= realDamage;

            // ü���� 0 ���϶�� ��� ó��
            if (CurrentHealth <= 0f)
            {
                CurrentHealth = 0;
                //Die();
            }
        }

        // ü�� ȸ��
        public void Heal(float amount)
        {
            // �� ���� �� ü�� ����
            float beforeHealth = CurrentHealth;

            // �� ����
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, maxHealth);

            // ���� ���� ���
            float realHeal = CurrentHealth - beforeHealth;
        }

        // ���� ���
        public void UseMana(float amount)
        {
            // �Ҹ� ��ȿ�� �˻�
            amount = Mathf.Clamp(amount, 0, Mathf.Infinity);

            // ���� �Ҹ�
            CurrentMana -= amount;
            // ���� �� ��ȿ�� �˻�
            CurrentMana = Mathf.Clamp(CurrentMana, 0f, maxMana);

            // ������ 0���� ���� ���
            if (CurrentMana <= 0f)
            {
                CurrentMana = 0;
            }
        }

        // ���� ���
        public void ChargeMana(float ratio)
        {
            // �ִ� ���� ���� ���� ���
            if(CurrentMana < maxMana)
            {
                CurrentMana += Time.deltaTime * ratio;
            }
        }
    }
}
