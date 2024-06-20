using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Theming.Implementation;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;

namespace IGToolbar.Samples
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            InitializeComponent();
        }
    }

    public class BaseThemeTypeCollection : List<BaseThemeInfo>
    {

    }

    public class BaseThemeInfo
    {
        public string Description { get; set; }
        public BaseControlTheme Value { get; set; }
    }
}