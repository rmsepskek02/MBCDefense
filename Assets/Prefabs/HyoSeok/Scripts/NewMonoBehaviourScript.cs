using UnityEngine;

[System.Serializable]
public class testdata
{

    //�÷��̾� �ڿ� ����
    public float money;
    public float health;
    public float tree;
    public float rock;
  
    //Ÿ�� �ر� ���� 0�϶� ��� 1�϶� �ر�
    public int isTowerUnlock1;
    public int isTowerUnlock2;
    public int isTowerUnlock3;
    public int isTowerUnlock4;
    public int isTowerUnlock5;
    public int isTowerUnlock6;
    public int isTowerUnlock7;
    public int isTowerUnlock8;
    public int isTowerUnlock9;
    public int isTowerUnlock10;
    public int isTowerUnlock11;
    public int isTowerUnlock12;
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
    public bool isPotalActive;
    public bool isMoveSpeedUp;
    public bool isAutoGain;
    //�� �������� ��ݿ���
    public bool[] isClear = new bool[5];
    //Ÿ����ġ - ���ϸ� �����ϴ½����� �� �Ⱦƹ�����(��ġ��������) �ڿ� �߰�


    public testdata(float money, float health, float tree, float rock)
    {
        this.money = money;
        this.health = health;
        this.tree = tree;
        this.rock = rock;
    }
}
