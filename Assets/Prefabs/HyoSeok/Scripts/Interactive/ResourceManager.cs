using Defend.Player;
using UnityEngine;

namespace Defend.Interactive
{
    public class ResourceManager : MonoBehaviour
    {
        #region
        public static ResourceManager Instance;

        private PlayerState playerState;

        // �ڿ� �߰� �̺�Ʈ
        public delegate void ResourceAddedHandler(float amount, string resourceType);
        public event ResourceAddedHandler OnResourceAdded;

        // �⺻ �ڿ� ȹ�淮
        private float rockmultiplier = 1.0f;
        private float treemultiplier = 1.0f;
        private float moneymultiplier = 1.0f;
        #endregion
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                playerState = Object.FindAnyObjectByType<PlayerState>(); // PlayerState�� ã��
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // �ڿ� �߰�
        public void AddResources(float amount, string resourceType)
        {

            switch (resourceType.ToLower())
            {
                case "rock":
                  
                    playerState.AddRock(amount* rockmultiplier);
                    break;
                case "tree":
                 
                    playerState.AddTree(amount* treemultiplier);
                    break;
                case "money":
                 
                    playerState.AddMoney(amount * moneymultiplier);
                    break;

            }
            // �ڿ� �߰� �̺�Ʈ �߻�
            OnResourceAdded?.Invoke(amount, resourceType);
        }

        // �ڿ� ȹ�淮 ���׷��̵�
        public void UpgradeResourceGain(string resourceType, float multiplier)
        {
         
            switch (resourceType.ToLower())
            {
                case "rock":
                    rockmultiplier *= multiplier;
                    Debug.Log($"rockamout = {moneymultiplier}");
                    break;
                case "tree":
                    treemultiplier *= multiplier;
                    Debug.Log($"treeAmount = {moneymultiplier}");
                    break;
                case "money":
                    moneymultiplier *= multiplier;
                    Debug.Log($"moneyAmount = {moneymultiplier}");
                    break;
            }
        }
    }
}