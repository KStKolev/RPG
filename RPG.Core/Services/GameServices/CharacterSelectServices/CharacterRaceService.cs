namespace RPG.Core.Services.GameServices.CharacterSelectServices
{
    using RPG.Core.Interfaces;
    using RPG.Core.Interfaces.ScreenServices.CharacterSelectServices;
    using RPG.Utilities.DataConstants.ScreenConstants;

    public class CharacterRaceService : ICharacterRaceService
    {

        private readonly IUserInputService userInputService;

        public CharacterRaceService(IUserInputService userInputService)
        {
            this.userInputService = userInputService;
        }

        public int PickCharacterRace()
        {
            PrintCharacterRaceOptions();

            int warriorOption = CharacterSelectConstants.WARRIOR_RACE;
            int archerOption = CharacterSelectConstants.ARCHER_RACE;
            int mageOption = CharacterSelectConstants.MAGE_RACE;

            Func<int, bool> inputRaceCondition = input => (input != warriorOption) &&
            (input != archerOption) && (input != mageOption);

            int raceOption = userInputService.GetUserInput(inputRaceCondition);
            Console.WriteLine(raceOption);

            return raceOption;
        }

        private void PrintCharacterRaceOptions()
        {
            Console.WriteLine(CharacterSelectConstants.CHOOSE_CHARACTER_TYPE);
            Console.WriteLine(CharacterSelectConstants.OPTIONS);
            Console.WriteLine(CharacterSelectConstants.WARRIOR_TYPE);
            Console.WriteLine(CharacterSelectConstants.ARCHER_TYPE);
            Console.WriteLine(CharacterSelectConstants.MAGE_TYPE);
            Console.WriteLine(CharacterSelectConstants.PLAYER_PICK);
        }

    }
}
