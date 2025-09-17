using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Themes;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Infragistics.Samples.Framework
{ 
    public enum ThemeType
    {
        RoyalLight,
        RoyalDark, 
        MetroDark, 
        MetroLight, 
        Office2010,
        Office2013,
        IG,  
        Default,   
    }

    public static class ThemeLoader
    { 
        static ThemeLoader()
        {
            Themes = new Dictionary<ThemeType, ThemeBase>();
            Themes.Add(ThemeType.RoyalLight, new RoyalLightTheme());
            Themes.Add(ThemeType.RoyalDark,  new RoyalDarkTheme());
            Themes.Add(ThemeType.MetroDark,  new MetroDarkTheme());
            Themes.Add(ThemeType.MetroLight, new MetroTheme());
            Themes.Add(ThemeType.Office2010, new Office2010BlueTheme());
            Themes.Add(ThemeType.Office2013, new Office2013Theme());
            Themes.Add(ThemeType.IG,         new IgTheme());
            Themes.Add(ThemeType.Default,    null);
        }

        public static Dictionary<ThemeType, ThemeBase> Themes { get; set; }

        /// <summary>
        /// Merges resources of a given theme on element's level using the ThemeManager
        /// </summary> 
        public static void SetTheme(FrameworkElement element, ThemeType themeType)
        { 
            if(themeType != ThemeType.Default)
            ThemeManager.SetTheme(element, Themes[themeType]); 
        }
        /// <summary>
        /// Merges resources of a given theme on application level using the ThemeManager
        /// </summary> 
        public static void SetTheme(ThemeType themeType)
        { 
            ThemeManager.ApplicationTheme = Themes[themeType]; 
        }
    }
}

namespace Infragistics.Samples.Shared.Models
{     
    /// <summary>
    /// Represents a list of all themes supported by IG controls
    /// </summary>
    public class ThemeList : List<ThemeInfo>
    {
        public ThemeList()
        {
            this.Add(new ThemeInfo ("RoyalLight", new RoyalLightTheme(), CommonStrings.XW_ThemeSupport_RoyalLight ));
            this.Add(new ThemeInfo ("RoyalDark",  new RoyalDarkTheme(), CommonStrings.XW_ThemeSupport_RoyalDark ));
            this.Add(new ThemeInfo ("MetroLight", new MetroTheme(), CommonStrings.XW_ThemeSupport_Metro ));
            this.Add(new ThemeInfo ("MetroDark",  new MetroDarkTheme(),  CommonStrings.XW_ThemeSupport_MetroDark ));
            this.Add(new ThemeInfo ("Office2010", new Office2010BlueTheme(), CommonStrings.XW_ThemeSupport_Office2010BlueTheme ));
            this.Add(new ThemeInfo ("Office2013", new Office2013Theme(),  CommonStrings.XW_ThemeSupport_Office2013Theme ));
            this.Add(new ThemeInfo ("IG", new IgTheme(), CommonStrings.XW_ThemeSupport_IGTheme ));
            this.Add(new ThemeInfo ("Default",  null, CommonStrings.XW_ThemeSupport_DefaultTheme ));
        }
    }
     
    /// <summary>
    /// Represents a model that holds basic information about a Theme (ResourceDictionary)
    /// </summary>
    public class ThemeInfo
    {
        /// <summary> Gets or sets the name of theme </summary>
        public string Name { get; set; }
         
        public ThemeBase Resources { get; set; }

        public bool IsDefaulTheme { get { return Resources == null; } }
        
        /// <summary> Gets or sets an identifier of theme </summary>
        public string ID { get; set; }

        public ThemeInfo(string id, ThemeBase theme, string displayName)
        {
            ID = id;
            Name = displayName;
            Resources = theme;
        } 

        /// <summary>
        /// Gets or sets the location of a resource dictionary with the theme 
        /// </summary>
        public Uri ThemeUri { get; set; }
        /// <summary>
        /// Gets whether or not this is default theme for a control
        /// <para>Returns true if ThemeUri is null, otherwise it returns false.</para>
        /// </summary>
        public bool IsDefaultTheme
        {
            get { return this.ThemeUri == null; }
        }

        public ThemeInfo()
        {
            this.ThemeUri = null;
        }

        //public Theme(string idx, string name, Uri uri)
        //{
        //    this.ThemeId = idx;
        //    this.ThemeName = name;
        //    this.ThemeUri = uri;
        //}

        public override string ToString()
        {
            return this.Name;
        }
     

    }
}