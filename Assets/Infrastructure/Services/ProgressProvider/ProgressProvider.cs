using System.Collections.Generic;
using System.IO;
using APIs.GameConfigReader;
using APIs.ProgressInteraction;
using Infrastructure.Data.GameConfiguration;
using Infrastructure.Data.GameProgress;
using Infrastructure.Services.ConfigurationProvider;
using Newtonsoft.Json;
using UnityEngine;
using File = System.IO.File;

namespace Infrastructure.Services.ProgressProvider
{
    public class ProgressProvider : IProgressProvider, IGameConfigReader
    {
        private List<IProgressReader> ProgressReaders { get; } = new();
        private List<IProgressWriter> ProgressWriters { get; } = new();

        private readonly string _savePath;

        private GameProgress _initialProgress;
        private GameProgress _gameProgress;

        public ProgressProvider(IConfigurationProvider configurationProvider)
        {
            configurationProvider.LoadConfiguration(this);
            
            _savePath = Path.Combine(Application.persistentDataPath, "GameProgress.json");
            
            LoadProgress();
        }

        public void LoadConfiguration(GameConfiguration gameConfiguration)
        {
            _initialProgress = gameConfiguration.InitialProgress.InitProgress;
        }

        public void LoadProgressToObservers()
        {
            foreach (var reader in ProgressReaders)
                reader.LoadProgress(_gameProgress);
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in ProgressWriters)
            {
                progressWriter.SaveProgress(_gameProgress);
            }
            
            string json = JsonConvert.SerializeObject(_gameProgress, Formatting.Indented);
            File.WriteAllText(_savePath, json);
        }

        public void RegisterObserver(IProgressReader reader)
        {
            ProgressReaders.Add(reader);

            if (reader is IProgressWriter writer)
                ProgressWriters.Add(writer);
        }

        private void LoadProgress()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                _gameProgress = JsonConvert.DeserializeObject<GameProgress>(json);
            }
            else
                _gameProgress = _initialProgress.DeepCopy();
        }
    }
}