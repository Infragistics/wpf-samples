using System;
using System.Collections.Generic;

namespace IGMap.Models
{
    public class MapLoadingArbitter
    {
        private double _totalProgress = 0;

        public double GetTotalProgress()
        {
            _totalProgress = 0;
            foreach (KeyValuePair<string, double> kvp in Dictionary)
            {
                _totalProgress += kvp.Value;
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            return _totalProgress;
        }

        public Dictionary<string, double> Dictionary = new Dictionary<string, double>();

        public new string ToString()
        {
            string result = string.Empty;
            double totalProgress = 0;
            foreach (KeyValuePair<string, double> kvp in Dictionary)
            {
                totalProgress += kvp.Value;
                //result += String.Format("{0}, {1:0.00} %", kvp.Key, kvp.Value);
                result += String.Format("{0} = {1} %", kvp.Key, kvp.Value);
                result += Environment.NewLine;
                //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }
            totalProgress = totalProgress / Dictionary.Count;
            result += String.Format("{0} %", totalProgress);
            return result;
        }
    }
}