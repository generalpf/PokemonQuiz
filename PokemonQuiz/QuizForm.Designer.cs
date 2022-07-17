namespace PokemonQuiz
{
    partial class QuizForm
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
            this.cmdStartQuiz = new System.Windows.Forms.Button();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.cmdP1 = new System.Windows.Forms.Button();
            this.cmdP2 = new System.Windows.Forms.Button();
            this.cmdP3 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.lblLastAnswerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScore = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdStartQuiz
            // 
            this.cmdStartQuiz.Location = new System.Drawing.Point(12, 12);
            this.cmdStartQuiz.Name = "cmdStartQuiz";
            this.cmdStartQuiz.Size = new System.Drawing.Size(75, 23);
            this.cmdStartQuiz.TabIndex = 0;
            this.cmdStartQuiz.Text = "Start &Quiz";
            this.cmdStartQuiz.UseVisualStyleBackColor = true;
            this.cmdStartQuiz.Click += new System.EventHandler(this.cmdStartQuiz_Click);
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new System.Drawing.Point(9, 49);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(0, 13);
            this.lblQuestion.TabIndex = 1;
            // 
            // cmdP1
            // 
            this.cmdP1.Enabled = false;
            this.cmdP1.Location = new System.Drawing.Point(7, 220);
            this.cmdP1.Name = "cmdP1";
            this.cmdP1.Size = new System.Drawing.Size(75, 23);
            this.cmdP1.TabIndex = 5;
            this.cmdP1.Text = "&1";
            this.cmdP1.UseVisualStyleBackColor = true;
            this.cmdP1.Click += new System.EventHandler(this.cmdP1_Click);
            // 
            // cmdP2
            // 
            this.cmdP2.Enabled = false;
            this.cmdP2.Location = new System.Drawing.Point(7, 220);
            this.cmdP2.Name = "cmdP2";
            this.cmdP2.Size = new System.Drawing.Size(75, 23);
            this.cmdP2.TabIndex = 6;
            this.cmdP2.Text = "&2";
            this.cmdP2.UseVisualStyleBackColor = true;
            this.cmdP2.Click += new System.EventHandler(this.cmdP2_Click);
            // 
            // cmdP3
            // 
            this.cmdP3.Enabled = false;
            this.cmdP3.Location = new System.Drawing.Point(7, 220);
            this.cmdP3.Name = "cmdP3";
            this.cmdP3.Size = new System.Drawing.Size(75, 23);
            this.cmdP3.TabIndex = 7;
            this.cmdP3.Text = "&3";
            this.cmdP3.UseVisualStyleBackColor = true;
            this.cmdP3.Click += new System.EventHandler(this.cmdP3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.lblLastAnswerStatus,
            this.lblScore});
            this.statusStrip1.Location = new System.Drawing.Point(0, 342);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(637, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            // 
            // lblLastAnswerStatus
            // 
            this.lblLastAnswerStatus.Name = "lblLastAnswerStatus";
            this.lblLastAnswerStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // lblScore
            // 
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.cmdP1);
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 249);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(7, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 194);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox2);
            this.groupBox2.Controls.Add(this.cmdP2);
            this.groupBox2.Location = new System.Drawing.Point(221, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 249);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(7, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(187, 194);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.Controls.Add(this.cmdP3);
            this.groupBox3.Location = new System.Drawing.Point(427, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 249);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(7, 20);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(187, 194);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // QuizForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 364);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.cmdStartQuiz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuizForm";
            this.Text = "Pokémon Quiz";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdStartQuiz;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button cmdP1;
        private System.Windows.Forms.Button cmdP2;
        private System.Windows.Forms.Button cmdP3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel lblLastAnswerStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblScore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

