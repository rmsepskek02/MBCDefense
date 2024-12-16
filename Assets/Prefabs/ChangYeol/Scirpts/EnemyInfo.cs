using Defend.Enemy;
using Defend.TestScript;
using UnityEngine;

namespace Defend.UI
{
    [System.Serializable]
    public class EnemyInfo
    {
        #region Variables
        public string Enemyname;
        public Sprite enemySprite;
        //ü��,����
        public Health Health;
        //���ݷ�,���� ���ǵ�
        public EnemyAttackController Attack;
        //�̵��ӵ�
        public EnemyMoveController Move;
        //��� ��
        public EnemyController Money;
        #endregion
    }
}