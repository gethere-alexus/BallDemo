using Coin.Presenter;
using UnityEngine;

namespace Infrastructure.Factories.CoinFactory
{
    public interface ICoinFactory
    {
        CoinPresenter CreateCoin(Vector3 at, Transform parent);
    }
}