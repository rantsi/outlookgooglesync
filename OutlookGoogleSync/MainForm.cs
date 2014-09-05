//TODO: consider description updates?
//TODO: optimize comparison algorithms
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.Win32;
using Application = System.Windows.Forms.Application;

namespace OutlookGoogleSync
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public static MainForm Instance;

        public const string FILENAME = "settings.xml";
        public const string VERSION = "1.1.0";

        private Timer _ogstimer;
        private DateTime _oldtime;
        private readonly List<int> _minuteOffsets = new List<int>();

        public MainForm()
        {
            InitializeComponent();
            label4.Text = label4.Text.Replace("{version}", VERSION);

            Instance = this;

            //set system proxy
            //WebProxy wp = (WebProxy)WebRequest.DefaultWebProxy;
            //wp.UseDefaultCredentials = true;
            //System.Net.WebRequest.DefaultWebProxy = wp;

            //load settings/create settings file
            if (File.Exists(FILENAME))
            {
                Settings.Instance = XMLManager.import<Settings>(FILENAME);
            }
            else
            {
                XMLManager.export(Settings.Instance, FILENAME);
            }

            //update GUI from Settings
            tbDaysInThePast.Text = Settings.Instance.DaysInThePast.ToString();
            tbDaysInTheFuture.Text = Settings.Instance.DaysInTheFuture.ToString();
            tbMinuteOffsets.Text = Settings.Instance.MinuteOffsets;
            cbCalendars.Items.Add(Settings.Instance.UseGoogleCalendar);
            cbCalendars.SelectedIndex = 0;
            cbSyncEveryHour.Checked = Settings.Instance.SyncEveryHour;
            cbShowBubbleTooltips.Checked = Settings.Instance.ShowBubbleTooltipWhenSyncing;
            cbStartInTray.Checked = Settings.Instance.StartInTray;
            cbMinimizeToTray.Checked = Settings.Instance.MinimizeToTray;
            cbAddDescription.Checked = Settings.Instance.AddDescription;
            cbAddAttendees.Checked = Settings.Instance.AddAttendeesToDescription;
            cbAddReminders.Checked = Settings.Instance.AddReminders;
            cbCreateFiles.Checked = Settings.Instance.CreateTextFiles;
            txtEWSPass.Text = Settings.Instance.ExchagePassword;
            txtEWSUser.Text = Settings.Instance.ExchageUser;
            txtEWSServerURL.Text = Settings.Instance.ExchageServerAddress;
            chkUseEWS.Checked = Settings.Instance.UseExchange;

            //Start in tray?
            if (cbStartInTray.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.Visible = true;
                this.Hide();
            }

            //set up timer (every 30s) for checking the minute offsets
            _ogstimer = new Timer();
            _ogstimer.Interval = 30000;
            _ogstimer.Tick += new EventHandler(ogstimer_Tick);
            _ogstimer.Start();
            _oldtime = DateTime.Now;

            //set up tooltips for some controls
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
                "If checked, this data is added at the end of the description as text.");
            toolTip1.SetToolTip(cbAddReminders,
                "If checked, the reminder set in outlook will be carried over to the Google Calendar entry (as a popup reminder).");
            toolTip1.SetToolTip(cbCreateFiles,
                "If checked, all entries found in Outlook/Google and identified for creation/deletion will be exported \n" +
                "to 4 separate text files in the application's directory (named \"export_*.txt\"). \n" +
                "Only for debug/diagnostic purposes.");
            toolTip1.SetToolTip(cbAddDescription,
                "The description may contain email addresses, which Outlook may complain about (PopUp-Message: \"Allow Access?\" etc.). \n" +
                "Turning this off allows OutlookGoogleSync to run without intervention in this case.");

            CreateOfficeCalendar();
        }

        private IOfficeCalendar CreateOfficeCalendar()
        {
            IOfficeCalendar calendar;

            if (Settings.Instance.UseExchange)
            {
                calendar = new EwsCalendar();
            }
            else
            {
                calendar = new OutlookCalendar();
            }
            return calendar;
        }

        private void ogstimer_Tick(object sender, EventArgs e)
        {
            if (!cbSyncEveryHour.Checked) return;
            DateTime newtime = DateTime.Now;
            if (newtime.Minute != _oldtime.Minute)
            {
                _oldtime = newtime;
                if (_minuteOffsets.Contains(newtime.Minute))
                {
                    if (cbShowBubbleTooltips.Checked) notifyIcon1.ShowBalloonTip(
                        500,
                        "OutlookGoogleSync",
                        "Sync started at desired minute offset " + newtime.Minute.ToString(),
                        ToolTipIcon.Info
                    );
                    SyncNow_Click(null, null);
                }
            }
        }

        private void GetMyGoogleCalendars_Click(object sender, EventArgs e)
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

        private void SyncNow_Click(object sender, EventArgs e)
        {
            if (Settings.Instance.UseGoogleCalendar.Id == "")
            {
                MessageBox.Show("You need to select a Google Calendar first on the 'Settings' tab.");
                return;
            }

            bSyncNow.Enabled = false;

            LogBox.Clear();

            DateTime syncStarted = DateTime.Now;

            logboxout("Sync started at " + syncStarted.ToString());
            logboxout("--------------------------------------------------");

            logboxout("Reading Outlook Calendar Entries...");
            List<OutlookAppointment> outlookEntries;
            try
            {
                var officeCalendar = CreateOfficeCalendar();
                outlookEntries = officeCalendar.GetCalendarEntriesInRange();
            }
            catch (System.Exception ex)
            {
                bSyncNow.Enabled = true;

                notifyIcon1.ShowBalloonTip(5, "outlook issue", "an error while reading from outlook: " + ex.Message, ToolTipIcon.Error);
                logboxout("an error while reading from outlook: " + ex.Message + "\n" + ex.StackTrace);
                return;
            }
            if (cbCreateFiles.Checked)
            {
                using(TextWriter tw = new StreamWriter("export_found_in_outlook.txt"))
                {
                    foreach (OutlookAppointment ai in outlookEntries)
                    {
                        tw.WriteLine(Signature(ai));
                    }
                    tw.Close();
                }
            }
            logboxout("Found " + outlookEntries.Count + " Outlook Calendar Entries.");
            logboxout("--------------------------------------------------");
            logboxout("Reading Google Calendar Entries...");

            List<Event> GoogleEntries = new List<Event>();
            try
            {
                GoogleEntries = GoogleCalendar.Instance.getCalendarEntriesInRange();
            }
            catch (System.Exception ex)
            {
                bSyncNow.Enabled = true;

                notifyIcon1.ShowBalloonTip(5, "google issue", "an error while reading from google: " + ex.Message, ToolTipIcon.Error);
                logboxout("an error while reading from google: " + ex.Message + "\n" + ex.StackTrace);
                return;
            }

            if (cbCreateFiles.Checked)
            {
                using(TextWriter tw = new StreamWriter("export_found_in_google.txt"))
                {
                    foreach (Event ev in GoogleEntries)
                    {
                        tw.WriteLine(Signature(ev));
                    }
                    tw.Close();
                }
            }
            logboxout("Found " + GoogleEntries.Count + " Google Calendar Entries.");
            logboxout("--------------------------------------------------");


            //  Make copies of each list of events (Not strictly needed)
            List<Event> GoogleEntriesToBeDeleted = new List<Event>(GoogleEntries);
            List<OutlookAppointment> OutlookEntriesToBeCreated = new List<OutlookAppointment>(outlookEntries);
            IdentifyGoogleAddDeletes(OutlookEntriesToBeCreated, GoogleEntriesToBeDeleted);

            //List<Event> GoogleEntriesToBeDeleted = IdentifyGoogleEntriesToBeDeleted(OutlookEntries, GoogleEntries);
            if (cbCreateFiles.Checked)
            {
                using(TextWriter tw = new StreamWriter("export_to_be_deleted.txt"))
                {
                    foreach (Event ev in GoogleEntriesToBeDeleted)
                    {
                        tw.WriteLine(Signature(ev));
                    }
                    tw.Close();
                }
            }
            logboxout(GoogleEntriesToBeDeleted.Count + " Google Calendar Entries to be deleted.");

            //OutlookEntriesToBeCreated ...in Google!
            //List<OutlookAppointment> OutlookEntriesToBeCreated = IdentifyOutlookEntriesToBeCreated(OutlookEntries, GoogleEntries);
            if (cbCreateFiles.Checked)
            {
                using (TextWriter tw = new StreamWriter("export_to_be_created.txt"))
                {
                    foreach (OutlookAppointment ai in OutlookEntriesToBeCreated)
                    {
                        tw.WriteLine(Signature(ai));
                    }
                    tw.Close();
                }
            }
            logboxout(OutlookEntriesToBeCreated.Count + " Entries to be created in Google.");
            logboxout("--------------------------------------------------");


            if (GoogleEntriesToBeDeleted.Count > 0)
            {
                logboxout("Deleting " + GoogleEntriesToBeDeleted.Count + " Google Calendar Entries...");
                foreach (Event ev in GoogleEntriesToBeDeleted) GoogleCalendar.Instance.deleteCalendarEntry(ev);
                logboxout("Done.");
                logboxout("--------------------------------------------------");
            }

            if (OutlookEntriesToBeCreated.Count > 0)
            {
                logboxout("Creating " + OutlookEntriesToBeCreated.Count + " Entries in Google...");
                foreach (OutlookAppointment ai in OutlookEntriesToBeCreated)
                {
                    Event ev = new Event
                    {
                        Start = new EventDateTime(), 
                        End = new EventDateTime()
                    };

                    if (ai.AllDayEvent)
                    {
                        ev.Start.Date = ai.Start.ToString("yyyy-MM-dd");
                        ev.End.Date = ai.End.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        //ev.Start.DateTime = ai.Start;
                        //ev.End.DateTime = ai.End;
                        ev.Start.DateTime = GoogleCalendar.Instance.GoogleTimeFrom(ai.Start);
                        ev.End.DateTime = GoogleCalendar.Instance.GoogleTimeFrom(ai.End);
                    }
                    ev.Summary = ai.Subject;
                    if (cbAddDescription.Checked)
                    {
                        ev.Description = ai.Body;
                    }
                    ev.Location = ai.Location;


                    //consider the reminder set in Outlook
                    if (cbAddReminders.Checked && ai.ReminderSet)
                    {
                        ev.Reminders = new Event.RemindersData();
                        ev.Reminders.UseDefault = false;
                        EventReminder reminder = new EventReminder();
                        reminder.Method = "popup";
                        reminder.Minutes = ai.ReminderMinutesBeforeStart;
                        ev.Reminders.Overrides = new List<EventReminder>();
                        ev.Reminders.Overrides.Add(reminder);
                    }


                    if (cbAddAttendees.Checked)
                    {
                        ev.Description += Environment.NewLine;
                        ev.Description += Environment.NewLine + "==============================================";
                        ev.Description += Environment.NewLine + "Added by OutlookGoogleSync:" + Environment.NewLine;
                        ev.Description += Environment.NewLine + "ORGANIZER: " + Environment.NewLine + ai.Organizer + Environment.NewLine;
                        ev.Description += Environment.NewLine + "REQUIRED: " + Environment.NewLine + splitAttendees(ai.RequiredAttendees) + Environment.NewLine;
                        ev.Description += Environment.NewLine + "OPTIONAL: " + Environment.NewLine + splitAttendees(ai.OptionalAttendees);
                        ev.Description += Environment.NewLine + "==============================================";
                    }

                    GoogleCalendar.Instance.addEntry(ev);
                }
                logboxout("Done.");
                logboxout("--------------------------------------------------");
            }

            DateTime SyncFinished = DateTime.Now;
            TimeSpan Elapsed = SyncFinished - syncStarted;
            logboxout("Sync finished at " + SyncFinished.ToString());
            logboxout("Time needed: " + Elapsed.Minutes + " min " + Elapsed.Seconds + " s");

            bSyncNow.Enabled = true;
        }

        //one attendee per line
        private string splitAttendees(string attendees)
        {
            if (attendees == null) return "";
            string[] tmp1 = attendees.Split(';');
            for (int i = 0; i < tmp1.Length; i++) tmp1[i] = tmp1[i].Trim();
            return String.Join(Environment.NewLine, tmp1);
        }

        // New logic for comparing Outlook and Google events works as follows:
	    //      1.  Scan through both lists looking for duplicates
	    //      2.  Remove found duplicates from both lists
	    //      3.  Items remaining in Outlook list are new and need to be created
	    //      4.  Items remaining in Google list need to be deleted
	    //
        private void IdentifyGoogleAddDeletes(List<OutlookAppointment> outlook, List<Event> google)
        {
            // Count backwards so that we can remove found items without affecting the order of remaining items
            for (int i = outlook.Count - 1; i >= 0; i--)
            {
                for (int j = google.Count - 1; j >= 0; j--)
                {
                    if (String.Compare(Signature(outlook[i]), Signature(google[j])) == 0)
                    {
                        outlook.Remove(outlook[i]);
                        google.Remove(google[j]);
                        break;
                    }
                }
            }
        }

        private List<Event> IdentifyGoogleEntriesToBeDeleted(List<OutlookAppointment> outlook, List<Event> google)
        {
            List<Event> result = new List<Event>();
            foreach (Event g in google)
            {
                bool found = false;
                foreach (OutlookAppointment o in outlook)
                {
                    if (Signature(g) == Signature(o)) found = true;
                }
                if (!found) result.Add(g);
            }
            return result;
        }

        public List<OutlookAppointment> IdentifyOutlookEntriesToBeCreated(List<OutlookAppointment> outlook, List<Event> google)
        {
            List<OutlookAppointment> result = new List<OutlookAppointment>();
            foreach (OutlookAppointment o in outlook)
            {
                bool found = false;
                foreach (Event g in google)
                {
                    if (Signature(g) == Signature(o)) found = true;
                }
                if (!found) result.Add(o);
            }
            return result;
        }

        //creates a standardized summary string with the key attributes of a calendar entry for comparison
        private string Signature(OutlookAppointment ai)
        {
            return (GoogleCalendar.Instance.GoogleTimeFrom(ai.Start) + ";" + GoogleCalendar.Instance.GoogleTimeFrom(ai.End) + ";" + ai.Subject + ";" + ai.Location).Trim();
        }

        private string Signature(Event ev)
        {
            if (ev.Start.DateTime == null)
            {
                //ev.Start.DateTime = DateTime.Parse(ev.Start.Date);
                ev.Start.DateTime = GoogleCalendar.Instance.GoogleTimeFrom(DateTime.Parse(ev.Start.Date));
            }
            if (ev.End.DateTime == null)
            {
                //ev.End.DateTime = DateTime.Parse(ev.End.Date);
                ev.End.DateTime = GoogleCalendar.Instance.GoogleTimeFrom(DateTime.Parse(ev.End.Date));
            }
            return (ev.Start.DateTime + ";" + ev.End.DateTime + ";" + ev.Summary + ";" + ev.Location).Trim();
        }

        private void logboxout(string s)
        {
            LogBox.Text += s + Environment.NewLine;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            XMLManager.export(Settings.Instance, FILENAME);
        }

        private void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Instance.UseGoogleCalendar = (MyCalendarListEntry)cbCalendars.SelectedItem;
        }

        private void TbDaysInThePastTextChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInThePast = int.Parse(tbDaysInThePast.Text);
        }

        private void TbDaysInTheFutureTextChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInTheFuture = int.Parse(tbDaysInTheFuture.Text);
        }

        private void TbMinuteOffsetsTextChanged(object sender, EventArgs e)
        {
            Settings.Instance.MinuteOffsets = tbMinuteOffsets.Text;

            _minuteOffsets.Clear();
            char[] delimiters = { ' ', ',', '.', ':', ';' };
            string[] chunks = tbMinuteOffsets.Text.Split(delimiters);
            foreach (string c in chunks)
            {
                int min = 0;
                int.TryParse(c, out min);
                _minuteOffsets.Add(min);
            }
        }


        private void CbSyncEveryHourCheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.SyncEveryHour = cbSyncEveryHour.Checked;
        }

        private void CbShowBubbleTooltipsCheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.ShowBubbleTooltipWhenSyncing = cbShowBubbleTooltips.Checked;
        }

        private void CbStartInTrayCheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.StartInTray = cbStartInTray.Checked;
        }

        private void CbMinimizeToTrayCheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.MinimizeToTray = cbMinimizeToTray.Checked;
        }

        private void CbAddDescriptionCheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.AddDescription = cbAddDescription.Checked;
        }

        private void CbAddRemindersCheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.AddReminders = cbAddReminders.Checked;
        }

        private void cbAddAttendees_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.AddAttendeesToDescription = cbAddAttendees.Checked;
        }

        private void cbCreateFiles_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.CreateTextFiles = cbCreateFiles.Checked;
        }

        private void NotifyIcon1Click(object sender, EventArgs e)
       {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void MainFormResize(object sender, EventArgs e)
        {
            if (!cbMinimizeToTray.Checked) return;
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                //notifyIcon1.ShowBalloonTip(500, "OutlookGoogleSync", "Click to open again.", ToolTipIcon.Info);
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                notifyIcon1.Visible = false;
            }
        }

        public void HandleException(System.Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            TextWriter tw = new StreamWriter("exception.txt");
            tw.WriteLine(ex.ToString());
            tw.Close();

            this.Close();
            System.Environment.Exit(-1);
            System.Windows.Forms.Application.Exit();
        }



        private void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void chkUseEWS_CheckedChanged(object sender, EventArgs e)
        {
            txtEWSPass.Enabled = chkUseEWS.Checked;
            txtEWSUser.Enabled = chkUseEWS.Checked;
            txtEWSServerURL.Enabled = chkUseEWS.Checked;
            Settings.Instance.UseExchange = chkUseEWS.Checked;
        }

        private void txtEWSUser_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.ExchageUser = txtEWSUser.Text;
        }

        private void txtEWSPass_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.ExchagePassword = txtEWSPass.Text;
        }

        private void txtEWSServerURL_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.ExchageServerAddress = txtEWSServerURL.Text;
        }

        private void checkBoxStartWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as CheckBox;
            
            var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
            
            if (checkBox.Checked)
            {
                // set value in registery
                key.SetValue(Application.ProductName, Application.ExecutablePath);
            }
            else
            {
                // remove value from registery
                key.DeleteValue(Application.ProductName, false);
            }
        }

    }
}
