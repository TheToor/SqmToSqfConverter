using SqmToSqfConverter.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SqmToSqfConverter
{
    public partial class GUI : System.Windows.Forms.Form
    {
        private Reader _reader;

        public GUI()
        {
            InitializeComponent();
        }

        private void ButtonSelectFile_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = "c:\\Users\\%USERNAME%\\Documents\\";
            dialog.Filter = "sqm files (*.sqm)|*.sqm";
            dialog.Multiselect = false;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                LabelSelectFile.Text = dialog.FileName;
            }
            else
            {
                LabelSelectFile.Text = "";
            }
        }

        private void ButtonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(LabelSelectFile.Text))
                    throw new Exception("Invalid path");

                if (String.IsNullOrEmpty(LabelSaveFile.Text))
                    throw new Exception("Invalid save path");

                var options = new ConvertOptions()
                {
                    AddToGlobalArray = checkBoxAddToGlobalArray.Checked,
                    AutoDeleteEmptyGroups = checkBoxAutoDeleteEmptyGroups.Checked
                };

                _reader = new Reader(LabelSelectFile.Text);
                var missionFile = _reader.ReadMissionFile();
                if (missionFile == null)
                    throw new Exception("Failed to extract mission data");

                var parser = new Parser(missionFile);
                var sqf = parser.Parse(options);

                Debug.WriteLine("=================================");
                Debug.WriteLine("Translated SQF:");
                foreach (var line in sqf)
                {
                    Debug.WriteLine(line);
                }

                File.WriteAllLines(LabelSaveFile.Text, sqf);
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show($"[ERROR]: {ex}");
#else
                MessageBox.Show($"[ERROR]: {ex.Message}");
#endif
            }
        }

        private void ButtonSaveFile_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = "c:\\Users\\%USERNAME%\\Documents\\";
            dialog.Filter = "sqf files (*.sqf)|*.sqf";
            dialog.Multiselect = false;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = true;

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                LabelSaveFile.Text = dialog.FileName;
            }
            else
            {
                LabelSaveFile.Text = "";
            }
        }
    }
}
