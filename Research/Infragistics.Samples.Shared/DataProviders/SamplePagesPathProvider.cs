using System;
using Infragistics.Samples.Framework.Utility;

namespace Infragistics.Samples.Shared.DataProviders
{
    public class SamplePagesPathProvider
    {
        /// <summary>
        /// Return Uri to an sample page in "/SamplesCommon/sl/[Local]/SamplePages/[fileName]" location for a given image.
        /// <para>where [Local] is "en-us" or "ja-jp" depending on Thread.CurrentThread.CurrentCulture. </para>
        /// <para>where [fileName] is file name of an image.</para>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Uri GetSamplePageUri(string fileName)
        {
            return new Uri(GetSamplePagePath(fileName));
        }

        public static string GetSamplePagePath(string pageName)
        {
            pageName = pageName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");

            return StorageDataProvider.GetStorageSamplePagePath(pageName);
        }
    }
}