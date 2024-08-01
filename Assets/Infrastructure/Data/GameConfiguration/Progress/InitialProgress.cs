using UnityEngine;

namespace Infrastructure.Data.GameConfiguration.Progress
{
    [CreateAssetMenu(menuName = "Configuration/InitialProgress", fileName = "InitialProgress")]
    public class InitialProgress : ScriptableObject
    {
        [SerializeField] private GameProgress.GameProgress _initialProgress;

        public GameProgress.GameProgress InitProgress => _initialProgress;
    }
}