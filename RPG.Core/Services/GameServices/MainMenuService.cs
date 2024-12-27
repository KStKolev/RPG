namespace RPG.Core.Services.GameServices
{
    using RPG.Core.Interfaces.ScreenServices;
    using RPG.Utilities.DataConstants.ScreenConstants;

    public class MainMenuService : IMainMenuService
    {

        public void ShowMainMenu()
        {
            Console.WriteLine(MainMenuConstants.GREETING);
            Console.Write(MainMenuConstants.PRESS_KEY_TO_PLAY);
            Console.ReadKey(intercept: true);
            Console.Clear();
        }

    }
}
