using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Data.GameConfiguration.UI.CoinDisplay.Modules;
using MVPBase;
using UI.CoinDisplay.Model;
using UI.CoinDisplay.Modules;
using UI.CoinDisplay.View;
using Utils.Extensions;
using Wallet;

namespace UI.CoinDisplay.Presenter
{
    public class CoinBalancePresenter : PresenterBase<CoinBalanceModel, CoinBalanceView>, IDisposable
    {
        private readonly IWallet _wallet;

        private CoinBalanceView _coinBalanceView;
        private CoinBalanceModel _coinBalanceModel;
        
        private CoinDisplayPunchAnimation _punchAnimation;

        private bool _isBalanceUpdating;

        private const CurrencyType TrackedCurrency = CurrencyType.Coin;

        public CoinBalancePresenter(IWallet wallet) => 
            _wallet = wallet;

        public override void LinkPresenter(CoinBalanceModel model, CoinBalanceView view)
        {
            _coinBalanceModel = model;
            _coinBalanceView = view;

            CoinDisplayAnimationConfig animationConfig = new CoinDisplayAnimationConfig()
                .With(config => config.PunchTime = model.PunchTime)
                .With(config => config.PunchScaleCoefficient = model.PunchScaleCoefficient);
            
            _punchAnimation = new CoinDisplayPunchAnimation(animationRunner: view, coinBalanceView: view)
                .With(animation => animation.Configure(animationConfig));
            
            _wallet.BalanceUpdated += OnBalanceUpdated;
            _coinBalanceView.Destroying += Dispose;
            
            Enable();
        }
        

        public override void Enable()
        {
            
        }

        public override void Disable()
        {
           
        }

        public void Dispose()
        {
            _wallet.BalanceUpdated -= OnBalanceUpdated;
            _coinBalanceView.Destroying -= Dispose;
        }

        private void OnBalanceUpdated()
        {
            if (_isBalanceUpdating == false)
                SetNewBalance();
        }

        private async void SetNewBalance()
        {
            _isBalanceUpdating = true;

            int updatedCoins = 0;
            for (int currentBalance = DisplayedBalance; currentBalance <= WalletCoinBalance; currentBalance++)
            {
                await UniTask.WaitForSeconds(_coinBalanceModel.TimeToAddCoin);
                _coinBalanceView.SetDisplayedBalance(currentBalance);
                updatedCoins++;
                
                if (updatedCoins >= _coinBalanceModel.PunchStep)
                {
                    updatedCoins = 0;
                    _punchAnimation.PlayPunchAnimation();
                }
            }
            
            _isBalanceUpdating = false;
        }
        
        private int DisplayedBalance => _coinBalanceView.DisplayedBalance;
        private int WalletCoinBalance => _wallet.GetBalanceAmount(TrackedCurrency);
    }
}