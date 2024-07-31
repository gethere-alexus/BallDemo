using System;
using Ball.Presenter;
using Ball.View;
using Coin.Model;
using UnityEngine;
using Wallet;
using Zenject;

namespace Coin.Presenter
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
            if (other.gameObject.TryGetComponent(out BallView _) && _isCoinClaimed == false)
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