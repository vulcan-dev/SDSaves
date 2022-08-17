using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// Todo: Cleanup paths.

namespace SDSaves {
    public partial class SaveManager : Form {
        /************************
        *        Variables      *
        *************************/
        private Size ButtonSize = new Size(294, 34);
        private Color ButtonColor = Color.FromArgb(37, 41, 51);
        private Color ButtonSaveColor = Color.FromArgb(255, 131, 114);
        private TableLayoutPanel ButtonPanel;
        private List<string> GameSaves = new List<string>();
        private string SavePath;

        /************************
        *       Constructor     *
        *************************/
        public SaveManager() {
            InitializeComponent();

            ButtonPanel = Controls.Find("button_panel", true)[0] as TableLayoutPanel;

            Button createSaveButton = Controls.Find("btn_createSave", true).FirstOrDefault() as Button;
            createSaveButton.Click += new EventHandler(createSaveButton_Click);

            SavePath = $"C:\\Users\\{Environment.UserName}\\AppData\\Local\\SurrounDead\\Saved\\SaveGames\\";
            if (!Directory.Exists(SavePath)) {
                MessageBox.Show($"Could not find save path: {SavePath}");
                Environment.Exit(1);
            }

            if (!Directory.Exists(".\\Saves\\")) {
                Directory.CreateDirectory(".\\Saves\\");
            } else {
                string[] saves = Directory.GetDirectories(".\\Saves\\");

                var sortedSaves = Array.ConvertAll(saves, x => new { Name = x, Date = Directory.GetCreationTime(x) });
                sortedSaves = sortedSaves.OrderBy(x => x.Date).ToArray();
                
                foreach (var save in sortedSaves) {
                    string saveName = save.Name.Substring(save.Name.LastIndexOf('\\') + 1);
                    AddSaveToList(saveName);
                }
            }

            if (!Directory.Exists(".\\BackupSave\\")) {
                Directory.CreateDirectory(".\\BackupSave\\");
            }

            string currentSave = GetCurrentSave();
            if (currentSave != "") {
                SetButtonColor("Save" + currentSave, ButtonSaveColor);
            }
        }

        /************************
        *    Button Utilities   *
        *************************/
        private void SetButtonColor(string name, Color color) {
            foreach (Control control in ButtonPanel.Controls) {
                if (control.Name == name) {
                    control.BackColor = color;
                } else {
                    control.BackColor = ButtonColor;
                }
            }
        }

        private Button CreateButton(string text, string name) {
            Button button = new Button();
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Text = text;
            button.BackColor = ButtonColor;
            button.ForeColor = Color.White;
            button.Size = ButtonSize;
            button.Name = name;
            button.Margin = new Padding(1, 1, 1, 1);

            return button;
        }

        private Tuple<Button, int> GetButton(string name, int offset = 0) {
            for (int i = 0; i < ButtonPanel.Controls.Count; i++) {
                if (ButtonPanel.Controls[i].Name == name) {
                    return Tuple.Create<Button, int>((Button)ButtonPanel.Controls[i + offset], i);
                }
            }

            return null;
        }

        /************************
        *  Helpers & Utilities  *
        *************************/
        public void AddSaveToList(string name) {
            if (!GameSaves.Contains(name)) {
                GameSaves.Add(name);
            } else {
                return; // Overwrite existing save, we don't need to add it again.
            }

            Button saveNameButton = CreateButton(name, "Save" + name);
            Button deleteButton = CreateButton("Delete", "Delete" + name);
            Button overwriteButton = CreateButton("Save", "Overwrite" + name);

            deleteButton.Click += DeleteButton_Click;
            saveNameButton.Click += SaveNameButton_Click;
            overwriteButton.Click += OverwriteButton_Click;

            ButtonPanel.SuspendLayout();
            ButtonPanel.Controls.Add(saveNameButton);
            ButtonPanel.Controls.Add(deleteButton);
            ButtonPanel.Controls.Add(overwriteButton);
            ButtonPanel.ResumeLayout();

            int totalButtonHeight = 0;

            for (int i = 0; i < GameSaves.Count; i++) {
                totalButtonHeight += ButtonSize.Height;
            }

            if (totalButtonHeight > ButtonPanel.Height - 39) {
                int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
                ButtonPanel.AutoScroll = true;
                ButtonPanel.Padding = new Padding(0, 0, vertScrollWidth, 0);
                ButtonPanel.HorizontalScroll.Enabled = false;
                ButtonPanel.HorizontalScroll.Visible = false;
                ButtonPanel.VerticalScroll.Enabled = true;
                ButtonPanel.VerticalScroll.Visible = true;
            }
        }

        private string GetHash(string path) {
            string finalHash = "";
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach (string file in files) {
                byte[] fileBytes = File.ReadAllBytes(file);
                string fileHash = Sha256.Hash(fileBytes.ToString());
                finalHash += fileHash;
            }

            return finalHash;
        }

        private string GetCurrentSave() {
            foreach (string save in GameSaves) {
                if (GetHash("Saves\\" + save) == GetHash(SavePath)) {
                    return save;
                }
            }

            return "";
        }

        /****************************
         * File/Directory Utilities *
        *****************************/
        void CopyDirectory(string src, string dst, bool recurse = true, bool overwrite = true) {
            DirectoryInfo source = new DirectoryInfo(src);
            DirectoryInfo destination = new DirectoryInfo(dst);

            if (!source.Exists) {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + src);
            }

            if (destination.Exists && !overwrite) {
                throw new IOException("Destination directory already exists and overwrite is false.");
            }

            if (!destination.Exists) {
                destination.Create();
            }

            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files) {
                file.CopyTo(Path.Combine(dst, file.Name), overwrite);
            }

            if (recurse) {
                DirectoryInfo[] dirs = source.GetDirectories();
                foreach (DirectoryInfo dir in dirs) {
                    CopyDirectory(dir.FullName, Path.Combine(dst, dir.Name), recurse, overwrite);
                }
            }
        }

        private void ClearDirectory(string path, bool recurse = true) {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles()) {
                file.Delete(); 
            }

            foreach (DirectoryInfo dir in di.GetDirectories()) {
                dir.Delete(recurse); 
            }
        }

        
        /************************
         * Handle Button Clicks *
        *************************/
        private void OverwriteButton_Click(object sender, EventArgs e) { // Save/Overwrite Button
            Button overwriteButton = (Button)sender;

            string saveName = GetButton(overwriteButton.Name, -2).Item1.Text;
            if (MessageBox.Show("Are you sure you want to overwrite \"" + saveName + "\"?", "Overwrite Save", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                string path = $".\\Saves\\{saveName}";
                if (!Directory.Exists(path)) {
                    MessageBox.Show("Could not find in \"Saves\" folder!");
                    return;
                } else {
                    ClearDirectory(path);
                }

                CopyDirectory(SavePath, path);
            }
        }

        private void SaveNameButton_Click(object sender, EventArgs e) { // Load Button
            Button loadButton = (Button)sender;

            string saveName = GetButton(loadButton.Name).Item1.Text;
            if (MessageBox.Show("Are you sure you want to load \"" + saveName + "\"?\n(Note: You may need to restart if the spawn position is incorrect, game bug (v1.1.0f))", "Load Save", MessageBoxButtons.YesNo) == DialogResult.Yes) {        
                CopyDirectory(SavePath, ".\\BackupSave\\");
                ClearDirectory(SavePath);
                CopyDirectory(".\\Saves\\" + saveName, SavePath);

                SetButtonColor("Save" + saveName, ButtonSaveColor);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e) { // Delete Button
            Button deleteButton = (Button)sender;

            Tuple<Button, int> btnTuple = GetButton(deleteButton.Name, -1);
            if (MessageBox.Show("Are you sure you want to delete \"" + btnTuple.Item1.Text + "\"?", "Delete Save", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                Directory.Delete(".\\Saves\\" + btnTuple.Item1.Text, true);

                GameSaves.Remove(btnTuple.Item1.Text);
                ButtonPanel.SuspendLayout();
                for (int i = 0; i < 3; i++) {
                    ButtonPanel.Controls.RemoveAt(btnTuple.Item2 - 1);
                }

                int totalButtonHeight = 0;
                for (int i = 0; i < GameSaves.Count; i++) {
                    totalButtonHeight += ButtonSize.Height;
                }

                if (totalButtonHeight < ButtonPanel.Height - 39) {
                    ButtonPanel.Padding = new Padding(0, 0, 0, 0);
                    ButtonPanel.AutoScroll = false;
                    ButtonPanel.HorizontalScroll.Enabled = false;
                    ButtonPanel.HorizontalScroll.Visible = false;
                    ButtonPanel.VerticalScroll.Enabled = false;
                    ButtonPanel.VerticalScroll.Visible = false;
                }

                ButtonPanel.ResumeLayout();
            }
        }

        private void createSaveButton_Click(object sender, EventArgs e) { // Create Save Button
            string saveName = Controls.Find("input_saveName", true).FirstOrDefault().Text;
            if (saveName == "") {
                MessageBox.Show("Please enter a name for the save.");
                return;
            }

            if (saveName.Contains("\n")) {
                MessageBox.Show("Save name cannot contain new lines.");
                return;
            }

            if (saveName.Length < 3) {
                MessageBox.Show("Save name must be at least 3 characters long.");
                return;
            }

            if (!Directory.Exists(SavePath + "\\Player")) {
                MessageBox.Show("Save does not exist! Make sure you create a new save in the game.");
                return;
            }

            string path = $".\\Saves\\{saveName}";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            } else {
                ClearDirectory(path);
            }

            CopyDirectory(SavePath, path, true);
            AddSaveToList(saveName);

            SetButtonColor("Save" + saveName, ButtonSaveColor);
        }
    }
}