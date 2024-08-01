using UnityEngine;

namespace Ball.View.Modules.Transform.TransformModules
{
    public class BallRotationView : MonoBehaviour
    {
        public void Rotate(Vector3 rotationStep) =>
            transform.Rotate(rotationStep, Space.World);
    }
}