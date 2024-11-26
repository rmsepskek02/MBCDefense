using UnityEngine;

/// <summary>
/// Enemy�� �⺻������ �����ϴ� Ŭ����
/// </summary>
namespace Defend.Enemy
{
    public enum EnemyType
    {
        Buffer,
        Warrior,
        Tanker,
        Boss
    }

    [System.Serializable]
    public class EnemyStats : MonoBehaviour
    {
        //Animator
        private Animator animator;

        //�̵�����
        public float baseSpeed = 5f;

        //���� ����
        public float baseAttackDamage = 10f;
        public float baseAttackDelay = 2f;

        //ü�°���
        public float baseHealth = 50f;

        //������
        public float baseArmor = 5f;

        //�������
        public int rewardGold = 1;

        //����ִ��� ����
        public bool isDeath = false;

        //�� Ÿ��
        public EnemyType type;
    }
}