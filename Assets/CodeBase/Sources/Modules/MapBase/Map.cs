using CodeBase.Sources.Utils.Extensions;
using UnityEngine;

namespace CodeBase.Sources.Modules.MapBase
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Transform _ballSpawnPoint;
        
        [SerializeField] private Transform _coinStorage;
        [SerializeField] private Transform[] _coinSpawnPoints;

        public Vector3 BallSpawnPoint => _ballSpawnPoint.position;
        public Vector3[] CoinSpawnPoints => _coinSpawnPoints.GetPositions();
        
        public Transform CoinsStorage => _coinStorage;
    }
}