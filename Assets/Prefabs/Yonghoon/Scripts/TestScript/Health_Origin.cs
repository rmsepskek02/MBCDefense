using UnityEngine;
using UnityEngine.Events;

namespace Defend.TestScript
{
    /// <summary>
    /// ü���� �����ϴ� Ŭ����
    /// </summary>
    public class Health_Origin : MonoBehaviour
    {
        #region Variables

        //ü�°���
        [SerializeField] private float maxHealth = 100f;    //�ִ� Hp
        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }

            private set { maxHealth = value; }
        }
        public float CurrentHealth { get; private set; }    //���� Hp

        //�Ƹ� ����
        [SerializeField] private float baseArmor = 5f;
        public float CurrentArmor { get; private set; }

        private bool isDeath = false;                       //���� üũ

        //UnityAction
        public UnityAction<float> OnDamaged;
        public UnityAction OnDie;
        public UnityAction<float> OnHeal;
        public UnityAction<float> Armorchange;

        public float GetRatio() => CurrentHealth / maxHealth;

        #endregion
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            CurrentHealth = maxHealth;
            CurrentArmor = baseArmor;
        }

        //��
        public void Heal(float amount)
        {
            // �� ���� �� ü�� ����
            float beforeHealth = CurrentHealth;

            // �� ����
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, maxHealth);

            // ���� ���� ���
            float realHeal = CurrentHealth - beforeHealth;

            //**************************************************
            // �������� ��� => ex) ���� ������ �ִ�ü�º��� �������� ���尪�� �شٰų�, �߰����� ������ �شٰų� �ʿ�� ���! 
            //float overheal = Mathf.Max(0f, amount - realHeal);
            //**************************************************

            // ����� �Ǵ� �α� ���
            Debug.Log($"Healed: {realHeal}, Current Health: {CurrentHealth}");
            if (realHeal > 0f)
            {
                //������Ʈ ����
                OnHeal?.Invoke(realHeal);
            }
        }

        //damageSource: �������� �ִ� ��ü
        public void TakeDamage(float damage)
        {
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