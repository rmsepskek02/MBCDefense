using Defend.Player;
using Defend.Tower;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Defend.UI
{
    public class Tile : XRSimpleInteractable
    {
        #region Variables
        //Ÿ�Ͽ� ��ġ�� Ÿ�� ���ӿ�����Ʈ ��ü
        private GameObject tower;
        //���� ���õ� Ÿ�� TowerInfo(prefab, cost, ....)
        public TowerInfo towerInfo;

        [SerializeField] private Vector3 offset;

        //����Ŵ��� ��ü
        private BuildManager buildManager;

        //��ġ �����ϸ� ��Ÿ���� ��ƼŬ
        public ParticleSystem particle;
        
        //��ġ �Ұ����ϸ� ��Ÿ���� ��ƼŬ
        public ParticleSystem notparticle;

        //����Ʈ ������
        public GameObject TowerImpectPrefab;

        //Ÿ�� ���׷��̵� ����
        //public bool IsUpgrade { get; private set; }

        //�Ǹ� ����Ʈ ������
        public GameObject SellImpectPrefab;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            particle.Stop();
            notparticle.Stop();
            buildManager = BuildManager.Instance;
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            Debug.Log("123");
            base.OnHoverEntered(args);
            //Ÿ���� ��ġ�Ǿ������� return
            if (tower != null)
            {
                StartCoroutine(notparticlePlay());
                return;
            }
            //��ġ�����ϸ� ��ƼŬ ���
            else if(tower == null)
            {
                Debug.Log("��ġ�� ��");
                particle.Play();
            }
            if (buildManager.CannotBuild)
            {
                return;
            }

            //������ �ͷ��� �Ǽ��� ����� ������ �ִ��� �ܰ�Ȯ��
            /*if (buildManager.HasBuildMoney == false)
            {
                rend.material.color = notenoughColor;
            }*/
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            //Ÿ���� ��ġ�Ǿ������� return
            /*if (args.interactableObject.)
            {
                //buildManager.SelectTile(this);
                StartCoroutine(notparticlePlay());

            }*/
            //Ÿ���� ��ġ �����ϸ� ��ġ
            if (buildManager.CannotBuild)
            {
                Debug.Log("Ÿ���� ��ġ���� ���߽��ϴ�"); //Ÿ���� �������� ���� ����
                return;
            }
            Debug.Log("��ġ");
            particle.Stop();
            BuildTower();
        }
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            //Debug.Log("hover");
            particle.Stop();
            notparticle.Stop();
        }
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            //Debug.Log("select");
            particle.Stop();
            notparticle.Stop();
        }
        //��ġ �Ǿ������� ȣ��Ǵ� �Լ�
        IEnumerator notparticlePlay()
        {
            Debug.Log("��ġ �Ǿ��ֽ��ϴ�");
            notparticle.Play();
            yield return new WaitForSeconds(2f);
            notparticle.Stop();
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
                GameObject effect = Instantiate(SellImpectPrefab, GetBuildPosition(), Quaternion.identity);
                Destroy(effect, 2f);
                //�⺻�ͷ����� �ݰ����� �Ǹ�
                //PlayerState.AddMoney();
            }
        }

        public void UpgradeTower()
        {
            Debug.Log("�ͷ� ���׷��̵�");
            /*if (blueprint == null)
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
            }*/
        }

        //Ÿ�� ��ġ ��ġ
        public Vector3 GetBuildPosition()
        {
            return this.transform.position + Vector3.up;
        }
        //Ÿ�� ����
        public void BuildTower()
        {
            //��ġ�� �ͷ��� �Ӽ��� �������� (�ͷ� ������, �Ǽ����, ���׷��̵� ������, ���׷��̵� ���...)
            towerInfo = buildManager.GetTowerToBuild();

            //���� �����Ѵ� 100, 250
            //Debug.Log($"�ͷ� �Ǽ����: {blueprint.cost}");
            //Ÿ�� ����
            tower = Instantiate(towerInfo.upgradeTower, GetBuildPosition(), Quaternion.identity);
            //Ÿ���� ���� �� �ִ� ������Ʈ �߰�
            tower.AddComponent<BoxCollider>();
            tower.AddComponent<TowerXR>();
            //Ÿ�� ���� ����Ʈ
            GameObject effgo = Instantiate(TowerImpectPrefab, towerInfo.upgradeTower.transform.position, Quaternion.identity);
            //Ÿ�� �ڽ����� ����
            effgo.transform.parent = transform;
            Debug.Log("�����");
        }
        /*private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                Vector3 spawnPosition = other.transform.position + Vector3.up + new Vector3(2, 0, 0);

                // ������ ����
                Instantiate(towerInfo.upgradeTower, spawnPosition, Quaternion.identity);
            }
        }*/
    }
}