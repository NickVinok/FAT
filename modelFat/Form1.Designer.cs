namespace modelFat
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.downloadFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.directories = new System.Windows.Forms.TreeView();
            this.pathToFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveToPath = new System.Windows.Forms.Button();
            this.deleteToPath = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // downloadFile
            // 
            this.downloadFile.Font = new System.Drawing.Font("Courier New", 10.25F);
            this.downloadFile.Location = new System.Drawing.Point(12, 12);
            this.downloadFile.Name = "downloadFile";
            this.downloadFile.Size = new System.Drawing.Size(121, 23);
            this.downloadFile.TabIndex = 0;
            this.downloadFile.Text = "Открыть файл";
            this.downloadFile.UseVisualStyleBackColor = true;
            this.downloadFile.Click += new System.EventHandler(this.downloadFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(491, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(297, 426);
            this.dataGridView1.TabIndex = 1;
            // 
            // directories
            // 
            this.directories.Location = new System.Drawing.Point(12, 93);
            this.directories.Name = "directories";
            this.directories.Size = new System.Drawing.Size(224, 345);
            this.directories.TabIndex = 2;
            // 
            // pathToFile
            // 
            this.pathToFile.Location = new System.Drawing.Point(76, 50);
            this.pathToFile.Name = "pathToFile";
            this.pathToFile.Size = new System.Drawing.Size(263, 20);
            this.pathToFile.TabIndex = 3;
            this.pathToFile.Text = "/";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 12F);
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Путь:";
            // 
            // saveToPath
            // 
            this.saveToPath.Font = new System.Drawing.Font("Courier New", 10.25F);
            this.saveToPath.Location = new System.Drawing.Point(139, 12);
            this.saveToPath.Name = "saveToPath";
            this.saveToPath.Size = new System.Drawing.Size(97, 23);
            this.saveToPath.TabIndex = 5;
            this.saveToPath.Text = "Сохранить по выбранному пути";
            this.saveToPath.UseVisualStyleBackColor = true;
            this.saveToPath.Click += new System.EventHandler(this.saveToPath_Click);
            // 
            // deleteToPath
            // 
            this.deleteToPath.Font = new System.Drawing.Font("Courier New", 10.25F);
            this.deleteToPath.Location = new System.Drawing.Point(242, 12);
            this.deleteToPath.Name = "deleteToPath";
            this.deleteToPath.Size = new System.Drawing.Size(97, 23);
            this.deleteToPath.TabIndex = 6;
            this.deleteToPath.Text = "Удалить";
            this.deleteToPath.UseVisualStyleBackColor = true;
            this.deleteToPath.Click += new System.EventHandler(this.deleteToPath_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.deleteToPath);
            this.Controls.Add(this.saveToPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pathToFile);
            this.Controls.Add(this.directories);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.downloadFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button downloadFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TreeView directories;
        private System.Windows.Forms.TextBox pathToFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveToPath;
        private System.Windows.Forms.Button deleteToPath;
    }
}

