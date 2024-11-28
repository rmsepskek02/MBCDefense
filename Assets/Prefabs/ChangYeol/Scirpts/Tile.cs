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
        public GameObject towerprefab;
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
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            //Ÿ���� ��ġ�Ǿ������� return
            if (tower != null)
            {
                //buildManager.SelectTile(this);
                StartCoroutine(notparticlePlay());
                return;
            }
            //��ġ�����ϸ� ��ƼŬ ���
            else if(tower == null)
            {
                Debug.Log("��ġ�� ��");
                particle.Play();
            }
            /*if (buildManager.CannotBuild)
            {
                return;
            }

            //������ �ͷ��� �Ǽ��� ����� ������ �ִ��� �ܰ�Ȯ��
            if (buildManager.HasBuildMoney == false)
            {
                rend.material.color = notenoughColor;
            }*/
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            //Ÿ���� ��ġ�Ǿ������� return
            if (tower != null)
            {
                //buildManager.SelectTile(this);
                StartCoroutine(notparticlePlay());
                return;
            }
            //Ÿ���� ��ġ �����ϸ� ��ġ
            Debug.Log("��ġ");
            particle.Stop();
            /*if (buildManager.CannotBuild)
            {
                Debug.Log("�ͷ��� ��ġ���� ���߽��ϴ�"); //�ͷ��� �������� ���� ����
                return;
            }*/
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
            return this.transform.position + offset;
        }
        //Ÿ�� ����
        private void BuildTower()
        {
            //��ġ�� �ͷ��� �Ӽ��� �������� (�ͷ� ������, �Ǽ����, ���׷��̵� ������, ���׷��̵� ���...)
            //towerInfo = buildManager.GetTurretToBuild();

            //���� �����Ѵ� 100, 250
            //Debug.Log($"�ͷ� �Ǽ����: {blueprint.cost}");
            //Ÿ�� ���� ����Ʈ
            GameObject effgo = Instantiate(TowerImpectPrefab, transform.position, Quaternion.identity);
            //Ÿ�� �ڽ����� ����
            effgo.transform.parent = transform;
            //Ÿ�� ����
            tower = Instantiate(towerprefab, GetBuildPosition(), Quaternion.identity);
            //Debug.Log($"�Ǽ��ϰ� ���� ���� {PlayerStats.Money}");
            //�ִϸ��̼� �ð� 1.5�� �� ����
            Destroy(effgo,1.5f);
        }
    }
}