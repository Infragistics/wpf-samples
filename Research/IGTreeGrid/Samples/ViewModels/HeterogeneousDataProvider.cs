using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IGTreeGrid.Samples.ViewModels
{
    public class HeterogeneousDataProvider : ObservableModel
    {
        public HeterogeneousDataProvider()
        {
            GenerateDataSource();
        }

        private ObservableCollection<SystemNodeBase> _systemNodeBaseObjects = null;
        public ObservableCollection<SystemNodeBase> SystemNodeBaseObjects
        {
            get
            {
                return this._systemNodeBaseObjects;
            }
            internal set
            {
                if (this._systemNodeBaseObjects != value)
                {
                    this._systemNodeBaseObjects = value;
                    this.OnPropertyChanged("SystemNodeBaseObjects");
                }
            }
        }

        void GenerateDataSource()
        {
            List<SystemNodeBase> listRoot = new List<SystemNodeBase>();

            listRoot.Add(
                new FolderNode(0, "Boot", new DateTime(2011, 6, 1))
            );

            listRoot.Add(
                new FolderNode(1, "Documents", new DateTime(2012, 1, 1),
                    new FileNode[]
                    {
                        new FileNode(11, "MonthReport.xlsx", 104211, new DateTime(2012, 9, 1)),
                        new FileNode(12, "YearReport.xlsx", 883756, new DateTime(2009, 4, 1)),
                    }
                )
            );

            listRoot.Add(
                new FolderNode(2, "Program Files", new DateTime(2013, 2, 1))
            );

            listRoot.Add(
                new FolderNode(3, "Program Files (x86)", new DateTime(2010, 10, 1))
            );

            listRoot.Add(
                new FolderNode(4, "ProgramData", new DateTime(2009, 1, 1))
            );

            listRoot.Add(
                new FolderNode(5, "Users", new DateTime(2011, 8, 1),
                    new FolderNode[]
                    {
                        new FolderNode(51, "admin", new DateTime(2009, 2, 1)),
                        new FolderNode(52, "ben", new DateTime(2011, 6, 13)),
                        new FolderNode(53, "default", new DateTime(2012, 1, 22)),
                    }
                )
            );

            listRoot.Add(
                new FolderNode(6, "Windows", new DateTime(2009, 4, 1),
                    new FolderNode[]
                    {
                        new FolderNode(61, "Fonts", new DateTime(2013, 2, 1),
                            new FileNode[]
                            {
                                new FileNode(611, "Arial.ttf", 48233, new DateTime(2013, 2, 1)),
                                new FileNode(612, "Calibri.ttf", 118298, new DateTime(2013, 2, 1)),
                                new FileNode(613, "Consolas.ttf", 34452, new DateTime(2013, 2, 1)),
                                new FileNode(614, "Courier.ttf", 48233, new DateTime(2013, 2, 1)),
                                new FileNode(615, "Tahoma.ttf", 96244, new DateTime(2013, 2, 1)),
                                new FileNode(616, "Terminal.ttf", 19545, new DateTime(2013, 2, 1)),
                                new FileNode(617, "Times New Roman.ttf", 66425, new DateTime(2013, 2, 1)),
                                new FileNode(618, "Verdana.ttf", 82345, new DateTime(2013, 2, 1)),
                            }
                        ),
                        new FolderNode(62, "Help", new DateTime(2010, 10, 1)),
                        new FolderNode(63, "Media", new DateTime(2009, 1, 1)),
                        new FolderNode(64, "System", new DateTime(2011, 8, 1)),
                        new FolderNode(65, "System32", new DateTime(2009, 2, 1)),
                        new FolderNode(66, "Temp", new DateTime(2012, 9, 7)),
                    }
                )
            );

            this.SystemNodeBaseObjects = new ObservableCollection<SystemNodeBase>(listRoot);
        }
    }

    public class SystemNodeBase
    {
        public int ID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }

    public class FileNode : SystemNodeBase
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }

        public FileNode(int id, string name, int size, DateTime mod)
        {
            this.ID = id;
            this.FileName = name;
            this.FileSize = size;
            this.DateModified = mod;
        }
    }

    public class FolderNode : SystemNodeBase
    {
        public string FolderName { get; set; }
        private List<object> _children;

        public FolderNode(int id, string name, DateTime mod, SystemNodeBase[] ch = null)
        {
            this.ID = id;
            this.FolderName = name;
            this.DateModified = mod;
            if (ch == null) this._children = new List<object>();
            else this._children = ch.ToList<object>();
        }

        public List<object> Children
        {
            get { return this._children; }
        }
    }
}
