namespace RPG.Core.Services
{
    using RPG.Core.Interfaces;
    using RPG.Utilities.Enums;
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Core.Interfaces.ScreenServices;

    public class ScreenService : IScreenService
    {

        private readonly IMainMenuService mainMenuService;
        private readonly ICharacterSelectService characterSelectionService;
        private readonly IInGameService gameService;
        private Screen screen;

        public ScreenService(IMainMenuService _mainMenuService, 
            ICharacterSelectService _characterSelectionService, IInGameService _gameService)
        {
            mainMenuService = _mainMenuService;
            characterSelectionService = _characterSelectionService;
            gameService = _gameService;
        }

        public void ManageGameScreen()
        {
            screen = Screen.MainMenu;
            Character character = new Character();

            while (screen != Screen.Exit)
            {
                switch (screen)
                {
                    case Screen.MainMenu:
                        mainMenuService.ShowMainMenu();
                        screen = Screen.CharacterSelect;
                        break;
                    case Screen.CharacterSelect:
                        character = characterSelectionService.PickCharacter();
                        screen = Screen.InGame;
                        break;
                    case Screen.InGame:
                        gameService.PlayGame(character);
                        screen = Screen.Exit;
                        break;
                }
            }
        }

    }
}
