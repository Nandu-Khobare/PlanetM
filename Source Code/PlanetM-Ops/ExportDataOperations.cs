using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Gios.Pdf;
using System.Drawing;
using PlanetM_Utility;

namespace PlanetM_Ops
{
    public class ExportDataOperations
    {
        public static void ExportGridToPDF(System.Data.DataTable table, string type, string fileName)
        {
            //fileName = @"C:\Users\KeNJe\Desktop\Movies.pdf";
            // Starting instantiate the document.
            // Remember to set the Docuement Format. In this case, we specify width and height.
            PdfDocument myPdfDocument = null;
            if (type == "Limited")
                myPdfDocument = new PdfDocument(PdfDocumentFormat.InCentimeters(30, 40));//850*1120
            else if (type == "ImdbLimited")
                myPdfDocument = new PdfDocument(PdfDocumentFormat.InCentimeters(45, 55));//1250*1550
            else if (type == "ALL")
                myPdfDocument = new PdfDocument(PdfDocumentFormat.InCentimeters(40, 50));//1120*1400
            else if (type == "ImdbALL")
                myPdfDocument = new PdfDocument(PdfDocumentFormat.InCentimeters(50, 60));//1400*1700
            else if (type == "Search")
                myPdfDocument = new PdfDocument(PdfDocumentFormat.InCentimeters(40, 50));//1120*1400
            else if (type == "ImdbSearch")
                myPdfDocument = new PdfDocument(PdfDocumentFormat.InCentimeters(50, 60));//1400*1700

            // This time we crate some Font in order to call them easy way...
            System.Drawing.Font FontRegular = new System.Drawing.Font("Verdana", 11, FontStyle.Regular);
            System.Drawing.Font FontRegularSmall = new System.Drawing.Font("Verdana", 9, FontStyle.Regular);
            System.Drawing.Font FontHeaders = new System.Drawing.Font("Verdana", 13, FontStyle.Bold);
            System.Drawing.Font FontHeadersSmall = new System.Drawing.Font("Verdana", 12, FontStyle.Bold);
            System.Drawing.Font FontTitle = new System.Drawing.Font("Verdana", 25, FontStyle.Bold);

            // Now we create a Table of x lines, y columns and 4 points of Padding.
            PdfTable myPdfTable = null;
            if (type == "Limited")
                myPdfTable = myPdfDocument.NewTable(FontRegular, table.Rows.Count, table.Columns.Count, 4);
            else if (type == "ImdbLimited")
                myPdfTable = myPdfDocument.NewTable(FontRegular, table.Rows.Count, table.Columns.Count, 4);
            else if (type == "ALL")
                myPdfTable = myPdfDocument.NewTable(FontRegularSmall, table.Rows.Count, table.Columns.Count, 4);
            else if (type == "ImdbALL")
                myPdfTable = myPdfDocument.NewTable(FontRegularSmall, table.Rows.Count, table.Columns.Count, 4);
            else if (type == "Search")
                myPdfTable = myPdfDocument.NewTable(FontRegularSmall, table.Rows.Count, table.Columns.Count, 4);
            else if (type == "ImdbSearch")
                myPdfTable = myPdfDocument.NewTable(FontRegularSmall, table.Rows.Count, table.Columns.Count, 4);

            // Importing datas from the datatables... (also column names for the headers!)
            myPdfTable.ImportDataTable(table);

            // Now we set our Graphic Design: Colors and Borders...
            myPdfTable.HeadersRow.SetColors(Color.Black, Color.Orange);
            myPdfTable.SetColors(Color.Black, Color.BlanchedAlmond, Color.Bisque);
            myPdfTable.SetBorders(Color.Red, 1, BorderType.CompleteGrid);
            if (type == "Limited")
                myPdfTable.HeadersRow.SetFont(FontHeaders);
            else if (type == "ImdbLimited")
                myPdfTable.HeadersRow.SetFont(FontHeaders);
            else if (type == "ALL")
                myPdfTable.HeadersRow.SetFont(FontHeaders);
            else if (type == "ImdbALL")
                myPdfTable.HeadersRow.SetFont(FontHeadersSmall);
            else if (type == "Search")
                myPdfTable.HeadersRow.SetFont(FontHeadersSmall);
            else if (type == "ImdbSearch")
                myPdfTable.HeadersRow.SetFont(FontHeadersSmall);

            // With just one method we can set the proportional width of the columns.
            // It's a "percentage like" assignment, but the sum can be different from 100.
            if (type == "ALL")
                myPdfTable.SetColumnsWidth(new int[] { 30, 10, 5, 5, 7, 8, 7, 22 });
            if (type == "ImdbALL")
                myPdfTable.SetColumnsWidth(new int[] { 6, 30, 9, 5, 5, 7, 30, 9, 13, 6, 6, 9, 6, 6 });
            else if (type == "Limited")
                myPdfTable.SetColumnsWidth(new int[] { 50, 15, 10, 15, 10 });
            else if (type == "ImdbLimited")
                myPdfTable.SetColumnsWidth(new int[] { 30, 13, 7, 7, 8, 30, 10, 7, 7 });
            else if (type == "Search")
                myPdfTable.SetColumnsWidth(new int[] { 30, 8, 4, 4, 10, 7, 7, 23 });
            else if (type == "ImdbSearch")
                myPdfTable.SetColumnsWidth(new int[] { 6, 30, 8, 5, 5, 25, 9, 6, 9, 6, 6 });

            // Now we set some alignment... for the whole table and then, for a column.
            myPdfTable.SetContentAlignment(ContentAlignment.MiddleCenter);
            myPdfTable.Columns[0].SetContentAlignment(ContentAlignment.MiddleLeft);
            if (type == "ImdbALL" || type == "ImdbSearch")
                myPdfTable.Columns[1].SetContentAlignment(ContentAlignment.MiddleLeft);

            // This will load the image without placing into the document. The good thing
            // is that the image will be written into the document just once even if we put it
            // more times and in different sizes and places!
            PdfImage LogoImage = myPdfDocument.NewImage(Path.Combine(Environment.CurrentDirectory, @"Images\PDFLogo.jpg"));

            // Here we start the loop to generate the table...
            while (!myPdfTable.AllTablePagesCreated)
            {
                // we create a new page to put the generation of the new TablePage:
                PdfPage newPdfPage = myPdfDocument.NewPage();
                PdfTablePage newPdfTablePage = null;
                PdfTextArea pta = null;
                if (type == "Limited")
                {
                    newPdfTablePage = myPdfTable.CreateTablePage(new PdfArea(myPdfDocument, 50, 150, 750, 900));

                    // now we start putting the logo into the right place with a high resoluton...
                    newPdfPage.Add(LogoImage, 50, 20, 100);
                    newPdfPage.Add(LogoImage, 700, 20, 100);

                    // we also put a Label 
                    pta = new PdfTextArea(FontTitle, Color.Red, new PdfArea(myPdfDocument, 0, 50, 850, 100), ContentAlignment.MiddleCenter, Configuration.ReadConfig("PDFTitle"));
                }
                else if (type == "ALL" || type == "Search")
                {
                    newPdfTablePage = myPdfTable.CreateTablePage(new PdfArea(myPdfDocument, 50, 150, 1020, 1150));

                    // now we start putting the logo into the right place with a high resoluton...
                    newPdfPage.Add(LogoImage, 50, 20, 100);
                    newPdfPage.Add(LogoImage, 970, 20, 100);

                    // we also put a Label 
                    pta = new PdfTextArea(FontTitle, Color.Red, new PdfArea(myPdfDocument, 0, 50, 1120, 100), ContentAlignment.MiddleCenter, Configuration.ReadConfig("PDFTitle"));
                }
                else if (type == "ImdbLimited")
                {
                    newPdfTablePage = myPdfTable.CreateTablePage(new PdfArea(myPdfDocument, 50, 150, 1150, 1300));

                    // now we start putting the logo into the right place with a high resoluton...
                    newPdfPage.Add(LogoImage, 50, 20, 100);
                    newPdfPage.Add(LogoImage, 1100, 20, 100);

                    // we also put a Label 
                    pta = new PdfTextArea(FontTitle, Color.Red, new PdfArea(myPdfDocument, 0, 50, 1250, 100), ContentAlignment.MiddleCenter, Configuration.ReadConfig("PDFTitle"));
                }
                else if (type == "ImdbALL" || type == "ImdbSearch")
                {
                    newPdfTablePage = myPdfTable.CreateTablePage(new PdfArea(myPdfDocument, 50, 150, 1300, 1450));

                    // now we start putting the logo into the right place with a high resoluton...
                    newPdfPage.Add(LogoImage, 50, 20, 100);
                    newPdfPage.Add(LogoImage, 1250, 20, 100);

                    // we also put a Label 
                    pta = new PdfTextArea(FontTitle, Color.Red, new PdfArea(myPdfDocument, 0, 50, 1400, 100), ContentAlignment.MiddleCenter, Configuration.ReadConfig("PDFTitle"));
                }
                // nice thing: we can put all the objects in the following lines, so we can have
                // a great control of layer sequence... 
                newPdfPage.Add(newPdfTablePage);
                newPdfPage.Add(pta);

                // we save each generated page before start rendering the next.
                newPdfPage.SaveToDocument();

            }
            // Finally we save the docuement...
            if (File.Exists(fileName))
                File.Delete(fileName);
            myPdfDocument.SaveToFile(fileName);
            MessageBox.Show("File : " + fileName + " has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ExportGridToExcel(DataGridView dataGridView, string fileName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range xlRange;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlApp.Visible = false;
            xlApp.UserControl = true;

            //xlApp.Columns.ColumnWidth = 30;
            // System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            // Add a workbook.
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            // Delete two Worksheets
            ((Excel.Worksheet)xlWorkBook.Sheets[2]).Delete();
            ((Excel.Worksheet)xlWorkBook.Sheets[2]).Delete();
            // Get First Worksheet
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = "Movies";
            xlWorkSheet.Cells.Font.Name = "Trebuchet MS";
            xlWorkSheet.Cells.Font.Size = 11;
            try
            {
                // Export titles
                for (int j = 0; j < dataGridView.Columns.Count - 1; j++)
                {
                    xlRange = (Excel.Range)xlWorkSheet.Cells[2, j + 2];
                    if (dataGridView.Columns[j + 1].Name == "MovieName" || dataGridView.Columns[j + 1].Name == "Title")
                        xlRange.EntireColumn.NumberFormat = "@";
                    if (dataGridView.Columns[j + 1].Name == "Year" || dataGridView.Columns[j + 1].Name == "Rating" || dataGridView.Columns[j + 1].Name == "MyRating" || dataGridView.Columns[j + 1].Name == "IMDBRating")
                        xlRange.EntireColumn.HorizontalAlignment = HorizontalAlignment.Center;
                    xlRange.Value2 = dataGridView.Columns[j + 1].HeaderText;
                    xlRange.Font.Bold = true;
                    xlRange.Font.Size = 12;
                    xlRange.Interior.Color = System.Drawing.Color.Orange;
                    xlRange.Borders.Color = System.Drawing.Color.Red;
                    //xlRange.Font.Background = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                }
                // Export data
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count - 1; j++)
                    {
                        xlRange = (Excel.Range)xlWorkSheet.Cells[i + 3, j + 2];
                        xlRange.Value2 = dataGridView[j + 1, i].Value.ToString();
                        xlRange.Interior.Color = System.Drawing.Color.BlanchedAlmond;
                        xlRange.Borders.Color = System.Drawing.Color.Red;
                    }
                }
                xlWorkSheet.Columns.AutoFit();

                #region KeepXLOpen
                //xlApp.Visible = true;
                //xlWorkBook = null;
                //xlApp = null;
                //GC.Collect();
                #endregion

                #region KeepXLOpenAndSave
                xlApp.Visible = true;
                if (File.Exists(fileName))
                    File.Delete(fileName);
                xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook = null;
                xlApp = null;
                GC.Collect();
                MessageBox.Show("File : " + fileName + " has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion

                #region SaveXLAndClose
                //if (File.Exists(fileName))
                //    File.Delete(fileName);
                //xlWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                //xlWorkBook.Close(true, misValue, misValue);
                //xlApp.Quit();
                //MessageBox.Show("File : " + fileName + " has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);
                MessageBox.Show(errorMessage, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExportGridToCSV(DataGridView dataGridView, string fileName)
        {
            StringBuilder strSave = new StringBuilder();
            try
            {
                // Export titles
                for (int j = 0; j < dataGridView.Columns.Count - 1; j++)
                {
                    strSave.Append(dataGridView.Columns[j + 1].HeaderText + ",");
                }
                strSave.AppendLine();

                // Export data
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count - 1; j++)
                    {
                        if (dataGridView.Columns[j + 1].Name == "MovieName" || dataGridView.Columns[j + 1].Name == "Title")
                            strSave.Append(dataGridView[j + 1, i].Value.ToString().Replace(",", " ") + ",");
                        else if (dataGridView.Columns[j + 1].Name == "Genre")
                            strSave.Append(dataGridView[j + 1, i].Value.ToString().Replace(",", "-") + ",");
                        else
                            strSave.Append(dataGridView[j + 1, i].Value + ",");
                    }
                    strSave.AppendLine();
                }
                if (File.Exists(fileName))
                    File.Delete(fileName);
                StreamWriter writer = new StreamWriter(fileName);
                writer.Write(strSave);
                writer.Close();
                MessageBox.Show("File : " + fileName + " has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ExportGridToText(DataGridView dataGridView, string fileName)
        {
            StringBuilder strSave = new StringBuilder();
            try
            {
                if (dataGridView.Columns[1].Name.ToUpper() == "MOVIENAME")
                {
                    // Export titles
                    strSave.Append(dataGridView.Columns[2].HeaderText + "\t\t" +
                        dataGridView.Columns[3].HeaderText + "\t" +
                        dataGridView.Columns[1].HeaderText);
                    strSave.AppendLine();

                    // Export data
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        if (dataGridView[2, i].Value.ToString().Length >= 10)
                        {
                            strSave.Append(dataGridView[2, i].Value + "\t" +
                                dataGridView[3, i].Value + "\t" +
                                dataGridView[1, i].Value);
                        }
                        else
                        {
                            strSave.Append(dataGridView[2, i].Value + "\t\t" +
                                dataGridView[3, i].Value + "\t" +
                                dataGridView[1, i].Value);
                        }
                        strSave.AppendLine();
                    }
                }
                else if (dataGridView.Columns[1].Name.ToUpper() == "IMDBID")
                {
                    // Export titles
                    strSave.Append(dataGridView.Columns[3].HeaderText + "\t\t" +
                        dataGridView.Columns[4].HeaderText + "\t" +
                        dataGridView.Columns[2].HeaderText);
                    strSave.AppendLine();

                    // Export data
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        if (dataGridView[3, i].Value.ToString().Length >= 10)
                        {
                            strSave.Append(dataGridView[3, i].Value + "\t" +
                                dataGridView[4, i].Value + "\t" +
                                dataGridView[2, i].Value);
                        }
                        else
                        {
                            strSave.Append(dataGridView[3, i].Value + "\t\t" +
                                dataGridView[4, i].Value + "\t" +
                                dataGridView[2, i].Value);
                        }
                        strSave.AppendLine();
                    }
                }
                if (File.Exists(fileName))
                    File.Delete(fileName);
                StreamWriter writer = new StreamWriter(fileName);
                writer.Write(strSave);
                writer.Close();
                MessageBox.Show("File : " + fileName + " has been created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
