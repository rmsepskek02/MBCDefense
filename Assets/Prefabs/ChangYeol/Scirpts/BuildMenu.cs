using Defend.Tower;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;

namespace Defend.UI
{
    public class BuildMenu : MonoBehaviour
    {
        #region Variables
        private BuildManager buildManager;
        //Ÿ������ ������
        public TowerInfo[] towerinfo;
        public Sprite[] towerSprite;
        public List<BoxCollider> boxes;
        private GameObject tower;

        public Tile tile;

        public GameObject BuildUI;

        private BoxCollider Trirggerbox;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
            //�÷��̾�� box�ݶ��̴� �߰��ϰ� trigger�� �ֺ��� �ִ� �ݶ��̴��� ������ ���â�� ���
            Trirggerbox = buildManager.playerState.gameObject.AddComponent<BoxCollider>();
            Trirggerbox.isTrigger = true;
            Trirggerbox.center = new Vector3(0, 0.3f, 0);
            Trirggerbox.size = new Vector3(2.5f, 0.3f, 2.5f);
            buildManager.isSelect = false;
        }


        //�⺻ �ͷ� ��ư�� Ŭ���� ȣ��
        public void SelectTower(int index)
        {
            if (buildManager.isSelect)
            {
                StartCoroutine(TriggerWarning(towerinfo[index]));
                return;
            }
            else if (!buildManager.playerState.SpendMoney(towerinfo[index].cost1))
            {
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return;
            }
            if (buildManager.playerState.SpendMoney(towerinfo[index].cost1) && buildManager.isInstall)
            {
                buildManager.isInstall = false;
                //Debug.Log("�⺻ �ͷ��� ���� �Ͽ����ϴ�");
                //��ġ�� �ͷ��� �⺻ �ͷ�(������)�� ����
                buildManager.SetTowerToBuild(towerinfo[index]/*, towerSprite[index]*/);
                tile.BuildTower(boxes[index].size, boxes[index].center,index);
                //buildManager.SetTowerToInfo(towerinfo[7]);
            }
            BuildUI.SetActive(false);
        }
        public void SelectUpgradeTower(int index)
        {
            if (buildManager.isSelect)
            {
                StartCoroutine(TriggerWarning(towerinfo[index]));
                return;
            }
            else if (!buildManager.playerState.SpendMoney(towerinfo[index].cost2))
            {
                buildManager.warningWindow.ShowWarning("Not Enough Money");
                return;
            }
            if (buildManager.playerState.SpendMoney(towerinfo[index].cost2) && buildManager.isInstall)
            {
                buildManager.isInstall = false;
                //Debug.Log("�⺻ �ͷ��� ���� �Ͽ����ϴ�");
                //��ġ�� �ͷ��� �⺻ �ͷ�(������)�� ����
                buildManager.SetTowerToBuild(towerinfo[index]/*, towerImage[index]*/);
                tile.UpgradeTower(boxes[index].size, boxes[index].center);
            }
        }
        public void BuildMenuUI()
        {
            BuildUI.SetActive(!BuildUI.activeSelf);
        }
        IEnumerator TriggerWarning(TowerInfo tower)
        {
            buildManager.warningWindow.ShowWarning($"There's an {tower.upgradeTower.ToString()} in front of me");
            yield return new WaitForSeconds(2);
            buildManager.warningWindow.ShowWarning($"Create it somewhere else");
            yield return new WaitForSeconds(5);
        }
    }
}
/*
0 - BallistaTower_1
1 - BallistaTower_2
2 - BallistaTower_3
3 - BatTower_1
4 - BatTower_2
5 - BatTower_3
6 - CannonTower_1
7 - CannonTower_2
8 - CannonTower_3
9 - CrossbowTower_1
10 - CrossbowTower_2
11 - CrossbowTower_3
*/