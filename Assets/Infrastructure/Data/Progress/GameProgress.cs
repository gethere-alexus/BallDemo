using System;
using Infrastructure.Data.Progress.Wallet;
using Newtonsoft.Json;

namespace Infrastructure.Data.Progress
{
    [Serializable]
    public class GameProgress
    {
        public WalletProgress WalletProgress;
        
        public GameProgress DeepCopy()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<GameProgress>(serialized);
        }
    }
}