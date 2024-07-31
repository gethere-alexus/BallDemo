using Infrastructure.StateMachine.GameMachine;
using Infrastructure.StateMachine.GameMachine.States;

namespace Infrastructure.Bootstrap
{
    public class Game
    {
        public Game(GameStateMachine gameStateMachine)
        {
            gameStateMachine.Enter<BootstrapState>();
        }
    }
}