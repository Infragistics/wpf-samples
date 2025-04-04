﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;

namespace IGToolbar.Samples
{
    public partial class CategoryChartIntegration : SampleContainer
    {
        public CategoryChartIntegration()
        {
            InitializeComponent();
            Infragistics.Controls.Grids.GridToolbarSupport.Init();
        }
    }
}