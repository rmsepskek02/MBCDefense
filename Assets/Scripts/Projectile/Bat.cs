using Defend.TestScript;
using UnityEngine;
/// <summary>
/// Bat �߻�ü ����
/// �ܰŸ�, ����, ǥâ
/// </summary>
namespace Defend.Projectile
{
    public class Bat : TargetProjectile
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