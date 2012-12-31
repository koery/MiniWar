using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGFramework
{
    public enum WindowState { Full = 0, Snap1Quarter = 1, Snap3Quarter = 2 };
    public class MGDirector
    {
        public static float WinHeight;
        public static float WinWidth;
        private static MGDirector _sharedDirector;
        public ContentManager Content;
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public BasicEffect BasicEffect;
        public BasicEffect SpriteEffect;
        public bool Landscape;
        public int ScreenHeight;
        public int ScreenWidth;
        private MGScene _runningScene;
        private float _accumDt;
        private bool _paused;
        private bool _displayFPS;
        private Rectangle _drawRect;
        private SpriteFont _font;
        private float _frameRate;
        private int _frames;
        private float _renderRot;
        private RenderTarget2D _renderTarget;
        protected float ScreenToGameRatio;
        private Rectangle _sourceRect;
        public bool StartAd;
        private Matrix _projectionMatrix;
        private VertexDeclaration _vertexDeclaration;
        private Matrix _viewMatrix;
        private Matrix _worldMatrix;
        public Game Game { get; set; }
        //public static CoreWindow Window;
        public static WindowState WindowState;
        public static Rectangle WindowsBounds;
        public float SizeFactory = 1;

        private Texture2D _left;
        public bool b = false;
        protected MGDirector()
        {
            var WindowsBounds = new Rectangle(0, 0, 1366, 768);
            //Window = CoreWindow.GetForCurrentThread();
            WindowState = WindowState.Full;
            //WindowsBounds = Window.Bounds;
            //Window.SizeChanged += Window_SizeChanged;
            float proportion = (float)(WindowsBounds.Width / WindowsBounds.Height);

            if (proportion > 1.3f && proportion < 1.4f)
            {
                WindowState = WindowState.Snap3Quarter;
                Config.ORI_WIN_HEIGHT = 1024;
                MGDirector.WinWidth = 1024;
                b = true;
            }
            if (proportion > 1.7f && proportion < 1.8f)
            {
                Config.ORI_WIN_HEIGHT = 1366;
                MGDirector.WinWidth = 1366;
            }


        }

        //void Window_SizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        //{
        //    ////Window.Activate();
        //    ////Graphics.ApplyChanges();
        //    //var WindowsBounds = Window.Bounds;
        //    //WindowsBounds = Window.Bounds;
        //    //float proportion = (float)(WindowsBounds.Width / WindowsBounds.Height);
        //    //if (proportion > 1.3f && proportion < 1.4f)
        //    //{
        //    //    Config.ORI_WIN_HEIGHT = 1024;
        //    //    MGDirector.WinWidth = 1024;
        //    //}
        //    //if (proportion > 1.7f && proportion < 1.8f)
        //    //{
        //    //    MGDirector.WinWidth = 1366;
        //    //    Config.ORI_WIN_HEIGHT = 1366;
        //    //}


        //    //Window.Activate();
        //    //Graphics.ApplyChanges();
        //    WindowsBounds = Window.Bounds;
        //    float proportion = (float)(WindowsBounds.Width / WindowsBounds.Height);
        //    if (proportion > 1.3f && proportion < 1.4f)
        //    {
        //        Config.ORI_WIN_HEIGHT = 1024;
        //        MGDirector.WinWidth = 1024;
        //        int w = MetroGameWindow.Instance.ClientBounds.Width;
        //        if (b)
        //        {
        //            Config.ORI_WIN_HEIGHT = (int)(1366f * 1366f / 1024f);
        //            MGDirector.WinWidth = (int)(1366f * 1366f / 1024f);
        //            SizeFactory = w / 1024f;
        //        }
        //        else
        //        {
        //            SizeFactory = w / 1024f;
        //        }
        //    }
        //    else if (proportion > 1.7f && proportion < 1.8f)
        //    {
        //        MGDirector.WinWidth = 1366;
        //        Config.ORI_WIN_HEIGHT = 1366;
        //        int w = MetroGameWindow.Instance.ClientBounds.Width;
        //        SizeFactory = w / 1366f;
        //    }
        //    else
        //    {
        //        MGDirector.WinWidth = 1366;
        //        Config.ORI_WIN_HEIGHT = 1366;
        //        SizeFactory = 1;
        //    }

        //    //if (args.Size.Width == WindowsBounds.Width)
        //    //{
        //    //    WindowState = WindowState.Full;
        //    //}
        //    if (args.Size.Width <= 320.00)
        //    {
        //        WindowState = WindowState.Snap1Quarter;
        //    }
        //    else
        //    {
        //        WindowState = WindowState.Full;
        //    }
        //    //else
        //    //{
        //    //    WindowState = WindowState.Snap3Quarter;
        //    //}
        //    //if (WindowState == WindowState.Snap3Quarter)
        //    //{
        //    //    Config.ORI_WIN_HEIGHT = 1024;
        //    //    MGDirector.WinWidth = 1024;
        //    //}
        //    //else if (WindowState == WindowState.Full)
        //    //{
        //    //    float proportion = (float)(WindowsBounds.Width / WindowsBounds.Height);
        //    //    if (proportion > 1.3f && proportion < 1.4f)
        //    //    {
        //    //        Config.ORI_WIN_HEIGHT = 1024;
        //    //        MGDirector.WinWidth = 1024;
        //    //    }
        //    //    if (proportion > 1.7f && proportion < 1.8f)
        //    //    {
        //    //        Config.ORI_WIN_HEIGHT = 1366;
        //    //    }
        //    //}
        //    //else
        //    //{

        //    //}


        //    //WindowsBounds.Height = args.Size.Height;
        //    //WindowsBounds.Width = args.Size.Width;
        //}

        public Vector2 ConvertToGamePos(Vector2 screenPosition)
        {
            var x = this.ScreenToGameRatio * screenPosition.X;
            return new Vector2 { X = x, Y = WinHeight - (this.ScreenToGameRatio * screenPosition.Y) };
        }

        public bool DisplayFPS
        {
            get { return _displayFPS; }
            set { _displayFPS = value; }
        }

        public MGScene RunningScene
        {
            get { return _runningScene; }
        }

        public static MGDirector SharedDirector()
        {
            return _sharedDirector ?? (_sharedDirector = new MGDirector());
        }

        public void Update(float time)
        {
            if (!_paused)
            {
                _runningScene.UpdateNode(time);
            }
            _runningScene.InputUpdate();
            if (_displayFPS)
            {
                UpdateFPS(time);
            }
        }

        public void Draw()
        {
            Graphics.GraphicsDevice.SetRenderTarget(_renderTarget);
            Graphics.GraphicsDevice.Clear(Color.Black);
            _runningScene.DrawNode(SpriteBatch, null);
            if (_displayFPS)
            {
                SpriteBatch.Begin();
                SpriteBatch.DrawString(_font, string.Format("{0:F2}", _frameRate), new Vector2(5f, WinHeight - 30f),
                                       Color.Black, 0f, Vector2.Zero, new Vector2(1f, 1f), SpriteEffects.None, 0f);
                SpriteBatch.End();
            }
            Graphics.GraphicsDevice.SetRenderTarget(null);
            SpriteBatch.Begin();
            if (WindowState == WindowState.Snap1Quarter)
            {
                SpriteBatch.Draw(_left, new Vector2(), Color.White);
            }
            else
            {
                Rectangle rect = new Rectangle(_drawRect.X, _drawRect.Y, (int)(_drawRect.Width * SizeFactory), (int)(_drawRect.Height * SizeFactory));
                //_drawRect
                SpriteBatch.Draw(_renderTarget, rect, _sourceRect, Color.White, _renderRot, new Vector2(0f, 0f),
                                 SpriteEffects.None, 0f);
            }
            SpriteBatch.End();
        }

        public void UpdateFPS(float time)
        {
            _frames++;
            _accumDt += time;
            if (_accumDt > 0.1)
            {
                _frameRate = (_frames) / _accumDt;
                _frames = 0;
                _accumDt = 0f;
            }
        }

        public void Initialize()
        {
            InitializeBatch();
            initializeTextureMgr();
            InitializeOrientation();
            InitializeTransform();
            InitializeEffect();
            InitializeFont();
            //this.initializeAd();

            //_left = Content.Load<Texture2D>("left");
        }

        public void Pause()
        {
            this._paused = true;
        }

        public void Resume()
        {
            this._paused = false;
        }

        public void RunWithScene(MGScene scene)
        {
            this._runningScene = scene;
        }

        public void InitializeBatch()
        {
            SpriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
        }

        public void InitializeEffect()
        {
            _vertexDeclaration = VertexPositionColor.VertexDeclaration;
            BasicEffect = new BasicEffect(Graphics.GraphicsDevice);
            BasicEffect.VertexColorEnabled = true;
            _worldMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
            BasicEffect.World = _worldMatrix;
            BasicEffect.View = _viewMatrix;
            BasicEffect.Projection = _projectionMatrix;
            SpriteEffect = new BasicEffect(Graphics.GraphicsDevice);
            SpriteEffect.TextureEnabled = true;
            _worldMatrix = Matrix.CreateTranslation(0f, 0f, 0f);
            SpriteEffect.World = _worldMatrix;
            SpriteEffect.View = _viewMatrix;
            SpriteEffect.Projection = _projectionMatrix;
        }

        private void InitializeFont()
        {
            //_font = Content.Load<SpriteFont>("SpriteFont1");
        }

        public void InitializeTransform()
        {
            _viewMatrix = Matrix.CreateLookAt(new Vector3(0f, 0f, 1f), Vector3.Zero, Vector3.Up);
            _projectionMatrix = Matrix.CreateOrthographicOffCenter(0f, _renderTarget.Bounds.Width,
                                                                  _renderTarget.Bounds.Height, 0f, 1f, 1000f);
        }

        private void initializeTextureMgr()
        {
            MGTextureMgr.Init();
        }

        private void InitializeOrientation()
        {
            if (!Landscape)
            {
                SpriteBatch.GraphicsDevice.Viewport = new Viewport(0, 0, Config.TARGET_WIN_WIDTH, Config.TARGET_WIN_HEIGHT);

                Graphics.PreferredBackBufferWidth = Config.TARGET_WIN_WIDTH;
                Graphics.PreferredBackBufferHeight = Config.TARGET_WIN_HEIGHT;
                Graphics.SupportedOrientations = DisplayOrientation.Portrait;
                Graphics.ApplyChanges();
                ScreenWidth = Graphics.GraphicsDevice.Viewport.Width;
                ScreenHeight = Graphics.GraphicsDevice.Viewport.Height;
                _renderTarget = new RenderTarget2D(Graphics.GraphicsDevice, ScreenWidth, ScreenHeight);
                _drawRect = new Rectangle(0, 0, ScreenWidth, ScreenHeight);
                _sourceRect = new Rectangle(0, 0, Config.ORI_WIN_WIDTH, Config.ORI_WIN_HEIGHT);
                WinHeight = Config.ORI_WIN_WIDTH;
                WinWidth = Config.ORI_WIN_HEIGHT;
            }
            else
            {
                SpriteBatch.GraphicsDevice.Viewport = new Viewport(0, 0, Config.TARGET_WIN_HEIGHT, Config.TARGET_WIN_WIDTH);

                Graphics.PreferredBackBufferWidth = Config.TARGET_WIN_HEIGHT;
                Graphics.PreferredBackBufferHeight = Config.TARGET_WIN_WIDTH;
                Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
                Graphics.ApplyChanges();
                ScreenWidth = Graphics.GraphicsDevice.Viewport.Width;
                ScreenHeight = Graphics.GraphicsDevice.Viewport.Height;
                _renderTarget = new RenderTarget2D(Graphics.GraphicsDevice, ScreenWidth, ScreenHeight);
                _drawRect = new Rectangle(0, 0, ScreenWidth, ScreenHeight);
                _sourceRect = new Rectangle(0, 0, Config.ORI_WIN_HEIGHT, Config.ORI_WIN_WIDTH);
                WinWidth = Config.ORI_WIN_HEIGHT;
                WinHeight = Config.ORI_WIN_WIDTH;
            }
            ScreenToGameRatio = (1f * Config.ORI_WIN_WIDTH) / (Config.TARGET_WIN_WIDTH);
        }
    }
}