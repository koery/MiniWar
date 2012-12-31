#region Using Statements

using System;
using MGFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Microsoft.Xna.Framework.Media;


#endregion

namespace MiniWar
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this)
                            {
                                PreferredBackBufferWidth = Config.TARGET_WIN_HEIGHT,
                                PreferredBackBufferHeight = Config.TARGET_WIN_WIDTH,
                                //IsFullScreen = true
                            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            MGDirector.SharedDirector().Game = this;
            MGDirector.SharedDirector().Graphics = _graphics;
            MGDirector.SharedDirector().Content = Content;
            MGDirector.SharedDirector().Landscape = true;
            MGDirector.SharedDirector().DisplayFPS = false;

        }

        public void CompleteLoading()
        {
            Control.SharedControl().ReplaceScene(Control.SceneType.LaunchLoadingSceneType);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MGDirector.SharedDirector().Initialize();
            CompleteLoading();
            AudioMgr.LoadAudio();

            AudioMgr.PlayBMG();
        }

        protected override void UnloadContent()
        {
        }



        protected override void OnExiting(object sender, EventArgs args)
        {
            Control.SharedControl().SaveGame();
            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MGDirector.SharedDirector().Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            MGDirector.SharedDirector().Draw();
            MainGameLogic.SharedMainGameLogic().UpdateGameLoop((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Draw(gameTime);
        }
    }
}