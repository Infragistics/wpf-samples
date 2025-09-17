using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace Infragistics.Framework
{
    public class SamplesManager : ObservableModel
    {
        public SamplesManager()
        {
            AppAssembly = Assembly.GetEntryAssembly();
            AppTitle = new AssemblyName(AppAssembly.FullName).Name;
            if (!AppTitle.StartsWith("Infragistics"))
            {
                AppTitle = "Infragistics " + AppTitle;
            }
        }

        protected Dictionary<string, SamplesGroup> SamplesDict = new Dictionary<string, SamplesGroup>();
        bool IsInitialize = false;
        public void Initialize()
        {
            if (this.IsInitialize) return;

            if (this.FilterTypes == null) return;
            if (this.FilterTypes.Count == 0) return;

            if (this.Assemblies == null || this.Assemblies.Count == 0)
            { 
                //var frmAssembly = Assembly.GetExecutingAssembly();

                System.Diagnostics.Debug.WriteLine("SB Initialize this.Assemblies to " + AppAssembly.GetName().Name + " app");
                this.Assemblies.Add(Assembly.GetEntryAssembly());
                //System.Diagnostics.Debug.WriteLine("SB Initialize appAssembly=" + appAssembly.GetName().Name);
                //System.Diagnostics.Debug.WriteLine("SB Initialize frmAssembly=" + frmAssembly.GetName().Name);

            }



            //var samples = new List<SamplesModel>();

            //var samples = new Dictionary<string, SamplesGroup>();

            //_SelectedSample = -1;
            //_SelectedType = null;
            this.SamplesDict.Clear(); 

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
                            var name = type.FullName.Split('.').ToList().Last();
                            name = name.Replace("_", " - ");
                            var group = type.Namespace.Split('.').ToList().Last();

                            foreach (var hide in this.FilterKeywords)
                            {
                                group = group.Replace(hide, "");
                            }

                            var sample = new SamplesModel();
                            sample.Name = name;
                            sample.Group = group;
                            sample.Type = type;
                            sample.Title = name;

                            if (!string.IsNullOrEmpty(group))
                                sample.Title = group + " - " + name;


                            if (!SamplesDict.ContainsKey(group))
                                 SamplesDict.Add(group, new SamplesGroup(group));

                            SamplesDict[group].Add(sample);

                            break;
                        }
                    }
                }
            }

            var newSamples = new List<SamplesModel>();
            var newGroups = new List<SamplesGroup>();
            foreach (var group in SamplesDict.Values)
            {
                newGroups.Add(group);
                newSamples.AddRange(group);
            }

            newSamples = newSamples.OrderBy(s => s.Type.FullName).ToList();
            for (var i = 0; i < newSamples.Count; i++)
            {
                newSamples[i].Index = i;
                //var s = newSamples[i];
                //System.Diagnostics.Debug.WriteLine("SB Initialize sample=" + s.Index + " " + s.Name);
            }

            this.Groups = newGroups;
            this.Samples = newSamples;

            IsInitialize = true;
        }

        public Assembly AppAssembly { get; private set; }
        public string AppTitle { get; private set; }

        //public Assembly AppAssembly
        //{
        //    get { return Assembly.GetEntryAssembly(); }
        //    //get { return Assemblies.Count > 0 ? Assemblies.Last() : null; }
        //}

        //public string AppTitle
        //{
        //    get
        //    {
        //        var asmb = AppAssembly;
        //        if (asmb == null) return "Samples Browser";
        //        var title = new AssemblyName(asmb.FullName).Name;

        //        if (!title.StartsWith("Infragistics"))
        //        {
        //            title = "Infragistics " + title;
        //        }
        //        return title;
        //    }
        //}

        private List<Assembly> _assemblies = new List<Assembly>();
        /// <summary> Gets or sets App </summary>
        public List<Assembly> Assemblies
        {
            get { return _assemblies; }
            set
            {
                if (_assemblies == value) return; _assemblies = value; OnPropertyChanged();
            }
        }

        private List<Type> _filterTypes = new List<Type> { typeof(UserControl) };
        /// <summary> Gets or sets base class(es) of samples </summary>
        public List<Type> FilterTypes
        {
            get { return _filterTypes; }
            set
            {
                if (_filterTypes == value) return; _filterTypes = value; OnPropertyChanged();
            }
        }

        private List<string> _filterKeywords = new List<string> { "Samples", "Views", "TestApp" };
        /// <summary> Gets or sets Keywords </summary>
        public List<string> FilterKeywords
        {
            get { return _filterKeywords; }
            set
            {
                if (_filterKeywords == value) return; _filterKeywords = value; OnPropertyChanged();
                Initialize();
            }
        }

        private int _SelectedSample = -1;
        /// <summary> Gets or sets SelectedSample </summary>
        public int SelectedSample
        {
            get { return _SelectedSample; }
            set
            {
                Initialize();
                if (_SelectedSample == value) return; _SelectedSample = value; OnPropertyChanged();
            }
        }

        private Type _SelectedType = null;
        /// <summary> Gets or sets SelectedType </summary>
        public Type SelectedType
        {
            get { return _SelectedType; }
            set
            {
                Initialize();
                if (_SelectedType == value) return; _SelectedType = value; OnPropertyChanged();
                UpdateSelection();
            }
        }

        /// <summary> Gets or sets Samples </summary>
        //public List<SamplesModel> Samples { get; set; }
        public List<SamplesGroup> Groups { get; set; }

        private List<SamplesModel> _Samples = new List<SamplesModel>();
        /// <summary> Gets or sets SelectedSample </summary>
        public List<SamplesModel> Samples
        {
            get { return _Samples; }
            set
            { 
                if (_Samples == value) return; _Samples = value; OnPropertyChanged();
            }
        }

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
                    //System.Diagnostics.Debug.WriteLine("SB UpdateSelection sample=" + sample.Index + " " + sample.Name + " MATCH");

                    this.SelectedSample = sample.Index;
                    break;
                }
                //else
                //{
                //    System.Diagnostics.Debug.WriteLine("SB UpdateSelection sample=" + sample.Index + " " + sample.Name);
                //}
            }
        }
    }

}

