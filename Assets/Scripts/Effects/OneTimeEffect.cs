using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class OneTimeEffect : MonoBehaviour
    {
        private ParticleSystem _effect;

        private void Awake() => 
            _effect = GetComponent<ParticleSystem>();

        private void Start() => 
            _effect.Play();

        private void Update()
        {
            if(_effect.isPlaying == false)
                Destroy(gameObject);
        }
    }
}