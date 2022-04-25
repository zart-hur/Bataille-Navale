namespace BatailleNavale.View;

internal class ContentStyle
{
    public static void TitleText(string? content, ConsoleColor color = ConsoleColor.Yellow)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(" **************************************");
        Console.WriteLine("         {0} ", content);
        Console.WriteLine(" **************************************");
    }

    public static void LineText(string? content, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine("\n{0}\n", content);
        Console.ResetColor();
    }

    public static void UnordoredList(string? content)
    {
        Console.WriteLine(" # {0}", content);
    }
}
