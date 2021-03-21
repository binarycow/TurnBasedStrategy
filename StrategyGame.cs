using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrategyGame.Screens;
using System.Diagnostics;
using StrategyGame.IO;

namespace StrategyGame
{
    public class StrategyGame : Game
    {
        private GraphicsDeviceManager graphics;
        private readonly ScreenManager screenManager;

        public StrategyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            this.screenManager = new ScreenManager(new MenuScreen(), this);
            this.Components.Add(this.screenManager);
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            ContentService.Instance.LoadContent(Content, GraphicsDevice);
            GraphicsManager.Initialize(graphics);
            EventService.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            if (Input.KeyIsPressed(Keys.Escape) && GameState.GameIsRunning)
                screenManager.Menu();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Settings.GraphicsDeviceColor);
            base.Draw(gameTime);
        }
    }
}
