﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace CountDownTimerV0
{
	public partial class DigitalCountTimer : Form
	{
		private const string TIMER_DISPLAY_DEFAULT_STRING = "00:00:00";
		private const string NAME_ENTRY_PROMPT_STRING = "[Enter Name]";
		private const string DURATION_ENTRY_PROMPT_STRING = "00:00:00";

		private const string MISSING_NAME_MESSAGE = "You did not enter a timer name." +
			" Retry.";
		private const string MISSING_NAME_CAPTION = "Missing Name Input";
		private const string MISSING_DURATION_MESSAGE = "You did not enter a timer duration." +
			" Retry.";
		private const string MISSING_DURATION_CAPTION = "Missing Duration Input";

		private const int MINUTES_PER_HOUR = 60;
		private const int SECONDS_PER_MINUTE = 60;

		private FormattedTimeColumns _formattedColumns;
		private MessageBoxInfo _timerNameMsgBoxInfo;
		private MessageBoxInfo _timerDurationMsgBoxInfo;
		private ChosenTimer _chosenTimer;

		private string[] _durationTimeColumns;
		private int _durationAsSeconds;

		private int _countUpDurationTarget;
		private bool _countDown = true;
		private const string COUNT_UP_BUTTON_TEXT = "COUNTING UP";
		private const string COUNT_DOWN_BUTTON_TEXT = "COUNTIING DOWN";
		private int _upCount;

		private SoundPlayer _soundPlayer;
		private SelectedAudio _selectedAudio;

		public DigitalCountTimer()
		{
			InitializeComponent();

			SetupForm();
		}

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

		private void SetupForm()
		{
			SetTabIndices();

			_formattedColumns = new FormattedTimeColumns();

			_timerNameMsgBoxInfo = new MessageBoxInfo(
				MISSING_NAME_MESSAGE, MISSING_NAME_CAPTION, MessageBoxButtons.OK);

			_timerDurationMsgBoxInfo = new MessageBoxInfo(
				MISSING_DURATION_MESSAGE, MISSING_DURATION_CAPTION, MessageBoxButtons.OK);

			_chosenTimer = new ChosenTimer(NAME_ENTRY_PROMPT_STRING, DURATION_ENTRY_PROMPT_STRING);
			_selectedAudio = new SelectedAudio();

			StartPosition = FormStartPosition.CenterScreen;

			_durationTimeColumns = new string[3]; /* holds 3 time hh, mm, and ss (hh:mm:ss) columns */

			/* LOAD SOUND FILE TO PLAY AS THE ALARM */
			_soundPlayer = new SoundPlayer();
			//_soundPlayer.SoundLocationChanged += OnSoundLocationChange();

			// CACHE THE SELECTED PATH PROPERTY VALUE OF audioFolderBrowser?

			/* LOAD TIMER VALUE TO BEGIN COUNT DOWN/UP */
			//get the count up/down time from the profile of timers
			/* f.y.i. a profile is just a .txt file of 'newline' separated strings
			   each containing the name of the timer and a 'comma' separating the 
			   timer length. The timer length is just an int number counted up to 
			   (-= 1 every 1000 miliseconds), or that is counted 
			   down from (+= 1 every 1000 miliseconds). */
			//
		}

		private void SetTabIndices()
		{
			startButton.TabIndex = 1;
			stopButton.TabIndex = 2;
			resetButton.TabIndex = 3;
			counterSelectorPanel.TabIndex = 4;
			navigateUpBtn.TabIndex = 5;
			navigateDwnBtn.TabIndex = 6;
			continuousModeBtn.TabIndex = 7;
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
			if ( defaultPrompt )
			{
				//set text to empty (or null)
				textBoxEntered.Text = string.Empty;
				return;
			}

			bool emptyTextField = string.IsNullOrEmpty(textBoxEntered.Text);
			bool resumedEnteringStr = !emptyTextField && !defaultPrompt;
			//if user entered string, clicked on another control, then back to this one,
			//the text would NOT be empty AND
			//the text would NOT be EQUAL to 'defaultTextBoxString, so
			if ( resumedEnteringStr )
				//do nothing (return)
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
			bool emptyName = string.IsNullOrEmpty(textBoxLeft.Text);
			//if empty name text field, show prompt text of required name string format
			if ( !emptyName ) return;

			/* Enforce string format via regex */
			//only leave if correct string format
			if ( requireProperFormat )
			{
				//if incorrect timer duration format
				bool timerHhMmSsFormat = true; /* ########### NEEDS CHECKING WITH REGEX ########### */
					//show popup prompting user to re-input with correct hh:mm:ss format
					//bring focus back to the 'timerDurationEntry' control for retry
					//return

			}

			textBoxLeft.Text = defaultTextBoxString;
		}

		private void timerDurationEntry_Leave(object sender, EventArgs e)
		{
			LeavingTextBox(timerDurationEntry, DURATION_ENTRY_PROMPT_STRING, true, "");
		}

		// user finished entering name and presses 'Enter' to start entering duration 
		private void timerNameEntry_KeyDown(object sender, KeyEventArgs e)
		{
			bool pressedEnter = e.KeyCode == Keys.Enter;
			if ( pressedEnter )
			{
				ShiftFocusOnTextSubmit(
					e, Keys.Enter, timerNameEntry, NAME_ENTRY_PROMPT_STRING, timerDurationEntry);
			}
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
		private void ShiftFocusOnTextSubmit(KeyEventArgs e, Keys submitKey, TextBox textToSubmit, string defaultTextBoxString, Control focusShiftedTo)
		{
			//if the key pressed != 'ENTER', return
			bool pressedEnterKey = e.KeyCode == submitKey;
			//if 'timerNameEntry' control text EQUALS 'NAME_ENTRY_PROMPT_STRING',
			bool defaultPrompt = textToSubmit.Text.Equals(defaultTextBoxString);
			bool containsDefaultPrompt = textToSubmit.Text.Contains(defaultTextBoxString);
			bool enteredText = !defaultPrompt && !containsDefaultPrompt;
			bool shiftFocus = pressedEnterKey && enteredText;
			//return
			if ( !shiftFocus ) return;

			//user provided a adequate text, so can move focus to 'next' control
			focusShiftedTo.Focus();
		}

		// user finished entering duration and presses 'Enter' to move tab focus to timer submit btn
		private void timerDurationEntry_KeyDown(object sender, KeyEventArgs e)
		{
			bool pressedEnter = e.KeyCode == Keys.Enter;
			if ( pressedEnter )
			{
				ShiftFocusOnTextSubmit(
					e, Keys.Enter, timerDurationEntry, DURATION_ENTRY_PROMPT_STRING, timerAddBtn);
			}

			/* Enforce correct timer duration format */
			if ( !pressedEnter ) return;

			//format seconds column
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
		private void FormatTimeFromBulkSeconds(
			int bulkSeconds, ref FormattedTimeColumns formattedTimeColumns)
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
		}

		/// <summary>
		/// Takes the provided <paramref name="hoursColumn"/>, <paramref name="minutesColumn"/>,
		/// and <paramref name="secondsColumn"/> args, and creates a string formatted to have
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
				RefocusInvalidTextEntry(timerDurationEntry, DURATION_ENTRY_PROMPT_STRING, _timerDurationMsgBoxInfo);

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
		/// <returns></returns>
		private bool RefocusInvalidTextEntry(
			TextBox refocusedOn, 
			string defaultBoxText, 
			MessageBoxInfo invalidMsgInfo)
		{
			bool emptyTextField = string.IsNullOrEmpty(refocusedOn.Text);
			bool defaultText = refocusedOn.Text.Equals(defaultBoxText);
			bool containsDefaultText = refocusedOn.Text.Contains(defaultBoxText);

			bool enteredText = !emptyTextField && !defaultText && !containsDefaultText;

			//if valid entry, no need to refocus, so return false
			if ( enteredText ) return false;

			//TextBox text is empty OR EQUALS the defaultBoxText, so
			//display popup informing user to enter a valid text string
			MessageBox.Show(
				invalidMsgInfo.Message, invalidMsgInfo.Caption, invalidMsgInfo.Buttons);

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
			//set selected of timerDurationsList to selected index above
			timerDurationsList.SelectedItem = timerDurationsList.Items[selectedNameI];

			//add the name and duration of selected to the 'ChosenTimer' struct
			string selectedName = timerNamesList.Items[selectedNameI].ToString();
			string selectedDuration = timerDurationsList.Items[selectedNameI].ToString();
			_chosenTimer.Name = selectedName;
			_chosenTimer.Duration = selectedDuration;

			//set 'timerDisplay' control text to the 'Duration' property of 'ChosenTimer' struct
			timerDisplay.Text = _chosenTimer.Duration;
		}

		// so user can select timer by either clicking on its name or duration, then press 'Start'
		private void timerDurationsList_SelectedValueChanged(object sender, EventArgs e)
		{
			/* only the duration at the selected row will be highlighted, but we also
			   want to highlight the corresponding name in timerNamesList, so */
			//get the selected index in timerDurationsList
			int selectedDurationI = timerDurationsList.SelectedIndex;
			//set selected of timerNamesList to selected index above
			timerNamesList.SelectedItem = timerNamesList.Items[selectedDurationI];

			//add the duration and of selected to the 'ChosenTimer' struct
			string selectedDuration = timerDurationsList.Items[selectedDurationI].ToString();
			string selectedName = timerNamesList.Items[selectedDurationI].ToString();
			_chosenTimer.Duration = selectedDuration;
			_chosenTimer.Name = selectedName;

			//set 'timerDisplay' control text to the 'Duration' property of 'ChosenTimer' struct
			timerDisplay.Text = _chosenTimer.Duration;
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
			//if no timer is selected from the list
				//display message box informing user to first select a timer
				//return focus
			bool defaultTimerDisplay = timerDisplay.Text.Equals(TIMER_DISPLAY_DEFAULT_STRING);
			//if 'timerDisplay' text EQUALS the TIMER_DISPLAY_DEFAULT_STRING,
			if ( defaultTimerDisplay )
			{
				//put focus back on the 'navigateUpBtn' control
				navigateUpBtn.Focus();

				//return
				return;
			}

			/* To increment upto or decrement down from the 'ChosenTimer.Duration',
			   we have to determine what the entire duration is in seconds for simple 
			   decrement, increment (++,--) operations. */
			_durationAsSeconds = DurationAsBulkSeconds(_chosenTimer.Duration);

			_countUpDurationTarget = _durationAsSeconds;

			//start the timer that will raise an 'elapsed' event every 1000 milliseconds (1 second)
			countTimer.Enabled = true;
			countTimer.Start();
		}

		// handles event raised whenever the ticker control's set interval elapses
		private void countTimer_Tick(object sender, EventArgs e)
		{
			/* Sound alarm if timer duration expired (counted down to zero or up to duration) */
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
			//AlarmNotifyingWindow alarmWin = new AlarmNotifyingWindow(this);

		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			 /* The continuously running routines upon pressing the 
			    'startButton' are:
			    - the 'countTimer'
			    - the '_soundPlayer' as a consequence of the timer
			    So disable those two */

			countTimer.Enabled = false;
			_soundPlayer.Stop();
		}
	}
}
