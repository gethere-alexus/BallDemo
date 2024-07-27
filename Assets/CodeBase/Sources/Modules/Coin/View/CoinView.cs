using System;
using System.Collections;
using CodeBase.Sources.Modules.Coin.Presenter;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Sources.Modules.Coin.View
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private GameObject _coinGameObject;
        [SerializeField] private float _timeToDisappear;
        [SerializeField] private ParticleSystem _passiveAura;
        [SerializeField] private ParticleSystem _onDestroyedParticles;

        [Inject]
        public void Construct(CoinPresenter coinPresenter)
        {
            CoinPresenter = coinPresenter;
        }

        private void OnEnable()
        {
            if (CoinPresenter != null)
                CoinPresenter.CoinClaimed += OnCoinClaimed;
        }

        private void OnDisable()
        {
            if (CoinPresenter != null)
                CoinPresenter.CoinClaimed -= OnCoinClaimed;
        }

        private void OnCoinClaimed()
        {
            _passiveAura.gameObject.SetActive(false);
            StartCoroutine(AnimateDestruction());
        }

        private IEnumerator AnimateDestruction()
        {
            Vector3 zeroScale = new Vector3(0, 0, 0);
            
            Tween scaleOperation = _coinGameObject.transform.DOScale(zeroScale, _timeToDisappear);
            while (scaleOperation.IsActive())
                yield return null;
            
            _onDestroyedParticles.Play();
            while (_onDestroyedParticles.isPlaying)
                yield return null;
            
            Destroy(gameObject);
        }

        public CoinPresenter CoinPresenter { get; private set; }
    }
}