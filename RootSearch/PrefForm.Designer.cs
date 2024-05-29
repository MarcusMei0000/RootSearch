﻿namespace RootSearch
{
    partial class PrefForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.inputCombobox = new System.Windows.Forms.ComboBox();
            this.expandAllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeView1.Indent = 40;
            this.treeView1.ItemHeight = 25;
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(624, 750);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(676, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(432, 29);
            this.label1.TabIndex = 10;
            this.label1.Text = "Выберите первый суффикс цепочки";
            // 
            // inputCombobox
            // 
            this.inputCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputCombobox.FormattingEnabled = true;
            this.inputCombobox.Location = new System.Drawing.Point(681, 78);
            this.inputCombobox.Name = "inputCombobox";
            this.inputCombobox.Size = new System.Drawing.Size(475, 37);
            this.inputCombobox.TabIndex = 12;
            // 
            // expandAllButton
            // 
            this.expandAllButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.expandAllButton.Location = new System.Drawing.Point(681, 335);
            this.expandAllButton.Name = "expandAllButton";
            this.expandAllButton.Size = new System.Drawing.Size(309, 46);
            this.expandAllButton.TabIndex = 13;
            this.expandAllButton.Text = "Раскрыть все узлы";
            this.expandAllButton.UseVisualStyleBackColor = true;
            this.expandAllButton.Click += new System.EventHandler(this.expandAllButton_Click);
            // 
            // PrefForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1530, 903);
            this.Controls.Add(this.expandAllButton);
            this.Controls.Add(this.inputCombobox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Name = "PrefForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Суффиксальные окружения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox inputCombobox;
        private System.Windows.Forms.Button expandAllButton;
    }
}