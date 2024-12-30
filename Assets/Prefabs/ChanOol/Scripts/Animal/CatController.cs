using UnityEngine;

namespace MyPet.AI
{
    public class CatController : AnimalController
    {        
        protected override void Start()
        {
            base.Start(); //StateMachine ����, IdleState() ���

            //����� ������ ���� �߰� ���
            stateMachine.AddState(new SitState());
            stateMachine.AddState(new DrinkState());
        }

        public void Idle()
        {
            ChangeState<IdleState>();
        }

        public void Sit()
        {
            ChangeState<SitState>();
        }

        public void Drink()
        {
            ChangeState<DrinkState>();
        }
    }
}
