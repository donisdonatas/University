namespace UniExam.BusinessLogic.Utilities
{
    public static class Converter
    {
        public static List<int> StringToListInt(string intString)
        {
            IEnumerable<string> strArr = intString.Split(",").Select(s => s.Trim());
            List<int> Numbers = new List<int>();
            foreach (string str in strArr)
            {
                bool result = int.TryParse(str, out int Num);
                if (result) Numbers.Add(Num);
            }
            return Numbers;
        }
    }
}
