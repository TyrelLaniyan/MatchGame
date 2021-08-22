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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
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
            {
                int index = random.Next(animalEmoji.Count);//Pick a random number between 0 and the number of emoji left in the list and call it index
                string nextEmoji = animalEmoji[index];//Use the random number called "index" to get a random emoji from the list
                textBlock.Text = nextEmoji;//Update the TextBlock with the random emoji from the list
                animalEmoji.RemoveAt(index);//Remove the random emoji from the list
            }

            
        }
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}
           
                
            
            



