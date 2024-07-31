using System;
using System.Collections;
using Ball.View;
using Coin.Model;
using Coin.View;
using DG.Tweening;
using Infrastructure.Factories.EffectsFactory;
using MVPBase;
using UnityEngine;
using Utils.Extensions;
using Wallet;
using Object = UnityEngine.Object;

namespace Coin.Presenter
{
    public class CoinPresenter : PresenterBase<CoinModel, CoinView>, IDisposable
    {
        private readonly IWallet _wallet;
        private readonly IEffectFactory _effectFactory;

        private CoinModel _coinModel;
        private CoinView _coinView;

        private ParticleSystem _auraEffect;

        private ParticleSystem _explosionEffect;

        private bool _isCoinClaimed;

        public CoinPresenter(IWallet wallet, IEffectFactory effectFactory)
        {
            _wallet = wallet;
            _effectFactory = effectFactory;
        }

        public override void Enable() => 
            _coinView.TriggerEntered += OnTriggerEntered;

        public override void Disable() => 
            _coinView.TriggerEntered -= OnTriggerEntered;

        public override void LinkPresenter(CoinModel model, CoinView view)
        {
            _coinModel = model;
            _coinView = view;
            
            ConfigureEffects();

            _coinView.Enabling += Enable;
            _coinView.Disabling += Disable;

            Enable();
        }

        private void ConfigureEffects()
        {
            _coinView.SetRotationSpeed(_coinModel.RotationSpeed);
            
            _auraEffect = _effectFactory
                .CreateEffect(EffectType.CoinAura, _coinView.CoinGameObject.Position(), Quaternion.identity)
                .With(effect => effect.transform.SetParent(_coinView.transform));

            _explosionEffect = _effectFactory
                .CreateEffect(EffectType.CoinExplosion, _coinView.transform.position, Quaternion.identity)
                .With(effect => effect.transform.SetParent(_coinView.transform));
        }

        public void Dispose()
        {
            _coinView.Enabling -= Enable;
            _coinView.Disabling -= Disable;
        }

        private void OnTriggerEntered(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BallView _) && _isCoinClaimed == false)
                ClaimCoin();
        }

        private void ClaimCoin()
        {
            _wallet.GetCurrencyBalance(CurrencyType.Coin).Add(_coinModel.CoinsToClaim);
            _isCoinClaimed = true;

            _coinView.StartCoroutine(AnimateDestruction());
        }

        private IEnumerator AnimateDestruction()
        {
            Vector3 zeroScale = new Vector3(0, 0, 0);

            _auraEffect.Stop();
            Tween scaleOperation = _coinView.CoinGameObject.transform.DOScale(zeroScale, _coinModel.AnimationTimeToDisappear);
            while (scaleOperation.IsActive())
                yield return null;
            
            _explosionEffect.Play();
            while (_explosionEffect.isPlaying)
                yield return null;
            
            Dispose();
            Object.Destroy(_coinView.gameObject);
        }
    }
}