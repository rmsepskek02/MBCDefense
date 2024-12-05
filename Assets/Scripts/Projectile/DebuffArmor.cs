using Defend.TestScript;
/// <summary>
/// DebuffArmor �߻�ü ��� ����
/// ���Ÿ�, ����, ���� ����
/// </summary>
namespace Defend.Projectile
{
    public class DebuffArmor : Debuffer<Health>
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
        protected override void DoDebuffAction(Health component)
        {
            component.ChangedArmor(projectileInfo.debuffRatio); // ���� ����
        }

        // ����� ���� �ߵ�
        protected override void UndoDebuffAction(Health component)
        {
            component.ChangedArmor(-projectileInfo.debuffRatio); // ���� ���󺹱�
        }
    }
}
