using System;
using System.Windows.Forms;

using System.Collections.Generic;
//using static System.Collections.Generic.List;

namespace RootSearch
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
            this.components = new System.ComponentModel.Container();
            this.errorProviderPrefix = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderSuffix = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonInput = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.labelPrefix = new System.Windows.Forms.Label();
            this.labelSuffix = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonChooseFilePath = new System.Windows.Forms.Button();
            this.labelFilePath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPrefix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderSuffix)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProviderPrefix
            // 
            this.errorProviderPrefix.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProviderPrefix.ContainerControl = this;
            // 
            // errorProviderSuffix
            // 
            this.errorProviderSuffix.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProviderSuffix.ContainerControl = this;
            // 
            // buttonInput
            // 
            this.buttonInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInput.Location = new System.Drawing.Point(707, 274);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(112, 48);
            this.buttonInput.TabIndex = 4;
            this.buttonInput.Text = "Ввод";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxOutput.Location = new System.Drawing.Point(12, 328);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.Size = new System.Drawing.Size(1506, 100);
            this.textBoxOutput.TabIndex = 5;
            this.textBoxOutput.Text = "Здесь будут названия сгенерированных файлов.";
            // 
            // labelPrefix
            // 
            this.labelPrefix.AutoSize = true;
            this.labelPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPrefix.Location = new System.Drawing.Point(10, 20);
            this.labelPrefix.Name = "labelPrefix";
            this.labelPrefix.Size = new System.Drawing.Size(678, 36);
            this.labelPrefix.TabIndex = 6;
            this.labelPrefix.Text = "Выберите приставки из выпадающих списков";
            // 
            // labelSuffix
            // 
            this.labelSuffix.AutoSize = true;
            this.labelSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSuffix.Location = new System.Drawing.Point(12, 148);
            this.labelSuffix.Name = "labelSuffix";
            this.labelSuffix.Size = new System.Drawing.Size(689, 36);
            this.labelSuffix.TabIndex = 7;
            this.labelSuffix.Text = "Выберите суффиксы из выпадающих списков";
            // 
            // buttonChooseFilePath
            // 
            this.buttonChooseFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonChooseFilePath.Location = new System.Drawing.Point(815, 13);
            this.buttonChooseFilePath.Name = "buttonChooseFilePath";
            this.buttonChooseFilePath.Size = new System.Drawing.Size(241, 71);
            this.buttonChooseFilePath.TabIndex = 8;
            this.buttonChooseFilePath.Text = "Выбрать путь для сохранения файлов";
            this.buttonChooseFilePath.UseVisualStyleBackColor = true;
            this.buttonChooseFilePath.Click += new System.EventHandler(this.buttonChooseFilePath_Click);
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFilePath.Location = new System.Drawing.Point(816, 87);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(322, 29);
            this.labelFilePath.TabIndex = 9;
            this.labelFilePath.Text = "Текущий путь сохранения:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1530, 440);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.buttonChooseFilePath);
            this.Controls.Add(this.labelSuffix);
            this.Controls.Add(this.labelPrefix);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonInput);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Классификация корней русского языка";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPrefix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderSuffix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProviderPrefix;
        private System.Windows.Forms.Label labelSuffix;
        private System.Windows.Forms.Label labelPrefix;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.ErrorProvider errorProviderSuffix;

        public List<System.Windows.Forms.ComboBox> comboBoxesPref;
        public List<System.Windows.Forms.ComboBox> comboBoxesSuf;
        private Label labelFilePath;
        private Button buttonChooseFilePath;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}

