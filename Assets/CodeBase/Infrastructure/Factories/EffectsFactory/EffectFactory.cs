using System.Collections.Generic;
using CodeBase.Infrastructure.Services.PrefabProvider;
using CodeBase.Infrastructure.Services.ResourceLoader;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.EffectsFactory
{
    public class EffectFactory : IEffectFactory
    {
        private readonly IPrefabProvider _prefabProvider;
        
        private readonly Dictionary<EffectType, ParticleSystem> _effects;
        public EffectFactory(IResourceLoader resourceLoader, IPrefabProvider prefabProvider)
        {
            _prefabProvider = prefabProvider;
            _effects = new Dictionary<EffectType, ParticleSystem>
            {
                { EffectType.HitCloud , resourceLoader.Load<ParticleSystem>(PrefabPaths.CloudEffect)}
            };
        }

        public ParticleSystem CreateEffect(EffectType effectType,Vector3 at, Quaternion rotation)
        {
            ParticleSystem prefab = _effects[effectType];
            return _prefabProvider.Instantiate(prefab, at, rotation);
        }
    }
}