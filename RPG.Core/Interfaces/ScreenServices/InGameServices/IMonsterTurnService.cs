namespace RPG.Core.Interfaces.ScreenServices.InGameServices
{
    using RPG.Data.Entities.GameEntityTypes;

    public interface IMonsterTurnService
    {

        void MonsterTurn(List<Monster> monsterCollection,
            Character character, char[,] gameField);

    }
}
