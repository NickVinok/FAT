using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace modelFat
{
    /**
     * TODO
     * Доделать анализ фрагментации
     * Дефрагментацию
     * Исправить баги с удалением
     * Добавить возможность перезаписи файлов
     * И тогда придется писать поиск ошибок и фикс их а надо ли мне это
     * */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateFAT();
            UpdateSystemInfo();
        }

        private Dictionary<int, int> FAT;
        FileList<BitArray> listL;
        DataGridView viewData;
        FileOperate<BitArray> FO;
        //private Dictionary<string, int> DirectoriesTable;
        Directories dirs;



        public void CreateFAT()
        {
            FAT = new Dictionary<int, int>(100) { };
            for (int i = 0; i < 100; i++)
            {
                FAT.Add(i, 0);
            }
            listL = new FileList<BitArray>(FAT);
            FO = new FileOperate<BitArray>(listL);
            //DirectoriesTable = new Dictionary<string, int>();
            this.dirs = new Directories(directories);
            dirs.InitTree();
            //InitTree();
        }
        private byte[] sourceFile;
        //private string sourceNameFile;
        private void downloadFile_Click(object sender, EventArgs e)
        {           
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {                
                sourceFile = File.ReadAllBytes(openFileDialog1.FileName);                
            }
        }

        public void saveFile()
        {
            BitArray[] sepFile = FO.Separate(sourceFile);

            for (int i = 0; i < sepFile.Length; i++)
            {
                bool EOF = i + 1 == sepFile.Length;
                listL.Add(sepFile[i], EOF: EOF);
            }
            
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
            UpdateSystemInfo();
        }

        public void ShowDirectory()
        {
            viewData = new DataGridView();
            DataTable table = new DataTable();
            table.Columns.Add();
            table.Columns.Add();
            var i = 0;
            foreach (var item in dirs.DirectoriesTable)
            {
                table.Rows.Add(table.NewRow());
                table.Rows[i][0] = item.Key;                
                table.Rows[i][1] = item.Value;
                i++;
            }
            dataGridView2.DataSource = table;
        }


        

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        


        //Создание папки
        private void saveToPath_Click_1(object sender, EventArgs e)
        {
            string[] paths = pathToFile.Text.Split('/');
            
            if(!dirs.DirectoriesTable.Keys.Contains("/"+paths[paths.Length-2]))
                dirs.AddFile(pathToFile.Text);
        }

        

        
        //Заполнение таблицы директории файлами Сохранение файла
        private void saveToPath_Click(object sender, EventArgs e)
        {
            if(dirs.AddFile(pathToFile.Text))
            {
                //Если идет запись по тому же адресу то перезаписывать 
                dirs.DirectoriesTable.Add(pathToFile.Text, listL.GetFirstEmptyBlock());
                saveFile();                
                
                ShowFAT();
                ShowDirectory();
            }
            
        }

        private double EmptySpacePercent()
        {
            int emptyBlock = 0;
            foreach (var row in this.FAT)
            {
                //Console.WriteLine();
                if (row.Value == 0)
                    emptyBlock++;
            }
            return ((double)emptyBlock/100)*100;
        }

        private void UpdateSystemInfo()
        {
            double empt = EmptySpacePercent();
            emptSpicePrc.Text = empt.ToString() + "%";
            progressBar1.Value = Convert.ToInt32(empt);

            spaceKB.Text = (((double)FO.sizeBlock * FAT.Count() / 8000) * (empt / 100)).ToString() + "Kb";
            lossFrag.Text = CalcLossSpaceDisk().ToString() + "Bit";
            CalcLossSpaceDisk();
        }

        private double CalcLossSpaceDisk()
        {
            return (double)dirs.DirectoriesTable.Count() * (double)FO.sizeBlock / 2;
        }

        private void deleteToPath_Click_1(object sender, EventArgs e)
        {
            if (directories.SelectedNode.Text != "/")
            {
                //string pathFile = directories.SelectedNode.Text;
                int startIndexFile = dirs.DirectoriesTable[pathToFile.Text];
                listL.Remove(startIndexFile);
                dirs.DirectoriesTable.Remove(pathToFile.Text);
                directories.SelectedNode.Remove();
                ShowFAT();
                ShowDirectory();
            }
            else
            {
                MessageBox.Show("Нельзя удалить корень файловой системы");
            }
        }
    }
}
