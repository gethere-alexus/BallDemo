using Ball.Presenter;
using Ball.View;
using UnityEngine;

namespace Infrastructure.Services.GameBuilder
{
    public interface IGameBuilder
    {
        void Build(Vector3 buildPoint);
        BallView BallInstance { get; }
    }
}