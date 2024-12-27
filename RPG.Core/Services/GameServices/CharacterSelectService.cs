namespace RPG.Core.Services.GameServices
{
    using RPG.Core.Interfaces.ScreenServices;
    using RPG.Core.Interfaces.ScreenServices.CharacterSelectServices;
    using RPG.Data.Entities.GameEntityTypes;

    public class CharacterSelectService : ICharacterSelectService
    {

        private readonly ICharacterRaceService characterRaceService;
        private readonly ICreateCharacterService createCharacterService;

        public CharacterSelectService(ICharacterRaceService characterRaceService, 
            ICreateCharacterService createCharacterService)
        {
            this.characterRaceService = characterRaceService;
            this.createCharacterService = createCharacterService;
        }

        public Character PickCharacter()
        {
            int raceOption = characterRaceService.PickCharacterRace();
            var(strengthPoints, agilityPoints, intelligencePoints) = createCharacterService.BuffPoints();

            return createCharacterService.CreateCharacter(raceOption, strengthPoints, 
                agilityPoints, intelligencePoints);
        }

    }
}
