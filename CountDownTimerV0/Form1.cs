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
		private const string PROFILE_DIRECTORY = @"\Count Down Up Timer\Profiles\";
		private string PROFILE_SAVE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + PROFILE_DIRECTORY;
		private string PROFILE_LOAD_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + PROFILE_DIRECTORY;

		/*private const string LAPSES_MEM_FILE_NAME = "Remembered Lapses.txt";
		private string LAPSES_MEM_FILE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + LAPSES_MEM_FILE_NAME;*/
		private const string LAPSES_MEM_FILE_PATH = @"C:\Users\GDK\Documents\Count Down Up Timer\Remembered Lapses.txt";
		/*private const string SAVE_LAPSES_FLAG_FILE_NAME = "Save Lapses OnExit Flag.txt";
		private string SAVE_LAPSES_ON_EXIT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + SAVE_LAPSES_FLAG_FILE_NAME;*/
		private const string SAVE_LAPSES_ON_EXIT_PATH = @"C:\Users\GDK\Documents\Count Down Up Timer\Save Lapses OnExit Flag.txt";

		private const string DEFAULT_PROFILE_FILE_EXT = ".txt";
		private const string AUDIO_SAVE_FILE_SUFFIX = "_chosenAudio.txt";

		private bool _saveLapsesOnExit;
		private string _loadedProfilePath;

		private const char DELIMITER_TIMER_LAPSES = ':';
		private const char DELIMITER_TIMER_PROFILES = '|';

		private const string TIMER_DISPLAY_DEFAULT_STRING = "00:00:00";
		private const string NAME_ENTRY_PROMPT_STRING = "[Enter Name]";
		private const string DURATION_ENTRY_PROMPT_STRING = "00:00:00";

		private const string MISSING_NAME_CAPTION = "Missing Name Input";
		private const string MISSING_NAME_MESSAGE = "You did not enter a timer name. Retry.";

		private const string MISSING_DURATION_CAPTION = "Missing Duration Input";
		private const string MISSING_DURATION_MESSAGE = "You did not enter a timer duration. Retry.";

		private const string SELECT_A_TIMER_CAPTION = "Select a Timer";
		private const string SELECT_A_TIMER_MESSAGE = "You need to select a timer to start.";

		private const string ALARM_CAPTION = "Timer Elapsed";
		private const string ALARM_MESSAGE = "End Alarm.";

		private const string INCORRECT_FORMAT_TOOLTIP = "Incorrect format, use empty default as template";
		private const int INCORRECT_FORMAT_TOOLTIP_DUR = 3000;

		Form _alarmAlertWindow;
		
		private const int MINUTES_PER_HOUR = 60;
		private const int SECONDS_PER_MINUTE = 60;

		private FormattedTimeColumns _formattedColumns;
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
			Stopped,
			Reset
		}
		private TimerState _timerState = TimerState.Stopped;

		private enum Start
		{
			FromBeginning,
			FromPaused
		}
		private Start _startButtonState;

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
			TimerDurationsList
		}

		#endregion

		public DigitalCountTimer()
		{
			InitializeComponent();

			SetupForm();
		}

		private void SetupForm()
		{
			SetTabIndices();

			MoveControlsToBack();

			_formattedColumns = new FormattedTimeColumns();

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

			/* LOAD TIMER VALUE TO BEGIN COUNT DOWN/UP */
			//get the count up/down time from the profile of timers
			/* f.y.i. a profile is just a .txt file of 'newline' separated strings
			   each containing the name of the timer and a 'comma' separating the 
			   timer length. The timer length is just an int number counted up to 
			   (-= 1 every 1000 miliseconds), or that is counted 
			   down from (+= 1 every 1000 miliseconds). */

			/* SET STARTING STATE */
			_startButtonState = Start.FromBeginning;

			timerNameEntry.Focus();

			/* CREATE MAIN DIRECTORY FOR SAVING/LOADING PROFILES, LAPSES, AND FLAGS */
			//Directory.CreateDirectory

			/* SET INITIAL VALUES FOR FILE OPEN AND SAVE DIALOG CONTROLLERS */
			saveProfileDialog.InitialDirectory = PROFILE_SAVE_PATH;
			loadProfileDiaglog.InitialDirectory = PROFILE_LOAD_PATH;

			saveProfileDialog.DefaultExt = DEFAULT_PROFILE_FILE_EXT;
			loadProfileDiaglog.DefaultExt = DEFAULT_PROFILE_FILE_EXT;
			// CACHE THE SELECTED PATH PROPERTY VALUE OF audioFolderBrowser?

			LoadSavedTimerLapses();
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
		
		private void LoadSavedTimerLapses()
		{
			//if NOT toggled 'save' lapses on last exit, return
			if ( !ToggledSaveLapsesOnPrevExit() ) return;

			bool timerLapsesFileExists = File.Exists(LAPSES_MEM_FILE_PATH);
			if ( !timerLapsesFileExists ) return;

			string entireTimersStr = string.Empty;

			//open the saved file at LAPSES_MEM_FILE_PATH
			using ( StreamReader reader = new StreamReader(LAPSES_MEM_FILE_PATH) )
			{
				string line;
				while ( (line = reader.ReadLine()) != null )
				{
					//timerDisplay.Text = line; // DEBUGGING - DELETE!!!
					entireTimersStr += $"{line}{Environment.NewLine}";

					/* TRY ADDING THE BELOW */
					/*//split string at colons separating name and seconds
					string[] timerAndBulkSeconds = line.Split(':');
					//make _timerSecondsByNameDict name:duration entry
					//i.e. 1st elem of split = key and 2nd = value
					string timerName = timerAndBulkSeconds[0];
					_timerSecondsByNameDict[timerName] = int.Parse(timerAndBulkSeconds[1]);*/
				}
			}

			/* THE BELOW MIGHT NOT BE NECESSARY */
			//split the line (string) at the new lines
			char[] newLineChars = Environment.NewLine.ToCharArray();
			string[] splitTimers = entireTimersStr.Split(newLineChars, StringSplitOptions.RemoveEmptyEntries);
			foreach ( string lapse in splitTimers )
			{
				//skip empty strings introduced by Environment.NewLine
				//if ( string.IsNullOrEmpty(lapse) ) continue;

				//split string at colons separating name and seconds
				string[] timerAndBulkSeconds = lapse.Split(DELIMITER_TIMER_LAPSES);
				//make _timerSecondsByNameDict name:duration entry
				//i.e. 1st elem of split = key and 2nd = value
				string timerName = timerAndBulkSeconds[0];
				_lapsesByNameDict[timerName] = int.Parse(timerAndBulkSeconds[1]);
			}

			//LoadLapsesIntoMappingCacheDict();
		}

		private bool ToggledSaveLapsesOnPrevExit()
		{
			bool flagFileExists = File.Exists(SAVE_LAPSES_ON_EXIT_PATH);
			if ( !flagFileExists ) return false;

			int streamLenth;
			using ( FileStream stream = File.OpenRead(SAVE_LAPSES_ON_EXIT_PATH) ) 
			{
				streamLenth = (int)stream.Length;
			}

			string extractedFlagStr = string.Empty;
			//open _saveLapsesOnExit file at SAVE_LAPSES_ON_EXIT_PATH
			using ( StreamReader reader = new StreamReader(SAVE_LAPSES_ON_EXIT_PATH) )
			{
				string line;
				while ( (line = reader.ReadLine()) != null )
				{
					//string flagString = strEncode.GetString(flagBytes);
					extractedFlagStr += $"{line}{Environment.NewLine}";
				}

				bool toggledSaveLapses = false;
				char[] newLineChars = Environment.NewLine.ToCharArray();
				string[] splitNewLines = extractedFlagStr.Split(newLineChars, StringSplitOptions.RemoveEmptyEntries);
				foreach ( string splitStr in splitNewLines )
				{
					//skip empty string introducted by Environment.NewLine
					//if ( string.IsNullOrEmpty(splitStr) ) continue;

					//assuming dict-like mapping of flag name and value,
					//so split the string at the colon
					string[] flagAndVal = splitStr.Split(DELIMITER_TIMER_LAPSES);
					//get the second value of the split consequent array
					string flagValStr = flagAndVal[1];
					//assuming the value is a binary 0 or 1, so...
					int flagValue = int.Parse(flagValStr);
					//if the parsed int is 0, it means the flag to save lapses was FALSE,
					toggledSaveLapses = flagValue == 1;
				}

				return toggledSaveLapses;
			}
		}

		// user clicked in text field to begin entering timer name
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

		// user clicked in text field to begin entering timer duration
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

			//if correct timer duration format, return
			bool timerHhMmSsFormat = Regex.IsMatch(textBoxLeft.Text, requiredFormatRegex);
			if ( timerHhMmSsFormat ) return;

			//show popup prompting user to re-input with correct hh:mm:ss format
			//timerFormatTip.SetToolTip(textBoxLeft, "Incorrect format");
			timerFormatTip.Show(
				INCORRECT_FORMAT_TOOLTIP, textBoxLeft, INCORRECT_FORMAT_TOOLTIP_DUR);
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

		// user finished entering duration and presses 'Enter' to move tab focus to timer submit btn
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

			/* Enforce correct timer duration format */
			int bulkSeconds = DurationAsBulkSeconds(timerDurationEntry.Text);
			FormatTimeFromBulkSeconds(bulkSeconds, ref _formattedColumns);

			//reasign 60 based duration to 'timerDurationEntry' 
			//format 2 character hour, minute and seconds columns (hh:mm:ss)
			string rebuiltDurationString = FormatHhMmSsTime(
				_formattedColumns.Hours, _formattedColumns.Minutes, _formattedColumns.Seconds);

			timerDurationEntry.Text = rebuiltDurationString;
		}

		/// <summary>
		/// Takes the string input timer duration and computes the equivalent in seconds
		/// of said entire duration.
		/// </summary>
		/// <param name="durationString">The user input timer duration</param>
		/// <returns></returns>
		private int DurationAsBulkSeconds(string durationString)
		{
			//convert the formatted 'ChosenTimer.Duration' to 'durationAsSeconds' as,
			//hours column to minutes to seconds, i.e. ( (hours * 60mins) * 60secs ) as 'hoursAsSeconds', 
			_durationTimeColumns = durationString.Split(':');
			string hoursColumn = _durationTimeColumns[0];
			int hours = int.Parse(hoursColumn);
			int hoursInSeconds = (hours * MINUTES_PER_HOUR) * SECONDS_PER_MINUTE;
			//PLUS minutes column to seconds, i.e. (mins * 60secs) as 'minutesAsSeconds',
			string minutesColumn = _durationTimeColumns[1];
			int minutes = int.Parse(minutesColumn);
			int minutesInSeconds = minutes * SECONDS_PER_MINUTE;
			//PLUS seconds column
			string secondsColumn = _durationTimeColumns[2];
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

		// user clicks to submit timer (name+duration) to 'Timers' list
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

			//focus on 'timerNameEntry' control to ready for next timer entry
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
		/// event of an invalid timer name or duration entry.</param>
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
			//pause painting list box while adding text
			listBox.BeginUpdate();
			//get the text value from the Text property of the textBox control
			//add text value to list box
			string textBoxText = textBox.Text;
			listBox.Items.Add(textBoxText);
			//reset the Text property of textBox control to the defaultBoxText
			textBox.Text = defaultTextBoxString;
			//un-pause painting list box
			listBox.EndUpdate();
		}

		// so user can select timer by either clicking on its name or duration, then press 'Start'
		private void timerNamesList_SelectedValueChanged(object sender, EventArgs e)
		{
			/* only the name at the selected row will be highlighted, but we also
			   want to highlight the corresponding duration in timerDurationsList, so*/
			//get the selected index in timerNamesList
			int selectedNameI = timerNamesList.SelectedIndex;
			bool unSelected = selectedNameI < 0;
			if ( unSelected ) return;

			//set selected of timerDurationsList to selected index above
			timerDurationsList.SelectedItem = timerDurationsList.Items[selectedNameI];

			//add the name and duration of selected to the 'ChosenTimer' struct
			string selectedName = timerNamesList.Items[selectedNameI].ToString();
			string selectedDuration = timerDurationsList.Items[selectedNameI].ToString();
			_chosenTimer.Name = selectedName;
			_chosenTimer.Duration = selectedDuration;

			//set 'timerDisplay' control text to the 'Duration' property of 'ChosenTimer' struct
			bool stoppedWithoutReset = _lapsesByNameDict.TryGetValue(selectedName, out int durationAsSeconds);
			string timerDuration = stoppedWithoutReset ? FormatTimeFromBulkSeconds(durationAsSeconds, ref _formattedColumns, true) : selectedDuration;

			timerDisplay.Text = timerDuration;
		}

		// so user can select timer by either clicking on its name or duration, then press 'Start'
		private void timerDurationsList_SelectedValueChanged(object sender, EventArgs e)
		{
			/* only the duration at the selected row will be highlighted, but we also
			   want to highlight the corresponding name in timerNamesList, so */
			//get the selected index in timerDurationsList
			int selectedDurationI = timerDurationsList.SelectedIndex;
			bool unSelected = selectedDurationI < 0;
			if ( unSelected ) return;

			//set selected of timerNamesList to selected index above
			timerNamesList.SelectedItem = timerNamesList.Items[selectedDurationI];

			//add the duration and of selected to the 'ChosenTimer' struct
			string selectedDuration = timerDurationsList.Items[selectedDurationI].ToString();
			string selectedName = timerNamesList.Items[selectedDurationI].ToString();
			_chosenTimer.Duration = selectedDuration;
			_chosenTimer.Name = selectedName;

			//set 'timerDisplay' control text to the 'Duration' property of 'ChosenTimer' struct
			bool stoppedWithoutReset = _lapsesByNameDict.TryGetValue(selectedName, out int durationAsSeconds);
			string timerDuration = stoppedWithoutReset ? FormatTimeFromBulkSeconds(durationAsSeconds, ref _formattedColumns, true) : selectedDuration;

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

		// user intends to begin count down/up
		private void startButton_Click(object sender, EventArgs e)
		{
			switch ( _timerState )
			{
				//if already in START state, do nothing 
				case TimerState.Ticking:
					return;
				case TimerState.Stopped:
				default:
					break;
			}

			/* To allow resuming count when clicking to another timer and 
			   back again, have an actively count down/up 'DurationAsBulkSeconds'
			   that is only updated when clicking the 'startButton', not when
			   clicking on a different timer by either the 'timerNamesList' or
			   the 'timerDurationsList'. */

			/* -give the 'startButton' a switch state machine and two enum states.
			   -the first enum state being 'FromBeginning' is the default, with 
			   current implementation as seen below the 'defaultTimerDisplay' guard 
			   clause.
			   -the second enum state being 'FromPause', wherein _durationAsSeconds
			   and _countUpDurationTarget are not recomputed before enabling and
			   starting the countTimer. */
			switch ( _startButtonState )
			{
				case Start.FromBeginning:
					//if no timer is selected from the list
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

					//start the timer that will raise an 'elapsed' event every 1000 milliseconds (1 second)
					countTimer.Enabled = true;
					countTimer.Start();

					break;
				case Start.FromPaused:
					//retrieve bulk seconds cached when timer was STOPPED (paused)
					string selectedTimer = _chosenTimer.Name;
					bool resumingTimer = _lapsesByNameDict.TryGetValue(selectedTimer, out int timerCount);

					int newBulkSeconds = DurationAsBulkSeconds(_chosenTimer.Duration);
					//set _durationAsSeconds/_upCount to cached bulk seconds
					if ( _countDown )
						_durationAsSeconds = resumingTimer ? timerCount : newBulkSeconds;
					else
						_upCount = resumingTimer ? timerCount : 0;

					//re-enable timer
					countTimer.Enabled = true;
					countTimer.Start();
					_startButtonState = Start.FromBeginning;

					break;
				default:
					break;
			}

			//suspend value changing of timerNamesList list box
			timerNamesList.SelectionMode = SelectionMode.None;
			//suspend value changing of timerDurationsList list box
			timerDurationsList.SelectionMode = SelectionMode.None;

			//indicate restricted (disallowed) controls while timer ticks
			ToggleCursorOfMainControls(ControlEngaged.StartButton, Cursors.No, Cursors.Default);

			_timerState = TimerState.Ticking;
		}

		/// <summary>
		/// Toggles the mouse cursor of the form controls according to the 
		/// current timer state, as toggled by the main controls, START, and
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
					clearTimersListBtn.Cursor = cursorValue;
					timerNamesList.Cursor = cursorValue;
					timerDurationsList.Cursor = cursorValue;

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
					clearTimersListBtn.Cursor = cursorValue;
					timerNamesList.Cursor = cursorValue;
					timerDurationsList.Cursor = cursorValue;

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
		/// Sound alarm if timer duration expired (counted down to zero or up to duration).
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
			_soundPlayer.PlayLooping();

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
		}

		// user intends to reset the chosen timer to its initial (input) duration
		private void stopButton_Click(object sender, EventArgs e)
		{
			switch ( _timerState )
			{
				//if already in STOPPED state, do nothing
				case TimerState.Stopped:
					return;
				case TimerState.Ticking:
				default:
					break;
			}

			 /* The continuously running routines started when pressing the 
			    'startButton' are:
					- the 'countTimer', and
					- the '_soundPlayer' as a consequence of the timer
			    So disable those two, then set the 'startButton' state to
			    'Start.FromPaused'. */
			
			//change the text and image of the 'startButton' to read 'pause'
			countTimer.Enabled = false;
			_soundPlayer.Stop();

			/* Map name:duration to persist count beyond pressing 'STOP' (until pressing
			   'RESET') and continue despite switching timers */
			int timerCount = _countDown ? _durationAsSeconds : _upCount;
			_lapsesByNameDict[_chosenTimer.Name] = timerCount;

			//set the state of the 'startButton' to 'FromPause'
			_startButtonState = Start.FromPaused;

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
				case TimerState.Reset:
				case TimerState.Ticking:

					return;
				case TimerState.Stopped:
				default:

					break;
			}

			//set value (duration) of name:duration mapping to _chosenTimer.Duration
			int inputBulkSeconds = DurationAsBulkSeconds(_chosenTimer.Duration);
			_lapsesByNameDict[_chosenTimer.Name] = inputBulkSeconds;
			//set timerDisplay.Text to _chosenTimer.Duration
			timerDisplay.Text = _chosenTimer.Duration;

			_timerState = TimerState.Reset;
		}

		// user intends choose the timer above current in the timers list 
		private void navigateUpBtn_Click(object sender, EventArgs e)
		{
			switch ( _timerState )
			{
				//if TICKING, do nothing
				case TimerState.Ticking:
					return;
				case TimerState.Stopped:
				default:
					break;
			}

			int timerNamesCount = timerNamesList.Items.Count;
			int lastTimerI = timerNamesCount - 1;
			bool emptyTimersList = timerNamesCount < 1;
			//if 'timerNamesList' is empty, return
			if ( emptyTimersList ) return;

			bool noSelectedTimer = timerNamesList.SelectedItem == null;
			//if there is no selected item in 'timerNamesList'
			if ( noSelectedTimer )
				//set the very last (list bottom) item as selected
				timerNamesList.SelectedItem = timerNamesList.Items[lastTimerI];
			//else,
			else
			{
				//get index of selected item
				int selectedItemI = timerNamesList.SelectedIndex;
				//decrement said index
				selectedItemI--;
				bool neg = selectedItemI < 0;
				//clamp above -1 & assist loop to bottom,
				//loop back to bottom if reached top
				selectedItemI = neg ? lastTimerI : selectedItemI; 

				//set selected item to Items' item at decremented index
				timerNamesList.SelectedItem = timerNamesList.Items[selectedItemI];
			}
		}

		// user intends choose the timer below current in the timers list 
		private void navigateDwnBtn_Click(object sender, EventArgs e)
		{
			switch ( _timerState )
			{
				//if TICKING, do nothing
				case TimerState.Ticking:
					return;
				case TimerState.Stopped:
				default:
					break;
			}

			int timerNamesCount = timerNamesList.Items.Count;
			bool emptyTimersList = timerNamesCount < 1;
			//if 'timerNamesList' is empty, return
			if ( emptyTimersList ) return;

			bool noSelectedTimer = timerNamesList.SelectedItem == null;
			//if no selected item in 'timerNamesList'
			if ( noSelectedTimer )
				//set the very first (list top) item to selected
				timerNamesList.SelectedItem = timerNamesList.Items[0];
			//else,
			else
			{
				//get index of selected item
				int selectedItemI = timerNamesList.SelectedIndex;
				//increment said index
				selectedItemI++;
				bool indexOutOfBounds = selectedItemI >= timerNamesCount;
				//clamp below lastI & assist loop to top,
				//loop back to top if reached bottom
				selectedItemI = indexOutOfBounds ? 0 : selectedItemI;

				//set selected item to Items' item at incremented index
				timerNamesList.SelectedItem = timerNamesList.Items[selectedItemI];
			}
		}

		// user intends to close the timer form window
		private void DigitalCountTimer_FormClosing(object sender, FormClosingEventArgs e)
		{
			#region SAVE TO FILE, THE FLAG TELLING TO SAVE LAPSES

			//create string that maps flag name and value, akin to a dict pair
			bool saveLapses = saveLapsesCheckBox.Checked;
			int boolAsBinary = saveLapses ? 1 : 0;
			string flagString = $"_saveLapsesOnExit:{boolAsBinary}";
			//write to file
			File.WriteAllText(SAVE_LAPSES_ON_EXIT_PATH, flagString);

			/*//open a file stream
			FileInfo flagFileInfo = new FileInfo(SAVE_LAPSES_ON_EXIT_PATH);
			using ( StreamWriter writer = new StreamWriter(flagFileInfo.Open(FileMode.Truncate)) )
			//using ( StreamWriter writer = new StreamWriter(SAVE_LAPSES_ON_EXIT_PATH, false) )
			{
				//write string that maps flag name and value, akin to a dict pair
				bool saveLapses = saveLapsesCheckBox.Checked;
				int boolAsBinary = saveLapses ? 1 : 0;
				string flagString = $"_saveLapsesOnExit:{boolAsBinary}";

				char[] flagMapping = flagString.ToCharArray();
				writer.WriteAsync(flagMapping, 0, flagMapping.Length);
			}*/

			#endregion

			#region SAVE LAPSES TO FILE

			bool saveTimerLapses = saveLapsesCheckBox.Checked;
			//if 'saveLapesesCheckBox is NOT checked, return
			if ( !saveTimerLapses ) return;

			string nameAndSecondsStr = string.Empty;

			//get a list of the keys from _lapsesByNameDict
			Dictionary<string, int>.KeyCollection keys = _lapsesByNameDict.Keys;
			string[] keysArray = keys.ToArray<string>();
			//convert colon separated timer name and bulkSeconds string
			foreach ( string name in keysArray )
			{
				string durationAsString = _lapsesByNameDict[name].ToString();
				nameAndSecondsStr += $"{name}:{durationAsString}{Environment.NewLine}";
			}

			File.WriteAllText(LAPSES_MEM_FILE_PATH, nameAndSecondsStr);

			/*//open a file stream
			FileInfo lapseFileInfo = new FileInfo(LAPSES_MEM_FILE_PATH);
			using ( StreamWriter writer = new StreamWriter(lapseFileInfo.Open(FileMode.Truncate)) )
			//using ( StreamWriter writer = new StreamWriter(LAPSES_MEM_FILE_PATH, false) )
			{
				//get a list of the keys from _lapsesByNameDict
				Dictionary<string, int>.KeyCollection keys = _lapsesByNameDict.Keys;
				//convert colon separated timer name and bulkSeconds string
				string[] keysArray = keys.ToArray<string>();

				foreach ( string name in keysArray )
				{
					string durationAsString = _lapsesByNameDict[name].ToString();
					string nameAndSecondsStr = $"{name}:{durationAsString}{Environment.NewLine}";
					char[] timerChars = nameAndSecondsStr.ToCharArray();
					writer.WriteAsync(timerChars, 0, timerChars.Length);
				}
			}*/

			#endregion
		}

		// user intends to save the current list of timers and chosen audio
		private void saveProfileBtn_Click(object sender, EventArgs e)
		{
			//open 'saveProfileDialog' save file dialog so user sets PROFILE_SAVE_PATH
			bool specifiedSaveFile = saveProfileDialog.ShowDialog() == DialogResult.OK;
			//if user did NOT press the 'ok' button of said dialog, return
			if ( !specifiedSaveFile ) return;

			#region SAVE LIST OF TIMERS

			ListBox.ObjectCollection timerNameItems = timerNamesList.Items;
			ListBox.ObjectCollection timerDurationItems = timerDurationsList.Items;
			string timer = string.Empty;
			for ( int nameI = 0; nameI < timerNameItems.Count; nameI++ )
			{
				//get timer name from timerNamesList
				string timerName = timerNameItems[nameI].ToString();
				//get timer duration from from timerDurationsList
				string timerDuration = timerDurationItems[nameI].ToString();
				//write name:duration pair to file
				timer += $"{timerName}|{timerDuration}{Environment.NewLine}";
			}
			//now that user specified profile save file name,
			//build the file path
			string userSetFilePath = Path.Combine(PROFILE_SAVE_PATH, saveProfileDialog.FileName);
			//write to file
			File.WriteAllText(userSetFilePath, timer);

			/*//open file stream
			FileInfo listFileInfo = new FileInfo(userSetFilePath);
			using ( StreamWriter writer = new StreamWriter(listFileInfo.Open(FileMode.Truncate)) )
			//using ( StreamWriter writer = new StreamWriter(userSetFilePath, false) )
			{
				ListBox.ObjectCollection timerNameItems = timerNamesList.Items;
				ListBox.ObjectCollection timerDurationItems = timerDurationsList.Items;
				for ( int nameI = 0; nameI < timerNameItems.Count; nameI++ )
				{
					//get timer name from timerNamesList
					string timerName = timerNameItems[nameI].ToString();
					//get timer duration from from timerDurationsList
					string timerDuration = timerDurationItems[nameI].ToString();
					//write name:duration pair to file
					string timer = $"{timerName}|{timerDuration}{Environment.NewLine}";
					writer.WriteAsync(timer);
				}
			}*/

			#endregion

			#region SAVE CHOSEN AUDIO

			bool noAudioSelected = string.IsNullOrEmpty(_selectedAudio.FullPath);
			//to preserve audio select start directory, if no audio path selected, return
			if ( noAudioSelected ) return;

			//now that user specified profile save file name,
			//strip its '.txt' extension before adding audio save file suffix
			//use suffix appended file name (path) to open (create) audio save file
			int profilePathLen = userSetFilePath.Length;
			int profileExtLen = DEFAULT_PROFILE_FILE_EXT.Length;
			int lenMinusExt = profilePathLen - profileExtLen;
			string profilePathLessExt = userSetFilePath.Substring(0, lenMinusExt);
			string audioSaveFilePath = $"{profilePathLessExt}{AUDIO_SAVE_FILE_SUFFIX}";
			//write to file
			File.WriteAllText(audioSaveFilePath, _selectedAudio.FullPath);

			/*FileInfo audioFileInfo = new FileInfo(audioSaveFilePath);
			using ( StreamWriter writer = new StreamWriter(audioFileInfo.Open(FileMode.Truncate)) )
			//using ( StreamWriter writer = new StreamWriter(audioSaveFilePath, false) )
			{
				writer.WriteLine(_selectedAudio.FullPath);
			}*/

			#endregion
		}

		// user intends to load a previously saved list of timers and chosen audio
		private void loadProfileBtn_Click(object sender, EventArgs e)
		{
			//open 'loadProfileDialog' load file dialog so user sets PROFILE_LOAD_PATH 
			bool specifiedLoadFile = loadProfileDiaglog.ShowDialog() == DialogResult.OK;
			if ( !specifiedLoadFile ) return;

			#region LOAD LIST OF TIMERS

			//now that user specified profile load file name,
			//open that file by building its path
			string userSetFilePath = Path.Combine(PROFILE_LOAD_PATH, loadProfileDiaglog.FileName);
			//open file stream
			using ( StreamReader reader = new StreamReader(userSetFilePath) )
			{
				string line;
				while ( (line = reader.ReadLine()) != null )
				{
					//split line at profiles delimiter
					string[] timerConjugate = line.Split(DELIMITER_TIMER_PROFILES);
					//add timer name to timerNamesList.Items[i]
					timerNamesList.Items.Add( timerConjugate[0] );
					//add timer duration to timerDurationsList.Items[i]
					timerDurationsList.Items.Add( timerConjugate[1] );
				}
			}

			#endregion

			#region LOAD PREVIOUSLY CHOSEN AUDIO

			//now that user specified profile load file name,
			//strip its '.txt' extension before adding audio save file suffix
			//use suffix appended file name (path) to open audio load file
			int profilePathLen = userSetFilePath.Length;
			int profileExtLen = DEFAULT_PROFILE_FILE_EXT.Length;
			int lenMinusExt = profilePathLen - profileExtLen;
			string profilePathLessExt = userSetFilePath.Substring(0, lenMinusExt);
			string audioSaveFilePath = $"{profilePathLessExt}{AUDIO_SAVE_FILE_SUFFIX}";
			using ( StreamReader reader = new StreamReader(audioSaveFilePath) )
			{
				string line;
				while ( (line = reader.ReadLine()) != null )
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

			#endregion

			//cache loaded profile path
			_loadedProfilePath = userSetFilePath;
		}
	}
}
