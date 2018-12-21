
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace modelFat
{
    class Directories
    {
        public TreeView directoriesTree;
        public Dictionary<string, int> DirectoriesTable;

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;
        private string LastAddFile;

        public Directories(TreeView directories)
        {
            this.directoriesTree = directories;
            DirectoriesTable = new Dictionary<string, int>();
        }

        public void InitTree()
        {
            TreeNode rootNode = new TreeNode("/");
            directoriesTree.Nodes.Add(rootNode);
        }
        public bool AddFile(string path)
        {
            string[] paths = path.Split('/');
            var dir = paths[paths.Length - 2];
            var newDir = paths[paths.Length - 1];
            if (dir == "")
                dir = "/";
            /*
             * Дописать проверку на наличие в таблице директорий Пока отключаю ето чтоб не мешалось
             */
            //if (LastAddFile == newDir)
            //    return false;

            if (LastSearchText != newDir)
            {
                //It's a new Search
                CurrentNodeMatches.Clear();
                LastSearchText = dir;
                LastNodeIndex = 0;
                SearchNodes(dir, directoriesTree.Nodes[0]);
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
                this.directoriesTree.SelectedNode = selectedNode;
                this.directoriesTree.SelectedNode.Expand();
                this.directoriesTree.Select();
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
    }
}
