using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IGDataGrid.DataSources
{
    public class PlainObjects
    {
        private bool UpdateThreadShouldRun = false;
        private bool UpdateThreadStopped = false;
        private List<PlainBook> _books = new List<PlainBook>();
        private Random rnd = new Random();

        public PlainObjects()
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.StartsWith("ja"))
            {
                _books.Add(new PlainBook("0789728966", "Absolute Beginner's Guide to Computer Basics", "Michael Miller", GetRandomPrice()));
                _books.Add(new PlainBook("0764540742", "PCs for Dummies, Ninth Edition", "Dan Gookin", GetRandomPrice()));
                _books.Add(new PlainBook("0789730332", "How Computers Work, Seventh Edition", "Ron White and Timothy Edward Downs", GetRandomPrice()));
                _books.Add(new PlainBook("0070004846", "Structure and Interpretation of Computer Programs", "Harold Abelson, Gerald Sussman, and Julie Sussman", GetRandomPrice()));
                _books.Add(new PlainBook("1418843725", "Discovering Computers: Fundamentals, Second Edition", "Gary B. Shelly, Thomas J. Cashman, and Misty E. Vermaat", GetRandomPrice()));
                _books.Add(new PlainBook("0132433109", "Data and Computer Communications (8th Edition)", "William Stallings", GetRandomPrice()));
                _books.Add(new PlainBook("1568812698", "Fundamentals of Computer Graphics, Second Ed.", "Peter Shirley, Michael Ashikhmin, Michael Gleicher, and Stephen Marschner", GetRandomPrice()));
                _books.Add(new PlainBook("0764134175", "Dictionary of Computer and Internet Terms", "Douglas Downing, Michael Covington, and Melody Mauldin Covington", GetRandomPrice()));
                _books.Add(new PlainBook("0131432249", "Computers Brief (12th Edition)", "Larry Long and Nancy Long", GetRandomPrice()));
                _books.Add(new PlainBook("0131433512", "Computer Networks and Internets, Fourth Edition", "Douglas E Comer and Ralph E. Droms", GetRandomPrice()));
                _books.Add(new PlainBook("013034074X", "Computer Systems: A Programmer's Perspective ", "Randal E. Bryant and David R. O'Hallaron", GetRandomPrice()));
                _books.Add(new PlainBook("0763741493", "Computer Science Illuminated ", "Nell B. Dale", GetRandomPrice()));
                _books.Add(new PlainBook("0619213892", "Practical Computer Literacy", "June Jamrich Parsons and Dan Oja", GetRandomPrice()));
                _books.Add(new PlainBook("0321247442", "Introduction to Computer Security", "Matt Bishop", GetRandomPrice()));
                _books.Add(new PlainBook("0789734206", "Easy Computer Basics (Que's Easy Series)", "Michael Miller", GetRandomPrice()));
            }
            else
            {
                _books.Add(new PlainBook("0789728966", "Absolute Beginner's Guide to Computer Basics", "Michael Miller", GetRandomPrice()));
                _books.Add(new PlainBook("0764540742", "PCs for Dummies, Ninth Edition", "Dan Gookin", GetRandomPrice()));
                _books.Add(new PlainBook("0789730332", "How Computers Work, Seventh Edition", "Ron White and Timothy Edward Downs", GetRandomPrice()));
                _books.Add(new PlainBook("0070004846", "Structure and Interpretation of Computer Programs", "Harold Abelson, Gerald Sussman, and Julie Sussman", GetRandomPrice()));
                _books.Add(new PlainBook("1418843725", "Discovering Computers: Fundamentals, Second Edition", "Gary B. Shelly, Thomas J. Cashman, and Misty E. Vermaat", GetRandomPrice()));
                _books.Add(new PlainBook("0132433109", "Data and Computer Communications (8th Edition)", "William Stallings", GetRandomPrice()));
                _books.Add(new PlainBook("1568812698", "Fundamentals of Computer Graphics, Second Ed.", "Peter Shirley, Michael Ashikhmin, Michael Gleicher, and Stephen Marschner", GetRandomPrice()));
                _books.Add(new PlainBook("0764134175", "Dictionary of Computer and Internet Terms", "Douglas Downing, Michael Covington, and Melody Mauldin Covington", GetRandomPrice()));
                _books.Add(new PlainBook("0131432249", "Computers Brief (12th Edition)", "Larry Long and Nancy Long", GetRandomPrice()));
                _books.Add(new PlainBook("0131433512", "Computer Networks and Internets, Fourth Edition", "Douglas E Comer and Ralph E. Droms", GetRandomPrice()));
                _books.Add(new PlainBook("013034074X", "Computer Systems: A Programmer's Perspective ", "Randal E. Bryant and David R. O'Hallaron", GetRandomPrice()));
                _books.Add(new PlainBook("0763741493", "Computer Science Illuminated ", "Nell B. Dale", GetRandomPrice()));
                _books.Add(new PlainBook("0619213892", "Practical Computer Literacy", "June Jamrich Parsons and Dan Oja", GetRandomPrice()));
                _books.Add(new PlainBook("0321247442", "Introduction to Computer Security", "Matt Bishop", GetRandomPrice()));
                _books.Add(new PlainBook("0789734206", "Easy Computer Basics (Que's Easy Series)", "Michael Miller", GetRandomPrice()));
            }
        }

        private double GetRandomPrice()
        {
            return this.rnd.NextDouble() * 100;
        }

        // thread used to change the prices of the books
        public void UpdatingThread()
        {
            UpdateThreadStopped = false;
            UpdateThreadShouldRun = true;

            while (UpdateThreadShouldRun)
            {
                for (int i=0; i<_books.Count; i++)
                {
                    _books[i].Price = GetRandomPrice();
                }
                Thread.Sleep(50);
            }

            UpdateThreadStopped = true;
        }

        // used to start the updating thread
        public void StartUpdateThread()
        {
            new Thread(new ThreadStart(UpdatingThread)).Start();
        }

        // used to stop the updating thread
        public void StopUpdateThread()
        {
            UpdateThreadShouldRun = false;

            while (UpdateThreadStopped)
            {
                Thread.Sleep(100);
            }
        }

        public List<PlainBook> Books
        {
            get { return this._books; }
        }
    }

    public class PlainBook
    {
        public PlainBook(string isbn, string title, string author, double price)
        {
            ISBN = isbn;
            Title = title;
            Author = author;
            Price = price;
        }

        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
    }
}
