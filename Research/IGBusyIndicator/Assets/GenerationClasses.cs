using IGBusyIndicator.Samples.DataProviders;
using Infragistics.Controls.Interactions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media;

namespace IGBusyIndicator.Assets
{
    partial class ExportedStyleTemplate
    {
        BusyAnimationPresenter _presenter;

        public ExportedStyleTemplate(ConfigurationViewModel model, BusyAnimationPresenter presenter)
        {
            this.Model = model;
            this._presenter = presenter;
            this.AnimationSize = (int)this._presenter.Height;
            this.IsAnimationSizeVisible = !double.IsNaN(this._presenter.Height) && !double.IsInfinity(this._presenter.Height);

            this.InitAnimationInfo();
        }

        #region Properties
        public ConfigurationViewModel Model { get; set; }

        public AnimationInfo Info { get; private set; }

        public int AnimationSize { get; set; }

        public bool IsAnimationSizeVisible { get; set; }

        public string PresenterType { get; set; }
        #endregion //Properties

        private void InitAnimationInfo()
        {
            this.Info = new AnimationInfo();

            var currentAnimationType = this.Model.CurrentAnimation.GetType();
            var presenterType = currentAnimationType.BaseType.GetGenericArguments().First();
            this.PresenterType = presenterType.Name;

            var animationPropertyInfos = currentAnimationType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                            .Where(x => x.PropertyType.Equals(typeof(System.Windows.Media.Brush)));

            var propertyInfos = presenterType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Where(x => x.PropertyType.Equals(typeof(System.Windows.Media.Brush)))
                                .Where(pi => animationPropertyInfos.Any(api => api.Name == pi.Name));

            this.Info.AnimationName = currentAnimationType.Name;

            foreach (var pInfo in propertyInfos)
            {
                this.Info.Properties.Add(
                    new BrushProperty()
                    {
                        Name = pInfo.Name,
                        Color = ((SolidColorBrush)pInfo.GetValue(this._presenter, null)).Color
                    }); 
            }


        }

        #region Embedded classes
        public class AnimationInfo
        {
            public AnimationInfo()
            {
                this.Properties = new List<BrushProperty>();
            }
            public string AnimationName { get; set; }
            public List<BrushProperty> Properties { get; set; }
        }

        public class BrushProperty
        {
            public string Name { get; set; }
            public Color Color { get; set; }
        }
        #endregion // Embedded classes

    }

    //partial class AnimationTemplate
    //{

    //    public AnimationTemplate(AnimationInfo info)
    //    {
    //        this.Info = info;
    //    }

    //    public AnimationInfo Info { get; set; }


    //    public class AnimationInfo
    //    {
    //        public string AnimationName { get; set; }
    //        public List<BrushProperty> Properties { get; set; }
    //    }

    //    public class BrushProperty
    //    {
    //        public string Name { get; set; }
    //        public Color Color { get; set; }
    //    }
    //}

}
