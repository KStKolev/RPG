namespace RPG.Core.Interfaces.ScreenServices.InGameServices
{
    using RPG.Data.Entities.GameEntityTypes;

    public interface ICreateMonsterService
    {

        Monster CreateMonster(char[,] gameField);

    }
}
