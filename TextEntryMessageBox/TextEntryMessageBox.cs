using System;
using System.Drawing;
using System.Windows.Forms;

namespace SapphTools.Utils.UX
{
	/// <summary>
	/// An extended MessageBox with lot of customizing capabilities.
	/// </summary>
	public class TextEntryMessageBox
	{
		#region Fields
		private TextEntryMessageBoxForm _msgBox = new TextEntryMessageBoxForm();

		private string _name = null;
		#endregion

		#region Properties
		internal string Name
		{
			get{ return _name; }
			set{ _name = value; }
		}

		/// <summary>
		/// Sets the caption of the message box
		/// </summary>
		public string Caption
		{
			set{_msgBox.Caption = value;}
		}

		/// <summary>
		/// Sets the text of the message box
		/// </summary>
		public string Text
		{
			set{_msgBox.Message = value;}
		}

		/// <summary>
		/// Sets the icon to show in the message box
		/// </summary>
		public Icon CustomIcon
		{
			set{_msgBox.CustomIcon = value;}
		}

		/// <summary>
		/// Sets the icon to show in the message box
		/// </summary>
		public TextEntryMessageBoxIcon Icon
		{
			set{ _msgBox.StandardIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), value.ToString());}
		}
		
		/// <summary>
		/// Sets the font for the text of the message box
		/// </summary>
		public Font Font
		{
			set{_msgBox.Font = value;}
		}

		public int InputMaxLength {
			get => _msgBox.InputMaxLength;
			set => _msgBox.InputMaxLength = value;
		}

		public int InputWidth {
			get => _msgBox.InputWidth;
			set => _msgBox.InputWidth = value;
		}

		/// <summary>
		/// Sets or Gets wether an alert sound is played while showing the message box.
		/// The sound played depends on the the Icon selected for the message box
		/// </summary>
		public bool PlayAlsertSound
		{
			get{ return _msgBox.PlayAlertSound; }
			set{ _msgBox.PlayAlertSound = value; }
		}

        /// <summary>
        /// Sets or Gets the time in milliseconds for which the message box is displayed.
        /// </summary>
        public int Timeout
        {
            get{ return _msgBox.Timeout; }
            set{ _msgBox.Timeout = value; }
        }

        /// <summary>
        /// Controls the result that will be returned when the message box times out.
        /// </summary>
        public TimeoutResult TimeoutResult
        {
            get{ return _msgBox.TimeoutResult; }
            set{ _msgBox.TimeoutResult = value; }
        }
		#endregion

		#region Methods
		/// <summary>
		/// Shows the message box
		/// </summary>
		/// <returns></returns>
		public string Show()
		{
			return Show(null);
		}

		/// <summary>
		/// Shows the messsage box with the specified owner
		/// </summary>
		/// <param name="owner"></param>
		/// <returns></returns>
		public string Show(IWin32Window owner) {
			
			if(owner == null) {
				_msgBox.ShowDialog();
			} else {
				_msgBox.ShowDialog(owner);
			}

            Dispose();

			return _msgBox.Result;
		}

		/// <summary>
		/// Add a custom button to the message box
		/// </summary>
		/// <param name="button">The button to add</param>
		public void AddButton(TextEntryMessageBoxButton button)
		{
			if(button == null)
				throw new ArgumentNullException("button","A null button cannot be added");

			_msgBox.Buttons.Add(button);

			if(button.IsCancelButton)
			{
				_msgBox.CustomCancelButton = button;
			}
		}

		/// <summary>
		/// Add a custom button to the message box
		/// </summary>
		/// <param name="text">The text of the button</param>
		/// <param name="val">The return value in case this button is clicked</param>
		public void AddButton(string text, string val)
		{
			if(text == null)
				throw new ArgumentNullException("text","Text of a button cannot be null");

			if(val == null)
				throw new ArgumentNullException("val","Value of a button cannot be null");

            TextEntryMessageBoxButton button = new TextEntryMessageBoxButton {
                Text = text,
                Value = val
            };

            AddButton(button);
		}
        
		/// <summary>
		/// Add a standard button to the message box
		/// </summary>
		/// <param name="buttons">The standard button to add</param>
		public void AddButton(TextEntryMessageBoxButtons button)
		{
            string buttonText = TextEntryMessageBoxManager.GetLocalizedString(button.ToString()) ?? button.ToString();
            string buttonVal = button.ToString();

            TextEntryMessageBoxButton btn = new TextEntryMessageBoxButton {
                Text = buttonText,
                Value = buttonVal
            };

            if (button == TextEntryMessageBoxButtons.Cancel)
            {
                btn.IsCancelButton = true;
            }

			AddButton(btn);
		}

		/// <summary>
		/// Add standard buttons to the message box.
		/// </summary>
		/// <param name="buttons">The standard buttons to add</param>
		public void AddButtons(MessageBoxButtons buttons)
		{
			switch(buttons)
			{
				case MessageBoxButtons.OK:
					AddButton(TextEntryMessageBoxButtons.Ok);
					break;

				case MessageBoxButtons.AbortRetryIgnore:
					AddButton(TextEntryMessageBoxButtons.Abort);
					AddButton(TextEntryMessageBoxButtons.Retry);
					AddButton(TextEntryMessageBoxButtons.Ignore);
					break;

				case MessageBoxButtons.OKCancel:
					AddButton(TextEntryMessageBoxButtons.Ok);
					AddButton(TextEntryMessageBoxButtons.Cancel);
					break;

				case MessageBoxButtons.RetryCancel:
					AddButton(TextEntryMessageBoxButtons.Retry);
					AddButton(TextEntryMessageBoxButtons.Cancel);
					break;

				case MessageBoxButtons.YesNo:
					AddButton(TextEntryMessageBoxButtons.Yes);
					AddButton(TextEntryMessageBoxButtons.No);
					break;

				case MessageBoxButtons.YesNoCancel:
					AddButton(TextEntryMessageBoxButtons.Yes);
					AddButton(TextEntryMessageBoxButtons.No);
					AddButton(TextEntryMessageBoxButtons.Cancel);
					break;
			}
		}
		#endregion

		#region Ctor
		/// <summary>
		/// Ctor is internal because this can only be created by MBManager
		/// </summary>
		internal TextEntryMessageBox()
		{
		}

		/// <summary>
		/// Called by the manager when it is disposed
		/// </summary>
		internal void Dispose()
		{
			_msgBox?.Dispose();
		}
		#endregion
	}
}
