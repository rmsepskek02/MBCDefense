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
        //타워들의 정보값
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
            //초기화
            buildManager = BuildManager.Instance;
            //플레이어에게 box콜라이더 추가하고 trigger로 주변에 있는 콜라이더가 있으면 경고창이 뜬다
            Trirggerbox = buildManager.playerState.gameObject.AddComponent<BoxCollider>();
            Trirggerbox.isTrigger = true;
            Trirggerbox.center = new Vector3(0, 0.3f, 0);
            Trirggerbox.size = new Vector3(2.5f, 0.3f, 2.5f);
            buildManager.isSelect = false;
        }


        //기본 터렛 버튼을 클릭시 호출
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
                //Debug.Log("기본 터렛을 선택 하였습니다");
                //설치할 터렛에 기본 터렛(프리팹)을 저장
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
                //Debug.Log("기본 터렛을 선택 하였습니다");
                //설치할 터렛에 기본 터렛(프리팹)을 저장
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