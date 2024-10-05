namespace SportsProcessor.Extensions;
public static class ListExtensions
{

    /// <summary>
    /// Serves same functionality to JS array's Pop() function
    /// Returns the first element found in the list, and removes it from the list.
    /// If the list will be empty - returns default(T)
    /// </summary>
    /// <typeparam name="T">Type of the objects stored within the list</typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T? Pop<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            return default(T);
        }

        T firstElement = list[0];

        list.RemoveAt(0);

        return firstElement;
    }
}
