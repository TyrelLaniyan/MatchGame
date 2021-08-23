using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {//these three lines of code are added to create a new timer and add two fields(varible) to keep track of the time elapsed and number of matches the player found
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElasped;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }
        //the timer tick methods updates the newTextBlock with the elasped time and stops the timer once the player has found all of the matches
        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElasped++;
            timeTextBlock.Text = (tenthsOfSecondsElasped / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }
        private void SetUpGame()
        {
            // we've created 8 pairs of animal emojis 
            List<string> animalEmoji = new List<string>()
            {
                "🐵", "🐵",
                "🐶", "🐶",
                "🦊", "🦊",
                "🐱", "🐱",
                "🦁", "🦁",
                "🐯", "🐯",
                "🐹", "🐹",
                "🐼", "🐼",

            };
            //Randomly assign the emojis to the textblock 
            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())//Find every textblock in the main grid and repeat the following statements for each of them
            {//
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);//Pick a random number between 0 and the number of emoji left in the list and call it index
                    string nextEmoji = animalEmoji[index];//Use the random number called "index" to get a random emoji from the list
                    textBlock.Text = nextEmoji;//Update the TextBlock with the random emoji from the list
                    animalEmoji.RemoveAt(index);//Remove the random emoji from the list
                }
            }

            timer.Start();
            tenthsOfSecondsElasped = 0;
            matchesFound = 0;

        }
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {//what this does is set finding match as false
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch == false)
            {//because finding match is full it comes here
                //it first makes the emoji you clicked invisible
                textBlock.Visibility = Visibility.Hidden;
                //This makes the emoji you just clicked = to textblock
                lastTextBlockClicked = textBlock;
                //it then sets finding match as true
                findingMatch = true;
            }
            //The finding match is set as true so it comes here if the emoji you clicked is the same as the emoji you just clicked 
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                //this then makes them invisible
                textBlock.Visibility = Visibility.Hidden;
                //this sets finding match to false again so the loop can start again
                findingMatch = false;
            }
            else
            {//this makes both object you click visible because the emoji you clicked isnt the same to the 1 u just clicked 
                lastTextBlockClicked.Visibility = Visibility.Visible;
                //it then sets it as false so the loop can start again
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchesFound == 8)//This resets the game if all 8 matched pairs have been found 
            {
                SetUpGame(); 
            }

        }
    }
}
           
                
            
            



