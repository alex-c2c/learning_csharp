namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyLinkedList<int> ll = new MyLinkedList<int>();

            for (int i = 0; i < 10; i++)
            {
                ll.AddTail(i);
            }

            ll.PrintAllNodes();

            int counter = 0;
            foreach (int v in ll)
            {
                Console.Write($"counter: {counter} - {v}\n");
                counter++;
            }

            Console.WriteLine($"----------");

            counter = 0;
            foreach (int v in ll.GetEnumeratorReverse())
            {
                Console.Write($"counter: {counter} - {v}\n");
                counter++;
            }
        }
    }
}
