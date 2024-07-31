using Infrastructure.StateMachine.GameMachine;
using Zenject;

namespace Infrastructure.Factories.GameStateFactory
{
    public class GameStateFactory : IGameStateFactory
    {
        private readonly DiContainer _projectContainer;

        public GameStateFactory(DiContainer projectContainer) => 
            _projectContainer = projectContainer;

        public TState CreateState<TState>() where TState : IGameState => 
            _projectContainer.Instantiate<TState>();
    }
}