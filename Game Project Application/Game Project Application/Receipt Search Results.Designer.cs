﻿namespace Game_Project_Application
{
    partial class ReceiptSearchResults
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
            this.uxResults = new System.Windows.Forms.DataGridView();
            this.ReceiptId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.uxResults)).BeginInit();
            this.SuspendLayout();
            // 
            // uxResults
            // 
            this.uxResults.AllowUserToAddRows = false;
            this.uxResults.AllowUserToDeleteRows = false;
            this.uxResults.AllowUserToOrderColumns = true;
            this.uxResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.uxResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReceiptId,
            this.FirstName,
            this.LastName,
            this.Email,
            this.GameCount,
            this.Total});
            this.uxResults.Location = new System.Drawing.Point(12, 12);
            this.uxResults.Name = "uxResults";
            this.uxResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.uxResults.Size = new System.Drawing.Size(559, 432);
            this.uxResults.TabIndex = 0;
            // 
            // ReceiptId
            // 
            this.ReceiptId.HeaderText = "Receipt #";
            this.ReceiptId.Name = "ReceiptId";
            this.ReceiptId.Width = 50;
            // 
            // FirstName
            // 
            this.FirstName.HeaderText = "First Name";
            this.FirstName.Name = "FirstName";
            // 
            // LastName
            // 
            this.LastName.HeaderText = "Last Name";
            this.LastName.Name = "LastName";
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            // 
            // GameCount
            // 
            this.GameCount.HeaderText = "Games Purchased";
            this.GameCount.Name = "GameCount";
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            // 
            // ReceiptSearchResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 456);
            this.Controls.Add(this.uxResults);
            this.Name = "ReceiptSearchResults";
            this.Text = "Receipt Search Results";
            ((System.ComponentModel.ISupportInitialize)(this.uxResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView uxResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceiptId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
    }
}