﻿using System.Collections.Generic; 
using System.ComponentModel;
using System.Linq;
using Infragistics.Samples.Core;        // provides AssemblyProvider
using System.Diagnostics;
using System;

namespace Infragistics.SamplesBrowser.ViewModel
{
    public class TocItemViewModel : INotifyPropertyChanged
    {
        #region Data

        bool _isExpanded;
        bool _isSelected;
         
        /// <summary> Gets or sets Release Version </summary>
        public double ReleaseVersion { get; private set; }
        /// <summary> Gets or sets Status </summary>
        public string Status { get; private set; }

        #endregion // Data

        #region Initialization

        protected TocItemViewModel(string name, string xamlFilePath, 
            double releaseVersion, string status, IList<TocItemViewModel> children)
        {
            this.Name = name;
            this.AssemblyName = string.Empty;
            this.XamlFilePath = xamlFilePath.Trim();
            this.ReleaseVersion = releaseVersion;
            this.Status = status;

            if (children != null)
                this.Children = new List<TocItemViewModel>(children);
        }
        //MT: added new constructor with assemblyName
        protected TocItemViewModel(string name, string xamlFilePath, string assemblyName, 
            double releaseVersion, string status, IList<TocItemViewModel> children)
        {
            this.Name = name;
            this.AssemblyName = assemblyName;

            //MT: use assemblyName to set XamlFilePath
            if (this.AssemblyName != string.Empty)
                this.XamlFilePath = AssemblyProvider.GetAssemblyPack(this.AssemblyName) + "/" + xamlFilePath.Trim();
            else
                this.XamlFilePath = xamlFilePath.Trim();

            this.ReleaseVersion = releaseVersion;
            this.Status = status;

            if (children != null)
                this.Children = new List<TocItemViewModel>(children);
        }

        public void Initialize(TocItemViewModel parent, double currentVersion)
        {
            this.Parent = parent;
            
            var info = this.Name;
            if (this.HasParent && !string.IsNullOrEmpty(this.Parent.Name))
            {
                if (this.Parent.HasParent && !string.IsNullOrEmpty(this.Parent.Parent.Name))
                    info = this.Parent.Parent.Name + " - " + this.Name;
            }

            if (this.HasChildren) // control or category
            {
                foreach (TocItemViewModel child in this.Children)
                {
                    child.Initialize(this, currentVersion);
                }
                ReleaseVersion = this.Children.Max(c => c.ReleaseVersion);

                var newCount = this.Children.Count(c => c.Status == "NEW");
                var ctpCount = this.Children.Count(c => c.Status == "CTP");
                var updCount = this.Children.Count(c => c.Status == "UPD");
                var betaCount = this.Children.Count(c => c.Status == "BETA");

                if (newCount == this.Children.Count)
                { 
                    Status = "NEW"; 
                }
                else if (betaCount == this.Children.Count)  
                {
                    Status = "BETA";
                }
                else if (ctpCount == this.Children.Count)  
                {
                    Status = "CTP";
                }
                else if (updCount == this.Children.Count)  
                {
                    Status = "UPD";
                }
                else if (newCount > 0 || updCount > 0)
                {
                    Status = "UPD"; 
                }
                else if (ctpCount > 0)
                {
                    if (ReleaseVersion == currentVersion)
                        Status = "UPD";
                    else
                        Status = "OLD";
                }
                else 
                {
                    Status = "OLD";
                }
            }
            else // sample
            { 
                if (double.IsNaN(ReleaseVersion))
                { 
                    if (Status == "NEW" || Status == "UPD")
                    {
                        ReleaseVersion = currentVersion;
                    }
                    else
                    {
                        ReleaseVersion = 10.1;
                        Debug.WriteLine("WARNING sample is missing ReleaseVersion attribute: " + info);
                    } 
                }
                 
                if (string.IsNullOrEmpty(Status))
                {
                    if (ReleaseVersion == currentVersion)
                        Status = "NEW";
                    else
                        Status = "OLD";
                }
                else
                {
                    if (Status != "NEW" && Status != "CTP" && Status != "UPD" && Status != "OLD" && Status != "BETA")
                    {
                        Status = "ERR";
                        Debug.WriteLine("WARNING sample has unknown Status attribute: " + info);
                    } 
                } 
            }
        }

        #endregion // Initialization

        #region State Properties

        public List<TocItemViewModel> Children { get; protected set; }
        public bool HasChildren { get { return this.Children != null && 0 < this.Children.Count; } }

        public TocItemViewModel Parent { get; private set; }
        public bool HasParent { get { return this.Parent != null; } }
       
        public string Name { get; private set; }
        public string XamlFilePath { get; private set; }
        public string AssemblyName { get; private set; }
        public bool HasCodeFiles { get; set; }

        public string Description { get; set; }
        public List<string> CodeFiles { get; set; }

        #endregion // State Properties

        #region Presentation Properties

        #region IsNew
        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is new.
        /// </summary>
        public bool IsNew
        {
            get
            {
                return Status == "NEW";              
            }            
        }
        #endregion // IsNew


        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (this.IsExpanded && this.Parent != null)
                    this.Parent.IsExpanded = true;
            }
        }

        #endregion // IsExpanded

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");

                    if (_isSelected && this.Parent != null)
                        this.Parent.IsExpanded = true;
                }
            }
        }

        #endregion // IsSelected

        #region IsUpdated

        /// <summary>
        /// Returns true if this control/category is not new in this release, but contains 
        /// one or more new samples in this release.
        /// </summary>
        public bool IsUpdated
        {
            get
            {
                return Status == "UPD";
            }
        }

        #endregion // IsUpdated

        #region CTP

        /// <summary>
        /// Determines whether the control's release is CTP.
        /// </summary>
        public bool IsCtp
        {
            get
            {
                return Status == "CTP";
            }
        }

        #endregion // IsCtp

        #region BETA

        /// <summary>
        /// Determines whether the control's release is BETA.
        /// </summary>
        public bool IsBETA
        {
            get
            {
                return Status == "BETA";
            }
        }

        #endregion // BETA


        #endregion // Presentation Properties

        #region Virtual Methods

        public virtual bool MatchesSearchText(string searchText)
        {
            return false;
        }

        #endregion // Virtual Methods

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        public override string ToString()
        {
            return this.Name + " IsNew=" + this.IsNew + " IsCtp=" + this.IsCtp + " IsBETA=" + this.IsBETA + " Status=" + this.Status;
        }
    }
}
