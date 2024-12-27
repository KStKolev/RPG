namespace RPG.Core.Interfaces.ScreenServices.CharacterSelectServices
{
    using RPG.Data.Entities.GameEntityTypes;

    public interface ICreateCharacterService
    {
        (int, int, int) BuffPoints();

        Character CreateCharacter(int raceOption, int strengthPoints, int agilityPoints, int intelligencePoints);
    }
}
