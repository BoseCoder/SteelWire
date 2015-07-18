using System;
using System.ComponentModel;
using Microsoft.Reporting.WinForms;
using SteelWire.AppCode.CustomException;
using SteelWire.Business.Database;
using SteelWire.Business.DbOperator;
using System.Collections.Generic;
using System.Windows;

namespace SteelWire.Windows
{
    /// <summary>
    /// ReportCutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportCutWindow
    {
        private static ReportCutWindow _currentWindow;

        public static ReportCutWindow CurrentWindow
        {
            get { return _currentWindow; }
        }

        public ReportCutWindow()
        {
            InitializeComponent();

            _currentWindow = this;
        }

        private void RefreshReport()
        {
            string wireNo = this.TxtCurrentWireNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(wireNo))
            {
                throw new ErrorException("CurrentWireNoEmpty");
            }

            ReportViewer reportViewer = ReportFormsHost.Child as ReportViewer;
            if (reportViewer != null)
            {
                List<CumulationReset> dataSource = ResetOperator.GetResetHistoryForCut(wireNo);
                ReportDataSource reportDataSource = new ReportDataSource { Name = "DataSetMain", Value = dataSource };
                reportViewer.LocalReport.ReportEmbeddedResource = "SteelWire.Resources.Rdlc.ReportCut.rdlc";
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                reportViewer.RefreshReport();
            }
        }

        private void ReportSearch(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshReport();
            }
            catch (Exception ex)
            {
                BaseException.HandleException(ex);
            }
        }

        private void WindowOnClosing(object sender, CancelEventArgs e)
        {
            _currentWindow = null;
        }
    }
}
