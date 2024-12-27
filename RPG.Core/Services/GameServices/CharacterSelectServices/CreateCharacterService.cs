namespace RPG.Core.Services.GameServices.CharacterSelectServices
{
    using RPG.Core.Interfaces;
    using RPG.Core.Interfaces.ScreenServices.CharacterSelectServices;
    using RPG.Data;
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Data.Entities.GameEntityTypes.CharacterTypes;
    using RPG.Utilities.DataConstants.ScreenConstants;

    public class CreateCharacterService : ICreateCharacterService
    {

        private readonly IUserInputService userInputService;
        private readonly RPGDbContext dbContext;

        public CreateCharacterService(RPGDbContext dbContext, IUserInputService userInputService)
        {
            this.dbContext = dbContext;
            this.userInputService = userInputService;
        }

        public (int, int, int) BuffPoints()
        {
            Console.WriteLine(CharacterSelectConstants.BUFF_STATS_QUESTION);
            Console.WriteLine(CharacterSelectConstants.BUFF_STATS_RESPONSE);

            const char ACCEPT_BUFF = CharacterSelectConstants.ACCEPT_BUFF;
            const char DECLINE_BUFF = CharacterSelectConstants.DECLINE_BUFF;

            Func<char, bool> inputResponseCondition = input => (input != ACCEPT_BUFF) && (input != DECLINE_BUFF);

            int strengthPoints = 0, agilityPoints = 0, intelligencePoints = 0;

            char response = userInputService.GetUserInput(inputResponseCondition);
            Console.WriteLine(response);

            if (response == ACCEPT_BUFF)
            {
                SetBuffToStatPoints(ref strengthPoints, ref agilityPoints, ref intelligencePoints);
            }

            return (strengthPoints, agilityPoints, intelligencePoints);
        }

        public Character CreateCharacter(int raceOption, int strengthPoints,
            int agilityPoints, int intelligencePoints)
        {
            const int WARRIOR_OPTION = CharacterSelectConstants.WARRIOR_RACE;
            const int ARCHER_OPTION = CharacterSelectConstants.ARCHER_RACE;
            const int MAGE_OPTION = CharacterSelectConstants.MAGE_RACE;
            Character character;

            switch (raceOption)
            {
                case WARRIOR_OPTION:
                    character = new Warrior(strengthPoints, agilityPoints, intelligencePoints);
                    break;
                case ARCHER_OPTION:
                    character = new Archer(strengthPoints, agilityPoints, intelligencePoints);
                    break;
                case MAGE_OPTION:
                    character = new Mage(strengthPoints, agilityPoints, intelligencePoints);
                    break;
                default:
                    Environment.Exit(0);
                    return null;
            }

            character.Setup();
            dbContext.Characters.Add(character);
            dbContext.SaveChanges();
            Console.Clear();
            return character;
        }

        private void SetBuffToStatPoints(ref int strengthPoints, 
            ref int agilityPoints, ref int intelligencePoints)
        {
            int bonusStatsPoints = CharacterSelectConstants.BONUS_POINTS;
            const int STATS_OPTIONS = CharacterSelectConstants.STATS_OPTIONS;
            const int STRENGTH_OPTION = CharacterSelectConstants.STRENGTH_OPTION;
            const int AGILITY_OPTION = CharacterSelectConstants.AGILITY_OPTION;
            const int INTELLIGENCE_OPTION = CharacterSelectConstants.INTELLIGENCE_OPTION;

            for (int currentOption = 1; currentOption <= STATS_OPTIONS; currentOption++)
            {
                if (bonusStatsPoints <= 0)
                {
                    break;
                }

                Func<int, bool> inputBonusStatsCondition = input => (input < 0) || (input > bonusStatsPoints);
                Console.WriteLine($"{CharacterSelectConstants.REMAINING_POINTS} {bonusStatsPoints}");

                switch (currentOption)
                {
                    case STRENGTH_OPTION:
                        Console.WriteLine(CharacterSelectConstants.INCREASE_STRENGTH_STATS);
                        strengthPoints = userInputService.GetUserInput(inputBonusStatsCondition);
                        Console.WriteLine(strengthPoints);
                        bonusStatsPoints -= strengthPoints;
                        break;
                    case AGILITY_OPTION:
                        Console.WriteLine(CharacterSelectConstants.INCREASE_AGILITY_STATS);
                        agilityPoints = userInputService.GetUserInput(inputBonusStatsCondition);
                        Console.WriteLine(agilityPoints);
                        bonusStatsPoints -= agilityPoints;
                        break;
                    case INTELLIGENCE_OPTION:
                        Console.WriteLine(CharacterSelectConstants.INCREASE_INTELLIGENCE_STATS);
                        intelligencePoints = userInputService.GetUserInput(inputBonusStatsCondition);
                        Console.WriteLine(intelligencePoints);
                        bonusStatsPoints -= intelligencePoints;
                        break;
                }
            }
        }

    }
}
