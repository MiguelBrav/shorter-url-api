using System.Text;

namespace ShorterAPI.Helpers;

public static class CsvHelper
{
    public static byte[] ExportToCsv<T>(IEnumerable<T> data)
    {
        var props = typeof(T).GetProperties();
        var sb = new StringBuilder();

        sb.AppendLine(string.Join(",", props.Select(p => p.Name)));

        foreach (var item in data)
        {
            var values = props.Select(p =>
            {
                var val = p.GetValue(item, null)?.ToString() ?? "";
                if (val.Contains(",") || val.Contains("\""))
                {
                    val = $"\"{val.Replace("\"", "\"\"")}\"";
                }
                return val;
            });

            sb.AppendLine(string.Join(",", values));
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }
}