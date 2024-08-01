using System;
using Infrastructure.Data.GameProgress.Wallet;
using Newtonsoft.Json;

namespace Infrastructure.Data.GameProgress
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