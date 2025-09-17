using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Infragistics.Framework
{
    public class SamplesManager : ObservableModel
    {
        public SamplesManager()
        {
            Samples = new List<SamplesModel>();
            Assemblies = new List<Assembly>();
            FilterTypes = new List<Type>();
        }
        protected Dictionary<string, SamplesGroup> SamplesDict = new Dictionary<string, SamplesGroup>();

        public void Initialize()
        {
            if (this.Assemblies == null) return;
            if (this.Assemblies.Count == 0) return;
            if (this.FilterTypes == null) return;
            if (this.FilterTypes.Count == 0) return;

            //var samples = new List<SamplesModel>();

            //var samples = new Dictionary<string, SamplesGroup>();

            foreach (var asmb in this.Assemblies)
            {
                var asmbName = new AssemblyName(asmb.FullName).Name;
                var asmbTypes = asmb.GetTypesList();
                asmbTypes = asmbTypes.OrderBy(t => t.FullName).ToList();

                foreach (var type in asmbTypes)
                {
                    if (type.FullName.StartsWith("System") ||
                        type.FullName.StartsWith("Xamarin.Forms") ||
                        type.FullName.StartsWith("Infragistics.Controls") ||
                        type.FullName.StartsWith("Infragistics.Extension") ||
                        type.FullName.StartsWith("Infragistics.Framework.Annotations") ||
                        type.FullName.StartsWith("Infragistics.Framework.Browser") || 
                        //type.FullName.StartsWith("Infragistics.Framework") ||
                        type.FullName.StartsWith("Infragistics.XamarinForms"))
                        continue;

                    var baseType = type.GetTypeInfo().BaseType;
                    if (baseType == null)
                        continue;

                    foreach (var filter in this.FilterTypes)
                    {
                        if (baseType.FullName == filter.FullName)
                        {  
                            var group = type.Namespace.Split('.').ToList().Last();

                            var name = type.FullName.Split('.').ToList().Last();

                            var sample = new SamplesModel();
                            sample.Name = name; 
                            sample.Group = group;
                            sample.Title = group + " - " + name;
                            sample.Type = type;

                            if (!SamplesDict.ContainsKey(group))
                                 SamplesDict.Add(group, new SamplesGroup(group));

                            SamplesDict[group].Add(sample);  

                            break;
                        }
                    }
                }
            }

            this.Groups = new List<SamplesGroup>();
            foreach (var group in SamplesDict.Values)
            {
                this.Groups.Add(group);
                this.Samples.AddRange(group);
            }

            this.Samples = Samples.OrderBy(s => s.Type.FullName).ToList();
            for (var i = 0; i < this.Samples.Count; i++)
            {
                this.Samples[i].Index = i;
            }

            //Filters.AddRange(SamplesDict.Keys);

            //var groups = SamplesDict.Values.Select(g => g).ToList(); 
            //foreach (var group in groups)
            //{
            //    group.SortSamples();
            //}
            //this.Groups = groups.OrderBy(g => g.Name).ToList(); 
        }

        public Assembly AppAssembly
        {
            get { return Assemblies.Count > 0 ? Assemblies.Last() : null; }
        }
        public string AppTitle
        {
            get
            {
                var asmb = AppAssembly;
                if (asmb == null) return "Samples Browser";
                var title = new AssemblyName(asmb.FullName).Name;

                if (!title.StartsWith("Infragistics"))
                {
                    title = "Infragistics " + title;
                }
                return title;
            }
        }

        private List<Assembly> _assemblies;
        /// <summary> Gets or sets App </summary>
        public List<Assembly> Assemblies
        {
            get { return _assemblies; }
            set
            {
                if (_assemblies == value) return; _assemblies = value; //OnPropertyChanged("Assemblies");
                //Initialize();
            }
        }

        private List<Type> _filterTypes;
        /// <summary> Gets or sets base types </summary>
        public List<Type> FilterTypes
        {
            get { return _filterTypes; }
            set
            {
                if (_filterTypes == value) return; _filterTypes = value; //OnPropertyChanged("FilterTypes");
                //Initialize();
            }
        }
        private string[] _filterKeywords = new[] { "Samples", "Views" };
        /// <summary> Gets or sets Keywords </summary>
        public string[] FilterKeywords
        {
            get { return _filterKeywords; }
            set
            {
                if (_filterKeywords == value) return; _filterKeywords = value; //OnPropertyChanged("FilterKeywords");
                //Initialize();
            }
        }

        private int _SelectedSample = -1;
        /// <summary> Gets or sets SelectedSample </summary>
        public int SelectedSample
        {
            get { return _SelectedSample; }
            set
            {
                if (_SelectedSample == value) return; _SelectedSample = value; 
                OnPropertyChanged("SelectedSample");
            }
        }
        private Type _SelectedType;
        /// <summary> Gets or sets SelectedType </summary>
        public Type SelectedType
        {
            get { return _SelectedType; }
            set
            {
                if (_SelectedType == value) return; _SelectedType = value; //OnPropertyChanged("SelectedType");
                UpdateSelection();
            }
        }
        /// <summary> Gets or sets Samples </summary>
        public List<SamplesModel> Samples { get; set; }

        public List<SamplesGroup> Groups { get; set; } 

        protected List<string> Filters = new List<string>();

        public void Filter(string groupName)
        {
            if (Filters.Contains(groupName))
                Filters.Remove(groupName);
            else
                Filters.Add(groupName);

            var groups = new List<SamplesGroup>();

            //foreach (var samples in SamplesDict.Values)
            //{ 
            //    var group = new SamplesGroup { Name = samples.Name };

            //    if (Filters.Contains(group.Name))
            //    {
            //        group.AddRange(samples);
            //        group.SortSamples();
            //        groups.Add(group);
            //    }
            //}

            this.Groups = groups.OrderBy(g => g.Name).ToList();
        }

        public void UpdateSelection()
        {
            foreach (var sample in this.Samples)
            {
                if (sample.Type == this.SelectedType)
                {
                    this.SelectedSample = sample.Index;
                    break;
                }
            }
        }
    }

}

