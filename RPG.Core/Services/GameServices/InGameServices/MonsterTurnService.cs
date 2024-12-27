namespace RPG.Core.Services.GameServices.InGameServices
{
    using RPG.Core.Interfaces.ScreenServices.InGameServices;
    using RPG.Data;
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Utilities.DataConstants.ScreenConstants;

    public class MonsterTurnService : IMonsterTurnService
    {

        private readonly RPGDbContext dbContext;

        public MonsterTurnService(RPGDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void MonsterTurn(List<Monster> monsterCollection, 
            Character character, char[,] gameField)
        {
            const int FIELD_ROWS = InGameConstants.ROWS_COUNT;
            const int FIELD_COLUMNS = InGameConstants.COLUMN_COUNT;
            const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;

            foreach (var monster in monsterCollection)
            {
                int rowDirection = (character.FieldRow > monster.FieldRow ? 1 
                    : (character.FieldRow < monster.FieldRow ? -1 : 0))
                    + monster.FieldRow;

                int columnDirection = (character.FieldColumn > monster.FieldColumn ? 1
                    : (character.FieldColumn < monster.FieldColumn ? -1 : 0))
                    + monster.FieldColumn;

                if (rowDirection >= 0 && rowDirection < FIELD_ROWS 
                    && columnDirection >= 0 && columnDirection < FIELD_COLUMNS)
                {
                    if (gameField[rowDirection, columnDirection] == FIELD_SYMBOL)
                    {
                        MoveMonster(gameField, monster, rowDirection, columnDirection);
                    }
                    else if (gameField[rowDirection, columnDirection] == character.CharacterSymbol)
                    {
                        character.Health -= monster.Damage;
                        if (character.Health <= 0)
                        {
                            character.Health = 0;
                            dbContext.SaveChanges();
                            return;
                        }
                    }
                    else if (gameField[rowDirection, columnDirection] == monster.CharacterSymbol)
                    {
                        TryMoveMonster(gameField, monster, rowDirection, columnDirection);
                    }
                }
            }
        }

        private void MoveMonster(char[,] gameField, Monster monster, 
            int newRow, int newColumn)
        {
            const char fieldSymbol = InGameConstants.FIELD_SYMBOL;
            gameField[monster.FieldRow, monster.FieldColumn] = fieldSymbol;
            gameField[newRow, newColumn] = monster.CharacterSymbol;
            monster.FieldRow = newRow;
            monster.FieldColumn = newColumn;
        }

        private void TryMoveMonster(char[,] gameField, Monster monster, 
            int rowDirection, int columnDirection)
        {
            if (rowDirection == 0 && columnDirection != 0)
            {
                TryMoveInRow(gameField, monster, columnDirection);
            }
            else if (rowDirection != 0 && columnDirection == 0)
            {
                TryMoveInColumn(gameField, monster, rowDirection);
            }
            else if (rowDirection != 0 && columnDirection != 0)
            {
                TryMoveDiagonally(gameField, monster);
            }
        }

        private void TryMoveInRow(char[,] gameField, Monster monster, int columnDirection)
        {
            int[] directions = { -1, 1 };
            const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;
            const int FIELD_ROWS = InGameConstants.ROWS_COUNT;
            const int FIELD_COLUMNS = InGameConstants.COLUMN_COUNT;

            foreach (int rowDir in directions)
            {
                int targetRow = monster.FieldRow + rowDir;
                int targetColumn = monster.FieldColumn + columnDirection;

                if ((targetRow >= 0 && targetRow < FIELD_ROWS) && 
                    (targetColumn >= 0 && targetColumn < FIELD_COLUMNS))
                {
                    if (gameField[targetRow, targetColumn] == FIELD_SYMBOL)
                    {
                        MoveMonster(gameField, monster, targetRow, targetColumn);
                        return;
                    }
                }
            }
        }

        private void TryMoveInColumn(char[,] gameField, Monster monster, int rowDirection)
        {
            int[] directions = { -1, 1 };
            const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;
            const int FIELD_ROWS = InGameConstants.ROWS_COUNT;
            const int FIELD_COLUMNS = InGameConstants.COLUMN_COUNT;

            foreach (int colDir in directions)
            {
                int targetRow = monster.FieldRow + rowDirection;
                int targetColumn = monster.FieldColumn + colDir;

                if ((targetColumn >= 0 && targetColumn < FIELD_COLUMNS) 
                    && (targetRow >= 0 && targetRow < FIELD_ROWS))
                {
                    if (gameField[targetRow, targetColumn] == FIELD_SYMBOL)
                    {
                        MoveMonster(gameField, monster, targetRow, targetColumn);
                        return;
                    }
                }
            }
        }

        private void TryMoveDiagonally(char[,] gameField, Monster monster)
        {
            int[] directions = { -1, 1 };
            const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;
            const int FIELD_ROWS = InGameConstants.ROWS_COUNT;
            const int FIELD_COLUMNS = InGameConstants.COLUMN_COUNT;

            foreach (int rowDir in directions)
            {
                int targetRow = monster.FieldRow + rowDir;

                if (targetRow >= 0 && targetRow < FIELD_ROWS)
                {
                    if (gameField[targetRow, monster.FieldColumn] == FIELD_SYMBOL)
                    {
                        MoveMonster(gameField, monster, targetRow, monster.FieldColumn);
                        return;
                    }
                }
            }

            foreach (int colDir in directions)
            {
                int targetColumn = monster.FieldColumn + colDir;

                if (targetColumn >= 0 && targetColumn < FIELD_COLUMNS)
                {
                    if (gameField[monster.FieldRow, targetColumn] == FIELD_SYMBOL)
                    {
                        MoveMonster(gameField, monster, monster.FieldRow, targetColumn);
                        return;
                    }
                }
            }
        }

    }
}
