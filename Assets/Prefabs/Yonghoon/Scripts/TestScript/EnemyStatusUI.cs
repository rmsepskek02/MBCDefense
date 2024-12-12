using Defend.TestScript;
using Defend.Utillity;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HealthBar �� ��� ����
/// </summary>
namespace Defend.UI
{
    public class EnemyStatusUI : MonoBehaviour
    {
        #region Variables
        public Transform target;              // Player
        public GameObject healthBar;          // ü�¹� Transform
        public Image fillHealth;              // ü�¹�
        public TextMeshProUGUI healthText;    // ü�� �ؽ�Ʈ
        public GameObject buffsFisrt;         // ����â1
        public GameObject buffsSecond;        // ����â2

        //������ �޾ƿ� ����
        private Health health;
        #endregion

        void Start()
        {
            health = GetComponentInParent<Health>();

            health.OnDamaged += SetHealthUI;
            health.OnHeal += SetHealthUI;
            health.OnDie += DisableUI;

            //UI �ʱ�ȭ
            //healthText.text = $"{Mathf.Round(health.CurrentHealth)}/{health.MaxHealth}";
            healthText.text = $"{Mathf.Round(health.CurrentHealth)}/{health.maxHealth}";
            fillHealth.fillAmount = health.GetRatio();
            gameObject.SetActive(false);

            if (target == null)
            {
                // TODO :: Player�� �ٶ������
                target = Camera.main.transform;
            }
        }

        //������ UI ��Ȱ��ȭ
        private void DisableUI()
        {
            gameObject.SetActive(false);
        }

        //���̳� �������� ������ amount�� ����� �޾ƿͼ� text�� fillAmount���� ����
        private void SetHealthUI(float amount)
        {
            gameObject.SetActive(true);
            healthText.text = $"{Mathf.Round(health.CurrentHealth)}/{health.maxHealth}";
            fillHealth.fillAmount = health.GetRatio();
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position + target.forward);
        }
    }
}
