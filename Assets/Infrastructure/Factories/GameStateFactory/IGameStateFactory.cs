using Infrastructure.StateMachine.GameMachine;

namespace Infrastructure.Factories.GameStateFactory
{
    public interface IGameStateFactory
    {
        TState CreateState<TState>() where TState : IGameState;
    }
}