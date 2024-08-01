using System;
using System.Collections.Generic;
using APIs.ProgressInteraction;
using Infrastructure.Data.GameProgress;
using Infrastructure.Services.ProgressProvider;
using Utils.Extensions;
using Wallet.Balances;
using Wallet.Balances.Coin;

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

        public void AddCurrency(ICurrency currency)
        {
            GetCurrencyBalance(currency.CurrencyType)
                .With(balance => balance.Add(currency.Amount));
        }

        public int GetBalanceAmount(CurrencyType currencyType) => 
            GetCurrencyBalance(currencyType).Balance;

        private BalanceBase GetCurrencyBalance(CurrencyType searchingCurrency) => 
            _currencyBalances[searchingCurrency];

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