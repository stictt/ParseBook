using ParseBook;

internal class Program
{
    private static void Main(string[] args)
    {
        string argument = args.FirstOrDefault();
        if (args.Length == 0)
        {
            Console.WriteLine("Введите путь файла");
            argument = Console.ReadLine();
        }

        Parser parser = new Parser(argument);
        Bookkeeper bookkeeper = new Bookkeeper(parser);
        bookkeeper.Execute();

        foreach (var item in bookkeeper)
        {
            Console.WriteLine(item);
        }
    }
}