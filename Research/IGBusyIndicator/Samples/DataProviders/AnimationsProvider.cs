using Infragistics.Controls.Interactions;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;

namespace IGBusyIndicator.Samples.DataProviders
{
    public class AnimationsProvider : ObservableModel
    {
        private BusyAnimation[] _animations;
        public BusyAnimation[] Animations
        {
            get
            {
                return this._animations;
            }
            set
            {
                if (this._animations != value)
                {
                    this._animations = value;
                    this.OnPropertyChanged("Animations");
                }
            }
        }
       
        public AnimationsProvider()
        {
            if (_animations == null)
            {
                _animations = GetBusyIndicatorAnimations();
            }
        }

        private static BusyAnimation[] GetBusyIndicatorAnimations()
        {
            var animations = new List<BusyAnimation>();

            try
            {
                foreach (var field in typeof(BusyAnimations).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
                {
                    if (typeof(BusyAnimation).IsAssignableFrom(field.FieldType))
                        animations.Add((BusyAnimation)field.GetValue(null));
                }
            }
            catch (Exception)
            {
            }

            return animations.ToArray();
        } 
    }
}
