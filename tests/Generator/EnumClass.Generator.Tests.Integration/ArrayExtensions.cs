namespace EnumClass.Generator.Tests.Integration;

public static class ArrayExtensions
{
    public static IEnumerable<(T Value, T[] Others)> GetDifferent<T>(this T[] array)
    {
        for (var i = 0; i < array.Length; i++)
        {
            var element = array[i];
            var rest = new List<T>(array.Length - 1);
            for (var j = 0; j < array.Length; j++)
            {
                if (j != i)
                {
                    rest.Add(array[j]);
                }
            }

            yield return ( element, rest.ToArray() );
        }
    }
}