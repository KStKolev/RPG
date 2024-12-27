namespace RPG.Data.Entities.GameEntityTypes.CharacterTypes
{
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Utilities.DataConstants.CharacterConstants;

    public class Mage : Character
    {

        public Mage(int strengthPoints, int agilityPoints, int intelligencePoints)
        {
            base.Strength = MageConstants.INITIAL_STRENGTH + strengthPoints;
            base.Agility = MageConstants.INITIAL_AGILITY + agilityPoints;
            base.Intelligence = MageConstants.INITIAL_INTELLIGENCE + intelligencePoints;
            base.Range = MageConstants.INITIAL_RANGE;
            base.CharacterSymbol = MageConstants.CHARACTER_SYMBOL;
        }

    }
}
