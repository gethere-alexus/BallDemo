using CodeBase.Infrastructure.StateMachine.GameMachine;
using CodeBase.Infrastructure.StateMachine.GameMachine.States;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class Game
    {
        public Game(GameStateMachine gameStateMachine)
        {
            gameStateMachine.Enter<BootstrapState>();
        }
    }
}