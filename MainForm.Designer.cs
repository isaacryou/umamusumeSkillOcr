
namespace UmamusumeSkillOCR
{
    partial class MainForm
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
            this.languageBox = new System.Windows.Forms.ComboBox();
            this.languageTextBox = new System.Windows.Forms.TextBox();
            this.previewScreenButton = new System.Windows.Forms.Button();
            this.screenXText = new System.Windows.Forms.TextBox();
            this.screenYText = new System.Windows.Forms.TextBox();
            this.screenWidthText = new System.Windows.Forms.TextBox();
            this.screenHeightText = new System.Windows.Forms.TextBox();
            this.screenXTextEdit = new System.Windows.Forms.TextBox();
            this.screenYTextEdit = new System.Windows.Forms.TextBox();
            this.screenWidthTextEdit = new System.Windows.Forms.TextBox();
            this.screenHeightTextEdit = new System.Windows.Forms.TextBox();
            this.configText = new System.Windows.Forms.TextBox();
            this.skillListTextBox = new System.Windows.Forms.TextBox();
            this.windowStatusText = new System.Windows.Forms.TextBox();
            this.loadConfigButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // languageBox
            // 
            this.languageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageBox.FormattingEnabled = true;
            this.languageBox.Location = new System.Drawing.Point(147, 48);
            this.languageBox.Name = "languageBox";
            this.languageBox.Size = new System.Drawing.Size(121, 21);
            this.languageBox.TabIndex = 13;
            this.languageBox.SelectedIndexChanged += new System.EventHandler(this.languageBox_SelectedIndexChanged);
            // 
            // languageTextBox
            // 
            this.languageTextBox.Location = new System.Drawing.Point(24, 49);
            this.languageTextBox.Name = "languageTextBox";
            this.languageTextBox.ReadOnly = true;
            this.languageTextBox.Size = new System.Drawing.Size(117, 20);
            this.languageTextBox.TabIndex = 14;
            this.languageTextBox.Text = "Translation Language:";
            this.languageTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // previewScreenButton
            // 
            this.previewScreenButton.Location = new System.Drawing.Point(443, 145);
            this.previewScreenButton.Name = "previewScreenButton";
            this.previewScreenButton.Size = new System.Drawing.Size(75, 23);
            this.previewScreenButton.TabIndex = 15;
            this.previewScreenButton.Text = "Preview";
            this.previewScreenButton.UseVisualStyleBackColor = true;
            this.previewScreenButton.Click += new System.EventHandler(this.previewScreenButton_Click);
            // 
            // screenXText
            // 
            this.screenXText.Location = new System.Drawing.Point(24, 93);
            this.screenXText.Name = "screenXText";
            this.screenXText.ReadOnly = true;
            this.screenXText.Size = new System.Drawing.Size(117, 20);
            this.screenXText.TabIndex = 16;
            this.screenXText.Text = "Screen X: ";
            // 
            // screenYText
            // 
            this.screenYText.Location = new System.Drawing.Point(24, 119);
            this.screenYText.Name = "screenYText";
            this.screenYText.ReadOnly = true;
            this.screenYText.Size = new System.Drawing.Size(117, 20);
            this.screenYText.TabIndex = 17;
            this.screenYText.Text = "Screen Y: ";
            // 
            // screenWidthText
            // 
            this.screenWidthText.Location = new System.Drawing.Point(274, 93);
            this.screenWidthText.Name = "screenWidthText";
            this.screenWidthText.ReadOnly = true;
            this.screenWidthText.Size = new System.Drawing.Size(117, 20);
            this.screenWidthText.TabIndex = 18;
            this.screenWidthText.Text = "Screen Width: ";
            // 
            // screenHeightText
            // 
            this.screenHeightText.Location = new System.Drawing.Point(274, 119);
            this.screenHeightText.Name = "screenHeightText";
            this.screenHeightText.ReadOnly = true;
            this.screenHeightText.Size = new System.Drawing.Size(117, 20);
            this.screenHeightText.TabIndex = 19;
            this.screenHeightText.Text = "Screen Height: ";
            // 
            // screenXTextEdit
            // 
            this.screenXTextEdit.Location = new System.Drawing.Point(147, 93);
            this.screenXTextEdit.Name = "screenXTextEdit";
            this.screenXTextEdit.Size = new System.Drawing.Size(121, 20);
            this.screenXTextEdit.TabIndex = 20;
            this.screenXTextEdit.TextChanged += new System.EventHandler(this.screenXTextEdit_TextChanged);
            this.screenXTextEdit.LostFocus += new System.EventHandler(this.screenXTextEdit_LostFocus);
            // 
            // screenYTextEdit
            // 
            this.screenYTextEdit.Location = new System.Drawing.Point(147, 119);
            this.screenYTextEdit.Name = "screenYTextEdit";
            this.screenYTextEdit.Size = new System.Drawing.Size(121, 20);
            this.screenYTextEdit.TabIndex = 21;
            this.screenYTextEdit.TextChanged += new System.EventHandler(this.screenYTextEdit_TextChanged);
            this.screenYTextEdit.LostFocus += new System.EventHandler(this.screenYTextEdit_LostFocus);
            // 
            // screenWidthTextEdit
            // 
            this.screenWidthTextEdit.Location = new System.Drawing.Point(397, 93);
            this.screenWidthTextEdit.Name = "screenWidthTextEdit";
            this.screenWidthTextEdit.Size = new System.Drawing.Size(121, 20);
            this.screenWidthTextEdit.TabIndex = 22;
            this.screenWidthTextEdit.TextChanged += new System.EventHandler(this.screenWidthTextEdit_TextChanged);
            this.screenWidthTextEdit.LostFocus += new System.EventHandler(this.screenWidthTextEdit_LostFocus);
            // 
            // screenHeightTextEdit
            // 
            this.screenHeightTextEdit.Location = new System.Drawing.Point(397, 119);
            this.screenHeightTextEdit.Name = "screenHeightTextEdit";
            this.screenHeightTextEdit.Size = new System.Drawing.Size(121, 20);
            this.screenHeightTextEdit.TabIndex = 23;
            this.screenHeightTextEdit.TextChanged += new System.EventHandler(this.screenHeightTextEdit_TextChanged);
            this.screenHeightTextEdit.LostFocus += new System.EventHandler(this.screenHeightTextEdit_LostFocus);
            // 
            // configText
            // 
            this.configText.Location = new System.Drawing.Point(147, 21);
            this.configText.Name = "configText";
            this.configText.ReadOnly = true;
            this.configText.Size = new System.Drawing.Size(121, 20);
            this.configText.TabIndex = 24;
            // 
            // skillListTextBox
            // 
            this.skillListTextBox.Location = new System.Drawing.Point(24, 187);
            this.skillListTextBox.Multiline = true;
            this.skillListTextBox.Name = "skillListTextBox";
            this.skillListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.skillListTextBox.Size = new System.Drawing.Size(494, 236);
            this.skillListTextBox.TabIndex = 5;
            this.skillListTextBox.TextChanged += new System.EventHandler(this.skillListTextBox_TextChanged);
            // 
            // windowStatusText
            // 
            this.windowStatusText.Location = new System.Drawing.Point(397, 22);
            this.windowStatusText.Name = "windowStatusText";
            this.windowStatusText.ReadOnly = true;
            this.windowStatusText.Size = new System.Drawing.Size(121, 20);
            this.windowStatusText.TabIndex = 25;
            // 
            // loadConfigButton
            // 
            this.loadConfigButton.Location = new System.Drawing.Point(24, 21);
            this.loadConfigButton.Name = "loadConfigButton";
            this.loadConfigButton.Size = new System.Drawing.Size(117, 21);
            this.loadConfigButton.TabIndex = 26;
            this.loadConfigButton.Text = "Reload Config";
            this.loadConfigButton.UseVisualStyleBackColor = true;
            this.loadConfigButton.Click += new System.EventHandler(this.loadConfigButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 450);
            this.Controls.Add(this.loadConfigButton);
            this.Controls.Add(this.windowStatusText);
            this.Controls.Add(this.configText);
            this.Controls.Add(this.screenHeightTextEdit);
            this.Controls.Add(this.screenWidthTextEdit);
            this.Controls.Add(this.screenYTextEdit);
            this.Controls.Add(this.screenXTextEdit);
            this.Controls.Add(this.screenHeightText);
            this.Controls.Add(this.screenWidthText);
            this.Controls.Add(this.screenYText);
            this.Controls.Add(this.screenXText);
            this.Controls.Add(this.previewScreenButton);
            this.Controls.Add(this.languageTextBox);
            this.Controls.Add(this.languageBox);
            this.Controls.Add(this.skillListTextBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox languageBox;
        private System.Windows.Forms.TextBox languageTextBox;
        private System.Windows.Forms.Button previewScreenButton;
        private System.Windows.Forms.TextBox screenXText;
        private System.Windows.Forms.TextBox screenYText;
        private System.Windows.Forms.TextBox screenWidthText;
        private System.Windows.Forms.TextBox screenHeightText;
        private System.Windows.Forms.TextBox screenXTextEdit;
        private System.Windows.Forms.TextBox screenYTextEdit;
        private System.Windows.Forms.TextBox screenWidthTextEdit;
        private System.Windows.Forms.TextBox screenHeightTextEdit;
        private System.Windows.Forms.TextBox configText;
        private System.Windows.Forms.TextBox skillListTextBox;
        private System.Windows.Forms.TextBox windowStatusText;
        private System.Windows.Forms.Button loadConfigButton;
    }
}

