using CodeBase.Sources.Modules.Ball.Presenter;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.MapFactory.BallFactory
{
    public interface IBallFactory
    {
        BallPresenter CreateBall(string ballID, Vector3 at, Quaternion rotation);
    }
}