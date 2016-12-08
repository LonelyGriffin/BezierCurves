namespace BezierCurves
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
            this.canvasPanel = new System.Windows.Forms.Panel();
            this.controllPanel = new System.Windows.Forms.Panel();
            this.removePointRadioButton = new System.Windows.Forms.RadioButton();
            this.movePointRadioButton = new System.Windows.Forms.RadioButton();
            this.addPointRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.clear_button = new System.Windows.Forms.Button();
            this.controllPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // canvasPanel
            // 
            this.canvasPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.canvasPanel.AutoSize = true;
            this.canvasPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.canvasPanel.Location = new System.Drawing.Point(12, 12);
            this.canvasPanel.Name = "canvasPanel";
            this.canvasPanel.Size = new System.Drawing.Size(652, 498);
            this.canvasPanel.TabIndex = 0;
            this.canvasPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseDown);
            this.canvasPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseMove);
            this.canvasPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvasPanel_MouseUp);
            // 
            // controllPanel
            // 
            this.controllPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.controllPanel.AutoSize = true;
            this.controllPanel.BackColor = System.Drawing.SystemColors.Control;
            this.controllPanel.Controls.Add(this.clear_button);
            this.controllPanel.Controls.Add(this.removePointRadioButton);
            this.controllPanel.Controls.Add(this.movePointRadioButton);
            this.controllPanel.Controls.Add(this.addPointRadioButton);
            this.controllPanel.Controls.Add(this.label1);
            this.controllPanel.Location = new System.Drawing.Point(670, 12);
            this.controllPanel.Name = "controllPanel";
            this.controllPanel.Size = new System.Drawing.Size(129, 329);
            this.controllPanel.TabIndex = 1;
            // 
            // removePointRadioButton
            // 
            this.removePointRadioButton.AutoSize = true;
            this.removePointRadioButton.Location = new System.Drawing.Point(3, 86);
            this.removePointRadioButton.Name = "removePointRadioButton";
            this.removePointRadioButton.Size = new System.Drawing.Size(98, 17);
            this.removePointRadioButton.TabIndex = 3;
            this.removePointRadioButton.TabStop = true;
            this.removePointRadioButton.Text = "Удалить точку";
            this.removePointRadioButton.UseVisualStyleBackColor = true;
            this.removePointRadioButton.CheckedChanged += new System.EventHandler(this.removePointRadioButton_CheckedChanged);
            // 
            // movePointRadioButton
            // 
            this.movePointRadioButton.AutoSize = true;
            this.movePointRadioButton.Location = new System.Drawing.Point(3, 63);
            this.movePointRadioButton.Name = "movePointRadioButton";
            this.movePointRadioButton.Size = new System.Drawing.Size(123, 17);
            this.movePointRadioButton.TabIndex = 2;
            this.movePointRadioButton.TabStop = true;
            this.movePointRadioButton.Text = "Переместить точку";
            this.movePointRadioButton.UseVisualStyleBackColor = true;
            this.movePointRadioButton.CheckedChanged += new System.EventHandler(this.movePointRadioButton_CheckedChanged);
            // 
            // addPointRadioButton
            // 
            this.addPointRadioButton.AutoSize = true;
            this.addPointRadioButton.Checked = true;
            this.addPointRadioButton.Location = new System.Drawing.Point(3, 40);
            this.addPointRadioButton.Name = "addPointRadioButton";
            this.addPointRadioButton.Size = new System.Drawing.Size(105, 17);
            this.addPointRadioButton.TabIndex = 1;
            this.addPointRadioButton.TabStop = true;
            this.addPointRadioButton.Text = "Добавить точку";
            this.addPointRadioButton.UseVisualStyleBackColor = true;
            this.addPointRadioButton.CheckedChanged += new System.EventHandler(this.addPointRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Режим работы";
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(20, 136);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(75, 23);
            this.clear_button.TabIndex = 4;
            this.clear_button.Text = "Очистить";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 522);
            this.Controls.Add(this.controllPanel);
            this.Controls.Add(this.canvasPanel);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.controllPanel.ResumeLayout(false);
            this.controllPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel canvasPanel;
        private System.Windows.Forms.Panel controllPanel;
        private System.Windows.Forms.RadioButton removePointRadioButton;
        private System.Windows.Forms.RadioButton movePointRadioButton;
        private System.Windows.Forms.RadioButton addPointRadioButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clear_button;
    }
}

