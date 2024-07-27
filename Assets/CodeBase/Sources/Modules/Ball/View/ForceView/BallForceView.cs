using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Sources.Modules.Ball.View.ForceView
{
    public class BallForceView : MonoBehaviour
    {
        [SerializeField] private Image _forceView;
        
        private const float MinimumDisplayedScale = 0.5f, MaximumDisplayedScale = 15.0f;

        private const float RotationNoChanges = 1.0f;
        private const float XScaleNoChanges = 1f, ZScaleNoChanges = 1.0f;

        public void SetForceView(Vector3 displayAt, float applyingForce, float direction)
        {
            bool hasApplyingForce = applyingForce > 0;

            _forceView.gameObject.SetActive(hasApplyingForce);

            if (hasApplyingForce)
            {
                float scaleToSet = GetForceScale(applyingForce);

                transform.position = new Vector3(displayAt.x, 1.0f, displayAt.z);

                _forceView.transform.localRotation = Quaternion.Euler(RotationNoChanges, RotationNoChanges, direction);
                _forceView.transform.localScale = new Vector3(XScaleNoChanges, scaleToSet, ZScaleNoChanges);
            }
        }

        private float GetForceScale(float applyingForce)
        {
            float scaleToSet = applyingForce;

            scaleToSet = scaleToSet switch
            {
                < MinimumDisplayedScale => MinimumDisplayedScale,
                > MaximumDisplayedScale => MaximumDisplayedScale,
                _ => scaleToSet
            };

            return scaleToSet;
        }
    }
}