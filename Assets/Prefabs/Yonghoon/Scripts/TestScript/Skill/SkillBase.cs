using UnityEngine;

namespace Defend.Enemy.Skill
{
    public abstract class SkillBase : MonoBehaviour
    {
        /// <summary>
        /// ��ų�� Ȱ��ȭ�ϴ� �޼���
        /// </summary>
        /// <param name="activator">��ų�� �ߵ��ϴ� ���� Transform</param>
        /// <param name="range">��ų �ߵ� ����</param>
        public abstract void ActivateSkill();

        /// <summary>
        /// ��ų�� Ȱ��ȭ�� �� �ִ��� ���θ� ��ȯ�ϴ� �޼���
        /// </summary>
        /// <param name="currentHealth">���� ü��</param>
        /// <param name="maxHealth">�ִ� ü��</param>
        /// <param name="elapsedTime">��� �ð� (�ʿ��� ���)</param>
        /// <returns>��ų Ȱ��ȭ ���� ����</returns>
        public abstract bool CanActivateSkill(float healthRatio);
    }
}