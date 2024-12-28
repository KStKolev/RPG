namespace RPG.Core.Services.GameServices.InGameServices
{
    using RPG.Core.Interfaces;
    using RPG.Core.Interfaces.ScreenServices.InGameServices;
    using RPG.Data;
    using RPG.Data.Entities;
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Utilities.DataConstants.EntityConstants;
    using RPG.Utilities.DataConstants.ScreenConstants;

    public class CharacterTurnService : ICharacterTurnService
    {

        private readonly RPGDbContext dbContext;
        private readonly IUserInputService userInputService;

        public CharacterTurnService(RPGDbContext dbContext, IUserInputService userInputService)
        {
            this.dbContext = dbContext;
            this.userInputService = userInputService;
        }

        public void CharacterTurn(char[,] gameField, Character character, 
            List<Monster> monsterCollection, GameSession gameSession)
        {
            PrintActionOptions();

            const char ATTACK_ACTION = InGameConstants.ATTACK_ACTION;
            const char MOVE_ACTION = InGameConstants.MOVE_ACTION;

            Func<char, bool> actionCondition = input => (input != ATTACK_ACTION) && (input != MOVE_ACTION);
            char userInput = userInputService.GetUserInput(actionCondition);
            Console.WriteLine(userInput);

            if (userInput == ATTACK_ACTION)
            {
                AttackAction(gameField, monsterCollection, character, gameSession);
            }
            else if (userInput == MOVE_ACTION)
            {
                MoveAction(gameField, character);
            }
        }

        private void PrintActionOptions()
        {
            Console.WriteLine($"{InGameConstants.ACTIONS_OPTION}");
            Console.WriteLine($"{InGameConstants.ATTACK_OPTION}");
            Console.WriteLine($"{InGameConstants.MOVE_OPTION}");
        }

        private void AttackAction(char[,] gameField, 
            List<Monster> monsterCollection, Character character, GameSession game)
        {
            Dictionary<int, Monster> monsterInRangeCollection = new Dictionary<int, Monster>();
            int monsterAroundCount = TargetMonstersInRange(gameField, monsterCollection, character, monsterInRangeCollection);

            if (monsterAroundCount == 0)
            {
                Console.WriteLine("No available targets in your range");
                Console.ReadKey(intercept: true);
            }
            else
            {
                AttackMonster(gameField, monsterCollection, character, game, monsterInRangeCollection);
            }
        }

        private void MoveAction(char[,] gameField, Character character)
        {
            var validInputs = new Dictionary<char, (int rowDirection, int colDirection)>
            {
                { 'W', (-1, 0) }, { 'S', (1, 0) }, { 'D', (0, 1) }, { 'A', (0, -1) },
                { 'E', (-1, 1) }, { 'X', (1, 1) }, { 'Q', (-1, -1) }, { 'Z', (1, -1) }
            };

            bool isMoved = false;
            while (isMoved == false)
            {
                Func<char, bool> moveCondition = input => !validInputs.ContainsKey(input);
                char userInput = userInputService.GetUserInput(moveCondition);

                var (rowDirection, colDirection) = validInputs[userInput];
                isMoved = IsCharacterMoved(gameField, character, rowDirection, colDirection);
            }
        }

        private bool IsCharacterMoved(char[,] gameField, 
            Character character, int rowDirection, int columnDirection)
        {
            const int FIELD_ROWS = InGameConstants.ROWS_COUNT;
            const int FIELD_COLUMNS = InGameConstants.COLUMN_COUNT;
            const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;

            if (IsSpaceAroundCharacterOccupied(gameField, character))
            {
                return true;
            }

            rowDirection += character.FieldRow;
            columnDirection += character.FieldColumn;

            bool isOutOfField = (rowDirection < 0 || rowDirection >= FIELD_ROWS) ||
                (columnDirection < 0 || columnDirection >= FIELD_COLUMNS);

            if (!isOutOfField && gameField[rowDirection, columnDirection] == FIELD_SYMBOL)
            {
                gameField[rowDirection, columnDirection] = character.CharacterSymbol;
                gameField[character.FieldRow, character.FieldColumn] = FIELD_SYMBOL;

                character.FieldRow = rowDirection;
                character.FieldColumn = columnDirection;
                return true;
            }
            return false;
        }

        private bool IsSpaceAroundCharacterOccupied(char[,] gameField, Character character)
        {
            const int FIELD_ROWS = InGameConstants.ROWS_COUNT;
            const int FIELD_COLUMNS = InGameConstants.COLUMN_COUNT;
            const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;

            for (int rowOffset = -1; rowOffset <= 1; rowOffset++)
            {
                for (int colOffset = -1; colOffset <= 1; colOffset++)
                {
                    int checkRow = character.FieldRow + rowOffset;
                    int checkCol = character.FieldColumn + colOffset;

                    bool isInsideField = (checkRow >= 0 && checkRow < FIELD_ROWS)
                        && (checkCol >= 0 && checkCol < FIELD_COLUMNS);

                    if (isInsideField)
                    {
                        if (gameField[checkRow, checkCol] == FIELD_SYMBOL)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private int TargetMonstersInRange(char[,] gameField, List<Monster> monsterList, Character character,
           Dictionary<int, Monster> monsterInRangeCollection)
        {
            int monsterAroundCount = 0;
            const int FIELD_ROWS = InGameConstants.ROWS_COUNT;
            const int FIELD_COLUMNS = InGameConstants.COLUMN_COUNT;
            const char MONSTER_SYMBOL = MonsterConstants.CHARACTER_SYMBOL;

            Func<int, bool> isInRows = row => row < FIELD_ROWS && row >= 0;
            Func<int, bool> isInColumns = column => column < FIELD_COLUMNS && column >= 0;
            HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>();

            for (int range = character.Range; range > 0; range--)
            {
                for (int rowIndex = -range; rowIndex <= range; rowIndex++)
                {
                    for (int columnIndex = -range; columnIndex <= range; columnIndex++)
                    {
                        int targetRow = character.FieldRow + rowIndex;
                        int targetCol = character.FieldColumn + columnIndex;

                        if (isInRows(targetRow) && isInColumns(targetCol)
                            && gameField[targetRow, targetCol] == MONSTER_SYMBOL
                            && visitedPositions.Add((targetRow, targetCol)))
                        {
                            Monster monster = monsterList.First(m => m.FieldRow == targetRow && m.FieldColumn == targetCol);
                            monsterInRangeCollection.Add(monsterAroundCount, monster);
                            Console.WriteLine($"{monsterAroundCount++}) Target with remaining blood: {monster.Health}");
                        }
                    }
                }
            }
            return monsterAroundCount;
        }

        private void AttackMonster(char[,] gameField, List<Monster> monsterCollection,
          Character character, GameSession game, Dictionary<int, Monster> monsterInRangeCollection)
        {
            Console.WriteLine(InGameConstants.ATTACK_OPTION_DIALOG);

            Func<int, bool> attackMonsterCondition = input => !monsterInRangeCollection.ContainsKey(input);
            int result = userInputService.GetUserInput(attackMonsterCondition);
            Console.WriteLine(result);

            Monster attackedMonster = monsterCollection.First(m => m == monsterInRangeCollection[result]);
            attackedMonster.Health -= character.Damage;

            if (attackedMonster.Health <= 0)
            {
                const char FIELD_SYMBOL = InGameConstants.FIELD_SYMBOL;

                monsterCollection.Remove(attackedMonster);
                gameField[attackedMonster.FieldRow, attackedMonster.FieldColumn] = FIELD_SYMBOL;
                game.MonsterKillCount++;
                dbContext.SaveChanges();
            }
        }

    }
}
