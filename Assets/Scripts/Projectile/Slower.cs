using Defend.Enemy;
/// <summary>
/// Slower �߻�ü ��� ����
/// ���Ÿ�, ����, �̵��ӵ� ����
/// </summary>
namespace Defend.Projectile
{
    public class Slower : Debuffer<EnemyMoveController>
    {
        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();
        }

        // ����� �׼� �ߵ�
        protected override void DoDebuffAction(EnemyMoveController component)
        {
            component.ChangedMoveSpeed(this.gameObject,projectileInfo.debuffRatio); // �̵� �ӵ� ����
        }

        // ����� ���� �ߵ�
        protected override void UndoDebuffAction(EnemyMoveController component)
        {
            component.RemoveMoveSource(this.gameObject); // �̵� �ӵ� ���󺹱�
        }
    }
}