using System.Collections.Concurrent;

namespace Homework_12_2
{
    internal class Program
    {
       private static ConcurrentDictionary<string, int> _books = new ConcurrentDictionary<string, int>();
        static void Main(string[] args)
        {
            Thread thread = new Thread(() => Calculate());
            thread.Start();
           while (true)
            {
                Menu();
            }                     
        }

        static void Menu ()
        {
            Console.WriteLine("1 - добавить книгу; 2 - вывести список непрочитанного; 3 - выйти");
            string number = Console.ReadLine();
            switch (number)
            {
                case "3":
                    Environment.Exit(0);
                    break;
                case "1":
                    AddBook();
                    break;
                case "2":
                    PrintBook();
                    break;
            }
        }

        static void AddBook ()
        {
            Console.WriteLine("Введите название книги:");
            string nameBook = Console.ReadLine();
            if (_books.ContainsKey(nameBook))
            {
                return;
            }
            else
            {
                _books.TryAdd(nameBook, 0);
            }
        }

        static void PrintBook()
        {
            foreach(var book in _books)
            {
                Console.WriteLine($"Название книги + {book.Key} + {book.Value}");
            }
        }

        static void Calculate()
        {
            while(true)
            {
                foreach (var book in _books)
                {
                    if (book.Value == 100)
                    {
                        _books.TryRemove(book.Key, out _);
                        continue;
                    }
                    _books.AddOrUpdate(book.Key, 0, (key, oldValue) => oldValue + 1);
                }
                Thread.Sleep(1000);
            }
           
        }
        
    }
}