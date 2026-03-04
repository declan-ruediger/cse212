public static class ArraySelector
{
    public static void Run()
    {
        var l1 = new[] { 1, 2, 3, 4, 5 };
        var l2 = new[] { 2, 4, 6, 8, 10};
        var select = new[] { 1, 1, 1, 2, 2, 1, 2, 2, 2, 1};
        var intResult = ListSelector(l1, l2, select);
        Console.WriteLine("<int[]>{" + string.Join(", ", intResult) + "}"); // <int[]>{1, 2, 3, 2, 4, 4, 6, 8, 10, 5}
    }

    private static int[] ListSelector(int[] list1, int[] list2, int[] select)
    {
        int list1index = 0;
        int list2index = 0;

        var result = Array.Empty<int>();

        foreach(var selector in select)
        {
            switch(selector)
            {
                case 1:
                    result = result.Append(list1[list1index]).ToArray();
                    ++list1index;
                    break;
                case 2:
                    result = result.Append(list2[list2index]).ToArray();
                    ++list2index;
                    break;
                default:
                    throw new KeyNotFoundException("Must be 1 or 2");
            }
        }

        return result;
    }
}