using System;
using CodeBase.Sources.Modules.Wallet.Balances;

namespace CodeBase.Sources.Modules.Wallet
{
    public interface IWallet
    {
        BalanceBase GetCurrencyBalance(CurrencyType searchingCurrency);
        public event Action BalanceUpdated;
    }
}