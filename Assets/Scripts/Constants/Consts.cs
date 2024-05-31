using System.IO;
using UnityEngine;

namespace Constants
{
    public static class Consts
    {
        public static class AudioState
        {
            public const string AudioStateKey = "AUDIOSTATE_KEY";
            public const string AudioVolumeValueKey = "AUDIOVOLUMEVALUE_KEY ";
        }
        
        public static class Scenes
        {
            public const string MainMenuScene = "MainMenuScene";
            public const string GameScene = "GameScene";
            public const string LoadingScene = "LoadingScene";
        }

        public static class SaveSystem
        {
            public static readonly string ItemPath = Path.Combine(Application.persistentDataPath + "/upgradableItem.json");
            public static readonly string UserProgress = Path.Combine(Application.persistentDataPath + "/UserProgress.json");
        }
        
        public static class Values
        {
            public const int DefaultMoneyCount = 60;
        }
    }
}
