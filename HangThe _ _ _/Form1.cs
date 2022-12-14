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



namespace HangThe______
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        const string path = "words.txt";
        string randomWord = "";
        int letters = 0;
        int score = 0;
        int pass = 0;
        int error = 0;
        



        public Form1()
        {

            InitializeComponent();

            this.Height =720;
            this.Width = 1280;
            LblIncorrectChar.Text ="";
            LblIncorrectLetters.Text ="";
            GenerateNewWord();
            error = 0;

        }

       


        private void NumberBTN(object sender, EventArgs e)
        {
            Button button = sender as Button;


            button.Enabled = false;

            List<Label> placeHoldersToChange = new List<Label>(); //this stores all the placeHolders that need to be removed before a new word is generated
            foreach (Label label in Controls.OfType<Label>()) //This foreach loop identifies all labels that do not have empty tags and those whose tags are "placeHolder"
            {
                if (label.Tag != null && label.Tag.ToString() == "placeHolder")
                {
                    placeHoldersToChange.Add(label);
                }
            }

            for (int i = 0; i < randomWord.Length; i++)
            {
                if (randomWord[i] == Convert.ToChar(button.Text.ToLower()))
                {
                    pass = 1;
                    placeHoldersToChange[i].Text = Convert.ToString(button.Text);
                    if (placeHoldersToChange[i].Text != "--")
                    { 
                        letters++;
                    }
                    if (letters == randomWord.Length)
                    {
                        score++;
                        TxtDebug2.Text = Convert.ToString(score);
                        if (cbNewWord.Checked == true)
                        {
                            GenerateNewWord();
                            error = 0;
                            LblIncorrectChar.Text ="";
                            LblIncorrectLetters.Text ="";
                           
                        }
                    }
                }
            }
            if (error>=0)
            {
                LblIncorrectLetters.Text = "Incorrect Letters";
              
            }
            
            if (pass == 0 && error<= 7)
            {
                error++;

                LblIncorrectChar.Text = LblIncorrectChar.Text + Convert.ToString(button.Text);
                if (error ==1)
                {
                    PicBase.Visible = false;
                }
                if (error ==2)
                {
                    picPole.Visible = false;
                }
                if (error==3)
                {
                    picHanger.Visible = false;
                }
                if (error==4)
                {
                    picRope.Visible = false;
                }
                if (error==5)
                {
                    picHead.Visible = false;
                }
                if (error==6)
                {
                    PicArms.Visible = false;
                }
                if (error==7)
                {
                    PicLegs.Visible = false;
                }
            }
            if(error == 7)
            {
                TxtDebug.Text = "fail";
                error = 0;
                DisableLetters();
            }
            pass = 0;
        }

        private void btnGetNewWord_Click(object sender, EventArgs e)
        {
            GenerateNewWord();
            LblIncorrectChar.Text ="";
            LblIncorrectLetters.Text ="";
            error = 0;
          
           

           

          
        }
        private void RemoveAllPlaceHolders()
        {
            List<Label> placeHoldersToRemove = new List<Label>(); //this stores all the placeHolders that need to be removed before a new word is generated
            foreach (Label label in Controls.OfType<Label>()) //This foreach loop identifies all labels that do not have empty tags and those whose tags are "placeHolder"
            {
                if (label.Tag != null && label.Tag.ToString() == "placeHolder")
                {
                    placeHoldersToRemove.Add(label);

                }
            }
            foreach (Label label in placeHoldersToRemove) //this foreach loop goes through each label in the placeholderstoremove list and removes them
            {
                label.Dispose();
            }
        }
        private void ReadTextFile(List<String> words)
        {
            StreamReader sr = new StreamReader(path); //creates a streamreader used to read the words from the external file (path is the name of the text file)
            string line = ""; //this variable will temporarily store each read line from the text file.
            using (sr) //this is the streamreader which is used to place each word in the words list

            {
                while ((line = sr.ReadLine()) != null)
                {
                    byte[] data = Convert.FromBase64String(line);//Convert encoded text into a byte
                    string decodedString = Encoding.UTF8.GetString(data);//Convert the byte into a usable string
                    words.Add(decodedString);



                }

            }
        }

        private void SelectRandomWord(List<String> words)
        {
            Random random = new Random(); //random number generator
            int randomIndex = random.Next(words.Count); //used to generate a random integer that can be used to select a random word from the list "words"
            randomWord = words[randomIndex]; // the randomWord selected from the word list using the randomIndex variable

        }
        private void GenerateLabelsForRandomWord()
        {
            Label[] labels = new Label[randomWord.Length]; // Array of Labels of length of random word is declared.
            int xPos = 420; //This is the initial x position of the first placeholder on the form
            for (int i = 0; i < randomWord.Length; i++) //this for loop runs for the number of characters in the random word and creates a new label
            { //setting a bunch of properties including the important tag used to delete them when a new
                labels[i] = new Label(); //word is generated. each label represents a character in the randomword and is initially displayed
                labels[i].Text = "--"; // as two dashes
                labels[i].Location = new Point(xPos, 140);
                labels[i].Enabled = true;
                labels[i].Visible = true;
                labels[i].Width = 30;
                labels[i].Height = 12;
                labels[i].Tag = "placeHolder";
                xPos += 30;




            }
            for (int i = 0; i < randomWord.Length; i++) //this for loop adds each label to the controls collection of the winform
            {


                this.Controls.Add(labels[i]);

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        void GenerateNewWord()
        {
            List<string> words = new List<string>(); // creates a list to store all of the words in the text file
            EnableAllLetters();
            RemoveAllPlaceHolders();
            ReadTextFile(words);
            SelectRandomWord(words);
            GenerateLabelsForRandomWord();
            HideHangman();
           
            letters = 0;

        }

        void EnableAllLetters()
        {
            btnA.Enabled = true;
            btnB.Enabled = true;
            btnC.Enabled = true;
            btnD.Enabled = true;
            btnE.Enabled = true;
            btnF.Enabled = true;
            btnG.Enabled = true;
            btnH.Enabled = true;
            btnI.Enabled = true;
            btnJ.Enabled = true;
            btnK.Enabled = true;
            btnL.Enabled = true;
            btnM.Enabled = true;
            btnN.Enabled = true;
            btnO.Enabled = true;
            btnP.Enabled = true;
            btnQ.Enabled = true;
            btnR.Enabled = true;
            btnS.Enabled = true;
            btnT.Enabled = true;
            btnU.Enabled = true;
            btnV.Enabled = true;
            btnW.Enabled = true;
            btnX.Enabled = true;
            btnY.Enabled = true;
            btnZ.Enabled = true;
        }
        void DisableLetters()
        {
            btnA.Enabled = false;
            btnB.Enabled = false;
            btnC.Enabled = false;
            btnD.Enabled = false;
            btnE.Enabled = false;
            btnF.Enabled = false;
            btnG.Enabled = false;
            btnH.Enabled = false;
            btnI.Enabled = false;
            btnJ.Enabled = false;
            btnK.Enabled = false;
            btnL.Enabled = false;
            btnM.Enabled = false;
            btnN.Enabled = false;
            btnO.Enabled = false;
            btnP.Enabled = false;
            btnQ.Enabled = false;
            btnR.Enabled = false;
            btnS.Enabled = false;
            btnT.Enabled = false;
            btnU.Enabled = false;
            btnV.Enabled = false;
            btnW.Enabled = false;
            btnX.Enabled = false;
            btnY.Enabled = false;
            btnZ.Enabled = false;
        }
        private void CheckKey(object sender, EventArgs e)
        {

            btnA.Enabled = false;


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        void HideHangman()
        {
            PicBase.Visible = true;
            picPole.Visible = true;
            picHanger.Visible = true;
            picRope.Visible = true;
            picHead.Visible = true;
            PicArms.Visible = true;
            PicLegs.Visible = true;
        }
    }
}