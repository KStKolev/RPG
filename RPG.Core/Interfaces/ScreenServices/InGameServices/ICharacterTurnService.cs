namespace RPG.Core.Interfaces.ScreenServices.InGameServices
{
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Data.Entities;

    public interface ICharacterTurnService
    {

        void CharacterTurn(char[,] gameField, Character character,
           List<Monster> monsterCollection, GameSession gameSession);

    }
}
