/*
 * Created by SharpDevelop.
 * User: zsianti
 * Date: 14.08.2012
 * Time: 14:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;


namespace OutlookGoogleSync
{
    /// <summary>
    /// Description of MyCalendarListEntry.
    /// </summary>
    public class MyCalendarListEntry
    {
        public string Id = "";
        public string Name = "";
        
        
        public MyCalendarListEntry()
        {
        }
        
        public MyCalendarListEntry(CalendarListEntry init)
        {
            Id = init.Id;
            Name = init.Summary;
        }
        
        public override string ToString()
		{
			return Name;
		}

        
    }
}
