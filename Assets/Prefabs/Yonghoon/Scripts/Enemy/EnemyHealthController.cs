//using UnityEngine;

//namespace Defend.Enemy
//{
//    public class EnemyHealthController : MonoBehaviour
//    {
//        #region Variables
//        EnemyState enemyBase;
//        [SerializeField] public float CurrentHealth { get; private set; }
//        [SerializeField] public float CurrentArmor { get; private set; }
//        #endregion

//        private void Start()
//        {
//            enemyBase = GetComponent<EnemyState>();
//            CurrentHealth = enemyBase.baseHealth;
//            CurrentArmor = enemyBase.baseArmor;
//        }

//        public void TakeDamage(float damage)
//        {
//            // ���� ���� �� ���� ������ ���
//            float mitigatedDamage = Mathf.Clamp(damage - CurrentArmor, 0, Mathf.Infinity);

//            // ���������� ���� ������ ���
//            float realDamage = Mathf.Min(CurrentHealth, mitigatedDamage);

//            // ü�� ����
//            CurrentHealth -= realDamage;

//            // �α׳� ����� �뵵�� ������ �������� ��� (�ɼ�)
//            Debug.Log($"Damage Taken: {realDamage}, Remaining Health: {CurrentHealth}");

//            // ü���� 0 ���϶�� ��� ó��
//            if (CurrentHealth <= 0f)
//            {
//                //�������� ü�º��� ���������, ������ ��ġ�� �ʿ��ҽ� ���
//                //float overkillDamage = Mathf.Max(0, mitigatedDamage - CurrentHealth);
//                //Debug.Log($"Overkill Damage: {overkillDamage}");

//                Die();
//            }
//        }

//        //��
//        public void Heal(float amount)
//        {
//            // �� ���� �� ü�� ����
//            float beforeHealth = CurrentHealth;

//            // �� ����
//            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0f, enemyBase.baseHealth);

//            // ���� ���� ���
//            float realHeal = CurrentHealth - beforeHealth;

//            //**************************************************
//            // �������� ��� => ex) ���� ������ �ִ�ü�º��� �������� ���尪�� �شٰų�, �߰����� ������ �شٰų� �ʿ�� ���! 
//            //float overheal = Mathf.Max(0f, amount - realHeal);
//            //**************************************************

//            // ����� �Ǵ� �α� ���
//            Debug.Log($"Healed: {realHeal}, Current Health: {CurrentHealth}");
//        }


//        public void ReduceArmor(float amount)
//        {
//            CurrentArmor -= amount;
//        }

//        public void Die()
//        {
//            Debug.Log("Enemy Die!");
//            Destroy(gameObject);
//        }
//    }
//}