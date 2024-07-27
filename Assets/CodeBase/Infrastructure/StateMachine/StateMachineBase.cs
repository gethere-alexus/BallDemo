using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.StateMachine
{
    public abstract class StateMachineBase<TState> where TState : class, IState
    {
        private readonly Dictionary<Type, TState> _states = new();
        private TState _activeState;

        public void AddState<TEnterState>(TEnterState state) where TEnterState : class, TState =>
            _states.Add(typeof(TEnterState), state);
        
        public void Enter<TEnterState>() where TEnterState : class, TState =>
            ChangeState<TEnterState>().Enter();

        private TState ChangeState<TEnterState>() where TEnterState : class, TState
        {
            _activeState?.Exit();
            TState state = GetState<TEnterState>();
            _activeState = state;
            return state;
        }
        
        private TState GetState<TEnterState>() where TEnterState : class, TState =>
            _states[typeof(TEnterState)] as TEnterState;
    }
}