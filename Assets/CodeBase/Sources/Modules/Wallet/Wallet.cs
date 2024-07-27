using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Data.Progress;
using CodeBase.Infrastructure.Services.ProgressProvider;
using CodeBase.Infrastructure.Services.ProgressProvider.API;
using CodeBase.Sources.Modules.Wallet.Balances;

namespace CodeBase.Sources.Modules.Wallet
{
    public class Wallet : IWallet, IProgressWriter
    {
        public event Action BalanceUpdated;
        private readonly Dictionary<CurrencyType, BalanceBase> _currencyBalances;


        public Wallet(IProgressProvider progressProvider)
        {
            progressProvider.RegisterObserver(this);
            _currencyBalances = new Dictionary<CurrencyType, BalanceBase>
            {
                { CurrencyType.Coin, new CoinBalance() },
            };
            
            foreach (var balance in _currencyBalances.Values)
                balance.BalanceUpdated += () => BalanceUpdated?.Invoke();
        }

        public BalanceBase GetCurrencyBalance(CurrencyType searchingCurrency) => _currencyBalances[searchingCurrency];

        public void LoadProgress(GameProgress progress)
        {
            _currencyBalances[CurrencyType.Coin].SetBalance(progress.WalletProgress.CoinBalance); 
        }

        public void SaveProgress(GameProgress progress)
        {
            progress.WalletProgress.CoinBalance = _currencyBalances[CurrencyType.Coin].Balance;
        }
    }
}