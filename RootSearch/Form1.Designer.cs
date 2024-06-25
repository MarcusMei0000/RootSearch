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
            this.OpenFilesButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.comboboxForEnviroments = new System.Windows.Forms.ComboBox();
            this.prefFormButton = new System.Windows.Forms.Button();
            this.sufFormButton = new System.Windows.Forms.Button();
            this.buttonInputEnviroment = new System.Windows.Forms.Button();
            this.labelHelper = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.buttonInputRoot = new System.Windows.Forms.Button();
            this.comboboxForRoot = new System.Windows.Forms.ComboBox();
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
            this.buttonInput.Location = new System.Drawing.Point(576, 219);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(119, 49);
            this.buttonInput.TabIndex = 4;
            this.buttonInput.Text = "Ввод";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxOutput.Location = new System.Drawing.Point(12, 426);
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
            this.labelSuffix.Location = new System.Drawing.Point(6, 101);
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
            this.labelFilePath.Location = new System.Drawing.Point(813, 87);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(322, 29);
            this.labelFilePath.TabIndex = 9;
            this.labelFilePath.Text = "Текущий путь сохранения:";
            // 
            // OpenFilesButton
            // 
            this.OpenFilesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OpenFilesButton.Location = new System.Drawing.Point(1226, 157);
            this.OpenFilesButton.Name = "OpenFilesButton";
            this.OpenFilesButton.Size = new System.Drawing.Size(292, 103);
            this.OpenFilesButton.TabIndex = 10;
            this.OpenFilesButton.Text = "Найти пересечение множеств";
            this.OpenFilesButton.UseVisualStyleBackColor = true;
            this.OpenFilesButton.Click += new System.EventHandler(this.OpenFilesButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // comboboxForEnviroments
            // 
            this.comboboxForEnviroments.FormattingEnabled = true;
            this.comboboxForEnviroments.Location = new System.Drawing.Point(12, 298);
            this.comboboxForEnviroments.Name = "comboboxForEnviroments";
            this.comboboxForEnviroments.Size = new System.Drawing.Size(557, 24);
            this.comboboxForEnviroments.TabIndex = 11;
            this.comboboxForEnviroments.Enter += new System.EventHandler(this.comboboxForEnviroments_Enter);
            // 
            // prefFormButton
            // 
            this.prefFormButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.prefFormButton.Location = new System.Drawing.Point(1226, 283);
            this.prefFormButton.Name = "prefFormButton";
            this.prefFormButton.Size = new System.Drawing.Size(292, 48);
            this.prefFormButton.TabIndex = 12;
            this.prefFormButton.Text = "Работа с приставками";
            this.prefFormButton.UseVisualStyleBackColor = true;
            this.prefFormButton.Click += new System.EventHandler(this.prefFormButton_Click);
            // 
            // sufFormButton
            // 
            this.sufFormButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sufFormButton.Location = new System.Drawing.Point(1226, 349);
            this.sufFormButton.Name = "sufFormButton";
            this.sufFormButton.Size = new System.Drawing.Size(292, 48);
            this.sufFormButton.TabIndex = 13;
            this.sufFormButton.Text = "Работа с суффиксами";
            this.sufFormButton.UseVisualStyleBackColor = true;
            this.sufFormButton.Click += new System.EventHandler(this.sufFormButton_Click);
            // 
            // buttonInputEnviroment
            // 
            this.buttonInputEnviroment.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInputEnviroment.Location = new System.Drawing.Point(576, 295);
            this.buttonInputEnviroment.Name = "buttonInputEnviroment";
            this.buttonInputEnviroment.Size = new System.Drawing.Size(337, 48);
            this.buttonInputEnviroment.TabIndex = 14;
            this.buttonInputEnviroment.Text = "Ввод целого окружения";
            this.buttonInputEnviroment.UseVisualStyleBackColor = true;
            this.buttonInputEnviroment.Click += new System.EventHandler(this.buttonInputEnviroment_Click);
            // 
            // labelHelper
            // 
            this.labelHelper.AutoSize = true;
            this.labelHelper.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHelper.Location = new System.Drawing.Point(1370, 20);
            this.labelHelper.Name = "labelHelper";
            this.labelHelper.Size = new System.Drawing.Size(134, 29);
            this.labelHelper.TabIndex = 15;
            this.labelHelper.Text = "СПРАВКА";
            this.labelHelper.Click += new System.EventHandler(this.labelHelper_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(701, 227);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(221, 33);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Жёсткий режим";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox2.Location = new System.Drawing.Point(917, 304);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(221, 33);
            this.checkBox2.TabIndex = 19;
            this.checkBox2.Text = "Жёсткий режим";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // buttonInputRoot
            // 
            this.buttonInputRoot.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInputRoot.Location = new System.Drawing.Point(576, 363);
            this.buttonInputRoot.Name = "buttonInputRoot";
            this.buttonInputRoot.Size = new System.Drawing.Size(337, 48);
            this.buttonInputRoot.TabIndex = 21;
            this.buttonInputRoot.Text = "Ввод корня";
            this.buttonInputRoot.UseVisualStyleBackColor = true;
            this.buttonInputRoot.Click += new System.EventHandler(this.buttonInputRoot_Click);
            // 
            // comboboxForRoot
            // 
            this.comboboxForRoot.FormattingEnabled = true;
            this.comboboxForRoot.Location = new System.Drawing.Point(12, 366);
            this.comboboxForRoot.Name = "comboboxForRoot";
            this.comboboxForRoot.Size = new System.Drawing.Size(557, 24);
            this.comboboxForRoot.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1532, 653);
            this.Controls.Add(this.buttonInputRoot);
            this.Controls.Add(this.comboboxForRoot);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.labelHelper);
            this.Controls.Add(this.buttonInputEnviroment);
            this.Controls.Add(this.sufFormButton);
            this.Controls.Add(this.prefFormButton);
            this.Controls.Add(this.comboboxForEnviroments);
            this.Controls.Add(this.OpenFilesButton);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.buttonChooseFilePath);
            this.Controls.Add(this.labelSuffix);
            this.Controls.Add(this.labelPrefix);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonInput);
            this.MinimumSize = new System.Drawing.Size(1550, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Классификативность морфем русского языка";
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

        public List<Label> labelsPref;
        public List<Label> labelsSuf;

        private Label labelFilePath;
        private Button buttonChooseFilePath;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button OpenFilesButton;
        private OpenFileDialog openFileDialog1;
        private ComboBox comboboxForEnviroments;
        private Button prefFormButton;
        private Button sufFormButton;
        private Button buttonInputEnviroment;
        private Label labelHelper;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button buttonInputRoot;
        private ComboBox comboboxForRoot;
    }
}

