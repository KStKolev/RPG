namespace RPG.Data.Entities.GameEntityTypes.CharacterTypes
{
    using RPG.Utilities.DataConstants.CharacterConstants;
    using RPG.Data.Entities.GameEntityTypes;

    public class Warrior : Character
    {

        public Warrior(int strengthPoints, int agilityPoints, int intelligencePoints)
        {
            base.Strength = WarriorConstants.INITIAL_STRENGTH + strengthPoints;
            base.Agility = WarriorConstants.INITIAL_AGILITY + agilityPoints;
            base.Intelligence = WarriorConstants.INITIAL_INTELLIGENCE + intelligencePoints;
            base.Range = WarriorConstants.INITIAL_RANGE;
            base.CharacterSymbol = WarriorConstants.CHARACTER_SYMBOL;
        }

    }
}
