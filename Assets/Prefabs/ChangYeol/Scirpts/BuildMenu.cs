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

        public GameObject[] falsetowers;

        public Tile tile;

        public GameObject BuildUI;

        private BoxCollider Trirggerbox;
        public int indexs;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
        }


        //�⺻ �ͷ� ��ư�� Ŭ���� ȣ��
        public void SelectTower(int index)
        {
            indexs = index;
            buildManager.SetTowerToBuild(towerinfo[index]);
            tile.BuildTower(boxes[index].size, boxes[index].center, index);
            BuildUI.SetActive(false);
        }
        /*public void SelectUpgradeTower(int index)
        {
            indexs = index + 1;
            //Debug.Log("�⺻ �ͷ��� ���� �Ͽ����ϴ�");
            //��ġ�� �ͷ��� �⺻ �ͷ�(������)�� ����
            buildManager.SetTowerToBuild(towerinfo[index]);
            tile.UpgradeTower(boxes[index].size, boxes[index].center);
        }*/
        public void BuildMenuUI()
        {
            BuildUI.SetActive(!BuildUI.activeSelf);
            Destroy(tile.lineVisual.reticle);
            tile.lineVisual.reticle = null;
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