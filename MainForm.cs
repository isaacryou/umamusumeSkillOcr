using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using Windows.Graphics.Imaging;
using System.Text.RegularExpressions;


namespace UmamusumeSkillOCR
{

    public partial class MainForm : Form
    {
        private OCR skillOcr;
        private UmamusumeSkill skillFinder;
        private List<skillInfo> allSkills;
        private List<String> allBannedTexts;

        private Bitmap activeGameBitmap;
        private String windowTitle;

        private Config programConfig;

        private int gameWindowX = -1;
        private int gameWindowY = -1;

        private bool gameWindowFound = false;

        public MainForm()
        {
            InitializeComponent();

            this.Text = "Umamusume Skill OCR";
            this.TopMost = true;
            
            skillOcr = new OCR();
            skillFinder = new UmamusumeSkill();
            loadConfig();

            populateInterface();

            allSkills = skillFinder.getAllSkills(programConfig);
            allBannedTexts = skillFinder.getAllBannedTexts();

            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(tickEvent);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        private void populateInterface()
        {
            this.Icon = Properties.Resources.UmamusumeSkillIcon;

            previewScreenButton.Enabled = false;
            screenXTextEdit.Enabled = false;
            screenYTextEdit.Enabled = false;
            screenHeightTextEdit.Enabled = false;
            screenWidthTextEdit.Enabled = false;

            screenXTextEdit.Text = programConfig.screenX.ToString();
            screenYTextEdit.Text = programConfig.screenY.ToString();
            screenWidthTextEdit.Text = programConfig.screenWidth.ToString();
            screenHeightTextEdit.Text = programConfig.screenHeight.ToString();

            foreach (var language in skillFinder.languageList)
            {
                languageBox.Items.Add(language);
            }

            languageBox.SelectedItem = programConfig.language;
        }

        private void loadConfig()
        {
            programConfig = Config.loadProgramConfig();
            configText.Text = "Config loaded";
        }

        private async void tickEvent(object sender, EventArgs e)
        {
            getActiveGameWindow();

            if (windowTitle == programConfig.gameTitle)
            {
                if (gameWindowFound == false)
                {
                    gameWindowFound = true;
                    windowStatusText.Text = "Game Window Found!";
                    previewScreenButton.Enabled = true;
                    screenXTextEdit.Enabled = true;
                    screenYTextEdit.Enabled = true;
                    screenHeightTextEdit.Enabled = true;
                    screenWidthTextEdit.Enabled = true;
                }

                String extractedText = await skillOcr.ExtractTextAsync(activeGameBitmap);

                List<String> extractedTextLines = extractedText.Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.None
                ).ToList();

                skillListTextBox.Text = skillFinder.getAllDetectedSkillList(extractedTextLines, allSkills, allBannedTexts);
            }

            disposeBitmaps();
        }

        private void disposeBitmaps()
        {
            if (activeGameBitmap != null)
            {
                activeGameBitmap.Dispose();
            }
        }

        public void getActiveGameWindow()
        {
            IntPtr handler = WindowsUtilities.GetActiveWindowHandler();
            var targetTitle = WindowsUtilities.GetWindowTitle(handler)?.Trim();
            Rectangle rect = WindowsUtilities.GetWindowArea(handler);

            windowTitle = targetTitle;

            if (targetTitle == programConfig.gameTitle)
            {
                gameWindowX = rect.X;
                gameWindowY = rect.Y;
            }

            if (gameWindowX != -1 && gameWindowY != -1)
            {
                using(Bitmap bitmap = WindowsUtilities.GetActiveGameBitmap(gameWindowX, gameWindowY, programConfig))
                {
                    System.Drawing.Size newSize = new System.Drawing.Size((bitmap.Width * 3), (bitmap.Height * 3));

                    Bitmap enhancedBitmap = new Bitmap(bitmap, newSize);
                    activeGameBitmap = enhancedBitmap;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {;
        }

        private void skillListTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void previewScreenButton_Click(object sender, EventArgs e)
        {
            Bitmap previewGameBitmap = WindowsUtilities.GetActiveGameBitmap(gameWindowX, gameWindowY, programConfig);

            if (previewGameBitmap != null)
            {
                bitmapPreview preview = new bitmapPreview(previewGameBitmap);
                preview.Show();
            }
        }

        private void screenXTextEdit_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(screenXTextEdit.Text))
            {
                programConfig.screenX = int.Parse(screenXTextEdit.Text);
                getActiveGameWindow();
                programConfig.saveProgramConfig();
            }
            
        }

        private void screenYTextEdit_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(screenYTextEdit.Text))
            {
                programConfig.screenY = int.Parse(screenYTextEdit.Text);
                getActiveGameWindow();
                programConfig.saveProgramConfig();
            }
                
        }

        private void screenWidthTextEdit_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(screenWidthTextEdit.Text))
            {
                programConfig.screenWidth = int.Parse(screenWidthTextEdit.Text);
                getActiveGameWindow();
                programConfig.saveProgramConfig();
            }
                
        }

        private void screenHeightTextEdit_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(screenHeightTextEdit.Text))
            {
                programConfig.screenHeight = int.Parse(screenHeightTextEdit.Text);
                getActiveGameWindow();
                programConfig.saveProgramConfig();
            }
                
        }

        private void loadConfigButton_Click(object sender, EventArgs e)
        {
            loadConfig();
            configText.Text = "Config re-loaded!";
        }

        private void languageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            programConfig.language = languageBox.Text;
            allSkills = skillFinder.getAllSkills(programConfig);
            programConfig.saveProgramConfig();
        }
    }
}
