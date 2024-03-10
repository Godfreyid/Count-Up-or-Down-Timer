using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Text.RegularExpressions;

namespace CountDownTimerV0
{
	public partial class DigitalCountTimer : Form
	{
		/* THESE PATHS NEED TO USE THE Environment.SpecialFolder.MyDocuments var */
		private const string PROFILE_DIR = @"\Count Down Up Timer\Profiles\";
		private readonly string MY_DOCUMENTS_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		private readonly string PROFILES_DIR_PATH;

		private const string PROFILE_AUDIO_DIR = @"\Count Down Up Timer\Profile Audio\";
		private readonly string PROFILES_AUDIO_DIR_PATH;

		private const string SAVED_LAPSES_DIR = @"\Count Down Up Timer\Remembered Lapses\";
		private readonly string LAPSES_DIR_PATH;

		/*private const string LAPSES_MEM_FILE_NAME = "Remembered Lapses.txt";
		private string LAPSES_MEM_FILE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + LAPSES_MEM_FILE_NAME;*/
		/*private const string LAPSES_MEM_FILE_PATH = @"C:\Users\GDK\Documents\Count Down Up Timer\Remembered Lapses.txt";*/
		/*private const string SAVE_LAPSES_FLAG_FILE_NAME = "Save Lapses OnExit Flag.txt";
		private string SAVE_LAPSES_ON_EXIT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + SAVE_LAPSES_FLAG_FILE_NAME;*/
		/*private const string SAVE_LAPSES_ON_EXIT_PATH = @"C:\Users\GDK\Documents\Count Down Up Timer\Save Lapses OnExit Flag.txt";*/

		private const string DEFAULT_PROFILE_FILE_EXT = ".txt";
		private const string AUDIO_SAVE_FILE_SUFFIX = "_chosenAudio.txt";
		private const string LAPSES_SAVE_FILE_SUFFIX = "_cachedLapses.txt";

		private string _currentProfilePath;

		private const char DELIMITER_TIMER_LAPSES = ':';
		private const char DELIMITER_TIMER_PROFILES = '|';

		private const string TIMER_DISPLAY_DEFAULT_STRING = "00:00:00";
		private const string NAME_ENTRY_PROMPT_STRING = "[Enter Name]";
		private const string DURATION_ENTRY_PROMPT_STRING = "00:00:00";

		private const string ASSIGN_TO_PROFILE_CAPTION = "Needs Assigning to a Profile";
		private const string ASSIGN_TO_PROFILE_MESSAGE = "You need to choose a profile to assign the remembered lapses to.";

		private const string MISSING_NAME_CAPTION = "Missing Name Input";
		private const string MISSING_NAME_MESSAGE = "You did not enter a timers name. Retry.";

		private const string MISSING_DURATION_CAPTION = "Missing Duration Input";
		private const string MISSING_DURATION_MESSAGE = "You did not enter a timers duration. Retry.";

		private const string SELECT_A_TIMER_CAPTION = "Select a Timer";
		private const string SELECT_A_TIMER_MESSAGE = "You need to select a timers to start.";

		private const string ALARM_CAPTION = "Timer Elapsed";
		private const string ALARM_MESSAGE = "End Alarm.";

		private const string TOOLTIP_CHOOSE_AUDIO_BTN = "Choose timer ALARM	audio";
		private const int TOOLTIP_CHOOSE_AUDIO_BTN_DUR = 3000;
		private const string TOOLTIP_INCORRECT_FORMAT = "Wrong format, use empty default as template";
		private const int TOOLTIP_INCORRECT_FORMAT_DUR = 3000;
		private const string TOOLTIP_REMOVE_BTN = "Remote SELECTED timer from list";
		private const int TOOLTIP_REMOVE_BTN_DUR = 3000;
		private const string TOOLTIP_CLEAR_TIMERS_LIST = "Clear (ENTIRE) timers list";
		private const int TOOLTIP_CLEAR_TIMERS_LIST_DUR = 3000;
		private const string TOOLTIP_SELECT_TIMER_TO_REMOVE = "First SELECT a timer to remove";
		private const int TOOLTIP_SELECT_TIMER_TO_REMOVE_DUR = 3000;

		Form _alarmAlertWindow;
		
		private const int MINUTES_PER_HOUR = 60;
		private const int SECONDS_PER_MINUTE = 60;

		private FormattedTimeColumns _formattedColumns;

		private MessageBoxInfo _assignProfileMsgBoxInfo;
		private MessageBoxInfo _timerNameMsgBoxInfo;
		private MessageBoxInfo _timerDurationMsgBoxInfo;
		private MessageBoxInfo _startButtonMsgBoxInfo;
		private MessageBoxInfo _alarmMsgBoxInfo;

		private ChosenTimer _chosenTimer;

		private string[] _durationTimeColumns;
		private int _durationAsSeconds;

		private int _countUpDurationTarget;
		private bool _countDown = true;
		private const string COUNT_UP_BUTTON_TEXT = "COUNTING UP";
		private const string COUNT_DOWN_BUTTON_TEXT = "COUNTIING DOWN";
		private int _upCount;
		private Dictionary<string, int> _lapsesByNameDict;

		private SoundPlayer _soundPlayer;
		private SelectedAudio _selectedAudio;

		private struct FormattedTimeColumns
		{
			public int Hours { get; set; }
			public int Minutes { get; set; }
			public int Seconds { get; set; }
		}

		private struct MessageBoxInfo
		{
			public string Message { get; set; }
			public string Caption { get; set; }
			public MessageBoxButtons Buttons { get; set; }

			public MessageBoxInfo(string message, string caption, MessageBoxButtons buttons)
			{
				Message = message;
				Caption = caption;
				Buttons = buttons;
			}
		}

		private struct ChosenTimer
		{
			public ChosenTimer(string name, string duration)
			{
				Name = name;
				Duration = duration;
			}

			public string Name { get; set; }
			public string Duration { get; set; }
		}

		private struct SelectedAudio
		{
			public string FullPath { get; set; }
			public string OnlyFileName { get; set; }
		}

		private enum TimerState
		{
			Ticking,
			TickFromBeginning,
			TickFromPaused,
			Stopped
		}
		private TimerState _timerState = TimerState.Stopped;

		#region COSMETIC (NON-FUNCTIONALITY) CONSTRUCTS

		private enum ControlEngaged
		{
			StartButton,
			StopButton,
			ResetButton,
			CountInversionButton,
			NavigateUpButton,
			NavigateDownButton,
			MuteAudioButton,
			TimerNameEntryBox,
			TimerDurationEntryBox,
			TimerAddButton,
			ClearTimersButton,
			ChooseAudioButton,
			TimerNamesList,
			TimerDurationsList,
			SaveProfile,
			LoadProfile,
			RememberTimerLapses
		}

		#endregion

		public DigitalCountTimer()
		{
			InitializeComponent();

			/* INITIALIZE DIRECTORY PATHS */
			PROFILES_DIR_PATH = MY_DOCUMENTS_PATH + PROFILE_DIR;

			PROFILES_AUDIO_DIR_PATH = MY_DOCUMENTS_PATH + PROFILE_AUDIO_DIR;

			LAPSES_DIR_PATH = MY_DOCUMENTS_PATH + SAVED_LAPSES_DIR;

			SetupForm();
		}

		private void SetupForm()
		{
			SetTabIndices();

			MoveControlsToBack();

			_formattedColumns = new FormattedTimeColumns();

			_assignProfileMsgBoxInfo = new MessageBoxInfo(
				ASSIGN_TO_PROFILE_MESSAGE, ASSIGN_TO_PROFILE_CAPTION, MessageBoxButtons.OKCancel);

			_timerNameMsgBoxInfo = new MessageBoxInfo(
				MISSING_NAME_MESSAGE, MISSING_NAME_CAPTION, MessageBoxButtons.OK);

			_timerDurationMsgBoxInfo = new MessageBoxInfo(
				MISSING_DURATION_MESSAGE, MISSING_DURATION_CAPTION, MessageBoxButtons.OK);

			_startButtonMsgBoxInfo = new MessageBoxInfo(
				SELECT_A_TIMER_MESSAGE, SELECT_A_TIMER_CAPTION, MessageBoxButtons.OK);

			_alarmMsgBoxInfo = new MessageBoxInfo(
				ALARM_MESSAGE, ALARM_CAPTION, MessageBoxButtons.OK);

			_chosenTimer = new ChosenTimer(NAME_ENTRY_PROMPT_STRING, DURATION_ENTRY_PROMPT_STRING);
			_selectedAudio = new SelectedAudio();

			_lapsesByNameDict = new Dictionary<string, int>();

			StartPosition = FormStartPosition.CenterScreen;

			//setup alarm alert window shown when the alarm is raised
			_alarmAlertWindow = new Form();
			_alarmAlertWindow.Hide();
			_alarmAlertWindow.FormBorderStyle = FormBorderStyle.None;
			_alarmAlertWindow.WindowState = FormWindowState.Maximized;
			_alarmAlertWindow.BackColor = Color.FromArgb(192, 192, 0);
			_alarmAlertWindow.AllowTransparency = true;
			_alarmAlertWindow.Opacity = 0.25;

			_durationTimeColumns = new string[3]; /* holds 3 time hh, mm, and ss (hh:mm:ss) columns */

			/* LOAD SOUND FILE TO PLAY AS THE ALARM */
			_soundPlayer = new SoundPlayer();

			timerNameEntry.Focus();

			/* CREATE MAIN DIRECTORY FOR SAVING/LOADING PROFILES, LAPSES, AND FLAGS */
			CreateAppDirectories();

			/* SET INITIAL VALUES FOR FILE OPEN AND SAVE DIALOG CONTROLLERS */
			saveProfileDialog.InitialDirectory = PROFILES_DIR_PATH;
			loadProfileDiaglog.InitialDirectory = PROFILES_DIR_PATH;

			saveProfileDialog.DefaultExt = DEFAULT_PROFILE_FILE_EXT;
			loadProfileDiaglog.DefaultExt = DEFAULT_PROFILE_FILE_EXT;

			/* SET INITIAL DIRECTORY FOR AUDIO SELECT DIALOG CONTROLLER */
			#region DEBUG VERSION (NOT RELEASE VERSION)

			/* exe is in 'Release/', which is in 'bin/', which is in 'CountDownTimerV0/'
			   soundClips are in 'Standard Alarm Tunes/', which is in 'CountDownTimerV0/'
			   so from 'Release/', go to parent 'bin/', then again to parent
			   'CountDownTimerV0/' *//*
			var exeParentBinDir = Directory.GetParent(Environment.CurrentDirectory);
			var binParentCountDownTimerV0Dir = Directory.GetParent(exeParentBinDir.FullName);
			//then combine that parent 'CountDownTimerV0/' with 'Standard Alarm Tunes/'
			string alarmTunesDir = Path.Combine(
				binParentCountDownTimerV0Dir.FullName, @"Standard Alarm Tunes\");
			//set audioFolderBrowser.InitialDirectory to the combined path
			audioFileSelector.InitialDirectory = alarmTunesDir;*/

			#endregion
			#region RELEASE VERSION (NOT DEBUG VERSION)

			/* exe is in 'Installation Folder/' (ex. CountDownTimer), and soundClips are 
			   in 'Standard Alarm Tunes/', which is in 'Installation Folder/'.
			   So get parent dir, then go to 'Standard Alarm Tunes/' */
			var exeParentDir = Directory.GetParent(Environment.CurrentDirectory);
			var binParentCountDownTimerV0Dir = Directory.GetParent(exeParentDir.FullName);
			//then combine that parent 'CountDownTimerV0/' with 'Standard Alarm Tunes/'
			string alarmTunesDir = Path.Combine(
				exeParentDir.FullName, @"Standard Alarm Tunes\");
			//set audioFolderBrowser.InitialDirectory to the combined path
			audioFileSelector.InitialDirectory = alarmTunesDir;

			#endregion
		}

		private void SetTabIndices()
		{
			startButton.TabIndex = 1;
			stopButton.TabIndex = 2;
			resetButton.TabIndex = 3;
			counterSelectorPanel.TabIndex = 4;
			navigateUpBtn.TabIndex = 5;
			navigateDwnBtn.TabIndex = 6;
			countInverseBtn.TabIndex = 8;
			muteBtn.TabIndex = 9;
			timerNameEntry.TabIndex = 10;
			timerDurationEntry.TabIndex = 11;
			timerAddBtn.TabIndex = 12;
			chooseAudioBtn.TabIndex = 13;
			clearTimersListBtn.TabIndex = 14;
			saveProfileBtn.TabIndex = 15;
			loadProfileBtn.TabIndex = 16;
		}

		private void MoveControlsToBack()
		{
			saveLapsesCheckBox.SendToBack();
		}
		
		private void CreateAppDirectories() 
		{
			bool existingProfilesDir = Directory.Exists(PROFILES_DIR_PATH);
			//if not existing, create 'Profiles' directory 
			if ( !existingProfilesDir )
				Directory.CreateDirectory(PROFILES_DIR_PATH);

			bool existingProfileAudioDir = Directory.Exists(PROFILES_AUDIO_DIR_PATH);
			//if not existing, create 'Profile Audio' directory
			if ( !existingProfileAudioDir )
				Directory.CreateDirectory(PROFILES_AUDIO_DIR_PATH);

			bool existingLapsesDir = Directory.Exists(LAPSES_DIR_PATH);
			//if not existing, create 'Remembered Lapses' directory
			if ( !existingLapsesDir )
				Directory.CreateDirectory(LAPSES_DIR_PATH);
		}

		// user clicked in text field to begin entering timers name
		private void timerNameEntry_Enter(object sender, EventArgs e)
		{
			ReadyTextBoxInput(timerNameEntry, NAME_ENTRY_PROMPT_STRING);
		}

		/// <summary>
		/// Clears the default (user guiding) prompt string of 
		/// the <paramref name="textBoxEntered"/> so that the user
		/// can simply begin entering their desired text without
		/// first having to delete the default text string.
		/// </summary>
		/// <param name="textBoxEntered">The TextBox into whom's
		/// text box field text will be input.</param>
		/// <param name="defaultTextBoxString">The default prompt 
		/// string that guides the user as to what the text 
		/// (including format) ought to be.</param>
		private void ReadyTextBoxInput(TextBox textBoxEntered, string defaultTextBoxString)
		{
			//highlight (select all) the default text prompting user's entry
			//OR PERHAPS BETTER,
			bool defaultPrompt = textBoxEntered.Text.Equals(defaultTextBoxString);
			//if the current text EQUALS 'defaultTextBoxString'
			if ( !defaultPrompt ) return;

			//set text to empty (or null)
			textBoxEntered.Text = string.Empty;
			return;
		}

		// user clicked in text field to begin entering timers duration
		private void timerDurationEntry_Enter(object sender, EventArgs e)
		{
			ReadyTextBoxInput(timerDurationEntry, DURATION_ENTRY_PROMPT_STRING);
		}

		// user clicked away from text field taking focus to another control
		private void timerNameEntry_Leave(object sender, EventArgs e)
		{
			LeavingTextBox(timerNameEntry, NAME_ENTRY_PROMPT_STRING);
		}

		/// <summary>
		/// Will refill the text field of the <paramref name="textBoxLeft"/>
		/// if the user leaves the TextBox without inputing a string that
		/// is different from the <paramref name="defaultTextBoxString"/>.
		/// </summary>
		/// <param name="textBoxLeft">The TextBox being left, and into whom's
		/// text box field the <paramref name="defaultTextBoxString"/> will 
		/// be input.</param>
		/// <param name="defaultTextBoxString">The default prompt 
		/// string that guides the user as to what the text 
		/// (including format) ought to be.</param>
		/// <param name="requireProperFormat">Toggles whether the expected
		/// entered text string should be matched against a regular expression to
		/// ensure the string was entered with proper format.</param>
		/// <param name="requiredFormatRegex">The regular expression string used 
		/// to match (check) proper format of the text string entered 
		/// into <paramref name="textBoxLeft"/>.</param>
		private void LeavingTextBox(TextBox textBoxLeft, string defaultTextBoxString, bool requireProperFormat = false, string requiredFormatRegex = "")
		{
			bool emptyTextField = string.IsNullOrEmpty(textBoxLeft.Text);
			//if empty name/duration text field, set to default
			if ( emptyTextField )
			{
				textBoxLeft.Text = defaultTextBoxString;
				return;
			}

			/* Enforce string format via regex */
			//if not requireing proper formate, return
			if ( !requireProperFormat ) return;

			//if correct timers duration format, return
			bool timerHhMmSsFormat = Regex.IsMatch(textBoxLeft.Text, requiredFormatRegex);
			if ( timerHhMmSsFormat ) return;

			//show popup prompting user to re-input with correct hh:mm:ss format
			toolTips.Show(
				TOOLTIP_INCORRECT_FORMAT, textBoxLeft, TOOLTIP_INCORRECT_FORMAT_DUR);
		}

		private void timerDurationEntry_Leave(object sender, EventArgs e)
		{
			string formatPattern = @"\d{2}:\d{2}:\d{2}";
			LeavingTextBox(timerDurationEntry, DURATION_ENTRY_PROMPT_STRING, true, formatPattern);
		}

		// user finished entering name and presses 'Enter' to start entering duration 
		private void timerNameEntry_KeyDown(object sender, KeyEventArgs e)
		{
			bool pressedEnter = e.KeyCode == Keys.Enter;
			if ( !pressedEnter ) return;

			bool positiveRefocus = ShiftFocusOnTextSubmit(
				e, Keys.Enter, timerNameEntry, NAME_ENTRY_PROMPT_STRING, timerDurationEntry);
			if ( positiveRefocus ) return;

			//mimic timerNameEntry control Enter event handler
			ReadyTextBoxInput(timerNameEntry, NAME_ENTRY_PROMPT_STRING);
		}

		/// <summary>
		/// Will shift focus to the controller <paramref name="focusShiftedTo"/>
		/// only if the submitted text (entered into the text field 
		/// of <paramref name="textToSubmit"/>) is not equal to 
		/// the <paramref name="defaultTextBoxString"/>.
		/// </summary>
		/// <param name="e">Used to confirm if the pressed key is 
		/// the <paramref name="submitKey"/>.</param>
		/// <param name="submitKey">Key treated as the key to press when wishing to
		/// submit the text entered into <paramref name="textToSubmit"/>.</param>
		/// <param name="textToSubmit">The TextBox into whome the text to submit was
		/// entered.</param>
		/// <param name="defaultTextBoxString">The text string which the text entered
		/// for submission should not be equal if it is to be accepted as a submission.</param>
		/// <param name="focusShiftedTo">The Control to shift focus to upon a successfull 
		/// text string submission.</param>
		private bool ShiftFocusOnTextSubmit(KeyEventArgs e, Keys submitKey, TextBox textToSubmit, string defaultTextBoxString, Control focusShiftedTo)
		{
			//if the key pressed != 'ENTER', return
			bool pressedEnterKey = e.KeyCode == submitKey;
			//if 'timerNameEntry' control text EQUALS 'NAME_ENTRY_PROMPT_STRING',
			bool defaultPrompt = textToSubmit.Text.Equals(defaultTextBoxString);
			bool containsDefaultPrompt = textToSubmit.Text.Contains(defaultTextBoxString);
			bool enteredText = !defaultPrompt && !containsDefaultPrompt;
			bool shiftFocus = pressedEnterKey && enteredText;
			//return
			if ( !shiftFocus ) return false;

			//user provided a adequate text, so can move focus to 'next' control
			focusShiftedTo.Focus();
			return true;
		}

		// user finished entering duration and presses 'Enter' to move tab focus to timers submit btn
		private void timerDurationEntry_KeyDown(object sender, KeyEventArgs e)
		{
			bool pressedEnter = e.KeyCode == Keys.Enter;
			if ( !pressedEnter ) return;

			bool positiveRefocus = ShiftFocusOnTextSubmit(
				e, Keys.Enter, timerDurationEntry, DURATION_ENTRY_PROMPT_STRING, timerAddBtn);
			if ( !positiveRefocus )
			{
				ReadyTextBoxInput(timerDurationEntry, DURATION_ENTRY_PROMPT_STRING);
				return;
			}

			/* Enforce correct timers duration format */
			int bulkSeconds = DurationAsBulkSeconds(timerDurationEntry.Text);
			FormatTimeFromBulkSeconds(bulkSeconds, ref _formattedColumns);

			//reasign 60 based duration to 'timerDurationEntry' 
			//format 2 character hour, minute and seconds columns (hh:mm:ss)
			string rebuiltDurationString = FormatHhMmSsTime(
				_formattedColumns.Hours, _formattedColumns.Minutes, _formattedColumns.Seconds);

			timerDurationEntry.Text = rebuiltDurationString;
		}

		/// <summary>
		/// Takes the string input timers duration and computes the equivalent in seconds
		/// of said entire duration.
		/// </summary>
		/// <param name="durationString">The user input timers duration</param>
		/// <returns></returns>
		private int DurationAsBulkSeconds(string durationString)
		{
			//convert the formatted 'ChosenTimer.Duration' to 'durationAsSeconds' as,
			//hours column to minutes to seconds, i.e. ( (hours * 60mins) * 60secs ) as 'hoursAsSeconds', 
			_durationTimeColumns = durationString.Split(':');
			//guard against no hours entry
			bool noHours = string.IsNullOrEmpty(_durationTimeColumns[0]);
			string hoursColumn = noHours ? "0" : _durationTimeColumns[0];
			int hours = int.Parse(hoursColumn);
			int hoursInSeconds = (hours * MINUTES_PER_HOUR) * SECONDS_PER_MINUTE;
			//PLUS minutes column to seconds, i.e. (mins * 60secs) as 'minutesAsSeconds',
			//guard against no minutes entry
			bool noMinutes = string.IsNullOrEmpty(_durationTimeColumns[1]);
			string minutesColumn = noMinutes ? "0" : _durationTimeColumns[1];
			int minutes = int.Parse(minutesColumn);
			int minutesInSeconds = minutes * SECONDS_PER_MINUTE;
			//PLUS seconds column
			//guard against no seconds entry
			bool noSeconds = string.IsNullOrEmpty(_durationTimeColumns[2]);
			string secondsColumn = noSeconds ? "0" : _durationTimeColumns[2];
			int seconds = int.Parse(secondsColumn);
			int durationAsSeconds = hoursInSeconds + minutesInSeconds + seconds;

			return durationAsSeconds;
		}

		/// <summary>
		/// Derives the hours, minutes, and seconds column from
		/// the <paramref name="bulkSeconds"/>, essentially formatting
		/// it to HH:MM:SS.
		/// </summary>
		/// <param name="bulkSeconds"></param>
		/// <param name="formattedTimeColumns">Caches the formatted time columns</param>
		/// <param name="forceTwoNumberColumns">Toggles whether to force each column
		/// of the formatted (hh:mm:ss) time be made of two numbers.</param>
		/// <returns>The formatted time consisting of two numbers per column (if 
		/// <paramref name="forceTwoNumberColumns"/> is toggled), else returns
		/// an empty string.</returns>
		private string FormatTimeFromBulkSeconds(
			int bulkSeconds, ref FormattedTimeColumns formattedTimeColumns,
			bool forceTwoNumberColumns = false)
		{
			//format seconds column in hh:mm:ss from 'durationAsSeconds' as,
			formattedTimeColumns.Seconds = bulkSeconds % 60;
			float secondsToMinutes = bulkSeconds / 60;
			int minutesCarriedOver = (int)Math.Floor(secondsToMinutes);

			//format minutes column in hh:mm:ss from 'minutesCarriedOver' as,
			formattedTimeColumns.Minutes = minutesCarriedOver % 60;
			float minutesToHours = minutesCarriedOver / 60;
			int hoursCarriedOver = (int)Math.Floor(minutesToHours);

			//format hours column in hh:mm:ss from 'hoursCarriedOver' as,
			bool hoursExceed99 = hoursCarriedOver > 99;
			formattedTimeColumns.Hours = hoursExceed99 ? 99 : hoursCarriedOver;

			if ( !forceTwoNumberColumns ) return string.Empty;

			//ensures each time column (hh:mm:ss) has a minimum of two numbers
			//e.g. 01:00:25
			string twoNumberColumnarTime = FormatHhMmSsTime(
				formattedTimeColumns.Hours,
				formattedTimeColumns.Minutes,
				formattedTimeColumns.Seconds);

			return twoNumberColumnarTime;
		}

		/// <summary>
		/// Takes the provided <paramref name="hoursColumn"/>, 
		/// <paramref name="minutesColumn"/>, and 
		/// <paramref name="secondsColumn"/> args, then creates a string formatted to have
		/// two number characters for each column to create the HH:MM:SS format.
		/// </summary>
		/// <param name="hoursColumn">The number of hours placed in the hours (HH) 
		/// column of HH:MM:SS</param>
		/// <param name="minutesColumn">The number of minutes placed in the minutes (MM) 
		/// column of HH:MM:SS</param>
		/// <param name="secondsColumn">The number of seconds placed in the seconds (SS) 
		/// column of HH:MM:SS</param>
		/// <returns></returns>
		private static string FormatHhMmSsTime(int hoursColumn, int minutesColumn, int secondsColumn)
		{
			bool oneHoursChar = hoursColumn.ToString().Length < 2;
			string formattedHours = oneHoursChar ? $"0{hoursColumn}" : hoursColumn.ToString();

			bool oneMinutesChar = minutesColumn.ToString().Length < 2;
			string formattedMinutes = oneMinutesChar ? $"0{minutesColumn}" : minutesColumn.ToString();

			bool oneSecondsChar = secondsColumn.ToString().Length < 2;
			string formattedSeconds = oneSecondsChar ? $"0{secondsColumn}" : secondsColumn.ToString();

			string rebuiltDurationString = $"{formattedHours}:{formattedMinutes}:{formattedSeconds}";
			return rebuiltDurationString;
		}

		// user clicks to submit timers (name+duration) to 'Timers' list
		private void listAddBtn_Click(object sender, EventArgs e)
		{
			/* Ensure valid data submission */
			bool invalidName = 
				RefocusInvalidTextEntry(timerNameEntry, NAME_ENTRY_PROMPT_STRING, _timerNameMsgBoxInfo);
			bool invalidDuration = 
				RefocusInvalidTextEntry(timerDurationEntry, DURATION_ENTRY_PROMPT_STRING, _timerDurationMsgBoxInfo, invalidName);

			if ( invalidName || invalidDuration ) return;

			/* Add 'timerNameEntry' text to 'timerNamesList' listbox */
			AddTextBoxTextToListBox(timerNamesList, timerNameEntry, NAME_ENTRY_PROMPT_STRING);

			/* Add 'timerDurationEntry' text to 'timerDurationsList' listbox */
			AddTextBoxTextToListBox(timerDurationsList, timerDurationEntry, DURATION_ENTRY_PROMPT_STRING);

			//focus on 'timerNameEntry' control to ready for next timers entry
			timerNameEntry.Focus();
		}

		/// <summary>
		/// Determines if the text entered into <paramref name="refocusedOn"/>
		/// is valid, then refocuses on <paramref name="refocusedOn"/> if said
		/// text is invalid. Which gives the user a chance to quickly re-enter
		/// the text correctly. Though, if the text is valid, the default string
		/// for the text field is entered to remind the user what proper formatting
		/// is upon later entries.
		/// </summary>
		/// <param name="refocusedOn">The TextBox whose entered text is validated
		/// and refocused if said text is invalid.</param>
		/// <param name="defaultBoxText">The default string that the text field 
		/// of <paramref name="refocusedOn"/> set to when the current to-be-assessed 
		/// text is valid.</param>
		/// <param name="invalidMsgInfo">A struct containing the minimal required
		/// information for a MessageBox informing the user on what to correct in the
		/// event of an invalid timers name or duration entry.</param>
		/// <param name="skipRefocus">A flag to skip refocusing on the subject TextBox
		/// despite potentially detecting invalid <paramref name="defaultBoxText"/>.
		/// Usefull when you just want to alert the user of invalid entry, but
		/// do not want to refocus, but instead refocus on another TextBox -like
		/// one coming before or after this <paramref name="refocusedOn"/>
		/// text bos.</param>
		/// <returns></returns>
		private bool RefocusInvalidTextEntry(
			TextBox refocusedOn, 
			string defaultBoxText, 
			MessageBoxInfo invalidMsgInfo,
			bool skipRefocus = false)
		{
			bool emptyTextField = string.IsNullOrEmpty(refocusedOn.Text);
			bool defaultText = refocusedOn.Text.Equals(defaultBoxText);
			bool containsDefaultText = refocusedOn.Text.Contains(defaultBoxText);

			bool enteredText = !emptyTextField && !defaultText && !containsDefaultText;
			//if valid entry, no need to refocus, so return false
			if ( enteredText ) return false;

			//TextBox text is empty, EQUALS defaultBoxText, or wrong format, so
			//display popup informing user to enter a valid text string
			MessageBox.Show(
				invalidMsgInfo.Message, invalidMsgInfo.Caption, invalidMsgInfo.Buttons);

			// skip refocusing on 'refocusedOn' text box despite invalid 'defaultBoxText'
			if ( skipRefocus ) return true;

			//return focus to 'refocusedOn' TextBox
			refocusedOn.Focus();
			//return true for refocused
			return true;
		}

		private void AddTextBoxTextToListBox(ListBox listBox, TextBox textBox, string defaultTextBoxString)
		{
			#region .NET BUG WORKAROUND

			//transfer contents of 'listBox' to an array that is 1 index longer
			object[] tempHolding = new object[listBox.Items.Count + 1];
			listBox.Items.CopyTo(tempHolding, 0);
			//clear 'listBox'
			listBox.Items.Clear();
			//add 'Text' value to the array
			tempHolding[tempHolding.Length - 1] = textBox.Text;

			//pause painting list box while transferring
			listBox.BeginUpdate();

			//transfer contents of the array back to the 'listBox'
			foreach ( object text in tempHolding )
			{
				listBox.Items.Add(text);
			}

			//reset the Text property of textBox control to the defaultBoxText
			textBox.Text = defaultTextBoxString;

			//upause painting list box
			listBox.EndUpdate();

			#endregion

			/*//pause painting list box while adding text
			listBox.BeginUpdate();
			//get the text value from the Text property of the textBox control
			//add text value to list box
			string textBoxText = textBox.Text;
			listBox.Items.Add(textBoxText);
			//reset the Text property of textBox control to the defaultBoxText
			textBox.Text = defaultTextBoxString;
			//un-pause painting list box
			listBox.EndUpdate();*/
		}

		// so user can select timers by either clicking on its name or duration, then press 'Start'
		private void timerNamesList_SelectedValueChanged(object sender, EventArgs e)
		{
			/* only the name at the selected row will be highlighted, but we also
			   want to highlight the corresponding duration in timerDurationsList, so*/
			//get the selected index in timerNamesList
			int selectedNameI = timerNamesList.SelectedIndex;
			bool unSelected = selectedNameI < 0;
			if ( unSelected ) return;

			//set selectedIndex of timerDurationsList to that of timerNamesList
			timerDurationsList.SelectedIndex = selectedNameI;

			//add the name and duration of selected to the 'ChosenTimer' struct
			string selectedName = timerNamesList.Items[selectedNameI].ToString();
			string selectedDuration = timerDurationsList.Items[selectedNameI].ToString();
			_chosenTimer.Name = selectedName;
			_chosenTimer.Duration = selectedDuration;

			//set 'timerDisplay' control text to the 'Duration' property of 'ChosenTimer' struct
			bool stoppedWithoutReset = _lapsesByNameDict.TryGetValue(selectedName, out int durationAsSeconds);
			string timerDuration = stoppedWithoutReset ? FormatTimeFromBulkSeconds(durationAsSeconds, ref _formattedColumns, true) : selectedDuration;

			//set timer state according to the selected timer lapse 'state'
			_timerState = stoppedWithoutReset ? TimerState.TickFromPaused : TimerState.TickFromBeginning;

			timerDisplay.Text = timerDuration;
		}

		// so user can select timers by either clicking on its name or duration, then press 'Start'
		private void timerDurationsList_SelectedValueChanged(object sender, EventArgs e)
		{
			/* only the duration at the selected row will be highlighted, but we also
			   want to highlight the corresponding name in timerNamesList, so */
			//get the selected index in timerDurationsList
			int selectedDurationI = timerDurationsList.SelectedIndex;
			bool unSelected = selectedDurationI < 0;
			if ( unSelected ) return;

			//set selectedIndex of timerNamesList to that of timerDurationsList
			timerNamesList.SelectedIndex = selectedDurationI;

			//add the duration and of selected to the 'ChosenTimer' struct
			string selectedDuration = timerDurationsList.Items[selectedDurationI].ToString();
			string selectedName = timerNamesList.Items[selectedDurationI].ToString();
			_chosenTimer.Duration = selectedDuration;
			_chosenTimer.Name = selectedName;

			//set 'timerDisplay' control text to the 'Duration' property of 'ChosenTimer' struct
			bool stoppedWithoutReset = _lapsesByNameDict.TryGetValue(selectedName, out int durationAsSeconds);
			string timerDuration = stoppedWithoutReset ? FormatTimeFromBulkSeconds(durationAsSeconds, ref _formattedColumns, true) : selectedDuration;

			//set timer state according to the selected timer lapse 'state'
			_timerState = stoppedWithoutReset ? TimerState.TickFromPaused : TimerState.TickFromBeginning;

			timerDisplay.Text = timerDuration;
		}

		private void countInverseBtn_Click(object sender, EventArgs e)
		{
			//invert count flag
			_countDown = !_countDown;
			//change button text to match flag
			countInverseBtn.Text = _countDown ? COUNT_DOWN_BUTTON_TEXT : COUNT_UP_BUTTON_TEXT;
		}

		private void chooseAudioBtn_Click(object sender, EventArgs e)
		{
			//cache the current 'FileName' of 'audioFileSelector'
			string cachedChosenAudioFilePath = audioFileSelector.FileName;
			//open the 'audioFileSelector' control dialog
			bool selectionFailed = audioFileSelector.ShowDialog() != DialogResult.OK;
			if ( selectionFailed ) return;

			/* After the user chose an audio file by clicking 'OK' on
			   the dialog window, the 'FileName' property of the
			   'audioFileSelector' control will be set to the path of
			   said chosen audio file. But only if a file different
			   from the current is chosen. */
			string newAudioPath = audioFileSelector.FileName;
			int newAudioPathHash = newAudioPath.GetHashCode();
			int prevAudioPathHash = cachedChosenAudioFilePath.GetHashCode();
			bool choseSameAudioAsPrev = prevAudioPathHash == newAudioPathHash;
			//if prev 'selectedAudioName' text to chosen audio file path == newly chosen,
			if ( choseSameAudioAsPrev ) return;
			//return;

			CacheAudioFileName(newAudioPath, out string audioFileName);
			//set 'selectedAudioName' text to selected file simple name
			selectedAudioName.Text = audioFileName;
		}

		private void CacheAudioFileName(string filePath, out string fileName)
		{
			/* Once the user selected a new audio file via the 'chooseAudioBtn' 
			   file select dialog control, that file path will have been set
			   as the Text of the 'selectedAudioName' text box of the
			   'chosenAudioName' label. But we have to present a more user 
			   readeable chosen audio file name with 'selectedAudioName'. */
			//cache 'selectedAudioName' text of the 'chosenAudioLabel'
			//take just the .extension file name from selected audio name
			string[] nestingDirs = filePath.Split('\\');
			int lastI = nestingDirs.Length - 1;
			string justTheName = nestingDirs[lastI];
			//cache justTheName of the selected audio file
			_selectedAudio.OnlyFileName = justTheName;
			_selectedAudio.FullPath = filePath;

			fileName = justTheName;
		}

		// user intends to clear all selectable timers from the 'Timers' list
		private void clearTimersListBtn_Click(object sender, EventArgs e)
		{
			//when timer state is 'TickFromBeginning' return
			switch ( _timerState )
			{
				case TimerState.TickFromBeginning:
				case TimerState.TickFromPaused:

					return;
				case TimerState.Stopped:
				default:

					break;
			}

			//pause drawing of the timer name and duration list boxes
			timerNamesList.BeginUpdate();
			timerDurationsList.BeginUpdate();

			//reset display text
			timerDisplay.Text = TIMER_DISPLAY_DEFAULT_STRING;

			//clear the timerNamesList
			timerNamesList.Items.Clear();
			//clear the timerDurationsList
			timerDurationsList.Items.Clear();

			//clear the lapsesByNameDict
			_lapsesByNameDict.Clear();

			//clear the _chosenTimer struct
			_chosenTimer.Name = string.Empty;
			_chosenTimer.Duration = string.Empty;

			//nullify _currentProfilePath
			_currentProfilePath = string.Empty;

			//resume drawing of the timer name and duration list boxes
			timerNamesList.EndUpdate();
			timerDurationsList.EndUpdate();
		}

		// user intends to remove the currently selected timer from 'Timers' list
		private void removeTimerBtn_Click(object sender, EventArgs e)
		{
			bool timerSelected = timerNamesList.SelectedIndex > -1;
			//if no timer names or durations list selected item,
			if ( !timerSelected )
			{
				//show tooltip telling user to first select a timer
				toolTips.Show(
					TOOLTIP_SELECT_TIMER_TO_REMOVE,
					removeTimerBtn, 
					TOOLTIP_SELECT_TIMER_TO_REMOVE_DUR);
				//return
				return;
			}

			//pause drawing of the timer names and durations list boxes
			timerNamesList.BeginUpdate();
			timerDurationsList.BeginUpdate();

			//remove the _lapsesByNameDict pair matching the name of _choseTimer
			bool timerHasLapse = _lapsesByNameDict.ContainsKey(_chosenTimer.Name);
			if ( timerHasLapse )
				_lapsesByNameDict.Remove(_chosenTimer.Name);

			//cache seletectedItem of timer names and durations lists
			object selectedTimerName = timerNamesList.SelectedItem;
			object selectedTimerDuration = timerDurationsList.SelectedItem;

			//remove selected item from timer names and durations lists
			timerNamesList.Items.Remove(selectedTimerName);
			timerDurationsList.Items.Remove(selectedTimerDuration);

			//nullify _chosenTimer struct properties
			_chosenTimer.Name = string.Empty;
			_chosenTimer.Duration = string.Empty;

			//unpause drawing of the timer names and durations list boxes
			timerNamesList.EndUpdate();
			timerDurationsList.EndUpdate();
		}

		private void chooseAudioBtn_MouseHover(object sender, EventArgs e)
		{
			//show tooltip with TOOLTIP_CHOOSE_AUDIO_BTN text and chooseAudioBtn window
			toolTips.Show(
				TOOLTIP_CHOOSE_AUDIO_BTN,
				chooseAudioBtn,
				TOOLTIP_CHOOSE_AUDIO_BTN_DUR);
		}

		private void removeTimerBtn_MouseHover(object sender, EventArgs e)
		{
			//show tooltip with TOOLTIP_REMOVE_BTN text and removeTimerBtn window
			toolTips.Show(
				TOOLTIP_REMOVE_BTN,
				removeTimerBtn,
				TOOLTIP_REMOVE_BTN_DUR);
		}

		private void clearTimersListBtn_MouseHover(object sender, EventArgs e)
		{
			//show tooltip with TOOLTIP_REMOVE_BTN text and removeTimerBtn window
			toolTips.Show(
				TOOLTIP_CLEAR_TIMERS_LIST,
				clearTimersListBtn,
				TOOLTIP_CLEAR_TIMERS_LIST_DUR);
		}

		// user intends to begin count down/up
		private void startButton_Click(object sender, EventArgs e)
		{
			/* To allow resuming count when clicking to another timers and 
			   back again, have an actively count down/up 'DurationAsBulkSeconds'
			   that is only updated when clicking the 'startButton', not when
			   clicking on a different timers by either the 'timerNamesList' or
			   the 'timerDurationsList'. */

			/* -give the 'startButton' a switch state machine and two enum states.
			   -the first enum state being 'FromBeginning' is the default, with 
			   current implementation as seen below the 'defaultTimerDisplay' guard 
			   clause.
			   -the second enum state being 'FromPause', wherein _durationAsSeconds
			   and _countUpDurationTarget are not recomputed before enabling and
			   starting the countTimer. */
			switch ( _timerState )
			{
				case TimerState.Ticking:

					return;
				case TimerState.TickFromBeginning:
					//if no timers is selected from the list
					bool defaultTimerDisplay = timerDisplay.Text.Equals(TIMER_DISPLAY_DEFAULT_STRING);
					//if 'timerDisplay' text EQUALS the TIMER_DISPLAY_DEFAULT_STRING,
					if ( defaultTimerDisplay )
					{
						//display message box informing user to first select a timer
						MessageBox.Show(_startButtonMsgBoxInfo.Message, _startButtonMsgBoxInfo.Caption, _startButtonMsgBoxInfo.Buttons);
						//put focus back on the 'navigateDwnBtn' control
						navigateDwnBtn.Focus();

						//return
						return;
					}

					/* To increment upto or decrement down from the 
					   'ChosenTimer.Duration', we have to determine what the 
					   entire duration is in seconds for simple decrement, 
					   increment (++,--) operations. */
					_durationAsSeconds = DurationAsBulkSeconds(_chosenTimer.Duration);

					_upCount = 0;
					_countUpDurationTarget = _durationAsSeconds;

					//start the timers that will raise an 'elapsed' event every 1000 milliseconds (1 second)
					countTimer.Enabled = true;
					countTimer.Start();

					break;
				case TimerState.TickFromPaused:
					//retrieve bulk seconds cached when timers was STOPPED (paused)
					string selectedTimer = _chosenTimer.Name;
					bool resumingTimer = _lapsesByNameDict.TryGetValue(selectedTimer, out int timerCount);

					int newBulkSeconds = DurationAsBulkSeconds(_chosenTimer.Duration);
					//set _durationAsSeconds/_upCount to cached bulk seconds
					if ( _countDown )
						_durationAsSeconds = resumingTimer ? timerCount : newBulkSeconds;
					else
						_upCount = resumingTimer ? timerCount : 0;

					//re-enable timers
					countTimer.Enabled = true;
					countTimer.Start();

					break;
				case TimerState.Stopped:
				default:

					break;
			}

			//suspend value changing of timerNamesList list box
			timerNamesList.SelectionMode = SelectionMode.None;
			//suspend value changing of timerDurationsList list box
			timerDurationsList.SelectionMode = SelectionMode.None;

			//indicate restricted (disallowed) controls while timers ticks
			ToggleCursorOfMainControls(ControlEngaged.StartButton, Cursors.No, Cursors.Default);

			//update the 'active timer' label text
			activeTimerTextBox.Text = _chosenTimer.Name;

			_timerState = TimerState.Ticking;
		}

		/// <summary>
		/// Toggles the mouse cursor of the form controls according to the 
		/// current timers state, as toggled by the main controls, START, and
		/// STOP. E.g. you could decide that when pressing 'START', the 'STOP'
		/// button mouse cursor is set to 'default' (normal), and the rest
		/// of the controls (including START) has their mouse cursors set
		/// to 'no' (disallowed).
		/// </summary>
		/// <param name="controlEngaged">Either the STOP, or START buttons.</param>
		/// <param name="cursorValue">The cursor value to set for all controls
		/// who are not one of the 'major' (START/STOP) controls.</param>
		/// <param name="invCursorValue">The cursor value to set for the
		/// other (opposing) major control. E.g. if 
		/// <paramref name="controlEngaged"/> is the START button, then
		/// the STOP button mouse cursor is set to whatever
		/// <paramref name="invCursorValue"/>is.</param>
		private void ToggleCursorOfMainControls( 
			ControlEngaged controlEngaged, 
			Cursor cursorValue,
			Cursor invCursorValue )
		{
			switch ( controlEngaged )
			{
				case ControlEngaged.StartButton:
					stopButton.Cursor = invCursorValue;

					startButton.Cursor = cursorValue;
					resetButton.Cursor = cursorValue;
					countInverseBtn.Cursor = cursorValue;
					navigateUpBtn.Cursor = cursorValue;
					navigateDwnBtn.Cursor = cursorValue;
					timerNameEntry.Cursor = cursorValue;
					timerDurationEntry.Cursor = cursorValue;
					timerAddBtn.Cursor = cursorValue;
					chooseAudioBtn.Cursor = cursorValue;
					clearTimersListBtn.Cursor = cursorValue;
					removeTimerBtn.Cursor = cursorValue;
					timerNamesList.Cursor = cursorValue;
					timerDurationsList.Cursor = cursorValue;
					saveProfileBtn.Cursor = cursorValue;
					loadProfileBtn.Cursor = cursorValue;
					saveLapsesCheckBox.Cursor = cursorValue;

					break;
				case ControlEngaged.StopButton:
					stopButton.Cursor = invCursorValue;

					startButton.Cursor = cursorValue;
					resetButton.Cursor = cursorValue;
					countInverseBtn.Cursor = cursorValue;
					navigateUpBtn.Cursor = cursorValue;
					navigateDwnBtn.Cursor = cursorValue;
					timerNameEntry.Cursor = cursorValue;
					timerDurationEntry.Cursor = cursorValue;
					timerAddBtn.Cursor = cursorValue;
					chooseAudioBtn.Cursor = cursorValue;
					clearTimersListBtn.Cursor = cursorValue;
					removeTimerBtn.Cursor = cursorValue;
					timerNamesList.Cursor = cursorValue;
					timerDurationsList.Cursor = cursorValue;
					saveProfileBtn.Cursor = cursorValue;
					loadProfileBtn.Cursor = cursorValue;
					saveLapsesCheckBox.Cursor = cursorValue;

					break;
			}
		}

		// handles event raised whenever the ticker control's set interval elapses
		private void countTimer_Tick(object sender, EventArgs e)
		{
			PlayExpirationAlarm();

			/* Per user toggle, a count down/up method will handle the '1 second elapsed' event,
			   by either counting down from the 'durationAsSeconds' by decrementing 1, or counting 
			   up from zero (0) to 'durationAsSeconds'. Then after ticking by 1 second, we need to
			   reflect the increment/decrement in the 'timerDisplay' text. */

			//if user did not toggle countUp
			if ( _countDown )
			{
				//decrement the 'durationAsSeconds' by 1
				_durationAsSeconds--;
				bool negative = _durationAsSeconds < 1;
				_durationAsSeconds = negative ? 0 : _durationAsSeconds; /* Since Math.Clamp() 
				                                                           is missing */
				FormatTimeFromBulkSeconds(_durationAsSeconds, ref _formattedColumns);
			}
			//else, user toggled countUp is true,
			else
			{
				//increment the running '_upCount' by 1
				_upCount++;
				bool exceedsDuration = _upCount > _durationAsSeconds;
				_upCount = exceedsDuration ? _durationAsSeconds : _upCount; /* Since Math.Clamp()
				                                                               is missing */
				FormatTimeFromBulkSeconds(_upCount, ref _formattedColumns);
			}

			//rebuild 60 based updated 'ChosenTimer.Duration' with corresponding columns
			//format 2 character hour, minute and seconds columns (hh:mm:ss)
			timerDisplay.Text = FormatHhMmSsTime(
				_formattedColumns.Hours, _formattedColumns.Minutes, _formattedColumns.Seconds);
		}

		/// <summary>
		/// Sound alarm if timers duration expired (counted down to zero or up to duration).
		/// </summary>
		private void PlayExpirationAlarm()
		{
			bool countedDownDuration = _durationAsSeconds <= 0;
			bool countedUpDuration = _upCount >= _countUpDurationTarget;

			bool durationElapsed = _countDown ? countedDownDuration : countedUpDuration;
			if ( !durationElapsed ) return;

			//pause ticker control to stop ticking during alarm period
			countTimer.Stop();

			//try to assign audio file to play as alarm siren
			_soundPlayer.SoundLocation = _selectedAudio.FullPath;
			bool audioFileSelected = !_soundPlayer.SoundLocation.Equals("");
			if ( !audioFileSelected ) return;

			//raise alarm 
			bool mutedAlarmAudio = muteBtn.Checked;
			if ( !mutedAlarmAudio ) _soundPlayer.PlayLooping();

			//show alarm window
			_alarmAlertWindow.Show(this);
			//AlarmNotifyingWindow alarmWin = new AlarmNotifyingWindow(this);
			DialogResult alarmMsgBoxResult = MessageBox.Show(
				_alarmMsgBoxInfo.Message, _alarmMsgBoxInfo.Caption, _alarmMsgBoxInfo.Buttons);
			//if clicked 'ok' in message box,
			bool stopAlarm = DialogResult.OK == alarmMsgBoxResult;
			if ( !stopAlarm ) return;

			//hide alarmAlertWindow
			_alarmAlertWindow.Hide();
			//stop _soundPlayer
			_soundPlayer.Stop();
			//mimic pressing of 'STOP' timer button
			StopTimer();
		}

		// user intends to reset the chosen timers to its initial (input) duration
		private void stopButton_Click(object sender, EventArgs e)
		{
			StopTimer();
		}

		private void StopTimer()
		{
			switch ( _timerState )
			{
				//if already in STOPPED state, do nothing
				case TimerState.Stopped:

					return;
				case TimerState.Ticking:
				case TimerState.TickFromBeginning:
				case TimerState.TickFromPaused:
				default:

					break;
			}

			//change the text and image of the 'startButton' to read 'pause'
			countTimer.Enabled = false;
			_soundPlayer.Stop();

			/* Map name:duration to persist count beyond pressing 'STOP' (until pressing
			   'RESET') and continue despite switching timers */
			int timerCount = _countDown ? _durationAsSeconds : _upCount;
			_lapsesByNameDict[_chosenTimer.Name] = timerCount;

			//re-enable value changing of timerNamesList list box
			timerNamesList.SelectionMode = SelectionMode.One;
			//re-enable value changing of timerDurationsList list box
			timerDurationsList.SelectionMode = SelectionMode.One;

			_timerState = TimerState.Stopped;

			//indicate restored responsivenes of respective controls
			ToggleCursorOfMainControls(ControlEngaged.StopButton, Cursors.Default, Cursors.No);
		}

		private void resetButton_Click(object sender, EventArgs e)
		{
			switch ( _timerState )
			{
				//if already reset OR ticking, do nothing
				case TimerState.Ticking:

					return;
				case TimerState.Stopped:
				case TimerState.TickFromPaused:
				case TimerState.TickFromBeginning:
				default:

					break;
			}

			//set value (duration) of name:duration mapping to _chosenTimer.Duration
			int inputBulkSeconds = DurationAsBulkSeconds(_chosenTimer.Duration);
			_lapsesByNameDict[_chosenTimer.Name] = inputBulkSeconds;
			//set timerDisplay.Text to _chosenTimer.Duration
			timerDisplay.Text = _chosenTimer.Duration;

			//reset timer state
			_timerState = TimerState.TickFromBeginning;
		}

		// user intends choose the timers above current in the timers list 
		private void navigateUpBtn_Click(object sender, EventArgs e)
		{
			switch ( _timerState )
			{
				//if TICKING, do nothing
				case TimerState.Ticking:

					return;
				case TimerState.TickFromBeginning:
				case TimerState.TickFromPaused:
				case TimerState.Stopped:
				default:

					break;
			}

			int timerNamesCount = timerNamesList.Items.Count;
			int lastTimerI = timerNamesCount - 1;
			bool emptyTimersList = timerNamesCount < 1;
			//if 'timerNamesList' is empty, return
			if ( emptyTimersList ) return;

			string selectedName = string.Empty;

			bool noSelectedName = timerNamesList.SelectedItem == null;
			bool noSelectedDuration = timerDurationsList.SelectedItem == null;
			bool noSelectedTimer = noSelectedName || noSelectedDuration;
			//if there is no selected item in 'timerNamesList'
			if ( noSelectedTimer )
			{
				//set the very last (list bottom) item as selected
				timerNamesList.SelectedItem = noSelectedName ? timerNamesList.Items[lastTimerI] : timerNamesList.SelectedItem;
				timerDurationsList.SelectedItem = noSelectedDuration ? timerDurationsList.Items[lastTimerI] : timerDurationsList.SelectedItem;
				selectedName = timerNamesList.SelectedItem.ToString();

				//set the selectedIndex accordingly
				timerNamesList.SelectedIndex = noSelectedName ? lastTimerI : timerNamesList.SelectedIndex;
				timerDurationsList.SelectedIndex = noSelectedDuration ? lastTimerI : timerDurationsList.SelectedIndex;
			}
			//else,
			else
			{
				bool selectedAName = timerNamesList.SelectedItem != null;
				int selectedNameI = timerNamesList.SelectedIndex;
				int selectedDurationI = timerDurationsList.SelectedIndex;
				//get index of selected item
				int selectedItemI = selectedAName ? selectedNameI : selectedDurationI;
				//decrement said index
				selectedItemI--;
				bool neg = selectedItemI < 0;
				//clamp above -1 & assist loop to bottom,
				//loop back to bottom if reached top
				selectedItemI = neg ? lastTimerI : selectedItemI;

				//set selected item to Items' item at decremented index
				timerNamesList.SelectedItem = timerNamesList.Items[selectedItemI];
				timerDurationsList.SelectedItem = timerDurationsList.Items[selectedItemI];
				selectedName = timerNamesList.SelectedItem.ToString();

				//set the selectedIndex accordingly
				timerNamesList.SelectedIndex = selectedItemI;
				timerDurationsList.SelectedIndex = selectedItemI;
			}

			//set timer state according to the selected timer lapse 'state'
			bool stoppedWithoutReset = _lapsesByNameDict.TryGetValue(selectedName, out int durationAsSeconds);
			_timerState = stoppedWithoutReset ? TimerState.TickFromPaused : TimerState.TickFromBeginning;
		}

		// user intends choose the timers below current in the timers list 
		private void navigateDwnBtn_Click(object sender, EventArgs e)
		{
			switch ( _timerState )
			{
				//if TICKING, do nothing
				case TimerState.Ticking:

					return;
				case TimerState.TickFromBeginning:
				case TimerState.TickFromPaused:
				case TimerState.Stopped:
				default:

					break;
			}

			int timerNamesCount = timerNamesList.Items.Count;
			bool emptyTimersList = timerNamesCount < 1;
			//if 'timerNamesList' is empty, return
			if ( emptyTimersList ) return;

			string selectedName = string.Empty;

			bool noSelectedName = timerNamesList.SelectedItem == null;
			bool noSelectedDuration = timerDurationsList.SelectedItem == null;
			bool noSelectedTimer = noSelectedName || noSelectedDuration;
			//if there is no selected item in 'timerNamesList'
			if ( noSelectedTimer )
			{
				//set the very first (list top) item as selected
				timerNamesList.SelectedItem = noSelectedName ? timerNamesList.Items[0] : timerNamesList.SelectedItem;
				timerDurationsList.SelectedItem = noSelectedDuration ? timerDurationsList.Items[0] : timerDurationsList.SelectedItem;
				selectedName = timerNamesList.SelectedItem.ToString();

				//set the selectedIndex accordingly
				timerNamesList.SelectedIndex = noSelectedName ? 0 : timerNamesList.SelectedIndex;
				timerDurationsList.SelectedIndex = noSelectedDuration ? 0 : timerDurationsList.SelectedIndex;
			}
			//else,
			else
			{
				bool selectedAName = timerNamesList.SelectedItem != null;
				int selectedNameI = timerNamesList.SelectedIndex;
				int selectedDurationI = timerDurationsList.SelectedIndex;
				//get index of selected item
				int selectedItemI = selectedAName ? selectedNameI : selectedDurationI;
				//increment said index
				selectedItemI++;
				bool indexOutOfBounds = selectedItemI >= timerNamesCount;
				//clamp below lastI & assist loop to top,
				//loop back to top if reached bottom
				selectedItemI = indexOutOfBounds ? 0 : selectedItemI;

				//set selected item to Items' item at incremented index
				timerNamesList.SelectedItem = timerNamesList.Items[selectedItemI];
				timerDurationsList.SelectedItem = timerDurationsList.Items[selectedItemI];
				selectedName = timerNamesList.SelectedItem.ToString();

				//set the selectedIndex accordingly
				timerNamesList.SelectedIndex = selectedItemI;
				timerDurationsList.SelectedIndex = selectedItemI;
			}

			//set timer state according to the selected timer lapse 'state'
			bool stoppedWithoutReset = _lapsesByNameDict.TryGetValue(selectedName, out int durationAsSeconds);
			_timerState = stoppedWithoutReset ? TimerState.TickFromPaused : TimerState.TickFromBeginning;
		}

		// user intends to close the timers form window
		private void DigitalCountTimer_FormClosing(object sender, FormClosingEventArgs e)
		{
			#region SAVE LAPSES TO FILE

			bool saveTimerLapses = saveLapsesCheckBox.Checked;
			//if 'saveLapesesCheckBox is NOT checked, return
			if ( !saveTimerLapses ) return;

			//if no profile to associate saved lapses with, open dialogs
			bool activeProfile = File.Exists(_currentProfilePath);
			if ( activeProfile )
			{
				SaveTimerLapses(_currentProfilePath);
				return;
			}

			//show message box telling user of need for profile to assign to
			bool willSaveProfile = MessageBox.Show(_assignProfileMsgBoxInfo.Message, _assignProfileMsgBoxInfo.Caption, _assignProfileMsgBoxInfo.Buttons) == DialogResult.OK;
			if ( !willSaveProfile ) return;

			/* Since no active profile was detected, it means user entered 
			   timers without saving them to a profile. So give user a chance
			   to save entered timers to a profile. */
			//open 'save profile' dialog in case no profile to load exists
			bool enteredProfileName = saveProfileDialog.ShowDialog() == DialogResult.OK;
			if ( !enteredProfileName ) return;

			string profileFullPath = saveProfileDialog.FileName;
			string profileName = Path.GetFileName(profileFullPath);
			SaveTimersList(profileName);
			SaveChosenAudio(profileName);
			SaveTimerLapses(profileFullPath);

			#endregion
		}

		private void SaveTimerLapses(string profileFullPath)
		{
			string nameAndSecondsStr = string.Empty;
			//get a list of the keys from _lapsesByNameDict
			Dictionary<string, int>.KeyCollection keys = _lapsesByNameDict.Keys;
			string[] keysArray = keys.ToArray<string>();
			//convert colon separated timers name and bulkSeconds string
			foreach ( string name in keysArray )
			{
				string durationStr = _lapsesByNameDict[name].ToString();
				nameAndSecondsStr += $"{name}{DELIMITER_TIMER_LAPSES}{durationStr}{Environment.NewLine}";
			}

			//build lapses file name in lapses dir, with suffixed profile name 
			string profileFileName = Path.GetFileName(profileFullPath);
			string lapsesFilePath = SuffixFileAtPath(
				profileFileName, DEFAULT_PROFILE_FILE_EXT, LAPSES_DIR_PATH, LAPSES_SAVE_FILE_SUFFIX);
			//write to file
			File.WriteAllText(lapsesFilePath, nameAndSecondsStr);
		}

		// user intends to save the current list of timers and chosen audio
		private void saveProfileBtn_Click(object sender, EventArgs e)
		{
			switch ( _timerState )
			{
				case TimerState.Ticking:

					return;
				case TimerState.Stopped:
				case TimerState.TickFromPaused:
				case TimerState.TickFromBeginning:
				default:

					break;
			}

			//open 'saveProfileDialog' save file dialog so user sets PROFILES_DIR_PATH
			bool specifiedSaveFile = saveProfileDialog.ShowDialog() == DialogResult.OK;
			//if user did NOT press the 'ok' button of said dialog, return
			if ( !specifiedSaveFile ) return;

			string profileFileName = Path.GetFileName(saveProfileDialog.FileName);

			SaveTimersList(profileFileName);

			SaveChosenAudio(profileFileName);
		}

		private void SaveTimersList(string fileName)
		{
			ListBox.ObjectCollection timerNameItems = timerNamesList.Items;
			ListBox.ObjectCollection timerDurationItems = timerDurationsList.Items;
			string timers = string.Empty;
			for ( int nameI = 0; nameI < timerNameItems.Count; nameI++ )
			{
				//get timers name from timerNamesList
				string timerName = timerNameItems[nameI].ToString();
				//get timers duration from from timerDurationsList
				string timerDuration = timerDurationItems[nameI].ToString();
				//write name:duration pair to file
				timers += $"{timerName}|{timerDuration}{Environment.NewLine}";
			}
			//now that user specified profile save file name,
			//build the file path
			string saveFilePath = Path.Combine(PROFILES_DIR_PATH, fileName);
			//write to file
			File.WriteAllText(saveFilePath, timers);

			//cache current profile path
			_currentProfilePath = saveFilePath;
		}

		private void SaveChosenAudio(string profileFileName)
		{
			bool noAudioSelected = string.IsNullOrEmpty(_selectedAudio.FullPath);
			//to preserve audio select start directory, if no audio path selected, return
			if ( noAudioSelected ) return;

			//use suffix appended file name (path) to open (create) audio save file
			string audioSaveFilePath = SuffixFileAtPath(
				profileFileName, 
				DEFAULT_PROFILE_FILE_EXT, 
				PROFILES_AUDIO_DIR_PATH,
				AUDIO_SAVE_FILE_SUFFIX);
			//write to file
			File.WriteAllText(audioSaveFilePath, _selectedAudio.FullPath);
		}

		/// <summary>
		/// Adds a suffix to the provide file name, then creates a file path with
		/// the provided directory path.
		/// </summary>
		/// <param name="fileName">The file name to which will be 
		/// added the <paramref name="suffixToAdd"/>.</param>
		/// <param name="fileExtension">The file extension shared by
		/// both the existing <paramref name="fileName"/>, and which should
		/// be a part of the <paramref name="suffixToAdd"/> by default.</param>
		/// <param name="directoryPath">Path to the directory in which the
		/// suffixed <paramref name="fileName"/> will belong (its path).</param>
		/// <param name="suffixToAdd">The portion that will be added to
		/// the back of <paramref name="fileName"/>.</param>
		/// <returns></returns>
		private string SuffixFileAtPath(
			string fileName,
			string fileExtension,
			string directoryPath, 
			string suffixToAdd)
		{
			//subtract extension from 'fileName'
			int fileNameLen = fileName.Length;
			int fileExtLen = fileExtension.Length;
			int nameLenMinusExt = fileNameLen - fileExtLen;
			string nameMinusExt = fileName.Substring(0, nameLenMinusExt);
			//add suffix to extensionless 'fileName'
			string namePlusSuffix = $"{nameMinusExt}{suffixToAdd}";
			//combine directoryPath with final 'fileName' to get 'suffixedFileName'
			string suffixedFileAtPath = Path.Combine(directoryPath, namePlusSuffix);

			return suffixedFileAtPath;

			/*int pathLen = pathToFile.Length;
			int fileExtLen = fileExtension.Length;
			int lenMinusExt = pathLen - fileExtLen;
			string pathMinusExt = pathToFile.Substring(0, lenMinusExt);
			string suffixedFileName = $"{pathMinusExt}{suffixToAdd}";

			return suffixedFileName;*/
		}

		// user intends to load a previously saved list of timers and chosen audio
		private void loadProfileBtn_Click(object sender, EventArgs e)
		{
			switch (_timerState)
			{
				case TimerState.Ticking:

					return;
				case TimerState.Stopped:
				case TimerState.TickFromPaused:
				case TimerState.TickFromBeginning:
				default:

					break;
			}

			//open 'loadProfileDialog' load file dialog so user sets PROFILE_LOAD_PATH 
			bool specifiedLoadFile = loadProfileDiaglog.ShowDialog() == DialogResult.OK;
			if ( !specifiedLoadFile ) return;

			string profilePath = loadProfileDiaglog.FileName;

			LoadTimersList(profilePath);
			
			LoaderTimerLapses(profilePath);

			LoadTimerAudio(profilePath);

			/*//indicate restricted (disallowed) controls while timers ticks
			ToggleCursorOfMainControls(ControlEngaged.StartButton, Cursors.No, Cursors.Default);*/
		}

		private void LoadTimersList(string profilePath)
		{
			//clear out timers list in case of consecutive profile loading
			timerNamesList.Items.Clear();
			timerDurationsList.Items.Clear();

			//now that user specified profile load file name,
			//open that file by building its path
			string userSetFilePath = Path.Combine(PROFILES_DIR_PATH, profilePath);
			//open file stream
			using ( StreamReader reader = new StreamReader(userSetFilePath) )
			{
				string line;
				while ( (line = reader.ReadLine()) != null )
				{
					//split line at profiles delimiter
					string[] timerConjugate = line.Split(DELIMITER_TIMER_PROFILES);
					//add timers name to timerNamesList.Items[i]
					timerNamesList.Items.Add(timerConjugate[0]);
					//add timers duration to timerDurationsList.Items[i]
					timerDurationsList.Items.Add(timerConjugate[1]);
				}
			}

			//cache loaded profile path as current profile path
			_currentProfilePath = userSetFilePath;
		}

		private void LoaderTimerLapses(string profilePath)
		{
			//clear the lapses by name dictionary in case current timers were lapsed
			_lapsesByNameDict.Clear();

			string profileFileName = Path.GetFileName(profilePath);
			string lapsesFilePath = SuffixFileAtPath(
				profileFileName,
				DEFAULT_PROFILE_FILE_EXT,
				LAPSES_DIR_PATH,
				LAPSES_SAVE_FILE_SUFFIX);

			bool availableLapses = File.Exists(lapsesFilePath);
			if ( availableLapses )
			{

				using ( StreamReader lapseReader = new StreamReader(lapsesFilePath) )
				{
					string line;
					while ( (line = lapseReader.ReadLine()) != null )
					{
						string[] lapsesConjugate = line.Split(DELIMITER_TIMER_LAPSES);
						string timerName = lapsesConjugate[0];
						string bulkSeconds = lapsesConjugate[1];
						_lapsesByNameDict[timerName] = int.Parse(bulkSeconds);
					}
				}
			}
		}

		private void LoadTimerAudio(string profilePath)
		{
			string profileFileName = Path.GetFileName(profilePath);
			string audioSaveFilePath = SuffixFileAtPath(
				profileFileName,
				DEFAULT_PROFILE_FILE_EXT,
				PROFILES_AUDIO_DIR_PATH,
				AUDIO_SAVE_FILE_SUFFIX);

			bool availableAudioFile = File.Exists(audioSaveFilePath);
			if ( !availableAudioFile ) return;

			using ( StreamReader profAudioReader = new StreamReader(audioSaveFilePath) )
			{
				string line;
				while ( (line = profAudioReader.ReadLine()) != null )
				{
					_selectedAudio.FullPath = line;
				}

				//set 'selectedAudioName' text to read path to show loading
				string audioFilePath = _selectedAudio.FullPath;
				string[] dirsToAudioFile = audioFilePath.Split('\\');
				int lastI = dirsToAudioFile.Length - 1;
				string justAudioFileName = dirsToAudioFile[lastI];
				selectedAudioName.Text = justAudioFileName;
			}
		}
	}
}
