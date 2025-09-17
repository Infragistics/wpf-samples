using System; 
using System.Collections.Generic; 
using Infragistics.Scheduler;
using Infragistics.XamarinForms.Controls.Scheduler;

#if PORTABLE
using Infragistics.Portable.Runtime;
#else
using System.Runtime.CompilerServices;
#endif

namespace Infragistics.Framework
{
#region DataSourceFactory class
#if TINYCLR
    [DontObfuscate]
#endif
    public class DataSourceFactory
    {
        #region Create
        static
#if PCL_UNIT_TEST
        internal
#else
        public
#endif
        ScheduleDataSource Create()
        {
            int year = DateTime.Today.Year;
            DateTime min = new DateTime(year, 1, 1);
            DateTime max = new DateTime(year + 1, 1, 1);        

            return DataSourceFactory.Create(min, max, 1000, 10);
        }

        static
#if PCL_UNIT_TEST
        internal
#else
        public
#endif
        ScheduleDataSource Create(
            DateTime minDate,
            DateTime maxDate,
            int appointmentCount,
            int resourceCount,
            bool hasRecurrences = true)
        {
            IList<AppointmentDataObjectBase> appointmentList = null;
            IList<ResourceDataObject> resourceList = null;
            AppointmentDataGenerator generator =
                new AppointmentDataGenerator(
                    minDate,
                    maxDate,
                    appointmentCount,
                    resourceCount,
                    true,
                    false,
                    hasRecurrences);

            generator.GenerateAppointmentsAndResources(out appointmentList, out resourceList);


#if TINYCLR
            ResourceDataObject[] resourceArray = new ResourceDataObject[resourceList.Count];
            AppointmentDataObjectBase[] appointmentArray = new AppointmentDataObjectBase[appointmentList.Count];
            resourceList.CopyTo(resourceArray, 0);
            appointmentList.CopyTo(appointmentArray, 0);
#endif

            ScheduleListDataSource ds = new ScheduleListDataSource()
            {
#if TINYCLR
                AppointmentItemsSource = appointmentArray,
                ResourceItemsSource = resourceArray,
#else
                AppointmentItemsSource = appointmentList,
                ResourceItemsSource = resourceList,
#endif
            };

            return ds;
        }
        #endregion Create

#region ToAppointment
        static public Appointment ToAppointment(AppointmentDataObjectBase dataObject)
        {
            DateTime start, end;
            dataObject.GetAppointmentTimes(out start, out end);

            return new
                Appointment
                {
                    Id = dataObject.Id,
                    ResourceId = dataObject.ResourceId,
                    Start = start,
                    End = end,
                    Subject = dataObject.GetSubject(),
                    Location = dataObject.Location,
                    Recurrence = dataObject.Recurrence,
                };
        }
#endregion ToAppointment
    }
#endregion DataSourceFactory class
}