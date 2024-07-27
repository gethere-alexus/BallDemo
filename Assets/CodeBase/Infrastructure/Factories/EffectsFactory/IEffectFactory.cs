using UnityEngine;

namespace CodeBase.Infrastructure.Factories.EffectsFactory
{
    public interface IEffectFactory
    {
        ParticleSystem CreateEffect(EffectType type,Vector3 at, Quaternion rotation);
    }
}