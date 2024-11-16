public static class StringExt
{
    public static int GetPotatoHash(this string str)
    {
        int hash = 0;
        var chars = str.ToCharArray();
        int index = 1;
        foreach (var letter in chars)
        {
            int charValue = letter - '0';
            hash += charValue * index;
            index += 1;
        }

        return hash;
    }
}