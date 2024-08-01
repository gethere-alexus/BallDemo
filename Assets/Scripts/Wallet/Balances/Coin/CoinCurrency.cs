namespace Wallet.Balances.Coin
{
    public class CoinCurrency : ICurrency
    {
        public CurrencyType CurrencyType => CurrencyType.Coin;

        public int Amount { get; private set; }

        public CoinCurrency()
        {
            
        }

        public void SetAmount(int toSet) => 
            Amount = toSet;

        public CoinCurrency(int amount) => 
            Amount = amount;
    }
}