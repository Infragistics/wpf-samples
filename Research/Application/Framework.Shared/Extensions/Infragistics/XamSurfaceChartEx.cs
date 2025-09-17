using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Media3D;


namespace Infragistics.Controls.Charts
{
    public static class SurfaceChartEx
    {
        public static DependencyProperty GetAxisProp(string propName)
        {
            var propDescriptor = DependencyPropertyDescriptor.FromName(
            propName, typeof(SurfaceChartAxis), typeof(SurfaceChartAxis));

            var prop = propDescriptor.DependencyProperty;
            return prop;
        }
        public static DependencyProperty GetChartProp(string propName)
        {
            var propDescriptor = DependencyPropertyDescriptor.FromName(
            propName, typeof(XamScatterSurface3D), typeof(XamScatterSurface3D));

            var prop = propDescriptor.DependencyProperty;
            return prop;
        }

        public static T GetChartDefault<T>(string propName)
        {
            //var propDescriptor = DependencyPropertyDescriptor.FromName(
            //propName, typeof(XamScatterSurface3D), typeof(XamScatterSurface3D));

            var propDefault = GetChartProp(propName).DefaultMetadata.DefaultValue;
            return (T)Convert.ChangeType(propDefault, typeof(T));
        }
    }
}