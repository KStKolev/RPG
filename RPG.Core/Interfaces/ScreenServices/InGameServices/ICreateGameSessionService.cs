namespace RPG.Core.Interfaces.ScreenServices.InGameServices
{
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Data.Entities;

    public interface ICreateGameSessionService
    {

        GameSession CreateGameSession(Character character);

    }
}
