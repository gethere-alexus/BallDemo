using CodeBase.Sources.Modules.Coin.Presenter;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.MapFactory.CoinFactory
{
    public interface ICoinFactory
    {
        CoinPresenter CreateCoin(Vector3 at, Transform parent);
    }
}