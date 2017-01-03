using System;
using System.ComponentModel;
using Microsoft.Reporting.WinForms;
using SteelWire.AppCode.CustomException;
using SteelWire.Business.Database;
using SteelWire.Business.DbOperator;
using System.Collections.Generic;
using System.Windows;
using SteelWire.AppCode.Data;
using SteelWire.Language;

namespace SteelWire.Windows
{
    /// <summary>
    /// ReportCutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReportCutWindow
    {
        private static ReportCutWindow _currentWindow;

        public static ReportCutWindow CurrentWindow => _currentWindow;

        public ReportCutWindow()
        {
            InitializeComponent();

            GlobalData.Language.ValueChangedHandler += RefreshReport;
            GlobalData.Wireline.UnitSystem.ValueChangedHandler += RefreshReport;

            _currentWindow = this;
        }

        private void WindowRendered(object sender, EventArgs e)
        {
            this.TxtCurrentWireNo.Text = GlobalData.Wireline.Number;
        }

        private void RefreshReport(object sender, EventArgs e)
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
                WirelineInfo lineInfo = WirelineOperator.GetWireline(wireNo);
                if (lineInfo != null)
                {
                    WireropeWorkload wireropeWorkload = CriticalOperator.GetLastWireropeWorkloadConfig(GlobalData.SearchUserId, lineInfo);
                    if (wireropeWorkload != null)
                    {
                        List<ReportParameter> reportParameters = new List<ReportParameter>
                        {
                            new ReportParameter("ReportParamTitle", $"{lineInfo.Struct}  D{wireropeWorkload.Diameter:F1}  {lineInfo.StrongLevel}  {lineInfo.TwistDirection}  {lineInfo.OrderLength:F3}{UnitData.Metre.Value}"),
                            new ReportParameter("ReportParamHeaderCutSequence",
                                LanguageManager.GetLocalResourceStringLeft("ReportHeaderCutSequence")),
                            new ReportParameter("ReportParamHeaderCutTime",
                                LanguageManager.GetLocalResourceStringLeft("ReportHeaderCutTime")),
                            new ReportParameter("ReportParamHeaderCutValue",
                                $"{LanguageManager.GetLocalResourceStringLeft("ReportHeaderCutValue")}{UnitData.TonKilometre.Value}"),
                            new ReportParameter("ReportParamHeaderCutLength",
                                $"{LanguageManager.GetLocalResourceStringLeft("ReportHeaderCutLength")}{UnitData.Metre.Value}"),
                            new ReportParameter("ReportParamFooterSummary",
                                LanguageManager.GetLocalResourceStringLeft("ReportFooterSummary"))
                        };
                        List<CutRecord> dataSource = CutOperator.GetAllHistory(GlobalData.SearchUserId, GlobalData.Wireline.Id);
                        ReportDataSource reportDataSource = new ReportDataSource
                        {
                            Name = "DataSetMain",
                            Value = dataSource
                        };
                        reportViewer.LocalReport.ReportEmbeddedResource = "SteelWire.Resources.Rdlc.ReportCut.rdlc";
                        reportViewer.LocalReport.SetParameters(reportParameters);
                        reportViewer.LocalReport.DataSources.Clear();
                        reportViewer.LocalReport.DataSources.Add(reportDataSource);
                        reportViewer.RefreshReport();
                        return;
                    }
                }
                List<ReportParameter> emptyReportParameters = new List<ReportParameter>
                {
                    new ReportParameter("ReportParamTitle", string.Empty),
                    new ReportParameter("ReportParamHeaderCutSequence",
                        LanguageManager.GetLocalResourceStringLeft("ReportHeaderCutSequence")),
                    new ReportParameter("ReportParamHeaderCutTime",
                        LanguageManager.GetLocalResourceStringLeft("ReportHeaderCutTime")),
                    new ReportParameter("ReportParamHeaderCutValue",
                        $"{LanguageManager.GetLocalResourceStringLeft("ReportHeaderCutValue")}({UnitData.TonKilometre.Value})"),
                    new ReportParameter("ReportParamHeaderCutLength",
                        $"{LanguageManager.GetLocalResourceStringLeft("ReportHeaderCutLength")}({UnitData.Metre.Value})"),
                    new ReportParameter("ReportParamFooterSummary",
                        LanguageManager.GetLocalResourceStringLeft("ReportFooterSummary"))
                };
                reportViewer.LocalReport.SetParameters(emptyReportParameters);
                reportViewer.LocalReport.DataSources.Clear();
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
