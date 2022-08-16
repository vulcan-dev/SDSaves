using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// Todo: Cleanup paths.

namespace SDSaves {
    public partial class SaveManager : Form {
        private Size ButtonSize = new Size(294, 34);
        private Color ButtonColor = Color.FromArgb(47, 51, 61);
        private Color ButtonSaveColor = Color.FromArgb(255, 131, 114);
        private TableLayoutPanel ButtonPanel;
        private List<string> GameSaves = new List<string>();
        private string SavePath;
        private string ActiveSave;

        public void AddSaveToList(string name) {
            if (!GameSaves.Contains(name)) {
                GameSaves.Add(name);
            } else {
                return; // Overwrite existing save, we don't need to add it again.
            }

            Button saveNameButton = new Button();
            saveNameButton.FlatStyle = FlatStyle.Flat;
            saveNameButton.FlatAppearance.BorderSize = 0;
            saveNameButton.Text = name;
            saveNameButton.BackColor = ButtonColor;
            saveNameButton.ForeColor = Color.White;
            saveNameButton.Size = ButtonSize;
            saveNameButton.Name = "Save" + name;

            Button deleteButton = new Button();
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.Text = "Delete";
            deleteButton.BackColor = ButtonColor;
            deleteButton.ForeColor = Color.White;
            deleteButton.Size = ButtonSize;
            deleteButton.Name = "Delete" + name;

            Button overwriteButton = new Button();
            overwriteButton.FlatStyle = FlatStyle.Flat;
            overwriteButton.FlatAppearance.BorderSize = 0;
            overwriteButton.Text = "Save";
            overwriteButton.BackColor = ButtonColor;
            overwriteButton.ForeColor = Color.White;
            overwriteButton.Size = ButtonSize;
            overwriteButton.Name = "Overwrite" + name;

            deleteButton.Click += DeleteButton_Click;
            saveNameButton.Click += SaveNameButton_Click;
            overwriteButton.Click += OverwriteButton_Click;

            ButtonPanel.Controls.Add(saveNameButton);
            ButtonPanel.Controls.Add(deleteButton);
            ButtonPanel.Controls.Add(overwriteButton);
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

        private void SetButtonColor(string name, Color color) {
            foreach (Control control in ButtonPanel.Controls) {
                if (control.Name == name) {
                    control.BackColor = color;
                } else {
                    control.BackColor = ButtonColor;
                }
            }
        }

        private string GetCurrentSave() {
            foreach (string save in GameSaves) {
                if (GetHash("Saves\\" + save) == GetHash(SavePath)) {
                    return save;
                }
            }

            return "";
        }

        private void OverwriteButton_Click(object sender, EventArgs e) {
            Button overwriteButton = (Button)sender;
            TableLayoutPanel panel = (TableLayoutPanel)overwriteButton.Parent;

            int rowIndex = -1;
            for (int i = 0; i < panel.Controls.Count; i++) {
                if (panel.Controls[i].Name == overwriteButton.Name) {
                    rowIndex = i;
                    break;
                }
            }

            if (rowIndex == -1) {
                MessageBox.Show("Error finding save!");
                return;
            }

            string saveName = panel.Controls[rowIndex-2].Text;
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

        private void SaveNameButton_Click(object sender, EventArgs e) {
            Button loadButton = (Button)sender;
            TableLayoutPanel panel = (TableLayoutPanel)loadButton.Parent;

            int rowIndex = -1;
            for (int i = 0; i < panel.Controls.Count; i++) {
                if (panel.Controls[i].Name == loadButton.Name) {
                    rowIndex = i;
                    break;
                }
            }

            if (rowIndex == -1) {
                MessageBox.Show("Error finding save!");
                return;
            }

            string saveName = panel.Controls[rowIndex].Text;

            CopyDirectory(SavePath, ".\\BackupSave\\");
            ClearDirectory(SavePath);
            CopyDirectory(".\\Saves\\" + saveName, SavePath);

            SetButtonColor("Save" + saveName, ButtonSaveColor);
            ActiveSave = saveName;
        }

        private void DeleteButton_Click(object sender, EventArgs e) {
            Button deleteButton = (Button)sender;
            TableLayoutPanel panel = (TableLayoutPanel)deleteButton.Parent;

            int rowIndex = -1;
            for (int i = 0; i < panel.Controls.Count; i++) {
                if (panel.Controls[i].Name == deleteButton.Name) {
                    rowIndex = i;
                    break;
                }
            }
            
            if (rowIndex == -1) {
                MessageBox.Show("Error finding button!");
                return;
            }

            string saveName = panel.Controls[rowIndex-1].Text;
            if (MessageBox.Show("Are you sure you want to delete \"" + saveName + "\"?", "Delete Save", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                Directory.Delete(".\\Saves\\" + saveName, true);

                GameSaves.Remove(saveName);

                for (int i = 0; i < 3; i++) {
                    panel.Controls.RemoveAt(rowIndex-1);
                }
            }
        }

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
                foreach (string dir in saves) {
                    AddSaveToList(dir.Substring(dir.LastIndexOf("\\") + 1));
                }
            }

            if (!Directory.Exists(".\\BackupSave\\")) {
                Directory.CreateDirectory(".\\BackupSave\\");
            }

            string currentSave = GetCurrentSave();
            if (currentSave != "") {
                SetButtonColor("Save" + currentSave, ButtonSaveColor);
                ActiveSave = currentSave;
            }
        }

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

        private void createSaveButton_Click(object sender, EventArgs e) {
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
            ActiveSave = saveName;
        }
    }
}