using Defend.Tower;
using Defend.Utillity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HealthBar �� ��� ����
/// </summary>
namespace Defend.UI
{
    public class StatusUI : MonoBehaviour
    {
        #region Variables
        public Transform target;                // Player
        public GameObject healthBar;            // ü�¹� Transform
        public Image fillHealth;                // ü�¹�
        public TextMeshProUGUI healthText;      // ü�� �ؽ�Ʈ
        public GameObject manaBar;              // ������ Transform
        public Image fillMana;                  // ������
        public TextMeshProUGUI manaText;        // ���� �ؽ�Ʈ
        public GameObject buffsFisrt;           // ����â1
        public GameObject buffsSecond;          // ����â2
        [SerializeField] private Status status; // ���¸� �޾ƿ� ��ũ��Ʈ
        #endregion

        void Start()
        {
            if (target == null)
            {
                // TODO :: Player�� �ٶ������
                target = Camera.main.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position + target.forward);
            // TODO :: ��������Ʈ ����ؼ� �ѱ���
            SetFillHealth();
            SetHealthText();
            SetFillMana();
            SetManaText();
        }

        void SetFillHealth()
        {
            fillHealth.fillAmount = (status.CurrentHealth / status.MaxHealth);
        }
        void SetHealthText()
        {
            healthText.text = $"{Mathf.Round(status.CurrentHealth)}/{status.MaxHealth}";
        }
        void SetFillMana()
        {
            fillMana.fillAmount = (status.CurrentMana / status.MaxMana);
        }
        void SetManaText()
        {
            manaText.text = $"{Mathf.Round(status.CurrentMana)}/{status.MaxMana}";
        }
    }
}
