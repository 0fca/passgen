using System;
using Gtk;
using PassGen;

public partial class MainWindow: Gtk.Window
{
	private Boolean IS_UPPER_CASED = false;
	private Boolean IS_LOWER_CASED = false;
	private Boolean IS_SYMBOLS_CHECKED = false;
	private Boolean IS_NUMBERS_CHECKED = false;
	private int WORD_COUNT = 0;
	private int PASS_COUNT = 0;

	private PassGen.PassGenerator PASS_GEN;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void generateOnClick (object sender, EventArgs e)
	{
		PASS_GEN = new PassGenerator();
		PASS_GEN.initialize(IS_UPPER_CASED,IS_LOWER_CASED,IS_SYMBOLS_CHECKED,IS_NUMBERS_CHECKED,WORD_COUNT);

		for(int i = 0; i<PASS_COUNT; i++){
			String result = PASS_GEN.getSequence();

			if (result != null) {
				pass_view.Buffer.Text = result;
				Console.Write("Result: "+result);
			} else {
				MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Null");
				md.Run ();
				md.Destroy();
			}
		}
	}
		

	protected void symbolChecked (object sender, EventArgs e)
	{
		if (symbol_chckbx.Active) {
			IS_SYMBOLS_CHECKED = true;
		} else {
			IS_SYMBOLS_CHECKED = false;
		}	
	}

	protected void upperChecked (object sender, EventArgs e)
	{
		if (upper_chckbx.Active) {
			IS_UPPER_CASED = true;
		} else {
			IS_UPPER_CASED = false;
		}
	}

	protected void lowerChecked (object sender, EventArgs e)
	{
		
		if (lower_chckbx.Active) {
			Console.Write ("Clicked");
			IS_LOWER_CASED = true;
		} else {
			IS_LOWER_CASED = false;
		}
	}

	protected void onWordCountChanged (object sender, EventArgs e)
	{
		Console.Write("Changed");
		WORD_COUNT = Int32.Parse(word_count_spin.Text);
		Console.Write(WORD_COUNT.ToString());
	}

	protected void onPassCountChanged (object sender, EventArgs e)
	{
		PASS_COUNT = Int32.Parse(pass_count_spin.Text);
	}

	protected void onTextInserted (object o, TextInsertedArgs args)
	{
		WORD_COUNT = Int32.Parse(word_count_spin.Text);
	}

	protected void numberChecked (object sender, EventArgs e)
	{
		Console.Write("Clicked");
		if (number_chckbx.Active) {
			IS_NUMBERS_CHECKED = true;
		} else {
			IS_NUMBERS_CHECKED = false;
		}
	}

	protected void saveOnClick (object sender, EventArgs e)
	{
		throw new NotImplementedException ();
	}

	protected void viewAboutOnClick (object sender, EventArgs e)
	{
		MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "About\nPassGen v.1.0 BETA\nAuthor: Obsidiam\nLicense: GPLv.3.0");
		md.Run ();
		md.Destroy();
	}
}
