using CodeBase.Sources.Modules.Ball.Presenter;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.GameBuilder
{
    public interface IGameBuilder
    {
        void Build(Vector3 buildPoint);
        BallPresenter BallInstance { get; }
    }
}