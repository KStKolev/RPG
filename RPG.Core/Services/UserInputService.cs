namespace RPG.Core.Services
{
    using RPG.Core.Interfaces;

    public class UserInputService : IUserInputService
    {

        public char GetUserInput(Func<char, bool> condition)
        {
            char userKeyInput;

            do
            {
                userKeyInput = Console.ReadKey(intercept: true).KeyChar;
            } while (condition(userKeyInput));

            return userKeyInput;
        }

        public int GetUserInput(Func<int, bool> condition)
        {
            char userKeyInput;
            int number;
            bool isNumber;

            do
            {
                userKeyInput = Console.ReadKey(intercept: true).KeyChar;
                isNumber = int.TryParse(userKeyInput.ToString(), out number);
            } while (!isNumber || condition(number));

            return number;
        }

    }
}
