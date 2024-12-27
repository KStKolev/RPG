namespace RPG.Core.Services.GameServices.InGameServices
{
    using RPG.Core.Interfaces.ScreenServices.InGameServices;
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Utilities.DataConstants.ScreenConstants;

    public class GameFieldService : IGameFieldService
    {

        public char[,] CreateField(Character character)
        {
            const int ROWS = InGameConstants.ROWS_COUNT;
            const int COLUMNS = InGameConstants.COLUMN_COUNT;

            char[,] gameField = new char[ROWS, COLUMNS];
            const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;

            for (int rowIndex = 0; rowIndex < ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < COLUMNS; columnIndex++)
                {
                    gameField[rowIndex, columnIndex] = FIELD_SYMBOL;
                }
            }

            character.FieldRow = InGameConstants.INITIAL_FIELD_ROW;
            character.FieldColumn = InGameConstants.INITIAL_FIELD_COLUMN;
            gameField[character.FieldRow, character.FieldColumn] = character.CharacterSymbol;
            return gameField;
        }

        public void PrintField(char[,] gameField)
        {
            const int ROWS = InGameConstants.ROWS_COUNT;
            const int COLUMNS = InGameConstants.COLUMN_COUNT;

            for (int rowIndex = 0; rowIndex < ROWS; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < COLUMNS; columnIndex++)
                {
                    Console.Write(gameField[rowIndex, columnIndex]);
                }
                Console.WriteLine();
            }
        }

        public void PrintHealthAndManaPoints(Character character)
        {
            Console.Write($"{InGameConstants.HEALTH_INFO} {character.Health}    ");
            Console.Write($"{InGameConstants.MANA_INFO} {character.Mana}");
            Console.WriteLine();
            Console.WriteLine();
        }

    }
}
