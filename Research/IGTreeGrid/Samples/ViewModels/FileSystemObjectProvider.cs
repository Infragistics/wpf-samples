using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IGTreeGrid.Samples.ViewModels
{
    public class FileSystemObjectProvider : ObservableModel
    {

        public FileSystemObjectProvider()
        {
            GenerateDataSource();
        }

        private ObservableCollection<FileSystemNode> _fileSystemObjects = null;
        public ObservableCollection<FileSystemNode> FileSystemObjects
        {
            get
            {
                return this._fileSystemObjects;
            }
            internal set
            {
                if (this._fileSystemObjects != value)
                {
                    this._fileSystemObjects = value;
                    this.OnPropertyChanged("FileSystemObjects");
                }
            }
        }

        void GenerateDataSource()
        {
            List<FileSystemNode> listRoot = new List<FileSystemNode>();

            listRoot.Add(
                new FileSystemNode(0, "Boot", true, 0, new DateTime(2011, 6, 1))
            );

            listRoot.Add(
                new FileSystemNode(1, "Documents", true, 0, new DateTime(2012, 1, 1),
                    new FileSystemNode[]
                    {
                        new FileSystemNode(11, "MonthReport.xlsx", false, 104211, new DateTime(2012, 9, 1)),
                        new FileSystemNode(12, "YearReport.xlsx", false, 883756, new DateTime(2009, 4, 1)),
                    }
                )
            );

            listRoot.Add(
                new FileSystemNode(2, "Program Files", true, 0, new DateTime(2013, 2, 1))
            );

            listRoot.Add(
                new FileSystemNode(3, "Program Files (x86)", true, 0, new DateTime(2010, 10, 1))
            );

            listRoot.Add(
                new FileSystemNode(4, "ProgramData", true, 0, new DateTime(2009, 1, 1))
            );

            listRoot.Add(
                new FileSystemNode(5, "Users", true, 0, new DateTime(2011, 8, 1),
                    new FileSystemNode[]
                    {
                        new FileSystemNode(51, "admin", true, 0, new DateTime(2009, 2, 1)),
                        new FileSystemNode(52, "ben", true, 0, new DateTime(2011, 6, 13)),
                        new FileSystemNode(53, "default", true, 0, new DateTime(2012, 1, 22)),
                    }
                )
            );

            listRoot.Add(
                new FileSystemNode(6, "Windows", true, 0, new DateTime(2009, 4, 1),
                    new FileSystemNode[]
                    {
                        new FileSystemNode(61, "Fonts", true, 0, new DateTime(2013, 2, 1),
                            new FileSystemNode[]
                            {
                                new FileSystemNode(611, "Arial.ttf", false, 48233, new DateTime(2013, 2, 1)),
                                new FileSystemNode(612, "Calibri.ttf", false, 118298, new DateTime(2013, 2, 1)),
                                new FileSystemNode(613, "Consolas.ttf", false, 34452, new DateTime(2013, 2, 1)),
                                new FileSystemNode(614, "Courier.ttf", false, 48233, new DateTime(2013, 2, 1)),
                                new FileSystemNode(615, "Tahoma.ttf", false, 96244, new DateTime(2013, 2, 1)),
                                new FileSystemNode(616, "Terminal.ttf", false, 19545, new DateTime(2013, 2, 1)),
                                new FileSystemNode(617, "Times New Roman.ttf", false, 66425, new DateTime(2013, 2, 1)),
                                new FileSystemNode(618, "Verdana.ttf", false, 82345, new DateTime(2013, 2, 1)),
                            }
                        ),
                        new FileSystemNode(62, "Help", true, 0, new DateTime(2010, 10, 1)),
                        new FileSystemNode(63, "Media", true, 0, new DateTime(2009, 1, 1)),
                        new FileSystemNode(64, "System", true, 0, new DateTime(2011, 8, 1)),
                        new FileSystemNode(65, "System32", true, 0, new DateTime(2009, 2, 1)),
                        new FileSystemNode(66, "Temp", true, 0, new DateTime(2012, 9, 7)),
                    }
                )
            );

            this.FileSystemObjects = new ObservableCollection<FileSystemNode>(listRoot);
        }
    }

    public class FileSystemNode
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int FileSize { get; set; }
        public bool IsFolder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        private List<FileSystemNode> children;

        public FileSystemNode()
        {
            this.children = new List<FileSystemNode>();
        }

        public FileSystemNode(int id, string name, bool isFolder, int size, DateTime dateModified, FileSystemNode[] ch = null)
        {
            this.ID = id;
            this.Name = name;
            this.IsFolder = isFolder;
            this.FileSize = size;
            this.DateModified = dateModified;
            if (ch == null) this.children = new List<FileSystemNode>();
            else this.children = ch.ToList();
        }

        public List<FileSystemNode> Children
        {
            get { return this.children; }
        }
    }
}
