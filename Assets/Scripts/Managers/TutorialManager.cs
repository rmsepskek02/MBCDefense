using Defend.Enemy;
using Defend.TestScript;
using Defend.Tower;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
/// <summary>
/// Tutorial Scene�� �����ϴ� Manager
/// </summary>
namespace Defend.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        #region Variables

        #region UI
        public Canvas tutorialCanvas;
        public GameObject backgroundUI;
        public TextMeshProUGUI guideText;
        public Button hideButton;
        public Button showButton;
        public GameObject endTutorialUI;
        public Button nextButton;
        public Button retryButton;
        public GameObject buildUI;
        public TMP_SpriteAsset axeSpriteAsset;
        public TMP_SpriteAsset pickaxSpriteAsset;
        public TMP_SpriteAsset handSpriteAsset;
        #endregion

        public GameObject player;               // Player
        public GameObject playerDummy;          // PlayerDummy TopView �νĿ� Obj
        public GameObject rock;                 // Ʃ�丮��� rock
        public GameObject tree;                 // Ʃ�丮��� tree
        public GameObject castle;               // Ʃ�丮��� castle
        public GameObject axe;                  // �÷��̾� Axe
        public GameObject PickAxe;              // �÷��̾� PickAxe
        public ListSpawnManager lsm;            // ListSpawnManager
        public float fontSize;                  // guideText font size
        public InputActionProperty leftXButton; // LeftHandController 'X'
        public InputActionProperty leftYButton; // LeftHandController 'Y'
        public InputActionProperty leftMenuButton; // LeftHandController 'Menu'
        private string guideString;             // UI�� ��Ÿ���� ����
        private UnityAction buttonAnimAction;   // ShowButton Animation UnityAction
        CinemachineBrain cmb;                   // Player�� CinemachineBrain
        [SerializeField] private string isNewGuide = "IsNewGuide"; // Animation bool ����
        [SerializeField] private string loadToScene; // ���� �� �ε� �� Scene

        #region Step ���� Bool Variables
        // TopVIew Ȯ���ϱ�
        private bool isA = true;
        // ��̷� �ٲٱ�
        private bool isB = false;
        // ä���ϱ�
        private bool isC = false;
        // �����ϱ�
        private bool isD = false;
        // User UI ����
        private bool isE = false;
        // ���� �����ϱ�
        private bool isF = false;

        UnityAction endTutorial;
        Animator animator;
        Health health;                           // castle�� health ����
        #endregion

        #endregion
        void Start()
        {
            health = castle.GetComponent<Health>();
            animator = showButton.GetComponent<Animator>();
            endTutorial += EndUI;
            guideText.fontSize = fontSize;
            buttonAnimAction += playShowButtonAnim;
            cmb = player.GetComponent<CinemachineBrain>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) || leftXButton.action.WasCompletedThisFrame() || leftMenuButton.action.WasCompletedThisFrame())
            {
                HideUI();
            }

            //if (leftYButton.action.WasCompletedThisFrame())
            //{
            //    HideUIWhenViewChange();
            //}

            // Step.A TopView Ȯ���ϱ�
            if (isA == true)
            {
                AChangeTopView();
                if (playerDummy.activeSelf == true)
                {
                    isA = false;
                    isB = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.B ��̷� ���� �ٲٱ�
            if (isB == true)
            {
                BChangeToPickax();
                if (PickAxe.activeSelf == true)
                {
                    isB = false;
                    isC = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.C ��̷� ä���ϱ�
            if (isC == true)
            {
                CMiningRock();
                // Ʃ�丮�� Rock�� ����� ���
                if (rock == null)
                {
                    isC = false;
                    isD = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.D ������ �����ϱ�
            if (isD == true)
            {
                DLoggingTree();
                // Ʃ�丮�� Tree�� ����� ���
                if (tree == null)
                {
                    isD = false;
                    isE = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.E User UI ���� Build �����ϰ� Ÿ�� �Ǽ��ϱ�
            if (isE == true)
            {
                EShowUserUI();
                TowerBase go = FindFirstObjectByType<TowerBase>();
                // Ÿ���� �ִ� ���
                if (go != null)
                {
                    isE = false;
                    isF = true;
                    buttonAnimAction.Invoke();
                }
            }

            // Step.F �޼� UI show ��ư Ŭ�� �� skip
            if (isF == true)
            {
                FSkipRoundTimer();
                // �޼տ� SHOW ��ư���� UI �Ѱ� SKIP ��ư���� ���� ����
                if (lsm.waveCount > 0)
                {
                    isF = false;
                    guideString = $"Protect the castle from the enemy";
                    guideText.text = guideString;
                }
            }

            // ���� �ν����ų� enemy�� ���� ��� Ʃ�丮�� ����
            if (health.CurrentHealth <= 0 || (lsm.waveCount > 0 && ListSpawnManager.enemyAlive == 0))
            {
                endTutorial.Invoke();
            }
        }
        // Step.A TopView Ȯ���ϱ�
        void AChangeTopView()
        {
            guideString = $"Press the <color=#FF0000>Y</color>-Action button to see the entire map";
            guideText.text = guideString;
        }

        // Step.B ��̷� ���� �ٲٱ�
        void BChangeToPickax()
        {
            guideText.spriteAsset = pickaxSpriteAsset;
            guideString = $"Press the <color=#FF0000>A</color>-Action button to change the   <size=12><sprite=0>";
            guideText.text = guideString;
        }

        // Step.C ��̷� ä���ϱ�
        void CMiningRock()
        {
            guideString = $"Use    <size=12><sprite=0><size={fontSize}>to mine the rock";
            guideText.text = guideString;
        }

        // Step.D ������ �����ϱ�
        void DLoggingTree()
        {
            guideText.spriteAsset = axeSpriteAsset;
            guideString = $"Change equipment into  <size=12><sprite=0><size={fontSize}> and logging";
            guideText.text = guideString;
        }

        // Step.E User UI ���� Build �����ϰ� Ÿ�� �Ǽ��ϱ�
        void EShowUserUI()
        {
            guideText.spriteAsset = handSpriteAsset;
            guideString = $"Change to    <size=12><sprite=0><size={fontSize}>\nPress the <color=#FF0000>X</color>-Action button to show the UI\nSelect Build, and use the Grab button to build a tower";
            guideText.text = guideString;
        }

        // Step.F �޼� UI show ��ư Ŭ�� �� skip
        void FSkipRoundTimer()
        {
            guideString = "Turn on UI through <color=#FF0000>Show</color> button attached to left hand\n Start the round through the <color=#FF0000>Skip</color> button";
            guideText.text = guideString;
        }

        // Show UI
        public void ShowUI()
        {
            backgroundUI.SetActive(true);
            showButton.gameObject.SetActive(false);
            animator.SetBool(isNewGuide, false);
        }

        // Hide Guide UI
        public void HideUI()
        {
            backgroundUI.SetActive(false);
            showButton.gameObject.SetActive(true);
        }

        // ViewChange�� ���� UI
        //public void HideUIWhenViewChange()
        //{
        //    if (cmb.enabled == false)
        //    {
        //        backgroundUI.SetActive(false);
        //        showButton.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        backgroundUI.SetActive(false);
        //        showButton.gameObject.SetActive(true);
        //    }
        //}

        // End UI
        public void EndUI()
        {
            backgroundUI.SetActive(false);
            showButton.gameObject.SetActive(false);
            endTutorialUI.SetActive(true);
        }

        // Next
        public void OnClickNext()
        {
            SceneManager.LoadScene(loadToScene);
        }

        // Retry 
        public void OnClickRetry()
        {
            // ���� Ȱ��ȭ�� ���� �̸� ��������
            string currentSceneName = SceneManager.GetActiveScene().name;
            // �� �ٽ� �ε�
            SceneManager.LoadScene(currentSceneName);
        }

        // ShowButtonAnim ���
        public void playShowButtonAnim()
        {
            animator.SetBool(isNewGuide, !backgroundUI.activeSelf);
        }
    }
}

