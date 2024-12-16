using Defend.Enemy;
using Defend.Manager;
using Defend.Player;
using Defend.TestScript;
using MyVrSample;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
namespace Defend.Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        //����
        PlayerState playerState;
        ListSpawnManager ListSpawnManager;
        CastleUpgrade castleUpgrade;
        Health health;
        public GameObject player;
        public GameObject castle;
        //ui
        public GameObject clearUI;
        public GameObject gameoverUI;

        //sfs
        private AudioSource audioSource;
        public AudioClip clearSound;
        public AudioClip gameoverSound;

        //�׽�Ʈ�� �ð���
        public TextMeshProUGUI testtext;
        float time;

        //public Button saveButton; // ���̺� ��ư
        public Data data = new Data(); // ���� ������


        #endregion

        private void Start()
        {
            //����
            health = castle.GetComponent<Health>();
            playerState = Object.FindAnyObjectByType<PlayerState>();
            castleUpgrade = Object.FindAnyObjectByType<CastleUpgrade>();
            //�������ڸ��� �ҷ�����
            //LoadGameData();
            //playerState.money = data.money;

            //....
            //...



            if (!audioSource)
                return;
            audioSource = player.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // �ڵ� ��� ����

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

            time += Time.deltaTime;
            testtext.text = time.ToString("F1");
        }

        //���� ����� �ڵ�����
        private void OnApplicationQuit()
        {
            DataManager.Instance.SaveGameData(data);
        }

        public void OnSaveButtonClicked()
        {
            SaveGameData();
        }


        //���̺� ��ư��
        public void SaveGameData()
        {
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

            // ������ ����=
            DataManager.Instance.SaveGameData(data);
        }

        //�ε� ��ư
        public void LoadGameData()
        {
            // ������ �ε�
            DataManager.Instance.LoadGameData();

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
        }

        //�������ʱ�ȭ
        public void DeleteGameData()
        {
            data = null;
            //�����ͻ���
            DataManager.Instance.DeleteGameData(data);
        }

        //����Ŭ����
        IEnumerator GameClear()
        {

            if (ListSpawnManager.enemyAlive <= 0 && ListSpawnManager.waveCount >= 5)
            {
                //����Ŭ���� â �߱�
                clearUI.SetActive(true);

                //Ŭ���� ����
                audioSource.clip = clearSound;
                audioSource.Play();


                yield return new WaitForSeconds(3f);

                audioSource.Stop();
            }
        }
        //���ӿ���
        IEnumerator GameOver()
        {

            if (playerState.health <= 0)
            {
                //���ӿ��� â ����
                gameoverUI.SetActive(true);
                //���ӿ��� ����
                audioSource.clip = gameoverSound;
                audioSource.Play();
                yield return new WaitForSeconds(3f);
                audioSource?.Stop();
            }
        }
    }
}


