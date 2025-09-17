using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IGDataGrid.DataSources
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

                    Category c1 = new Category("Thriller");
                    c1.AddBook("0399152970", "S is for Silence", "Sue Grafton");
                    c1.AddBook("015601131X", "A Darkening Stain", "Robert Wilson");
                    c1.AddBook("0446696269", "Honeymoon", "James Patterson");
                    c1.AddBook("0140231706", "Berlin Noir", "Philip Kerr");
                    c1.AddBook("0553587889", "Prodigal Son", "Dean Koontz");
                    c1.AddBook("0743431685", "A Murder of Quality", "John le Carre");
                    c1.AddBook("0553804790", "The Husband", "Dean Koontz");
                    c1.AddBook("0743270363", "Consent to Kill: A Thriller", "Vince Flynn");
                    c1.AddBook("0142004308", "Dissolution", "C. J. Sansom");
                    c1.AddBook("0385510454", "The Broker", "John Grisham");
                    _categories.Add(c1);

                    Category c2 = new Category("Biography");
                    c2.AddBook("0743226712", "1776", "David McCullough");
                    c2.AddBook("1594200092", "Alexander Hamilton", "Ron Chernow");
                    c2.AddBook("0375705244", "Founding Brothers: The Revolutionary Generation", "Joseph J. Ellis");
                    c2.AddBook("0679764410", "American Sphinx: The Character of Thomas Jefferson", "Joseph J. Ellis");
                    c2.AddBook("0743223136", "John Adams", "David McCullough");
                    c2.AddBook("0684824906", "Team of Rivals", "Doris Kearns Goodwin");
                    c2.AddBook("0684807610", "Benjamin Franklin : An American Life", "Walter Isaacson");
                    c2.AddBook("0385507380", "Andrew Jackson: His Life and Times", "H.W.Brands");
                    _categories.Add(c2);

                    Category c3 = new Category("Computer");
                    c3.AddBook("0789728966", "Absolute Beginner's Guide to Computer Basics", "Michael Miller");
                    c3.AddBook("0764540742", "PCs for Dummies, Ninth Edition", "Dan Gookin");
                    c3.AddBook("0789730332", "How Computers Work, Seventh Edition", "Ron White and Timothy Edward Downs");
                    c3.AddBook("0070004846", "Structure and Interpretation of Computer Programs", "Harold Abelson, Gerald Sussman, and Julie Sussman");
                    c3.AddBook("1418843725", "Discovering Computers: Fundamentals, Second Edition", "Gary B. Shelly, Thomas J. Cashman, and Misty E. Vermaat");
                    c3.AddBook("0132433109", "Data and Computer Communications (8th Edition)", "William Stallings");
                    c3.AddBook("1568812698", "Fundamentals of Computer Graphics, Second Ed.", "Peter Shirley, Michael Ashikhmin, Michael Gleicher, and Stephen Marschner");
                    c3.AddBook("0764134175", "Dictionary of Computer and Internet Terms", "Douglas Downing, Michael Covington, and Melody Mauldin Covington");
                    c3.AddBook("0131432249", "Computers Brief (12th Edition)", "Larry Long and Nancy Long");
                    c3.AddBook("0131433512", "Computer Networks and Internets, Fourth Edition", "Douglas E Comer and Ralph E. Droms");
                    c3.AddBook("013034074X", "Computer Systems: A Programmer's Perspective ", "Randal E. Bryant and David R. O'Hallaron");
                    c3.AddBook("0763741493", "Computer Science Illuminated ", "Nell B. Dale");
                    c3.AddBook("0619213892", "Practical Computer Literacy", "June Jamrich Parsons and Dan Oja");
                    c3.AddBook("0321247442", "Introduction to Computer Security", "Matt Bishop");
                    c3.AddBook("0789734206", "Easy Computer Basics (Que's Easy Series)", "Michael Miller");
                    _categories.Add(c3);
                }

                return _categories;
            }
        }       
    }

    public class Category : INotifyPropertyChanged
    {
        string m_name;
        ObservableCollection<Book> m_books = new ObservableCollection<Book>();

        public Category(string name)
        {
            this.Name = name;
        }

        public void AddBook(string isbn, string title, string author)
        {
            this.Books.Add(new Book(isbn, title, author));
        }

        public string Name
        {
            get { return m_name; }
            set
            {
                if (m_name != value)
                {
                    m_name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public ObservableCollection<Book> Books
        {
            get { return m_books; }
            set
            {
                if (m_books != value)
                {
                    m_books = value;
                    NotifyPropertyChanged("Books");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }

    public class Book : INotifyPropertyChanged
    {
        string m_isbn;
        string m_title;
        string m_author;

        public Book(string isdn, string title, string author)
        {
            this.Isbn = isdn;
            this.Title = title;
            this.Author = author;

        }

        public string Isbn
        {
            get { return m_isbn; }
            set 
            {
                if (m_isbn != value)
                {
                    m_isbn = value;
                    NotifyPropertyChanged("Isbn");
                }
            }
        }

        public string Title
        {
            get { return m_title; }
            set
            {
                if (m_title != value)
                {
                    m_title = value;
                    NotifyPropertyChanged("Title");
                }
            }
        }

        public string Author
        {
            get { return m_author; }
            set
            {
                if (m_author != value)
                {
                    m_author = value;
                    NotifyPropertyChanged("Author");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
