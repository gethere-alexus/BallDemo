using System;
using Wallet.Balances;

namespace Wallet
{
    public interface IWallet
    {
        int GetBalanceAmount(CurrencyType currencyType);
        void AddCurrency(ICurrency currency);
        public event Action BalanceUpdated;
    }
}