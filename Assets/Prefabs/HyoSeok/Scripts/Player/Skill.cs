using UnityEngine;
using Defend.Interactive;
using System.Collections;
using Defend.Enemy;
using Defend.Tower;
using TMPro;
using UnityEngine.UI;
using Defend.Utillity;
using Defend.Player;
using Unity.VisualScripting;
using Defend.TestScript;
using System.Net;

namespace Defend.UI
{
    public class Skill : MonoBehaviour
    {
        #region Variables
        TowerBase[] towerbase;
        //Status[] status;
        //����
        private EnemyMoveController moveController;

        //ĵ����
        public GameObject canvas;
        //
        public GameObject player;

        //��Ÿ��
        public GameObject[] coolTimeUI;        //��Ÿ���̹���
        public TextMeshProUGUI[] coolTimeText; //��Ÿ�� �ð�
        [SerializeField] float magnetCoolTime = 60f;
        [SerializeField] float timeStopCoolTime = 120f;
        [SerializeField] float atkSpeedUpCoolTime = 180f;

        private bool[] isCooldown;
        public Button[] skillButtons;
        //�ڼ��� ����
        private float originalMagnetSpeed;
        private float originalMagnetDistance;

        //Ÿ�ӽ�ž ����
        private GameObject[] enemies;

        EnemyState[] enemyState;

        //Ÿ�����Ӿ� ����
        //private float originalshootDelay;

        //����
        public AudioClip magnetSound;
        private AudioSource audioSource;

        //����Ʈ
        public GameObject magnetEffectPrefab;   //�ڼ�����
        #endregion

        void Start()
        {

            moveController = FindFirstObjectByType<EnemyMoveController>();
            audioSource = player.AddComponent<AudioSource>();

            isCooldown = new bool[coolTimeUI.Length];
            audioSource.clip = magnetSound;
            audioSource.playOnAwake = false; // �ڵ� ��� ����

        }
        //�ʵ��� �ڿ� ��� ����ϴ� ��ų
        public void MagnetPlay()
        {

            StartCoroutine(Magnet());

        }


        public IEnumerator Magnet()
        {

            //canvas.SetActive(false);
            //������� 
            GameObject magnetEffect = Instantiate(magnetEffectPrefab, player.transform.position + transform.forward, Quaternion.identity);
            magnetEffect.transform.SetParent(transform);
            //���� ����
            audioSource.Play();
            //yield return new WaitForSeconds(1f);    //������ �ð�


            originalMagnetSpeed = ResourceManager.speed;
            originalMagnetDistance = ResourceManager.distance;
            ResourceManager.speed = 20f;
            ResourceManager.distance = 500f;
            yield return new WaitForSeconds(3f);
            //���� ����
            Destroy(magnetEffect);
            //���� ����
            audioSource.Stop();
            ResourceManager.speed = originalMagnetSpeed;
            ResourceManager.distance = originalMagnetDistance;
        }

        //���� ���� ��ų
        public void TimeStopPlay()
        {
            StartCoroutine(TimeStop());
        }

        public IEnumerator TimeStop()
        {
            EnemyState[] enemys = FindObjectsByType<EnemyState>(FindObjectsSortMode.None);

            
            foreach (EnemyState e in enemys)
            {
                if (e == null)
                {
                    continue; 
                }
                e.gameObject.GetComponent<EnemyMoveController>().enabled = false;
              
                e.gameObject.GetComponent<Animator>().speed=0.01f;
                Debug.Log($"e={e.gameObject}");
                ////������� 
                GameObject magnetEffect = Instantiate(magnetEffectPrefab, e.transform.position + transform.forward, Quaternion.identity);
                magnetEffect.transform.SetParent(transform);
                //���� ����
                Destroy(magnetEffect, 3f);
            }
         
            yield return new WaitForSeconds(3f);

            foreach (EnemyState e in enemys)
            {
                if (e == null)
                {
                    continue;
                }
                e.gameObject.GetComponent<EnemyMoveController>().enabled = true;
              
                e.gameObject.GetComponent<Animator>().speed = 1f;

            }
     
            //���� ����
            audioSource.Stop();
        }
        //Ÿ�� ���� ��
        public void TowerAtkSpeedPlay()
        {

            StartCoroutine(TowerAttakSpeed());

        }

        public IEnumerator TowerAttakSpeed()
        {
            //������� 
            GameObject magnetEffect = Instantiate(magnetEffectPrefab, player.transform.position + transform.forward, Quaternion.identity);
            magnetEffect.transform.SetParent(transform);
            //���� ����
            audioSource.Play();
            if (CastleUpgrade.buffContents != null)
            {
                CastleUpgrade.buffContents.duration = 5f;
                CastleUpgrade.buffContents.atk = 0f;
                CastleUpgrade.buffContents.armor = 0f;
                CastleUpgrade.buffContents.healthRegen = 1f;
                CastleUpgrade.buffContents.manaRegen = 1f;
                CastleUpgrade.buffContents.atkRange = 1f;
                CastleUpgrade.buffContents.shootDelay = 5f;
            }

            towerbase = FindObjectsByType<TowerBase>(FindObjectsSortMode.None);
            foreach (var tower in towerbase)
            {
                tower.BuffTower(CastleUpgrade.buffContents, false);
            }
            yield return new WaitForSeconds(3f);

            //���� ����
            Destroy(magnetEffect);
            //���� ����
            audioSource.Stop();
        }

        //�����
        public void StartCooldown(int skillIndex)
        {
            if (isCooldown[skillIndex]) return;

            isCooldown[skillIndex] = true;
            skillButtons[skillIndex].interactable = false;
            StartCoroutine(SkillCoolDown(skillIndex));
        }

        //��ų ��ٿ�
        IEnumerator SkillCoolDown(int skillIndex)
        {
            float cooldownTime = 0f;


            switch (skillIndex)
            {
                case 0:
                    cooldownTime = magnetCoolTime;
                    break;
                case 1:
                    cooldownTime = timeStopCoolTime;
                    break;
                case 2:
                    cooldownTime = atkSpeedUpCoolTime;
                    break;
            }

            //UI ������Ʈ
            float elapsedTime = 0f;
            while (elapsedTime < cooldownTime)
            {
                elapsedTime += Time.deltaTime;
                float fillAmount = Mathf.Clamp01(1 - (elapsedTime / cooldownTime));
                coolTimeUI[skillIndex].GetComponent<Image>().fillAmount = fillAmount;
                coolTimeText[skillIndex].text = Mathf.Ceil(cooldownTime - elapsedTime).ToString();

                yield return null;
            }

            //UI �ʱ�ȭ
            coolTimeUI[skillIndex].GetComponent<Image>().fillAmount = 1;
            coolTimeText[skillIndex].text = "";
            skillButtons[skillIndex].interactable = true;
            isCooldown[skillIndex] = false;
        }

        public void OnSkillButtonClick(int skillIndex)
        {
            StartCooldown(skillIndex);

        }
    }



}


