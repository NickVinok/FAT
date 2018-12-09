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
        private Dictionary<string, int> DirectoriesTable;



        public void CreateFAT()
        {
            FAT = new Dictionary<int, int>(100) { };
            for (int i = 0; i < 100; i++)
            {
                FAT.Add(i, 0);
            }
            listL = new FileList<BitArray>(FAT);
            FO = new FileOperate<BitArray>(listL);
            DirectoriesTable = new Dictionary<string, int>();
            InitTree();
        }
        private byte[] sourceFile;
        private string sourceNameFile;
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
        }

        public void ShowDirectory()
        {
            viewData = new DataGridView();
            DataTable table = new DataTable();
            table.Columns.Add();
            table.Columns.Add();
            var i = 0;
            foreach (var item in DirectoriesTable)
            {
                table.Rows.Add(table.NewRow());
                table.Rows[i][0] = item.Key;                
                table.Rows[i][1] = item.Value;
                i++;
            }
            dataGridView2.DataSource = table;
        }


        public void InitTree() {
            TreeNode rootNode = new TreeNode("/");
            directories.Nodes.Add(rootNode);

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;
        private string LastAddFile;

        /**
         * TODO
         * ВЫНЕСТИ ВСЕ ГОВНО С СОЗДАНИЕМ ДИРЕКТРИЙ В ОТДЕЛЬНЫЙ КЛАСС МУДИЛА!
         * */



        //Создание папки
        private void saveToPath_Click_1(object sender, EventArgs e)
        {
            string[] paths = pathToFile.Text.Split('/');
            if(!DirectoriesTable.Keys.Contains("/"+paths[paths.Length-2])) 
                AddFile(pathToFile.Text);


        }

        public bool AddFile(string path)
        {
            string[] paths = path.Split('/');
            var dir = paths[paths.Length - 2];
            var newDir = paths[paths.Length - 1];
            if (dir == "")
                dir = "/";

            if (LastAddFile == newDir)
                return false;

            if (LastSearchText != newDir)
            {
                //It's a new Search
                CurrentNodeMatches.Clear();
                LastSearchText = dir;
                LastNodeIndex = 0;
                SearchNodes(dir, directories.Nodes[0]);
            }

            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                try
                {
                    selectedNode.Nodes[LastNodeIndex - 1].Nodes.Add(new TreeNode(newDir));
                    LastAddFile = newDir;
                }
                catch (Exception)
                {
                    selectedNode.Nodes.Add(new TreeNode(newDir));
                    LastAddFile = newDir;
                }

                var tmp = selectedNode.Nodes;

                LastNodeIndex++;
                this.directories.SelectedNode = selectedNode;
                this.directories.SelectedNode.Expand();
                this.directories.Select();
                return true;
            }
            else
            {
                MessageBox.Show("Данной директории не существует");
                return false;
            }
        }
        
        private void SearchNodes(string SearchText, TreeNode StartNode)
        {            
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                };
                StartNode = StartNode.NextNode;
            };
        }

        
        //Заполнение таблицы директории файлами
        private void saveToPath_Click(object sender, EventArgs e)
        {
            if(AddFile(pathToFile.Text))
            {
                saveFile();
                DirectoriesTable.Add(pathToFile.Text, listL.GetFirstIndex());
                ShowFAT();
                ShowDirectory();
            }
            
        }

        private void deleteToPath_Click_1(object sender, EventArgs e)
        {
            if(directories.SelectedNode.Text != "/")
            {
                directories.SelectedNode.Remove();
            }
            else
            {
                MessageBox.Show("Нельзя удалить корень");
            }
        }
    }
}
