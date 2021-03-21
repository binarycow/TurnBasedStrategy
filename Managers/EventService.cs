namespace StrategyGame.Managers
{
    internal static class EventService
    {
        public static void Initialize()
        {
            GameState.FullscreenEventHandler += GraphicsManager.ToggleFullscreen;
            GameState.LowResEventHandler += GraphicsManager.ToggleLowRes;
            GameState.HighResEventHandler += GraphicsManager.ToggleHighRes;
        }
    }
}
