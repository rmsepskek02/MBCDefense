using Defend.Tower;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class TileUI : XRSimpleInteractable
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        public GameObject tower;
        private GameObject tower_upgrade;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        public TowerInfo towerInfo;

        //����Ŵ��� ��ü
        private BuildManager buildManager;

        //������ �ν��Ͻ�
        private Renderer rend;

        //���콺�� ���� ������ Ÿ�� �÷���
        //public Color hoverColor;
        //��Ÿ���� �⺻ Color
        public Color notenoughColor;

        //���콺�� ���� ������ Ÿ�� ���͸���
        public Material hoverMaterial;
        //��Ÿ���� �⺻ Material
        private Material startMaterial;

        //����Ʈ ������
        public GameObject TowerImpectPrefab;

        //Ÿ�� ���׷��̵� ����
        public bool IsUpgrade { get; private set; }

        //�Ǹ� ����Ʈ ������
        public GameObject SellImpectPrefab;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            buildManager = BuildManager.Instance;

            //rend = this.transform.GetComponent<Renderer>();
            rend = this.GetComponent<Renderer>();
            //startColor = rend.material.color;
            startMaterial = rend.material;
            IsUpgrade = false;
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            Debug.Log("��ġ�� ��");
            /*if (buildManager.CannotBuild)
            {
                return;
            }
            rend.enabled = true;
            //rend.material.color = hoverColor;
            rend.material = hoverMaterial;

            //������ �ͷ��� �Ǽ��� ����� ������ �ִ��� �ܰ�Ȯ��
            if (buildManager.HasBuildMoney == false)
            {
                rend.material.color = notenoughColor;
            }
            if (tower != null || tower_upgrade != null)
            {
                rend.material.color = notenoughColor;
                return;
            }*/
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);

            Debug.Log("��ġ");
            /*if (tower != null || tower_upgrade != null)
            {
                buildManager.SelectTile(this);
                return;
            }
            if (buildManager.CannotBuild)
            {
                Debug.Log("�ͷ��� ��ġ���� ���߽��ϴ�"); //�ͷ��� �������� ���� ����
                return;
            }
            BuildTurret();*/
        }
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            Debug.Log("��ġX");
            /*rend.enabled = false;
            //rend.material.color = startColor;
            rend.material = startMaterial;*/
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            Debug.Log("��ġX");
            /*rend.enabled = false;
            //rend.material.color = startColor;
            rend.material = startMaterial;*/
        }
        /*private void OnMouseEnter()
        {
            //���콺 �����Ͱ� UI���� ������
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            //�ͷ��� �������� ������
            if (buildManager.CannotBuild)
            {
                return;
            }

            rend.enabled = true;
            //rend.material.color = hoverColor;
            rend.material = hoverMaterial;

            //������ �ͷ��� �Ǽ��� ����� ������ �ִ��� �ܰ�Ȯ��
            if (buildManager.HasBuildMoney == false)
            {
                rend.material.color = notenoughColor;
            }
            if (turret != null || turret_upgrade != null)
            {
                rend.material.color = notenoughColor;
                return;
            }

        }*/

        /*private void OnMouseDown()
        {
            //���콺 �����Ͱ� UI���� ������
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (turret != null || turret_upgrade != null)
            {
                buildManager.SelectTile(this);
                return;
            }
            if (buildManager.CannotBuild)
            {
                Debug.Log("�ͷ��� ��ġ���� ���߽��ϴ�"); //�ͷ��� �������� ���� ����
                return;
            }
            BuildTurret();
        }*/
        /*public void SellTurret()
        {
            //���׷��̵� �ͷ��� �Ǹ�
            if (turret_upgrade != null)
            {
                Destroy(turret_upgrade);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //���׷��̵��ͷ����� �ݰ����� �Ǹ�
                PlayerStats.AddMoney(blueprint.Getupgradecost());
            }
            //�⺻ �ͷ��� �Ǹ�
            else if (turret != null)
            {
                Destroy(turret);
                IsUpgrade = false;
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //�⺻�ͷ����� �ݰ����� �Ǹ�
                PlayerStats.AddMoney(blueprint.GetSellcost());
            }
        }*/

        /*public void UpgradeTurret()
        {
            //Debug.Log("�ͷ� ���׷��̵�");
            if (blueprint == null)
            {
                //Debug.Log("���׷��̵� �����߽��ϴ�");
                return;
            }
            if (PlayerStats.UseMoney(blueprint.costUpgrade))
            {
                //Effect
                GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effectGo, 2f);

                //�ͷ� ���׷��̵� ����
                IsUpgrade = true;

                //�ͷ� ���׷��̵� ����
                turret_upgrade = Instantiate(TowerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
                Destroy(turret);
            }
        }*/

        //�ͷ� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            return this.transform.position;
        }
        //���콺�� Ÿ�� �ȿ��� ������ �̵���
        /*private void OnMouseExit()
        {
            rend.enabled = false;
            //rend.material.color = startColor;
            rend.material = startMaterial;
        }*/

        private void BuildTurret()
        {
            //��ġ�� �ͷ��� �Ӽ��� �������� (�ͷ� ������, �Ǽ����, ���׷��̵� ������, ���׷��̵� ���...)
            towerInfo = buildManager.GetTurretToBuild();

            //���� �����Ѵ� 100, 250
            //Debug.Log($"�ͷ� �Ǽ����: {blueprint.cost}");
            //Effect
            GameObject effectGo = Instantiate(TowerImpectPrefab, GetBuildPosition(), Quaternion.identity);
            Destroy(effectGo, 2f);

            tower = Instantiate(towerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
            //Debug.Log($"�Ǽ��ϰ� ���� ���� {PlayerStats.Money}");
        }
    }
}