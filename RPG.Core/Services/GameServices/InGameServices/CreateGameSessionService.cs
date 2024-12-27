namespace RPG.Core.Services.GameServices.InGameServices
{
    using RPG.Core.Interfaces.ScreenServices.InGameServices;
    using RPG.Data;
    using RPG.Data.Entities;
    using RPG.Data.Entities.GameEntityTypes;

    public class CreateGameSessionService : ICreateGameSessionService
    {

        private readonly RPGDbContext dbContext;

        public CreateGameSessionService(RPGDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public GameSession CreateGameSession(Character character)
        {
            GameSession gameSession = new GameSession();
            gameSession.CharacterId = character.Id;
            gameSession.Character = character;
            dbContext.GameSessions.Add(gameSession);
            dbContext.SaveChanges();
            return gameSession;
        }

    }
}
