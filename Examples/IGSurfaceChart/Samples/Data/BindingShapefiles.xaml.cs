﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq; 
using IGSurfaceChart.Samples.Models; 
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework; 
using System.ComponentModel; 

namespace IGSurfaceChart.Samples.Data
{ 
    public partial class BindingShapefiles : SampleContainer 
    { 
        public BindingShapefiles()
        {
            InitializeComponent(); 

            this.SynchronizeScale(this.SurfaceChart); 
        }
    }
 }