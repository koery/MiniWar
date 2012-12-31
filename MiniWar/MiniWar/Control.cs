using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MGFramework;

namespace MiniWar
{
    class Control
    {
        #region SceneType enum

        public enum SceneType
        {
            LaunchLoadingSceneType,
            StartSceneType,
            MainMenuSceneType,
            MainGameSceneType,
            GameOverSceneType,
            TeachingSceneType,
        }

        #endregion

        private static Control _sharedControl;
        public static Control SharedControl()
        {
            return _sharedControl ?? (_sharedControl = new Control());
        }

        private readonly Random _random;
        private MGScene _curScene;
        private int _curSceneType;
        private Control()
        {
            _random = new Random();
            LoadGame();
        }

        public void ReplaceScene(SceneType sceneType)
        {
            _curSceneType = (int)sceneType;
            switch (sceneType)
            {
                case SceneType.LaunchLoadingSceneType:
                    _curScene = new LaunchLoadingScene();
                    break;
                case SceneType.StartSceneType:
                    _curScene = new StartSceen();
                    break;
                case SceneType.MainMenuSceneType:
                    _curScene = new MainMenuScene();
                    break;
                case SceneType.MainGameSceneType:
                    _curScene = new MainGameScene();
                    break;
                case SceneType.GameOverSceneType:
                    _curScene = new GameOverScene();
                    break;
                case SceneType.TeachingSceneType:
                    _curScene = new TeachingScene();
                    break;
            }
            if (MGDirector.SharedDirector().RunningScene == null)
            {
                MGDirector.SharedDirector().RunWithScene(_curScene);
                _curScene.OnSceneActive();
            }
            else
            {
                MGDirector.SharedDirector().RunWithScene(_curScene);
            }
        }

        public bool Back()
        {
            switch (_curSceneType)
            {
                case 0:
                    return _curScene.Back();
                //.....
            }
            return false;
        }


        public void LoadGame()
        {

        }

        public void SaveGame()
        {

        }

        public int GetRandom(int maxMinusOne)
        {
            return _random.Next(0, maxMinusOne);
        }

        public int GetRandom(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
}
