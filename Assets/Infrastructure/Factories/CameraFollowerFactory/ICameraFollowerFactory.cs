using CameraFollower.Presenter;
using UnityEngine;

namespace Infrastructure.Factories.CameraFollowerFactory
{
    public interface ICameraFollowerFactory
    {
        CameraFollowPresenter CreateCameraFollower(Camera attachTo);
    }
}