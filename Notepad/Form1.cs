using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Form1 : Form
    {
        string buffer = "";
        int n = 0;
        ToolStripMenuItem filestram = new ToolStripMenuItem("File");
        ToolStripMenuItem editstram = new ToolStripMenuItem("Edit");
        ToolStripMenuItem viewstram = new ToolStripMenuItem("View");

        ToolStripMenuItem create = new ToolStripMenuItem("Create");
        ToolStripMenuItem newform = new ToolStripMenuItem("New Form");
        ToolStripMenuItem open = new ToolStripMenuItem("Open...");
        ToolStripMenuItem save = new ToolStripMenuItem("Save");
        ToolStripMenuItem saveass = new ToolStripMenuItem("Save as...");

        ToolStripMenuItem back = new ToolStripMenuItem("Back");
        ToolStripMenuItem cut = new ToolStripMenuItem("Cut");
        ToolStripMenuItem copy = new ToolStripMenuItem("Copy");
        ToolStripMenuItem paste = new ToolStripMenuItem("Paste");
        ToolStripMenuItem del = new ToolStripMenuItem("Delete");
        ToolStripMenuItem search = new ToolStripMenuItem("Search");

        ToolStripMenuItem cumsoon = new ToolStripMenuItem("Cumming (not) Soon...");
        public Form1()
        {
            InitializeComponent();
            textBox1.Width = this.Width;
            textBox1.Height = this.Height;

            save.Enabled = false;
            saveass.Enabled = false;

            back.Enabled = false;
            cut.Enabled = false;
            copy.Enabled = false;
            paste.Enabled = false;
            del.Enabled = false;
            search.Enabled = false;

            filestram.DropDownItems.Add(create);
            filestram.DropDownItems.Add(newform);
            filestram.DropDownItems.Add(open);
            filestram.DropDownItems.Add(save);
            filestram.DropDownItems.Add(saveass);

            create.Click += create_click;
            newform.Click += newform_click;
            open.Click += open_click;
            save.Click += save_click;
            saveass.Click += saveass_click;

            create.ShortcutKeys = Keys.Control | Keys.N;
            newform.ShortcutKeys = Keys.Control | Keys.Shift | Keys.C;
            open.ShortcutKeys = Keys.Control | Keys.O;
            save.ShortcutKeys = Keys.Control | Keys.S;
            saveass.ShortcutKeys = Keys.Control | Keys.Shift | Keys.C;

            editstram.DropDownItems.Add(back);
            editstram.DropDownItems.Add(cut);
            editstram.DropDownItems.Add(copy);
            editstram.DropDownItems.Add(paste);
            editstram.DropDownItems.Add(del);
            editstram.DropDownItems.Add(search);

            back.Click += back_click;
            cut.Click += cut_click;
            copy.Click += copy_click;
            paste.Click += paste_click;
            del.Click += del_click;
            search.Click += search_click;

            back.ShortcutKeys = Keys.Control | Keys.Z;
            cut.ShortcutKeys = Keys.Control | Keys.X;
            copy.ShortcutKeys = Keys.Control | Keys.C;
            paste.ShortcutKeys = Keys.Control | Keys.V;
            del.ShortcutKeys = Keys.Delete;
            search.ShortcutKeys = Keys.Control | Keys.F;

            cumsoon.Enabled = false;
            viewstram.DropDownItems.Add(cumsoon);


            menuStrip1.Items.Add(filestram);
            menuStrip1.Items.Add(editstram);
            menuStrip1.Items.Add(viewstram);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                save.Enabled = false;
                saveass.Enabled = false;

                back.Enabled = false;
                cut.Enabled = false;
                copy.Enabled = false;
                del.Enabled = false;
                search.Enabled = false;
            }
            else
            {
                save.Enabled = true;
                saveass.Enabled = true;

                back.Enabled = true;
                cut.Enabled = true;
                copy.Enabled = true;
                del.Enabled = true;
                search.Enabled = true;
            }
        }
        private void create_click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes to the file?", "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.No)
                {
                    textBox1.Text = "";
                }
                else if (result == DialogResult.Yes)
                {
                    StreamWriter write_text;
                    FileInfo file;
                    if (n != 0)
                        file = new FileInfo($"text{n}.txt");
                    else
                        file = new FileInfo($"text.txt");
                    write_text = file.AppendText();
                    write_text.WriteLine(textBox1.Text);
                    write_text.Close();
                    n++;
                }
            }
            else
            {
                MessageBox.Show("File empty!", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void newform_click(object sender, EventArgs e) { }
        private void open_click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = ofd.FileName;
            string fileText = System.IO.File.ReadAllText(filename);
            textBox1.Text = fileText;
        }
        private void save_click(object sender, EventArgs e)
        {
            StreamWriter write_text;
            FileInfo file = new FileInfo($"text.txt");
            write_text = file.AppendText();
            write_text.WriteLine(textBox1.Text);
            write_text.Close();
            MessageBox.Show($"Saved at {Application.StartupPath + "\\text.txt"}", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (n == 0)
                n++;
        }
        private void saveass_click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                if (sf.ShowDialog() == DialogResult.Cancel)
                    return;
                string filename = sf.FileName;
                System.IO.File.WriteAllText(filename, textBox1.Text);
                MessageBox.Show($"{filename} saved!", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                MessageBox.Show("File empty!", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void back_click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
        }
        private void cut_click(object sender, EventArgs e)
        {
            paste.Enabled = true;
            buffer = textBox1.SelectedText;
            textBox1.SelectedText = "";
        }
        private void copy_click(object sender, EventArgs e)
        {
            paste.Enabled = true;
            buffer = textBox1.SelectedText;
        }
        private void paste_click(object sender, EventArgs e)
        {
            textBox1.Text.Insert(textBox1.SelectionStart, buffer);            
       }
        private void del_click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "";
        }
        private void search_click(object sender, EventArgs e)
        {
            Thread x = new Thread(CreateNewForm);
            x.Start();
            while(Data.Value == null) { }
            string s = Data.Value;
            Data.Value = null;
            if(!textBox1.Text.ToUpper().Contains(s.ToUpper()))
                MessageBox.Show("0 results: " + s, "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            else
            {
                int k = 0,j = 0,f = 0;
                for(int i = 0; i < textBox1.Text.Length; i++)
                {
                    if(textBox1.Text[i] == s[j])
                    {
                        j++;
                        if (j >= s.Length)
                        {
                            j = 0;
                            k++;
                        }
                        if(k == 1 && f == 0)
                        {   
                            f++;
                            textBox1.SelectionStart = i - s.Length + 1;
                            textBox1.SelectionLength = s.Length;
                        }
                    }
                }
                MessageBox.Show(k + " results: " + s, "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            }
        }
        private void CreateNewForm()
        {
            Form f = new Form2();
            f.ShowDialog();
        }
        private void Form1_SizeChanged(object sender, System.EventArgs e)
        {
            textBox1.Width = this.Width;
            textBox1.Height = this.Height;
        }
    }
}
