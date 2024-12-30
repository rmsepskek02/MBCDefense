using UnityEngine;
using System;
using System.Collections.Generic;
using Defend.Tower;
using Defend.Projectile;
using Defend.TestScript;

namespace Defend.Manager
{
    /// <summary>
    /// ������ ������
    /// </summary>
    [SerializeField]
    public class Data
    {

        //�������� ī��Ʈ
        public int Round;
        public float countdown;
        //Ÿ�� �ر� ���� 0�϶� ��� 1�϶� �ر�
        public bool isTowerUnlock1;
        public bool isTowerUnlock2;
        public bool isTowerUnlock3;
        public bool isTowerUnlock4;
        public bool isTowerUnlock5;
        public bool isTowerUnlock6;
        public bool isTowerUnlock7;
        public bool isTowerUnlock8;
        public bool isTowerUnlock9;
        public bool isTowerUnlock10;
        public bool isTowerUnlock11;
        public bool isTowerUnlock12;

        //
        //Ÿ�� �ر� ���� 0�϶� ��� 1�϶� �ر�
        public bool isTowerUnlocked1;
        public bool isTowerUnlocked2;
        public bool isTowerUnlocked3;
        public bool isTowerUnlocked4;
        public bool isTowerUnlocked5;
        public bool isTowerUnlocked6;
        public bool isTowerUnlocked7;
        public bool isTowerUnlocked8;
        public bool isTowerUnlocked9;
        public bool isTowerUnlocked10;
        public bool isTowerUnlocked11;
        //public bool isTowerUnlocked12;

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
        //�÷��̾� �ڿ� ����
        public float money = 3000;
        public float health = 100;
        public float tree = 0;
        public float rock = 0;
        //Ÿ����ġ - ���ϸ� �����ϴ½����� �� �Ⱦƹ�����(��ġ��������) �ڿ� �߰�

        // ����
        public SoundData soundData;

        //public Dictionary<string, float> soundSettings = new Dictionary<string, float>
        //{
        //    { "Master", 1.0f },
        //    { "BGM", 1.0f },
        //    { "SFX", 1.0f }
        //};
        //�ͳθ�����
        public bool isTuneeling;
        //�÷��̾� ui����
        public bool isPlayerUI;
        public void initialSound(float master, float bgm, float sfx)
        {
            soundData = new SoundData(master, bgm, sfx);
        }

        public List<TowerData> towers = new List<TowerData>();
        //public List<EnemyData> enemies = new List<EnemyData>();
        public List<ProjectileData> projectiles = new List<ProjectileData>();
        // TODO ���� �� ���� 

        // TODO ��ų��Ÿ��, ����, ����Ʈ����������, �÷��̾� ��ġ, ��񿩺�
    }
}

[System.Serializable]
public class SoundData
{
    public float masterVolumn;
    public float bgmVolumn;
    public float sfxVolumn;

    public SoundData(float master, float bgm, float sfx)
    {
        this.masterVolumn = master;
        this.bgmVolumn = bgm;
        this.sfxVolumn = sfx;
    }
}

[System.Serializable]
public class ObjectData<T> where T : Component
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;

    public ObjectData(T obj)
    {
        GameObject gameObject = obj.gameObject;
        this.name = gameObject.name.Replace("(Clone)", "").Trim();
        this.position = gameObject.transform.position;
        this.rotation = gameObject.transform.rotation;
    }
}
[System.Serializable]
public class TowerData : ObjectData<TowerBase>
{
    // TowerData ������ �ʵ带 �߰��� �� ���� (��: ���ݷ�, ���� ��)
    public TowerData(TowerBase obj) : base(obj)
    {
        // �ʿ信 ���� TowerData ���� �ʵ带 �ʱ�ȭ
    }
}

//[System.Serializable]
//public class EnemyData : ObjectData<EnemyBase>
//{
//    // EnemyData ������ �ʵ带 �߰��� �� ���� (��: ���ݷ�, ���� ��)
//    public EnemyData(EnemyBase  obj) : base(obj)
//    {
//        // �ʿ信 ���� EnemyData ���� �ʵ带 �ʱ�ȭ
//    }
//}

[System.Serializable]
public class ProjectileData : ObjectData<ProjectileBase>
{
    public Transform target;

    public ProjectileData(ProjectileBase projectile) : base(projectile)
    {
        this.target = projectile.target;
    }
}