using Defend.TestScript;
using UnityEngine;
/// <summary>
/// Crossbow �߻�ü ����
/// �ܰŸ�, ����, ȭ��
/// </summary>
namespace Defend.Projectile
{
    public class Crossbow : TargetProjectile
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected override void Hit()
        {
            base.Hit();
            HitOnTarget();
        }
    }
}