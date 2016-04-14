﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using EmotionalDecisionMaking.DTOs;
using EmotionalDecisionMakingWF.Properties;
using Equin.ApplicationFramework;
using KnowledgeBase.Conditions;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalDecisionMakingWF
{
    public partial class MainForm : Form
    {
        private EmotionalDecisionMakingAsset _edmAsset;
        private string _saveFileName;

        private BindingListView<ActionTendenciesDTO> _reactiveActions;
        private BindingListView<ConditionDTO> _conditions; 
        private Guid _selectedActionId;

        public MainForm()
        {
            InitializeComponent();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length <= 1)
            {
                Reset(true);
            }
            else
            {
                _saveFileName = args[1];
                try
                {
                    _edmAsset = EmotionalDecisionMakingAsset.LoadFromFile(_saveFileName);
                    Reset(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reset(true);
                }
            }
        }

        private void Reset(bool newFile)
        {
            if (newFile)
            {
                this.Text = Resources.MainFormTitle;
                this._edmAsset = new EmotionalDecisionMakingAsset();
            }
            else
            {
                this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }

            this._reactiveActions = new BindingListView<ActionTendenciesDTO>(_edmAsset.GetAllActionTendencies().ToList());
            dataGridViewReactiveActions.DataSource = this._reactiveActions;
            dataGridViewReactiveActions.Columns[PropertyUtil.GetName<ActionTendenciesDTO>(dto => dto.Id)].Visible = false;
            dataGridViewReactiveActions.Columns[PropertyUtil.GetName<ActionTendenciesDTO>(dto => dto.Conditions)].Visible = false;


            if (_reactiveActions.Any())
            {
                this._conditions = new BindingListView<ConditionDTO>(_edmAsset.GetReactionsConditions(_reactiveActions.First().Id).ToList());
            }
            else
            {
                this._conditions = new BindingListView<ConditionDTO>(new List<ConditionDTO>());
            }
            
            dataGridViewReactionConditions.DataSource = this._conditions;
            dataGridViewReactionConditions.Columns[PropertyUtil.GetName<ConditionDTO>(dto => dto.Id)].Visible = false;

            comboBoxQuantifierType.DataSource = Enum.GetNames(typeof(LogicalQuantifier));
        }



        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset(true);
        }


        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _edmAsset = EmotionalDecisionMakingAsset.LoadFromFile(ofd.FileName);
                    _saveFileName = ofd.FileName;
                    Reset(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_saveFileName))
            {
                saveHelper(true);
            }
            else
            {
                saveHelper(false);
            }
        }

        private void saveAsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveHelper(true);
        }

        private void saveHelper(bool newSaveFile)
        {
            if (newSaveFile)
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "EDM File|*.edm";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(sfd.FileName))
                    {
                        _saveFileName = sfd.FileName;
                    }
                }
                else
                {
                    return;
                }
            }
            try
            {
                using (var file = File.Create(_saveFileName))
                {
                    _edmAsset.SaveToFile(file);
                }
                this.Text = Resources.MainFormTitle + " - " + _saveFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.UnableToSaveFileError, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewReactiveActions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var reaction = ((ObjectView<ActionTendenciesDTO>)dataGridViewReactiveActions.Rows[e.RowIndex].DataBoundItem).Object;
            _selectedActionId = reaction.Id;

            _conditions.DataSource = _edmAsset.GetReactionsConditions(_selectedActionId).ToList();
            _conditions.Refresh();
        }
    }
}
