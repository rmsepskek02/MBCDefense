using Defend.TestScript;
using Defend.Tower;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        public TMP_SpriteAsset axeSpriteAsset;
        public TMP_SpriteAsset pickaxSpriteAsset;
        public TMP_SpriteAsset handSpriteAsset;
        #endregion

        public GameObject player;
        public GameObject rock;                 // Ʃ�丮��� rock
        public GameObject tree;                 // Ʃ�丮��� tree
        public GameObject castle;               // Ʃ�丮��� castle
        public GameObject enemy;                // Ʃ�丮��� enemy
        private string guideString;             // UI�� ��Ÿ���� ����
        private Health health;                  // castle�� health ����
        public float fontSize;                  // guideText font size

        #region Step ���� Bool Variables
        // ��̷� �ٲٱ�
        private bool isA = true;
        // ä���ϱ�
        private bool isB = false;
        // �����ϱ�
        private bool isC = false;
        // User UI ����
        private bool isD = false;
        // ���� �����ϱ�
        private bool isE = false;
        #endregion

        #endregion
        void Start()
        {
            health = castle.GetComponent<Health>();
        }

        // TODO :: SHOW ��ư ��¦�Ÿ���, ��ġ ���
        // TODO :: ž�� �غ���
        void Update()
        {
            guideText.fontSize = fontSize;
            // TODO :: UI ��ȣ�ۿ� �ϴ¹�

            // Step.A ��̷� ���� �ٲٱ�
            if (isA == true)
            {
                AChangeToPickax();
                // TODO :: Player �տ� ��̰� ���� ���
                if (Input.GetKeyDown(KeyCode.A))
                {
                    isA = false;
                    isB = true;
                }
            }

            // Step.B ��̷� ä���ϱ�
            if (isB == true)
            {
                BMiningRock();
                // Ʃ�丮�� Rock�� ����� ���
                if (Input.GetKeyDown(KeyCode.B))
                //if (rock == null)
                {
                    isB = false;
                    isC = true;
                }
            }

            // Step.C ������ �����ϱ�
            if (isC == true)
            {
                CLoggingTree();
                if (Input.GetKeyDown(KeyCode.C))
                // Ʃ�丮�� Tree�� ����� ���
                //if (tree == null)
                {
                    isC = false;
                    isD = true;
                }
            }

            // Step.D User UI ���� Build �����ϰ� Ÿ�� �Ǽ��ϱ�
            if (isD == true)
            {
                DShowUserUI();
                TowerBase go = FindFirstObjectByType<TowerBase>();
                if (Input.GetKeyDown(KeyCode.D))
                // Ÿ���� �ִ� ���
                //if(go !=null)
                {
                    isD = false;
                    isE = true;
                }
            }

            // Step.E �޼� UI show ��ư Ŭ�� �� skip
            if (isE == true)
            {
                ESkipRoundTimer();
                if (Input.GetKeyDown(KeyCode.F))
                // �޼տ� SHOW ��ư���� UI �Ѱ� SKIP ��ư���� ���� ����
                // TODO :: Waypoint �����
                //if (enemy.activeSelf == true)
                {
                    isE = false;
                }
            }

            // ���� �ν����ų� enemy�� �ı��� ��� Ʃ�丮�� ����
            if (health.CurrentHealth <= 0 || enemy == null)
            {
                // TODO :: ���� Scene �̵�
            }
        }

        // Step.A ��̷� ���� �ٲٱ�
        void AChangeToPickax()
        {
            // TODO :: ��� �ٲ�� �� Ȯ��, ���� Ű ������ 
            guideText.spriteAsset = pickaxSpriteAsset;
            guideString = $"Press the <color=#FF0000>Y</color>-Action button to change the   <size=12><sprite=0>";
            guideText.text = guideString;
        }

        // Step.B ��̷� ä���ϱ�
        void BMiningRock()
        {
            // TODO :: ä���Ǵ��� Ȯ�� 
            guideString = $"Use    <size=12><sprite=0><size={fontSize}>to mine the rock";
            guideText.text = guideString;
        }

        // Step.C ������ �����ϱ�
        void CLoggingTree()
        {
            // TODO :: ����Ǵ��� Ȯ�� 
            guideText.spriteAsset = axeSpriteAsset;
            guideString = $"Change equipment into  <size=12><sprite=0><size={fontSize}> and logging";
            guideText.text = guideString;
        }

        // Step.D User UI ���� Build �����ϰ� Ÿ�� �Ǽ��ϱ�
        void DShowUserUI()
        {
            // TODO :: Ÿ�� �Ǽ��ϴ¹� �̽��ϱ�, ���� Ű ������
            guideText.spriteAsset = axeSpriteAsset;
            //guideText.spriteAsset = handSpriteAsset;
            guideString = $"Change to <size=12><sprite=0><size={fontSize}>\nPress the <color=#FF0000>Y</color>-Action button to show the UI\nSelect Build , and build a tower";
            guideText.text = guideString;
        }

        // Step.E �޼� UI show ��ư Ŭ�� �� skip
        void ESkipRoundTimer()
        {
            // TODO :: �޼� UI �۵�Ȯ��, TIMER �߰��ϱ�, SKIP �۵� Ȯ���ϱ�, Enemy Ȱ��ȭ
            // SKIP�� �ܼ� Enemy Ȱ��ȭ�� �ص� ����
            guideString = "Turn on UI through <color=#FF0000>Show</color> button attached to left hand\n Start the round through the <color=#FF0000>Skip</color> button";
            guideText.text = guideString;
        }

        // Show UI
        public void ShowUI()
        {
            backgroundUI.SetActive(true);
            showButton.gameObject.SetActive(false);
        }
        // Hide UI
        public void HideUI()
        {
            backgroundUI.SetActive(false);
            showButton.gameObject.SetActive(true);
        }
    }
}
