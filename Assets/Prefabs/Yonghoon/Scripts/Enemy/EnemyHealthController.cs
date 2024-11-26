using System;
using UnityEngine;

namespace Defend.Enemy
{
    public class EnemyHealthController : MonoBehaviour
    {
        #region Variables
        EnemyStats enemyBase;
        [SerializeField] public float CurrentHealth { get; private set; }
        [SerializeField] public float CurrentArmor { get; private set; }
        #endregion

        private void Start()
        {
            enemyBase = GetComponent<EnemyStats>();
            CurrentHealth = enemyBase.baseHealth;
            CurrentArmor = enemyBase.baseArmor;
        }

        public void TakeDamage(float damage)
        {
            float beforeHealth = CurrentHealth;
            damage -= CurrentArmor;
            damage = Mathf.Clamp(damage, 0, Mathf.Infinity);
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, enemyBase.baseHealth);

            //real Damage ���ϱ�
            float realDamage = beforeHealth - CurrentHealth;
            if (realDamage > 0f)
            {
                //������ ����                
                CurrentHealth -= realDamage;
                if (CurrentHealth <= 0f)
                {
                    Die();
                }
            }
        }

        //��
        public void Heal(float amount)
        {
            float beforeHealth = CurrentHealth; 
            CurrentHealth += amount;            
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, enemyBase.baseHealth);      

            //real Heal ���ϱ�
            float realHeal = CurrentHealth - beforeHealth;  
            if (realHeal > 0f)
            {
                //�� ����
                CurrentHealth += realHeal;
            }
        }

        public void ReduceArmor(float amount)
        {
            CurrentArmor -= amount;
        }

        public void Die()
        {
            Debug.Log("Enemy Die!");
        }
    }
}