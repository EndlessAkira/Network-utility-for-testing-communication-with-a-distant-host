namespace Client
{
    partial class AddServer
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
            this.addButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.otherServerDataTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addButton.Location = new System.Drawing.Point(334, 124);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(124, 37);
            this.addButton.TabIndex = 26;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(38, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 22);
            this.label3.TabIndex = 25;
            this.label3.Text = "Введите имя хоста или IPv4:";
            // 
            // otherServerDataTextBox
            // 
            this.otherServerDataTextBox.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.otherServerDataTextBox.Location = new System.Drawing.Point(38, 81);
            this.otherServerDataTextBox.Name = "otherServerDataTextBox";
            this.otherServerDataTextBox.Size = new System.Drawing.Size(420, 27);
            this.otherServerDataTextBox.TabIndex = 24;
            // 
            // AddServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 226);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.otherServerDataTextBox);
            this.Name = "AddServer";
            this.Text = "Окно добавления нового сервера";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddServer_FormClosing);
            this.Load += new System.EventHandler(this.AddServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button addButton;
        private Label label3;
        private TextBox otherServerDataTextBox;
    }
}