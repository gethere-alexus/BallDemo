using System;
using CodeBase.Infrastructure.Data.Progress.Wallet;
using Newtonsoft.Json;

namespace CodeBase.Infrastructure.Data.Progress
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