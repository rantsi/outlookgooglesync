﻿using System;
using System.Collections.Generic;
//using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Outlook;


namespace OutlookGoogleSync
{
    /// <summary>
    /// Description of OutlookCalendar.
    /// </summary>
    public class OutlookCalendar
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
            oNS.Logon("","", true, true);

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
        
        
        public List<AppointmentItem> getCalendarEntries()
        {
            Items OutlookItems = UseOutlookCalendar.Items;
            if (OutlookItems != null)
            {
                List<AppointmentItem> result = new List<AppointmentItem>();
                foreach (AppointmentItem ai in OutlookItems)
                {
                    result.Add(ai);
                }
                return result;
            }
            return null;
        }
        
        public List<AppointmentItem> getCalendarEntriesInRange()
        {
            List<AppointmentItem> result = new List<AppointmentItem>();
            
            Items OutlookItems = UseOutlookCalendar.Items;
            OutlookItems.Sort("[Start]",Type.Missing);
            OutlookItems.IncludeRecurrences = true;
            
            if (OutlookItems != null)
            {
                DateTime min = DateTime.Now.AddDays(-Settings.Instance.DaysInThePast);
                DateTime max = DateTime.Now.AddDays(+Settings.Instance.DaysInTheFuture+1);

                string filter = "[End] >= '" + min.ToString("dd.MM.yyyy HH:mm") + "' AND [Start] < '" + max.ToString("dd.MM.yyyy HH:mm") + "'";
                foreach(AppointmentItem ai in OutlookItems.Restrict(filter))
                {
                    result.Add(ai);
                }
            }
            return result;
        }
        
        
        
    }
}
