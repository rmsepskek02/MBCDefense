using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    public class WarningWindow : MonoBehaviour
    {
        #region Variables
        public TextMeshProUGUI WarningText;
        public Button confirmButton;
        public Button cancelButton;
        public GameObject WarningUI;
        #endregion
        /*���â �����ִ� �Լ� (������ �ؽ�Ʈ, Ȯ�� ��ư Ŭ�� �� ����� ����, ��� ��ư Ŭ�� �� ����� ����,
        Ȯ�� ��ư Ŭ�� �� ����� ���� �� ��� ��ư Ŭ�� �� ����� ������ ������ UI �����ִ� �ð�)*/
        public void ShowWarning(string message, System.Action onConfirm = null, System.Action onCancel = null,float show = 3)
        {
            WarningText.text = message;
            confirmButton.onClick.AddListener(() =>
            {
                // Ȯ�� ��ư Ŭ�� �� ����� ����
                onConfirm?.Invoke();
                Hide();
            });
            cancelButton.onClick.AddListener(() =>
            {
                // ��� ��ư Ŭ�� �� ����� ����
                onCancel?.Invoke();
                Hide();
            });

            cancelButton.gameObject.SetActive(onCancel != null);
            confirmButton.gameObject.SetActive(onConfirm != null);
            if(onCancel == null && onConfirm ==null)
            {
                StartCoroutine(ShowWindow(show));
                return;
            }
            WarningUI.SetActive(true);
            // ���⿡ ���â ��Ÿ�� �� �ð� ȿ�� �߰� (��: �ִϸ��̼�)
        }

        public void Hide()
        {
            WarningUI.SetActive(false);
            // ���⿡ ���â ����� �� �ð� ȿ�� �߰� (��: �ִϸ��̼�)
        }
        IEnumerator ShowWindow(float showtime)
        {
            WarningUI.SetActive(true);
            yield return new WaitForSeconds(showtime);
            WarningUI.SetActive(false);
        }
    }
}