using UnityEngine;
using UnityEngine.UI;

namespace Ball.View.Modules
{
    public class BallForceView : MonoBehaviour
    {
        [SerializeField] private Image _forceView;

        private const float PositionNoChanges = 1.0f, RotationNoChanges = 1.0f, ScaleNoChanges = 1f;

        public void SetForceView(Vector3 displayAt, float applyingForce, float direction)
        {
            bool hasApplyingForce = applyingForce > 0;

            _forceView.gameObject.SetActive(hasApplyingForce);

            if (hasApplyingForce)
            {
                transform.position = new Vector3(displayAt.x, PositionNoChanges, displayAt.z);

                _forceView.transform.localRotation = Quaternion.Euler(RotationNoChanges, RotationNoChanges, direction);
                _forceView.transform.localScale = new Vector3(ScaleNoChanges, applyingForce, ScaleNoChanges);
            }
        }
    }
}