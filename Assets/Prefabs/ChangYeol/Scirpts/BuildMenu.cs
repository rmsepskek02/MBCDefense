using Defend.Player;
using Defend.Tower;
using UnityEngine;
using UnityEngine.UI;
namespace Defend.UI
{
    public class BuildMenu : MonoBehaviour
    {
        #region Variables
        private BuildManager buildManager;
        //Ÿ������ ������
        public TowerInfo[] towerinfo;
        //Ÿ������ �̹���
        public Sprite[] towerSprite;
        //Ÿ������ �ڽ��ݶ��̴�
        public BoxCollider[] boxes;
        //��ġ ��ġ�� �����ִ� ��¥ Ÿ��
        public GameObject[] falsetowers;
        public Tile tile;
        //���� �޴� UI
        public GameObject BuildUI;
        //���� �޴��� Ÿ�� ���ý� ������ Ÿ���� ������ �����ִ� UI
        public GameObject buildpro;
        //index��° Ÿ���� �����ϸ� �����ϴ� �� 
        public int indexs;
        public int levelindex = Mathf.Clamp(1,1,3);
        //reticle�� Ȱ��ȭ ��Ȱ��ȭ ����
        public bool isReticle = false;
        public bool istowerup = false;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;
        }
        
        //Ÿ�� ��ư�� Ŭ���� ȣ��
        public void SelectTower(int index)
        {
            if (!towerinfo[index].isLock) return;
            indexs = index;
            isReticle = true;
            istowerup = true;
            BuildUI.SetActive(false);
            //buildpro.SetActive(true);
        }
        public void SetLevel(int level)
        {
            levelindex = level;
        }
        public void BuildMenuUI()
        {
            BuildUI.SetActive(!BuildUI.activeSelf);
            isReticle=false;
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