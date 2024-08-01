using UnityEngine;

namespace Ball.View.Modules.Transform.TransformModules
{
    public class BallMovementView : MonoBehaviour
    {
        public void Move(Vector3 movementStep) => 
            transform.position += movementStep;
    }
}