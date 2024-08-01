using System;
using UnityEngine;

namespace Ball.View.Modules.Collision
{
    public class BallCollisionView : MonoBehaviour
    {
        public event Action<UnityEngine.Collision> ObjectCollided;
        
        private void OnCollisionEnter(UnityEngine.Collision collision) => 
            ObjectCollided?.Invoke(collision);
    }
}