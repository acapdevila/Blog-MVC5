using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;

namespace Blog.Smoothies.Helpers
{

    public class ReportPdfResult : FileResult
    {
        public ReportPdfResult(Report localReport)  : base("application/pdf")
        {
            //The DeviceInfo settings should be changed based on the reportType
            const string deviceInfo = "<DeviceInfo>" +
                                      "  <OutputFormat>PDF</OutputFormat>" +
                                      "  <PageWidth>21cm</PageWidth>" +
                                      "  <PageHeight>25.7cm</PageHeight>" +
                                      "  <MarginTop>1cm</MarginTop>" +
                                      "  <MarginLeft>2cm</MarginLeft>" +
                                      "  <MarginRight>2cm</MarginRight>" +
                                      "  <MarginBottom>1cm</MarginBottom>" +
                                      "</DeviceInfo>";

            //Render the report
            FileContents = localReport.Render(
                "PDF",
                deviceInfo,
                out var mimeType,
                out var encoding,
                out var fileNameExtension,
                out var streams,
                out Warning[] warnings);
        }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "There's no reason to tamper-proof this array since it's supplied to the type's constructor.")]
        private byte[] FileContents { get; }

        protected override void WriteFile(HttpResponseBase response)
        {
            response.OutputStream.Write(FileContents, 0, FileContents.Length);
        }
    }
}