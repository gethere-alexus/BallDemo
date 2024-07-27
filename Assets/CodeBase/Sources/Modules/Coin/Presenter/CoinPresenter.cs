using System;
using CodeBase.Sources.Modules.Ball.Presenter;
using CodeBase.Sources.Modules.Coin.Model;
using CodeBase.Sources.Modules.Wallet;
using UnityEngine;
using Zenject;

namespace CodeBase.Sources.Modules.Coin.Presenter
{
    public class CoinPresenter : MonoBehaviour
    {
        private CoinModel _coinModel;
        private IWallet _wallet;

        private bool _isCoinClaimed;
        public event Action CoinClaimed;

        [Inject]
        public void Construct(CoinModel coinModel, IWallet wallet)
        {
            _coinModel = coinModel;
            _wallet = wallet;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BallPresenter _) && _isCoinClaimed == false)
                OnCoinClaimed();
        }

        private void OnCoinClaimed()
        {
            _wallet.GetCurrencyBalance(CurrencyType.Coin).Add(_coinModel.CoinsAmount);
            _isCoinClaimed = true;
            CoinClaimed?.Invoke();
        }
    }
}