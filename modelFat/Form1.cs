using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace modelFat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateFAT();
        }

        private Dictionary<int, int> FAT;
        FileList<BitArray> listL;
        DataGridView viewData;
        FileOperate<BitArray> FO;



        public void CreateFAT()
        {
            FAT = new Dictionary<int, int>(100) { };
            for (int i = 0; i < 100; i++)
            {
                FAT.Add(i, 0);
            }
            listL = new FileList<BitArray>(FAT);
            FO = new FileOperate<BitArray>(listL);
        }
        private byte[] sourceFile;
        private void downloadFile_Click(object sender, EventArgs e)
        {           
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {                
                sourceFile = File.ReadAllBytes(openFileDialog1.FileName);
                BitArray[] sepFile = FO.Separate(sourceFile);

                for (int i = 0; i < sepFile.Length; i++)
                {
                    bool EOF = i + 1 == sepFile.Length;
                    listL.Add(sepFile[i], EOF: EOF);
                }
            }
            ShowFAT();
            var FILE = FO.Combine(0);            
        }

        public void ShowFAT()
        {
            viewData = new DataGridView();
            DataTable table = new DataTable();
            table.Columns.Add();
            table.Columns.Add();
            var i = 0;
            foreach (var item in FAT)
            {
                table.Rows.Add(table.NewRow());
                table.Rows[item.Key][0] = i;
                var val = item.Value == -3 ? "EOF" : item.Value.ToString();
                table.Rows[item.Key][1] = val;
                i++;
            }
            dataGridView1.DataSource = table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
