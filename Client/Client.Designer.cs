namespace Client
{
    partial class Client
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.serversLabel = new System.Windows.Forms.Label();
            this.serversListBox = new System.Windows.Forms.ListBox();
            this.messageBoxLabel = new System.Windows.Forms.Label();
            this.updateListServersButton = new System.Windows.Forms.Button();
            this.contactServerButton = new System.Windows.Forms.Button();
            this.contactByNameRadioButton = new System.Windows.Forms.RadioButton();
            this.contactByIPRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numberOfEchoRequestsTrackBar = new System.Windows.Forms.TrackBar();
            this.minCountLabel = new System.Windows.Forms.Label();
            this.maxCountLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tracertCheckBox = new System.Windows.Forms.CheckBox();
            this.numberOfEchoRequestsTextBox = new System.Windows.Forms.TextBox();
            this.infoButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.clearMessageTextBoxButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.findOtherButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfEchoRequestsTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // serversLabel
            // 
            this.serversLabel.AutoSize = true;
            this.serversLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.serversLabel.Location = new System.Drawing.Point(28, 37);
            this.serversLabel.Name = "serversLabel";
            this.serversLabel.Size = new System.Drawing.Size(256, 22);
            this.serversLabel.TabIndex = 0;
            this.serversLabel.Text = "Список доступных серверов:";
            // 
            // serversListBox
            // 
            this.serversListBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.serversListBox.FormattingEnabled = true;
            this.serversListBox.HorizontalScrollbar = true;
            this.serversListBox.ItemHeight = 20;
            this.serversListBox.Location = new System.Drawing.Point(28, 78);
            this.serversListBox.Name = "serversListBox";
            this.serversListBox.Size = new System.Drawing.Size(420, 204);
            this.serversListBox.TabIndex = 1;
            this.serversListBox.SelectedIndexChanged += new System.EventHandler(this.serversListBox_SelectedIndexChanged);
            // 
            // messageBoxLabel
            // 
            this.messageBoxLabel.AutoSize = true;
            this.messageBoxLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.messageBoxLabel.Location = new System.Drawing.Point(489, 37);
            this.messageBoxLabel.Name = "messageBoxLabel";
            this.messageBoxLabel.Size = new System.Drawing.Size(161, 22);
            this.messageBoxLabel.TabIndex = 3;
            this.messageBoxLabel.Text = "Окно сообщений:";
            // 
            // updateListServersButton
            // 
            this.updateListServersButton.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updateListServersButton.Location = new System.Drawing.Point(28, 338);
            this.updateListServersButton.Name = "updateListServersButton";
            this.updateListServersButton.Size = new System.Drawing.Size(155, 41);
            this.updateListServersButton.TabIndex = 4;
            this.updateListServersButton.Text = "Update ♻";
            this.updateListServersButton.UseVisualStyleBackColor = true;
            this.updateListServersButton.Click += new System.EventHandler(this.updateListServersButton_Click);
            // 
            // contactServerButton
            // 
            this.contactServerButton.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.contactServerButton.Location = new System.Drawing.Point(217, 338);
            this.contactServerButton.Name = "contactServerButton";
            this.contactServerButton.Size = new System.Drawing.Size(155, 41);
            this.contactServerButton.TabIndex = 5;
            this.contactServerButton.Text = "Ping 💬";
            this.contactServerButton.UseVisualStyleBackColor = true;
            this.contactServerButton.Click += new System.EventHandler(this.contactServerButton_Click);
            // 
            // contactByNameRadioButton
            // 
            this.contactByNameRadioButton.AutoSize = true;
            this.contactByNameRadioButton.Checked = true;
            this.contactByNameRadioButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.contactByNameRadioButton.Location = new System.Drawing.Point(532, 338);
            this.contactByNameRadioButton.Name = "contactByNameRadioButton";
            this.contactByNameRadioButton.Size = new System.Drawing.Size(231, 26);
            this.contactByNameRadioButton.TabIndex = 6;
            this.contactByNameRadioButton.TabStop = true;
            this.contactByNameRadioButton.Text = "связь по имени сервера";
            this.contactByNameRadioButton.UseVisualStyleBackColor = true;
            // 
            // contactByIPRadioButton
            // 
            this.contactByIPRadioButton.AutoSize = true;
            this.contactByIPRadioButton.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.contactByIPRadioButton.Location = new System.Drawing.Point(532, 371);
            this.contactByIPRadioButton.Name = "contactByIPRadioButton";
            this.contactByIPRadioButton.Size = new System.Drawing.Size(213, 26);
            this.contactByIPRadioButton.TabIndex = 7;
            this.contactByIPRadioButton.Text = "связь по IPv4 сервера";
            this.contactByIPRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(475, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 22);
            this.label1.TabIndex = 8;
            this.label1.Text = "Настройки связи с сервером:";
            // 
            // numberOfEchoRequestsTrackBar
            // 
            this.numberOfEchoRequestsTrackBar.Location = new System.Drawing.Point(532, 455);
            this.numberOfEchoRequestsTrackBar.Maximum = 200;
            this.numberOfEchoRequestsTrackBar.Minimum = 2;
            this.numberOfEchoRequestsTrackBar.Name = "numberOfEchoRequestsTrackBar";
            this.numberOfEchoRequestsTrackBar.Size = new System.Drawing.Size(433, 56);
            this.numberOfEchoRequestsTrackBar.TabIndex = 9;
            this.numberOfEchoRequestsTrackBar.Value = 2;
            this.numberOfEchoRequestsTrackBar.Scroll += new System.EventHandler(this.numberOfEchoRequestsTrackBar_Scroll);
            // 
            // minCountLabel
            // 
            this.minCountLabel.AutoSize = true;
            this.minCountLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minCountLabel.Location = new System.Drawing.Point(506, 455);
            this.minCountLabel.Name = "minCountLabel";
            this.minCountLabel.Size = new System.Drawing.Size(20, 22);
            this.minCountLabel.TabIndex = 10;
            this.minCountLabel.Text = "2";
            // 
            // maxCountLabel
            // 
            this.maxCountLabel.AutoSize = true;
            this.maxCountLabel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maxCountLabel.Location = new System.Drawing.Point(971, 455);
            this.maxCountLabel.Name = "maxCountLabel";
            this.maxCountLabel.Size = new System.Drawing.Size(40, 22);
            this.maxCountLabel.TabIndex = 11;
            this.maxCountLabel.Text = "200";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(585, 420);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 22);
            this.label4.TabIndex = 12;
            this.label4.Text = "Количество эхо-запросов:";
            // 
            // tracertCheckBox
            // 
            this.tracertCheckBox.AutoSize = true;
            this.tracertCheckBox.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tracertCheckBox.Location = new System.Drawing.Point(532, 532);
            this.tracertCheckBox.Name = "tracertCheckBox";
            this.tracertCheckBox.Size = new System.Drawing.Size(213, 26);
            this.tracertCheckBox.TabIndex = 13;
            this.tracertCheckBox.Text = "Определить маршрут";
            this.tracertCheckBox.UseVisualStyleBackColor = true;
            // 
            // numberOfEchoRequestsTextBox
            // 
            this.numberOfEchoRequestsTextBox.Location = new System.Drawing.Point(820, 418);
            this.numberOfEchoRequestsTextBox.Name = "numberOfEchoRequestsTextBox";
            this.numberOfEchoRequestsTextBox.Size = new System.Drawing.Size(81, 27);
            this.numberOfEchoRequestsTextBox.TabIndex = 14;
            this.numberOfEchoRequestsTextBox.Text = "2";
            this.numberOfEchoRequestsTextBox.TextChanged += new System.EventHandler(this.numberOfEchoRequestsTextBox_TextChanged);
            // 
            // infoButton
            // 
            this.infoButton.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.infoButton.Location = new System.Drawing.Point(28, 401);
            this.infoButton.Name = "infoButton";
            this.infoButton.Size = new System.Drawing.Size(155, 41);
            this.infoButton.TabIndex = 15;
            this.infoButton.Text = "Info ❗";
            this.infoButton.UseVisualStyleBackColor = true;
            this.infoButton.Click += new System.EventHandler(this.infoButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.closeButton.Location = new System.Drawing.Point(28, 470);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(155, 41);
            this.closeButton.TabIndex = 16;
            this.closeButton.Text = "Close ❎";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(28, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 22);
            this.label2.TabIndex = 17;
            this.label2.Text = "Панель управления:";
            // 
            // clearMessageTextBoxButton
            // 
            this.clearMessageTextBoxButton.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearMessageTextBoxButton.Location = new System.Drawing.Point(826, 28);
            this.clearMessageTextBoxButton.Name = "clearMessageTextBoxButton";
            this.clearMessageTextBoxButton.Size = new System.Drawing.Size(186, 41);
            this.clearMessageTextBoxButton.TabIndex = 18;
            this.clearMessageTextBoxButton.Text = "Очистить";
            this.clearMessageTextBoxButton.UseVisualStyleBackColor = true;
            this.clearMessageTextBoxButton.Click += new System.EventHandler(this.clearMessageTextBoxButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(485, 78);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(527, 204);
            this.messageTextBox.TabIndex = 19;
            this.messageTextBox.Text = resources.GetString("messageTextBox.Text");
            // 
            // findOtherButton
            // 
            this.findOtherButton.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.findOtherButton.Location = new System.Drawing.Point(217, 401);
            this.findOtherButton.Name = "findOtherButton";
            this.findOtherButton.Size = new System.Drawing.Size(155, 44);
            this.findOtherButton.TabIndex = 20;
            this.findOtherButton.Text = "Find other..🔎";
            this.findOtherButton.UseVisualStyleBackColor = true;
            this.findOtherButton.Click += new System.EventHandler(this.findOtherButton_Click);
            // 
            // Client
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1041, 578);
            this.Controls.Add(this.findOtherButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.clearMessageTextBoxButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.infoButton);
            this.Controls.Add(this.numberOfEchoRequestsTextBox);
            this.Controls.Add(this.tracertCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maxCountLabel);
            this.Controls.Add(this.minCountLabel);
            this.Controls.Add(this.numberOfEchoRequestsTrackBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contactByIPRadioButton);
            this.Controls.Add(this.contactByNameRadioButton);
            this.Controls.Add(this.contactServerButton);
            this.Controls.Add(this.updateListServersButton);
            this.Controls.Add(this.messageBoxLabel);
            this.Controls.Add(this.serversListBox);
            this.Controls.Add(this.serversLabel);
            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Клиентская часть к.р.\"Связь с удалённым хостом\"";
            this.Load += new System.EventHandler(this.Client_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numberOfEchoRequestsTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label serversLabel;
        private ListBox serversListBox;
        private Label messageBoxLabel;
        private Button updateListServersButton;
        private Button contactServerButton;
        private RadioButton contactByNameRadioButton;
        private RadioButton contactByIPRadioButton;
        private Label label1;
        private TrackBar numberOfEchoRequestsTrackBar;
        private Label minCountLabel;
        private Label maxCountLabel;
        private Label label4;
        private CheckBox tracertCheckBox;
        private TextBox numberOfEchoRequestsTextBox;
        private Button infoButton;
        private Button closeButton;
        private Label label2;
        private Button clearMessageTextBoxButton;
        private RichTextBox messageTextBox;
        private Button findOtherButton;
    }
}