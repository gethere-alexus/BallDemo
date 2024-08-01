using Wallet.Balances;

namespace Utils.Extensions.Wallet
{
    public static class CurrencyExtensions
    {
        public static TCurrency AsCurrency<TCurrency>(this int amount) where TCurrency : class, ICurrency, new() => 
            new TCurrency().With(currency => currency.SetAmount(amount));
    }
}