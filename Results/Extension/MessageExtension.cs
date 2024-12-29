using System.Text;

namespace Results.Extension;

public static class MessageExtension
{
    public static bool Containing(this List<string> source, string toCheck)
    {
        return source?.Any(m => m.Contains(toCheck)) ?? false;
    }

    public static string Flatten(this List<string> source)
    {
        if (source == null || !source.Any()) return string.Empty;

        var result = source.Aggregate(new StringBuilder(),
            (sb, str) => sb.AppendLine(String.Join(",", str)),
            sb => sb.ToString());

        return result;
    }
}
