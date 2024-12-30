using UnityEngine;


namespace MyPet.AI
{
    public class DogController : AnimalController
    {

        protected override void Start()
        {
            base.Start();

            //dog�� ���õ� ���� �߰� ���
            stateMachine.AddState(new DrinkState());
            stateMachine.AddState(new SitState());
            stateMachine.AddState(new DogIdleState());

        }

    }
}