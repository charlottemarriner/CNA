﻿namespace Client
{
    partial class ClientForm
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
            this.MessageWindow = new System.Windows.Forms.TextBox();
            this.InputField = new System.Windows.Forms.TextBox();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.ClientList = new System.Windows.Forms.ListBox();
            this.messageSelection = new System.Windows.Forms.ComboBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MessageWindow
            // 
            this.MessageWindow.Location = new System.Drawing.Point(12, 12);
            this.MessageWindow.Multiline = true;
            this.MessageWindow.Name = "MessageWindow";
            this.MessageWindow.Size = new System.Drawing.Size(501, 387);
            this.MessageWindow.TabIndex = 0;
            // 
            // InputField
            // 
            this.InputField.Location = new System.Drawing.Point(12, 418);
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(501, 20);
            this.InputField.TabIndex = 1;
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(568, 408);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(178, 30);
            this.SubmitButton.TabIndex = 2;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // ClientList
            // 
            this.ClientList.FormattingEnabled = true;
            this.ClientList.Location = new System.Drawing.Point(568, 75);
            this.ClientList.Name = "ClientList";
            this.ClientList.Size = new System.Drawing.Size(167, 186);
            this.ClientList.TabIndex = 3;
            // 
            // messageSelection
            // 
            this.messageSelection.FormattingEnabled = true;
            this.messageSelection.Items.AddRange(new object[] {
            "Send Global",
            "Send Private",
            "Set Name",
            "Game Challenge"});
            this.messageSelection.Location = new System.Drawing.Point(568, 267);
            this.messageSelection.Name = "messageSelection";
            this.messageSelection.Size = new System.Drawing.Size(167, 21);
            this.messageSelection.TabIndex = 4;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(568, 26);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(167, 43);
            this.loginButton.TabIndex = 5;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.messageSelection);
            this.Controls.Add(this.ClientList);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.MessageWindow);
            this.Name = "ClientForm";
            this.Text = "Unconnected";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MessageWindow;
        private System.Windows.Forms.TextBox InputField;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.ListBox ClientList;
        private System.Windows.Forms.ComboBox messageSelection;
        private System.Windows.Forms.Button loginButton;
    }
}