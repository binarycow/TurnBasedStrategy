﻿using StrategyGame.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrategyGame.Screens;
using System.Diagnostics;

namespace StrategyGame
{
    public class StrategyGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ScreenManager screenManager;

        public StrategyGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentService.Instance.LoadContent(Content, GraphicsDevice);
            screenManager = new ScreenManager(new MenuScreen(), this);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            if (Input.KeyIsPressed(Keys.Escape) && GameState.GameIsRunning)
                screenManager.Menu();
            screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            screenManager.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
