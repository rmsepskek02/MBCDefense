using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Defend.UI
{
    [System.Serializable]
    public class Upgrade
    {
        #region Variables
        //���׷��̵� UI �� ���׷��̵� �� Ÿ�� ���� �ؽ�Ʈ
        public Image image;
        public TextMeshProUGUI name;
        public TextMeshProUGUI Buycost;
        public TextMeshProUGUI Sellcost;
        public TextMeshProUGUI Hp;
        public TextMeshProUGUI Mp;
        public TextMeshProUGUI Attack;
        public TextMeshProUGUI AttackSpeed;
        public TextMeshProUGUI AttackRange;
        public TextMeshProUGUI UpgradeMoney;
        #endregion
    }
}