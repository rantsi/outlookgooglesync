using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace OutlookGoogleSync
{
    public class OutlookAppointment
    {
        ExchangeService _service;

        public ExchangeService Service
        {
            get { return _service; }
            set { _service = value; }
        }

        private bool _isEWS = false;
        public bool IsEWS
        {
            get { return _isEWS; }
            set { _isEWS = value; }
        }
        public bool AllDayEvent { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Location { get; set; }
        public bool ReminderSet { get; set; }
        public int ReminderMinutesBeforeStart { get; set; }
        public string Organizer { get; set; }
        string _requiredAttendees;

        public string RequiredAttendees
        {
            get
            {
                LoadItemDataFromEWS();
                return _requiredAttendees;
            }
            set { _requiredAttendees = value; }
        }
        string _optionalAttendees;

        public string OptionalAttendees
        {
            get
            {
                LoadItemDataFromEWS();
                return _optionalAttendees;
            }
            set { _optionalAttendees = value; }
        }

        public string Subject { get; set; }

        string _body;
        public string Body
        {
            get
            {
                LoadItemDataFromEWS();
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        private void LoadItemDataFromEWS()
        {
            if (_body == null && IsEWS)
            {
                var appointment = Appointment.Bind(_service, new ItemId(Id));
                appointment.Load();
                _body = appointment.Body.Text;
                _optionalAttendees = GetAttendeeString(appointment.OptionalAttendees);
                _requiredAttendees = GetAttendeeString(appointment.RequiredAttendees);
            }
        }

        public string Id { get; set; }

        string GetAttendeeString(Microsoft.Exchange.WebServices.Data.AttendeeCollection collection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var attendee in collection)
            {
                sb.Append(attendee.ToString() + ";");
            }
            return sb.ToString();
        }
    }
}
