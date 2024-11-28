using UnityEngine;
using Defend.Enemy;

namespace Defend.TestScript
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        //�ִϸ�����
        private Animator animator;

        //ü�´�� ������Ʈ
        private Health health;

        public Vector3 offset;

        #endregion
        void Start()
        {
            //����
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();

            //UnityAction
            health.OnDie += OnDie;
            health.OnHeal += OnHeal;
            health.OnDamaged += OnDamaged;
        }

        private void OnDamaged(float arg0)
        {
            Debug.Log("���� ����");
        }

        private void OnHeal(float arg0)
        {
            Debug.Log("�� ����");
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDie()
        {
            //����ִ� ���ʹ� �� ����
            SpawnManager.enemyAlive--;

            animator.SetBool("IsDeath", true);

            //Enemy ų
            Destroy(gameObject, 2f);
        }
    }
}