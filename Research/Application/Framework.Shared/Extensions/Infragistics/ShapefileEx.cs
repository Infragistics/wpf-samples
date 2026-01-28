using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Infragistics.Controls.Maps;

namespace System.Collections.Generic
{
    public static class RectEx
    {
        public static List<Point> Points(this Rect rect)
        {
            var points = new List<Point>();
            points.Add(rect.TopLeft);
            points.Add(rect.TopRight);
            points.Add(rect.BottomRight);
            points.Add(rect.BottomLeft);

            return points;
        }
    }
    public static class PointListEx
    {
        public static Rect Bounds(this List<List<Point>> shapes)
        { 
            var rect = Rect.Empty;
            foreach (var shape in shapes)
            {
                rect.Union(shape.Bounds());
            }

            return rect;
        }
        public static Rect Bounds(this List<Point> points)
        {
            var xMin = double.MaxValue;
            var yMin = double.MaxValue;

            var xMax = double.MinValue;
            var yMax = double.MinValue;

            foreach (var point in points)
            {
                xMin = System.Math.Min(xMin, point.X);
                yMin = System.Math.Min(yMin, point.Y);
                xMax = System.Math.Max(xMax, point.X);
                yMax = System.Math.Max(yMax, point.Y);
            }

            return new Rect(new Point(xMin, yMin), new Point(xMax, yMax));
        }

        public static Rect Copy(this Rect rect)
        {

            return new Rect(rect.TopLeft, rect.BottomRight);
        }
    }
    
}


namespace Infragistics.Controls.Mapping
{
    public static class ShapefileEx
    {
        public static int GetPointsTotal(this ShapefileConverter shapefile )
        {
            var total = 0;
            foreach (var record in shapefile)
            {
                foreach (var points in record.Points)
                {
                    total += points.Count;
                }
            }
            return total;
        }

        /// <summary>
        /// Extracts <see cref="ShapefileRecord"/> items that match field value in the Fields[fieldKey] column 
        /// </summary>
        public static List<ShapefileRecord> Extract(this ShapefileConverter shapefile, string fieldKey, string fieldValue)
        {
            var items = new List<ShapefileRecord>();
            foreach (var record in shapefile)
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
            foreach (var record in shapefile)
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
               // if (typeof(T) == typeof(double)) return (T)0.0;
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
        public static double GetValue(this ShapefileRecord record, string fieldKey)
        {
            return record.Fields.GetValue<double>(fieldKey);
        }
        public static string GetString(this ShapefileRecord record, string fieldKey)
        {
            return record.Fields.GetValue<string>(fieldKey);
        }

        public static Rect Bounds(this ShapefileRecord record)
        {
            return record.Points.Bounds();
        }

        #endregion
    }
}