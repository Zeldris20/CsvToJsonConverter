using System;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace CSV2JSON_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            openFileDialog.InitialDirectory = downloadsFolder;
            openFileDialog.Filter = "CSV Files|*.csv|All Files|*.*";

            string selectedFilePath = "";
            List<dynamic> records = null;
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)

            {
                selectedFilePath = openFileDialog.FileName;

                // Read CSV file using CsvHelper
                using (var streamReader = new StreamReader(selectedFilePath))
                using (var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {

                    records = csvReader.GetRecords<dynamic>().ToList();

                }
            }



            if (!string.IsNullOrEmpty(selectedFilePath) && records != null && records.Count > 0)
            {
                string jsonFilePath = Path.Combine(downloadsFolder, Path.GetFileNameWithoutExtension(selectedFilePath) + ".json");

                string json = JsonConvert.SerializeObject(records, Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);

                MessageBox.Show("CSV file successfully converted to JSON and saved to Downloads folder.");
           
            }
            else
            {
                MessageBox.Show("\"No data found in the CSV file.\"");

            }

        }
    }
}