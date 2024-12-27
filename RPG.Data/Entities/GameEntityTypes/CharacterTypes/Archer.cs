namespace RPG.Data.Entities.GameEntityTypes.CharacterTypes
{
    using RPG.Data.Entities.GameEntityTypes;
    using RPG.Utilities.DataConstants.CharacterConstants;

    public class Archer : Character
    {

        public Archer(int strengthPoints, int agilityPoints, int intelligencePoints)
        {
            base.Strength = ArcherConstants.INITIAL_STRENGTH + strengthPoints;
            base.Agility = ArcherConstants.INITIAL_AGILITY + agilityPoints;
            base.Intelligence = ArcherConstants.INITIAL_INTELLIGENCE + intelligencePoints;
            base.Range = ArcherConstants.INITIAL_RANGE;
            base.CharacterSymbol = ArcherConstants.CHARACTER_SYMBOL;
        }

    }
}
