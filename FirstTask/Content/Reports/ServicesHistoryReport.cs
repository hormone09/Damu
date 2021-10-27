using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using Stimulsoft.Controls;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Dialogs;
using Stimulsoft.Report.Components;

namespace Reports
{
    public class Отчет : Stimulsoft.Report.StiReport
    {
        public Отчет()        {
            this.InitializeComponent();
        }

        #region StiReport Designer generated code - do not modify
        public string ServiceFilter;
        public string CompanyFilter;
        public string EmployeeFilter;
        public string DateBegin;
        public string DateEnd;
        public Stimulsoft.Report.Components.StiPage Page2;
        public Stimulsoft.Report.Components.StiPageHeaderBand PageHeaderBand1;
        public Stimulsoft.Report.Components.StiText Text1;
        public Stimulsoft.Report.Components.StiText Text2;
        public Stimulsoft.Report.Components.StiText Text3;
        public Stimulsoft.Report.Components.StiText Text4;
        public Stimulsoft.Report.Components.StiText Text5;
        public Stimulsoft.Report.Components.StiText Text6;
        public Stimulsoft.Report.Components.StiText Text7;
        public Stimulsoft.Report.Components.StiText Text8;
        public Stimulsoft.Report.Components.StiText Text9;
        public Stimulsoft.Report.Components.StiText Text20;
        public Stimulsoft.Report.Components.StiText Text21;
        public Stimulsoft.Report.Components.StiContainer Container1;
        public Stimulsoft.Report.Components.StiHeaderBand HeaderBand1;
        public Stimulsoft.Report.Components.StiText Text11;
        public Stimulsoft.Report.Components.StiText Text15;
        public Stimulsoft.Report.Components.StiText Text13;
        public Stimulsoft.Report.Components.StiText Text17;
        public Stimulsoft.Report.Components.StiDataBand DataBand1;
        public Stimulsoft.Report.Components.StiText Text10;
        public Stimulsoft.Report.Components.StiText Text12;
        public Stimulsoft.Report.Components.StiText Text16;
        public Stimulsoft.Report.Components.StiText Text14;
        public Stimulsoft.Report.Components.StiFooterBand FooterBand1;
        public Stimulsoft.Report.Components.StiText Text18;
        public Stimulsoft.Report.Components.StiText Text19;
        public Stimulsoft.Report.Components.StiWatermark Page2_Watermark;
        public Stimulsoft.Report.Print.StiPrinterSettings Отчет_PrinterSettings;
        public TestDataSource Test;
        
        public override void SaveState(System.String stateName)
        {
            base.SaveState(stateName);
            this.States.Push(stateName, this, "ServiceFilter", this.ServiceFilter);
            this.States.Push(stateName, this, "CompanyFilter", this.CompanyFilter);
            this.States.Push(stateName, this, "EmployeeFilter", this.EmployeeFilter);
            this.States.Push(stateName, this, "DateBegin", this.DateBegin);
            this.States.Push(stateName, this, "DateEnd", this.DateEnd);
        }
        
        public override void RestoreState(System.String stateName)
        {
            base.RestoreState(stateName);
            this.ServiceFilter = ((string)(this.States.Pop(stateName, this, "ServiceFilter")));
            this.CompanyFilter = ((string)(this.States.Pop(stateName, this, "CompanyFilter")));
            this.EmployeeFilter = ((string)(this.States.Pop(stateName, this, "EmployeeFilter")));
            this.DateBegin = ((string)(this.States.Pop(stateName, this, "DateBegin")));
            this.DateEnd = ((string)(this.States.Pop(stateName, this, "DateEnd")));
        }
        
        public void Text1__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Отчет оказанных услуг";
        }
        
        public void Text2__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Период:";
        }
        
        public void Text3__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Компания:";
        }
        
        public void Text4__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Услуга:";
        }
        
        public void Text5__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Сотрудник:";
        }
        
        public void Text6__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, ServiceFilter, true);
        }
        
        public void Text7__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, CompanyFilter, true);
        }
        
        public void Text8__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, EmployeeFilter, true);
        }
        
        public void Text9__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, DateBegin, true);
        }
        
        public void Text20__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "   по:    ";
        }
        
        public void Text21__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, DateEnd, true);
        }
        
        public void Text11__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Наименование услуги";
        }
        
        public void Text15__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Стоимость услуги";
        }
        
        public void Text13__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Название компнаии";
        }
        
        public void Text17__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Имя сотрудника";
        }
        
        public void Text10__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Test.Наименование_услуги, true);
        }
        
        public void Text12__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Test.Название_компании, true);
        }
        
        public void Text16__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Test.Имя_сотрудника, true);
        }
        
        public void Text14__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Test.Стоимость_услуги, true);
        }
        
        public void Text18__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = "Количество оказанных услуг по указанному фильтру: ";
        }
        
        public void Text19__GetValue(object sender, Stimulsoft.Report.Events.StiGetValueEventArgs e)
        {
            e.Value = ToString(sender, Test.TotalRows, true);
        }
        
        private void InitializeComponent()
        {
            this.Test = new TestDataSource();
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "ServiceFilter", "ServiceFilter", "", typeof(string), "", false, Stimulsoft.Report.Dictionary.StiVariableInitBy.Value, true));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "CompanyFilter", "CompanyFilter", "", typeof(string), "", false, Stimulsoft.Report.Dictionary.StiVariableInitBy.Value, false));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "EmployeeFilter", "EmployeeFilter", "", typeof(string), "", false, Stimulsoft.Report.Dictionary.StiVariableInitBy.Value, false));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "DateBegin", "DateBegin", "", typeof(string), "", false, Stimulsoft.Report.Dictionary.StiVariableInitBy.Value, true));
            this.Dictionary.Variables.Add(new Stimulsoft.Report.Dictionary.StiVariable("", "DateEnd", "DateEnd", "", typeof(string), "", false, Stimulsoft.Report.Dictionary.StiVariableInitBy.Value, true));
            this.NeedsCompiling = false;
            // 
            // Variables init
            // 
            this.ServiceFilter = "";
            this.CompanyFilter = "";
            this.EmployeeFilter = "";
            this.DateBegin = "";
            this.DateEnd = "";
            this.EngineVersion = Stimulsoft.Report.Engine.StiEngineVersion.EngineV1;
            this.ReferencedAssemblies = new System.String[] {
                    "System.Dll",
                    "System.Drawing.Dll",
                    "System.Windows.Forms.Dll",
                    "System.Data.Dll",
                    "System.Xml.Dll",
                    "Stimulsoft.Controls.Dll",
                    "Stimulsoft.Base.Dll",
                    "Stimulsoft.Report.Dll"};
            this.ReportAlias = "Отчет";
            // 
            // ReportChanged
            // 
            this.ReportChanged = new DateTime(2021, 10, 27, 15, 51, 31, 654);
            // 
            // ReportCreated
            // 
            this.ReportCreated = new DateTime(2021, 10, 19, 14, 1, 1, 0);
            this.ReportFile = "C:\\Users\\Domu\\Desktop\\ServicesHistoryReport.mrt";
            this.ReportGuid = "d375d86944764903ae61b1709d007426";
            this.ReportName = "Отчет";
            this.ReportUnit = Stimulsoft.Report.StiReportUnitType.Centimeters;
            this.ReportVersion = "2013.1.1600";
            this.ScriptLanguage = Stimulsoft.Report.StiReportLanguageType.CSharp;
            // 
            // Page2
            // 
            this.Page2 = new Stimulsoft.Report.Components.StiPage();
            this.Page2.Guid = "4776daea8ab747679ddac43920083467";
            this.Page2.Name = "Page2";
            this.Page2.PageHeight = 29.7;
            this.Page2.PageWidth = 21;
            this.Page2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 2, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Page2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // PageHeaderBand1
            // 
            this.PageHeaderBand1 = new Stimulsoft.Report.Components.StiPageHeaderBand();
            this.PageHeaderBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.4, 19, 4);
            this.PageHeaderBand1.Name = "PageHeaderBand1";
            this.PageHeaderBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.PageHeaderBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text1
            // 
            this.Text1 = new Stimulsoft.Report.Components.StiText();
            this.Text1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.2, 19, 0.6);
            this.Text1.HorAlignment = Stimulsoft.Base.Drawing.StiTextHorAlignment.Center;
            this.Text1.Name = "Text1";
            this.Text1.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text1__GetValue);
            this.Text1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.Text1.Guid = null;
            this.Text1.Indicator = null;
            this.Text1.Interaction = null;
            this.Text1.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text1.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text1.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text2
            // 
            this.Text2 = new Stimulsoft.Report.Components.StiText();
            this.Text2.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1, 1, 3, 0.6);
            this.Text2.Guid = "0315c4ab51404411ac3af78ef4e0ab2f";
            this.Text2.Name = "Text2";
            this.Text2.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text2__GetValue);
            this.Text2.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text2.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text2.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.Text2.Indicator = null;
            this.Text2.Interaction = null;
            this.Text2.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text2.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text2.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text3
            // 
            this.Text3 = new Stimulsoft.Report.Components.StiText();
            this.Text3.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1, 1.6, 3, 0.6);
            this.Text3.Guid = "73caff886f2b4f8e8d3461fe6cbcf6b9";
            this.Text3.Name = "Text3";
            this.Text3.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text3__GetValue);
            this.Text3.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text3.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text3.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.Text3.Indicator = null;
            this.Text3.Interaction = null;
            this.Text3.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text3.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text3.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text4
            // 
            this.Text4 = new Stimulsoft.Report.Components.StiText();
            this.Text4.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1, 2.2, 3, 0.6);
            this.Text4.Guid = "0332e1ef27de4900aaee490c7b9a3f88";
            this.Text4.Name = "Text4";
            this.Text4.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text4__GetValue);
            this.Text4.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text4.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text4.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.Text4.Indicator = null;
            this.Text4.Interaction = null;
            this.Text4.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text4.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text4.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text5
            // 
            this.Text5 = new Stimulsoft.Report.Components.StiText();
            this.Text5.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1, 2.8, 3, 0.6);
            this.Text5.Guid = "7e336e7979954d3ca90e534fd3fb84e3";
            this.Text5.Name = "Text5";
            this.Text5.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text5__GetValue);
            this.Text5.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text5.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text5.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.Text5.Indicator = null;
            this.Text5.Interaction = null;
            this.Text5.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text5.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text5.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text6
            // 
            this.Text6 = new Stimulsoft.Report.Components.StiText();
            this.Text6.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4, 2.2, 4, 0.6);
            this.Text6.Name = "Text6";
            this.Text6.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text6__GetValue);
            this.Text6.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text6.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text6.Guid = null;
            this.Text6.Indicator = null;
            this.Text6.Interaction = null;
            this.Text6.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text6.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text6.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text7
            // 
            this.Text7 = new Stimulsoft.Report.Components.StiText();
            this.Text7.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4, 1.6, 4, 0.6);
            this.Text7.Name = "Text7";
            this.Text7.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text7__GetValue);
            this.Text7.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text7.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text7.Guid = null;
            this.Text7.Indicator = null;
            this.Text7.Interaction = null;
            this.Text7.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text7.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text7.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text8
            // 
            this.Text8 = new Stimulsoft.Report.Components.StiText();
            this.Text8.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4, 2.8, 4, 0.6);
            this.Text8.Name = "Text8";
            this.Text8.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text8__GetValue);
            this.Text8.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text8.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text8.Guid = null;
            this.Text8.Indicator = null;
            this.Text8.Interaction = null;
            this.Text8.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text8.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text8.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text9
            // 
            this.Text9 = new Stimulsoft.Report.Components.StiText();
            this.Text9.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4, 1, 3, 0.6);
            this.Text9.Name = "Text9";
            this.Text9.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text9__GetValue);
            this.Text9.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text9.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text9.Guid = null;
            this.Text9.Indicator = null;
            this.Text9.Interaction = null;
            this.Text9.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text9.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text9.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text20
            // 
            this.Text20 = new Stimulsoft.Report.Components.StiText();
            this.Text20.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(7, 1, 1.4, 0.6);
            this.Text20.Guid = "7cd69f7b765f44c981c6a0e2545e592b";
            this.Text20.Name = "Text20";
            this.Text20.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text20__GetValue);
            this.Text20.Type = Stimulsoft.Report.Components.StiSystemTextType.Expression;
            this.Text20.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text20.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text20.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.Text20.Indicator = null;
            this.Text20.Interaction = null;
            this.Text20.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text20.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text20.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text21
            // 
            this.Text21 = new Stimulsoft.Report.Components.StiText();
            this.Text21.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(8.4, 1, 3.6, 0.6);
            this.Text21.Name = "Text21";
            this.Text21.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text21__GetValue);
            this.Text21.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text21.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text21.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text21.Guid = null;
            this.Text21.Indicator = null;
            this.Text21.Interaction = null;
            this.Text21.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text21.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text21.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.PageHeaderBand1.Guid = null;
            this.PageHeaderBand1.Interaction = null;
            // 
            // Container1
            // 
            this.Container1 = new Stimulsoft.Report.Components.StiContainer();
            this.Container1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(1, 5, 17, 22);
            this.Container1.Name = "Container1";
            this.Container1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Container1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // HeaderBand1
            // 
            this.HeaderBand1 = new Stimulsoft.Report.Components.StiHeaderBand();
            this.HeaderBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.4, 17, 0.6);
            this.HeaderBand1.Name = "HeaderBand1";
            this.HeaderBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.HeaderBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text11
            // 
            this.Text11 = new Stimulsoft.Report.Components.StiText();
            this.Text11.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 4.4, 0.6);
            this.Text11.Name = "Text11";
            this.Text11.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text11__GetValue);
            this.Text11.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text11.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text11.Guid = null;
            this.Text11.Indicator = null;
            this.Text11.Interaction = null;
            this.Text11.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text11.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text11.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text15
            // 
            this.Text15 = new Stimulsoft.Report.Components.StiText();
            this.Text15.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4.4, 0, 3.6, 0.6);
            this.Text15.Name = "Text15";
            this.Text15.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text15__GetValue);
            this.Text15.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text15.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text15.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text15.Guid = null;
            this.Text15.Indicator = null;
            this.Text15.Interaction = null;
            this.Text15.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text15.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text15.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text13
            // 
            this.Text13 = new Stimulsoft.Report.Components.StiText();
            this.Text13.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(8, 0, 4.2, 0.6);
            this.Text13.Name = "Text13";
            this.Text13.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text13__GetValue);
            this.Text13.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text13.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text13.Guid = null;
            this.Text13.Indicator = null;
            this.Text13.Interaction = null;
            this.Text13.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text13.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text13.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text17
            // 
            this.Text17 = new Stimulsoft.Report.Components.StiText();
            this.Text17.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(12.2, 0, 4.8, 0.6);
            this.Text17.Name = "Text17";
            this.Text17.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text17__GetValue);
            this.Text17.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text17.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text17.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text17.Guid = null;
            this.Text17.Indicator = null;
            this.Text17.Interaction = null;
            this.Text17.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text17.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text17.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.HeaderBand1.Guid = null;
            this.HeaderBand1.Interaction = null;
            // 
            // DataBand1
            // 
            this.DataBand1 = new Stimulsoft.Report.Components.StiDataBand();
            this.DataBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 1.8, 17, 0.6);
            this.DataBand1.DataSourceName = "Test";
            this.DataBand1.Name = "DataBand1";
            this.DataBand1.Sort = new System.String[0];
            this.DataBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.DataBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.DataBand1.BusinessObjectGuid = null;
            // 
            // Text10
            // 
            this.Text10 = new Stimulsoft.Report.Components.StiText();
            this.Text10.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0, 4.4, 0.6);
            this.Text10.Name = "Text10";
            this.Text10.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text10__GetValue);
            this.Text10.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text10.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text10.Guid = null;
            this.Text10.Indicator = null;
            this.Text10.Interaction = null;
            this.Text10.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text10.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text10.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text12
            // 
            this.Text12 = new Stimulsoft.Report.Components.StiText();
            this.Text12.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(8, 0, 4.2, 0.6);
            this.Text12.Name = "Text12";
            this.Text12.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text12__GetValue);
            this.Text12.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text12.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text12.Guid = null;
            this.Text12.Indicator = null;
            this.Text12.Interaction = null;
            this.Text12.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text12.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text12.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text16
            // 
            this.Text16 = new Stimulsoft.Report.Components.StiText();
            this.Text16.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(12.2, 0, 4.8, 0.6);
            this.Text16.Name = "Text16";
            this.Text16.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text16__GetValue);
            this.Text16.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text16.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text16.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text16.Guid = null;
            this.Text16.Indicator = null;
            this.Text16.Interaction = null;
            this.Text16.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text16.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text16.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text14
            // 
            this.Text14 = new Stimulsoft.Report.Components.StiText();
            this.Text14.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(4.4, 0, 3.6, 0.6);
            this.Text14.Name = "Text14";
            this.Text14.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text14__GetValue);
            this.Text14.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.All, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text14.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text14.Guid = null;
            this.Text14.Indicator = null;
            this.Text14.Interaction = null;
            this.Text14.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text14.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text14.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.DataBand1.DataRelationName = null;
            this.DataBand1.Guid = null;
            this.DataBand1.Interaction = null;
            this.DataBand1.MasterComponent = null;
            // 
            // FooterBand1
            // 
            this.FooterBand1 = new Stimulsoft.Report.Components.StiFooterBand();
            this.FooterBand1.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 3.2, 17, 0.8);
            this.FooterBand1.Name = "FooterBand1";
            this.FooterBand1.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.FooterBand1.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            // 
            // Text18
            // 
            this.Text18 = new Stimulsoft.Report.Components.StiText();
            this.Text18.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(0, 0.2, 10.4, 0.6);
            this.Text18.Guid = "5ec681b02f6847689d62e972f8f91fd4";
            this.Text18.Name = "Text18";
            this.Text18.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text18__GetValue);
            this.Text18.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text18.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text18.Indicator = null;
            this.Text18.Interaction = null;
            this.Text18.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text18.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text18.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            // 
            // Text19
            // 
            this.Text19 = new Stimulsoft.Report.Components.StiText();
            this.Text19.ClientRectangle = new Stimulsoft.Base.Drawing.RectangleD(10.4, 0.2, 1.6, 0.6);
            this.Text19.Name = "Text19";
            this.Text19.GetValue += new Stimulsoft.Report.Events.StiGetValueEventHandler(this.Text19__GetValue);
            this.Text19.Border = new Stimulsoft.Base.Drawing.StiBorder(Stimulsoft.Base.Drawing.StiBorderSides.None, System.Drawing.Color.Black, 1, Stimulsoft.Base.Drawing.StiPenStyle.Solid, false, 4, new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black), false);
            this.Text19.Brush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Transparent);
            this.Text19.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.Text19.Guid = null;
            this.Text19.Indicator = null;
            this.Text19.Interaction = null;
            this.Text19.Margins = new Stimulsoft.Report.Components.StiMargins(0, 0, 0, 0);
            this.Text19.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.Black);
            this.Text19.TextOptions = new Stimulsoft.Base.Drawing.StiTextOptions(false, false, false, 0F, System.Drawing.Text.HotkeyPrefix.None, System.Drawing.StringTrimming.None);
            this.FooterBand1.Guid = null;
            this.FooterBand1.Interaction = null;
            this.Container1.Guid = null;
            this.Container1.Interaction = null;
            this.Page2.ExcelSheetValue = null;
            this.Page2.Interaction = null;
            this.Page2.Margins = new Stimulsoft.Report.Components.StiMargins(1, 1, 1, 1);
            this.Page2_Watermark = new Stimulsoft.Report.Components.StiWatermark();
            this.Page2_Watermark.Font = new System.Drawing.Font("Arial", 100F);
            this.Page2_Watermark.Image = null;
            this.Page2_Watermark.TextBrush = new Stimulsoft.Base.Drawing.StiSolidBrush(System.Drawing.Color.FromArgb(50, 0, 0, 0));
            this.Отчет_PrinterSettings = new Stimulsoft.Report.Print.StiPrinterSettings();
            this.PrinterSettings = this.Отчет_PrinterSettings;
            this.Page2.Report = this;
            this.Page2.Watermark = this.Page2_Watermark;
            this.PageHeaderBand1.Page = this.Page2;
            this.PageHeaderBand1.Parent = this.Page2;
            this.Text1.Page = this.Page2;
            this.Text1.Parent = this.PageHeaderBand1;
            this.Text2.Page = this.Page2;
            this.Text2.Parent = this.PageHeaderBand1;
            this.Text3.Page = this.Page2;
            this.Text3.Parent = this.PageHeaderBand1;
            this.Text4.Page = this.Page2;
            this.Text4.Parent = this.PageHeaderBand1;
            this.Text5.Page = this.Page2;
            this.Text5.Parent = this.PageHeaderBand1;
            this.Text6.Page = this.Page2;
            this.Text6.Parent = this.PageHeaderBand1;
            this.Text7.Page = this.Page2;
            this.Text7.Parent = this.PageHeaderBand1;
            this.Text8.Page = this.Page2;
            this.Text8.Parent = this.PageHeaderBand1;
            this.Text9.Page = this.Page2;
            this.Text9.Parent = this.PageHeaderBand1;
            this.Text20.Page = this.Page2;
            this.Text20.Parent = this.PageHeaderBand1;
            this.Text21.Page = this.Page2;
            this.Text21.Parent = this.PageHeaderBand1;
            this.Container1.Page = this.Page2;
            this.Container1.Parent = this.Page2;
            this.HeaderBand1.Page = this.Page2;
            this.HeaderBand1.Parent = this.Container1;
            this.Text11.Page = this.Page2;
            this.Text11.Parent = this.HeaderBand1;
            this.Text15.Page = this.Page2;
            this.Text15.Parent = this.HeaderBand1;
            this.Text13.Page = this.Page2;
            this.Text13.Parent = this.HeaderBand1;
            this.Text17.Page = this.Page2;
            this.Text17.Parent = this.HeaderBand1;
            this.DataBand1.Page = this.Page2;
            this.DataBand1.Parent = this.Container1;
            this.Text10.Page = this.Page2;
            this.Text10.Parent = this.DataBand1;
            this.Text12.Page = this.Page2;
            this.Text12.Parent = this.DataBand1;
            this.Text16.Page = this.Page2;
            this.Text16.Parent = this.DataBand1;
            this.Text14.Page = this.Page2;
            this.Text14.Parent = this.DataBand1;
            this.FooterBand1.Page = this.Page2;
            this.FooterBand1.Parent = this.Container1;
            this.Text18.Page = this.Page2;
            this.Text18.Parent = this.FooterBand1;
            this.Text19.Page = this.Page2;
            this.Text19.Parent = this.FooterBand1;
            // 
            // Add to PageHeaderBand1.Components
            // 
            this.PageHeaderBand1.Components.Clear();
            this.PageHeaderBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text1,
                        this.Text2,
                        this.Text3,
                        this.Text4,
                        this.Text5,
                        this.Text6,
                        this.Text7,
                        this.Text8,
                        this.Text9,
                        this.Text20,
                        this.Text21});
            // 
            // Add to HeaderBand1.Components
            // 
            this.HeaderBand1.Components.Clear();
            this.HeaderBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text11,
                        this.Text15,
                        this.Text13,
                        this.Text17});
            // 
            // Add to DataBand1.Components
            // 
            this.DataBand1.Components.Clear();
            this.DataBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text10,
                        this.Text12,
                        this.Text16,
                        this.Text14});
            // 
            // Add to FooterBand1.Components
            // 
            this.FooterBand1.Components.Clear();
            this.FooterBand1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.Text18,
                        this.Text19});
            // 
            // Add to Container1.Components
            // 
            this.Container1.Components.Clear();
            this.Container1.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.HeaderBand1,
                        this.DataBand1,
                        this.FooterBand1});
            // 
            // Add to Page2.Components
            // 
            this.Page2.Components.Clear();
            this.Page2.Components.AddRange(new Stimulsoft.Report.Components.StiComponent[] {
                        this.PageHeaderBand1,
                        this.Container1});
            // 
            // Add to Pages
            // 
            this.Pages.Clear();
            this.Pages.AddRange(new Stimulsoft.Report.Components.StiPage[] {
                        this.Page2});
            this.Test.Columns.AddRange(new Stimulsoft.Report.Dictionary.StiDataColumn[] {
                        new Stimulsoft.Report.Dictionary.StiDataColumn("ServiceName", "Наименование услуги", "Наименование услуги", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("CompanyName", "Название компании", "Название компнаии", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("EmployeeName", "Имя сотрудника", "Имя сотрудника", typeof(string)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("ServicePrice", "Стоимость услуги", "Стоимость услуги", typeof(int)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("Дата оказания услуги", "DateOfCreate", "DateOfCreate", typeof(DateTime)),
                        new Stimulsoft.Report.Dictionary.StiDataColumn("TotalRows", "TotalRows", "TotalRows", typeof(int))});
            this.Test.Parameters.AddRange(new Stimulsoft.Report.Dictionary.StiDataParameter[] {
                        new Stimulsoft.Report.Dictionary.StiDataParameter("CompanyId", 8, 0),
                        new Stimulsoft.Report.Dictionary.StiDataParameter("ServiceId", 8, 0),
                        new Stimulsoft.Report.Dictionary.StiDataParameter("EmployeeId", 8, 0),
                        new Stimulsoft.Report.Dictionary.StiDataParameter("DateBegin", 12, 50),
                        new Stimulsoft.Report.Dictionary.StiDataParameter("DateEnd", 31, 7)});
            this.DataSources.Add(this.Test);
            this.Dictionary.Databases.Add(new Stimulsoft.Report.Dictionary.StiSqlDatabase("FitrstTask", "FitrstTask", "Integrated Security=True;Data Source=DESKTOP-8LKEMKN;Initial Catalog=FirstTask;Password=;User ID=", false));
            this.Test.Connecting += new System.EventHandler(this.GetTest_SqlCommand);
        }
        
        public void GetTest_SqlCommand(object sender, System.EventArgs e)
        {
            this.Test.SqlCommand = "IF @CompanyId > 0 AND @ServiceId > 0 AND @EmployeeId > 0\r\n(SELECT  Services.Name as ServiceName, Services.Price as ServicePrice, Employee.FullName as EmployeeName, Companies.Name as CompanyName, \r\nCOUNT(*) OVER() AS TotalRows\tFROM ServicesHistory \r\n\tINNER JOIN Services ON Services.Id = ServicesHistory.ServiceId\r\nINNER JOIN Employee ON Employee.Id = ServicesHistory.EmployeeId\r\nINNER JOIN Companies ON Companies.Id = ServicesHistory.CompanyId\r\nWHERE CONVERT(date, DateOfCreate) >= CONVERT(date, @DateBegin) AND CONVERT(date, DateOfCreate) <= CONVERT(date, @DateEnd) \r\nAND ServicesHistory.CompanyId = @CompanyId AND ServiceId = @ServiceId AND EmployeeId = EmployeeId)\r\n\r\nELSE IF @CompanyId > 0 AND @ServiceId > 0\r\n(SELECT  Services.Name as ServiceName, Services.Price as ServicePrice, Employee.FullName as EmployeeName, Companies.Name as CompanyName, \r\n\tCOUNT(*) OVER() AS TotalRows\tFROM ServicesHistory \r\n\tINNER JOIN Services ON Services.Id = ServicesHistory.ServiceId\r\nINNER JOIN Employee ON Employee.Id = ServicesHistory.E" +
"mployeeId\r\nINNER JOIN Companies ON Companies.Id = ServicesHistory.CompanyId\r\nWHERE CONVERT(date, DateOfCreate) >= CONVERT(date, @DateBegin) AND CONVERT(date, DateOfCreate) <= CONVERT(date, @DateEnd) \r\nAND ServicesHistory.CompanyId = @CompanyId AND ServiceId = @ServiceId)\r\n\r\nELSE IF @CompanyId > 0 AND @EmployeeId > 0\r\n(SELECT  Services.Name as ServiceName, Services.Price as ServicePrice, Employee.FullName as EmployeeName, Companies.Name as CompanyName, \r\n\tCOUNT(*) OVER() AS TotalRows\tFROM ServicesHistory \r\n\tINNER JOIN Services ON Services.Id = ServicesHistory.ServiceId\r\nINNER JOIN Employee ON Employee.Id = ServicesHistory.EmployeeId\r\nINNER JOIN Companies ON Companies.Id = ServicesHistory.CompanyId\r\nWHERE CONVERT(date, DateOfCreate) >= CONVERT(date, @DateBegin) AND CONVERT(date, DateOfCreate) <= CONVERT(date, @DateEnd)\r\nAND ServicesHistory.CompanyId = @CompanyId AND EmployeeId = @EmployeeId)\r\n\r\nELSE IF @EmployeeId > 0 AND @ServiceId > 0\r\n(SELECT  Services.Name as ServiceName, Services.Price as ServicePrice, Emp" +
"loyee.FullName as EmployeeName, Companies.Name as CompanyName, \r\n\tCOUNT(*) OVER() AS TotalRows\tFROM ServicesHistory \r\n\tINNER JOIN Services ON Services.Id = ServicesHistory.ServiceId\r\nINNER JOIN Employee ON Employee.Id = ServicesHistory.EmployeeId\r\nINNER JOIN Companies ON Companies.Id = ServicesHistory.CompanyId\r\nWHERE CONVERT(date, DateOfCreate) >= CONVERT(date, @DateBegin) AND CONVERT(date, DateOfCreate) <= CONVERT(date, @DateEnd) \r\nAND ServicesHistory.EmployeeId = @EmployeeId AND ServiceId = @ServiceId)\r\n\r\nELSE IF @CompanyId > 0 \r\n(SELECT  Services.Name as ServiceName, Services.Price as ServicePrice, Employee.FullName as EmployeeName, Companies.Name as CompanyName, \r\n\tCOUNT(*) OVER() AS TotalRows\tFROM ServicesHistory \r\n\tINNER JOIN Services ON Services.Id = ServicesHistory.ServiceId\r\nINNER JOIN Employee ON Employee.Id = ServicesHistory.EmployeeId\r\nINNER JOIN Companies ON Companies.Id = ServicesHistory.CompanyId\r\nWHERE CONVERT(date, DateOfCreate) >= CONVERT(date, @DateBegin) AND CONVERT(date, DateOfCreate) <=" +
" CONVERT(date, @DateEnd) \r\nAND ServicesHistory.CompanyId = @CompanyId)\r\n\r\nELSE IF @ServiceId > 0 \r\n(SELECT  Services.Name as ServiceName, Services.Price as ServicePrice, Employee.FullName as EmployeeName, Companies.Name as CompanyName, \r\n\tCOUNT(*) OVER() AS TotalRows\tFROM ServicesHistory \r\n\tINNER JOIN Services ON Services.Id = ServicesHistory.ServiceId\r\nINNER JOIN Employee ON Employee.Id = ServicesHistory.EmployeeId\r\nINNER JOIN Companies ON Companies.Id = ServicesHistory.CompanyId\r\nWHERE CONVERT(date, DateOfCreate) >= CONVERT(date, @DateBegin) AND CONVERT(date, DateOfCreate) <= CONVERT(date, @DateEnd)\r\nAND ServicesHistory.ServiceId = @ServiceId)\r\n\r\nELSE IF @EmployeeId > 0 \r\n(SELECT  Services.Name as ServiceName, Services.Price as ServicePrice, Employee.FullName as EmployeeName, Companies.Name as CompanyName, \r\n\tCOUNT(*) OVER() AS TotalRows\tFROM ServicesHistory \r\n\tINNER JOIN Services ON Services.Id = ServicesHistory.ServiceId\r\nINNER JOIN Employee ON Employee.Id = ServicesHistory.EmployeeId\r\nINNER JOIN Companie" +
"s ON Companies.Id = ServicesHistory.CompanyId\r\nWHERE CONVERT(date, DateOfCreate) >= CONVERT(date, @DateBegin) AND CONVERT(date, DateOfCreate) <= CONVERT(date, @DateEnd)\r\nAND ServicesHistory.EmployeeId = @EmployeeId)\r\n\r\nELSE\r\n(SELECT  Services.Name as ServiceName, Services.Price as ServicePrice, Employee.FullName as EmployeeName, Companies.Name as CompanyName, \r\n\tCOUNT(*) OVER() AS TotalRows\tFROM ServicesHistory \r\n\tINNER JOIN Services ON Services.Id = ServicesHistory.ServiceId\r\nINNER JOIN Employee ON Employee.Id = ServicesHistory.EmployeeId\r\nINNER JOIN Companies ON Companies.Id = ServicesHistory.CompanyId\r\nWHERE CONVERT(date, DateOfCreate) >= CONVERT(date, @DateBegin) AND CONVERT(date, DateOfCreate) <= CONVERT(date, @DateEnd))";
        }
        
        #region DataSource Test
        public class TestDataSource : Stimulsoft.Report.Dictionary.StiSqlSource
        {
            
            public TestDataSource() : 
                    base("FitrstTask", "Test", "Test", "", true, false, 30)
            {
            }
            
            public virtual string Наименование_услуги
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["ServiceName"], typeof(string), true)));
                }
            }
            
            public virtual string Название_компании
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["CompanyName"], typeof(string), true)));
                }
            }
            
            public virtual string Имя_сотрудника
            {
                get
                {
                    return ((string)(StiReport.ChangeType(this["EmployeeName"], typeof(string), true)));
                }
            }
            
            public virtual int Стоимость_услуги
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["ServicePrice"], typeof(int), true)));
                }
            }
            
            public virtual DateTime DateOfCreate
            {
                get
                {
                    return ((DateTime)(StiReport.ChangeType(this["Дата оказания услуги"], typeof(DateTime), true)));
                }
            }
            
            public virtual int TotalRows
            {
                get
                {
                    return ((int)(StiReport.ChangeType(this["TotalRows"], typeof(int), true)));
                }
            }
        }
        #endregion DataSource Test
        #endregion StiReport Designer generated code - do not modify
    }
}
