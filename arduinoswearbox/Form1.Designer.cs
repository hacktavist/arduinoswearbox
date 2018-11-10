using System;

namespace arduinoswearbox
{
    partial class Form1
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
            this.arduinoComPortCombo = new System.Windows.Forms.ComboBox();
            this.arduinoComPortLabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.serialPortConnection = new System.Windows.Forms.Button();
            this.speechRecognitionOutputTextbox = new System.Windows.Forms.TextBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // arduinoComPortCombo
            // 
            this.arduinoComPortCombo.BackColor = System.Drawing.Color.Black;
            this.arduinoComPortCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.arduinoComPortCombo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.arduinoComPortCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arduinoComPortCombo.ForeColor = System.Drawing.Color.Lime;
            this.arduinoComPortCombo.FormattingEnabled = true;
            this.arduinoComPortCombo.Location = new System.Drawing.Point(168, 73);
            this.arduinoComPortCombo.Name = "arduinoComPortCombo";
            this.arduinoComPortCombo.Size = new System.Drawing.Size(121, 28);
            this.arduinoComPortCombo.TabIndex = 0;
            // 
            // arduinoComPortLabel
            // 
            this.arduinoComPortLabel.AutoSize = true;
            this.arduinoComPortLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arduinoComPortLabel.Location = new System.Drawing.Point(12, 76);
            this.arduinoComPortLabel.Name = "arduinoComPortLabel";
            this.arduinoComPortLabel.Size = new System.Drawing.Size(150, 20);
            this.arduinoComPortLabel.TabIndex = 1;
            this.arduinoComPortLabel.Text = "Arduino Com Port";
            // 
            // exitButton
            // 
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.ForeColor = System.Drawing.Color.Red;
            this.exitButton.Location = new System.Drawing.Point(883, 484);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // serialPortConnection
            // 
            this.serialPortConnection.BackColor = System.Drawing.Color.Black;
            this.serialPortConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.serialPortConnection.Location = new System.Drawing.Point(311, 76);
            this.serialPortConnection.Name = "serialPortConnection";
            this.serialPortConnection.Size = new System.Drawing.Size(75, 23);
            this.serialPortConnection.TabIndex = 3;
            this.serialPortConnection.Text = "Connect";
            this.serialPortConnection.UseVisualStyleBackColor = false;
            this.serialPortConnection.Click += new System.EventHandler(this.serialPortConnection_Click);
            // 
            // speechRecognitionOutputTextbox
            // 
            this.speechRecognitionOutputTextbox.AcceptsReturn = true;
            this.speechRecognitionOutputTextbox.BackColor = System.Drawing.Color.Black;
            this.speechRecognitionOutputTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speechRecognitionOutputTextbox.ForeColor = System.Drawing.Color.Lime;
            this.speechRecognitionOutputTextbox.Location = new System.Drawing.Point(452, 73);
            this.speechRecognitionOutputTextbox.Multiline = true;
            this.speechRecognitionOutputTextbox.Name = "speechRecognitionOutputTextbox";
            this.speechRecognitionOutputTextbox.ReadOnly = true;
            this.speechRecognitionOutputTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.speechRecognitionOutputTextbox.Size = new System.Drawing.Size(462, 178);
            this.speechRecognitionOutputTextbox.TabIndex = 4;
            // 
            // refreshButton
            // 
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Location = new System.Drawing.Point(189, 44);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(974, 527);
            this.ControlBox = false;
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.speechRecognitionOutputTextbox);
            this.Controls.Add(this.serialPortConnection);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.arduinoComPortLabel);
            this.Controls.Add(this.arduinoComPortCombo);
            this.ForeColor = System.Drawing.Color.Lime;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Arduino Swear Box";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox arduinoComPortCombo;
        private System.Windows.Forms.Label arduinoComPortLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button serialPortConnection;
        private System.Windows.Forms.TextBox speechRecognitionOutputTextbox;
        private System.Windows.Forms.Button refreshButton;
    }
}

