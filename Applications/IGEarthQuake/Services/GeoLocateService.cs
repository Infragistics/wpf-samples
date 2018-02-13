//-------------------------------------------------------------------------
// <copyright file="GeoLocateService.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows;

namespace IGShowcase.EarthQuake.Services
{
    //TODO move to services assembly
	public class GeoLocateService : IApplicationService
	{
		#region Private Member Variables
		private static Regex _regex = new Regex(
										"^Country: (?<Country>.*)[\\r\\n]*^City: (?<City>.*)[\\r\\n]*^Latitude: (?<Latitude>.*)[\\r\\n]*^Longitude: (?<Longitude>.*)[\\r\\n]*^IP: (?<IP>.*)", 
										RegexOptions.Multiline | RegexOptions.IgnoreCase);
		#endregion Private Member Variables

		#region IApplicationService Members

		/// <summary>
		/// Called by an application in order to initialize the application extension service.
		/// </summary>
		/// <param name="context">Provides information about the application state.</param>
		public void StartService(ApplicationServiceContext context)
		{
		}

		/// <summary>
		/// Called by an application in order to stop the application extension service.
		/// </summary>
		public void StopService()
		{
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Gets the location.
		/// </summary>
		/// <param name="callBack">The call back.</param>
		public void GetLocation(Action<GeoLocation> callBack)
		{
			const string sUrl = "http://api.hostip.info/get_html.php?position=true";
			var client = new WebClient();

			client.DownloadStringCompleted += (o, e) =>
			{
				if (e.Error != null) return;

				var location = new GeoLocation();
                location.IsPositionValid = true;
                
				var mc = _regex.Match(e.Result);

				if (!mc.Success) return;

				location.Country	= mc.Groups["Country"].Value;
				location.City		= mc.Groups["City"].Value;

                var stringLatitude = mc.Groups["Latitude"].Value;
                var stringLongitude = mc.Groups["Longitude"].Value;
                if (stringLatitude != null)
                {
                    stringLatitude = stringLatitude.TrimEnd();
                }
                if (stringLongitude != null)
                {
                    stringLongitude = stringLongitude.TrimEnd();
                }
                if (string.IsNullOrEmpty(stringLatitude) ||
                    string.IsNullOrEmpty(stringLongitude))
                {
                    location.IsPositionValid = false;
                }

				double.TryParse(stringLatitude, out location.Latitude);
				double.TryParse(stringLongitude, out location.Longitude);

				location.IP			= mc.Groups["IP"].Value;

				var cb = (Action<GeoLocation>)e.UserState;
				SynchronizationContext.Current.Post(x => cb(location), location);
			};

			client.DownloadStringAsync(new Uri(sUrl), callBack);
		}
		#endregion Public Methods
	}

	public struct GeoLocation
	{
		public string Country;
		public string City;
		public string IP;
		public double Latitude;
		public double Longitude;
        public bool IsPositionValid;
	}
}

