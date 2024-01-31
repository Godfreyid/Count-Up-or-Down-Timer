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

namespace CountDownTimerV0
{
	public partial class DigitalCountTimer : Form
	{
		private const string TIMER_DISPLAY_DEFAULT_STRING = "00:00:00";
		private const string NAME_ENTRY_PROMPT_STRING = "[Enter Name]";
		private const string DURATION_ENTRY_PROMPT_STRING = "00:00:00";
		private const int MINUTES_PER_HOUR = 60;
		private const int SECONDS_PER_MINUTE = 60;

		private ChosenTimer _chosenTimer;

		private string[] _durationTimeColumns;
		private int _durationAsSeconds;

		private bool _countUp = false;
		private int _upCount;
		private SoundPlayer _soundPlayer;

		public DigitalCountTimer()
		{
			InitializeComponent();

			SetupForm();
		}

		private void SetupForm()
		{
			SetTabIndices();

			_chosenTimer = new ChosenTimer(NAME_ENTRY_PROMPT_STRING, DURATION_ENTRY_PROMPT_STRING);

			StartPosition = FormStartPosition.CenterScreen;

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
		}

		// user clicks to submit timer (name+duration) to 'Timers' list
		private void listAddBtn_Click(object sender, EventArgs e)
		{
			/* Ensure valid data submission */
			bool invalidName = 
				RefocusInvalidTextEntry(timerNameEntry, NAME_ENTRY_PROMPT_STRING);
			bool invalidDuration = 
				RefocusInvalidTextEntry(timerDurationEntry, DURATION_ENTRY_PROMPT_STRING);

			if ( invalidName || invalidDuration ) return;

			/* Add 'timerNameEntry' text to 'timerNamesList' listbox */
			//pause painting timerNamesList list box while adding text
			timerNamesList.BeginUpdate();
			//get the text value from the Text property of the 'timerNameEntry' control
			//add name text value to 'timerNamesList' listbox
			string timerName = timerNameEntry.Text;
			timerNamesList.Items.Add(timerName);
			//reset the Text property of 'timerNameEntry' to the 'NAME_ENTRY_PROMPT_STRING'
			timerNameEntry.Text = NAME_ENTRY_PROMPT_STRING;
			//un-pause painting timerNamesList list box
			timerNamesList.EndUpdate();

			/* Enforce correct timer duration format */
			//force correct format of user entered duration
			//if seconds columns exceed 60,
				//get modulo 60
				//add whole number quotient to minutes columns
			//if minutes exceeds 60
				//get modulo 60
				//add whole number quotient to hours columns
				//clamp hours columns between 0 and 99
				//
				//bool wrongFormat = 

			/* Add 'timerDurationEntry' text to 'timerDurationsList' listbox */
			//pause painting timerDurationsList list box while adding text
			timerDurationsList.BeginUpdate();
			//get the text value from the Text property of the 'timerDurationEntry' control
			//add duration text value to 'timerDurationEntry' listbox
			string timerDuration = timerDurationEntry.Text;
			timerDurationsList.Items.Add(timerDuration);
			//reset the Text property of 'timerDurationEntry' to the 'DURATION_ENTRY_PROMPT_STRING'
			timerDurationEntry.Text = DURATION_ENTRY_PROMPT_STRING;
			//un-pause painting timerDurationsList list box
			timerDurationsList.EndUpdate();

			//focus on 'timerNameEntry' control to ready for next timer entry
			timerNameEntry.Focus();
		}

		private bool RefocusInvalidTextEntry(TextBox refocusedOn, string defaultBoxText)
		{
			bool emptyTextField = string.IsNullOrEmpty(refocusedOn.Text);
			bool defaultText = refocusedOn.Text.Equals(defaultBoxText);
			bool invalidEntry = emptyTextField || defaultText;

			//if valid entry, return false for refocused
			if ( !invalidEntry ) return false;

			//TextBox text is empty OR EQUALS the defaultBoxText, so
			//display popup informing user to enter a valid text string

			//return focus to 'refocusedOn' TextBox
			refocusedOn.Focus();
			//return true for refocused
			return true;
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
			_countUp = !_countUp;
		}

		// user intends to begin count down/up
		private void startButton_Click(object sender, EventArgs e)
		{
			bool defaultTimerDisplay = timerDisplay.Text.Equals(TIMER_DISPLAY_DEFAULT_STRING);
			//if 'timerDisplay' text EQUALS the TIMER_DISPLAY_DEFAULT_STRING,
			if ( defaultTimerDisplay )
			{
				//put focus back on the 'navigateUpBtn' control
				navigateUpBtn.Focus();

				//display a popup notification about the selected timer having no duration


				//return
				return;
			}

			//maybe convert the 'ChosenTimer' 'Duration' property to the hh:mm:ss format?

			/* To increment upto or decrement down from the 'ChosenTimer.Duration',
			   we have to determine what the entire duration is in seconds for simple 
			   decrement, increment (++,--) operations. */
			//convert the formatted 'ChosenTimer.Duration' to 'durationAsSeconds' as,
			//hours column to minutes to seconds, i.e. ( (hours * 60mins) * 60secs ) as 'hoursAsSeconds', 
			_durationTimeColumns = _chosenTimer.Duration.Split(':');
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
			_durationAsSeconds = hoursInSeconds + minutesInSeconds + seconds;

			//start the timer that will raise an 'elapsed' event every 1000 milliseconds (1 second)
			countTimer.Enabled = true;
			countTimer.Start();
		}

		// handles event raised whenever the ticker control's set interval elapses
		private void countTimer_Tick(object sender, EventArgs e)
		{
			//DateTime.ParseExact();

			/* Per user toggle, a count down/up method will handle the '1 second elapsed' event,
			   by either counting down from the 'durationAsSeconds' by decrementing 1, or counting 
			   up from zero (0) to 'durationAsSeconds'. Then after ticking by 1 second, we need to
			   reflect the increment/decrement in the 'timerDisplay' text. */

			int m_secondsColumn;
			int m_minutesColumn;
			int m_hoursColumn;

			//if user toggled countUp is false,
			if ( !_countUp )
			{
				//decrement the 'durationAsSeconds' by 1
				_durationAsSeconds--;
				bool negative = _durationAsSeconds < 1;
				_durationAsSeconds = negative ? 0 : _durationAsSeconds; /* Since Math.Clamp() is missing */
				//compute hh:mm:ss equivalent of current 'durationAsSeconds' as,
				//dividing 'durationAsSeconds' by 60secs to get the decimal minutes
				double decimalUnBasedMinutes = _durationAsSeconds / 60;
				//the quotient is your decimal minutes column since it is potentially above 60mins, and 
				int wholeNumUnBasedMinutes = (int)Math.Floor(decimalUnBasedMinutes);
				//remainder * 60secs is your whole number seconds column
				double remainderAsSeconds = decimalUnBasedMinutes - wholeNumUnBasedMinutes;
				m_secondsColumn = (int)(remainderAsSeconds * 60);

				//divide the decimal minutes by 60secs to get the decimal hours
				double decimalUnBasedHours = decimalUnBasedMinutes / 60;
				//the decimal portion of the decimal hours * 60mins is your minutes column
				int wholeNumUnBasedHours = (int)Math.Floor(decimalUnBasedHours);
				double remainderAsMinutes = decimalUnBasedHours - wholeNumUnBasedHours;
				m_minutesColumn = (int)(remainderAsMinutes * 60);
				//the whole number quotient is your hours column
				m_hoursColumn = wholeNumUnBasedHours;
			}
			//else, user toggled countUp is true,
			else
			{
				//increment the running '_upCount' by 1
				_upCount++;
				bool exceedsDuration = _upCount > _durationAsSeconds;
				_upCount = exceedsDuration ? _durationAsSeconds : _upCount;
				//compute hh:mm:ss equivalent of current '_upCount' as,
				//dividing 'upCount' by 60secs to get the decimal minutes
				double decimalUnBasedMinutes = _upCount / 60;
				//the quotient is your decimal minutes column since it is potentially above 60mins, and 
				int wholeNumUnBasedMinutes = (int)Math.Floor(decimalUnBasedMinutes);
				//remainder * 60secs is your whole number seconds column
				double remainderAsSeconds = decimalUnBasedMinutes - wholeNumUnBasedMinutes;
				m_secondsColumn = (int)(remainderAsSeconds * 60);

				//divide the decimal minutes by 60secs to get the decimal hours
				double decimalUnBasedHours = decimalUnBasedMinutes / 60;
				//the decimal portion of the decimal hours * 60mins is your minutes column
				int wholeNumUnBasedHours = (int)Math.Floor(decimalUnBasedHours);
				double remainderAsMinutes = decimalUnBasedHours - wholeNumUnBasedHours;
				m_minutesColumn = (int)(remainderAsMinutes * 60);
				//the whole number quotient is your hours column
				m_hoursColumn = wholeNumUnBasedHours;

			}

			//build updated 'ChosenTimer.Duration' with corresponding quotients and multiplied remainders
			//set the 'timerDisplay' Text property to 'ChosenTimer.Duration'
			_chosenTimer.Duration = $"{m_hoursColumn}:{m_minutesColumn}:{m_secondsColumn}";

			/* Sound alarm if timer duration expired (counted down to zero or up to duration) */
			//if toggled count up,
			if ( _countUp )
			{
				bool countedFullDuration = _upCount >= _durationAsSeconds;
				//if '_upCount' does NOT EQUALS 'ChosenTimer.Duration' on this tick, return
				if ( !countedFullDuration ) return;

				//else, EQUALS means duration expired, so
				//pause ticker control to stop ticking during alarm period
				countTimer.Stop();
				//raise alarm 
				_soundPlayer.PlayLooping();

				return;
			}
			//else toggled count down, so

			bool countedToZero = _durationAsSeconds <= 0;
			//if 'durationAsSeconds' > 0, return
			if ( !countedToZero ) return;

			//else, LESS THAN OR EQUALS 0 means duration expired, so
			//pause ticker control to stop ticking during alarm period
			countTimer.Stop();
			//raise alarm
			_soundPlayer.PlayLooping();
		}

		
	}
}
