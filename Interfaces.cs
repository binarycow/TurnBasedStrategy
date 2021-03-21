using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Managers;

namespace StrategyGame
{
    internal interface IScreen
    {
        void Initialize(ScreenManager screenManager);
        void Reinitialize(object sender, EventArgs e);
        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
    }
}
