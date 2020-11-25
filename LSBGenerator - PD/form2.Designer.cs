namespace LSBGenerator___PD
{
    partial class Form2
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
            this.close = new System.Windows.Forms.Button();
            this.lupa_tekst = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.close.BackColor = System.Drawing.Color.White;
            this.close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.close.Location = new System.Drawing.Point(426, 771);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(146, 55);
            this.close.TabIndex = 15;
            this.close.Text = "Powrót";
            this.close.UseVisualStyleBackColor = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // lupa_tekst
            // 
            this.lupa_tekst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lupa_tekst.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lupa_tekst.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lupa_tekst.Location = new System.Drawing.Point(13, 13);
            this.lupa_tekst.Name = "lupa_tekst";
            this.lupa_tekst.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.lupa_tekst.Size = new System.Drawing.Size(976, 751);
            this.lupa_tekst.TabIndex = 16;
            this.lupa_tekst.Text = "";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1002, 838);
            this.Controls.Add(this.lupa_tekst);
            this.Controls.Add(this.close);
            this.Name = "Form2";
            this.Text = "Lupa";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.RichTextBox lupa_tekst;

    }
}