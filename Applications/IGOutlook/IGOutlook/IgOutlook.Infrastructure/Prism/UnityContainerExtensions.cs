using Microsoft.Practices.Unity;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IgOutlook.Infrastructure.Prism
{
    public static class UnityContainerExtensions
    {
        //public static void RegisterTypeForNavigation<T>(this IUnityContainer container)
        //{
        //    container.RegisterType(typeof(Object), typeof(T), typeof(T).FullName);
        //}

        public static void RegisterTypeForNavigation<T>(this IUnityContainer container, string name)
        {
            container.RegisterType(typeof(Object), typeof(T), name);
        }

        //public static void RegisterTypeForNavigation(this IUnityContainer container, Type type)
        //{
        //    container.RegisterType(typeof(Object), type, type.FullName);
        //}
    }
}
