namespace UniExam.BusinessLogic.Extensions
{
    public static class IsInRangeExtension
    {
        public static bool IsInRange(this int integer, int maxValue)
        {
            return integer > 0 & integer <= maxValue;
        }
    }
}
