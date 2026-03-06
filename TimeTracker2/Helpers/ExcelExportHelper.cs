using ClosedXML.Excel;

namespace TimeTracker.Helpers
{
    internal static class ExcelExportHelper
    {
        public static void ExportToExcel(string filePath, DatabaseManager db)
        {
            var matrix = db.GetAllProjectHoursByDate();
            var projects = matrix.Keys.Select(k => k.Project).Distinct().OrderBy(p => p).ToList();
            var dates = matrix.Keys.Select(k => k.Date).Distinct().OrderBy(d => d).ToList();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Timesheet");

            ws.Cell(1, 1).Value = "Projects";
            for (int c = 0; c < dates.Count; c++)
            {
                ws.Cell(1, c + 2).Value = dates[c].ToString("d-MMM");
            }

            ws.Range(1, 1, 1, dates.Count + 1).Style.Fill.BackgroundColor = XLColor.Gray;
            ws.Range(1, 1, 1, dates.Count + 1).Style.Font.Bold = true;

            for (int r = 0; r < projects.Count; r++)
            {
                ws.Cell(r + 2, 1).Value = projects[r];
                ws.Cell(r + 2, 1).Style.Font.Bold = true;
                for (int c = 0; c < dates.Count; c++)
                {
                    var key = (projects[r], dates[c]);
                    var hours = matrix.TryGetValue(key, out var h) ? h : 0;
                    ws.Cell(r + 2, c + 2).Value = Math.Round(hours, 2);
                    ws.Cell(r + 2, c + 2).Style.NumberFormat.Format = "0.00";
                }
            }

            workbook.SaveAs(filePath);
        }
    }
}
