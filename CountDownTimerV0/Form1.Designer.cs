
namespace CountDownTimerV0
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.TimerDisplay = new System.Windows.Forms.Label();
			this.startButton = new System.Windows.Forms.Button();
			this.stopButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.countInverseBtn = new System.Windows.Forms.Button();
			this.timerAddBtn = new System.Windows.Forms.Button();
			this.muteBtn = new System.Windows.Forms.CheckBox();
			this.continuousModeBtn = new System.Windows.Forms.CheckBox();
			this.clearTimersListBtn = new System.Windows.Forms.Button();
			this.selectTimerLabel1 = new System.Windows.Forms.Label();
			this.selectTimerLabel2 = new System.Windows.Forms.Label();
			this.navigateUpBtn = new System.Windows.Forms.Button();
			this.loadProfileBtn = new System.Windows.Forms.Button();
			this.saveProfileBtn = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.counterSelectorPanel = new System.Windows.Forms.Panel();
			this.navigateDwnBtn = new System.Windows.Forms.Button();
			this.timersLabel = new System.Windows.Forms.Label();
			this.timerNameLabel = new System.Windows.Forms.Label();
			this.timerDurationLabel = new System.Windows.Forms.Label();
			this.timerDurationEntry = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.timerNamesList = new System.Windows.Forms.ListBox();
			this.timerDurationsList = new System.Windows.Forms.ListBox();
			this.timerNameEntry = new System.Windows.Forms.TextBox();
			this.chooseAudioBtn = new System.Windows.Forms.Button();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.chosenAudioLabel = new System.Windows.Forms.Label();
			this.selectedAudioName = new System.Windows.Forms.Label();
			this.countDownTimer = new System.Windows.Forms.Timer(this.components);
			this.timerNamesListLabel = new System.Windows.Forms.Label();
			this.timerDurationsListLabel = new System.Windows.Forms.Label();
			this.counterSelectorPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			this.SuspendLayout();
			// 
			// TimerDisplay
			// 
			this.TimerDisplay.BackColor = System.Drawing.Color.Black;
			this.TimerDisplay.Font = new System.Drawing.Font("Unispace", 71.99999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TimerDisplay.ForeColor = System.Drawing.Color.Lime;
			this.TimerDisplay.Location = new System.Drawing.Point(5, 31);
			this.TimerDisplay.Name = "TimerDisplay";
			this.TimerDisplay.Size = new System.Drawing.Size(527, 133);
			this.TimerDisplay.TabIndex = 0;
			this.TimerDisplay.Text = "00:00:00";
			this.TimerDisplay.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
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
			this.startButton.Location = new System.Drawing.Point(5, 168);
			this.startButton.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.startButton.Name = "startButton";
			this.startButton.Padding = new System.Windows.Forms.Padding(32, 0, 26, 0);
			this.startButton.Size = new System.Drawing.Size(174, 74);
			this.startButton.TabIndex = 1;
			this.startButton.Text = "START";
			this.startButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.startButton.UseVisualStyleBackColor = false;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
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
			this.stopButton.Location = new System.Drawing.Point(181, 168);
			this.stopButton.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
			this.stopButton.Name = "stopButton";
			this.stopButton.Padding = new System.Windows.Forms.Padding(36, 0, 28, 0);
			this.stopButton.Size = new System.Drawing.Size(174, 74);
			this.stopButton.TabIndex = 2;
			this.stopButton.Text = "STOP";
			this.stopButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.stopButton.UseVisualStyleBackColor = false;
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
			this.resetButton.Location = new System.Drawing.Point(358, 168);
			this.resetButton.Name = "resetButton";
			this.resetButton.Padding = new System.Windows.Forms.Padding(36, 0, 28, 0);
			this.resetButton.Size = new System.Drawing.Size(174, 74);
			this.resetButton.TabIndex = 3;
			this.resetButton.Text = "RESET";
			this.resetButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.resetButton.UseVisualStyleBackColor = false;
			// 
			// countInverseBtn
			// 
			this.countInverseBtn.BackColor = System.Drawing.Color.Gray;
			this.countInverseBtn.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
			this.countInverseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.countInverseBtn.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.countInverseBtn.ForeColor = System.Drawing.Color.White;
			this.countInverseBtn.Location = new System.Drawing.Point(358, 245);
			this.countInverseBtn.Name = "countInverseBtn";
			this.countInverseBtn.Size = new System.Drawing.Size(174, 55);
			this.countInverseBtn.TabIndex = 5;
			this.countInverseBtn.Text = "COUNT UP";
			this.countInverseBtn.UseVisualStyleBackColor = false;
			// 
			// timerAddBtn
			// 
			this.timerAddBtn.BackColor = System.Drawing.Color.Gray;
			this.timerAddBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.timerAddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.timerAddBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerAddBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.timerAddBtn.Image = ((System.Drawing.Image)(resources.GetObject("timerAddBtn.Image")));
			this.timerAddBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.timerAddBtn.Location = new System.Drawing.Point(338, 459);
			this.timerAddBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.timerAddBtn.Name = "timerAddBtn";
			this.timerAddBtn.Padding = new System.Windows.Forms.Padding(17, 0, 6, 0);
			this.timerAddBtn.Size = new System.Drawing.Size(194, 143);
			this.timerAddBtn.TabIndex = 6;
			this.timerAddBtn.Text = "Add To Timers";
			this.timerAddBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.timerAddBtn.UseVisualStyleBackColor = false;
			this.timerAddBtn.Click += new System.EventHandler(this.listAddBtn_Click);
			// 
			// muteBtn
			// 
			this.muteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.muteBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.muteBtn.Image = ((System.Drawing.Image)(resources.GetObject("muteBtn.Image")));
			this.muteBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.muteBtn.Location = new System.Drawing.Point(158, 282);
			this.muteBtn.Name = "muteBtn";
			this.muteBtn.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
			this.muteBtn.Size = new System.Drawing.Size(140, 43);
			this.muteBtn.TabIndex = 7;
			this.muteBtn.Text = "     Mute Audio";
			this.muteBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.muteBtn.UseVisualStyleBackColor = true;
			// 
			// continuousModeBtn
			// 
			this.continuousModeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.continuousModeBtn.ForeColor = System.Drawing.Color.White;
			this.continuousModeBtn.Location = new System.Drawing.Point(158, 258);
			this.continuousModeBtn.Name = "continuousModeBtn";
			this.continuousModeBtn.Size = new System.Drawing.Size(148, 29);
			this.continuousModeBtn.TabIndex = 8;
			this.continuousModeBtn.Text = "Continuous Mode";
			this.continuousModeBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.continuousModeBtn.UseVisualStyleBackColor = true;
			// 
			// clearTimersListBtn
			// 
			this.clearTimersListBtn.BackColor = System.Drawing.Color.Gray;
			this.clearTimersListBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.clearTimersListBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.clearTimersListBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.clearTimersListBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.clearTimersListBtn.Image = ((System.Drawing.Image)(resources.GetObject("clearTimersListBtn.Image")));
			this.clearTimersListBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.clearTimersListBtn.Location = new System.Drawing.Point(292, 606);
			this.clearTimersListBtn.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.clearTimersListBtn.Name = "clearTimersListBtn";
			this.clearTimersListBtn.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.clearTimersListBtn.Size = new System.Drawing.Size(139, 58);
			this.clearTimersListBtn.TabIndex = 9;
			this.clearTimersListBtn.Text = "Clear Timers";
			this.clearTimersListBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.clearTimersListBtn.UseVisualStyleBackColor = false;
			// 
			// selectTimerLabel1
			// 
			this.selectTimerLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.selectTimerLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectTimerLabel1.ForeColor = System.Drawing.Color.White;
			this.selectTimerLabel1.Location = new System.Drawing.Point(10, 23);
			this.selectTimerLabel1.Margin = new System.Windows.Forms.Padding(0);
			this.selectTimerLabel1.Name = "selectTimerLabel1";
			this.selectTimerLabel1.Size = new System.Drawing.Size(57, 29);
			this.selectTimerLabel1.TabIndex = 10;
			this.selectTimerLabel1.Text = "Select";
			this.selectTimerLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// selectTimerLabel2
			// 
			this.selectTimerLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.selectTimerLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectTimerLabel2.ForeColor = System.Drawing.Color.White;
			this.selectTimerLabel2.Location = new System.Drawing.Point(13, 52);
			this.selectTimerLabel2.Margin = new System.Windows.Forms.Padding(0);
			this.selectTimerLabel2.Name = "selectTimerLabel2";
			this.selectTimerLabel2.Size = new System.Drawing.Size(54, 28);
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
			this.navigateUpBtn.Location = new System.Drawing.Point(71, 1);
			this.navigateUpBtn.Margin = new System.Windows.Forms.Padding(1);
			this.navigateUpBtn.Name = "navigateUpBtn";
			this.navigateUpBtn.Size = new System.Drawing.Size(54, 52);
			this.navigateUpBtn.TabIndex = 14;
			this.navigateUpBtn.UseVisualStyleBackColor = false;
			// 
			// loadProfileBtn
			// 
			this.loadProfileBtn.BackColor = System.Drawing.Color.Black;
			this.loadProfileBtn.FlatAppearance.BorderColor = System.Drawing.Color.Purple;
			this.loadProfileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.loadProfileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.loadProfileBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.loadProfileBtn.Location = new System.Drawing.Point(122, 4);
			this.loadProfileBtn.Name = "loadProfileBtn";
			this.loadProfileBtn.Size = new System.Drawing.Size(111, 24);
			this.loadProfileBtn.TabIndex = 16;
			this.loadProfileBtn.Text = "Load Profile";
			this.loadProfileBtn.UseVisualStyleBackColor = false;
			// 
			// saveProfileBtn
			// 
			this.saveProfileBtn.BackColor = System.Drawing.Color.Black;
			this.saveProfileBtn.FlatAppearance.BorderColor = System.Drawing.Color.Purple;
			this.saveProfileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.saveProfileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.saveProfileBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.saveProfileBtn.Location = new System.Drawing.Point(5, 4);
			this.saveProfileBtn.Name = "saveProfileBtn";
			this.saveProfileBtn.Size = new System.Drawing.Size(111, 24);
			this.saveProfileBtn.TabIndex = 17;
			this.saveProfileBtn.Text = "Save Profile";
			this.saveProfileBtn.UseVisualStyleBackColor = false;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// counterSelectorPanel
			// 
			this.counterSelectorPanel.Controls.Add(this.navigateDwnBtn);
			this.counterSelectorPanel.Controls.Add(this.navigateUpBtn);
			this.counterSelectorPanel.Controls.Add(this.selectTimerLabel2);
			this.counterSelectorPanel.Controls.Add(this.selectTimerLabel1);
			this.counterSelectorPanel.Location = new System.Drawing.Point(5, 245);
			this.counterSelectorPanel.Name = "counterSelectorPanel";
			this.counterSelectorPanel.Size = new System.Drawing.Size(126, 104);
			this.counterSelectorPanel.TabIndex = 22;
			// 
			// navigateDwnBtn
			// 
			this.navigateDwnBtn.BackColor = System.Drawing.Color.Indigo;
			this.navigateDwnBtn.FlatAppearance.BorderColor = System.Drawing.Color.Olive;
			this.navigateDwnBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.navigateDwnBtn.Image = ((System.Drawing.Image)(resources.GetObject("navigateDwnBtn.Image")));
			this.navigateDwnBtn.Location = new System.Drawing.Point(71, 52);
			this.navigateDwnBtn.Margin = new System.Windows.Forms.Padding(1);
			this.navigateDwnBtn.Name = "navigateDwnBtn";
			this.navigateDwnBtn.Size = new System.Drawing.Size(54, 52);
			this.navigateDwnBtn.TabIndex = 36;
			this.navigateDwnBtn.UseVisualStyleBackColor = false;
			// 
			// timersLabel
			// 
			this.timersLabel.AutoSize = true;
			this.timersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timersLabel.ForeColor = System.Drawing.Color.White;
			this.timersLabel.Location = new System.Drawing.Point(0, 401);
			this.timersLabel.Name = "timersLabel";
			this.timersLabel.Size = new System.Drawing.Size(60, 20);
			this.timersLabel.TabIndex = 23;
			this.timersLabel.Text = "Timers:";
			// 
			// timerNameLabel
			// 
			this.timerNameLabel.AutoSize = true;
			this.timerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerNameLabel.ForeColor = System.Drawing.Color.White;
			this.timerNameLabel.Location = new System.Drawing.Point(288, 368);
			this.timerNameLabel.Name = "timerNameLabel";
			this.timerNameLabel.Size = new System.Drawing.Size(94, 20);
			this.timerNameLabel.TabIndex = 24;
			this.timerNameLabel.Text = "Timer Name";
			// 
			// timerDurationLabel
			// 
			this.timerDurationLabel.AutoSize = true;
			this.timerDurationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDurationLabel.ForeColor = System.Drawing.Color.White;
			this.timerDurationLabel.Location = new System.Drawing.Point(430, 368);
			this.timerDurationLabel.Name = "timerDurationLabel";
			this.timerDurationLabel.Size = new System.Drawing.Size(70, 20);
			this.timerDurationLabel.TabIndex = 25;
			this.timerDurationLabel.Text = "Duration";
			// 
			// timerDurationEntry
			// 
			this.timerDurationEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.timerDurationEntry.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.timerDurationEntry.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDurationEntry.Location = new System.Drawing.Point(434, 388);
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
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(292, 475);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(43, 45);
			this.pictureBox1.TabIndex = 29;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(459, 413);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(46, 44);
			this.pictureBox2.TabIndex = 30;
			this.pictureBox2.TabStop = false;
			// 
			// timerNamesList
			// 
			this.timerNamesList.BackColor = System.Drawing.Color.Olive;
			this.timerNamesList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerNamesList.ForeColor = System.Drawing.Color.White;
			this.timerNamesList.FormattingEnabled = true;
			this.timerNamesList.ItemHeight = 20;
			this.timerNamesList.Location = new System.Drawing.Point(5, 441);
			this.timerNamesList.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
			this.timerNamesList.Name = "timerNamesList";
			this.timerNamesList.Size = new System.Drawing.Size(178, 224);
			this.timerNamesList.TabIndex = 31;
			this.timerNamesList.SelectedValueChanged += new System.EventHandler(this.timerNamesList_SelectedValueChanged);
			// 
			// timerDurationsList
			// 
			this.timerDurationsList.BackColor = System.Drawing.Color.Olive;
			this.timerDurationsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDurationsList.ForeColor = System.Drawing.Color.White;
			this.timerDurationsList.FormattingEnabled = true;
			this.timerDurationsList.ItemHeight = 20;
			this.timerDurationsList.Location = new System.Drawing.Point(182, 441);
			this.timerDurationsList.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.timerDurationsList.Name = "timerDurationsList";
			this.timerDurationsList.Size = new System.Drawing.Size(107, 224);
			this.timerDurationsList.TabIndex = 32;
			this.timerDurationsList.SelectedValueChanged += new System.EventHandler(this.timerDurationsList_SelectedValueChanged);
			// 
			// timerNameEntry
			// 
			this.timerNameEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.timerNameEntry.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.timerNameEntry.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerNameEntry.Location = new System.Drawing.Point(292, 388);
			this.timerNameEntry.Margin = new System.Windows.Forms.Padding(0);
			this.timerNameEntry.MaxLength = 24;
			this.timerNameEntry.Name = "timerNameEntry";
			this.timerNameEntry.Size = new System.Drawing.Size(137, 22);
			this.timerNameEntry.TabIndex = 33;
			this.timerNameEntry.Text = "[Enter Name]";
			this.timerNameEntry.Enter += new System.EventHandler(this.timerNameEntry_Enter);
			this.timerNameEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.timerNameEntry_KeyDown);
			this.timerNameEntry.Leave += new System.EventHandler(this.timerNameEntry_Leave);
			// 
			// chooseAudioBtn
			// 
			this.chooseAudioBtn.BackColor = System.Drawing.Color.Gray;
			this.chooseAudioBtn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
			this.chooseAudioBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.chooseAudioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chooseAudioBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.chooseAudioBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.chooseAudioBtn.Location = new System.Drawing.Point(434, 606);
			this.chooseAudioBtn.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.chooseAudioBtn.Name = "chooseAudioBtn";
			this.chooseAudioBtn.Padding = new System.Windows.Forms.Padding(8, 0, 4, 0);
			this.chooseAudioBtn.Size = new System.Drawing.Size(98, 58);
			this.chooseAudioBtn.TabIndex = 34;
			this.chooseAudioBtn.Text = "Choose Audio";
			this.chooseAudioBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chooseAudioBtn.UseVisualStyleBackColor = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(356, 413);
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
			this.pictureBox4.Location = new System.Drawing.Point(292, 544);
			this.pictureBox4.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(43, 45);
			this.pictureBox4.TabIndex = 39;
			this.pictureBox4.TabStop = false;
			// 
			// chosenAudioLabel
			// 
			this.chosenAudioLabel.AutoSize = true;
			this.chosenAudioLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chosenAudioLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.chosenAudioLabel.Location = new System.Drawing.Point(155, 329);
			this.chosenAudioLabel.Name = "chosenAudioLabel";
			this.chosenAudioLabel.Size = new System.Drawing.Size(105, 18);
			this.chosenAudioLabel.TabIndex = 40;
			this.chosenAudioLabel.Text = "Chosen Audio:";
			// 
			// selectedAudioName
			// 
			this.selectedAudioName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.selectedAudioName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.selectedAudioName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectedAudioName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.selectedAudioName.Location = new System.Drawing.Point(271, 329);
			this.selectedAudioName.Name = "selectedAudioName";
			this.selectedAudioName.Size = new System.Drawing.Size(261, 18);
			this.selectedAudioName.TabIndex = 41;
			// 
			// countDownTimer
			// 
			this.countDownTimer.Interval = 1000;
			// 
			// timerNamesListLabel
			// 
			this.timerNamesListLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.timerNamesListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerNamesListLabel.Location = new System.Drawing.Point(5, 424);
			this.timerNamesListLabel.Name = "timerNamesListLabel";
			this.timerNamesListLabel.Size = new System.Drawing.Size(178, 17);
			this.timerNamesListLabel.TabIndex = 42;
			this.timerNamesListLabel.Text = "Timer Name";
			this.timerNamesListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// timerDurationsListLabel
			// 
			this.timerDurationsListLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.timerDurationsListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerDurationsListLabel.Location = new System.Drawing.Point(182, 424);
			this.timerDurationsListLabel.Name = "timerDurationsListLabel";
			this.timerDurationsListLabel.Size = new System.Drawing.Size(107, 17);
			this.timerDurationsListLabel.TabIndex = 43;
			this.timerDurationsListLabel.Text = "Duration";
			this.timerDurationsListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.ClientSize = new System.Drawing.Size(536, 671);
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
			this.Controls.Add(this.counterSelectorPanel);
			this.Controls.Add(this.saveProfileBtn);
			this.Controls.Add(this.loadProfileBtn);
			this.Controls.Add(this.clearTimersListBtn);
			this.Controls.Add(this.continuousModeBtn);
			this.Controls.Add(this.muteBtn);
			this.Controls.Add(this.timerAddBtn);
			this.Controls.Add(this.countInverseBtn);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.TimerDisplay);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Digital Count Up/Down Timer";
			this.counterSelectorPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label TimerDisplay;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.Button countInverseBtn;
		private System.Windows.Forms.Button timerAddBtn;
		private System.Windows.Forms.CheckBox muteBtn;
		private System.Windows.Forms.CheckBox continuousModeBtn;
		private System.Windows.Forms.Button clearTimersListBtn;
		private System.Windows.Forms.Label selectTimerLabel1;
		private System.Windows.Forms.Label selectTimerLabel2;
		private System.Windows.Forms.Button navigateUpBtn;
		private System.Windows.Forms.Button loadProfileBtn;
		private System.Windows.Forms.Button saveProfileBtn;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel counterSelectorPanel;
		private System.Windows.Forms.Label timersLabel;
		private System.Windows.Forms.Label timerNameLabel;
		private System.Windows.Forms.Label timerDurationLabel;
		private System.Windows.Forms.TextBox timerDurationEntry;
		private System.Windows.Forms.PictureBox pictureBox1;
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
		private System.Windows.Forms.Timer countDownTimer;
		private System.Windows.Forms.Label timerNamesListLabel;
		private System.Windows.Forms.Label timerDurationsListLabel;
	}
}

