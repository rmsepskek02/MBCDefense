using Defend.Tower;
using Defend.Utillity;
using TMPro;
using Unity.XR.CoreUtils;
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
        TowerBase tb;
        #endregion

        void Start()
        {
            status.OnDamaged += SetFillHealth;
            status.OnDamaged += SetHealthText;
            status.OnUseMana += SetFillMana;
            status.OnUseMana += SetManaText;
            status.OnDamaged.Invoke();
            status.OnUseMana.Invoke();
            if (target == null)
            {
                target = FindFirstObjectByType<XROrigin>().gameObject.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // target ��ġ���� ���� ������Ʈ�� ��ġ�� �� ���� ���͸� ���
            Vector3 direction = target.position - transform.position;

            // UI�� Z���� �ƴ϶� �ٸ� ��(��: Y��)���� �ٶ󺸰� ����
            Quaternion rotation = Quaternion.LookRotation(-direction); // Z�� ������ ���� ���� ���
            transform.rotation = rotation;
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
