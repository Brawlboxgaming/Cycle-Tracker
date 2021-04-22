using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RT.Util;

namespace Cycle_Tracker
{
    public partial class VisualForm : Form
    {
        bool found = false;
        PictureBox[] drops;
        PictureBox[] dropsList;
        string inputDrop;
        string cycleTag = "4cycle";
        List<string> allDrops;
        List<string> leftDrops;
        List<string> usedDrops = new List<string>();
        GlobalKeyboardListener keyboard;

        public VisualForm()
        {
            InitializeComponent();
            tabControl.TabPages.Remove(bo1Tab);
            tabControl.TabPages.Remove(bo2Tab);
            tabControl.TabPages.Remove(bo3Tab);
            tabControl.TabPages.Remove(bo4Tab);
            tabControl.TabPages.Remove(bocwTab);
            keyboard = new GlobalKeyboardListener();
            keyboard.HookAllKeys = true;
            keyboard.KeyDown += PressKey;
            drops = new[] { dropMax, dropInsta, dropDouble, dropNuke, dropFire, dropDeath, dropBlood, dropBonus, dropFull, dropCarpenter };
            dropsList = new[] { dropList1, dropList2, dropList3, dropList4, dropList5, dropList6, dropList7, dropList8, dropList9, dropList10, dropList11, dropList12 };
        }

        private void PressKey(object sender, GlobalKeyEventArgs e)
        {
            if (e.VirtualKeyCode == Keys.NumPad0)
                startButton_Click(sender, e);
            else if (e.VirtualKeyCode >= Keys.NumPad1 && e.VirtualKeyCode <= Keys.NumPad9)
            {
                if (ModifierKeys.HasFlag(Keys.Alt))
                {
                    inputDrop = ProcessDigits(e.VirtualKeyCode - Keys.NumPad0 + 9);
                }
                else
                {
                    inputDrop = ProcessDigits(e.VirtualKeyCode - Keys.NumPad0);
                }
                CalculateDropsLeft();
                DisplayDrops();
            }
            else if (e.VirtualKeyCode == Keys.F5)
                carpToggle.Checked = !carpToggle.Checked;
            else if (e.VirtualKeyCode == Keys.F6)
                fireToggle.Checked = !fireToggle.Checked;
            else if (e.VirtualKeyCode == Keys.F7)
                deathToggle.Checked = !deathToggle.Checked;
            else if (e.VirtualKeyCode == Keys.F8)
                refillToggle.Checked = !refillToggle.Checked;
            else if (e.VirtualKeyCode == Keys.F9)
                usedDropsToggle.Checked = !usedDropsToggle.Checked;
        }

        private void CalculateDropsLeft()
        {
            if (allDrops.Contains(inputDrop))
            {
                usedDrops.Add(inputDrop);
            }

            if (usedDrops.Count > 12)
                usedDrops.RemoveAt(0);

            if (!leftDrops.Contains(inputDrop) && allDrops.Contains(inputDrop))
            {
                leftDrops.Clear();
                leftDrops.AddRange(allDrops);
                leftDrops.Remove(inputDrop);
                found = false;
            }
            else
            {
                leftDrops.Remove(inputDrop);

                if (leftDrops.Count == 0)
                {
                    leftDrops.AddRange(allDrops);
                    found = true;
                }
            }
        }

        private void DisplayDrops()
        {
            if (usedDrops.Count > 0)
            {
                for (int i = 0; i < usedDrops.Count; i++)
                {
                    switch (usedDrops[usedDrops.Count - 1 - i])
                    {
                        case "max":
                            dropsList[i].Image = Resources.maxammo_small;
                            break;
                        case "insta":
                            dropsList[i].Image = Resources.instakill_small;
                            break;
                        case "double":
                            dropsList[i].Image = Resources.doublepoints_small;
                            break;
                        case "nuke":
                            dropsList[i].Image = Resources.nuke_small;
                            break;
                        case "fire":
                            dropsList[i].Image = Resources.firesale_small;
                            break;
                        case "death":
                            dropsList[i].Image = Resources.deathmachine_small;
                            break;
                        case "blood":
                            dropsList[i].Image = Resources.zombieblood_small;
                            break;
                        case "bonus":
                            dropsList[i].Image = Resources.bonuspoints_small;
                            break;
                        case "full":
                            dropsList[i].Image = Resources.fullpower_small;
                            break;
                        case "carpenter":
                            dropsList[i].Image = Resources.carpenter_small;
                            break;
                    }
                }
            }

            if (found)
            {
                foundDisplay.Text = "Found Potential Drop Cycle!";
                foundDisplay.ForeColor = Color.Green;
                foundDisplay.Font = new Font(foundDisplay.Font, FontStyle.Bold);
                if (allDrops.Count == leftDrops.Count)
                {
                    for (int i = 0; i < allDrops.Count; i++)
                    {
                        switch (allDrops[i])
                        {
                            case "max":
                                dropMax.Visible = true;
                                break;
                            case "insta":
                                dropInsta.Visible = true;
                                break;
                            case "double":
                                dropDouble.Visible = true;
                                break;
                            case "nuke":
                                dropNuke.Visible = true;
                                break;
                            case "fire":
                                dropFire.Visible = true;
                                break;
                            case "death":
                                dropDeath.Visible = true;
                                break;
                            case "blood":
                                dropBlood.Visible = true;
                                break;
                            case "bonus":
                                dropBonus.Visible = true;
                                break;
                            case "carpenter":
                                dropCarpenter.Visible = true;
                                break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < drops.Length; i++)
                        drops[i].Visible = false;
                    for (int i = 0; i < leftDrops.Count; i++)
                    {
                        switch (leftDrops[i])
                        {
                            case "max":
                                dropMax.Visible = true;
                                break;
                            case "insta":
                                dropInsta.Visible = true;
                                break;
                            case "double":
                                dropDouble.Visible = true;
                                break;
                            case "nuke":
                                dropNuke.Visible = true;
                                break;
                            case "fire":
                                dropFire.Visible = true;
                                break;
                            case "death":
                                dropDeath.Visible = true;
                                break;
                            case "blood":
                                dropBlood.Visible = true;
                                break;
                            case "bonus":
                                dropBonus.Visible = true;
                                break;
                            case "full":
                                dropFull.Visible = true;
                                break;
                            case "carpenter":
                                dropCarpenter.Visible = true;
                                break;
                        }
                    }
                }
            }
            else
            {
                foundDisplay.Text = "Finding Drop Cycle...";
                foundDisplay.ForeColor = Color.Red;
                foundDisplay.Font = new Font(foundDisplay.Font, FontStyle.Regular);
                for (int i = 0; i < drops.Length; i++)
                    drops[i].Visible = false;
            }
        }

        private string ProcessDigits(int digit)
        {
            switch (digit)
            {
                case 1:
                    return "max";
                case 2:
                    return "insta";
                case 3:
                    return "double";
                case 4:
                    return "nuke";
                case 5:
                    return "fire";
                case 6:
                    return "death";
                case 7:
                    return "blood";
                case 8:
                    return "bonus";
                case 9:
                    return "full";
                case 10:
                    return "carpenter";
                default:
                    return null;
            }
        }

        private void InitialiseDrops()
        {
            switch (cycleTag)
            {
                case "4cycle":
                case "4cycleAlt":
                case "5cycle":
                case "5cycleAlt":
                case "5cycleAlt2":
                case "6cycle":
                    allDrops = new List<string> { "max", "insta", "double", "nuke" };
                    break;
                case "5cycleAlt3":
                case "5cycleAlt4":
                    allDrops = new List<string> { "max", "insta", "double", "nuke", "bonus" };
                    break;
                case "6cycleAlt":
                case "7cycle":
                    allDrops = new List<string> { "max", "insta", "double", "nuke", "blood" };
                    break;
                default:
                    return;
            }
            leftDrops = allDrops.ToList();
            usedDrops.Clear();
            dropMax.Image = Resources.maxammo;
            dropInsta.Image = Resources.instakill;
            dropDouble.Image = Resources.doublepoints;
            dropNuke.Image = Resources.nuke;
            dropFire.Image = Resources.firesale;
            dropDeath.Image = Resources.deathmachine;
            dropBlood.Image = Resources.zombieblood;
            dropBonus.Image = Resources.bonuspoints;
            dropFull.Image = Resources.fullpower;
            dropCarpenter.Image = Resources.carpenter;
            for (int i = 0; i < drops.Length; i++)
                drops[i].Visible = false;
            for (int i = 0; i < dropsList.Length; i++)
                dropsList[i].Image = null;
            foundDisplay.Text = "Finding Drop Cycle...";
            foundDisplay.ForeColor = Color.Red;
            foundDisplay.Font = new Font(foundDisplay.Font, FontStyle.Regular);
            found = false;
            if ((cycleTag == "5cycle" || cycleTag == "5cycleAlt2" || cycleTag == "5cycleAlt3" || cycleTag == "5cycleAlt4" || cycleTag == "6cycle" || cycleTag == "6cycleAlt" || cycleTag == "7cycle") && fireToggle.Checked)
            {
                allDrops.Add("fire");
                leftDrops.Add("fire");
            }
            if ((cycleTag == "5cycleAlt" || cycleTag == "6cycle" || cycleTag == "7cycle") && deathToggle.Checked)
            {
                allDrops.Add("death");
                leftDrops.Add("death");
            }
            if ((cycleTag == "4cycleAlt" || cycleTag == "5cycle" || cycleTag == "5cycleAlt" || cycleTag == "5cycleAlt3" || cycleTag == "5cycleAlt4" || cycleTag == "6cycle" || cycleTag == "7cycle") && carpToggle.Checked)
            {
                allDrops.Add("carpenter");
                leftDrops.Add("carpenter");
            }
        }

        private void gameButton_Clicked(object sender, System.EventArgs e)
        {
            tabControl.TabPages.Remove(wawTab);
            tabControl.TabPages.Remove(bo1Tab);
            tabControl.TabPages.Remove(bo2Tab);
            tabControl.TabPages.Remove(bo3Tab);
            tabControl.TabPages.Remove(bo4Tab);
            tabControl.TabPages.Remove(bocwTab);
            tabControl.TabPages.Insert(1,
                sender == waw ? wawTab :
                sender == bo1 ? bo1Tab :
                sender == bo2 ? bo2Tab :
                sender == bo3 ? bo3Tab :
                sender == bo4 ? bo4Tab :
                bocwTab);
            if (sender == waw)
                tabControl.SelectedTab = wawTab;
            if (sender == bo1)
                tabControl.SelectedTab = bo1Tab;
            if (sender == bo2)
                tabControl.SelectedTab = bo2Tab;
            if (sender == bo3)
                tabControl.SelectedTab = bo3Tab;
            if (sender == bo4)
                tabControl.SelectedTab = bo4Tab;
            if (sender == bocw)
                tabControl.SelectedTab = bocwTab;
        }

        private void mapButton_Click(object sender, EventArgs e)
        {
            cycleTag = (string)((Control)sender).Tag;
            tabControl.SelectedTab = tracker;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Start")
            {
                InitialiseDrops();
                startButton.Text = "Stop";
                restartButton.Visible = true;
                foundDisplay.Visible = true;
            }
            else
            {
                startButton.Text = "Start";
                restartButton.Visible = false;
                foundDisplay.Visible = false;
                //leftDropsDisplay.Text = "";
                //usedDropsDisplay.Text = "";
            }
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Stop")
            {
                InitialiseDrops();
            }
        }

        private void usedDropsToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (usedDropsToggle.Checked)
            {
                for (int i = 0; i < dropsList.Length; i++)
                    dropsList[i].Visible = true;
                usedDropsLabel.Visible = true;
                usedDropsToggle.Font = new Font(usedDropsToggle.Font, FontStyle.Bold);
            }
            else
            {
                for (int i = 0; i < dropsList.Length; i++)
                    dropsList[i].Visible = false;
                usedDropsLabel.Visible = false;
                usedDropsToggle.Font = new Font(usedDropsToggle.Font, FontStyle.Regular);
            }
        }

        private void carpToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (carpToggle.Checked)
                carpToggle.Font = new Font(carpToggle.Font, FontStyle.Bold);
            else
                carpToggle.Font = new Font(carpToggle.Font, FontStyle.Regular);
            if (cycleTag == "4cycleAlt" || cycleTag == "5cycle" || cycleTag == "5cycleAlt" || cycleTag == "5cycleAlt3" || cycleTag == "5cycleAlt4" || cycleTag == "6cycle" || cycleTag == "7cycle")
            {
                if (carpToggle.Checked)
                {
                    allDrops.Add("carpenter");
                    leftDrops.Add("carpenter");
                    dropCarpenter.Visible = true;
                }
                else
                {
                    allDrops.Remove("carpenter");
                    leftDrops.Remove("carpenter");
                    dropCarpenter.Visible = false;
                }
                inputDrop = null;
                CalculateDropsLeft();
                if (startButton.Text != "Start")
                    DisplayDrops();
            }
        }

        private void fireToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (fireToggle.Checked)
                fireToggle.Font = new Font(fireToggle.Font, FontStyle.Bold);
            else
                fireToggle.Font = new Font(fireToggle.Font, FontStyle.Regular);

            if (cycleTag == "5cycle" || cycleTag == "5cycleAlt2" || cycleTag == "5cycleAlt3" || cycleTag == "5cycleAlt4" || cycleTag == "6cycle" || cycleTag == "6cycleAlt" || cycleTag == "7cycle")
            {
                if (fireToggle.Checked)
                {
                    allDrops.Add("fire");
                    leftDrops.Add("fire");
                    dropFire.Visible = true;
                }
                else
                {
                    allDrops.Remove("fire");
                    leftDrops.Remove("fire");
                    dropFire.Visible = false;
                }
                inputDrop = null;
                CalculateDropsLeft();
                if (startButton.Text != "Start")
                    DisplayDrops();
            }
        }

        private void deathToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (deathToggle.Checked)
                deathToggle.Font = new Font(deathToggle.Font, FontStyle.Bold);
            else
                deathToggle.Font = new Font(deathToggle.Font, FontStyle.Regular);
            if (cycleTag == "5cycleAlt" || cycleTag == "6cycle" || cycleTag == "7cycle")
            {
                if (deathToggle.Checked)
                {
                    allDrops.Add("death");
                    leftDrops.Add("death");
                    dropDeath.Visible = true;
                }
                else
                {
                    allDrops.Remove("death");
                    leftDrops.Remove("death");
                    dropDeath.Visible = false;
                }
                inputDrop = null;
                CalculateDropsLeft();
                if (startButton.Text != "Start")
                    DisplayDrops();
            }
        }

        private void refillToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (refillToggle.Checked)
                refillToggle.Font = new Font(refillToggle.Font, FontStyle.Bold);
            else
                refillToggle.Font = new Font(refillToggle.Font, FontStyle.Regular);
            if (cycleTag == "5cycleAlt4")
            {
                if (refillToggle.Checked)
                {
                    allDrops.Add("full");
                    leftDrops.Add("full");
                    dropFull.Visible = true;
                }


                else
                {
                    allDrops.Remove("full");
                    leftDrops.Remove("full");
                    dropFull.Visible = false;
                }
            }
            inputDrop = null;
            CalculateDropsLeft();
            if (startButton.Text != "Start")
                DisplayDrops();
        }
    }
}