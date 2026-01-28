using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Data.OleDb;
using Infragistics.Samples.Assets.Providers;
using System.Threading;
using System.Globalization;

namespace Infragistics.Samples.Shared.Models.Common
{
    public enum NWindTable
    {
        Customers,
        Orders,
        OrderDetails,
    }

    public static class NWindDataSource
    {
        public static DataSet GetDataSet()
        {
            return GetDataSet(true);
        }

        public static DataSet GetDataSet(bool createRelationConstraints)
        {
            return FetchData(null, createRelationConstraints);
        }

        public static DataTable GetTable(NWindTable table, CultureInfo cultureInfo = null)
        {
            return GetTable(table, false, cultureInfo);
        }

        public static DataTable GetTable(NWindTable table, bool singleTableOnly, CultureInfo cultureInfo = null, int recordLimit = 0)
        {
            string tableName = GetTableName(table);

            if (null == tableName)
                return null;

            NWindTable? tableToFetch = singleTableOnly ? table : (NWindTable?)null;
            DataSet ds = FetchData(tableToFetch, true, cultureInfo, recordLimit);

            return ds.Tables[tableName];
        }

        private static string GetTableName(NWindTable table)
        {
            switch (table)
            {
                case NWindTable.Customers:
                    return "Customers";
                case NWindTable.Orders:
                    return "Orders";
                case NWindTable.OrderDetails:
                    return "Order Details";
                default:
                    Debug.Fail("Unrecognized table name:" + table.ToString());
                    return null;
            }
        }

        private static DataSet FetchData(NWindTable? table, bool createRelationConstraints, CultureInfo cultureInfo = null, int recordLimit = 0)
        {
            DataSet ds = new DataSet();

            string dbPath = StorageProvider.GetStorageMdbPath(cultureInfo);

            // this following will default to loading the English version of the NWind db
            if (!File.Exists(dbPath))
            {
                dbPath = StorageProvider.GetStorageMdbPath(new System.Globalization.CultureInfo("en-US"));
            }

            OleDbConnection oleConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath);

            OleDbCommand oleCustomersCommand = new OleDbCommand("SELECT * FROM Customers", oleConnection);
            OleDbCommand oleOrdersCommand = new OleDbCommand("SELECT * FROM Orders", oleConnection);
            OleDbCommand oleOrderDetailsCommand = new OleDbCommand("SELECT * FROM [Order Details]", oleConnection);

            OleDbDataAdapter oleAdapter = new OleDbDataAdapter();

            if (table == null || table == NWindTable.Customers)
            {
                oleAdapter.SelectCommand = oleCustomersCommand;
                if (recordLimit == 0)
                {
                    oleAdapter.Fill(ds, "Customers");
                }
                else
                {
                    oleAdapter.Fill(ds, 0, recordLimit, "Customers");
                }
            }

            if (table == null || table == NWindTable.Orders)
            {
                oleAdapter.SelectCommand = oleOrdersCommand;
                if (recordLimit == 0)
                {
                    oleAdapter.Fill(ds, "Orders");
                }
                else
                {
                    oleAdapter.Fill(ds, 0, recordLimit, "Orders");
                }
                InitializeAutoIncrementField(ds.Tables["Orders"], "OrderID");
            }

            if (table == null || table == NWindTable.OrderDetails)
            {
                oleAdapter.SelectCommand = oleOrderDetailsCommand;
                oleAdapter.Fill(ds, "Order Details");
            }

            if (table == null)
            {
                ds.Relations.Add(new DataRelation("CustomerOrders", ds.Tables["Customers"].Columns["CustomerID"], ds.Tables["Orders"].Columns["CustomerID"], createRelationConstraints));
                ds.Relations.Add(new DataRelation("OrdersOrderDetails", ds.Tables["Orders"].Columns["OrderID"], ds.Tables["Order Details"].Columns["OrderID"], createRelationConstraints));
            }

            return ds;
        }

        private static void InitializeAutoIncrementField(DataTable dt, string columnName)
        {
            DataColumn column = dt.Columns[columnName];
            column.AutoIncrement = true;
            column.AutoIncrementSeed = -1;
            column.AutoIncrementStep = -1;
        }
    }
}