using Defend.Enemy;
using UnityEngine;
using Defend.Player;
using Defend.TestScript;
using Defend.UI;
using System;
using System.Collections;
using TMPro;
using Defend.Tower;
using Defend.Interactive;
using Defend.item;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Defend.Utillity;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine.UI;
using Defend.Projectile;
using UnityEngine.XR.Interaction.Toolkit;

namespace Defend.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //����
        PlayerState playerState;
        TowerBase[] towerbase;
        ListSpawnManager listSpawnManager;
        CastleUpgrade castleUpgrade;
        BuildMenu build;
        Health health;
        TowerBuildMenuName towerBuildMenuName;
        GameResources resources;
        HealthBasedCastle healthBasedCastle;
        ToggleButton toggleButton;
        //private GameObject[] enemies;
        EnemyState[] enemyState;
        private BuildManager buildManager;
        public XROrigin player;
        public HealthBasedCastle castle;
        public BuildMenu buildMenu;
        public InteractionLayerMask layerMask;

        //sfs
        //private AudioSource audioSource;
        //public AudioClip clearSound;
        //public AudioClip gameoverSound;


        //public Button saveButton; // ���̺� ��ư
        public Data data = new Data(); // ���� ������

        //�̵��ӵ� ����
        public DynamicMoveProvider dynamicMove;
        
        public static GameManager instance;


        public List<TowerBase> towers = new List<TowerBase>();
        //public List<GameObject> enemies = new List<GameObject>();
        public List<ProjectileBase> projectiles = new List<ProjectileBase>();
        #endregion

        //�̱���

        public static GameManager Instance
        {
            get
            {
                if (!instance)
                {
                    if (GameObject.Find("Managers") != null)
                    {
                        Instance.transform.SetParent(GameObject.Find("Managers").transform);
                    }
                    DontDestroyOnLoad(Instance);
                }

                return instance;
            }
        }
        private void Awake()
        {
            // �ߺ��� GameManager�� �ִٸ� ����
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        private void Start()
        {
            //����
            health = castle.GetComponent<Health>();
            playerState = FindAnyObjectByType<PlayerState>();
            castleUpgrade = FindAnyObjectByType<CastleUpgrade>();
            listSpawnManager = FindAnyObjectByType<ListSpawnManager>();
            build = FindAnyObjectByType<BuildMenu>();
            towerBuildMenuName = FindAnyObjectByType<TowerBuildMenuName>();
            healthBasedCastle = FindAnyObjectByType<HealthBasedCastle>();
            toggleButton = FindAnyObjectByType<ToggleButton>();
            buildManager = BuildManager.instance;
            buildMenu = FindAnyObjectByType<BuildMenu>();

            //LoadGameData();


            //if (!audioSource)
            //    return;
            //audioSource = player.AddComponent<AudioSource>();
            //audioSource.playOnAwake = false; // �ڵ� ��� ����

        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Keypad0))
            {
                Time.timeScale = 10f;
            }
            if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                Time.timeScale = 1f;
            }


        }

        //���� ����� �ڵ�����
        // �ʱ�ȭ��
        //private void OnApplicationQuit()
        //{
        //    DataManager.Instance.SaveGameData(data);
        //}

        public void OnSaveButtonClicked()
        {
            SaveGameData();
        }


        //���̺� ��ư��
        public void SaveGameData()
        {

            //�������� ī��Ʈ
            if (data.Round <= 1)
            {
                data.Round = 1;
            }
            else
            {
                data.Round = listSpawnManager.waveCount - 1;
            }
            data.countdown = listSpawnManager.countdown;

            //�÷��̾� �ڿ� ����
            data.health = health.maxHealth;
            data.money = playerState.money;
            data.tree = playerState.tree;
            data.rock = playerState.rock;
            //���׷��̵� �ܰ�
            data.isHPUpgradelevel = castleUpgrade.currentHPUpgradeLevel;
            data.isHPTimeUpgradelevel = castleUpgrade.currentHPTimeUpgradeLevel;
            data.isArmorUpgradelevel = castleUpgrade.currenteArmorUpgradeLevel;
            data.isATKUpgradelevel = castleUpgrade.currentTowerATKUpgradeLevel;
            data.isATKSpeedUpgradelevel = castleUpgrade.currentTowerATKSpeedUpgradeLevel;
            data.isATKRangeUpgradelevel = castleUpgrade.currentTowerATKRangeUpgradeLevel;
            data.isMoneyGainlevel = castleUpgrade.currentMoneyGainUpgradeLevel;
            data.isTreeGainlevel = castleUpgrade.currentTreeGainUpgradeLevel;
            data.isRockGainlevel = castleUpgrade.currentRockGainUpgradeLevel;
            //���׷��̵� �رݿ���
            data.isPotalActive = castleUpgrade.isPotalActive;
            data.isMoveSpeedUp = castleUpgrade.isMoveSpeedUp;
            data.isAutoGain = castleUpgrade.isAutoGain;
            //Ÿ�� �رݿ���

            data.isTowerUnlock1 = build.towerinfo[3].isLock;
            data.isTowerUnlock2 = build.towerinfo[6].isLock;
            data.isTowerUnlock3 = build.towerinfo[9].isLock;
            data.isTowerUnlock4 = build.towerinfo[12].isLock;
            data.isTowerUnlock5 = build.towerinfo[15].isLock;
            data.isTowerUnlock6 = build.towerinfo[18].isLock;
            data.isTowerUnlock7 = build.towerinfo[21].isLock;
            //data.isTowerUnlock8 = build.towerinfo[8].isLock;
            //data.isTowerUnlock9 = build.towerinfo[9].isLock;
            //data.isTowerUnlock10 = build.towerinfo[10].isLock;
            //data.isTowerUnlock11 = build.towerinfo[11].isLock;
            //data.isTowerUnlock12 = build.towerinfo[12].isLock;

            //Ÿ��UI�ر�
            data.isTowerUnlocked1 = towerBuildMenuName.unlockTowerButton[0];
            data.isTowerUnlocked2 = towerBuildMenuName.unlockTowerButton[1];
            data.isTowerUnlocked3 = towerBuildMenuName.unlockTowerButton[2];
            data.isTowerUnlocked4 = towerBuildMenuName.unlockTowerButton[3];
            data.isTowerUnlocked5 = towerBuildMenuName.unlockTowerButton[4];
            data.isTowerUnlocked6 = towerBuildMenuName.unlockTowerButton[5];
            data.isTowerUnlocked7 = towerBuildMenuName.unlockTowerButton[6];
            //data.isTowerUnlocked8 = towerBuildMenuName.unlockTowerButton[7];
            //data.isTowerUnlocked9 = towerBuildMenuName.unlockTowerButton[8];
            //data.isTowerUnlocked10 = towerBuildMenuName.unlockTowerButton[9];
            //data.isTowerUnlocked11 = towerBuildMenuName.unlockTowerButton[10];
            //data.isTowerUnlocked12 = towerBuildMenuName.unlockTowerButton[12];

            ////����
            //Debug.Log(AudioUtility.GetVolume(Constants.AUDIO_UTIL_MASTER) + "savedata");
            //data.soundSettings[Constants.AUDIO_UTIL_MASTER] = AudioUtility.GetVolume(Constants.AUDIO_UTIL_MASTER);
            //data.soundSettings[Constants.AUDIO_UTIL_BGM] = AudioUtility.GetVolume(Constants.AUDIO_UTIL_BGM);
            //data.soundSettings[Constants.AUDIO_UTIL_EFFECT] = AudioUtility.GetVolume(Constants.AUDIO_UTIL_EFFECT);
            data.initialSound(
                AudioUtility.GetVolume(Constants.AUDIO_UTIL_MASTER),
                AudioUtility.GetVolume(Constants.AUDIO_UTIL_BGM),
                AudioUtility.GetVolume(Constants.AUDIO_UTIL_EFFECT)
                    );

            //�ͳθ�
            data.isTuneeling = toggleButton.isOnto;
            //�÷��̾�ui
            data.isPlayerUI = toggleButton.isOnPlay;

            Debug.Log("COUNT = " + projectiles.Count);
            // Tower ������ ����
            SaveTowers(towers);
            // Projectile ������ ����
            SaveProjectiles(projectiles);
            // ������ ����
            DataManager.Instance.SaveGameData(data);
        }

        // Tower ������ Save
        public void SaveTowers(List<TowerBase> gmTowers)
        {
            data.towers.Clear();
            foreach (TowerBase tower in gmTowers)
            {
                TowerData saveTower = new TowerData(tower);
                data.towers.Add(saveTower);
            }
        }

        // Tower ������ Load
        public void LoadTowers(List<TowerData> towersData)
        {
            foreach (TowerData tower in towersData)
            {
                string prefabPath = $"Prefabs/Tower/{tower.name}";
                GameObject towerPrefab = Resources.Load<GameObject>(prefabPath);

                if (towerPrefab == null)
                {
                    Debug.LogError($"Failed to load tower prefab at path: {prefabPath}");
                    continue;
                }

                GameObject towerGo = Instantiate(towerPrefab, tower.position, tower.rotation);
                towerGo.transform.parent = buildManager.transform;
                towerGo.AddComponent<BoxCollider>();
                towerGo.AddComponent<TowerXR>();
                BoxCollider box = towerGo.GetComponent<BoxCollider>();
                TowerXR towerXR = towerGo.GetComponent<TowerXR>();
                towerXR.interactionLayers = layerMask;
                box.isTrigger = false;
                box.size = buildMenu.boxes[buildMenu.indexs].size + new Vector3(0.5f, 0, 0.5f);
                box.center = buildMenu.boxes[(buildMenu.indexs)].center;
            }
        }
        // Projectile ������ Save
        public void SaveProjectiles(List<ProjectileBase> gmProjectiles)
        {
            data.projectiles.Clear();
            foreach (ProjectileBase projectile in gmProjectiles)
            {
                ProjectileData saveProjectile = new ProjectileData(projectile);
                data.projectiles.Add(saveProjectile);
            }
        }

        // Projectile ������ Load
        public void LoadProjectiles(List<ProjectileData> projectilesData)
        {
            foreach (ProjectileData projectile in projectilesData)
            {
                string prefabPath = $"Prefabs/Projectile/{projectile.name}";
                GameObject projectilePrefab = Resources.Load<GameObject>(prefabPath);

                if (projectilePrefab == null)
                {
                    Debug.LogError($"Failed to load tower prefab at path: {prefabPath}");
                    continue;
                }

                Instantiate(projectilePrefab, projectile.position, projectile.rotation);
            }
        }

        //�ε� ��ư
        public void LoadGameData()
        {
            Debug.Log("LOAD");
            // ������ �ε�
            data = DataManager.Instance.LoadGameData();

            // �� �ٽ� �ε�
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneLoaded += SetGameManager;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //Ÿ����
            //towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            //foreach (var tower in towerbase)
            //{
            //    Destroy(tower.gameObject);
            //}
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // �� �ε� �Ϸ� �� ������ ����
            SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ����
            SceneManager.sceneLoaded -= SetGameManager;

            LoadTowers(data.towers);
            LoadProjectiles(data.projectiles);

            //enemys
            EnemyState[] enemys = FindObjectsByType<EnemyState>(FindObjectsSortMode.None);
            foreach (EnemyState e in enemys)
            {
                if (e == null)
                {
                    continue;
                }
                //e.gameObject.transform
                //e.gameObject.GetComponent<EnemyMoveController>().enabled = false;

                Destroy(e.gameObject);
            }
            ListSpawnManager.enemyAlive = 0;

            //�ʵ忡 �ִ� �ڿ���
            DropItem[] item = FindObjectsByType<DropItem>(FindObjectsSortMode.None);
            foreach (DropItem e in item)
            {
                Destroy(e.gameObject);
            }
            //���� ���� ��
            listSpawnManager.waveCount = data.Round;
            listSpawnManager.countdown = data.countdown;
            //�÷��̾� �ڿ� ����
            health.maxHealth = data.health;
            playerState.money = data.money;
            playerState.tree = data.tree;
            playerState.rock = data.rock;
            //���׷��̵� �ܰ�
            castleUpgrade.currentHPUpgradeLevel = data.isHPUpgradelevel;
            castleUpgrade.currentHPTimeUpgradeLevel = data.isHPTimeUpgradelevel;
            castleUpgrade.currenteArmorUpgradeLevel = data.isArmorUpgradelevel;
            castleUpgrade.currentTowerATKUpgradeLevel = data.isATKUpgradelevel;
            castleUpgrade.currentTowerATKSpeedUpgradeLevel = data.isATKSpeedUpgradelevel;
            castleUpgrade.currentTowerATKRangeUpgradeLevel = data.isATKRangeUpgradelevel;
            castleUpgrade.currentMoneyGainUpgradeLevel = data.isMoneyGainlevel;
            castleUpgrade.currentTreeGainUpgradeLevel = data.isTreeGainlevel;
            castleUpgrade.currentRockGainUpgradeLevel = data.isRockGainlevel;
            //���׷��̵� �رݿ���
            castleUpgrade.isPotalActive = data.isPotalActive;
            castleUpgrade.isMoveSpeedUp = data.isMoveSpeedUp;
            castleUpgrade.isAutoGain = data.isAutoGain;
            //Ÿ�� �رݿ���

            build.towerinfo[3].isLock = data.isTowerUnlock1;
            build.towerinfo[6].isLock = data.isTowerUnlock2;
            build.towerinfo[9].isLock = data.isTowerUnlock3;
            build.towerinfo[12].isLock = data.isTowerUnlock4;
            build.towerinfo[15].isLock = data.isTowerUnlock5;
            build.towerinfo[18].isLock = data.isTowerUnlock6;
            build.towerinfo[21].isLock = data.isTowerUnlock7;
            //build.towerinfo[8].isLock = data.isTowerUnlock8;
            //build.towerinfo[9].isLock = data.isTowerUnlock9;
            //build.towerinfo[10].isLock = data.isTowerUnlock10;
            //build.towerinfo[11].isLock = data.isTowerUnlock11;
            //build.towerinfo[12].isLock = data.isTowerUnlock12;

            //Ÿ�� ��ư �ر�
            towerBuildMenuName.unlockTowerButton[0].interactable = data.isTowerUnlocked1;
            towerBuildMenuName.unlockTowerButton[1].interactable = data.isTowerUnlocked2;
            towerBuildMenuName.unlockTowerButton[2].interactable = data.isTowerUnlocked3;
            towerBuildMenuName.unlockTowerButton[3].interactable = data.isTowerUnlocked4;
            towerBuildMenuName.unlockTowerButton[4].interactable = data.isTowerUnlocked5;
            towerBuildMenuName.unlockTowerButton[5].interactable = data.isTowerUnlocked6;
            towerBuildMenuName.unlockTowerButton[6].interactable = data.isTowerUnlocked7;
            //towerBuildMenuName.unlockTowerButton[7].interactable = data.isTowerUnlocked8;
            //towerBuildMenuName.unlockTowerButton[8].interactable = data.isTowerUnlocked9;
            //towerBuildMenuName.unlockTowerButton[9].interactable = data.isTowerUnlocked10;
            //towerBuildMenuName.unlockTowerButton[10].interactable = data.isTowerUnlocked11;
            //towerBuildMenuName.unlockTowerButton[12].interactable = data.isTowerUnlocked12;


            //����
            //float masterSound = data.soundSettings[Constants.AUDIO_UTIL_MASTER];
            //Debug.Log(masterSound+"loaddata");
            //AudioUtility.SetVolume(masterSound, Constants.AUDIO_UTIL_MASTER);
            //audioManager.m_AudioMasterSlider.value = masterSound;

            //float bgmSound = data.soundSettings[Constants.AUDIO_UTIL_BGM];
            //AudioUtility.SetVolume(bgmSound, Constants.AUDIO_UTIL_BGM);
            //audioManager.m_AudioBGMSlider.value = bgmSound;

            //float effectSound = data.soundSettings[Constants.AUDIO_UTIL_EFFECT];
            //AudioUtility.SetVolume(effectSound, Constants.AUDIO_UTIL_EFFECT);
            //audioManager.m_AudioSFXSlider.value = effectSound;
            AudioUtility.SetVolume(data.soundData.masterVolumn, Constants.AUDIO_UTIL_MASTER);
            AudioUtility.SetVolume(data.soundData.bgmVolumn, Constants.AUDIO_UTIL_BGM);
            AudioUtility.SetVolume(data.soundData.sfxVolumn, Constants.AUDIO_UTIL_EFFECT);

            //�ͳθ�
            toggleButton.isOnto = data.isTuneeling;
            //�÷��̾�ui
            toggleButton.isOnPlay = data.isPlayerUI;
        }
        //�ٽ��ϱ� 
        public void RestartGame()
        {
            // ������ �ʱ�ȭ
            DataManager.Instance.SaveGameData(new Data());
            //���ٽý���
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // ���� ����
        public void QuitGame()
        {

            Application.Quit();
        }


        //ġƮ�� ��ư
        public void Cheating()
        {
            Debug.Log("CHEAT");
            playerState.money = 999999f;
            playerState.tree = 999999f;
            playerState.rock = 999999f;
            towerBuildMenuName.unlockTowerButton[0].interactable = true;
            towerBuildMenuName.unlockTowerButton[1].interactable = true;
            towerBuildMenuName.unlockTowerButton[2].interactable = true;
            towerBuildMenuName.unlockTowerButton[3].interactable = true;
            towerBuildMenuName.unlockTowerButton[4].interactable = true;
            towerBuildMenuName.unlockTowerButton[5].interactable = true;
            towerBuildMenuName.unlockTowerButton[6].interactable = true;
            build.towerinfo[3].isLock = true;
            build.towerinfo[6].isLock = true;
            build.towerinfo[9].isLock = true;
            build.towerinfo[12].isLock = true;
            build.towerinfo[15].isLock = true;
            build.towerinfo[18].isLock = true;
            build.towerinfo[21].isLock = true;

            dynamicMove.moveSpeed = 10f;
            //player.transform.position = new Vector3(0f, 4f, -6.0f);
        }

        //�÷��̾� ��ġ �� 
        public void PlayerTransformSenter()
        {
            player.gameObject.transform.position = new Vector3(0f, 4f, -6.0f);
        }

        public void AddTower(TowerBase go)
        {
            Debug.Log("addTower");
            towers.Add(go);
        }
        public void RemoveTower(TowerBase go)
        {
            Debug.Log("RemoveTower");
            towers.Remove(go);
        }
        public void AddProjectile(ProjectileBase go)
        {
            Debug.Log("addPorjectile");
            projectiles.Add(go);
        }
        public void RemoveProjectile(ProjectileBase go)
        {
            Debug.Log("RemoveProjectile");
            projectiles.Remove(go);
        }
        public void SetGameManager(Scene scene, LoadSceneMode mode)
        {
            StartCoroutine(InitializeGameManager());
        }

        public IEnumerator InitializeGameManager()
        {
            yield return new WaitForSeconds(0.5f); // 0.5�� ���
            castle = FindAnyObjectByType<HealthBasedCastle>();
            player = FindAnyObjectByType<XROrigin>();
            dynamicMove = FindAnyObjectByType<DynamicMoveProvider>();
            health = castle.GetComponent<Health>();
            playerState = FindAnyObjectByType<PlayerState>();
            castleUpgrade = FindAnyObjectByType<CastleUpgrade>();
            listSpawnManager = FindAnyObjectByType<ListSpawnManager>();
            build = FindAnyObjectByType<BuildMenu>();
            towerBuildMenuName = FindAnyObjectByType<TowerBuildMenuName>();
            healthBasedCastle = FindAnyObjectByType<HealthBasedCastle>();
            toggleButton = FindAnyObjectByType<ToggleButton>();
            buildManager = BuildManager.instance;
            buildMenu = FindAnyObjectByType<BuildMenu>();
        }
    }
}


