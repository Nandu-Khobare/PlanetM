using System.Globalization;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System;

namespace PlanetM_Ops
{
    public class ImportDataOperations
    {
        DBOperations dbObj = new DBOperations();
        public bool ImportIMDbMoviesFromCSV(string fileName, ref string strErrorMsg)
        {
            try
            {
                DataTable dataTable = LoadCSVinDataTable(fileName, true);
                DataTable dataTableForSP = new DataTable();
                dataTableForSP.Columns.Add("ID", typeof(int));
                dataTableForSP.Columns.Add("IMDBID", typeof(string));
                dataTableForSP.Columns.Add("Title", typeof(string));
                dataTableForSP.Columns.Add("Rating", typeof(float));

                foreach (DataRow dr in dataTable.Rows)
                {
                    DataRow ndr;
                    int temp1 = 0;
                    float temp2 = 0;
                    ndr = dataTableForSP.NewRow();
                    if (int.TryParse(dr.Field<string>("position"), out temp1) && temp1 != 0)
                        ndr.SetField<int>("ID", temp1);
                    ndr.SetField<string>("IMDBID", dr.Field<string>("const"));
                    ndr.SetField<string>("Title", dr.Field<string>("Title"));
                    if (float.TryParse(dr.Field<string>("IMDb Rating"), out temp2) && temp2 != 0)
                    {
                        ndr.SetField<float>("Rating", temp2);
                    }
                    else
                    {
                        ndr.SetField<float>("Rating", 0);
                    }
                    dataTableForSP.Rows.Add(ndr);
                }
                if (dataTableForSP.Rows.Count == 0)
                {
                    strErrorMsg = "No data available for import!";
                    return false;
                }
                else if (!dbObj.ImportMoviesInMiniIMDB(dataTableForSP, ref strErrorMsg))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                return false;
            }
            finally
            {
            }
        }
        public bool ImportMyRatingsFromCSV(string fileName, ref string strErrorMsg)
        {
            try
            {
                DataTable dataTable = LoadCSVinDataTable(fileName, true);
                DataTable dataTableForSP = new DataTable();
                dataTableForSP.Columns.Add("IMDBID", typeof(string));
                dataTableForSP.Columns.Add("MyRating", typeof(int));

                foreach (DataRow dr in dataTable.Rows)
                {
                    DataRow ndr;
                    ndr = dataTableForSP.NewRow();
                    ndr.SetField<string>("IMDBID", dr.Field<string>("const"));
                    int MyRating = 0;
                    if (int.TryParse(dr.Field<string>("You rated"), out MyRating) && MyRating != 0)
                    {
                        ndr.SetField<int>("MyRating", MyRating);
                        dataTableForSP.Rows.Add(ndr);
                    }
                }
                if (dataTableForSP.Rows.Count == 0)
                {
                    strErrorMsg = "No data available for import!";
                    return false;
                }
                else if (!dbObj.ImportMyRatingsOnImdb(dataTableForSP, ref strErrorMsg))
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                return false;
            }
            finally
            {
            }
        }

        private DataTable LoadCSVinDataTable(string fullFileName, bool isFirstRowHeader)
        {
            string header = "No";
            string sql = string.Empty;
            DataTable dataTable = null;
            try
            {
                string path = Path.GetDirectoryName(fullFileName);
                string fileName = Path.GetFileName(fullFileName);
                sql = @"SELECT * FROM [" + fileName + "]";
                if (isFirstRowHeader)
                    header = "Yes";

                using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Text;HDR=" + header + "\""))
                {
                    using (OleDbCommand command = new OleDbCommand(sql, connection))
                    {
                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            dataTable = new DataTable { Locale = CultureInfo.CurrentCulture };
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            finally
            {
            }
            return dataTable;
        }
    }
}
