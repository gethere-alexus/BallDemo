using System;

namespace Wallet.Balances.Coin
{
    public class CoinBalance : BalanceBase
    {
        private int _currentBalance;
        
        public override event Action BalanceUpdated;
        
        public override void Add(int toAdd)
        {
            _currentBalance += toAdd;
            BalanceUpdated?.Invoke();
        }

        public override void SetBalance(int toSet)
        {
            _currentBalance = toSet;
            BalanceUpdated?.Invoke();
        }

        public override int Balance => _currentBalance;
    }
}