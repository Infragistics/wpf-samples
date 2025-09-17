using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGDataTree.DataSources
{
    public class LibraryBusinessLogic
    {
        ObservableCollection<Category> _categories = null;

        public ObservableCollection<Category> Categories
        {
            get
            {
                if (this._categories == null)
                {
                    _categories = new ObservableCollection<Category>();

                    var c1 = new Category("Thriller");
                    c1.Branch = "Fiction";
                    c1.Description = "The thriller fiction genre, sometimes called suspense fiction, is a genre of literature that typically entails fast-paced plots, numerous action scenes, and limited character development.";
                    c1.AddBook("0399152970", "S is for Silence", "Sue Grafton", true);
                    c1.AddBook("015601131X", "A Darkening Stain", "Robert Wilson", true);
                    c1.AddBook("0446696269", "Honeymoon", "James Patterson", true);
                    c1.AddBook("0140231706", "Berlin Noir", "Philip Kerr", true);
                    c1.AddBook("0553587889", "Prodigal Son", "Dean Koontz", true);
                    c1.AddBook("0743431685", "A Murder of Quality", "John le Carre", false);
                    c1.AddBook("0553804790", "The Husband", "Dean Koontz", true);
                    c1.AddBook("0743270363", "Consent to Kill: A Thriller", "Vince Flynn", true);
                    c1.AddBook("0142004308", "Dissolution", "C. J. Sansom", true);
                    c1.AddBook("0385510454", "The Broker", "John Grisham", false);
                    _categories.Add(c1);

                    var c2 = new Category("Biography");
                    c2.Branch = "Nonfiction";
                    c2.Description = "A biography genre presents a subject's life story, highlighting various aspects of his or her life, including intimate details of experience, and may include an analysis of the subject's personality.";
                    c2.AddBook("0743226712", "1776", "David McCullough", false);
                    c2.AddBook("1594200092", "Alexander Hamilton", "Ron Chernow");
                    c2.AddBook("0375705244", "Founding Brothers: The Revolutionary Generation", "Joseph J. Ellis", true);
                    c2.AddBook("0679764410", "American Sphinx: The Character of Thomas Jefferson", "Joseph J. Ellis", true);
                    c2.AddBook("0743223136", "John Adams", "David McCullough", true);
                    c2.AddBook("0684824906", "Team of Rivals", "Doris Kearns Goodwin", true);
                    c2.AddBook("0684807610", "Benjamin Franklin : An American Life", "Walter Isaacson", false);
                    c2.AddBook("0385507380", "Andrew Jackson: His Life and Times", "H.W.Brands", true);
                    _categories.Add(c2);

                    var c3 = new Category("Computer");
                    c3.Branch = "Business/Technical";
                    c3.Description = "The computer category includes technical guides to improve your computer literacy as well as to help you dive more deeply in computer hardware, programming, operating systems and application development.";
                    c3.AddBook("0789728966", "Absolute Beginner's Guide to Computer Basics", "Michael Miller", false);
                    c3.AddBook("0764540742", "PCs for Dummies, Ninth Edition", "Dan Gookin", true);
                    c3.AddBook("0789730332", "How Computers Work, Seventh Edition", "Ron White and Timothy Edward Downs", true);
                    c3.AddBook("0070004846", "Structure and Interpretation of Computer Programs", "Harold Abelson, Gerald Sussman, and Julie Sussman", true);
                    c3.AddBook("1418843725", "Discovering Computers: Fundamentals, Second Edition", "Gary B. Shelly, Thomas J. Cashman, and Misty E. Vermaat", false);
                    c3.AddBook("0132433109", "Data and Computer Communications (8th Edition)", "William Stallings", true);
                    c3.AddBook("1568812698", "Fundamentals of Computer Graphics, Second Ed.", "Peter Shirley, Michael Ashikhmin, Michael Gleicher, and Stephen Marschner", true);
                    c3.AddBook("0764134175", "Dictionary of Computer and Internet Terms", "Douglas Downing, Michael Covington, and Melody Mauldin Covington", false);
                    c3.AddBook("0131432249", "Computers Brief (12th Edition)", "Larry Long and Nancy Long", true);
                    c3.AddBook("0131433512", "Computer Networks and Internets, Fourth Edition", "Douglas E Comer and Ralph E. Droms", true);
                    c3.AddBook("013034074X", "Computer Systems: A Programmer's Perspective ", "Randal E. Bryant and David R. O'Hallaron", true);
                    c3.AddBook("0763741493", "Computer Science Illuminated ", "Nell B. Dale", false);
                    c3.AddBook("0619213892", "Practical Computer Literacy", "June Jamrich Parsons and Dan Oja", true);
                    c3.AddBook("0321247442", "Introduction to Computer Security", "Matt Bishop", true);
                    c3.AddBook("0789734206", "Easy Computer Basics (Que's Easy Series)", "Michael Miller", false);
                    _categories.Add(c3);

                    var c4 = new Category("Travel");
                    c4.Branch = "Travel";
                    c4.Description = "The genre of travel literature includes outdoor literature, exploration literature, adventure literature, mountain literature, nature writing, and the guide book, as well as accounts of visits to foreign countries.";
                    c4.AddBook("9781101615164", "Travels with Charley in Search of America", "John Steinbeck, Jay Parini", true);
                    c4.AddBook("9781921382581", "My Greek Island Home", "Claire Lloyd", false);
                    c4.AddBook("9781579128548", "CUBA", "Francois Missen", true);
                    c4.AddBook("9780881928679", "THE NORTHWEST NATURE GUIDE", "James Luther Davis", true);
                    _categories.Add(c4);

                    var c5 = new Category("Art");
                    c5.Branch = "Nonfiction";
                    c5.Description = "";
                    c5.AddBook("9781579129460", "LEONARDO'S NOTEBOOKS", "LEONARDO DA VINCI", true);
                    c5.AddBook("9781579128869", "THE LOUVRE: ALL THE PAINTINGS", "Erich Lessing", true);
                    _categories.Add(c5);

                    var c6 = new Category("Business");
                    c6.Branch = "Business/Technical";
                    c6.Description = "This category includes books about advertising, economics, finance and marketing.";
                    c6.AddBook("9781565119345", "100 WAYS TO MOTIVATE OTHERS", "Steve Chandler and Scott Richardson", true);
                    c6.AddBook("9780761114055", "1001 WAYS TO TAKE INITIATIVE AT WORK", "Bob Nelson", false);
                    c6.AddBook("9780670920471", "Start It Up", "Luke Johnson", true);
                    c6.AddBook("9780452010444", "Essentials of Business Ethics", "Jay M. Shafritz and Peter Madsen", false);
                    _categories.Add(c6);
                }

                return _categories;
            }
        }
    }

    public class Category : ObservableModel
    {
        private string _name;
        private string _branch;
        private string _description;

        ObservableCollection<Book> _books = new ObservableCollection<Book>();

        public Category(string name)
        {
            this._name = name;
        }

        public void AddBook(string isbn, string title, string author)
        {
            this.Books.Add(new Book(isbn, title, author));
        }

        public void AddBook(string isbn, string title, string author, bool isAvailable)
        {
            this.Books.Add(new Book(isbn, title, author, isAvailable));
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public string Branch
        {
            get { return _branch; }
            set
            {
                if (_branch != value)
                {
                    _branch = value;
                    NotifyPropertyChanged("Branch");
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }


        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                if (_books != value)
                {
                    _books = value;
                    NotifyPropertyChanged("Books");
                }
            }
        }
    }

    public class Book : ObservableModel
    {
        private string _isbn;
        private string _title;
        private string _author;
        private bool _isAvailable;

        public Book(string isdn, string title, string author)
        {
            this._isbn = isdn;
            this._title = title;
            this._author = author;
        }

        public Book(string isdn, string title, string author, bool isAvailable)
        {
            this._isbn = isdn;
            this._title = title;
            this._author = author;
            this._isAvailable = isAvailable;
        }

        public string Isbn
        {
            get { return _isbn; }
            set
            {
                if (_isbn != value)
                {
                    _isbn = value;
                    NotifyPropertyChanged("Isbn");
                }
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        public string Author
        {
            get { return _author; }
            set
            {
                if (_author != value)
                {
                    _author = value;
                    NotifyPropertyChanged("Author");
                }
            }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set
            {
                if (_isAvailable != value)
                {
                    _isAvailable = value;
                    NotifyPropertyChanged("IsAvailable");
                }
            }
        }
    }

    public class ObservableModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}