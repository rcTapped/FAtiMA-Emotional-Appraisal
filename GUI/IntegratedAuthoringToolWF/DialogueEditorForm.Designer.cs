﻿namespace IntegratedAuthoringToolWF
{
    partial class DialogueEditorForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonPlayerEditDialogueAction = new System.Windows.Forms.Button();
            this.dataGridViewPlayerDialogueActions = new System.Windows.Forms.DataGridView();
            this.buttonAddPlayerDialogueAction = new System.Windows.Forms.Button();
            this.buttonPlayerRemoveDialogueAction = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAgentEditDialogAction = new System.Windows.Forms.Button();
            this.dataGridViewAgentDialogueActions = new System.Windows.Forms.DataGridView();
            this.buttonAgentAddDialogAction = new System.Windows.Forms.Button();
            this.buttonAgentRemoveDialogAction = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayerDialogueActions)).BeginInit();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAgentDialogueActions)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonPlayerEditDialogueAction);
            this.groupBox1.Controls.Add(this.dataGridViewPlayerDialogueActions);
            this.groupBox1.Controls.Add(this.buttonAddPlayerDialogueAction);
            this.groupBox1.Controls.Add(this.buttonPlayerRemoveDialogueAction);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 294);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player Dialogue Actions";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // buttonPlayerEditDialogueAction
            // 
            this.buttonPlayerEditDialogueAction.Location = new System.Drawing.Point(66, 19);
            this.buttonPlayerEditDialogueAction.Name = "buttonPlayerEditDialogueAction";
            this.buttonPlayerEditDialogueAction.Size = new System.Drawing.Size(54, 23);
            this.buttonPlayerEditDialogueAction.TabIndex = 14;
            this.buttonPlayerEditDialogueAction.Text = "Edit";
            this.buttonPlayerEditDialogueAction.UseVisualStyleBackColor = true;
            this.buttonPlayerEditDialogueAction.Click += new System.EventHandler(this.buttonPlayerEditDialogueAction_Click);
            // 
            // dataGridViewPlayerDialogueActions
            // 
            this.dataGridViewPlayerDialogueActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewPlayerDialogueActions.AllowUserToAddRows = false;
            this.dataGridViewPlayerDialogueActions.AllowUserToDeleteRows = false;
            this.dataGridViewPlayerDialogueActions.AllowUserToOrderColumns = true;
            this.dataGridViewPlayerDialogueActions.AllowUserToResizeRows = false;
            this.dataGridViewPlayerDialogueActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPlayerDialogueActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPlayerDialogueActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewPlayerDialogueActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlayerDialogueActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewPlayerDialogueActions.Location = new System.Drawing.Point(6, 51);
            this.dataGridViewPlayerDialogueActions.Name = "dataGridViewPlayerDialogueActions";
            this.dataGridViewPlayerDialogueActions.ReadOnly = true;
            this.dataGridViewPlayerDialogueActions.RowHeadersVisible = false;
            this.dataGridViewPlayerDialogueActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPlayerDialogueActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPlayerDialogueActions.Size = new System.Drawing.Size(478, 237);
            this.dataGridViewPlayerDialogueActions.TabIndex = 13;
            // 
            // buttonAddPlayerDialogueAction
            // 
            this.buttonAddPlayerDialogueAction.Location = new System.Drawing.Point(6, 19);
            this.buttonAddPlayerDialogueAction.Name = "buttonAddPlayerDialogueAction";
            this.buttonAddPlayerDialogueAction.Size = new System.Drawing.Size(54, 23);
            this.buttonAddPlayerDialogueAction.TabIndex = 10;
            this.buttonAddPlayerDialogueAction.Text = "Add";
            this.buttonAddPlayerDialogueAction.UseVisualStyleBackColor = true;
            this.buttonAddPlayerDialogueAction.Click += new System.EventHandler(this.buttonAddPlayerDialogueAction_Click);
            // 
            // buttonPlayerRemoveDialogueAction
            // 
            this.buttonPlayerRemoveDialogueAction.Location = new System.Drawing.Point(126, 19);
            this.buttonPlayerRemoveDialogueAction.Name = "buttonPlayerRemoveDialogueAction";
            this.buttonPlayerRemoveDialogueAction.Size = new System.Drawing.Size(70, 23);
            this.buttonPlayerRemoveDialogueAction.TabIndex = 11;
            this.buttonPlayerRemoveDialogueAction.Text = "Remove";
            this.buttonPlayerRemoveDialogueAction.UseVisualStyleBackColor = true;
            this.buttonPlayerRemoveDialogueAction.Click += new System.EventHandler(this.buttonPlayerRemoveDialogueAction_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(514, 24);
            this.mainMenu.TabIndex = 16;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(490, 589);
            this.splitContainer1.SplitterDistance = 294;
            this.splitContainer1.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAgentEditDialogAction);
            this.groupBox2.Controls.Add(this.dataGridViewAgentDialogueActions);
            this.groupBox2.Controls.Add(this.buttonAgentAddDialogAction);
            this.groupBox2.Controls.Add(this.buttonAgentRemoveDialogAction);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(490, 291);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agent Dialogue Actions";
            // 
            // buttonAgentEditDialogAction
            // 
            this.buttonAgentEditDialogAction.Location = new System.Drawing.Point(66, 19);
            this.buttonAgentEditDialogAction.Name = "buttonAgentEditDialogAction";
            this.buttonAgentEditDialogAction.Size = new System.Drawing.Size(54, 23);
            this.buttonAgentEditDialogAction.TabIndex = 14;
            this.buttonAgentEditDialogAction.Text = "Edit";
            this.buttonAgentEditDialogAction.UseVisualStyleBackColor = true;
            this.buttonAgentEditDialogAction.Click += new System.EventHandler(this.buttonAgentEditDialogAction_Click);
            // 
            // dataGridViewAgentDialogueActions
            // 
            this.dataGridViewAgentDialogueActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAgentDialogueActions.AllowUserToAddRows = false;
            this.dataGridViewAgentDialogueActions.AllowUserToDeleteRows = false;
            this.dataGridViewAgentDialogueActions.AllowUserToOrderColumns = true;
            this.dataGridViewAgentDialogueActions.AllowUserToResizeRows = false;
            this.dataGridViewAgentDialogueActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAgentDialogueActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAgentDialogueActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewAgentDialogueActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAgentDialogueActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAgentDialogueActions.Location = new System.Drawing.Point(6, 51);
            this.dataGridViewAgentDialogueActions.Name = "dataGridViewAgentDialogueActions";
            this.dataGridViewAgentDialogueActions.ReadOnly = true;
            this.dataGridViewAgentDialogueActions.RowHeadersVisible = false;
            this.dataGridViewAgentDialogueActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAgentDialogueActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAgentDialogueActions.Size = new System.Drawing.Size(478, 234);
            this.dataGridViewAgentDialogueActions.TabIndex = 13;
            // 
            // buttonAgentAddDialogAction
            // 
            this.buttonAgentAddDialogAction.Location = new System.Drawing.Point(6, 19);
            this.buttonAgentAddDialogAction.Name = "buttonAgentAddDialogAction";
            this.buttonAgentAddDialogAction.Size = new System.Drawing.Size(54, 23);
            this.buttonAgentAddDialogAction.TabIndex = 10;
            this.buttonAgentAddDialogAction.Text = "Add";
            this.buttonAgentAddDialogAction.UseVisualStyleBackColor = true;
            this.buttonAgentAddDialogAction.Click += new System.EventHandler(this.buttonAgentAddDialogAction_Click);
            // 
            // buttonAgentRemoveDialogAction
            // 
            this.buttonAgentRemoveDialogAction.Location = new System.Drawing.Point(126, 19);
            this.buttonAgentRemoveDialogAction.Name = "buttonAgentRemoveDialogAction";
            this.buttonAgentRemoveDialogAction.Size = new System.Drawing.Size(70, 23);
            this.buttonAgentRemoveDialogAction.TabIndex = 11;
            this.buttonAgentRemoveDialogAction.Text = "Remove";
            this.buttonAgentRemoveDialogAction.UseVisualStyleBackColor = true;
            this.buttonAgentRemoveDialogAction.Click += new System.EventHandler(this.buttonAgentRemoveDialogAction_Click);
            // 
            // DialogueEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 628);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenu);
            this.Name = "DialogueEditorForm";
            this.Text = "Dialogue Editor";
            this.Load += new System.EventHandler(this.DialogueEditorForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayerDialogueActions)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAgentDialogueActions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonPlayerEditDialogueAction;
        private System.Windows.Forms.DataGridView dataGridViewPlayerDialogueActions;
        private System.Windows.Forms.Button buttonAddPlayerDialogueAction;
        private System.Windows.Forms.Button buttonPlayerRemoveDialogueAction;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonAgentEditDialogAction;
        private System.Windows.Forms.DataGridView dataGridViewAgentDialogueActions;
        private System.Windows.Forms.Button buttonAgentAddDialogAction;
        private System.Windows.Forms.Button buttonAgentRemoveDialogAction;
    }
}