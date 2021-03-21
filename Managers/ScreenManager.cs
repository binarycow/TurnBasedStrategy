using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Screens;

namespace StrategyGame.Managers
{
    internal class ScreenManager : DrawableGameComponent
    {
        private readonly Stack<IScreen> screens;
        private IScreen currentScreen;
        private readonly SpriteBatch spriteBatch;

        public ScreenManager(IScreen startScreen, StrategyGame game) : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            screens = new Stack<IScreen>();
            SetCurrentScreen(startScreen);
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        private void SetCurrentScreen(IScreen startScreen)
        {
            currentScreen = startScreen;
            if (currentScreen is GameScreen)
            {
                GameState.IsOnMenuScreen = false;
                GameState.GameIsRunning = true;
            }
            else
                GameState.IsOnMenuScreen = true;
        }

        public void PushScreen(IScreen screen)
        {
            screens.Push(screen);
        }

        public void PopScreen()
        {
            screens.Pop();
            if (screens.Count > 0)
                SetCurrentScreen(screens.Peek());
            else
                Game.Exit();
        }

        public void RemoveAllScreens()
        {
            while (screens.Count > 0)
                screens.Pop();
        }

        public void PreviousScreen(object sender, EventArgs e)
        {
            PopScreen();
        }

        public void Menu()
        {
            SetCurrentScreen(new MenuScreen());
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void Options(object sender, EventArgs args)
        {
            SetCurrentScreen(new OptionsScreen());
            PushScreen(currentScreen);
            currentScreen.Initialize(this);
        }

        public void NewGame(object source, EventArgs args)
        {
            if (GameState.GameIsRunning)
            {
                while (screens.Count > 1)
                    screens.Pop();
                SetCurrentScreen(screens.Peek());
            }
            else
            {
                RemoveAllScreens();
                SetCurrentScreen(new GameScreen());
                PushScreen(currentScreen);
                currentScreen.Initialize(this);
            }
        }

        internal void ToggleFullscreen(object sender, EventArgs e)
        {
            GameState.IsFullScreen = true;
        }

        internal void ToggleHighRes(object sender, EventArgs e)
        {
            GameState.IsHighResolution = true;
        }

        internal void ToggleLowRes(object sender, EventArgs e)
        {
            GameState.IsLowResolution = true;
        }

        public void ToggleDebug(object sender, EventArgs e)
        {
            GameState.DebugColorMode = !GameState.DebugColorMode;
        }

        public void ToggleMusic(object sender, EventArgs e)
        {
            GameState.ToggleAudio = !GameState.ToggleAudio;
        }

        public void ExitGame(object source, EventArgs args)
        {
            Game.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
            this.currentScreen.Draw(this.spriteBatch);
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Game.IsActive)
            {
                GameState.WindowInFocus = true;
                currentScreen.Update(gameTime);
            }
            else
                GameState.WindowInFocus = false;
        }

    }
}
