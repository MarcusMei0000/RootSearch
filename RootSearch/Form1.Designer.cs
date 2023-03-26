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
            this.textBoxPref0 = new System.Windows.Forms.TextBox();
            this.textBoxSuf0 = new System.Windows.Forms.TextBox();
            this.errorProviderPrefix = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProviderSuffix = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonInput = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.labelPrefix = new System.Windows.Forms.Label();
            this.labelSuffix = new System.Windows.Forms.Label();
            this.textBoxPref1 = new System.Windows.Forms.TextBox();
            this.textBoxPref2 = new System.Windows.Forms.TextBox();
            this.textBoxPref3 = new System.Windows.Forms.TextBox();
            this.textBoxSuf1 = new System.Windows.Forms.TextBox();
            this.textBoxSuf2 = new System.Windows.Forms.TextBox();
            this.textBoxSuf3 = new System.Windows.Forms.TextBox();
            this.textBoxSuf4 = new System.Windows.Forms.TextBox();
            this.textBoxSuf5 = new System.Windows.Forms.TextBox();
            this.textBoxSuf6 = new System.Windows.Forms.TextBox();
            this.textBoxSuf7 = new System.Windows.Forms.TextBox();
            this.textBoxSuf8 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPrefix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderSuffix)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPref0
            // 
            this.errorProviderPrefix.SetError(this.textBoxPref0, "Неверный ввод");
            this.textBoxPref0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPref0.Location = new System.Drawing.Point(13, 49);
            this.textBoxPref0.Name = "textBoxPref0";
            this.textBoxPref0.Size = new System.Drawing.Size(76, 30);
            this.textBoxPref0.TabIndex = 0;
            // 
            // textBoxSuf0
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf0, "Неверный ввод");
            this.textBoxSuf0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf0.Location = new System.Drawing.Point(12, 125);
            this.textBoxSuf0.Name = "textBoxSuf0";
            this.textBoxSuf0.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf0.TabIndex = 3;
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
            this.buttonInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInput.Location = new System.Drawing.Point(418, 258);
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
            this.textBoxOutput.Location = new System.Drawing.Point(13, 192);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.Size = new System.Drawing.Size(898, 60);
            this.textBoxOutput.TabIndex = 5;
            // 
            // labelPrefix
            // 
            this.labelPrefix.AutoSize = true;
            this.labelPrefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPrefix.Location = new System.Drawing.Point(13, 21);
            this.labelPrefix.Name = "labelPrefix";
            this.labelPrefix.Size = new System.Drawing.Size(193, 25);
            this.labelPrefix.TabIndex = 6;
            this.labelPrefix.Text = "Введите префиксы";
            // 
            // labelSuffix
            // 
            this.labelSuffix.AutoSize = true;
            this.labelSuffix.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSuffix.Location = new System.Drawing.Point(12, 97);
            this.labelSuffix.Name = "labelSuffix";
            this.labelSuffix.Size = new System.Drawing.Size(198, 25);
            this.labelSuffix.TabIndex = 7;
            this.labelSuffix.Text = "Введите суффиксы";
            // 
            // textBoxPref1
            // 
            this.errorProviderPrefix.SetError(this.textBoxPref1, "Неверный ввод");
            this.textBoxPref1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPref1.Location = new System.Drawing.Point(113, 49);
            this.textBoxPref1.Name = "textBoxPref1";
            this.textBoxPref1.Size = new System.Drawing.Size(76, 30);
            this.textBoxPref1.TabIndex = 8;
            // 
            // textBoxPref2
            // 
            this.errorProviderPrefix.SetError(this.textBoxPref2, "Неверный ввод");
            this.textBoxPref2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPref2.Location = new System.Drawing.Point(212, 49);
            this.textBoxPref2.Name = "textBoxPref2";
            this.textBoxPref2.Size = new System.Drawing.Size(76, 30);
            this.textBoxPref2.TabIndex = 9;
            // 
            // textBoxPref3
            // 
            this.errorProviderPrefix.SetError(this.textBoxPref3, "Неверный ввод");
            this.textBoxPref3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPref3.Location = new System.Drawing.Point(313, 49);
            this.textBoxPref3.Name = "textBoxPref3";
            this.textBoxPref3.Size = new System.Drawing.Size(76, 30);
            this.textBoxPref3.TabIndex = 10;
            // 
            // textBoxSuf1
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf1, "Неверный ввод");
            this.textBoxSuf1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf1.Location = new System.Drawing.Point(113, 125);
            this.textBoxSuf1.Name = "textBoxSuf1";
            this.textBoxSuf1.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf1.TabIndex = 11;
            // 
            // textBoxSuf2
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf2, "Неверный ввод");
            this.textBoxSuf2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf2.Location = new System.Drawing.Point(212, 125);
            this.textBoxSuf2.Name = "textBoxSuf2";
            this.textBoxSuf2.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf2.TabIndex = 12;
            // 
            // textBoxSuf3
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf3, "Неверный ввод");
            this.textBoxSuf3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf3.Location = new System.Drawing.Point(313, 125);
            this.textBoxSuf3.Name = "textBoxSuf3";
            this.textBoxSuf3.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf3.TabIndex = 13;
            // 
            // textBoxSuf4
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf4, "Неверный ввод");
            this.textBoxSuf4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf4.Location = new System.Drawing.Point(418, 125);
            this.textBoxSuf4.Name = "textBoxSuf4";
            this.textBoxSuf4.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf4.TabIndex = 14;
            // 
            // textBoxSuf5
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf5, "Неверный ввод");
            this.textBoxSuf5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf5.Location = new System.Drawing.Point(521, 125);
            this.textBoxSuf5.Name = "textBoxSuf5";
            this.textBoxSuf5.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf5.TabIndex = 15;
            // 
            // textBoxSuf6
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf6, "Неверный ввод");
            this.textBoxSuf6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf6.Location = new System.Drawing.Point(624, 125);
            this.textBoxSuf6.Name = "textBoxSuf6";
            this.textBoxSuf6.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf6.TabIndex = 16;
            // 
            // textBoxSuf7
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf7, "Неверный ввод");
            this.textBoxSuf7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf7.Location = new System.Drawing.Point(728, 125);
            this.textBoxSuf7.Name = "textBoxSuf7";
            this.textBoxSuf7.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf7.TabIndex = 17;
            // 
            // textBoxSuf8
            // 
            this.errorProviderSuffix.SetError(this.textBoxSuf8, "Неверный ввод");
            this.textBoxSuf8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSuf8.Location = new System.Drawing.Point(834, 125);
            this.textBoxSuf8.Name = "textBoxSuf8";
            this.textBoxSuf8.Size = new System.Drawing.Size(77, 30);
            this.textBoxSuf8.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 332);
            this.Controls.Add(this.textBoxSuf8);
            this.Controls.Add(this.textBoxSuf7);
            this.Controls.Add(this.textBoxSuf6);
            this.Controls.Add(this.textBoxSuf5);
            this.Controls.Add(this.textBoxSuf4);
            this.Controls.Add(this.textBoxSuf3);
            this.Controls.Add(this.textBoxSuf2);
            this.Controls.Add(this.textBoxSuf1);
            this.Controls.Add(this.textBoxPref3);
            this.Controls.Add(this.textBoxPref2);
            this.Controls.Add(this.textBoxPref1);
            this.Controls.Add(this.labelSuffix);
            this.Controls.Add(this.labelPrefix);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonInput);
            this.Controls.Add(this.textBoxSuf0);
            this.Controls.Add(this.textBoxPref0);
            this.Name = "Form1";
            this.Text = "Классификация корней русского языка";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPrefix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderSuffix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPref0;
        private System.Windows.Forms.ErrorProvider errorProviderPrefix;
        private System.Windows.Forms.Label labelSuffix;
        private System.Windows.Forms.Label labelPrefix;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.TextBox textBoxSuf0;
        private System.Windows.Forms.ErrorProvider errorProviderSuffix;
        private System.Windows.Forms.TextBox textBoxPref1;
        private System.Windows.Forms.TextBox textBoxPref3;
        private System.Windows.Forms.TextBox textBoxPref2;
        private System.Windows.Forms.TextBox textBoxSuf8;
        private System.Windows.Forms.TextBox textBoxSuf7;
        private System.Windows.Forms.TextBox textBoxSuf6;
        private System.Windows.Forms.TextBox textBoxSuf5;
        private System.Windows.Forms.TextBox textBoxSuf4;
        private System.Windows.Forms.TextBox textBoxSuf3;
        private System.Windows.Forms.TextBox textBoxSuf2;
        private System.Windows.Forms.TextBox textBoxSuf1;
    }
}

