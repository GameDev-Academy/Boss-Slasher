using System;

namespace IngameStateMachine
{
    public interface IState : IDisposable
    {
        void Initialize(StateMachine stateMachine);

        void OnEnter();
        void OnExit();
    }
}