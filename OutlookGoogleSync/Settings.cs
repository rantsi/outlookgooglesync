﻿
using System;
using System.Drawing;

namespace OutlookGoogleSync
{
    /// <summary>
    /// Description of Settings.
    /// </summary>
    public class Settings
    {
        private static Settings instance;

        public static Settings Instance
        {
            get 
            {
                if (instance == null) instance = new Settings();
                return instance;
            }
            set
            {
                instance = value;            
            }
          
        }
        
        
        public string RefreshToken = "";
        public string MinuteOffsets = "";
        public int DaysInThePast = 1;
        public int DaysInTheFuture = 60;
        public MyCalendarListEntry UseGoogleCalendar = new MyCalendarListEntry();

        public bool SyncEveryHour = false;
        public bool ShowBubbleTooltipWhenSyncing = false;
        public bool StartInTray = false;
        public bool MinimizeToTray = false;
        
        public bool AddDescription = true;
        public bool AddAttendeesToDescription = true;
        public bool CreateTextFiles = true;

        

        public Settings()
        {

        }
    }
}
