﻿//TODO: consider description updates?
//TODO: optimize comparison algorithms

using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace OutlookGoogleSync
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public static MainForm Instance;
        
        public const string FILENAME = "settings.xml";
        
        public Timer ogstimer;
        public DateTime oldtime;
        public List<int> MinuteOffsets = new List<int>();
        
        public MainForm()
        {
            InitializeComponent();
            
            label4.Text = label4.Text.Replace("{version}", "1.0.4");
            
            Instance = this;
            
            
            
            if (File.Exists(FILENAME))
            {
                Settings.Instance = XMLManager.import<Settings>(FILENAME);    
            } else {
                XMLManager.export(Settings.Instance, FILENAME);
            }
            
            tbDaysInThePast.Text = Settings.Instance.DaysInThePast.ToString();
            tbDaysInTheFuture.Text = Settings.Instance.DaysInTheFuture.ToString();
            tbMinuteOffsets.Text = Settings.Instance.MinuteOffsets;
            cbCalendars.Items.Add(Settings.Instance.UseGoogleCalendar);
            cbCalendars.SelectedIndex = 0;
            cbAddAttendees.Checked = Settings.Instance.AddAttendeesToDescription;
            cbCreateFiles.Checked = Settings.Instance.CreateTextFiles;
            
            
            ogstimer = new Timer();
            ogstimer.Interval = 10000;
            ogstimer.Tick += new EventHandler(ogstimer_Tick);
            ogstimer.Start();
            oldtime = DateTime.Now;
            
            
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(cbCalendars, 
                "The Google Calendar to synchonize with.");
            toolTip1.SetToolTip(tbMinuteOffsets, 
                "One ore more Minute Offsets at which the sync is automatically started each hour. \n" +
                "Separate by comma (e.g. 5,15,25).");
            toolTip1.SetToolTip(cbAddAttendees, 
                "While Outlook has fields for Organizer, RequiredAttendees and OptionalAttendees, Google has not.\n" +
                "If checked, this data is added at the end of the description when creating an event in Google.");
            toolTip1.SetToolTip(cbCreateFiles, 
                "If checked, all entries found in Outlook/Google and identified for creation/deletion will be exported \n" +
                "to 4 separate text files in the application's directory (named \"export_*.txt\"). \n" +
                "Only for debug/diagnostic purpose.");
            
        }

        void ogstimer_Tick(object sender, EventArgs e)
        {
            DateTime newtime = DateTime.Now;
            if (newtime.Minute != oldtime.Minute)
            {
                oldtime = newtime;
                if (MinuteOffsets.Contains(newtime.Minute)) 
                {
                    notifyIcon1.ShowBalloonTip(
                        500, 
                        "OutlookGoogleSync", 
                        "Sync started at desired minute offset " + newtime.Minute.ToString(),
                        ToolTipIcon.Info
                    );
                    SyncNow_Click(null, null);
                }
            }
        }
        
        void GetMyGoogleCalendars_Click(object sender, EventArgs e)
        {
            bGetMyCalendars.Enabled = false;
            cbCalendars.Enabled = false;
            
            List<MyCalendarListEntry> calendars = GoogleCalendar.Instance.getCalendars();
            if (calendars != null)
            {
                cbCalendars.Items.Clear();
                foreach (MyCalendarListEntry mcle in calendars)
                {
                  cbCalendars.Items.Add(mcle);
                }
                MainForm.Instance.cbCalendars.SelectedIndex = 0;
            }
            
            bGetMyCalendars.Enabled = true;
            cbCalendars.Enabled = true;
        }
        
        void SyncNow_Click(object sender, EventArgs e)
        {
            if (Settings.Instance.UseGoogleCalendar.Id == "")
            {
                MessageBox.Show("You need to select a Google Calendar first on the 'Settings' tab.");
                return;
            }
        
            bSyncNow.Enabled = false;
            
            LogBox.Clear();
            
            DateTime SyncStarted = DateTime.Now;
            
            logboxout("Sync started at " + SyncStarted.ToString());
            logboxout("--------------------------------------------------");
            
            logboxout("Reading Outlook Calendar Entries...");
            List<AppointmentItem> OutlookEntries = OutlookCalendar.Instance.getCalendarEntriesInRange();
            if (cbCreateFiles.Checked)
            {
                TextWriter tw = new StreamWriter("export_found_in_outlook.txt");
                foreach(AppointmentItem ai in OutlookEntries)
                {
                    tw.WriteLine(signature(ai));
                }
                tw.Close();            
            }
            logboxout("Found " + OutlookEntries.Count + " Outlook Calendar Entries.");
            logboxout("--------------------------------------------------");
            
            
            
            logboxout("Reading Google Calendar Entries...");
            List<Event> GoogleEntries = GoogleCalendar.Instance.getCalendarEntriesInRange();
            if (cbCreateFiles.Checked)
            {
                TextWriter tw = new StreamWriter("export_found_in_google.txt");
                foreach(Event ev in GoogleEntries)
                {
                    tw.WriteLine(signature(ev));
                }
                tw.Close();
            }
            logboxout("Found " + GoogleEntries.Count + " Google Calendar Entries.");
            logboxout("--------------------------------------------------");
            
            
            List<Event> GoogleEntriesToBeDeleted = IdentifyGoogleEntriesToBeDeleted(OutlookEntries, GoogleEntries);
            if (cbCreateFiles.Checked)
            {
                TextWriter tw = new StreamWriter("export_to_be_deleted.txt");
                foreach(Event ev in GoogleEntriesToBeDeleted)
                {
                    tw.WriteLine(signature(ev));
                }
                tw.Close();
            }
            logboxout(GoogleEntriesToBeDeleted.Count + " Google Calendar Entries to be deleted.");


            List<AppointmentItem> OutlookEntriesToBeCreated = IdentifyOutlookEntriesToBeCreated(OutlookEntries, GoogleEntries);
            if (cbCreateFiles.Checked)
            {
                TextWriter tw = new StreamWriter("export_to_be_created.txt");
                foreach(AppointmentItem ai in OutlookEntriesToBeCreated)
                {
                    tw.WriteLine(signature(ai));
                }
                tw.Close();
            }
            logboxout(OutlookEntriesToBeCreated.Count + " Entries to be created in Google.");
            logboxout("--------------------------------------------------");
            
            
            if (GoogleEntriesToBeDeleted.Count>0)
            {
                logboxout("Deleting " + GoogleEntriesToBeDeleted.Count + " Google Calendar Entries...");
                foreach(Event ev in GoogleEntriesToBeDeleted) GoogleCalendar.Instance.deleteCalendarEntry(ev);
                logboxout("Done.");
                logboxout("--------------------------------------------------");
            }

            if (OutlookEntriesToBeCreated.Count>0)
            {
                logboxout("Creating " + OutlookEntriesToBeCreated.Count + " Entries in Google...");
                foreach(AppointmentItem ai in OutlookEntriesToBeCreated)
                {
                    Event ev = new Event();
                    
                    ev.Start = new EventDateTime();
                    ev.End = new EventDateTime();
                    
                    if (ai.AllDayEvent)
                    {
                        ev.Start.Date = ai.Start.ToString("yyyy-MM-dd");
                        ev.End.Date = ai.End.ToString("yyyy-MM-dd");
                    } else {
                        ev.Start.DateTime = GoogleCalendar.Instance.GoogleTimeFrom(ai.Start);
                        ev.End.DateTime = GoogleCalendar.Instance.GoogleTimeFrom(ai.End);
                    }
                    ev.Summary = ai.Subject;
                    ev.Description = ai.Body;
                    ev.Location = ai.Location;
                    
                    if (cbAddAttendees.Checked)
                    {
                        ev.Description += Environment.NewLine + "----------------------------------------";
                        ev.Description += Environment.NewLine + "Added by OutlookGoogleSync:" + Environment.NewLine;
                        ev.Description += Environment.NewLine + "ORGANIZER: " + ai.Organizer + Environment.NewLine;
                        ev.Description += Environment.NewLine + "REQUIRED: " + ai.RequiredAttendees + Environment.NewLine;
                        ev.Description += Environment.NewLine + "OPTIONAL: " + ai.OptionalAttendees;
                    }
                    
                    GoogleCalendar.Instance.addEntry(ev);
                }
                logboxout("Done.");
                logboxout("--------------------------------------------------");
            }

            DateTime SyncFinished = DateTime.Now;
            TimeSpan Elapsed = SyncFinished - SyncStarted;
            logboxout("Sync finished at " + SyncFinished.ToString());
            logboxout("Time needed: " + Elapsed.Minutes + " min " + Elapsed.Seconds + " s");
            
            bSyncNow.Enabled = true;
        }
        
        
        public List<Event> IdentifyGoogleEntriesToBeDeleted(List<AppointmentItem> outlook, List<Event> google)
        {
            List<Event> result = new List<Event>();
            foreach(Event g in google)
            {
                bool found = false;
                foreach(AppointmentItem o in outlook)
                {
                    if (signature(g) == signature(o)) found = true;
                }
                if (!found) result.Add(g);
            }
            return result;
        }
        
        public List<AppointmentItem> IdentifyOutlookEntriesToBeCreated(List<AppointmentItem> outlook, List<Event> google)
        {
            List<AppointmentItem> result = new List<AppointmentItem>();
            foreach(AppointmentItem o in outlook)
            {
                bool found = false;
                foreach(Event g in google)
                {
                    if (signature(g) == signature(o)) found = true;
                }
                if (!found) result.Add(o);
            }
            return result;
        }
        
        public string signature(AppointmentItem ai)
        {
            //return (ai.Start.GetDateTimeFormats('s')[0] + "+02:00" + ";" + ai.End.GetDateTimeFormats('s')[0] + "+02:00" + ";" + ai.Subject).Trim();
            return (GoogleCalendar.Instance.GoogleTimeFrom(ai.Start) + ";" + GoogleCalendar.Instance.GoogleTimeFrom(ai.End) + ";" + ai.Subject).Trim();
        }
        public string signature(Event ev)
        {
            if (ev.Start.DateTime==null) ev.Start.DateTime = GoogleCalendar.Instance.GoogleTimeFrom(DateTime.Parse(ev.Start.Date));
            if (ev.End.DateTime==null) ev.End.DateTime = GoogleCalendar.Instance.GoogleTimeFrom(DateTime.Parse(ev.End.Date));
            return (ev.Start.DateTime + ";" + ev.End.DateTime + ";" + ev.Summary).Trim();
        }
        
        void logboxout(string s)
        {
          LogBox.Text += s + Environment.NewLine;
        }
        
        void Save_Click(object sender, EventArgs e)
        {
            XMLManager.export(Settings.Instance, FILENAME);
        }
        
        void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Instance.UseGoogleCalendar = (MyCalendarListEntry) cbCalendars.SelectedItem;
        }
        
        void TbDaysInThePastTextChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInThePast = int.Parse(tbDaysInThePast.Text);
        }
        
        void TbDaysInTheFutureTextChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInTheFuture = int.Parse(tbDaysInTheFuture.Text);
        }

        void TbMinuteOffsetsTextChanged(object sender, EventArgs e)
		{
		    Settings.Instance.MinuteOffsets = tbMinuteOffsets.Text;
		    
		    MinuteOffsets.Clear();
	        char[] delimiters = { ' ', ',', '.', ':', ';' };
            string[] chunks = tbMinuteOffsets.Text.Split(delimiters);
            foreach (string c in chunks)
            {
                int min = 0;
                int.TryParse(c, out min);
                MinuteOffsets.Add(min);
            }
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
		    Settings.Instance.AddAttendeesToDescription = cbAddAttendees.Checked;
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
		    Settings.Instance.CreateTextFiles = cbCreateFiles.Checked;
		}
		
		void NotifyIcon1MouseDoubleClick(object sender, MouseEventArgs e)
		{
		    this.Show();
		    this.WindowState = FormWindowState.Normal;
		}
		
		void MainFormResize(object sender, EventArgs e)
		{
             notifyIcon1.BalloonTipTitle = "OutlookGoogleSync";
             notifyIcon1.BalloonTipText = "Double Click to open again.";
        
             if (FormWindowState.Minimized == this.WindowState)
             {
                  notifyIcon1.Visible = true;
                  notifyIcon1.ShowBalloonTip(500);
                  this.Hide();    
             }
             else if (FormWindowState.Normal == this.WindowState)
             {
                  notifyIcon1.Visible = false;
             }

        }
        
		public void HandleException(System.Exception ex)
		{
		    MessageBox.Show(ex.ToString(), "Exception!", MessageBoxButtons.OK,MessageBoxIcon.Error);
            TextWriter tw = new StreamWriter("exception.txt");
            tw.WriteLine(ex.ToString());
            tw.Close();
            
            this.Close();
            System.Environment.Exit(-1);
            System.Windows.Forms.Application.Exit();
		}
		

		
		void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(linkLabel1.Text);			
		}
    }
}
