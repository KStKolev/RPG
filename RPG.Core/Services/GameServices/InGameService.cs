namespace RPG.Core.Services.GameServices
{
    using RPG.Core.Interfaces.ScreenServices;
    using RPG.Core.Interfaces.ScreenServices.InGameServices;
    using RPG.Data.Entities;
    using RPG.Data.Entities.GameEntityTypes;

    public class InGameService : IInGameService
    {

        private readonly ICreateGameSessionService createGameSessionService;
        private readonly IMonsterTurnService monsterTurnService;
        private readonly ICharacterTurnService characterTurnService;
        private readonly IGameFieldService gameFieldService;
        private readonly ICreateMonsterService createMonsterService;

        public InGameService(ICreateGameSessionService createGameSessionService, 
            IMonsterTurnService monsterTurnService, ICharacterTurnService characterTurnService, 
            IGameFieldService gameFieldService, ICreateMonsterService createMonsterService)
        {
            this.createGameSessionService = createGameSessionService;
            this.monsterTurnService = monsterTurnService;
            this.characterTurnService = characterTurnService;
            this.gameFieldService = gameFieldService;
            this.createMonsterService = createMonsterService;
        }

        public void PlayGame(Character character)
        {
            List<Monster> monsterCollection = new List<Monster>();
            GameSession gameSession = createGameSessionService.CreateGameSession(character);
            char[,] gameField = gameFieldService.CreateField(character);

            while (character.Health > 0)
            {
                Monster newMonster = createMonsterService.CreateMonster(gameField);
                monsterCollection.Add(newMonster);

                gameFieldService.PrintHealthAndManaPoints(character);
                gameFieldService.PrintField(gameField);
                characterTurnService.CharacterTurn(gameField, character, monsterCollection, gameSession);
                monsterTurnService.MonsterTurn(monsterCollection, character, gameField);
                Console.Clear();
            }

            gameFieldService.PrintHealthAndManaPoints(character);
            gameFieldService.PrintField(gameField);
        }

    }
}
