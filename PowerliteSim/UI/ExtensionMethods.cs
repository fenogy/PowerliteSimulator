namespace System.Controls
{
    public static class ExtensionMethods
    {
        public static bool Contains(this string[] stringList, string item)
        {
            foreach (string s in stringList)
                if (item.Equals(s))
                    return true;
            return false;
        }
    }
}