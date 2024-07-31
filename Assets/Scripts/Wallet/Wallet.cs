using System;
using System.Collections.Generic;
using Infrastructure.Data.Progress;
using Infrastructure.Services.ProgressProvider;
using Infrastructure.Services.ProgressProvider.API;
using Wallet.Balances;

namespace Wallet
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