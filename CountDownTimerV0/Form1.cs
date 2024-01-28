using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountDownTimerV0
{
	public partial class Form1 : Form
	{
		private const string NAME_ENTRY_PROMPT_STRING = "[Enter Name]";
		private const string DURATION_ENTRY_PROMPT_STRING = "00:00:00";

		private ChosenTimer _chosenTimer;

		public Form1()
		{
			InitializeComponent();

			SetTabIndices();

			_chosenTimer = new ChosenTimer();

			StartPosition = FormStartPosition.CenterScreen;


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
			public string Name { get; set; }
			public string Duration { get; set; }
		}

		private void startButton_Click(object sender, EventArgs e)
		{

		}

		// user clicked in text field to begin entering timer name
		private void timerNameEntry_Enter(object sender, EventArgs e)
		{
			//highlight (select all) the default text prompting user's entry
			//OR PERHAPS BETTER,
			bool defaultPrompt = timerNameEntry.Text.Equals(NAME_ENTRY_PROMPT_STRING);
			//if the current text EQUALS NAME_ENTRY_PROMPT_STRING
			if (defaultPrompt)
			{
				//set text to empty (or null)
				timerNameEntry.Text = string.Empty;
				return;
			}

			bool emptyName = string.IsNullOrEmpty(timerNameEntry.Text);
			bool resumedEnteringName = !emptyName && !defaultPrompt;
			//if user entered name, clicked on another control, then back to this one,
			//the text would NOT be empty AND
			//the text would NOT be EQUAL to NAME_ENTRY_PROMPT_STRING, so
			if (resumedEnteringName)
				//do nothing (return)
				return;
		}

		// user clicked in text field to begin entering timer duration
		private void timerDurationEntry_Enter(object sender, EventArgs e)
		{
			//highlight (select all) the default text prompting user's entry
			//OR PERHAPS BETTER,
			bool defaultPrompt = timerDurationEntry.Text.Equals(DURATION_ENTRY_PROMPT_STRING);
			//if the current text EQUALS DURATION_ENTRY_PROMPT_STRING
			if (defaultPrompt)
			{
				//set text to empty (or null)
				timerDurationEntry.Text = string.Empty;
				return;
			}

			bool emptyDuration = string.IsNullOrEmpty(timerDurationEntry.Text);
			bool resumedEnteringDuration = !emptyDuration && !defaultPrompt;
			//if user entered duration, clicked on another control, then back to this one,
			//the text would NOT be empty AND
			//the text would NOT be EQUAL to DURATION_ENTRY_PROMPT_STRING, so
			if (resumedEnteringDuration)
				//do nothing (return)//highlight (select all) the default text prompting user's entry
				return;
		}

		// user clicked away from text field taking focus to another control
		private void timerNameEntry_Leave(object sender, EventArgs e)
		{
			bool emptyName = string.IsNullOrEmpty(timerNameEntry.Text);
			//if empty name text field, show prompt text of required name string format
			if ( !emptyName ) return;

			timerNameEntry.Text = NAME_ENTRY_PROMPT_STRING;
		}

		private void timerDurationEntry_Leave(object sender, EventArgs e)
		{
			bool emptyDuration = string.IsNullOrEmpty(timerDurationEntry.Text);
			//if empty duration text field, show prompt text of required duration string format
			if ( !emptyDuration ) return;

			timerDurationEntry.Text = DURATION_ENTRY_PROMPT_STRING;
		}

		// user finished entering name and presses 'Enter' to start entering duration 
		private void timerNameEntry_KeyDown(object sender, KeyEventArgs e)
		{
			//if the key pressed != 'ENTER', return
			bool pressedEnterKey = e.KeyCode == Keys.Enter;
			//if 'timerNameEntry' control text EQUALS 'NAME_ENTRY_PROMPT_STRING',
			bool isDefaultPrompt = timerNameEntry.Text.Equals(NAME_ENTRY_PROMPT_STRING);
			bool containsDefaultPrompt = timerNameEntry.Text.Contains(NAME_ENTRY_PROMPT_STRING);
			bool didNotEnterName = isDefaultPrompt || containsDefaultPrompt;
			//return
			if (!pressedEnterKey || didNotEnterName) return;

			//user provided a name, so can now move focus to the 'timerDurationEntry' control
			//set the 'timerDurationEntry' control as the active form control
			timerDurationEntry.Focus();
		}

		// user finished entering duration and presses 'Enter' to move tab focus to timer submit btn
		private void timerDurationEntry_KeyDown(object sender, KeyEventArgs e)
		{
			//if the key pressed != 'ENTER', return
			bool pressedEnterKey = e.KeyCode == Keys.Enter;
			//if 'timerDurationEntry' control text EQUALS 'DURATION_ENTRY_PROMPT_STRING',
			bool isDefaultPrompt = timerNameEntry.Text.Equals(DURATION_ENTRY_PROMPT_STRING);
			bool containsDefaultPrompt = timerNameEntry.Text.Contains(DURATION_ENTRY_PROMPT_STRING);
			bool didNotEnterDuration = isDefaultPrompt || containsDefaultPrompt;
			//return
			if (!pressedEnterKey || didNotEnterDuration) return;

			//user provided a duration, so can now move focus to the 'timerAddBtn' control
			//set the 'timerAddBtn' control as the active form control
			timerAddBtn.Focus();
		}

		// user clicks to submit timer (name+duration) to 'Timers' list
		private void listAddBtn_Click(object sender, EventArgs e)
		{
			/* Ensure valid data submission */
			bool emptyName = string.IsNullOrEmpty(timerNameEntry.Text);
			bool defaultNamePrompt = timerNameEntry.Text.Equals(NAME_ENTRY_PROMPT_STRING);
			bool invalidName = emptyName || defaultNamePrompt;
			//if text of 'timerNameEntry' is empty OR EQUALS the 'NAME_ENTRY_PROMPT_STRING'
			if ( invalidName )
			{
				//display popup informing user to enter a valid timer name

				//return focus to the 'timerNameEntry' control
				timerNameEntry.Focus();
				//return
				return;
			}

			bool emptyDuration = string.IsNullOrEmpty(timerDurationEntry.Text);
			bool defaultDurationPrompt = timerDurationEntry.Text.Equals(DURATION_ENTRY_PROMPT_STRING);
			bool timerHhMmSsFormat = true; /* ########### NEEDS CHECKING WITH REGEX ########### */
			bool invalidDuration = emptyDuration || defaultDurationPrompt || !timerHhMmSsFormat;
			//if text of 'timerDurationEntry' is empty OR EQUALS the 'DURATION_ENTRY_PROMPT_STRING'
			if ( invalidDuration )
			{
				//display popup informing user to enter a valid timer duration

				//return focus to the 'timerDurationEntry' control
				timerDurationEntry.Focus();
				//return
				return;
			}

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

			//System.Diagnostics.Debug.WriteLine($"timer name: {selectedName}");
			//System.Diagnostics.Debug.WriteLine($"timer duraton: {selectedDuration}");
		}

		// so user can select timer by either clicking on its name or duration, then press 'Start'
		private void timerDurationsList_SelectedValueChanged(object sender, EventArgs e)
		{
			/* only the duration at the selected row will be highlighted, but we also
			   want to highlight the corresponding name in timerNamesList, so*/
			//get the selected index in timerDurationsList
			int selectedDurationI = timerDurationsList.SelectedIndex;
			//set selected of timerNamesList to selected index above
			timerNamesList.SelectedItem = timerNamesList.Items[selectedDurationI];

			//add the duration and of selected to the 'ChosenTimer' struct
			string selectedDuration = timerDurationsList.Items[selectedDurationI].ToString();
			string selectedName = timerNamesList.Items[selectedDurationI].ToString();
			_chosenTimer.Duration = selectedDuration;
			_chosenTimer.Name = selectedName;
		}
	}
}
