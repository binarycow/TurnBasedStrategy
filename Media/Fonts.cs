using Microsoft.Xna.Framework.Graphics;
using StrategyGame.Managers;

namespace StrategyGame.Media
{
    public class Fonts
    {
        public static SpriteFont MenuFont { get; } = ContentService.Instance.Fonts["MenuFont"];
        public static SpriteFont UI { get; } = ContentService.Instance.Fonts["InGameFont"];
        public static SpriteFont Small { get; } = ContentService.Instance.Fonts["DetailFont"];

    }
}
