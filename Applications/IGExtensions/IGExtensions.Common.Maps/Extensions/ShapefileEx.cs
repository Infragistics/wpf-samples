using System.Collections.Generic;
using IGExtensions.Framework;

namespace Infragistics.Controls.Maps
{
    public static class ShapefileEx
    {

        /// <summary>
        /// Extracts <see cref="ShapefileRecord"/> items that match field value in the Fields[fieldKey] column 
        /// </summary>
        public static List<ShapefileRecord> Extract(this ShapefileConverter shapefile, string fieldKey, string fieldValue)
        {
            var items = new List<ShapefileRecord>();
            foreach (ShapefileRecord record in shapefile)
            {
                if (record.Fields.ContainsKey(fieldKey) && record.Fields[fieldKey] is string)
                {
                    var value = (string)record.Fields[fieldKey];
                    if (value == fieldValue)
                        items.Add(record);
                }
            }
            return items;
        }
        /// <summary>
        /// Extracts <see cref="ShapefileRecord"/> items that match field value in the Fields[fieldKey] column 
        /// </summary>
        public static List<ShapefileRecord> Extract(this ShapefileConverter shapefile, string fieldKey, double fieldValue)
        {
            var items = new List<ShapefileRecord>();
            foreach (ShapefileRecord record in shapefile)
            {
                if (record.Fields.ContainsKey(fieldKey) && record.Fields[fieldKey] is double)
                {
                    var value = (double)record.Fields[fieldKey];
                    if (value == fieldValue)
                        items.Add(record);
                }
            }
            return items;
        }

        #region ShapefileRecordFields
        /// <summary>
        /// Checks if <see cref="ShapefileRecordFields"/> contains a key in its list of keys loaded from database (.dbf) file 
        /// </summary>
        public static bool ContainsKey(this ShapefileRecordFields fields, string fieldKey)
        {
            foreach (var key in fields.Keys)
            {
                if (fieldKey == key) return true;
            }
            return false;
        }
        /// <summary>
        /// Gets value that corresponds to specified key in <see cref="ShapefileRecordFields"/>
        /// </summary>
        /// <remarks>
        /// <para>Warning: Throws InvalidCastException if type of field value is incompatible with provided type</para>
        /// <code>Usage: ShapefileRecord.Fields.GetValue<DATA_TYPE>("DATA_COLUMN");</code>
        /// </remarks>
        public static double GetValue(this ShapefileRecordFields fields, string fieldKey)
        {
            if (!fields.ContainsKey(fieldKey))
            {
                DebugManager.LogWarning("Key not found in ShapefileRecordFields: " + fieldKey);
                return double.NaN;
            }
            return (double)(fields[fieldKey]);
        }
        /// <summary>
        /// Gets value that corresponds to specified key in <see cref="ShapefileRecordFields"/>
        /// </summary>
        /// <remarks>
        /// <para>Warning: Throws InvalidCastException if type of field value is incompatible with provided type</para>
        /// <code>Usage: ShapefileRecord.Fields.GetValue<DATA_TYPE>("DATA_COLUMN");</code>
        /// </remarks>
        public static T GetValue<T>(this ShapefileRecordFields fields, string fieldKey)
        {
            if (!fields.ContainsKey(fieldKey))
            {
                DebugManager.LogWarning("Key not found in ShapefileRecordFields: " + fieldKey);
                return default(T);
            }
            return (T)(fields[fieldKey]);

        }
        #endregion

        #region ShapefileRecord
        /// <summary>
        /// Checks if <see cref="ShapefileRecord"/> contains a key in its field keys loaded from database (.dbf) file 
        /// </summary>
        public static bool ContainsKey(this ShapefileRecord record, string fieldKey)
        {
            foreach (ShapefileRecordFields field in record.Fields)
            {
                if (field.ContainsKey(fieldKey)) return true;
            }
            return false;
        } 
        /// <summary>
        /// Gets value that corresponds to specified key in <see cref="ShapefileRecord"/>
        /// </summary>
        /// <remarks>
        /// <para>Warning: Throws InvalidCastException if type of field value is incompatible with provided type</para>
        /// <code>Usage: ShapefileRecord.GetValue<DATA_TYPE>("DATA_COLUMN");</code>
        /// </remarks>
        public static T GetValue<T>(this ShapefileRecord record, string fieldKey)
        {
            return record.Fields.GetValue<T>(fieldKey);
        }
        #endregion
    }
}