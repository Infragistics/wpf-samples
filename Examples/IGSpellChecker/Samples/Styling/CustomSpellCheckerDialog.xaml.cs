﻿using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGSpellChecker.Samples.Styling
{
    public partial class CustomSpellCheckerDialog : SampleContainer
    {
        public CustomSpellCheckerDialog()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            customSpellChecker.DictionaryUri = DictionaryFileProvider.GetDictionaryLocalUri("us-english-v2-whole.dict");
        }

        private void btnCheckSpelling_Click(object sender, RoutedEventArgs e)
        {
            customSpellChecker.SpellCheck();
        }
    }
}
