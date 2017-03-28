﻿using FameBot.Core;
using FameBot.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FameBot.Data.Events;

namespace FameBot.UserInterface
{
    public partial class FameBotGUI : Form
    {
        private IntPtr flashPtr;

        public FameBotGUI()
        {
            InitializeComponent();
            eventLog.Text += "\n";
            FormClosed += (s, e) =>
            {
                Plugin.InvokeGuiEvent(GuiEvent.GuiClosed);
            };
            Plugin.logEvent += (s, e) =>
            {
                UpdateEventLog(s, e);
            };
        }
        
        private void UpdateEventLog(object sender, LogEventArgs args)
        {
            if (this.eventLog.InvokeRequired)
            {
                this.eventLog.Invoke(new MethodInvoker(() =>
                {
                    UpdateEventLog(sender, args);
                }));
            } else
            {
                eventLog.Text += (args.MessageWithTimestamp + "\n");
                eventLog.SelectionStart = eventLog.Text.Length;
                eventLog.ScrollToCaret();
            }
        }

        public void SetHandle(IntPtr handle)
        {
            flashPtr = handle;
        }

        private void onButton_Click(object sender, EventArgs e)
        {
            Plugin.InvokeGuiEvent(GuiEvent.StartBot);
        }

        private void offButton_Click(object sender, EventArgs e)
        {
            Plugin.InvokeGuiEvent(GuiEvent.StopBot);
        }

        private void windowOnTopBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = windowOnTopBox.Checked;
        }

        private void showHealthBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HealthBarGUI healthBarGUI = new HealthBarGUI();
            healthBarGUI.Show();
        }

        private void showKeyPressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KeyPressGUI keyPressGUI = new KeyPressGUI();
            keyPressGUI.Show();
        }

        private void openConfigManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsGUI settingsGUI = new SettingsGUI();
            settingsGUI.Show();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            eventLog.Text = "";
        }

        private void showGraphicsOverlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OverlayGUI overlayGUI = new OverlayGUI(flashPtr);
            overlayGUI.Show();
        }
    }
}
