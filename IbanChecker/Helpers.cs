namespace IbanChecker
{
  
    public static class Helpers
    {
        public static T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
