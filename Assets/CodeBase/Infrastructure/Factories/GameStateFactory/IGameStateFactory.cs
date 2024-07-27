using CodeBase.Infrastructure.StateMachine.GameMachine;

namespace CodeBase.Infrastructure.Factories.GameStateFactory
{
    public interface IGameStateFactory
    {
        TState CreateState<TState>() where TState : IGameState;
    }
}