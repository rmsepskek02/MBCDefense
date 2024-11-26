using UnityEngine;
using UnityEngine.UI;

namespace Defend.Enemy
{
    public class Enemy : MonoBehaviour
    {
        #region Variables
        //ü��
        private float health;
        [SerializeField] private float startHealth = 100;

        //���� �ݾ�
        [SerializeField]
        private int rewardGold = 50;

        //�׾����� Ȯ��
        private bool isDeath;

        //HealthBar
        //public Image healthBar;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            health = startHealth;
            isDeath = false;
        }

        //������ ó��
        public void TakeDamage(float damage)
        {
            if (isDeath) return;
            health -= damage;
            //Debug.Log($"health: {health}");

            //healthBar
            //healthBar.fillAmount = health / startHealth;

            if (health <= 0)
            {
                Die();
            }
        }

        //���� ó��
        void Die()
        {
            isDeath = true;
            //�״� ����Ʈ ó��

            //������� 50 Gold ����

            //Enemy count --

            //kill
            Destroy(gameObject);
        }
    }
}