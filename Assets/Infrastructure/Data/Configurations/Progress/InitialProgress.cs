using Infrastructure.Data.Progress;
using UnityEngine;

namespace Infrastructure.Data.Configurations.Progress
{
    [CreateAssetMenu(menuName = "Configuration/InitialProgress", fileName = "InitialProgress")]
    public class InitialProgress : ScriptableObject
    {
        [SerializeField] private GameProgress _initialProgress;

        public GameProgress InitProgress => _initialProgress;
    }
}