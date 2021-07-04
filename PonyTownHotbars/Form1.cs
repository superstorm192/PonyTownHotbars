using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace PonyTownHotbars
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            hotbarPanel.HorizontalScroll.Enabled = false;
            hotbarPanel.VerticalScroll.Enabled = true;
            hotbarPanel.VerticalScroll.Visible = true;
            toolStripStatusVersion.Text = "Version 1.0";
        }
        //Image PTEmoteImageMap = Image.FromFile(Application.StartupPath + "/PonyTownEmotes.png");
        Image PTEmoteImageMap = Properties.Resources.PonyTownEmotes;

        private void setPonyTownIcon(Button btn, PonyTownSlot slot)
        {
            if (slot == null || (slot.act == null && slot.cmd == null && slot.exp == null && slot.expr1 == null))
            {
                btn.Text = "";
                //emoteTooltips.SetToolTip(btn, "");

                btn.BackColor = Color.FromArgb(124, 202, 146);
                btn.FlatAppearance.BorderColor = Color.FromArgb(102, 165, 119);
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
                btn.Click += (object sender, EventArgs e) =>
                {
                    btn.Parent.Focus();
                };
            }
            else if (slot.act != null)
            {
                //btn.Text = "ACT\n" + slot.act;
                emoteTooltips.SetToolTip(btn, "act:" + slot.act);

                Image img = new Bitmap(29, 29);
                using (Graphics g = Graphics.FromImage(img)) {
                    PonyTownActions ptact = PonyTownActions.BLUSH;
                    if(Enum.TryParse(slot.act.Replace('-', '_'), true, out ptact))
                    {
                        g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), new Rectangle(29 * (int)ptact, 29 * EmoteMap.ACTIONS, 29, 29), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), new Rectangle(29 * (int)EmoteMap.SPECIAL.INVALID_ACTION, 0, 29, 29), GraphicsUnit.Pixel);
                    }
                }
                btn.BackColor = Color.FromArgb(220, 157, 130);
                btn.FlatAppearance.BorderColor = Color.FromArgb(183, 132, 110);
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
                btn.BackgroundImage = img;
                btn.BackgroundImageLayout = ImageLayout.None;
                btn.Click += (object sender, EventArgs e) =>
                {
                    btn.Parent.Focus();
                };
            }
            else if (slot.cmd != null)
            {
                //btn.Text = "CMD\n" + slot.cmd;
                emoteTooltips.SetToolTip(btn, "cmd:" + slot.cmd);

                Image img = new Bitmap(29, 29);
                using (Graphics g = Graphics.FromImage(img))
                {
                    PonyTownCommands ptact = PonyTownCommands.CANDIES;
                    if (Enum.TryParse(slot.cmd.Replace('-', '_').TrimStart('/'), true, out ptact))
                    {
                        g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), new Rectangle(29 * (int)ptact, 29 * EmoteMap.COMMANDS, 29, 29), GraphicsUnit.Pixel);
                    }
                    else
                    {
                        g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), new Rectangle(29 * (int)EmoteMap.SPECIAL.INVALID_ACTION, 0, 29, 29), GraphicsUnit.Pixel);
                    }
                    //g.DrawImage(PonyTownMap, -29*((int)Enum.Parse(typeof(PonyTownCommands), slot.cmd.Replace('-', '_').TrimStart('/'), true)), -29*6);
                }
                btn.BackColor = Color.FromArgb(95, 183, 179);
                btn.FlatAppearance.BorderColor = Color.FromArgb(78, 150, 146);
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
                btn.BackgroundImage = img;
                btn.BackgroundImageLayout = ImageLayout.None;
                btn.Click += (object sender, EventArgs e) =>
                {
                    btn.Parent.Focus();
                };
            }
            else if (slot.exp != null || slot.expr1 != null || slot.expr2 != null)
            {
                PonyTownEmote emote = new PonyTownEmote(slot);
                emoteTooltips.SetToolTip(btn, "expr1: " + emote.expr1 + "\nexpr2: " + emote.expr2
                    + "\nexp: " + emote.exp + (emote.isLesserCompatible()? " ✓" : " ✗")
                    + "\n\nMOUTH: " + emote.getMouthEnum() + " (" + emote.getMouth()
                    + ")\nL_EYE: " + emote.getEyeLeftEnum() + " (" + emote.getEyeLeft()
                    + ")\nR_EYE: " + emote.getEyeRightEnum() + " (" + emote.getEyeRight()
                    + ")\nL_PUPIL: " + emote.getPupilLeftEnum() + " (" + emote.getPupilLeft()
                    + ")\nR_PUPIL: " + emote.getPupilRightEnum() + " (" + emote.getPupilRight()
                    + ")\nTOGGLES: " + (emote.getBlush()==1 ? "\n- BLUSHING":"")
                    + (emote.getSleep()==1 ? "\n- SLEEPING" : "")
                    + (emote.getCry()==1 ? "\n- CRYING" : "")
                    + (emote.getTear()==1 ? "\n- TEARS" : "")
                    + (emote.getHeart()==1 ? "\n- HEARTS" : "")
                    );


                Image img = new Bitmap(29, 29);
                using (Graphics g = Graphics.FromImage(img))
                {
                    if (emote.exp == -1 || emote.expr1 == -1)
                    {
                        emoteTooltips.SetToolTip(btn, "expr1: -1\nexpr2: -1\nexp: -1 ✓\n\nReset Expression");
                        if (debugView.Checked)
                        {
                            //Draw reset expression
                            g.DrawImage(PTEmoteImageMap, -29 * (int)EmoteMap.SPECIAL.RESET_EXPRESSION, 0);
                        }
                        else
                        {
                            //Draw mouth
                            int currentInt = 0;
                            g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29),
                                new Rectangle(29 * (currentInt % 50), 29 * (EmoteMap.MOUTHS + (int)Math.Floor((float)currentInt / 50)), 29, 29), GraphicsUnit.Pixel);
                        }
                    }
                    else
                    {
                        if (debugView.Checked)
                        {
                            //Draw left pupil
                            int currentInt = Math.Min((int)emote.getPupilLeft(), 249);
                            g.DrawImage(PTEmoteImageMap,
                                new Rectangle(13, 0, 16, 20),
                                new Rectangle(29 * (currentInt % 50) + 13, 29 * (EmoteMap.LOOK + (int)Math.Floor((float)currentInt / 50)), 16, 20),
                                GraphicsUnit.Pixel);
                            //Draw right pupil
                            currentInt = Math.Min((int)emote.getPupilRight(), 249);
                            g.DrawImage(PTEmoteImageMap,
                                new Rectangle(0, 0, 13, 20),
                                new Rectangle(29 * (currentInt % 50), 29 * (EmoteMap.LOOK + (int)Math.Floor((float)currentInt / 50)), 13, 20),
                                GraphicsUnit.Pixel);

                            //Draw left eye
                            currentInt = Math.Min((int)emote.getEyeLeft(), 249);
                            g.DrawImage(PTEmoteImageMap,
                                new Rectangle(13, 0, 16, 20),
                                new Rectangle(29 * (currentInt % 50) + 13, 29 * (EmoteMap.EYES + (int)Math.Floor((float)currentInt / 50)), 16, 20),
                                GraphicsUnit.Pixel);
                            //Draw right eye
                            currentInt = Math.Min((int)emote.getEyeRight(), 249);
                            g.DrawImage(PTEmoteImageMap,
                                new Rectangle(0, 0, 13, 20),
                                new Rectangle(29 * (currentInt % 50), 29 * (EmoteMap.EYES + (int)Math.Floor((float)currentInt / 50)), 13, 20),
                                GraphicsUnit.Pixel);

                            //Draw toggle expressions
                            if (emote.getSleep() == 1)
                            {
                                //Draw transparent closed eye if sleeping
                                if (!emote.getEyeLeftEnum().ToString().Contains("CLOSED"))
                                {
                                    g.DrawImage(PTEmoteImageMap, new Rectangle(13, 0, 16, 20), new Rectangle(29 * (int)PonyTownToggles.SLEEP_OVERLAY + 13, 29 * EmoteMap.TOGGLE, 16, 20), GraphicsUnit.Pixel);
                                }
                                if (!emote.getEyeRightEnum().ToString().Contains("CLOSED"))
                                {
                                    g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 13, 20), new Rectangle(29 * (int)PonyTownToggles.SLEEP_OVERLAY, 29 * EmoteMap.TOGGLE, 13, 20), GraphicsUnit.Pixel);
                                }
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), 
                                    new Rectangle(29 * (int)PonyTownToggles.SLEEP, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }
                            if (emote.getBlush() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), 
                                    new Rectangle(29 * (int)PonyTownToggles.BLUSH, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }
                            if (emote.getCry() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), 
                                    new Rectangle(29 * (int)PonyTownToggles.CRY, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }
                            if (emote.getTear() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), 
                                    new Rectangle(29 * (int)PonyTownToggles.TEAR, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }
                            if (emote.getHeart() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), 
                                    new Rectangle(29 * (int)PonyTownToggles.HEART, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }

                            //Draw mouth
                            currentInt = Math.Min((int)emote.getMouth(), 249);
                            g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), 
                                new Rectangle(29 * (currentInt % 50), 29 * (EmoteMap.MOUTHS + (int)Math.Floor((float)currentInt / 50)), 29, 29), GraphicsUnit.Pixel);



                        }
                        else
                        {
                            int currentInt = 0;

                            //Draw left eye or closed if sleeping
                            if (emote.getEyeLeft() > 0)
                            {
                                //Draw left pupil
                                currentInt = Math.Min((int)emote.getPupilLeft(), (int)PonyTownLooking.Maximum);
                                g.DrawImage(PTEmoteImageMap,
                                    new Rectangle(13, 6, 16, 20),
                                    new Rectangle(29 * (currentInt % 50) + 13, 29 * (EmoteMap.LOOK + (int)Math.Floor((float)currentInt / 50)) + 6, 16, 20),
                                    GraphicsUnit.Pixel);
                                //Draw left eye or closed if sleeping
                                if (emote.getSleep() == 1 && !emote.getEyeLeftEnum().ToString().Contains("CLOSED"))
                                {
                                    currentInt = (int)PonyTownEye.SLEEP_CLOSED;
                                }  else {
                                    currentInt = Math.Min((int)emote.getEyeLeft(), (int)PonyTownEye.Maximum);
                                }
                                g.DrawImage(PTEmoteImageMap,
                                    new Rectangle(13, 0, 16, 20),
                                    new Rectangle(29 * (currentInt % 50) + 13, 29 * (EmoteMap.EYES + (int)Math.Floor((float)currentInt / 50)), 16, 20),
                                    GraphicsUnit.Pixel);
                            }
                            //Draw right eye or closed if sleeping
                            if (emote.getEyeRight() > 0)
                            {
                                //Draw right pupil
                                currentInt = Math.Min((int)emote.getPupilRight(), (int)PonyTownLooking.Maximum);
                                g.DrawImage(PTEmoteImageMap,
                                    new Rectangle(0, 6, 13, 20),
                                    new Rectangle(29 * (currentInt % 50), 29 * (EmoteMap.LOOK + (int)Math.Floor((float)currentInt / 50)) + 6, 13, 20),
                                    GraphicsUnit.Pixel);
                                //Draw right eye or closed if sleeping
                                if (emote.getSleep() == 1 && !emote.getEyeRightEnum().ToString().Contains("CLOSED"))
                                {
                                    currentInt = (int)PonyTownEye.SLEEP_CLOSED;
                                } else {
                                    currentInt = Math.Min((int)emote.getEyeRight(), (int)PonyTownEye.Maximum);
                                }
                                g.DrawImage(PTEmoteImageMap,
                                    new Rectangle(0, 0, 13, 20),
                                    new Rectangle(29 * (currentInt % 50), 29 * (EmoteMap.EYES + (int)Math.Floor((float)currentInt / 50)), 13, 20),
                                    GraphicsUnit.Pixel);
                            }

                            //Draw toggle expressions
                            if (emote.getSleep() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29), 
                                    new Rectangle(29 * (int)PonyTownToggles.SLEEP, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }
                            if (emote.getBlush() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29),
                                    new Rectangle(29 * (int)PonyTownToggles.BLUSH, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }
                            if (emote.getCry() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29),
                                    new Rectangle(29 * (int)PonyTownToggles.CRY, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }
                            if (emote.getTear() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29),
                                    new Rectangle(29 * (int)PonyTownToggles.TEAR, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }
                            if (emote.getHeart() == 1)
                            {
                                g.DrawImage(PTEmoteImageMap, new Rectangle(0, 0, 29, 29),
                                    new Rectangle(29 * (int)PonyTownToggles.HEART, 29 * EmoteMap.TOGGLE, 29, 29), GraphicsUnit.Pixel);
                            }

                            //Draw mouth
                            currentInt = Math.Min((int)emote.getMouth(), (int)PonyTownMouth.Maximum);
                            g.DrawImage(PTEmoteImageMap,
                                new Rectangle(0, 0, 29, 29),
                                new Rectangle(29 * (currentInt % 50), 29 * (EmoteMap.MOUTHS + (int)Math.Floor((float)currentInt / 50)), 29, 29),
                                GraphicsUnit.Pixel);
                        }
                    }
                }
                btn.BackColor = Color.FromArgb(231,170,78);
                btn.FlatAppearance.BorderColor = Color.FromArgb(183, 135, 62);
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                btn.BackgroundImage = img;
                btn.BackgroundImageLayout = ImageLayout.None;

                btn.Click += (object sender, EventArgs e) =>
                {
                    copyNewFormatToolStripMenuItem.Click += (object sender2, EventArgs e2) => {
                        Clipboard.SetText("{\"expr1\":" + emote.expr1 + ",\"expr2\":" + emote.expr2 + "}");
                    };
                    copyOldFormatToolStripMenuItem.Click += (object sender2, EventArgs e2) => {
                        Clipboard.SetText("{\"exp\":" + emote.exp + "}");
                    };
                    btn.Parent.Focus();
                    emoteContextMenu.Show(Cursor.Position);
                };
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePath.Text = Application.StartupPath;
            if (PonyTownHotbars.Properties.Settings.Default.FilePath != "")
            {
                filePath.Text = Properties.Settings.Default.FilePath;
            }
            //statusLabel.Text = "Loading hotbars from " + filePath.Text;
            setUpHotbars();

            //Int64[] nums = PonyTownEmote.ConvertToNewFormat(226370755);
            //MessageBox.Show("226370755 => "+nums[0]+", "+nums[1]+ 
            //    "\n252446982, 6659 =>"+PonyTownEmote.ConvertToOldFormat(252446982, 6659));
        }

        private void setUpHotbars()
        {
            statusLabel.Text = "Loading hotbars...";
            DateTime now = DateTime.UtcNow;
            emoteTooltips.RemoveAll();
            
            hotbarPanel.Controls.Clear();
            hotbarPanel.SuspendLayout();
            if (Directory.Exists(filePath.Text))
            {
                foreach(String fl in Directory.GetFiles(filePath.Text, "*.*").Where(x => x.ToLower().EndsWith(".json") || x.ToLower().EndsWith(".pta")))
                {
                    GroupBox gb = new GroupBox();
                    gb.Text = Path.GetFileName(fl) + " " + File.GetLastWriteTime(fl);
                    gb.Anchor = AnchorStyles.Left & AnchorStyles.Top & AnchorStyles.Right;
                   // gb.Height = 40;
                //    gb.Width = hotbarPanel.Width - hotbarPanel.Padding.Horizontal - gb.Margin.Horizontal - 20;
                    gb.MinimumSize = new Size(hotbarPanel.Width - hotbarPanel.Padding.Horizontal - gb.Margin.Horizontal - 20, 40);
                    gb.MaximumSize = new Size(hotbarPanel.Width - hotbarPanel.Padding.Horizontal - gb.Margin.Horizontal - 15, 200);
                    gb.AutoSize = true;

                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    flp.AutoSize = true;
                    flp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    //flp.Name = "";
                    flp.BackColor = Color.FromArgb(124,202,146);
                    flp.Height = 30;
                    //  flp.Width = hotbarPanel.Width - hotbarPanel.Margin.Horizontal - 30;


                    //flp.Anchor = AnchorStyles.Left & AnchorStyles.Top & AnchorStyles.Right;
                    flp.Top = flp.Margin.Top + gb.Padding.Top + 14;
                    flp.Left = flp.Margin.Left + gb.Padding.Left;
                    flp.FlowDirection = FlowDirection.LeftToRight;
                    flp.AutoScroll = true;
                    flp.WrapContents = true;

                 //   flp.MinimumSize = new Size(200,40);
                    flp.MinimumSize = new Size(hotbarPanel.Width - hotbarPanel.Margin.Horizontal - 40, 30);
                    flp.MaximumSize = new Size(hotbarPanel.Width - hotbarPanel.Margin.Horizontal - 40, 180);


                    //flp.Dock = DockStyle.Fill;
                    flp.AutoSize = true;
                    flp.Font = new Font(flp.Font.FontFamily, 6, FontStyle.Regular);

                    Button filebutton = new Button();
                    filebutton.Width = 33;
                    filebutton.Height = 33;
                    filebutton.FlatAppearance.BorderSize = 2;
                    filebutton.FlatStyle = FlatStyle.Flat;

                    filebutton.FlatAppearance.BorderColor = Color.FromArgb(183, 132, 110);
                    filebutton.FlatAppearance.MouseOverBackColor = filebutton.BackColor;
                    filebutton.FlatAppearance.MouseDownBackColor = filebutton.BackColor;
                    filebutton.Click += (object sender, EventArgs e) =>
                    {
                        Clipboard.SetText(fl);
                    };

                    flp.Controls.Add(filebutton);
                    emoteTooltips.SetToolTip(filebutton, "Copy Filepath to Clipboard\nPaste directly into \"File Name\" field when importing a hotbar.");


                    Image img = new Bitmap(29, 29);
                    using (Graphics g = Graphics.FromImage(img))
                    {
                        g.DrawImage(PTEmoteImageMap, -29 * (int)EmoteMap.SPECIAL.FOLDER, 0);
                    }
                    filebutton.BackgroundImage = img;
                    filebutton.BackgroundImageLayout = ImageLayout.None;

                    if (Path.GetExtension(fl) == ".json")
                    {
                        try
                        {
                            PonyTownSlot[] slots = JsonConvert.DeserializeObject<PonyTownSlot[]>(File.ReadAllText(fl));
                            for (Int32 cnt = 0; cnt < slots.Length; cnt++)
                            {
                                Button btn = new Button();
                                btn.Width = 33;
                                btn.Height = 33;
                                btn.FlatAppearance.BorderSize = 2;
                                btn.FlatStyle = FlatStyle.Flat;
                                setPonyTownIcon(btn, slots[cnt]);

                                flp.Controls.Add(btn);
                            }
                        } catch(Exception ex)
                        {
                            Label lb = new Label();
                            lb.Top = lb.Margin.Top + gb.Padding.Top + 14;
                            lb.Left = lb.Margin.Left + gb.Padding.Left;
                            lb.AutoSize = true;
                            lb.Text = ex.ToString();
                            lb.ForeColor = Color.FromArgb(255, 128, 0, 0);
                            gb.Controls.Add(lb);
                        }

                    }
                    else if (Path.GetExtension(fl) == ".pta")
                    {
                        try
                        {
                            byte[] bytes = Convert.FromBase64String(File.ReadAllText(fl));
                            //BinaryReader bread = new BinaryReader(File.ReadAllText(fl),Encoding.ASCII);
                            BinaryWriter binw = new BinaryWriter(new MemoryStream());
                            binw.Write(bytes);
                            BinaryReader bin = new BinaryReader(binw.BaseStream);
                            bin.BaseStream.Position = 0;

                            byte[] header = bin.ReadBytes(5);

                            UInt16 amt = bin.ReadByte();
                            if(amt < 50 && amt > 1) { amt--; }
                            for(Int32 cnt = 0; cnt < amt; cnt++)
                            {
                                Button btn = new Button();
                                btn.Width = 33;
                                btn.Height = 33;
                                btn.FlatAppearance.BorderSize = 2;
                                btn.FlatStyle = FlatStyle.Flat;

                                ushort oop = bin.ReadByte();
                                if (oop == 3)
                                {
                                    Int64 expr1 = BitConverter.ToInt32(bin.ReadBytes(4),0);
                                    Int64 expr2 = BitConverter.ToInt32(bin.ReadBytes(4),0);
                                    setPonyTownIcon(btn, new PonyTownEmote(expr1,expr2));
                                }
                                else if (oop == 1)
                                {
                                    setPonyTownIcon(btn, new PonyTownSlot() { act = bin.ReadString() });
                                }
                                else if (oop == 2)
                                {
                                    setPonyTownIcon(btn, new PonyTownSlot() { cmd = bin.ReadString() });
                                }
                                else if (oop == 0)
                                {
                                    setPonyTownIcon(btn, new PonyTownSlot() { });
                                }
                                else
                                {
                                    List<byte> extra = new byte[] { }.ToList();

                                    while(bin.PeekChar() > 3)
                                    {
                                        extra.Add(bin.ReadByte());
                                    }
                                    emoteTooltips.SetToolTip(btn, "Unknown Segment:/n" + BitConverter.ToString(extra.ToArray()));
                                    //btn.Text = "ACT\n" + BitConverter.ToString(extra.ToArray());
                                }

                                
                                flp.Controls.Add(btn);
                            }
                        }
                        catch (Exception ex)
                        {
                            Label lb = new Label();
                            lb.Top = lb.Margin.Top + gb.Padding.Top + 14;
                            lb.Left = lb.Margin.Left + gb.Padding.Left;
                            lb.AutoSize = true;
                            lb.Text = ex.ToString();
                            lb.ForeColor = Color.FromArgb(255, 128, 0, 0);
                            gb.Controls.Add(lb);
                        }

                        //Label lb = new Label();
                        //lb.Top = lb.Margin.Top + gb.Padding.Top + 14;
                        //lb.Left = lb.Margin.Left + gb.Padding.Left;
                        //lb.AutoSize = true;
                        //lb.Text = "PTA Format not supported yet.";
                        //lb.ForeColor = Color.FromArgb(255, 128, 0, 0);
                        //gb.Controls.Add(lb);
                    }

                    gb.Controls.Add(flp);
                    hotbarPanel.Controls.Add(gb);
                }
            }
            else
            {
                GroupBox gb = new GroupBox();
                gb.Text = "Information";
                gb.Anchor = AnchorStyles.Left & AnchorStyles.Top & AnchorStyles.Right;
                gb.Height = 50;
                gb.Width = hotbarPanel.Width - hotbarPanel.Padding.Horizontal - gb.Margin.Horizontal - 20;
                Label lb = new Label();
                lb.Top = lb.Margin.Top + gb.Padding.Top + 14;
                lb.Left = lb.Margin.Left + gb.Padding.Left;
                lb.AutoSize = true;
                lb.Text = "Selected directory does not exist.";
                lb.ForeColor = Color.FromArgb(255, 128, 0, 0);
                gb.Controls.Add(lb);

                hotbarPanel.Controls.Add(gb);
            }
            hotbarPanel.ResumeLayout();
            statusLabel.Text = "Loaded " + hotbarPanel.Controls.Count + " bars in " + (DateTime.UtcNow - now).TotalMilliseconds + " ms.";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FilePath = filePath.Text;
            Properties.Settings.Default.Save();
        }

        private void filePath_TextChanged(object sender, EventArgs e)
        {
            setUpHotbars();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            fld.SelectedPath = filePath.Text;
            DialogResult res = fld.ShowDialog();
            if (res == DialogResult.OK)
            {
                filePath.Text = fld.SelectedPath;
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            hotbarPanel.SuspendLayout();
            foreach (Control gb in hotbarPanel.Controls)
            {
                gb.MinimumSize = new Size(hotbarPanel.Width - hotbarPanel.Padding.Horizontal - gb.Margin.Horizontal - 20, gb.MinimumSize.Height);
                gb.MaximumSize = new Size(hotbarPanel.Width - hotbarPanel.Padding.Horizontal - gb.Margin.Horizontal - 20, gb.MaximumSize.Height);
            
                //gb.Width = hotbarPanel.Width - hotbarPanel.Padding.Horizontal - gb.Margin.Horizontal - 20;
                FlowLayoutPanel[] ctrls = gb.Controls.OfType<FlowLayoutPanel>().ToArray();
                if (ctrls.Length > 0)
                {
                   // ctrls.First().Height = 0;
                   // ctrls.First().PerformLayout();
                    //ctrls.First().Width = hotbarPanel.Width - hotbarPanel.Margin.Horizontal - 30;
                    ctrls.First().MinimumSize = new Size(hotbarPanel.Width - hotbarPanel.Margin.Horizontal - 40, ctrls.First().MinimumSize.Height);
                    ctrls.First().MaximumSize = new Size(hotbarPanel.Width - hotbarPanel.Margin.Horizontal - 40, ctrls.First().MaximumSize.Height);
                }
                //gb.PerformLayout();
                PonyTownEmote.ConvertToNewFormat(0);
            }
            hotbarPanel.ResumeLayout();
        }

        private void debugView_CheckedChanged(object sender, EventArgs e)
        {
            if( Control.ModifierKeys != Keys.Shift)
            {
                debugView.CheckedChanged -= debugView_CheckedChanged;
                debugView.Checked = !debugView.Checked;
                debugView.CheckedChanged += debugView_CheckedChanged;
            }
            debugView.Text = debugView.Checked ? "Reload 🔍" : "Reload [  ]";
            hotbarPanel.Focus();
            setUpHotbars();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/superstorm192/PonyTownHotbars");
        }
    }

    public class PonyTownSlot
    {
        /// <summary>
        /// The action associated with the slot.
        /// </summary>
        public string act { get; set; }
        /// <summary>
        /// The command associated with the slot.
        /// </summary>
        public string cmd { get; set; }
        /// <summary>
        /// The old emote format number.
        /// </summary>
        public Int64? exp { get; set; }
        /// <summary>
        /// The eye control segment of the new emote format.
        /// </summary>
        public Int64? expr1 { get; set; }
        /// <summary>
        /// The mouth and toggle segment of the new emote format.
        /// </summary>
        public Int64? expr2 { get; set; }
    }
    public class PonyTownEmote : PonyTownSlot {
        public bool isValid()
        {
            return this.exp != null && this.expr1 != null && this.expr2 != null;
        }
        /// <summary>
        /// Checks if the emote can be converted down to the old emote format without data loss.
        /// </summary>
        public bool isLesserCompatible()
        {
            Int64[] nums = ConvertToNewFormat(this.exp.Value);
            return nums[0] == this.expr1 && nums[1] == this.expr2;
            //return false;
        }
        /// <summary>
        /// Converts a number to new emote format.
        /// </summary>
        /// <param name="number">Old Emote Number</param>
        public static Int64[] ConvertToNewFormat(Int64 number)
        {
            return new Int64[]{ (number>>5&31) + (number>>2&7936) + (number<<1&983040) + (number<<5&251658240),
                                (number&31) + (number>>15&7936)};
        }
        /// <summary>
        /// Converts two numbers to old emote format. Out of bounds data may be lost on converting.
        /// </summary>
        /// <param name="number1">Eye segment of the new emote format.</param>
        /// <param name="number2">Mouth and toggles for new emote format.</param>
        public static Int64 ConvertToOldFormat(Int64 number1, Int64 number2)
        {
            return (number2&31) + (number1<<5&992) + (number1<<2&31744) + (number1>>1&491520) + (number1>>5&7864320)
                + (number2<<15&260046848);
        }
        /// <summary>
        /// A pony town slot formatted as a useful emote.
        /// </summary>
        public PonyTownEmote()
        {
            if(this.expr1.HasValue && this.expr2.HasValue)
            {
                this.exp = ConvertToOldFormat(this.expr1.Value, this.expr2.Value);
            }
            else if(this.exp.HasValue)
            {
                Int64[] nums = ConvertToNewFormat(this.exp.Value);
                this.expr1 = nums[0];
                this.expr2 = nums[1];
            }
            else
            {
                this.expr1 = 0;
                this.expr2 = 0;
                this.exp = ConvertToOldFormat(this.expr1.Value, this.expr2.Value); ;
            }
        }

        public PonyTownEmote(PonyTownSlot slot)
        {
            if (slot.expr1.HasValue && slot.expr2.HasValue)
            {
                this.expr1 = slot.expr1.Value;
                this.expr2 = slot.expr2.Value;
                this.exp = ConvertToOldFormat(slot.expr1.Value, slot.expr2.Value);
            }
            else if (slot.exp.HasValue)
            {
                this.exp = slot.exp.Value;
                Int64[] nums = ConvertToNewFormat(slot.exp.Value);
                this.expr1 = nums[0];
                this.expr2 = nums[1];
            }
            else
            {
                this.expr1 = 0;
                this.expr2 = 0;
                this.exp = ConvertToOldFormat(slot.expr1.Value, slot.expr2.Value); ;
            }
        }
        /// <param name="number">Old Emote Number</param>
        public PonyTownEmote(Int64 number)
        {
            this.exp = number;
            Int64[] nums = ConvertToNewFormat(number);
            this.expr1 = nums[0];
            this.expr2 = nums[1];
        }
        /// <param name="number1">Eye section of new emote number</param>
        /// <param name="number2">Mouth section of new emote number</param>
        public PonyTownEmote(Int64 number1, Int64 number2)
        {
            this.expr1 = number1;
            this.expr2 = number2;
            this.exp = ConvertToOldFormat(number1, number2);
        }

        public Int16 getMouth()
        {
            if(expr2.HasValue) { return (short)(this.expr2 & 255); }
            else if (exp.HasValue) { return (short)(this.exp & 31); }
            return 0;
        }
        public PonyTownMouth getMouthEnum()
        {
            return (PonyTownMouth)getMouth();
        }
        public Int16 getEyeLeft()
        {
            if (expr1.HasValue) { return (short)(this.expr1 & 255); }
            else if (exp.HasValue) { return (short)(this.exp >> 5 & 31); }
            return 0;
        }
        public PonyTownEye getEyeLeftEnum()
        {
            return (PonyTownEye)getEyeLeft();
        }
        public Int16 getEyeRight()
        {
            if (expr1.HasValue) { return (short)(this.expr1 >> 8 & 255); }
            else if (exp.HasValue) { return (short)(this.exp >> 10 & 31); }
            return 0;
        }
        public PonyTownEye getEyeRightEnum()
        {
            return (PonyTownEye)getEyeRight();
        }
        public Int16 getPupilLeft()
        {
            if (expr1.HasValue) { return (short)(this.expr1 >> 16 & 255); }
            else if (exp.HasValue) { return (short)(this.exp >> 15 & 15); }
            return 0;
        }
        public PonyTownLooking getPupilLeftEnum()
        {
            return (PonyTownLooking)getPupilLeft();
        }
        public Int16 getPupilRight()
        {
            if (expr1.HasValue) { return (short)(this.expr1 >> 24 & 255); }
            else if (exp.HasValue) { return (short)(this.exp >> 19 & 15); }
            return 0;
        }
        public PonyTownLooking getPupilRightEnum()
        {
            return (PonyTownLooking)getPupilRight();
        }
        public Int16 getBlush()
        {
            if (expr2.HasValue) { return (short)(this.expr2 >> 8 & 1); }
            else if (exp.HasValue) { return (short)(this.exp >> 23 & 1); }
            return 0;
        }
        public Int16 getSleep()
        {
            if (expr2.HasValue) { return (short)(this.expr2 >> 9 & 1); }
            else if (exp.HasValue) { return (short)(this.exp >> 24 & 1); }
            return 0;
        }
        public Int16 getCry()
        {
            if (expr2.HasValue) { return (short)(this.expr2 >> 10 & 1); }
            else if (exp.HasValue) { return (short)(this.exp >> 25 & 1); }
            return 0;
        }
        public Int16 getTear()
        {
            if (expr2.HasValue) { return (short)(this.expr2 >> 11 & 1); }
            else if (exp.HasValue) { return (short)(this.exp >> 26 & 1); }
            return 0;
        }
        public Int16 getHeart()
        {
            if (expr2.HasValue) { return (short)(this.expr2 >> 12 & 1); }
            else if (exp.HasValue) { return (short)(this.exp >> 27 & 1); }
            return 0;
        }
    }
    public enum PonyTownMouth
    {
        SMILE, FROWN, NEUTRAL, SCRUNCH, BLEP, SMILE_OPEN, STRAIGHT, MAD, MAD_OPEN, SMILE_WIDE, FROWN_WIDE,
        NEUTRAL_OPEN, MAD_WIDE, NOSE, SMILE_WIDER, NEUTRAL_WIDER, MAD_WIDER, NOSE_2, SMILE_TEETH, UPSET_TEETH,
        NEUTRAL_TEETH, ANGRY_TEETH, SMILE_TONGUE, NEUTRAL_TONGUE, FROWN_OPEN,
        //ADDITIONAL

        Maximum = 98
    }
    public enum PonyTownEye
    {
        NONE, OPENED_FULL, OPENED_MOST, OPENED_HALF, OPENED_SQUINT, BLINK, SLEEP_CLOSED, BROW_MOST, BROW_HALF, BROW_SQUINT,
        BROW_BLINK, NEUTRAL_CLOSED, SMILE_CLOSED, SMILE_HALF_CLOSED, SMILE_MOST_CLOSED, SLANTED_MOST, SHY_MOST, SHY_HALF,
        CALM_SQUINT, MAD_MOST, MAD_HALF, BLISS_CLOSED, GRIMACE_SQUINT_CLOSED, GRIMACE_FULL_CLOSED, GRIMACE_HALF_CLOSED,
        //ADDITIONAL
        CALM_MOST, CALM_HALF, BROW_FULL, MISCHIEF_FULL, MISCHIEF_MOST, MISCHIEF_HALF, MISCHIEF_SQUINT, SHY_MOST2,
        QBROW_FULL, QBROW_MOST, QBROW_HALF, QBROW_SQUINT, OBROW_FULL, OBROW_MOST, OBROW_HALF, OBROW_SQUINT,
        SHYBROW_MOST, SHYBROW_HALF, SHYBROW_SQUINT, SHYBROW2_MOST, SHYBROW2_HALF, SHYBROW2_SQUINT, SADBROW_MOST,
        SADBROW_HALF, SADBROW_SQUINT, SADBROW2_HALF, SADBROW2_SQUINT, SMILE_BROW_CLOSED, SMILE_BROW_CLOSED_HALF,
        SMILE_BROW_CLOSED_MOST, BROW_CLOSED_SQINT, BROW_CLOSED_HALF, BROW_CLOSED_MOST, GRIMACE_BROW_CLOSED_HALF,
        GRIMACE_BROW_CLOSED_MOST, GRIMACE_BROW_CLOSED, CALM_BROW_MOST, CALM_BROW_HALF, CALM_BROW_SQUINT, SLANT_SQUINT,
        Maximum = SLANT_SQUINT
    }
    public enum PonyTownLooking
    {
        LOOK, UPWARD, RIGHT, LEFT, TOP_RIGHT, TOP_LEFT, SCARED, DOWNWARD,
        //ADDITIONAL
        DOWN_RIGHT, DOWN_LEFT, SCARED_TOP, SCARED_RIGHT, SCARED_LEFT, SCARED_TOP_RIGHT, SCARED_TOP_LEFT,
        SCARED_DOWN, SCARED_DOWN_RIGHT, SCARED_DOWN_LEFT, BOTTOM, BOTTOM_RIGHT, BOTTOM_LEFT, SCARED_BOTTOM,
        SCARED_BOTTOM_RIGHT, SCARED_BOTTOM_LEFT,
        Maximum = SCARED_BOTTOM_LEFT
    }
    public enum PonyTownToggles
    {
        BLUSH, SLEEP, CRY, TEAR, HEART, SLEEP_OVERLAY
    }
    public enum PonyTownActions
    {
        DOWN, UP, BOOP, TURN_HEAD, SNEEZE, SLEEP, YAWN, LOVE, LAUGH, BLUSH, CRY, TEARS, KISS, DROP, DROP_TOY, MAGIC,
        EAT, NOM, USE, CHAR_EDITOR, UNDO, REDO, SWITCH_TOOL, SWITCH_ENTITY, SWITCH_ENTITY_REV, NEXT_COLOR, NEXT_SEASON,
        MOVE_ENTITY, CLONE_ENTITY, REMOVE_ENTITY, SWITCH_TILE, SWITCH_TILE_REV, TOGGLE_WALL, SWITCH_WALL_REV,
        MANAGE_MAPS, WALLS_MODE, DANCE, DANCE2, DANCE3, DANCE4, DANCE5, DANCE6, DANCE7, DANCE8, JUMP, MASK_DOWN, APPLAUD, APPLAUD2
    }
    public enum PonyTownCommands
    {
        SAVEMAP, LOADMAP, RESETMAP, CLEARMAP, REMOVETOOLBOX, RESTORETOOLBOX, ROLL, GIFTS, CANDIES, CLOVERS, TOYS,
        EGGS, PEARLS
    }
    public static class EmoteMap
    {
        public enum SPECIAL { FOLDER, RESET_EXPRESSION, INVALID_ACTION, INVALID_COMMAND, EXPRESSION_BASE };
        public static int LOOK = 1;
        public static int EYES = 2;
        public static int MOUTHS = 7;
        public static int TOGGLE = 12;
        public static int ACTIONS = 13;
        public static int COMMANDS = 16;
    }
}
