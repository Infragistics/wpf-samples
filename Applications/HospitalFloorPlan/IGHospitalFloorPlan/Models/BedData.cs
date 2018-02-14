using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using IGExtensions.Common.Data;
using IGExtensions.Common.Models;

namespace IGShowcase.HospitalFloorPlan.Models
{
    public class HospitalElement
    {
        public HospitalElement()
        {
            this.Shape = new List<List<Point>>();
        }
        public HospitalElement(string id, List<List<Point>> shape)
        {
            this.Id = id;
            this.Shape = shape;
        }
        private Point GetShapeCenter()
        {
            var geoCenter = this.Shape.ToShapeRect().Center();
            return geoCenter;
        }
        public Point ShapeCenter { get { return this.GetShapeCenter(); } }
        public List<List<Point>> Shape { get; set; }
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }
    }
    public class HospitalElementList : List<HospitalElement> 
    {
    }
	public class BedData
	{
	    public BedData()
	    {
	        this.BedShape = new List<List<Point>>();
	    }
		#region Public Variables
        public List<List<Point>> BedShape { get; set; }
        
        /// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the bed status.
		/// </summary>
		/// <value>The bed status.</value>
		public BedStatus BedStatus { get; set; }

		/// <summary>
		/// Gets or sets the patient.
		/// </summary>
		/// <value>The patient.</value>
		public Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has vital signs monitor.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has vital signs monitor; otherwise, <c>false</c>.
        /// </value>
		public bool HasVitalSignsMonitor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has X ray.
        /// </summary>
        /// <value><c>true</c> if this instance has X ray; otherwise, <c>false</c>.</value>
		public bool HasXRay { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        /// <value>The room.</value>
	    public string Room { get; set; }

        /// <summary>
        /// Gets or sets the floor.
        /// </summary>
        /// <value>The floor.</value>
        public string Floor { get; set; }

		#endregion Public Memeber Variables

		#region Public Methods
		/// <summary>
		/// Loads this instance.
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<BedData> Load()
		{
            var bdc = (BedDataCollection)(XamlReaderEx.Load(Assets.Resources.BedData.Xaml));

            //var strReader = new StringReader(Assets.Resources.BedData.Xaml);
            //XmlReader xmlReader = XmlReader.Create(strReader);
            //var bdc2 = (BedDataCollection)XamlReader.Load(xmlReader);

            foreach (BedData d in bdc)
			{
				if (d.BedStatus != BedStatus.Free)
				{
					if(d.Patient == null)
					{
						throw new NullReferenceException();
					}

                    if (string.IsNullOrEmpty(d.Patient.ImageUrl))
                    {
                        d.Patient.ImageUrl = "/IGShowcase.HospitalFloorPlan;component/assets/images/NoPicture.png";
                    }
                    else
                    {
                        d.Patient.ImageUrl = DataStorageProvider.ImagesPath + "" + d.Patient.ImageUrl;
                    }

					if (d.BedStatus != BedStatus.Assigned && d.Patient.EcgData == null)
					{
						d.Patient.EcgData = EcgDataGenerator.Generate(d.Patient.HeartRate);
					}
				}
			}
            return bdc;
		}
		#endregion Public Methods
	}
}
