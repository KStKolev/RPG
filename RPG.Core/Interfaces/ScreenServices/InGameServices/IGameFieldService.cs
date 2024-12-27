namespace RPG.Core.Interfaces.ScreenServices.InGameServices
{
    using RPG.Data.Entities.GameEntityTypes;

    public interface IGameFieldService
    {

        char[,] CreateField(Character character);
        void PrintHealthAndManaPoints(Character character);
        void PrintField(char[,] gameField);

    }
}
