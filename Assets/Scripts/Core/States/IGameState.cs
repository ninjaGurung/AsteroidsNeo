
namespace SuperLaggy.AsteroidsNeo.Core.States
{
    public interface IGameState
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
