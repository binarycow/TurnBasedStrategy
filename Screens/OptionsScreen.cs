using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.IO;
using StrategyGame.Managers;
using StrategyGame.Media;

namespace StrategyGame.Screens
{
    internal class OptionsScreen : IScreen
    {
        private ScreenManager screenManager;
        private Label screenTitle;
        private Button debugButton;
        private Button backButton;
        private Button musicButton;
        private Button fullScreenButton;
        private Button resolutionButton;
        private Button lowResButton;
        private Button highDefButton;

        private List<Button> optionsMenuButtons;
        private List<Button> subMenuButtons;
        private bool inSubmenu;

        public void Initialize(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            // Subscribe to changes in resolution
            GraphicsManager.ResolutionChanged += this.Reinitialize;

            this.optionsMenuButtons = new List<Button>();
            this.subMenuButtons = new List<Button>();
            this.inSubmenu = false;

            Rectangle view = GraphicsManager.Viewport.Bounds;
            Point screenCenter = GraphicsManager.Viewport.Bounds.Center;
            
            this.resolutionButton = this.CreateCenterAlignedButton("Change Resolution", screenCenter.X, screenCenter.Y - 100);
            this.musicButton = this.CreateCenterAlignedButton("Toggle Music", screenCenter.X, screenCenter.Y - 25);
            this.debugButton = this.CreateCenterAlignedButton("Toggle Debug", screenCenter.X, screenCenter.Y + 50);

            this.lowResButton = this.CreateCenterAlignedButton("1280 x 720", screenCenter.X, screenCenter.Y - 100);
            this.highDefButton = this.CreateCenterAlignedButton("1920 x 1080", screenCenter.X, screenCenter.Y - 25);
            this.fullScreenButton = this.CreateCenterAlignedButton("Toggle Fullscreen", screenCenter.X, screenCenter.Y + 50);

            this.backButton = this.CreateCenterAlignedButton("Back", screenCenter.X, this.debugButton.Y + 125);
            this.screenTitle = this.AddLabelCenter("Options Menu", screenCenter.X, this.resolutionButton.Y - 175);

            this.optionsMenuButtons.Add(this.resolutionButton);
            this.optionsMenuButtons.Add(this.musicButton);
            this.optionsMenuButtons.Add(this.debugButton);

            this.subMenuButtons.Add(this.fullScreenButton);
            this.subMenuButtons.Add(this.lowResButton);
            this.subMenuButtons.Add(this.highDefButton);

            // Assign Event Subscribers
            this.backButton.ButtonPressed += this.PreviousScreen;
            this.debugButton.ButtonPressed += screenManager.ToggleDebug;
            this.musicButton.ButtonPressed += screenManager.ToggleMusic;
            this.resolutionButton.ButtonPressed += this.NavigateSubMenu;
            this.fullScreenButton.ButtonPressed += screenManager.ToggleFullscreen;
            this.lowResButton.ButtonPressed += screenManager.ToggleLowRes;
            this.highDefButton.ButtonPressed += screenManager.ToggleHighRes;

            this.SetMenuButtonAudio(this.debugButton);
            this.SetMenuButtonAudio(this.musicButton);
            this.SetMenuButtonAudio(this.resolutionButton);
            this.SetMenuButtonAudio(this.fullScreenButton);
            this.SetMenuButtonAudio(this.lowResButton);
            this.SetMenuButtonAudio(this.highDefButton);

            this.backButton.Hover = Audio.OnMenuHover;
            this.backButton.Click = Audio.MenuBack;
        }
        private void PreviousScreen(object sender, EventArgs e)
        {
            if (this.inSubmenu)
                this.inSubmenu = !this.inSubmenu;
            else
                this.screenManager.PreviousScreen(sender, e);
        }

        private void NavigateSubMenu(object sender, EventArgs e)
        {
            this.inSubmenu = !this.inSubmenu;
        }

        public void Update(GameTime gameTime)
        {
            if (this.inSubmenu)
                foreach (var button in this.subMenuButtons)
                    button.Update(gameTime);
            else
                foreach (var button in this.optionsMenuButtons)
                    button.Update(gameTime);
            this.backButton.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(GraphicsManager.GetTextureOfColor(Settings.BackdropColor), GraphicsManager.Viewport.Bounds, Color.White);
            
            this.screenTitle.Draw(spriteBatch);
            this.backButton.Draw(spriteBatch);

            if (this.inSubmenu)
                foreach (var button in this.subMenuButtons)
                    button.Draw(spriteBatch);
            else
                foreach (var button in this.optionsMenuButtons)
                    button.Draw(spriteBatch);

            spriteBatch.End();
        }

        public Button CreateButton(string title, int x, int y)
        {
            return new Button(title, x, y, Textures.Empty);
        }

        public Button CreateCenterAlignedButton(string title, int x, int y)
        {
            Button button = this.CreateButton(title, x, y);
            GraphicsManager.CenterGameObjectX(button);
            return button;
        }

        public Label AddLabelCenter(string title, int x, int y)
        {
            Label label = new Label(title, new Vector2(x, y));
            GraphicsManager.CenterGameObjectX(label);
            return label;
        }

        public void SetMenuButtonAudio(Button button)
        {
            button.Hover = Audio.OnMenuHover;
            button.Click = Audio.MenuForward;
        }

        public void Reinitialize(object sender, EventArgs e)
        {
            this.Initialize(this.screenManager);
        }
    }

}
