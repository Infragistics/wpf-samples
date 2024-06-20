using System;
using System.Collections.Generic;
using System.ComponentModel;
using Infragistics.Controls.Description;
using IGCategoryChart.Samples.Models;

namespace IGCategoryChart.Samples.ViewModels
{

    public class SampleViewModel
        : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private SalesData _salesData = null;
        public SalesData SalesData
        {
            get
            {
                if (_salesData == null)
                {
                    _salesData = new SalesData(); 
                }
                return _salesData;
            }
        }
        
        private ComponentRenderer _componentRenderer = null;
        public ComponentRenderer Renderer
        {
            get
            {
                if (this._componentRenderer == null)
                {
                    this._componentRenderer = new ComponentRenderer();
                    var context = this._componentRenderer.Context;
                    PropertyEditorPanelDescriptionModule.Register(context);
                    LegendDescriptionModule.Register(context);
                    CategoryChartDescriptionModule.Register(context);
                }
            return this._componentRenderer;
            }
        }
    }

}