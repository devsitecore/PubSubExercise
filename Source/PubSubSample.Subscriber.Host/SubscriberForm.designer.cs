namespace PubSubSample.Subscriber
{
    partial class Subscriber
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
            this.btnSubscribe = new System.Windows.Forms.Button();
            this.btnUnubscribe = new System.Windows.Forms.Button();
            this.btnClearAstaListView = new System.Windows.Forms.Button();
            this.txtEventsCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstEvents = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.txtTopicName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btnSubscribe
            // 
            this.btnSubscribe.Location = new System.Drawing.Point(601, 18);
            this.btnSubscribe.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubscribe.Name = "btnSubscribe";
            this.btnSubscribe.Size = new System.Drawing.Size(100, 28);
            this.btnSubscribe.TabIndex = 3;
            this.btnSubscribe.Text = "&Subscribe";
            this.btnSubscribe.UseVisualStyleBackColor = true;
            this.btnSubscribe.Click += new System.EventHandler(this.OnSubscribe);
            // 
            // btnUnubscribe
            // 
            this.btnUnubscribe.Enabled = false;
            this.btnUnubscribe.Location = new System.Drawing.Point(709, 18);
            this.btnUnubscribe.Margin = new System.Windows.Forms.Padding(4);
            this.btnUnubscribe.Name = "btnUnubscribe";
            this.btnUnubscribe.Size = new System.Drawing.Size(112, 28);
            this.btnUnubscribe.TabIndex = 4;
            this.btnUnubscribe.Text = "&Un-subscribe";
            this.btnUnubscribe.UseVisualStyleBackColor = true;
            this.btnUnubscribe.Click += new System.EventHandler(this.OnUnSubscribe);
            // 
            // btnClearAstaListView
            // 
            this.btnClearAstaListView.Location = new System.Drawing.Point(13, 322);
            this.btnClearAstaListView.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearAstaListView.Name = "btnClearAstaListView";
            this.btnClearAstaListView.Size = new System.Drawing.Size(808, 28);
            this.btnClearAstaListView.TabIndex = 12;
            this.btnClearAstaListView.Text = "&Clear Messages";
            this.btnClearAstaListView.UseVisualStyleBackColor = true;
            this.btnClearAstaListView.Click += new System.EventHandler(this.ClearAstaListView_Click);
            // 
            // txtEventsCount
            // 
            this.txtEventsCount.Location = new System.Drawing.Point(769, 355);
            this.txtEventsCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtEventsCount.Name = "txtEventsCount";
            this.txtEventsCount.ReadOnly = true;
            this.txtEventsCount.Size = new System.Drawing.Size(52, 22);
            this.txtEventsCount.TabIndex = 14;
            this.txtEventsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(557, 358);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 17);
            this.label2.TabIndex = 13;
            this.label2.Text = "Total Messages Received :";
            // 
            // lstEvents
            // 
            this.lstEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader1});
            this.lstEvents.GridLines = true;
            this.lstEvents.Location = new System.Drawing.Point(16, 103);
            this.lstEvents.Margin = new System.Windows.Forms.Padding(4);
            this.lstEvents.MultiSelect = false;
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.Size = new System.Drawing.Size(805, 211);
            this.lstEvents.TabIndex = 16;
            this.lstEvents.UseCompatibleStateImageBehavior = false;
            this.lstEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "#";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Topic";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Message";
            this.columnHeader6.Width = 470;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(295, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(247, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Messages Received  from Server";
            // 
            // txtTopicName
            // 
            this.txtTopicName.Location = new System.Drawing.Point(111, 21);
            this.txtTopicName.Margin = new System.Windows.Forms.Padding(4);
            this.txtTopicName.Name = "txtTopicName";
            this.txtTopicName.Size = new System.Drawing.Size(482, 22);
            this.txtTopicName.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 28;
            this.label3.Text = "&Topic Name";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            // 
            // Subscriber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 398);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTopicName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstEvents);
            this.Controls.Add(this.btnUnubscribe);
            this.Controls.Add(this.btnSubscribe);
            this.Controls.Add(this.txtEventsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClearAstaListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(10, 345);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Subscriber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Subscriber Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubscribe;
        private System.Windows.Forms.Button btnUnubscribe;
        private System.Windows.Forms.Button btnClearAstaListView;
        private System.Windows.Forms.TextBox txtEventsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lstEvents;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTopicName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}