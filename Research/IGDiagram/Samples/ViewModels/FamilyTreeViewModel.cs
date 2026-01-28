using IGDiagram.Resources;
using Infragistics.Samples.Shared.DataProviders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Infragistics.Samples.Shared.Models;

namespace IGDiagram.ViewModels
{
    public class FamilyTreeViewModel : ObservableModel
    {
        ObservableCollection<Person> _familyTree;
        public ObservableCollection<Person> FamilyTree
        {
            get { return _familyTree; }
            set
            {
                _familyTree = value;
                OnPropertyChanged("FamilyTree");
            }
        }

        public FamilyTreeViewModel()
        {
            var me = new Person(1, DiagramStrings.FamilyTree_Me, DiagramStrings.FamilyTree_John, "GUY04.jpg");
            var dad = new Person(2, DiagramStrings.FamiliyTree_Father, DiagramStrings.FamilyTree_James, "GUY20.jpg") { Children = new List<int>() { 1 } };
            var mom = new Person(3, DiagramStrings.FamiliyTree_Mother, DiagramStrings.FamilyTree_Jane, "GIRL15.jpg") { Children = new List<int>() { 1 } };
            var grandpa = new Person(4, DiagramStrings.FamiliyTree_Grandfather, DiagramStrings.FamilyTree_Steve, "GUY19.jpg") { Children = new List<int>() { 2 } };
            var grandma = new Person(5, DiagramStrings.FamiliyTree_Grandfather, DiagramStrings.FamilyTree_William, "GUY05.jpg") { Children = new List<int>() { 3 } };

            var wife = new Person(6, DiagramStrings.FamiliyTree_Wife, DiagramStrings.FamilyTree_Christine, "GIRL28.jpg") { Children = new List<int>() { 1 } };
            var herdad = new Person(7, DiagramStrings.FamiliyTree_Father, DiagramStrings.FamilyTree_Charles, "GUY01.jpg") { Children = new List<int>() { 6 } };
            var hermom = new Person(8, DiagramStrings.FamiliyTree_Mother, DiagramStrings.FamilyTree_Maria, "GIRL29.jpg") { Children = new List<int>() { 6 } };
            var hergrandpa = new Person(9, DiagramStrings.FamiliyTree_Grandfather, DiagramStrings.FamilyTree_Bob, "GUY09.jpg") { Children = new List<int>() { 7 } };
            var hergrandma = new Person(10, DiagramStrings.FamiliyTree_Grandmother, DiagramStrings.FamilyTree_Eve, "GIRL22.jpg") { Children = new List<int>() { 7 } };

            FamilyTree = new ObservableCollection<Person>();
            FamilyTree.Add(me);
            FamilyTree.Add(dad);
            FamilyTree.Add(mom);
            FamilyTree.Add(grandpa);
            FamilyTree.Add(grandma);
            FamilyTree.Add(wife);
            FamilyTree.Add(herdad);
            FamilyTree.Add(hermom);
            FamilyTree.Add(hergrandpa);
            FamilyTree.Add(hergrandma);
        }
    }

    public class Person : ObservableModel
    {
        public Person()
        {
        }

        public Person(int id, string relation, string name, string path)
        {
            Id = id;
            Relation = relation;
            Name = name;
            ImagePath = ImageFilePathProvider.GetImageLocalPath() + "people/" + path;
        }

        int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        string _relation;
        public string Relation
        {
            get { return _relation; }
            set
            {
                _relation = value;
                OnPropertyChanged("Relation");
            }
        }

        string _path;
        public string ImagePath
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("ImagePath");
            }
        }

        IList _children;
        public IList Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged("Children");
            }
        }
    }
}
