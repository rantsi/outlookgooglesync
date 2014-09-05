/*
 * Created by SharpDevelop.
 * User: zsianti
 * Date: 14.08.2012
 * Time: 07:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OutlookGoogleSync
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.bSyncNow = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEWSServerURL = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEWSPass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEWSUser = new System.Windows.Forms.TextBox();
            this.chkUseEWS = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbAddReminders = new System.Windows.Forms.CheckBox();
            this.cbAddAttendees = new System.Windows.Forms.CheckBox();
            this.cbAddDescription = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.cbStartInTray = new System.Windows.Forms.CheckBox();
            this.cbCreateFiles = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbShowBubbleTooltips = new System.Windows.Forms.CheckBox();
            this.cbSyncEveryHour = new System.Windows.Forms.CheckBox();
            this.tbMinuteOffsets = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bGetMyCalendars = new System.Windows.Forms.Button();
            this.cbCalendars = new System.Windows.Forms.ComboBox();
            this.bSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbDaysInTheFuture = new System.Windows.Forms.TextBox();
            this.tbDaysInThePast = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBoxStartWithWindows = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.MinimumSize = new System.Drawing.Size(486, 514);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 542);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LogBox);
            this.tabPage1.Controls.Add(this.bSyncNow);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(504, 516);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sync";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LogBox
            // 
            this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogBox.Location = new System.Drawing.Point(8, 6);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.Size = new System.Drawing.Size(466, 439);
            this.LogBox.TabIndex = 1;
            // 
            // bSyncNow
            // 
            this.bSyncNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bSyncNow.Location = new System.Drawing.Point(8, 451);
            this.bSyncNow.Name = "bSyncNow";
            this.bSyncNow.Size = new System.Drawing.Size(98, 31);
            this.bSyncNow.TabIndex = 0;
            this.bSyncNow.Text = "Sync now";
            this.bSyncNow.UseVisualStyleBackColor = true;
            this.bSyncNow.Click += new System.EventHandler(this.SyncNow_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.bSave);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(504, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.txtEWSServerURL);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.txtEWSPass);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.txtEWSUser);
            this.groupBox6.Controls.Add(this.chkUseEWS);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Location = new System.Drawing.Point(212, 171);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(282, 163);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "EWS Settings";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(67, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(187, 23);
            this.label8.TabIndex = 14;
            this.label8.Text = "(leave blank to autodiscover, slower)";
            // 
            // txtEWSServerURL
            // 
            this.txtEWSServerURL.Enabled = false;
            this.txtEWSServerURL.Location = new System.Drawing.Point(70, 107);
            this.txtEWSServerURL.Name = "txtEWSServerURL";
            this.txtEWSServerURL.Size = new System.Drawing.Size(184, 20);
            this.txtEWSServerURL.TabIndex = 13;
            this.txtEWSServerURL.TextChanged += new System.EventHandler(this.txtEWSServerURL_TextChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "Server:";
            // 
            // txtEWSPass
            // 
            this.txtEWSPass.Enabled = false;
            this.txtEWSPass.Location = new System.Drawing.Point(70, 81);
            this.txtEWSPass.Name = "txtEWSPass";
            this.txtEWSPass.PasswordChar = '*';
            this.txtEWSPass.Size = new System.Drawing.Size(184, 20);
            this.txtEWSPass.TabIndex = 11;
            this.txtEWSPass.TextChanged += new System.EventHandler(this.txtEWSPass_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Password:";
            // 
            // txtEWSUser
            // 
            this.txtEWSUser.Enabled = false;
            this.txtEWSUser.Location = new System.Drawing.Point(70, 51);
            this.txtEWSUser.Name = "txtEWSUser";
            this.txtEWSUser.Size = new System.Drawing.Size(184, 20);
            this.txtEWSUser.TabIndex = 9;
            this.txtEWSUser.TextChanged += new System.EventHandler(this.txtEWSUser_TextChanged);
            // 
            // chkUseEWS
            // 
            this.chkUseEWS.Location = new System.Drawing.Point(6, 19);
            this.chkUseEWS.Name = "chkUseEWS";
            this.chkUseEWS.Size = new System.Drawing.Size(139, 24);
            this.chkUseEWS.TabIndex = 8;
            this.chkUseEWS.Text = "Use Exchange";
            this.chkUseEWS.UseVisualStyleBackColor = true;
            this.chkUseEWS.CheckedChanged += new System.EventHandler(this.chkUseEWS_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Email:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbAddReminders);
            this.groupBox5.Controls.Add(this.cbAddAttendees);
            this.groupBox5.Controls.Add(this.cbAddDescription);
            this.groupBox5.Location = new System.Drawing.Point(6, 171);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(192, 163);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Google Calendar Entries Settings";
            // 
            // cbAddReminders
            // 
            this.cbAddReminders.Location = new System.Drawing.Point(12, 79);
            this.cbAddReminders.Name = "cbAddReminders";
            this.cbAddReminders.Size = new System.Drawing.Size(139, 24);
            this.cbAddReminders.TabIndex = 8;
            this.cbAddReminders.Text = "Add Reminders";
            this.cbAddReminders.UseVisualStyleBackColor = true;
            this.cbAddReminders.CheckedChanged += new System.EventHandler(this.CbAddRemindersCheckedChanged);
            // 
            // cbAddAttendees
            // 
            this.cbAddAttendees.Location = new System.Drawing.Point(12, 49);
            this.cbAddAttendees.Name = "cbAddAttendees";
            this.cbAddAttendees.Size = new System.Drawing.Size(184, 24);
            this.cbAddAttendees.TabIndex = 6;
            this.cbAddAttendees.Text = "Append attendees to Description";
            this.cbAddAttendees.UseVisualStyleBackColor = true;
            this.cbAddAttendees.CheckedChanged += new System.EventHandler(this.cbAddAttendees_CheckedChanged);
            // 
            // cbAddDescription
            // 
            this.cbAddDescription.Location = new System.Drawing.Point(12, 19);
            this.cbAddDescription.Name = "cbAddDescription";
            this.cbAddDescription.Size = new System.Drawing.Size(139, 24);
            this.cbAddDescription.TabIndex = 7;
            this.cbAddDescription.Text = "Add Description";
            this.cbAddDescription.UseVisualStyleBackColor = true;
            this.cbAddDescription.CheckedChanged += new System.EventHandler(this.CbAddDescriptionCheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.checkBoxStartWithWindows);
            this.groupBox4.Controls.Add(this.cbMinimizeToTray);
            this.groupBox4.Controls.Add(this.cbStartInTray);
            this.groupBox4.Controls.Add(this.cbCreateFiles);
            this.groupBox4.Location = new System.Drawing.Point(6, 340);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(488, 133);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Options";
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.Location = new System.Drawing.Point(12, 49);
            this.cbMinimizeToTray.Name = "cbMinimizeToTray";
            this.cbMinimizeToTray.Size = new System.Drawing.Size(104, 24);
            this.cbMinimizeToTray.TabIndex = 0;
            this.cbMinimizeToTray.Text = "Minimize to Tray";
            this.cbMinimizeToTray.UseVisualStyleBackColor = true;
            this.cbMinimizeToTray.CheckedChanged += new System.EventHandler(this.CbMinimizeToTrayCheckedChanged);
            // 
            // cbStartInTray
            // 
            this.cbStartInTray.Location = new System.Drawing.Point(12, 19);
            this.cbStartInTray.Name = "cbStartInTray";
            this.cbStartInTray.Size = new System.Drawing.Size(104, 24);
            this.cbStartInTray.TabIndex = 1;
            this.cbStartInTray.Text = "Start in Tray";
            this.cbStartInTray.UseVisualStyleBackColor = true;
            this.cbStartInTray.CheckedChanged += new System.EventHandler(this.CbStartInTrayCheckedChanged);
            // 
            // cbCreateFiles
            // 
            this.cbCreateFiles.Location = new System.Drawing.Point(12, 79);
            this.cbCreateFiles.Name = "cbCreateFiles";
            this.cbCreateFiles.Size = new System.Drawing.Size(235, 24);
            this.cbCreateFiles.TabIndex = 7;
            this.cbCreateFiles.Text = "Create text files with found/identified entries";
            this.cbCreateFiles.UseVisualStyleBackColor = true;
            this.cbCreateFiles.CheckedChanged += new System.EventHandler(this.cbCreateFiles_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cbShowBubbleTooltips);
            this.groupBox3.Controls.Add(this.cbSyncEveryHour);
            this.groupBox3.Controls.Add(this.tbMinuteOffsets);
            this.groupBox3.Location = new System.Drawing.Point(177, 80);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(317, 85);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sync Regularly";
            // 
            // cbShowBubbleTooltips
            // 
            this.cbShowBubbleTooltips.Location = new System.Drawing.Point(6, 49);
            this.cbShowBubbleTooltips.Name = "cbShowBubbleTooltips";
            this.cbShowBubbleTooltips.Size = new System.Drawing.Size(259, 24);
            this.cbShowBubbleTooltips.TabIndex = 7;
            this.cbShowBubbleTooltips.Text = "Show Bubble Tooltip in Taskbar when Syncing";
            this.cbShowBubbleTooltips.UseVisualStyleBackColor = true;
            this.cbShowBubbleTooltips.CheckedChanged += new System.EventHandler(this.CbShowBubbleTooltipsCheckedChanged);
            // 
            // cbSyncEveryHour
            // 
            this.cbSyncEveryHour.Location = new System.Drawing.Point(6, 19);
            this.cbSyncEveryHour.Name = "cbSyncEveryHour";
            this.cbSyncEveryHour.Size = new System.Drawing.Size(221, 24);
            this.cbSyncEveryHour.TabIndex = 6;
            this.cbSyncEveryHour.Text = "Sync every hour at these Minute Offset(s)";
            this.cbSyncEveryHour.UseVisualStyleBackColor = true;
            this.cbSyncEveryHour.CheckedChanged += new System.EventHandler(this.CbSyncEveryHourCheckedChanged);
            // 
            // tbMinuteOffsets
            // 
            this.tbMinuteOffsets.Location = new System.Drawing.Point(231, 21);
            this.tbMinuteOffsets.Name = "tbMinuteOffsets";
            this.tbMinuteOffsets.Size = new System.Drawing.Size(35, 20);
            this.tbMinuteOffsets.TabIndex = 5;
            this.tbMinuteOffsets.TextChanged += new System.EventHandler(this.TbMinuteOffsetsTextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.bGetMyCalendars);
            this.groupBox2.Controls.Add(this.cbCalendars);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(488, 68);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Google Calendar";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Use Google Calendar:";
            // 
            // bGetMyCalendars
            // 
            this.bGetMyCalendars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bGetMyCalendars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bGetMyCalendars.Location = new System.Drawing.Point(388, 30);
            this.bGetMyCalendars.Name = "bGetMyCalendars";
            this.bGetMyCalendars.Size = new System.Drawing.Size(94, 21);
            this.bGetMyCalendars.TabIndex = 2;
            this.bGetMyCalendars.Text = "Get Google Cals";
            this.bGetMyCalendars.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.bGetMyCalendars.UseVisualStyleBackColor = true;
            this.bGetMyCalendars.Click += new System.EventHandler(this.GetMyGoogleCalendars_Click);
            // 
            // cbCalendars
            // 
            this.cbCalendars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCalendars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalendars.FormattingEnabled = true;
            this.cbCalendars.Location = new System.Drawing.Point(124, 30);
            this.cbCalendars.Name = "cbCalendars";
            this.cbCalendars.Size = new System.Drawing.Size(258, 21);
            this.cbCalendars.TabIndex = 1;
            this.cbCalendars.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bSave.Location = new System.Drawing.Point(6, 479);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 31);
            this.bSave.TabIndex = 8;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbDaysInTheFuture);
            this.groupBox1.Controls.Add(this.tbDaysInThePast);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sync Date Range";
            // 
            // tbDaysInTheFuture
            // 
            this.tbDaysInTheFuture.Location = new System.Drawing.Point(112, 51);
            this.tbDaysInTheFuture.Name = "tbDaysInTheFuture";
            this.tbDaysInTheFuture.Size = new System.Drawing.Size(39, 20);
            this.tbDaysInTheFuture.TabIndex = 4;
            this.tbDaysInTheFuture.TextChanged += new System.EventHandler(this.TbDaysInTheFutureTextChanged);
            // 
            // tbDaysInThePast
            // 
            this.tbDaysInThePast.Location = new System.Drawing.Point(112, 21);
            this.tbDaysInThePast.Name = "tbDaysInThePast";
            this.tbDaysInThePast.Size = new System.Drawing.Size(39, 20);
            this.tbDaysInThePast.TabIndex = 3;
            this.tbDaysInThePast.TextChanged += new System.EventHandler(this.TbDaysInThePastTextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Days in the Future";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Days in the Past";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.linkLabel2);
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(504, 516);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "About";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.Location = new System.Drawing.Point(6, 222);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(475, 23);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "https://github.com/rantsi/outlookgooglesync";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(3, 199);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(475, 23);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://outlookgooglesync.codeplex.com/";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(481, 151);
            this.label4.TabIndex = 1;
            this.label4.Text = "OutlookGoogleSync\r\n\r\nVersion {version}\r\n\r\ninitial version programmed 2012-2013 by" +
    "\r\nZissis Siantidis\r\n\r\nFork by rantsi 2014\r\n";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "OutlookGoogleSync";
            this.notifyIcon1.Click += new System.EventHandler(this.NotifyIcon1Click);
            // 
            // checkBoxStartWithWindows
            // 
            this.checkBoxStartWithWindows.Location = new System.Drawing.Point(212, 19);
            this.checkBoxStartWithWindows.Name = "checkBoxStartWithWindows";
            this.checkBoxStartWithWindows.Size = new System.Drawing.Size(153, 24);
            this.checkBoxStartWithWindows.TabIndex = 8;
            this.checkBoxStartWithWindows.Text = "Start with windows";
            this.checkBoxStartWithWindows.UseVisualStyleBackColor = true;
            this.checkBoxStartWithWindows.CheckedChanged += new System.EventHandler(this.checkBoxStartWithWindows_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(512, 542);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(506, 552);
            this.Name = "MainForm";
            this.Text = "OutlookGoogleSync";
            this.Resize += new System.EventHandler(this.MainFormResize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.CheckBox cbAddReminders;
		private System.Windows.Forms.CheckBox cbAddDescription;
		private System.Windows.Forms.CheckBox cbShowBubbleTooltips;
		private System.Windows.Forms.CheckBox cbSyncEveryHour;
		private System.Windows.Forms.CheckBox cbMinimizeToTray;
		private System.Windows.Forms.CheckBox cbStartInTray;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox tbMinuteOffsets;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox cbAddAttendees;
		private System.Windows.Forms.CheckBox cbCreateFiles;
		private System.Windows.Forms.TextBox LogBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.ComboBox cbCalendars;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbDaysInThePast;
		private System.Windows.Forms.TextBox tbDaysInTheFuture;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.Button bGetMyCalendars;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button bSyncNow;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtEWSPass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEWSUser;
        private System.Windows.Forms.CheckBox chkUseEWS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEWSServerURL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxStartWithWindows;
		
	



		

		

	}
}
