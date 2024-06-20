using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using IGFunnelChart.Model;
using IGFunnelChart.Resources;
using Infragistics.Samples.Framework;

namespace IGFunnelChart.Samples.Data
{
    public partial class BindingLiveData : SampleContainer
    {
        private int ImpressionsCeiling = 3000, ClicksCeiling = 2000, 
            FreeDownloadsCeiling = 1000, PurchaseCeiling = 500, RepeatPurchaseCeiling = 400;
        private int ImpressionsCurrent = 0, ClicksCurrent = 0,
            FreeDownloadsCurrent = 0, PurchaseCurrent = 0, RepeatPurchaseCurrent = 0;
        ObservableCollection<TestDataItem> _funneldata = null;

        BackgroundWorker _worker = null;

        #region Generate Initial Data
        private ObservableCollection<TestDataItem> GenerateData()
        {
            ObservableCollection<TestDataItem> funneldata = new ObservableCollection<TestDataItem>();

            funneldata.Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_Impressions,
                Value = ImpressionsCurrent
            });
            funneldata.Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_Clicks,
                Value = ClicksCurrent
            });
            funneldata.Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_FreeDownloads,
                Value = FreeDownloadsCurrent
            });
            funneldata.Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_Purchase,
                Value = PurchaseCurrent
            });
            funneldata.Add(new TestDataItem()
            {
                Label = FunnelChartStrings.XFC_FunnelChart_RepeatPurchase,
                Value = RepeatPurchaseCurrent
            });

            return funneldata;
        }
        #endregion

        public BindingLiveData()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
            this.Unloaded += OnSampleUnloaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            _funneldata = GenerateData();
            funnel.ItemsSource = _funneldata;

            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            _worker.RunWorkerAsync();
        }

        private void OnSampleUnloaded(object sender, RoutedEventArgs e)
        {
            _worker.CancelAsync();
        }

        #region Asynchronous Live Updates to the Data
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Boolean HitImpressionCeiling = false, HitClicksCeiling = false,
                HitFreeDownloadsCeiling = false, HitPurchaseCeiling = false,
                HitRepeatPurchaseCeiling = false;

            while (true)
            {
                if (_worker.CancellationPending)
                    break;

                Dispatcher.BeginInvoke((Action)(() =>
                {
                    ImpressionsCurrent += new Random().Next(7);
                    ClicksCurrent += new Random(ImpressionsCurrent).Next(5);
                    FreeDownloadsCurrent += new Random(ClicksCurrent).Next(3);
                    PurchaseCurrent += new Random(FreeDownloadsCurrent).Next(6);
                    RepeatPurchaseCurrent += new Random(PurchaseCurrent).Next(2);

                    if (ImpressionsCurrent > ImpressionsCeiling)
                    {
                        ImpressionsCurrent = ImpressionsCeiling;
                        HitImpressionCeiling = true;
                    }

                    if (ClicksCurrent > ClicksCeiling)
                    {
                        ClicksCurrent = ClicksCeiling;
                        HitClicksCeiling = true;
                    }

                    if (FreeDownloadsCurrent > FreeDownloadsCeiling)
                    {
                        FreeDownloadsCurrent = FreeDownloadsCeiling;
                        HitFreeDownloadsCeiling = true;
                    }

                    if (PurchaseCurrent > PurchaseCeiling)
                    {
                        PurchaseCurrent = PurchaseCeiling;
                        HitPurchaseCeiling = true;
                    }

                    if (RepeatPurchaseCurrent > RepeatPurchaseCeiling)
                    {
                        RepeatPurchaseCurrent = RepeatPurchaseCeiling;
                        HitRepeatPurchaseCeiling = true;
                    }

                    foreach (TestDataItem testitem in _funneldata)
                    {
                        if (testitem.Label.Equals(FunnelChartStrings.XFC_FunnelChart_Impressions))
                            testitem.Value = ImpressionsCurrent;
                        else if (testitem.Label.Equals(FunnelChartStrings.XFC_FunnelChart_Clicks))
                            testitem.Value = ClicksCurrent;
                        else if (testitem.Label.Equals(FunnelChartStrings.XFC_FunnelChart_FreeDownloads))
                            testitem.Value = FreeDownloadsCurrent;
                        else if (testitem.Label.Equals(FunnelChartStrings.XFC_FunnelChart_Purchase))
                            testitem.Value = PurchaseCurrent;
                        else if (testitem.Label.Equals(FunnelChartStrings.XFC_FunnelChart_RepeatPurchase))
                            testitem.Value = RepeatPurchaseCurrent;
                    }

                    if (HitImpressionCeiling && HitClicksCeiling && HitFreeDownloadsCeiling &&
                        HitPurchaseCeiling && HitRepeatPurchaseCeiling)
                        return;
                }));

                Thread.Sleep(1500);
            }
        }
        #endregion
    }
}
