namespace RPG.Core.Interfaces
{
    public interface IUserInputService
    {

        char GetUserInput(Func<char, bool> condition);

        int GetUserInput(Func<int, bool> condition);

    }
}
