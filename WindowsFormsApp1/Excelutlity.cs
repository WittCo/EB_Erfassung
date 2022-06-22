using System;
using System.Data;
using System.Windows.Forms;

namespace WittEyE
{
    public static class Excelutlity
    {

        // Export DataTable into an excel file with field names in the header line
        // - Save excel file without ever making it visible if filepath is given
        // - Don't save excel file, just make it visible if no filepath is given
        public static void ExportToExcel(this DataTable tbl, string excelFilePath = null)
        {
            try
            {
                if (tbl == null || tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                workSheet.Cells[1, 1] = "Offne Bestellungen";
                workSheet.Cells[1, 2] = "Datum : " + DateTime.Now.ToShortDateString() + " ";

                // column headings
                for (var i = 0; i < tbl.Columns.Count; i++)
                {
                    workSheet.Cells[2, i + 1] = tbl.Columns[i].ColumnName;
                }

                // rows
                for (var i = 0; i < tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (var j = 0; j < tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 3, j + 1] = tbl.Rows[i][j];
                    }
                }

                // now we resize the columns  
                var excelCellrange = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[tbl.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;

                // check file path
                if (!string.IsNullOrEmpty(excelFilePath))
                {
                    try
                    {
                        excelFilePath += "\\Offene Besellungen_" + DateTime.Now.ToShortDateString() + ".xlsx";
                        workSheet.SaveAs(excelFilePath);
                        excelApp.Quit();
                        MessageBox.Show("Excelliste ist gespeichert");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                            + ex.Message);
                    }
                }
                else
                { // no file path is given
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
    }
}
