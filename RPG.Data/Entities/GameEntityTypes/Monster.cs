namespace RPG.Data.Entities.GameEntityTypes
{
    using RPG.Utilities.DataConstants.CharacterConstants;

    public class Monster : GameEntity
    {

        public Monster()
        {
            base.Strength = SetRandomInitialStats();
            base.Agility = SetRandomInitialStats();
            base.Intelligence = SetRandomInitialStats();
            base.Range = MonsterConstants.INITIAL_RANGE;
            base.CharacterSymbol = MonsterConstants.CHARACTER_SYMBOL;
        }

        public override void Setup()
        {
            base.Setup();
        }

        private int SetRandomInitialStats()
        {
            int leastInitialPoints = MonsterConstants.LEAST_INITIAL_POINTS;
            int mostInitialPoints = MonsterConstants.MOST_INITIAL_POINTS;

            Random random = new Random();
            int initialPoints = random.Next(leastInitialPoints, mostInitialPoints);
            return initialPoints;
        }

    }
}
