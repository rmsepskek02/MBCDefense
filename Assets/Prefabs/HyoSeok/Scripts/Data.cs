using UnityEngine;
using System;
namespace Defend.Manager
{
    /// <summary>
    /// ������ ������
    /// </summary>
    [SerializeField]
    public class Data 
    {
        //�� �������� ��ݿ���
        public bool[] isClear = new bool[5];
        //Ÿ�� �ر� ����
        public bool[] isTowerUnlock = new bool[7];
        //���׷��̵� ����
        public int isHPUpgradelevel;
        public int isHPTimeUpgradelevel;
        public int isArmorUpgradelevel;
        public int isATKUpgradelevel;
        public int isATKSpeedUpgradelevel;
        public int isATKRangeUpgradelevel;
        public int isMoneyGainlevel;
        public int isTreeGainlevel;
        public int isRockGainlevel;
        public bool isPotalActive ;
        public bool isMoveSpeedUp;
        public bool isAutoGain;
        //�÷��̾� �ڿ� ����
        public float money;
        public float health;
        public float tree;
        public float rock;
        //Ÿ����ġ - ���ϸ� �����ϴ½����� �� �Ⱦƹ�����(��ġ��������) �ڿ� �߰�
    }
}