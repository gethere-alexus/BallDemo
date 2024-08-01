using UnityEngine;

namespace Ball.View.Modules.Transform.TransformModules
{
    public class BallScaleView : MonoBehaviour
    {
        public Vector3 OriginalScale { get; private set; }

        private void Awake() => 
            OriginalScale = transform.localScale;

        public void SetScale(Vector3 toSet) => 
            transform.localScale = toSet;
    }
}