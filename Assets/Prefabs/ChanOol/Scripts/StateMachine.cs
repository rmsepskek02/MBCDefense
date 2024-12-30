using System.Collections.Generic;
using UnityEngine;

namespace MyPet.AI
{
    /// <summary>
    /// <T>�� ���¸� �����ϴ� Ŭ����
    /// </summary>
    [System.Serializable]
    public abstract class State<T>
    {
        protected StateMachine<T> stateMachine; //�� state�� ��� �Ǿ��ִ� Machine
        protected T context;                    //stateMachine�� ������ �ִ� ��ü
        
        public State() { }                      //������

        public void SetMachineAndContext(StateMachine<T> stateMachine, T context)
        {
            this.stateMachine = stateMachine;
            this.context = context;

            OnInitialized();
        }

        public virtual void OnInitialized() { } //������ 1ȸ ����, �ʱⰪ ����

        public virtual void OnEnter() { }       //���� ��ȯ�� ���·� ���ö� 1ȸ ����

        public abstract void Update(float deltaTime);   //���� ������

        public virtual void OnExit() { }        //���� ��ȯ�� ���¸� ������ 1ȸ ����

    }

    /// <summary>
    /// <T>�� State���� �����ϴ� Ŭ����
    /// </summary>
    public class StateMachine<T>
    {
        private T context;                      //StateMachine�� ������ �ִ� ��ü

        private State<T> currenState;           //���� ����
        public State<T> CurrentState => currenState;

        private State<T> previousState;         //���� ����
        public State<T> PreviousState => previousState;

        private float elapsedTimeInState = 0.0f;    //���� ���� ���� �ð�
        public float ElapsedTimeInState => elapsedTimeInState;

        //��ϵ� ���¸� ������ Ÿ���� Ű������ �����Ѵ�
        private Dictionary<System.Type, State<T>> states = new Dictionary<System.Type, State<T>>();

        //������ : 
        public StateMachine(T context, State<T> initialState)
        {
            this.context = context;

            AddState(initialState);
            currenState = initialState;
            currenState.OnEnter();
        }

        //StateMachine�� State ���
        public void AddState(State<T> state)
        {
            state.SetMachineAndContext(this, context);
            states[state.GetType()] = state;
        }

        //StateMachine���� State�� ������Ʈ ����
        public void Update(float deltaTime)
        {
            elapsedTimeInState += deltaTime;

            currenState.Update(deltaTime);
        }

        //currenState�� ���� �ٲٱ�
        public R ChangeState<R>() where R : State<T>
        {
            //�����¿� ���ο� ���� ��
            var newType = typeof(R);
            if (currenState.GetType() == newType)
            {
                return currenState as R;
            }

            //���� ��������
            if (currenState != null)
            {
                currenState.OnExit();
            }
            previousState = currenState;

            //���� ����
            currenState = states[newType];
            currenState.OnEnter();
            elapsedTimeInState = 0.0f;

            return currenState as R;
        }

    }
}
