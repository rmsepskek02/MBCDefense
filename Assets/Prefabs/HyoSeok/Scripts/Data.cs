using UnityEngine;
using System;
using System.Collections.Generic;
using Defend.Tower;
using Defend.Projectile;
using Defend.TestScript;

namespace Defend.Manager
{
    /// <summary>
    /// 저장할 데이터
    /// </summary>
    [SerializeField]
    public class Data
    {

        //스테이지 카운트
        public int Round;
        public float countdown;
        //타워 해금 여부 0일때 잠금 1일때 해금
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
        //타워 해금 여부 0일때 잠금 1일때 해금
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

        //업그레이드 여부
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
        //플레이어 자원 여부
        public float money = 3000;
        public float health = 100;
        public float tree = 0;
        public float rock = 0;
        //타워설치 - 못하면 저장하는시점에 다 팔아버리고(설치가격으로) 자원 추가

        // 사운드
        public SoundData soundData;

        //public Dictionary<string, float> soundSettings = new Dictionary<string, float>
        //{
        //    { "Master", 1.0f },
        //    { "BGM", 1.0f },
        //    { "SFX", 1.0f }
        //};
        //터널링여부
        public bool isTuneeling;
        //플레이어 ui여부
        public bool isPlayerUI;
        public void initialSound(float master, float bgm, float sfx)
        {
            soundData = new SoundData(master, bgm, sfx);
        }

        public List<TowerData> towers = new List<TowerData>();
        //public List<EnemyData> enemies = new List<EnemyData>();
        public List<ProjectileData> projectiles = new List<ProjectileData>();
        // TODO 나무 돌 동전 

        // TODO 스킬쿨타임, 라운드, 리스트스포너정보, 플레이어 위치, 장비여부
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
    // TowerData 고유의 필드를 추가할 수 있음 (예: 공격력, 범위 등)
    public TowerData(TowerBase obj) : base(obj)
    {
        // 필요에 따라 TowerData 고유 필드를 초기화
    }
}

//[System.Serializable]
//public class EnemyData : ObjectData<EnemyBase>
//{
//    // EnemyData 고유의 필드를 추가할 수 있음 (예: 공격력, 범위 등)
//    public EnemyData(EnemyBase  obj) : base(obj)
//    {
//        // 필요에 따라 EnemyData 고유 필드를 초기화
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