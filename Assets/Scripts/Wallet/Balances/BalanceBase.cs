using System;

namespace Wallet.Balances
{
    public abstract class BalanceBase
    {
        public abstract event Action BalanceUpdated;
        public abstract void Add(int toAdd);
        public abstract void SetBalance(int toSet);
        public abstract int Balance { get;}
    }
}