﻿
namespace CountDownTimerV0
{
	partial class DigitalCountTimer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DigitalCountTimer));
			this.timerDisplay = new System.Windows.Forms.Label();
			this.startButton = new System.Windows.Forms.Button();
			this.stopButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.countInverseBtn = new System.Windows.Forms.Button();
			this.timerAddBtn = new System.Windows.Forms.Button();
			this.muteBtn = new System.Windows.Forms.CheckBox();
			this.clearTimersListBtn = new System.Windows.Forms.Button();
			this.selectTimerLabel1 = new System.Windows.Forms.Label();
			this.selectTimerLabel2 = new System.Windows.Forms.Label();
			this.navigateUpBtn = new System.Windows.Forms.Button();
			this.loadProfileBtn = new System.Windows.Forms.Button();
			this.saveProfileBtn = new System.Windows.Forms.Button();
			this.saveProfile = new System.Windows.Forms.SaveFileDialog();
			this.loadProfile = new System.Windows.Forms.OpenFileDialog();
			this.timerSelectorPanel = new System.Windows.Forms.Panel();
			this.navigateDwnBtn = new System.Windows.Forms.Button();
			this.timersLabel = new System.Windows.Forms.Label();
			this.timerNameLabel = new System.Windows.Forms.Label();
			this.timerDurationLabel = new System.Windows.Forms.Label();
			this.timerDurationEntry = new System.Windows.Forms.TextBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.timerNamesList = new System.Windows.Forms.ListBox();
			this.timerDurationsList = new System.Windows.Forms.ListBox();
			this.timerNameEntry = new System.Windows.Forms.TextBox();
			this.chooseAudioBtn = new System.Windows.Forms.Button();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.chosenAudioLabel = new System.Windows.Forms.Label();
			this.selectedAudioName = new System.Windows.Forms.Label();
			this.countTimer = new System.Windows.Forms.Timer(this.components);
			this.timerNamesListLabel = new System.Windows.Forms.Label();
			this.timerDurationsListLabel = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.audioFileSelector = new System.Windows.Forms.OpenFileDialog();
			this.activeTimerLabel = new System.Windows.Forms.Label();
			this.activeTimerTextBox = new System.Windows.Forms.TextBox();
			this.hoursLabel = new System.Windows.Forms.Label();
			this.minutesLabel = new System.Windows.Forms.Label();
			this.secondsLabel = new System.Windows.Forms.Label();
			this.saveLapsesCheckBox = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.loadProfileDiaglog = new System.Windows.Forms.OpenFileDialog();
			this.saveProfileDialog = new System.Windows.Forms.SaveFileDialog();
			this.toolTips = new System.Windows.Forms.ToolTip(this.components);
			this.removeTimerBtn = new System.Windows.Forms.Button();
			this.currentProfileTextBox = new System.Windows.Forms.TextBox();
			this.currentProfileLabel = new System.Windows.Forms.Label();
			this.continuousModeBtn = new System.Windows.Forms.CheckBox();
			this.timeBeforeNextTimer = new System.Windows.Forms.Timer(this.components);
			this.timerSelectorPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// timerDisplay
			// 
			this.timerDisplay.BackColor = System.Drawing.Color.Black;
			this.timerDisplay.Font = new System.Drawing.Font("Bodoni MT Condensed", 123.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDisplay.ForeColor = System.Drawing.Color.Lime;
			this.timerDisplay.Location = new System.Drawing.Point(5, 31);
			this.timerDisplay.Name = "timerDisplay";
			this.timerDisplay.Size = new System.Drawing.Size(527, 179);
			this.timerDisplay.TabIndex = 0;
			this.timerDisplay.Text = "00:00:00";
			this.timerDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// startButton
			// 
			this.startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.startButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.startButton.FlatAppearance.BorderColor = System.Drawing.Color.Green;
			this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.startButton.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.startButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
			this.startButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.startButton.Location = new System.Drawing.Point(5, 213);
			this.startButton.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.startButton.Name = "startButton";
			this.startButton.Padding = new System.Windows.Forms.Padding(32, 0, 26, 0);
			this.startButton.Size = new System.Drawing.Size(174, 74);
			this.startButton.TabIndex = 1;
			this.startButton.Text = "START";
			this.startButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.startButton.UseVisualStyleBackColor = false;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			this.startButton.MouseHover += new System.EventHandler(this.startButton_MouseHover);
			// 
			// stopButton
			// 
			this.stopButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.stopButton.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
			this.stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.stopButton.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.stopButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
			this.stopButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.stopButton.Location = new System.Drawing.Point(181, 213);
			this.stopButton.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.stopButton.Name = "stopButton";
			this.stopButton.Padding = new System.Windows.Forms.Padding(36, 0, 28, 0);
			this.stopButton.Size = new System.Drawing.Size(174, 74);
			this.stopButton.TabIndex = 2;
			this.stopButton.Text = "STOP";
			this.stopButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.stopButton.UseVisualStyleBackColor = false;
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			this.stopButton.MouseHover += new System.EventHandler(this.stopButton_MouseHover);
			// 
			// resetButton
			// 
			this.resetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.resetButton.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
			this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.resetButton.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.resetButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.resetButton.Image = ((System.Drawing.Image)(resources.GetObject("resetButton.Image")));
			this.resetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.resetButton.Location = new System.Drawing.Point(358, 213);
			this.resetButton.Name = "resetButton";
			this.resetButton.Padding = new System.Windows.Forms.Padding(36, 0, 28, 0);
			this.resetButton.Size = new System.Drawing.Size(174, 74);
			this.resetButton.TabIndex = 3;
			this.resetButton.Text = "RESET";
			this.resetButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.resetButton.UseVisualStyleBackColor = false;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			this.resetButton.MouseHover += new System.EventHandler(this.resetButton_MouseHover);
			// 
			// countInverseBtn
			// 
			this.countInverseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.countInverseBtn.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
			this.countInverseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.countInverseBtn.Font = new System.Drawing.Font("Lucida Sans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.countInverseBtn.ForeColor = System.Drawing.Color.White;
			this.countInverseBtn.Image = ((System.Drawing.Image)(resources.GetObject("countInverseBtn.Image")));
			this.countInverseBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.countInverseBtn.Location = new System.Drawing.Point(358, 290);
			this.countInverseBtn.Name = "countInverseBtn";
			this.countInverseBtn.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
			this.countInverseBtn.Size = new System.Drawing.Size(174, 55);
			this.countInverseBtn.TabIndex = 5;
			this.countInverseBtn.Text = "COUNT DOWN";
			this.countInverseBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.countInverseBtn.UseVisualStyleBackColor = false;
			this.countInverseBtn.Click += new System.EventHandler(this.countInverseBtn_Click);
			this.countInverseBtn.MouseHover += new System.EventHandler(this.countInverseBtn_MouseHover);
			// 
			// timerAddBtn
			// 
			this.timerAddBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.timerAddBtn.FlatAppearance.BorderColor = System.Drawing.Color.Purple;
			this.timerAddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.timerAddBtn.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerAddBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.timerAddBtn.Image = ((System.Drawing.Image)(resources.GetObject("timerAddBtn.Image")));
			this.timerAddBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.timerAddBtn.Location = new System.Drawing.Point(338, 500);
			this.timerAddBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.timerAddBtn.Name = "timerAddBtn";
			this.timerAddBtn.Padding = new System.Windows.Forms.Padding(16, 0, 6, 0);
			this.timerAddBtn.Size = new System.Drawing.Size(194, 115);
			this.timerAddBtn.TabIndex = 6;
			this.timerAddBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.timerAddBtn.UseVisualStyleBackColor = false;
			this.timerAddBtn.Click += new System.EventHandler(this.timerAddBtn_Click);
			this.timerAddBtn.MouseHover += new System.EventHandler(this.timerAddBtn_MouseHover);
			// 
			// muteBtn
			// 
			this.muteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.muteBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.muteBtn.Image = ((System.Drawing.Image)(resources.GetObject("muteBtn.Image")));
			this.muteBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.muteBtn.Location = new System.Drawing.Point(252, 302);
			this.muteBtn.Name = "muteBtn";
			this.muteBtn.Size = new System.Drawing.Size(88, 43);
			this.muteBtn.TabIndex = 7;
			this.muteBtn.Text = "     Mute";
			this.muteBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.muteBtn.UseVisualStyleBackColor = true;
			// 
			// clearTimersListBtn
			// 
			this.clearTimersListBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.clearTimersListBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.clearTimersListBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.clearTimersListBtn.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.clearTimersListBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.clearTimersListBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearTimersListBtn.Image")));
			this.clearTimersListBtn.Location = new System.Drawing.Point(6, 617);
			this.clearTimersListBtn.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.clearTimersListBtn.Name = "clearTimersListBtn";
			this.clearTimersListBtn.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.clearTimersListBtn.Size = new System.Drawing.Size(144, 58);
			this.clearTimersListBtn.TabIndex = 9;
			this.clearTimersListBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.clearTimersListBtn.UseVisualStyleBackColor = false;
			this.clearTimersListBtn.Click += new System.EventHandler(this.clearTimersListBtn_Click);
			this.clearTimersListBtn.MouseHover += new System.EventHandler(this.clearTimersListBtn_MouseHover);
			// 
			// selectTimerLabel1
			// 
			this.selectTimerLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.selectTimerLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectTimerLabel1.ForeColor = System.Drawing.Color.White;
			this.selectTimerLabel1.Location = new System.Drawing.Point(4, 23);
			this.selectTimerLabel1.Margin = new System.Windows.Forms.Padding(0);
			this.selectTimerLabel1.Name = "selectTimerLabel1";
			this.selectTimerLabel1.Size = new System.Drawing.Size(53, 29);
			this.selectTimerLabel1.TabIndex = 10;
			this.selectTimerLabel1.Text = "Select";
			this.selectTimerLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// selectTimerLabel2
			// 
			this.selectTimerLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.selectTimerLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectTimerLabel2.ForeColor = System.Drawing.Color.White;
			this.selectTimerLabel2.Location = new System.Drawing.Point(4, 52);
			this.selectTimerLabel2.Margin = new System.Windows.Forms.Padding(0);
			this.selectTimerLabel2.Name = "selectTimerLabel2";
			this.selectTimerLabel2.Size = new System.Drawing.Size(50, 28);
			this.selectTimerLabel2.TabIndex = 11;
			this.selectTimerLabel2.Text = "Timer";
			this.selectTimerLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// navigateUpBtn
			// 
			this.navigateUpBtn.BackColor = System.Drawing.Color.Indigo;
			this.navigateUpBtn.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
			this.navigateUpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.navigateUpBtn.Image = ((System.Drawing.Image)(resources.GetObject("navigateUpBtn.Image")));
			this.navigateUpBtn.Location = new System.Drawing.Point(58, 1);
			this.navigateUpBtn.Margin = new System.Windows.Forms.Padding(1);
			this.navigateUpBtn.Name = "navigateUpBtn";
			this.navigateUpBtn.Size = new System.Drawing.Size(86, 52);
			this.navigateUpBtn.TabIndex = 14;
			this.navigateUpBtn.UseVisualStyleBackColor = false;
			this.navigateUpBtn.Click += new System.EventHandler(this.navigateUpBtn_Click);
			this.navigateUpBtn.MouseHover += new System.EventHandler(this.navigateUpBtn_MouseHover);
			// 
			// loadProfileBtn
			// 
			this.loadProfileBtn.BackColor = System.Drawing.Color.Black;
			this.loadProfileBtn.FlatAppearance.BorderColor = System.Drawing.Color.Purple;
			this.loadProfileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.loadProfileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.loadProfileBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.loadProfileBtn.Image = ((System.Drawing.Image)(resources.GetObject("loadProfileBtn.Image")));
			this.loadProfileBtn.Location = new System.Drawing.Point(124, 3);
			this.loadProfileBtn.Name = "loadProfileBtn";
			this.loadProfileBtn.Size = new System.Drawing.Size(118, 32);
			this.loadProfileBtn.TabIndex = 16;
			this.loadProfileBtn.Text = "Load Profile";
			this.loadProfileBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.loadProfileBtn.UseVisualStyleBackColor = false;
			this.loadProfileBtn.Click += new System.EventHandler(this.loadProfileBtn_Click);
			this.loadProfileBtn.MouseHover += new System.EventHandler(this.loadProfileBtn_MouseHover);
			// 
			// saveProfileBtn
			// 
			this.saveProfileBtn.BackColor = System.Drawing.Color.Black;
			this.saveProfileBtn.FlatAppearance.BorderColor = System.Drawing.Color.Purple;
			this.saveProfileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.saveProfileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveProfileBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.saveProfileBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveProfileBtn.Image")));
			this.saveProfileBtn.Location = new System.Drawing.Point(0, 3);
			this.saveProfileBtn.Name = "saveProfileBtn";
			this.saveProfileBtn.Size = new System.Drawing.Size(118, 32);
			this.saveProfileBtn.TabIndex = 17;
			this.saveProfileBtn.Text = "Save Profile";
			this.saveProfileBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.saveProfileBtn.UseVisualStyleBackColor = false;
			this.saveProfileBtn.Click += new System.EventHandler(this.saveProfileBtn_Click);
			this.saveProfileBtn.MouseHover += new System.EventHandler(this.saveProfileBtn_MouseHover);
			// 
			// loadProfile
			// 
			this.loadProfile.FileName = "openFileDialog1";
			// 
			// timerSelectorPanel
			// 
			this.timerSelectorPanel.Controls.Add(this.navigateDwnBtn);
			this.timerSelectorPanel.Controls.Add(this.navigateUpBtn);
			this.timerSelectorPanel.Controls.Add(this.selectTimerLabel2);
			this.timerSelectorPanel.Controls.Add(this.selectTimerLabel1);
			this.timerSelectorPanel.Location = new System.Drawing.Point(5, 290);
			this.timerSelectorPanel.Name = "timerSelectorPanel";
			this.timerSelectorPanel.Size = new System.Drawing.Size(145, 104);
			this.timerSelectorPanel.TabIndex = 22;
			// 
			// navigateDwnBtn
			// 
			this.navigateDwnBtn.BackColor = System.Drawing.Color.Indigo;
			this.navigateDwnBtn.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
			this.navigateDwnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.navigateDwnBtn.Image = ((System.Drawing.Image)(resources.GetObject("navigateDwnBtn.Image")));
			this.navigateDwnBtn.Location = new System.Drawing.Point(58, 52);
			this.navigateDwnBtn.Margin = new System.Windows.Forms.Padding(1);
			this.navigateDwnBtn.Name = "navigateDwnBtn";
			this.navigateDwnBtn.Size = new System.Drawing.Size(86, 52);
			this.navigateDwnBtn.TabIndex = 36;
			this.navigateDwnBtn.UseVisualStyleBackColor = false;
			this.navigateDwnBtn.Click += new System.EventHandler(this.navigateDwnBtn_Click);
			this.navigateDwnBtn.MouseHover += new System.EventHandler(this.navigateDwnBtn_MouseHover);
			// 
			// timersLabel
			// 
			this.timersLabel.AutoSize = true;
			this.timersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timersLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.timersLabel.Location = new System.Drawing.Point(5, 412);
			this.timersLabel.Name = "timersLabel";
			this.timersLabel.Size = new System.Drawing.Size(60, 20);
			this.timersLabel.TabIndex = 23;
			this.timersLabel.Text = "Timers:";
			// 
			// timerNameLabel
			// 
			this.timerNameLabel.AutoSize = true;
			this.timerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.timerNameLabel.Location = new System.Drawing.Point(289, 412);
			this.timerNameLabel.Name = "timerNameLabel";
			this.timerNameLabel.Size = new System.Drawing.Size(51, 20);
			this.timerNameLabel.TabIndex = 24;
			this.timerNameLabel.Text = "Name";
			// 
			// timerDurationLabel
			// 
			this.timerDurationLabel.AutoSize = true;
			this.timerDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDurationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.timerDurationLabel.Location = new System.Drawing.Point(430, 412);
			this.timerDurationLabel.Name = "timerDurationLabel";
			this.timerDurationLabel.Size = new System.Drawing.Size(70, 20);
			this.timerDurationLabel.TabIndex = 25;
			this.timerDurationLabel.Text = "Duration";
			// 
			// timerDurationEntry
			// 
			this.timerDurationEntry.BackColor = System.Drawing.Color.Gold;
			this.timerDurationEntry.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.timerDurationEntry.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDurationEntry.ForeColor = System.Drawing.Color.Black;
			this.timerDurationEntry.Location = new System.Drawing.Point(434, 432);
			this.timerDurationEntry.Margin = new System.Windows.Forms.Padding(0);
			this.timerDurationEntry.MaxLength = 8;
			this.timerDurationEntry.Name = "timerDurationEntry";
			this.timerDurationEntry.Size = new System.Drawing.Size(98, 22);
			this.timerDurationEntry.TabIndex = 28;
			this.timerDurationEntry.Text = "00:00:00";
			this.timerDurationEntry.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.timerDurationEntry.Enter += new System.EventHandler(this.timerDurationEntry_Enter);
			this.timerDurationEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.timerDurationEntry_KeyDown);
			this.timerDurationEntry.Leave += new System.EventHandler(this.timerDurationEntry_Leave);
			this.timerDurationEntry.MouseHover += new System.EventHandler(this.timerDurationEntry_MouseHover);
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(434, 454);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(46, 44);
			this.pictureBox2.TabIndex = 30;
			this.pictureBox2.TabStop = false;
			// 
			// timerNamesList
			// 
			this.timerNamesList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.timerNamesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerNamesList.ForeColor = System.Drawing.Color.White;
			this.timerNamesList.FormattingEnabled = true;
			this.timerNamesList.ItemHeight = 20;
			this.timerNamesList.Location = new System.Drawing.Point(6, 452);
			this.timerNamesList.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.timerNamesList.Name = "timerNamesList";
			this.timerNamesList.Size = new System.Drawing.Size(178, 164);
			this.timerNamesList.TabIndex = 31;
			this.timerNamesList.SelectedValueChanged += new System.EventHandler(this.timerNamesList_SelectedValueChanged);
			this.timerNamesList.DoubleClick += new System.EventHandler(this.timerNamesList_DoubleClick);
			// 
			// timerDurationsList
			// 
			this.timerDurationsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.timerDurationsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDurationsList.ForeColor = System.Drawing.Color.White;
			this.timerDurationsList.FormattingEnabled = true;
			this.timerDurationsList.ItemHeight = 20;
			this.timerDurationsList.Location = new System.Drawing.Point(183, 452);
			this.timerDurationsList.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
			this.timerDurationsList.Name = "timerDurationsList";
			this.timerDurationsList.Size = new System.Drawing.Size(107, 164);
			this.timerDurationsList.TabIndex = 32;
			this.timerDurationsList.SelectedValueChanged += new System.EventHandler(this.timerDurationsList_SelectedValueChanged);
			this.timerDurationsList.DoubleClick += new System.EventHandler(this.timerDurationsList_DoubleClick);
			// 
			// timerNameEntry
			// 
			this.timerNameEntry.BackColor = System.Drawing.Color.Gold;
			this.timerNameEntry.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.timerNameEntry.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerNameEntry.ForeColor = System.Drawing.Color.Black;
			this.timerNameEntry.Location = new System.Drawing.Point(292, 432);
			this.timerNameEntry.Margin = new System.Windows.Forms.Padding(0);
			this.timerNameEntry.MaxLength = 24;
			this.timerNameEntry.Name = "timerNameEntry";
			this.timerNameEntry.Size = new System.Drawing.Size(137, 22);
			this.timerNameEntry.TabIndex = 33;
			this.timerNameEntry.Text = "[Enter Name]";
			this.timerNameEntry.Enter += new System.EventHandler(this.timerNameEntry_Enter);
			this.timerNameEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.timerNameEntry_KeyDown);
			this.timerNameEntry.Leave += new System.EventHandler(this.timerNameEntry_Leave);
			this.timerNameEntry.MouseHover += new System.EventHandler(this.timerNameEntry_MouseHover);
			// 
			// chooseAudioBtn
			// 
			this.chooseAudioBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.chooseAudioBtn.FlatAppearance.BorderColor = System.Drawing.Color.Purple;
			this.chooseAudioBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chooseAudioBtn.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chooseAudioBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.chooseAudioBtn.Image = ((System.Drawing.Image)(resources.GetObject("chooseAudioBtn.Image")));
			this.chooseAudioBtn.Location = new System.Drawing.Point(470, 616);
			this.chooseAudioBtn.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.chooseAudioBtn.Name = "chooseAudioBtn";
			this.chooseAudioBtn.Size = new System.Drawing.Size(62, 59);
			this.chooseAudioBtn.TabIndex = 34;
			this.chooseAudioBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chooseAudioBtn.UseVisualStyleBackColor = false;
			this.chooseAudioBtn.Click += new System.EventHandler(this.chooseAudioBtn_Click);
			this.chooseAudioBtn.MouseHover += new System.EventHandler(this.chooseAudioBtn_MouseHover);
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(383, 454);
			this.pictureBox3.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(46, 44);
			this.pictureBox3.TabIndex = 38;
			this.pictureBox3.TabStop = false;
			// 
			// pictureBox4
			// 
			this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
			this.pictureBox4.Location = new System.Drawing.Point(291, 558);
			this.pictureBox4.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(43, 45);
			this.pictureBox4.TabIndex = 39;
			this.pictureBox4.TabStop = false;
			// 
			// chosenAudioLabel
			// 
			this.chosenAudioLabel.AutoSize = true;
			this.chosenAudioLabel.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chosenAudioLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.chosenAudioLabel.Location = new System.Drawing.Point(296, 638);
			this.chosenAudioLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.chosenAudioLabel.Name = "chosenAudioLabel";
			this.chosenAudioLabel.Size = new System.Drawing.Size(43, 20);
			this.chosenAudioLabel.TabIndex = 40;
			this.chosenAudioLabel.Text = "Audio:";
			this.chosenAudioLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// selectedAudioName
			// 
			this.selectedAudioName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.selectedAudioName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.selectedAudioName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectedAudioName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.selectedAudioName.Location = new System.Drawing.Point(338, 638);
			this.selectedAudioName.Margin = new System.Windows.Forms.Padding(0);
			this.selectedAudioName.Name = "selectedAudioName";
			this.selectedAudioName.Size = new System.Drawing.Size(129, 18);
			this.selectedAudioName.TabIndex = 41;
			this.selectedAudioName.Text = "[NONE]";
			// 
			// countTimer
			// 
			this.countTimer.Interval = 1000;
			this.countTimer.Tick += new System.EventHandler(this.countTimer_Tick);
			// 
			// timerNamesListLabel
			// 
			this.timerNamesListLabel.BackColor = System.Drawing.Color.Olive;
			this.timerNamesListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerNamesListLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.timerNamesListLabel.Location = new System.Drawing.Point(6, 432);
			this.timerNamesListLabel.Name = "timerNamesListLabel";
			this.timerNamesListLabel.Size = new System.Drawing.Size(178, 20);
			this.timerNamesListLabel.TabIndex = 42;
			this.timerNamesListLabel.Text = "Timer Name";
			this.timerNamesListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// timerDurationsListLabel
			// 
			this.timerDurationsListLabel.BackColor = System.Drawing.Color.Olive;
			this.timerDurationsListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDurationsListLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.timerDurationsListLabel.Location = new System.Drawing.Point(182, 432);
			this.timerDurationsListLabel.Name = "timerDurationsListLabel";
			this.timerDurationsListLabel.Size = new System.Drawing.Size(107, 20);
			this.timerDurationsListLabel.TabIndex = 43;
			this.timerDurationsListLabel.Text = "Duration";
			this.timerDurationsListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(291, 513);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(43, 45);
			this.pictureBox1.TabIndex = 29;
			this.pictureBox1.TabStop = false;
			// 
			// activeTimerLabel
			// 
			this.activeTimerLabel.AutoSize = true;
			this.activeTimerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.activeTimerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.activeTimerLabel.Location = new System.Drawing.Point(182, 370);
			this.activeTimerLabel.Name = "activeTimerLabel";
			this.activeTimerLabel.Size = new System.Drawing.Size(178, 18);
			this.activeTimerLabel.TabIndex = 44;
			this.activeTimerLabel.Text = "-Currently-Active-Timer-->";
			this.activeTimerLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// activeTimerTextBox
			// 
			this.activeTimerTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.activeTimerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.activeTimerTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.activeTimerTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.activeTimerTextBox.Location = new System.Drawing.Point(358, 370);
			this.activeTimerTextBox.Name = "activeTimerTextBox";
			this.activeTimerTextBox.ReadOnly = true;
			this.activeTimerTextBox.Size = new System.Drawing.Size(174, 22);
			this.activeTimerTextBox.TabIndex = 45;
			this.activeTimerTextBox.Text = "[Active Timer]";
			// 
			// hoursLabel
			// 
			this.hoursLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.hoursLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.hoursLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.hoursLabel.Location = new System.Drawing.Point(43, 42);
			this.hoursLabel.Name = "hoursLabel";
			this.hoursLabel.Size = new System.Drawing.Size(120, 16);
			this.hoursLabel.TabIndex = 46;
			this.hoursLabel.Text = "Hours";
			this.hoursLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// minutesLabel
			// 
			this.minutesLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.minutesLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.minutesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.minutesLabel.Location = new System.Drawing.Point(201, 42);
			this.minutesLabel.Name = "minutesLabel";
			this.minutesLabel.Size = new System.Drawing.Size(120, 16);
			this.minutesLabel.TabIndex = 47;
			this.minutesLabel.Text = "Minutes";
			this.minutesLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// secondsLabel
			// 
			this.secondsLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.secondsLabel.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.secondsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.secondsLabel.Location = new System.Drawing.Point(360, 42);
			this.secondsLabel.Name = "secondsLabel";
			this.secondsLabel.Size = new System.Drawing.Size(120, 16);
			this.secondsLabel.TabIndex = 48;
			this.secondsLabel.Text = "Seconds";
			this.secondsLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// saveLapsesCheckBox
			// 
			this.saveLapsesCheckBox.BackColor = System.Drawing.Color.Transparent;
			this.saveLapsesCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveLapsesCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.saveLapsesCheckBox.Image = ((System.Drawing.Image)(resources.GetObject("saveLapsesCheckBox.Image")));
			this.saveLapsesCheckBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.saveLapsesCheckBox.Location = new System.Drawing.Point(399, 5);
			this.saveLapsesCheckBox.Margin = new System.Windows.Forms.Padding(0);
			this.saveLapsesCheckBox.Name = "saveLapsesCheckBox";
			this.saveLapsesCheckBox.Size = new System.Drawing.Size(128, 30);
			this.saveLapsesCheckBox.TabIndex = 49;
			this.saveLapsesCheckBox.Text = "Remember";
			this.saveLapsesCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.saveLapsesCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.saveLapsesCheckBox.UseVisualStyleBackColor = false;
			this.saveLapsesCheckBox.MouseHover += new System.EventHandler(this.saveLapsesCheckBox_MouseHover);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.panel1.Controls.Add(this.saveProfileBtn);
			this.panel1.Controls.Add(this.saveLapsesCheckBox);
			this.panel1.Controls.Add(this.loadProfileBtn);
			this.panel1.Location = new System.Drawing.Point(5, -1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(527, 37);
			this.panel1.TabIndex = 50;
			// 
			// saveProfileDialog
			// 
			this.saveProfileDialog.DefaultExt = "txt";
			// 
			// removeTimerBtn
			// 
			this.removeTimerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.removeTimerBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.removeTimerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.removeTimerBtn.Image = ((System.Drawing.Image)(resources.GetObject("removeTimerBtn.Image")));
			this.removeTimerBtn.Location = new System.Drawing.Point(151, 617);
			this.removeTimerBtn.Name = "removeTimerBtn";
			this.removeTimerBtn.Size = new System.Drawing.Size(139, 58);
			this.removeTimerBtn.TabIndex = 51;
			this.removeTimerBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.removeTimerBtn.UseVisualStyleBackColor = false;
			this.removeTimerBtn.Click += new System.EventHandler(this.removeTimerBtn_Click);
			this.removeTimerBtn.MouseHover += new System.EventHandler(this.removeTimerBtn_MouseHover);
			// 
			// currentProfileTextBox
			// 
			this.currentProfileTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.currentProfileTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.currentProfileTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.currentProfileTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.currentProfileTextBox.Location = new System.Drawing.Point(358, 348);
			this.currentProfileTextBox.Name = "currentProfileTextBox";
			this.currentProfileTextBox.ReadOnly = true;
			this.currentProfileTextBox.Size = new System.Drawing.Size(174, 22);
			this.currentProfileTextBox.TabIndex = 52;
			this.currentProfileTextBox.Text = "[Current Profile]";
			// 
			// currentProfileLabel
			// 
			this.currentProfileLabel.AutoSize = true;
			this.currentProfileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.currentProfileLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.currentProfileLabel.Location = new System.Drawing.Point(183, 349);
			this.currentProfileLabel.Name = "currentProfileLabel";
			this.currentProfileLabel.Size = new System.Drawing.Size(177, 18);
			this.currentProfileLabel.TabIndex = 54;
			this.currentProfileLabel.Text = "-Currently-Active-Profile->";
			this.currentProfileLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// continuousModeBtn
			// 
			this.continuousModeBtn.AutoSize = true;
			this.continuousModeBtn.Image = ((System.Drawing.Image)(resources.GetObject("continuousModeBtn.Image")));
			this.continuousModeBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.continuousModeBtn.Location = new System.Drawing.Point(185, 308);
			this.continuousModeBtn.Name = "continuousModeBtn";
			this.continuousModeBtn.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
			this.continuousModeBtn.Size = new System.Drawing.Size(51, 32);
			this.continuousModeBtn.TabIndex = 55;
			this.continuousModeBtn.UseVisualStyleBackColor = true;
			this.continuousModeBtn.MouseHover += new System.EventHandler(this.continuousModeBtn_MouseHover);
			// 
			// timeBeforeNextTimer
			// 
			this.timeBeforeNextTimer.Interval = 1000;
			this.timeBeforeNextTimer.Tick += new System.EventHandler(this.timeBeforeNextTimer_Tick);
			// 
			// DigitalCountTimer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(537, 676);
			this.Controls.Add(this.continuousModeBtn);
			this.Controls.Add(this.currentProfileLabel);
			this.Controls.Add(this.currentProfileTextBox);
			this.Controls.Add(this.removeTimerBtn);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.secondsLabel);
			this.Controls.Add(this.minutesLabel);
			this.Controls.Add(this.hoursLabel);
			this.Controls.Add(this.activeTimerTextBox);
			this.Controls.Add(this.activeTimerLabel);
			this.Controls.Add(this.timerDurationsListLabel);
			this.Controls.Add(this.timerNamesListLabel);
			this.Controls.Add(this.selectedAudioName);
			this.Controls.Add(this.chosenAudioLabel);
			this.Controls.Add(this.pictureBox4);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.chooseAudioBtn);
			this.Controls.Add(this.timerNameEntry);
			this.Controls.Add(this.timerDurationsList);
			this.Controls.Add(this.timerNamesList);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.timerDurationEntry);
			this.Controls.Add(this.timerDurationLabel);
			this.Controls.Add(this.timerNameLabel);
			this.Controls.Add(this.timersLabel);
			this.Controls.Add(this.timerSelectorPanel);
			this.Controls.Add(this.clearTimersListBtn);
			this.Controls.Add(this.muteBtn);
			this.Controls.Add(this.timerAddBtn);
			this.Controls.Add(this.countInverseBtn);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.timerDisplay);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DigitalCountTimer";
			this.Text = "Digital Count Up/Down Timer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DigitalCountTimer_FormClosing);
			this.timerSelectorPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label timerDisplay;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button countInverseBtn;
		private System.Windows.Forms.Button timerAddBtn;
		private System.Windows.Forms.CheckBox muteBtn;
		private System.Windows.Forms.Button clearTimersListBtn;
		private System.Windows.Forms.Label selectTimerLabel1;
		private System.Windows.Forms.Label selectTimerLabel2;
		private System.Windows.Forms.Button navigateUpBtn;
		private System.Windows.Forms.Button loadProfileBtn;
		private System.Windows.Forms.Button saveProfileBtn;
		private System.Windows.Forms.SaveFileDialog saveProfile;
		private System.Windows.Forms.OpenFileDialog loadProfile;
		private System.Windows.Forms.Panel timerSelectorPanel;
		private System.Windows.Forms.Label timersLabel;
		private System.Windows.Forms.Label timerNameLabel;
		private System.Windows.Forms.Label timerDurationLabel;
		private System.Windows.Forms.TextBox timerDurationEntry;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.ListBox timerNamesList;
		private System.Windows.Forms.ListBox timerDurationsList;
		private System.Windows.Forms.TextBox timerNameEntry;
		private System.Windows.Forms.Button chooseAudioBtn;
		private System.Windows.Forms.Button navigateDwnBtn;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Label chosenAudioLabel;
		private System.Windows.Forms.Label selectedAudioName;
		private System.Windows.Forms.Timer countTimer;
		private System.Windows.Forms.Label timerNamesListLabel;
		private System.Windows.Forms.Label timerDurationsListLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.OpenFileDialog audioFileSelector;
		private System.Windows.Forms.Label activeTimerLabel;
		private System.Windows.Forms.TextBox activeTimerTextBox;
		private System.Windows.Forms.Label hoursLabel;
		private System.Windows.Forms.Label minutesLabel;
		private System.Windows.Forms.Label secondsLabel;
		private System.Windows.Forms.CheckBox saveLapsesCheckBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.OpenFileDialog loadProfileDiaglog;
		private System.Windows.Forms.SaveFileDialog saveProfileDialog;
		private System.Windows.Forms.ToolTip toolTips;
		private System.Windows.Forms.Button removeTimerBtn;
		private System.Windows.Forms.TextBox currentProfileTextBox;
		private System.Windows.Forms.Label currentProfileLabel;
		private System.Windows.Forms.CheckBox continuousModeBtn;
		private System.Windows.Forms.Timer timeBeforeNextTimer;
	}
}

