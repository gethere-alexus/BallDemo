using Infrastructure.Factories.EffectsFactory;
using UnityEngine;
using Zenject;

namespace Effects
{
    public class BallHitEffect : MonoBehaviour
    {
        private IEffectFactory _effectFactory;

        [Inject]
        public void Construct(IEffectFactory effectFactory) => 
            _effectFactory = effectFactory;

        private void OnCollisionEnter(Collision other)
        {
            Vector3 ballPosition = transform.position;
            Vector3 hitNormal = other.contacts[0].normal;

            Quaternion hitRotation = Quaternion.LookRotation(hitNormal);
            
            _effectFactory.CreateEffect(EffectType.HitCloud, ballPosition, hitRotation);
        }
    }
}