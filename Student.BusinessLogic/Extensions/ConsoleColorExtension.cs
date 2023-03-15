namespace UniExam.BusinessLogic.Utilities
{
    public static class ConsoleColorExtension
    {
        public static void WriteLine(this ConsoleColor foregroundColor, string text)
        {
            var originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(text);
            Console.ForegroundColor = originalForegroundColor;
        }
    }
}
