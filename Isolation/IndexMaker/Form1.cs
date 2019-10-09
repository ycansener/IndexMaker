using IndexMaker.Helpers;
using IndexMaker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace IndexMaker
{
    public partial class Form1 : Form
    {
        FolderModel _folderModel;
        public Form1()
        {
            InitializeComponent();
            toolTip1.SetToolTip(treeViewResults, "Bulunduğu dizini açmak için çift tıklayınız. İşlem menüsü için sağ tıklayınız.");
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult dr = fbd.ShowDialog();
                if (dr.Equals(DialogResult.OK))
                {
                    string path = fbd.SelectedPath;
                    string name = DirectoryManager.GetName(path);
                    textBoxPath.Text = path;
                    _folderModel = new FolderModel(name, path, null);
                }
            }
        }
        private void buttonStartListing_Click(object sender, EventArgs e)
        {
            Clear();
            bool showCompletePath = checkBoxShowCompletePath.Checked;

            DirectoryManager dm = new DirectoryManager(_folderModel);
            dm.Investigate();

            FillTree(_folderModel, showCompletePath);
        }

        private void FillTree(FolderModel rootFolder, bool showCompletePath)
        {
            TreeNode rootNode = AddNode(null, rootFolder, showCompletePath);
            treeViewResults.Nodes.Add(rootNode);

            FillTree(rootNode, rootFolder, showCompletePath);
        }
        private void FillTree(TreeNode parentNode, FolderModel selectedFolder, bool showCompletePath)
        {
            TreeNode currentNode = AddNode(parentNode, selectedFolder, showCompletePath);

            foreach (var subFolder in selectedFolder.GetSubFolders())
            {
                FillTree(currentNode, subFolder, showCompletePath);
            }

            foreach (var file in selectedFolder.GetFiles())
            {
                AddNode(parentNode, file, showCompletePath);
            }
        }

        private TreeNode AddNode(TreeNode parentNode, IDirectoryItem itemToAdd, bool showCompletePath)
        {
            string nodeText = itemToAdd.Name;
            if (showCompletePath)
            {
                nodeText = itemToAdd.CompletePath;
            }

            if (parentNode != null)
                return parentNode.Nodes.Add(nodeText);

            return new TreeNode(nodeText);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            treeViewResults.Nodes.Clear();
            textBoxPath.Clear();
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchQuery;
            List<TreeNode> eslesenler = new List<TreeNode>();
            try
            {
                searchQuery = textBoxSearch.Text;
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    throw new NotImplementedException();
                }
                else throw new Exception("Aranacak kelimeyi giriniz!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void openDirectly_Click(object sender, EventArgs e)
        {
            string secilenEleman = "";
            if (treeViewResults.SelectedNode != null)
            {
                secilenEleman = treeViewResults.SelectedNode.Text;

                System.Diagnostics.Process.Start(secilenEleman);
            }
            else MessageBox.Show("Öncelikle seçim yapmalısınız.");
        }

        private void treeViewResults_DoubleClick(object sender, EventArgs e)
        {
            string secilenEleman = "";
            string dizin = "";
            if (treeViewResults.SelectedNode != null)
            {
                secilenEleman = treeViewResults.SelectedNode.Text;

                System.Diagnostics.Process.Start(dizin + secilenEleman);
            }
            else MessageBox.Show("Öncelikle seçim yapmalısınız.");
        }

        private void checkBoxShowCompletePath_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
