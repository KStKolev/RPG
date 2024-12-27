namespace RPG.Core.Services.GameServices.InGameServices
{
    using RPG.Core.Interfaces.ScreenServices.InGameServices;
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Utilities.DataConstants.ScreenConstants;

    public class CreateMonsterService : ICreateMonsterService
    {

        public Monster CreateMonster(char[,] gameField)
        {
            const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;
            const int FIRST_ROW = InGameConstants.FIRST_FIELD_ROW;
            const int LAST_ROW = InGameConstants.LAST_FIELD_ROW;
            const int FIRST_COLUMN = InGameConstants.FIRST_FIELD_COLUMN;
            const int LAST_COLUMN = InGameConstants.LAST_FIELD_COLUMN;

            Monster monster = new Monster();
            monster.Setup();

            Random random = new Random();
            int fieldRow = random.Next(FIRST_ROW, LAST_ROW);
            int fieldColumn = random.Next(FIRST_COLUMN, LAST_COLUMN);

            while (gameField[fieldRow, fieldColumn] != FIELD_SYMBOL)
            {
                fieldRow = random.Next(FIRST_ROW, LAST_ROW);
                fieldColumn = random.Next(FIRST_COLUMN, LAST_COLUMN);
            }

            gameField[fieldRow, fieldColumn] = monster.CharacterSymbol;
            monster.FieldRow = fieldRow;
            monster.FieldColumn = fieldColumn;

            return monster;
        }

    }
}
