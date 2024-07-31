using System;
using Wallet.Balances;

namespace Wallet
{
    public interface IWallet
    {
        BalanceBase GetCurrencyBalance(CurrencyType searchingCurrency);
        public event Action BalanceUpdated;
    }
}