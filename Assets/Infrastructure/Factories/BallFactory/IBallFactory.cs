using Ball.View;
using UnityEngine;

namespace Infrastructure.Factories.BallFactory
{
    public interface IBallFactory
    {
        BallView CreateBall(string ballID, Vector3 at, Quaternion rotation);
    }
}