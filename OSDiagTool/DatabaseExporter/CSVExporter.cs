﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using Oracle.ManagedDataAccess.Client;

namespace OSDiagTool.DatabaseExporter {
    class CSVExporter {
        
        public static void SQLToCSVExport(SqlConnection connection, string tableName, string csvFilePath, int queryTimeout) {

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(csvFilePath + "\\" + tableName + ".csv")) {
                               
                string _selectAllQuery = "SELECT * FROM " + tableName;
                SqlCommand command = new SqlCommand(_selectAllQuery, connection) {
                    CommandTimeout = queryTimeout
                };

                try {

                    SqlDataReader dr = command.ExecuteReader();

                    for(int i = 0; i < dr.FieldCount; i++) {
                        string name = dr.GetName(i);

                        fs.Write(name + ";");
                    }

                    fs.WriteLine();

                    while (dr.Read()) {
                        for (int i = 0; i < dr.FieldCount; i++) {
                            string value = dr[i].ToString();

                            fs.Write(value + ";");
                        }
                        fs.WriteLine();
                    }
                        
                } catch (Exception e){
                    FileLogger.LogError("Unable to read data from SQL DB: " + tableName, e.Message, writeToConsole:false, writeDateTime:false);
                }                        

            }
        }

        public static void ORCLToCsvExport(OracleConnection connection, string tableName, string csvFilePath, int queryTimeout) {

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(csvFilePath + "\\" + tableName + ".csv")) {
                    
                string _selectAllQuery = "SELECT * FROM " + tableName;

                OracleCommand command = new OracleCommand(_selectAllQuery, connection) {
                    CommandTimeout = queryTimeout
                };

                try {

                    OracleDataReader dr = command.ExecuteReader();

                    for (int i = 0; i < dr.FieldCount; i++) {
                        string name = dr.GetName(i);

                        fs.Write(name + ";");
                    }

                    fs.WriteLine();

                    while (dr.Read()) {
                        for (int i = 0; i < dr.FieldCount; i++) {
                            string value = dr[i].ToString();

                            fs.Write(value + ";");
                        }
                        fs.WriteLine();
                    }

                } catch (Exception e) {
                    FileLogger.LogError("Unable to read data from SQL DB: " + tableName, e.Message, writeToConsole: false, writeDateTime: false);
                }

            }


        }

    }
}
