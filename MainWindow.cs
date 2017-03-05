using System;
using Gtk;
using PassGen;

public partial class MainWindow: Gtk.Window
{
	private bool IS_UPPER_CASED = false;
	private bool IS_LOWER_CASED = false;
	private bool IS_SYMBOLS_CHECKED = false;
	private bool IS_OCT_SELECTED = false;
	private bool IS_HEX_SELECTED = true;
	private int WORD_COUNT = 0;
	private int PASS_COUNT = 0;
	Gtk.NodeStore store;

	private PassGen.PassGenerator PASS_GEN;



	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		nodeview1.NodeStore = Store;
		prepareTableColumns();
	}

	private string[] getHeadersNames(){
		string[] headers = new string[nodeview1.Columns.Length];
		headers [0] = "Password";
		headers [1] = "Input char count";
		headers [2] = "Type";
		return headers;
	}

	Gtk.NodeStore Store {
		get {
			if (store == null) {
				store = new Gtk.NodeStore (typeof (GenTreeNode));
			}
			return store;
		}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}


	protected void generateOnClick (object sender, EventArgs e)
	{

		PASS_GEN = new PassGenerator();
		PASS_GEN.initialize(IS_UPPER_CASED,IS_LOWER_CASED,IS_SYMBOLS_CHECKED,IS_HEX_SELECTED,IS_OCT_SELECTED,WORD_COUNT);
		string system = "hexal";
		if (IS_OCT_SELECTED)
			system = "octal";

		String result = "";
		for(int i = 0; i<PASS_COUNT; i++){

			try{
					result = PASS_GEN.getSequence();
			}finally{
				if (result != null) {
					store.AddNode (new GenTreeNode (result, WORD_COUNT.ToString(),system));
					Console.Write ("Result: " + result);
				} else {
					MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Null");
					md.Run ();
					md.Destroy ();
				}
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
			IS_LOWER_CASED = true;
		} else {
			IS_LOWER_CASED = false;
		}
	}

	protected void onWordCountChanged (object sender, EventArgs e)
	{
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
	
	protected void viewAboutOnClick (object sender, EventArgs e)
	{
		MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "About\nPassGen v.1.1\nAuthor: Obsidiam\nLicense: GPLv.3.0");
		md.Run ();
		md.Destroy();
	}

	protected void hexClicked (object sender, EventArgs e)
	{
		if (radiobutton1.Active) {
			radiobutton2.Active = false;
			IS_HEX_SELECTED = true;
		} else {
			IS_HEX_SELECTED = false;
		}
	}

	protected void octClicked (object sender, EventArgs e)
	{
		if (radiobutton2.Active) {
			radiobutton1.Active = false;
			IS_OCT_SELECTED = true;
		} else {
			IS_OCT_SELECTED = false;
		}
	}

	protected void prepareTableColumns(){
		nodeview1.AppendColumn ("Password", new Gtk.CellRendererText (), "text", 0);
		nodeview1.AppendColumn ("Input char count", new Gtk.CellRendererText (), "text", 1);
		nodeview1.AppendColumn ("Type", new Gtk.CellRendererText(),"text",2);
		nodeview1.ShowAll();
	}

	protected void saveOnClick (object sender, EventArgs e)
	{
		string filename = filechooserbutton1.Filename;
		TreeIter tri = new TreeIter();
		if (nodeview1.Selection.GetSelected (out tri)) {
			try{
				DateTime thisDay = DateTime.Today;
				Console.Write ("Saved.");

				System.IO.StreamWriter file = System.IO.File.AppendText (filename + "/passgen.txt");


				string[] out_res = new string[nodeview1.Columns.Length];
				string[] headers = getHeadersNames ();
				out_res [0] = "Session - " + thisDay.ToString () + "\n";

				for (int index = 1; index<nodeview1.Columns.Length; index++) {
					out_res [index] = nodeview1.Model.GetValue (tri, index).ToString ();
				}

				for (int i = 0; i<out_res.Length; i++) {
					file.WriteLine (headers [i] + ": " + out_res [i]);
				}
				file.Close ();
			}finally{
				MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Saved to "+filename);
				md.Run ();
				md.Destroy ();
			}
		} else {
			MessageDialog md = new MessageDialog (null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "You haven't selected any record.");
			md.Run ();
			md.Destroy();
		}
	}
}
