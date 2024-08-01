namespace Wallet.Balances
{
    public interface ICurrency 
    {
        public CurrencyType CurrencyType { get; }
        public int Amount { get; }

        public void SetAmount(int toSet);
    }
}