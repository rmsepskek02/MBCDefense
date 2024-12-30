using Defend.Tower;
using UnityEngine;
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
        //index��° Ÿ���� �����ϸ� �����ϴ� �� 
        public int indexs;
        public int levelindex = Mathf.Clamp(1,1,3);
        //reticle�� Ȱ��ȭ ��Ȱ��ȭ ����
        //public bool isReticle = false;
        //public bool istrigger = false;
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
            // �ش� �������� ��ġ�� �÷��̾ �� �� ������ ����
            falsetowers[indexs].transform.position = new Vector3(0, -1000, 0);
            // recticlePrefab�� ������ Ÿ�� �Ҵ�
            tile.leftReticleVisual.reticlePrefab = falsetowers[indexs];
            //isReticle = true;
            //istrigger = true;
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
            //isReticle=false;
        }
    }
}