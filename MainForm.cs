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
        private Translator ocrTranslator;

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

            ocrTranslator = new Translator(programConfig.translationApiKey);

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
            checkBoxChoiceMode.Checked = false;

            if (programConfig.translatorMode)
            {
                translatorCheckBox.Checked = true;
                languageBox.Enabled = false;
                buttonTranslate.Enabled = true;
                skillListTextBox.Visible = false;
                textBoxOcr.Visible = true;
                textBoxTranslation.Visible = true;
                checkBoxChoiceMode.Enabled = true;

                if (programConfig.translatorChoiceMode)
                {
                    checkBoxChoiceMode.Checked = true;

                    screenXTextEdit.Text = programConfig.translatorChoiceScreenX.ToString();
                    screenYTextEdit.Text = programConfig.translatorChoiceScreenY.ToString();
                    screenHeightTextEdit.Text = programConfig.translatorChoiceScreenHeight.ToString();
                    screenWidthTextEdit.Text = programConfig.translatorChoiceScreenWidth.ToString();
                }
                else
                {
                    screenXTextEdit.Text = programConfig.translatorScreenX.ToString();
                    screenYTextEdit.Text = programConfig.translatorScreenY.ToString();
                    screenHeightTextEdit.Text = programConfig.translatorScreenHeight.ToString();
                    screenWidthTextEdit.Text = programConfig.translatorScreenWidth.ToString();
                }

                
            }
            else
            {
                translatorCheckBox.Checked = false;
                languageBox.Enabled = true;
                buttonTranslate.Enabled = false;
                skillListTextBox.Visible = true;
                textBoxOcr.Visible = false;
                textBoxTranslation.Visible = false;
                checkBoxChoiceMode.Enabled = false;
                screenXTextEdit.Text = programConfig.screenX.ToString();
                screenYTextEdit.Text = programConfig.screenY.ToString();
                screenHeightTextEdit.Text = programConfig.screenHeight.ToString();
                screenWidthTextEdit.Text = programConfig.screenWidth.ToString();
            }

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

                if (!programConfig.translatorMode)
                {
                    List<String> extractedTextLines = extractedText.Split(
                        new[] { Environment.NewLine },
                        StringSplitOptions.None
                    ).ToList();

                    skillListTextBox.Text = skillFinder.getAllDetectedSkillList(extractedTextLines, allSkills, allBannedTexts);
                }
                else
                {
                    String stringWithoutSpace = ocrTranslator.removeAllSpaceAndNewLine(extractedText);
                    textBoxOcr.Text = stringWithoutSpace;
                }
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
            string numberOnlyXText = WindowsUtilities.GetNumberOnlyString(screenXTextEdit.Text);

            if (!String.IsNullOrEmpty(numberOnlyXText))
            {
                if (programConfig.translatorMode)
                {
                    if(programConfig.translatorChoiceMode)
                    {
                        programConfig.translatorChoiceScreenX = int.Parse(numberOnlyXText);
                    }
                    else
                    {
                        programConfig.translatorScreenX = int.Parse(numberOnlyXText);
                    }
                }
                else
                {
                    programConfig.screenX = int.Parse(numberOnlyXText);
                }

                getActiveGameWindow();
                programConfig.saveProgramConfig();
            }
        }

        private void screenYTextEdit_TextChanged(object sender, EventArgs e)
        {
            string numberOnlyYText = WindowsUtilities.GetNumberOnlyString(screenYTextEdit.Text);

            if (!String.IsNullOrEmpty(numberOnlyYText))
            {
                if (programConfig.translatorMode)
                {
                    if (programConfig.translatorChoiceMode)
                    {
                        programConfig.translatorChoiceScreenY = int.Parse(numberOnlyYText);
                    }
                    else
                    {
                        programConfig.translatorScreenY = int.Parse(numberOnlyYText);
                    }

                    
                }
                else
                {
                    programConfig.screenY = int.Parse(numberOnlyYText);
                }
                
                getActiveGameWindow();
                programConfig.saveProgramConfig();
            }
        }

        private void screenWidthTextEdit_TextChanged(object sender, EventArgs e)
        {
            string numberOnlyWidthText = WindowsUtilities.GetNumberOnlyString(screenWidthTextEdit.Text);

            if (!String.IsNullOrEmpty(numberOnlyWidthText))
            {
                if (programConfig.translatorMode)
                {
                    if (programConfig.translatorChoiceMode)
                    {
                        programConfig.translatorChoiceScreenWidth = int.Parse(numberOnlyWidthText);
                    }
                    else
                    {
                        programConfig.translatorScreenWidth = int.Parse(numberOnlyWidthText);
                    }
                    
                }
                else
                {
                    programConfig.screenWidth = int.Parse(numberOnlyWidthText);
                }

                
                getActiveGameWindow();
                programConfig.saveProgramConfig();
            }
                
        }

        private void screenHeightTextEdit_TextChanged(object sender, EventArgs e)
        {
            string numberOnlyHeightText = WindowsUtilities.GetNumberOnlyString(screenHeightTextEdit.Text);

            if (!String.IsNullOrEmpty(numberOnlyHeightText))
            {
                if (programConfig.translatorMode)
                {
                    if (programConfig.translatorChoiceMode)
                    {
                        programConfig.translatorChoiceScreenHeight = int.Parse(numberOnlyHeightText);
                    }
                    else
                    {
                        programConfig.translatorScreenHeight = int.Parse(numberOnlyHeightText);
                    }
                    
                }
                else
                {
                    programConfig.screenHeight = int.Parse(numberOnlyHeightText);
                }

                getActiveGameWindow();
                programConfig.saveProgramConfig();
            }
                
        }

        private void screenXTextEdit_LostFocus(object sender, EventArgs e)
        {
            screenXTextEdit.Text = WindowsUtilities.GetNumberOnlyString(screenXTextEdit.Text);
        }

        private void screenYTextEdit_LostFocus(object sender, EventArgs e)
        {
            screenYTextEdit.Text = WindowsUtilities.GetNumberOnlyString(screenYTextEdit.Text);
        }

        private void screenWidthTextEdit_LostFocus(object sender, EventArgs e)
        {
            screenWidthTextEdit.Text = WindowsUtilities.GetNumberOnlyString(screenWidthTextEdit.Text);
        }

        private void screenHeightTextEdit_LostFocus(object sender, EventArgs e)
        {
            screenHeightTextEdit.Text = WindowsUtilities.GetNumberOnlyString(screenHeightTextEdit.Text);
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

        private void translatorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            programConfig.translatorMode = translatorCheckBox.Checked;

            if (programConfig.translatorMode)
            {
                if (programConfig.translatorChoiceMode)
                {
                    screenXTextEdit.Text = programConfig.translatorChoiceScreenX.ToString();
                    screenYTextEdit.Text = programConfig.translatorChoiceScreenY.ToString();
                    screenHeightTextEdit.Text = programConfig.translatorChoiceScreenHeight.ToString();
                    screenWidthTextEdit.Text = programConfig.translatorChoiceScreenWidth.ToString();
                }
                else
                {
                    screenXTextEdit.Text = programConfig.translatorScreenX.ToString();
                    screenYTextEdit.Text = programConfig.translatorScreenY.ToString();
                    screenHeightTextEdit.Text = programConfig.translatorScreenHeight.ToString();
                    screenWidthTextEdit.Text = programConfig.translatorScreenWidth.ToString();
                }

                languageBox.Enabled = false;
                buttonTranslate.Enabled = true;
                skillListTextBox.Visible = false;
                textBoxOcr.Visible = true;
                textBoxTranslation.Visible = true;
                checkBoxChoiceMode.Enabled = true;
            }
            else
            {
                screenXTextEdit.Text = programConfig.screenX.ToString();
                screenYTextEdit.Text = programConfig.screenY.ToString();
                screenHeightTextEdit.Text = programConfig.screenHeight.ToString();
                screenWidthTextEdit.Text = programConfig.screenWidth.ToString();

                languageBox.Enabled = true;
                buttonTranslate.Enabled = false;
                skillListTextBox.Visible = true;
                textBoxOcr.Visible = false;
                textBoxTranslation.Visible = false;
                checkBoxChoiceMode.Enabled = false;
            }

            programConfig.saveProgramConfig();
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            String translatedText = ocrTranslator.getTranslatedText(textBoxOcr.Text, programConfig.language);

            textBoxTranslation.Text = translatedText;
        }

        private void buttonCaptureScreen_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            programConfig.translatorChoiceMode = checkBoxChoiceMode.Checked;

            if (programConfig.translatorMode)
            {
                if (programConfig.translatorChoiceMode)
                {
                    screenXTextEdit.Text = programConfig.translatorChoiceScreenX.ToString();
                    screenYTextEdit.Text = programConfig.translatorChoiceScreenY.ToString();
                    screenHeightTextEdit.Text = programConfig.translatorChoiceScreenHeight.ToString();
                    screenWidthTextEdit.Text = programConfig.translatorChoiceScreenWidth.ToString();
                }
                else
                {
                    screenXTextEdit.Text = programConfig.translatorScreenX.ToString();
                    screenYTextEdit.Text = programConfig.translatorScreenY.ToString();
                    screenHeightTextEdit.Text = programConfig.translatorScreenHeight.ToString();
                    screenWidthTextEdit.Text = programConfig.translatorScreenWidth.ToString();
                }

                languageBox.Enabled = false;
                buttonTranslate.Enabled = true;
                skillListTextBox.Visible = false;
                textBoxOcr.Visible = true;
                textBoxTranslation.Visible = true;
                checkBoxChoiceMode.Enabled = true;
            }
            else
            {
                screenXTextEdit.Text = programConfig.screenX.ToString();
                screenYTextEdit.Text = programConfig.screenY.ToString();
                screenHeightTextEdit.Text = programConfig.screenHeight.ToString();
                screenWidthTextEdit.Text = programConfig.screenWidth.ToString();

                languageBox.Enabled = true;
                buttonTranslate.Enabled = false;
                skillListTextBox.Visible = true;
                textBoxOcr.Visible = false;
                textBoxTranslation.Visible = false;
                checkBoxChoiceMode.Enabled = false;
            }

            programConfig.saveProgramConfig();
        }
    }
}
