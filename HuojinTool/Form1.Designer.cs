namespace HuojinTool
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
            this.btnRun = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.txtProvider = new System.Windows.Forms.TextBox();
            this.cmboxApp = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmboxDC = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmboxFeedType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMarket = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWebPageID = new System.Windows.Forms.TextBox();
            this.rbArticle = new System.Windows.Forms.RadioButton();
            this.rbArticleAndEntityList = new System.Windows.Forms.RadioButton();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(288, 481);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 21);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(66, 265);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(473, 175);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            // 
            // txtProvider
            // 
            this.txtProvider.Location = new System.Drawing.Point(164, 124);
            this.txtProvider.Name = "txtProvider";
            this.txtProvider.Size = new System.Drawing.Size(199, 21);
            this.txtProvider.TabIndex = 2;
            this.txtProvider.Text = "abc";
            // 
            // cmboxApp
            // 
            this.cmboxApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxApp.FormattingEnabled = true;
            this.cmboxApp.Location = new System.Drawing.Point(164, 53);
            this.cmboxApp.Name = "cmboxApp";
            this.cmboxApp.Size = new System.Drawing.Size(199, 20);
            this.cmboxApp.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "App";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "DataCenter";
            // 
            // cmboxDC
            // 
            this.cmboxDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxDC.FormattingEnabled = true;
            this.cmboxDC.Location = new System.Drawing.Point(164, 20);
            this.cmboxDC.Name = "cmboxDC";
            this.cmboxDC.Size = new System.Drawing.Size(199, 20);
            this.cmboxDC.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "ProviderName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 242);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "FeedUrl";
            // 
            // cmboxFeedType
            // 
            this.cmboxFeedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxFeedType.FormattingEnabled = true;
            this.cmboxFeedType.Location = new System.Drawing.Point(164, 89);
            this.cmboxFeedType.Name = "cmboxFeedType";
            this.cmboxFeedType.Size = new System.Drawing.Size(199, 20);
            this.cmboxFeedType.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "FeedType";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Market";
            // 
            // txtMarket
            // 
            this.txtMarket.Location = new System.Drawing.Point(164, 160);
            this.txtMarket.Name = "txtMarket";
            this.txtMarket.Size = new System.Drawing.Size(199, 21);
            this.txtMarket.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(65, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "WebPageID";
            // 
            // txtWebPageID
            // 
            this.txtWebPageID.Location = new System.Drawing.Point(164, 197);
            this.txtWebPageID.Name = "txtWebPageID";
            this.txtWebPageID.Size = new System.Drawing.Size(199, 21);
            this.txtWebPageID.TabIndex = 12;
            // 
            // rbArticle
            // 
            this.rbArticle.AutoSize = true;
            this.rbArticle.Checked = true;
            this.rbArticle.Location = new System.Drawing.Point(28, 31);
            this.rbArticle.Name = "rbArticle";
            this.rbArticle.Size = new System.Drawing.Size(65, 16);
            this.rbArticle.TabIndex = 13;
            this.rbArticle.TabStop = true;
            this.rbArticle.Text = "Article";
            this.rbArticle.UseVisualStyleBackColor = true;
            // 
            // rbArticleAndEntityList
            // 
            this.rbArticleAndEntityList.AutoSize = true;
            this.rbArticleAndEntityList.Location = new System.Drawing.Point(28, 54);
            this.rbArticleAndEntityList.Name = "rbArticleAndEntityList";
            this.rbArticleAndEntityList.Size = new System.Drawing.Size(125, 16);
            this.rbArticleAndEntityList.TabIndex = 14;
            this.rbArticleAndEntityList.Text = "ArticleEntityList";
            this.rbArticleAndEntityList.UseVisualStyleBackColor = true;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.rbArticleAndEntityList);
            this.groupBox.Controls.Add(this.rbArticle);
            this.groupBox.Location = new System.Drawing.Point(407, 20);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(200, 100);
            this.groupBox.TabIndex = 15;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "ChooseType";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 560);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.txtWebPageID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMarket);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmboxDC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmboxFeedType);
            this.Controls.Add(this.cmboxApp);
            this.Controls.Add(this.txtProvider);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.btnRun);
            this.Name = "Form1";
            this.Text = "HuoJinTool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.TextBox txtProvider;
        private System.Windows.Forms.ComboBox cmboxApp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmboxDC;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmboxFeedType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMarket;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtWebPageID;
        private System.Windows.Forms.RadioButton rbArticle;
        private System.Windows.Forms.RadioButton rbArticleAndEntityList;
        private System.Windows.Forms.GroupBox groupBox;
    }
}

