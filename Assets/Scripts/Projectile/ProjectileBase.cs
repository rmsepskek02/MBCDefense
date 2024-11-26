using UnityEngine;
/// <summary>
/// �߻�ü�� ����� ������ ���� Ŭ����
/// </summary>
namespace Defend.Projectile
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected Transform target;                    // ��ǥ��
        [SerializeField] protected ProjectileInfo projectileInfo;       // �߻�ü ����

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }

        // Ÿ�ٰ� �߻�ü ���� �ʱ�ȭ
        public virtual void Init(ProjectileInfo _projectileInfo, Transform _target)
        {
            target = _target;
            projectileInfo = _projectileInfo;
        }



        // Ÿ���� ����
        protected virtual void Hit()
        {
            //Hit ȿ��
            //GameObject effectGo = Instantiate(bulletImpactPrefab, this.transform.position, Quaternion.identity);
            //Destroy(effectGo, 2f);

            ////Ÿ�ٿ� ������ �ش�
            //Damage(target);

            ////źȯ ���ӿ�����Ʈ kill (Destroy)
            //Destroy(this.gameObject);

            Debug.Log("HIT");
        }
    }
}