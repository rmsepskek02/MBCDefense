using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class Tile : MonoBehaviour
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        [HideInInspector]public GameObject tower;
        [HideInInspector] public GameObject tower_upgrade;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        public TowerInfo[] towerInfo;
        private Image[] towerimage;

        private Vector3 offset;

        //����Ŵ��� ��ü
        private BuildManager buildManager;
        //��ġ�ϸ� �����Ǵ� ����Ʈ
        public GameObject TowerImpectPrefab;
        //�÷��̾� ��ġ
        public Transform head;
        //Ÿ�� ���׷��̵� ����
        public bool IsUpgrade { get; private set; }
        [SerializeField] private float distance = 1.5f;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ

            buildManager = BuildManager.Instance;
        }
        //Ÿ�� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            return head.position + new Vector3(head.forward.x,0,head.forward.z).normalized * distance;
        }
        //Ÿ�� ����
        public void BuildTower(Vector3 size, Vector3 center)
        {
            //��ġ�� �ͷ��� �Ӽ��� �������� (�ͷ� ������, �Ǽ����, ���׷��̵� ������, ���׷��̵� ���...)
            towerInfo[1] = buildManager.GetTowerToBuild();
            
            //���� �����Ѵ� 100, 250
            //Debug.Log($"�ͷ� �Ǽ����: {blueprint.cost}");
            //Ÿ�� ����
            tower = Instantiate(towerInfo[1].upgradeTower, GetBuildPosition(), Quaternion.identity);
            //Ÿ���� ���� �� �ִ� ������Ʈ �߰�
            tower.AddComponent<BoxCollider>();
            tower.AddComponent<TowerXR>();
            XRGrabInteractable xRGrabInter = tower.GetComponent<TowerXR>();

            BoxCollider boxCollider = tower.GetComponent<BoxCollider>();
            boxCollider.size = size;
            boxCollider.center = center;
            //Ÿ�� ���� ����Ʈ
            /*GameObject effgo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
            //Ÿ�� �ڽ����� ����
            effgo.transform.parent = transform;

            Destroy(effgo, 1.5f);*/
        }
        public void SellTower()
        {
            /*//���׷��̵� �ͷ��� �Ǹ�
            if (turret_upgrade != null)
            {
                Destroy(turret_upgrade);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //���׷��̵��ͷ����� �ݰ����� �Ǹ�
                PlayerStats.AddMoney(blueprint.Getupgradecost());
            }*/
            //�⺻ �ͷ��� �Ǹ�
            if (tower != null)
            {
                Destroy(tower);
                //IsUpgrade = false;
                //GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effect, 2f);
                //�⺻�ͷ����� �ݰ����� �Ǹ�
                buildManager.playerState.AddMoney(1);
            }
        }

        public void UpgradeTower(Vector3 size, Vector3 center)
        {
            
            if (tower == null)
            {
                Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if(tower)
            {
                towerInfo[2] = buildManager.GetTowerToBuild();
                Debug.Log("�ͷ� ���׷��̵�");
                //Effect
                //GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                //Destroy(effectGo, 2f);

                //�ͷ� ���׷��̵� ����
                IsUpgrade = true;

                //�ͷ� ���׷��̵� ����
                tower_upgrade = Instantiate(towerInfo[2].upgradeTower, tower.transform.position, Quaternion.identity);
                tower_upgrade.AddComponent<BoxCollider>();
                tower_upgrade.AddComponent<TowerXR>();
                BoxCollider boxCollider = tower.GetComponent<BoxCollider>();
                boxCollider.size = size;
                boxCollider.center = center;
                Destroy(tower);
                tower = tower_upgrade;
                tower_upgrade = null;
            }
        }
    }
}