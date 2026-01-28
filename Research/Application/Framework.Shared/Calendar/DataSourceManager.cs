using Infragistics.Scheduler; 
using Infragistics.XamarinForms.Controls.Scheduler;
using System;
using System.Collections.Generic; 

namespace Infragistics.Framework
{
    internal static class DataSourceManager
	{
		#region Methods

		#region Internal Methods

		#region CreateDataSource
		internal static ScheduleDataSource CreateDataSource(bool randomData)
		{
			//if (randomData)
			//	return DataSourceFactory.Create(DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(5), 100, 5);
			//else
				return CreateSampleData();
		}
		#endregion //CreateDataSource

		#endregion //Internal Methods

		#region Private Methods

		#region CreateSampleData
		private static ScheduleDataSource CreateSampleData()
		{
			DateTime			date	= DateTime.Now.Date;
			List<Appointment>	appts	= new List<Appointment>();
			int					count	= 0;

			Func<DateTime,int,int,int,DateTime> MakeDateTime = (d,h,m,s) =>
			{
				return new DateTime(d.Year, d.Month, d.Day, h, m, s);
			};


			// Create some resources
			List<ScheduleResource>	resources = new List<ScheduleResource>();
			for (int i = 0; i < 6; i++)
			{
				resources.Add(new ScheduleResource("ID" + i.ToString(), "Resource #" + i.ToString()));
			}

			int resourceIndex = 0;

			// Appointments contained in a single day.
			for (int i = 0; i < 10; i++)
			{
				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 12, 0, 0),
					End			= MakeDateTime(date, 12, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});
				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);

				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 13, 0, 0),
					End			= MakeDateTime(date, 13, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});
				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);

				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 15, 0, 0),
					End			= MakeDateTime(date, 15, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});
				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);
				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 16, 0, 0),
					End			= MakeDateTime(date, 16, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});
				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);

				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 17, 0, 0),
					End			= MakeDateTime(date, 17, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});
				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);

				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 18, 0, 0),
					End			= MakeDateTime(date, 18, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});
				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);

				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 19, 0, 0),
					End			= MakeDateTime(date, 19, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});
				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);

				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 20, 0, 0),
					End			= MakeDateTime(date, 20, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});
				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);

				date = date.AddDays(1);
			}

			// Appointments that span 2 days.
			date = date.AddDays(30);	// Start 30 days later
			for (int i = 0; i < 10; i++)
			{
				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 12, 0, 0),
					End			= MakeDateTime(date.AddDays(1), 12, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});

				count++;
				resourceIndex	= BumpResourceIndex(resourceIndex, resources.Count);

				date = date.AddDays(1);
			}

			// Appointments that span 3 days.
			date = date.AddDays(30);	// Start 30 days later
			for (int i = 0; i < 10; i++)
			{
				appts.Add(new Appointment
				{
					Start		= MakeDateTime(date, 12, 0, 0),
					End			= MakeDateTime(date.AddDays(2), 12, 15, 0),
					Subject		= "Daily Meeting " + count.ToString(),
					Location	= "Conference Room " + count.ToString(),
					ResourceId	= resources[resourceIndex].Id
				});

				count++;
				resourceIndex = BumpResourceIndex(resourceIndex, resources.Count);

				date = date.AddDays(1);
			}

			ScheduleListDataSource lds = new ScheduleListDataSource();
			lds.AppointmentItemsSource = appts;
			lds.ResourceItemsSource = resources;

			return lds;
		}
		#endregion //CreateSampleData

		private static int BumpResourceIndex(int resourceIndex, int resourceCount)
		{
			resourceIndex++;
			if (resourceIndex > resourceCount - 1)
				resourceIndex = 0;

			return resourceIndex;
		}

		#endregion //Private Methods

		#endregion //Methods
	}
}
