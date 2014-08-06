using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace OutlookGoogleSync
{
    public class EwsCalendar : IOfficeCalendar
    {
        public List<OutlookAppointment> GetCalendarEntriesInRange()
        {
            List<OutlookAppointment> appointments = new List<OutlookAppointment>();
            var service = new Microsoft.Exchange.WebServices.Data.ExchangeService(Microsoft.Exchange.WebServices.Data.ExchangeVersion.Exchange2010_SP1);
            service.Credentials = new Microsoft.Exchange.WebServices.Data.WebCredentials(Settings.Instance.ExchageUser, Settings.Instance.ExchagePassword);

            if (string.IsNullOrWhiteSpace(Settings.Instance.ExchageServerAddress))
            {
                service.AutodiscoverUrl(Settings.Instance.ExchageUser, ValidateRedirectionUrlCallback);
            }
            else
            {
                service.Url = new Uri(Settings.Instance.ExchageServerAddress + "/EWS/Exchange.asmx");
            }

            DateTime min = DateTime.Now.AddDays(-Settings.Instance.DaysInThePast);
            DateTime max = DateTime.Now.AddDays(+Settings.Instance.DaysInTheFuture + 1);

            var calView = new Microsoft.Exchange.WebServices.Data.CalendarView(min, max);
            calView.PropertySet = new PropertySet(
                BasePropertySet.IdOnly,
                AppointmentSchema.Subject,
                AppointmentSchema.Start,
                AppointmentSchema.IsRecurring,
                AppointmentSchema.AppointmentType,
                AppointmentSchema.End,
                AppointmentSchema.IsAllDayEvent,
                AppointmentSchema.Location,
                AppointmentSchema.Organizer,
                AppointmentSchema.ReminderMinutesBeforeStart,
                AppointmentSchema.IsReminderSet
                );

            FindItemsResults<Appointment> findResults = service.FindAppointments(WellKnownFolderName.Calendar, calView);

            foreach (Appointment appointment in findResults.Items)
            {
                OutlookAppointment newAppointment = GetOutlookAppointment(appointment, service);
                appointments.Add(newAppointment);
            }

            return appointments;
        }

        private bool ValidateRedirectionUrlCallback(string redirectionUrl)
        {
            return true;
        }

        public List<OutlookAppointment> GetCalendarEntries()
        {
            List<OutlookAppointment> appointments = new List<OutlookAppointment>();
            var _service = new Microsoft.Exchange.WebServices.Data.ExchangeService(Microsoft.Exchange.WebServices.Data.ExchangeVersion.Exchange2010_SP1);
            _service.Credentials = new Microsoft.Exchange.WebServices.Data.WebCredentials(Settings.Instance.ExchageUser, Settings.Instance.ExchagePassword);
            _service.Url = new Uri(Settings.Instance.ExchageServerAddress);
            var items = _service.FindItems(Microsoft.Exchange.WebServices.Data.WellKnownFolderName.Calendar, new Microsoft.Exchange.WebServices.Data.ItemView(int.MaxValue));
            foreach (Microsoft.Exchange.WebServices.Data.Appointment appointment in items)
            {
                OutlookAppointment newAppointment = GetOutlookAppointment(appointment, _service);

                appointments.Add(newAppointment);
            }
            return appointments;
        }

        private OutlookAppointment GetOutlookAppointment(Microsoft.Exchange.WebServices.Data.Appointment appointment, ExchangeService service)
        {
            OutlookAppointment newAppointment = new OutlookAppointment();

            PropertySet props = new PropertySet();
            props.Add(AppointmentSchema.Body);
            props.Add(AppointmentSchema.Subject);

            newAppointment.IsEWS = true;
            newAppointment.Service = service;
            newAppointment.Id = appointment.Id.UniqueId;
            newAppointment.Subject = appointment.Subject;
            newAppointment.Start = appointment.Start;
            newAppointment.End = appointment.End;
            newAppointment.AllDayEvent = appointment.IsAllDayEvent;
            newAppointment.Location = appointment.Location;
            newAppointment.Organizer = appointment.Organizer.ToString();
            newAppointment.ReminderMinutesBeforeStart = appointment.ReminderMinutesBeforeStart;
            newAppointment.ReminderSet = appointment.IsReminderSet;
            return newAppointment;
        }

    }
}