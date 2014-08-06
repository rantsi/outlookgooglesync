using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.Interop.Outlook;


namespace OutlookGoogleSync
{
    /// <summary>
    /// Description of OutlookCalendar.
    /// </summary>
    public class OutlookCalendar : IOfficeCalendar
    {
        private static OutlookCalendar instance;

        public static OutlookCalendar Instance
        {
            get
            {
                if (instance == null) instance = new OutlookCalendar();
                return instance;
            }
        }

        public MAPIFolder UseOutlookCalendar;


        public OutlookCalendar()
        {

            // Create the Outlook application.
            Application oApp = new Application();

            // Get the NameSpace and Logon information.
            // Outlook.NameSpace oNS = (Outlook.NameSpace)oApp.GetNamespace("mapi");
            NameSpace oNS = oApp.GetNamespace("mapi");

            //Log on by using a dialog box to choose the profile.
            oNS.Logon("", "", true, true);

            //Alternate logon method that uses a specific profile.
            // If you use this logon method, 
            // change the profile name to an appropriate value.
            //oNS.Logon("YourValidProfile", Missing.Value, false, true); 

            // Get the Calendar folder.
            UseOutlookCalendar = oNS.GetDefaultFolder(OlDefaultFolders.olFolderCalendar);


            //Show the item to pause.
            //oAppt.Display(true);

            // Done. Log off.
            oNS.Logoff();
        }


        public List<OutlookAppointment> GetCalendarEntries()
        {
            Items OutlookItems = UseOutlookCalendar.Items;

            List<OutlookAppointment> result = new List<OutlookAppointment>();

            if (OutlookItems != null)
            {
                foreach (AppointmentItem ai in OutlookItems)
                {
                    result.Add(GetOutlookAppointment(ai));
                }
            }
            return result;
        }

        public List<OutlookAppointment> GetCalendarEntriesInRange()
        {
            List<OutlookAppointment> result = new List<OutlookAppointment>();

            Items OutlookItems = UseOutlookCalendar.Items;
            OutlookItems.Sort("[Start]", Type.Missing);
            OutlookItems.IncludeRecurrences = true;

            if (OutlookItems != null)
            {
                DateTime min = DateTime.Now.AddDays(-Settings.Instance.DaysInThePast);
                DateTime max = DateTime.Now.AddDays(+Settings.Instance.DaysInTheFuture + 1);

                //initial version: did not work in all non-German environments
                //string filter = "[End] >= '" + min.ToString("dd.MM.yyyy HH:mm") + "' AND [Start] < '" + max.ToString("dd.MM.yyyy HH:mm") + "'";

                //proposed by WolverineFan, included here for future reference
                //string filter = "[End] >= '" + min.ToString("dd.MM.yyyy HH:mm") + "' AND [Start] < '" + max.ToString("dd.MM.yyyy HH:mm") + "'";

                //trying this instead, also proposed by WolverineFan, thanks!!! 
                string filter = "[End] >= '" + min.ToString("g") + "' AND [Start] < '" + max.ToString("g") + "'";


                foreach (AppointmentItem ai in OutlookItems.Restrict(filter))
                {
                    result.Add(GetOutlookAppointment(ai));
                }
            }

            return result;
        }

        private OutlookAppointment GetOutlookAppointment(AppointmentItem appointment)
        {
            OutlookAppointment newAppointment = new OutlookAppointment();
            newAppointment.Body = appointment.Body;
            newAppointment.Subject = appointment.Subject;
            newAppointment.Start = appointment.Start;
            newAppointment.End = appointment.End;
            newAppointment.AllDayEvent = appointment.AllDayEvent;
            newAppointment.Location = appointment.Location;
            newAppointment.OptionalAttendees = appointment.OptionalAttendees;
            newAppointment.RequiredAttendees = appointment.RequiredAttendees;
            newAppointment.Organizer = appointment.Organizer.ToString();
            newAppointment.ReminderMinutesBeforeStart = appointment.ReminderMinutesBeforeStart;
            newAppointment.ReminderSet = appointment.ReminderSet;
            return newAppointment;
        }


    }
}
