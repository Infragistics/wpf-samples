using System;
using System.Collections.Generic;
using System.Linq;

namespace Infragistics.Framework 
{
     
    /// <summary>
    /// Represents csv file as data table with rows of strings
    /// </summary>
    public class CsvDataTable
    {

        public CsvDataTable(List<List<string>> csv)
        {
            Rows = csv;
            Update();
        }


        #region Properties
        /// <summary> Gets or sets Rows </summary>
        public List<List<string>> Rows { get; private set; }

        /// <summary> Gets a dictionary of names and indexes for data columns </summary>
        public Dictionary<string, int> Columns { get; private set; }

        /// <summary> Gets index of column with specified name </summary>
        public int this[string name]
        {
            get
            {
                //var key = name.ToLower().Trim();
                var key = name.Trim();
                if (this.Columns.ContainsKey(key))
                {
                    return this.Columns[key];
                } 
                return -1;
            }
        }

        /// <summary> Gets name of column with specified index </summary>
        public string this[int columnIndex]
        {
            get
            {
                var columnName = string.Empty;
                if (this.Contains(columnIndex))
                {
                    columnName = Rows[0][columnIndex];
                }
                return columnName;
            }
        }
        /// <summary> Gets item with specified row index and column name</summary>
        public string this[int rowIndex, string columnName]
        {
            get
            {
                var columnIndex = this[columnName];
                if (columnIndex >= 0)
                {
                    return Rows[rowIndex][columnIndex];
                }
                return string.Empty;
            }
        } 
        #endregion

        #region Methods

        public bool Contains(int columnIndex)
        {
            if (this.Rows.Count > 0
                && columnIndex >= 0
                && columnIndex < Rows[0].Count)
            {
                return true;
            }
            return false;
        }

        private void Update()
        {
            this.Columns = new Dictionary<string, int>();
            var index = 0;
            foreach (var row in Rows[0])
            {
                var header = row;
                if (header.Contains("("))
                    header = header.Substring(0, header.IndexOf("(") - 1);

                //header = header.ToLower().Trim();
                header = header.Trim();

                if (!this.Columns.ContainsKey(header))
                     this.Columns.Add(header.Trim(), index++);
            }

        } 
        #endregion
    }

}