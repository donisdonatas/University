namespace UniExam.BusinessLogic.Utilities
{
    public static class Messages
    {
        public static void GetIdErrorMessage()
        {
            ConsoleColor.Red.WriteLine("This ID is not valid.");
        }

        public static void GetNumberErrorMessage(int number)
        {
            ConsoleColor.Red.WriteLine($"This {number} is not valid.");
        }
    }
}
