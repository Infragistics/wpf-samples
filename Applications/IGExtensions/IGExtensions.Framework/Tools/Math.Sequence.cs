using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections;
using System.Collections.Generic;

namespace IGExtensions.Framework.Tools
{
    public static partial class MathTool
    {
        /// <summary>
        /// Calculates the median value of a sequence of values.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns>Median value</returns>
        public static double Median(IEnumerable<double> sequence)
        {
            List<double> values = new List<double>();

            foreach (double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    values.Add(value);
                }
            }

            return ArrayTool.PartitionByOrder(values, values.Count / 2);
        }

        /// <summary>
        /// Calculates the mean value of a sequence of values.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns>Mean value</returns>
        public static double Mean(IEnumerable<double> sequence)
        {
            int n=0;
            double mean=0.0;

            foreach(double value in sequence)
            {
                if(!double.IsNaN(value))
                {
                    mean+=value;
                }
            }

            mean=mean/n;

            return mean;
        }

        /// <summary>
        /// Calculates the average deviation of a sequence of values.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="average">Precalculated average if known.</param>
        /// <returns>Average deviation.</returns>
        public static double AverageDeviation(IEnumerable<double> sequence, double average=double.NaN)
        {
            if (double.IsNaN(average))
            {
                average=Mean(sequence);
            }

            return double.NaN;
        }
 
        /// <summary>
        /// Calculates the variance of a sequence of values.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="average">Precalculated average if known.</param>
        /// <returns>Variance.</returns>
        public static double Variance(IEnumerable<double> sequence, double average=double.NaN)
        {
            if (double.IsNaN(average))
            {
                average=Mean(sequence);
            }

            double variance = 0.0;
            double ep = 0.0;
            int count = 0;

            foreach (double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    double s = value - average;
                    double p = s * s;

                    ep += s;
                    variance += p;
                    ++count;
                }
            }

            return (variance - ep * ep / count) / (count - 1);
        }

        /// <summary>
        /// Calculates the standard deviation of a sequence of values
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="variance">Precalculated variance if known.</param>
        /// <param name="average">Precalculated average if known.</param>
        /// <returns>Standard deviation</returns>
        public static double StandardDeviation(IEnumerable<double> sequence, double variance=double.NaN, double average = double.NaN)
        {
            if (double.IsNaN(average))
            {
                average=Mean(sequence);
            }

            if (double.IsNaN(variance))
            {
                variance = Variance(sequence, average);
            }

            return Math.Sqrt(variance);
        }

        /// <summary>
        /// Calculates the skew of a sequence of values
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="variance">Precalculated variance if known.</param>
        /// <param name="average">Precalculated average if known.</param>
        /// <returns>Skew</returns>
        public static double Skew(IEnumerable<double> sequence, double variance=double.NaN, double average = double.NaN)
        {
            if (double.IsNaN(average))
            {
                average = Mean(sequence);
            }

            if (double.IsNaN(variance))
            {
                variance = Variance(sequence, average);
            }

            double skew = 0.0;
            int n = 0;

            foreach (double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    double s = value - average;
                    double p = s * s;

                    p *= s;
                    skew += p;
                }
            }

            skew = skew / (n * variance * variance) - 3.0;

            return skew;
        }

        /// <summary>
        /// Calculates the kurtosis of a sequence of values
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="variance">Precalculated variance if known.</param>
        /// <param name="average">Precalculated average if known.</param>
        /// <returns>Skew</returns>
        public static double Kurtosis(IEnumerable<double> sequence, double variance=double.NaN, double average = double.NaN)
        {
            if (double.IsNaN(average))
            {
                average = Mean(sequence);
            }

            if (double.IsNaN(variance))
            {
                variance = Variance(sequence, average);
            }

            double kurtosis = 0.0;
            int n = 0;

            foreach(double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    double s = value - average;
                    double p = s * s;

                    p *= s;
                    p *= s;
                    kurtosis += p;
                }
            }

            kurtosis = kurtosis / (n * variance * variance) - 3.0;

            return kurtosis;
        }

        /// <summary>
        /// Calculates the moving standard deviation.
        /// </summary>
        /// <remarks>
        /// NaN items in the sequence are ignores.
        /// </remarks>
        /// <param name="sequence"></param>
        /// <param name="period"></param>
        /// <param name="strict">Require at least period good values</param>
        /// <returns>The moving standard deviation</returns>
        public static IEnumerable<double> StandardDeviation(IEnumerable<double> sequence, int period, bool strict=false)
        {
            double[] buffer = new double[period];
            int cursor = 0;
            int count=0;

            double sum = 0.0;
            double stddev=double.NaN;

            for (int i = 0; i < period; ++i)
            {
                buffer[i] = double.NaN;
            }

            foreach (double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    #region update buffer and sum
                    if (double.IsNaN(buffer[cursor]))
                    {
                        count = Math.Min(count + 1, period);
                    }
                    else
                    {
                        sum -= buffer[cursor];
                    }

                    buffer[cursor] = value;
                    cursor = (cursor + 1) % period;
                    sum += value;
                    #endregion

                    #region update stddev
                    if (count > period || !strict)
                    {
                        double mean = sum / (double)count;
                        double t = 0.0;

                        for (int i = 0; i < period; ++i)
                        {
                            if (!double.IsNaN(buffer[i]))
                            {
                                t += MathTool.Sqr(buffer[i] - mean);
                            }
                        }

                        stddev = Math.Sqrt(t / (double)count);
                    }
                    else
                    {
                        stddev = double.NaN;
                    }
                    #endregion
                }

                yield return stddev;
            }
        }

        /// <summary>
        /// Calculates the moving mean absolute deviation.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="period"></param>
        /// <param name="strict">Require at least period good values</param>
        /// <returns>The moving mean absolute deviation.</returns>
        public static IEnumerable<double> MeanAbsoluteDeviation(IEnumerable<double> sequence, int period, bool strict = false)
        {
            double[] buffer = new double[period];
            int cursor = 0;
            int count = 0;

            double sum = 0.0;
            double mad=double.NaN;

            for (int i = 0; i < period; ++i)
            {
                buffer[i] = double.NaN;
            }

            foreach (double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    #region update buffer and sum
                    if (double.IsNaN(buffer[cursor]))
                    {
                        count = Math.Min(count + 1, period);
                    }
                    else
                    {
                        sum -= buffer[cursor];
                    }

                    buffer[cursor] = value;
                    cursor = (cursor + 1) % period;
                    sum += value;
                    #endregion

                    #region update mad
                    if (count > period || !strict)
                    {
                        double mean = sum / (double)count;
                        double t = 0.0;

                        for (int i = 0; i < period; ++i)
                        {
                            if (!double.IsNaN(buffer[i]))
                            {
                                t += Math.Abs(buffer[i] - mean);
                            }
                        }

                        mad = t / (double)count;
                    }
                    else
                    {
                        mad = double.NaN;
                    }
                    #endregion

                    yield return mad;
                }
            }
        }

        /// <summary>
        /// Calculates the moving maximum absolute deviation.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="period"></param>
        /// <param name="strict">Require at least period good values</param>
        /// <returns>The moving maximum absolute deviation.</returns>
        public static IEnumerable<double> MaximumAbsoluteDeviation(IEnumerable<double> sequence, int period, bool strict = false)
        {
            double[] buffer = new double[period];
            int cursor = 0;
            int count = 0;

            double sum = 0.0;
            double mad=double.NaN;

            for (int i = 0; i < period; ++i)
            {
                buffer[i] = double.NaN;
            }

            foreach (double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    #region update buffer and sum
                    if (double.IsNaN(buffer[cursor]))
                    {
                        count = Math.Min(count + 1, period);
                    }
                    else
                    {
                        sum -= buffer[cursor];
                    }

                    buffer[cursor] = value;
                    cursor = (cursor + 1) % period;
                    sum += value;
                    #endregion

                    #region update mad
                    if (count > 0 && (count > period || !strict))
                    {
                        double mean = sum / (double)count;
                        double t = 0.0;

                        for (int i = 0; i < period; ++i)
                        {
                            if (!double.IsNaN(buffer[i]))
                            {
                                t = Math.Max(t, Math.Abs(buffer[i] - mean));
                            }
                        }

                        mad = t;
                    }
                    else
                    {
                        mad = double.NaN;
                    }
                    #endregion
                }

                yield return mad;
            }
        }

        /// <summary>
        /// Calculates the simple moving average.
        /// </summary>
        /// <remarks>
        /// NaN items in the sequence are ignored.
        /// </remarks>
        /// <param name="sequence"></param>
        /// <param name="period"></param>
        /// <param name="strict">Require at least period good values</param>
        /// <returns>The simple moving average.</returns>
        public static IEnumerable<double> SimpleMovingAverage(IEnumerable<double> sequence, int period, bool strict=false)
        {
            double[] buffer = new double[period];
            int cursor = 0;
            int count = 0;

            double sum = 0.0;
            double sma = double.NaN;

            for (int i = 0; i < period; ++i)
            {
                buffer[i] = double.NaN;
            }

            foreach (double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    #region update buffer and sum
                    if (double.IsNaN(buffer[cursor]))
                    {
                        count = Math.Min(count + 1, period);
                    }
                    else
                    {
                        sum -= buffer[cursor];
                    }

                    buffer[cursor] = value;
                    cursor = (cursor + 1) % period;
                    sum += value;
                    #endregion

                    #region update sma

                    if (count > period || !strict)
                    {
                        sma = sum / (double)count;
                    }
                    else
                    {
                        sma = double.NaN;
                    }
                    #endregion
                }

                yield return sma;
            }
        }

        /// <summary>
        /// Calculates the cumulative moving average.
        /// </summary>
        /// <remarks>
        /// NaN items in the sequence are ignored.
        /// </remarks>
        /// <param name="sequence"></param>
        /// <returns>The cumulative moving average.</returns>
        public static IEnumerable<double> CumulativeMovingAverage(IEnumerable<double> sequence)
        {
            IEnumerator<double> enumerator = sequence.GetEnumerator();
            double cma=double.NaN;
            int count=0;

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;

                if (!double.IsNaN(enumerator.Current))
                {
                    cma = enumerator.Current;
                    count = 1;
                    break;
                }
            }

            while (enumerator.MoveNext())
            {
                if (!double.IsNaN(enumerator.Current))
                {
                    cma = cma + (enumerator.Current + cma) / (count + 1);
                    ++count;
                }

                yield return cma;
            }
        }

        /// <summary>
        /// Calculates the exponential moving average.
        /// </summary>
        /// <remarks>
        /// NaN items in the sequence are ignored.
        /// </remarks>
        /// <param name="sequence"></param>
        /// <param name="α"></param>
        /// <returns>The exponential moving average.</returns>
        public static IEnumerable<double> ExponentialMovingAverage(IEnumerable<double> sequence, double α)
        {
            IEnumerator<double> enumerator = sequence.GetEnumerator();
            double ema = double.NaN;

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;

                if (!double.IsNaN(enumerator.Current))
                {
                    ema = enumerator.Current;
                    break;
                }
            }

            while (enumerator.MoveNext())
            {
                if (!double.IsNaN(enumerator.Current))
                {
                    ema += α * (enumerator.Current - ema);
                }

                yield return ema;
            }
        }

        /// <summary>
        /// Calculates the exponential moving average.
        /// </summary>
        /// <remarks>
        /// NaN items in the sequence are ignored.
        /// </remarks>
        /// <param name="sequence"></param>
        /// <param name="period"></param>
        /// <param name="strict"></param>
        /// <returns>The exponential moving average.</returns>
        public static IEnumerable<double> ExponentialMovingAverage(IEnumerable<double> sequence, int period, bool strict=false)
        {
            return exponentialMovingAverage(sequence, 2.0 / (1.0 + period), period, strict);
        }

        /// <summary>
        /// Calculates the modified moving average.
        /// </summary>
        /// <remarks>
        /// NaN items in the sequence are ignored.
        /// </remarks>
        /// <param name="sequence"></param>
        /// <param name="period"></param>
        /// <param name="strict"></param>
        /// <returns>The exponential moving average.</returns>
        public static IEnumerable<double> ModifiedMovingAverage(IEnumerable<double> sequence, int period, bool strict = false)
        {
            return exponentialMovingAverage(sequence, 1.0 / period, period, strict);
        }

        private static IEnumerable<double> exponentialMovingAverage(IEnumerable<double> sequence, double α, int period, bool strict = false)
        {
            double[] buffer = new double[period];
            int cursor = 0;
            int count = 0;
            double ema = double.NaN;

            for (int i = 0; i < period; ++i)
            {
                buffer[i] = double.NaN;
            }

            foreach (double value in sequence)
            {
                if (!double.IsNaN(value))
                {
                    #region update buffer
                    if (double.IsNaN(buffer[cursor]))
                    {
                        count = Math.Min(count + 1, period);
                    }

                    buffer[cursor] = value;
                    cursor = (cursor + 1) % period;

                    buffer[cursor] = value;
                    #endregion

                    ema = double.IsNaN(ema) ? value : ema + α * (value - ema);

                    yield return strict && count < period ? double.NaN : ema;
                }
            }
        }

        // mean, median 
    }
}
