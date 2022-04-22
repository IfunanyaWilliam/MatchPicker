using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
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

namespace MatchPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int timeElapsed;
        int matchesFound;
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            GameInit();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timeElapsed++;
            TimerTextBlock.Text = (timeElapsed / 10F).ToString("0.0s");
            if(matchesFound == 8)
            {
                timer.Stop();
                TimerTextBlock.Text = TimerTextBlock.Text + " Game is over, Play again";
            }
        }

        private void TimerTextBlock_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                GameInit();
            }
        }

        private void GameInit()
        {
            List<string> animalEmoji = new List<string>()
            {
                "🐙", "🐘", "🐘", "🐝", "🐙", "🐝",  "🐶", "🐶", "🦘", "🐜", "🐜", "🐢", "🐢", "🐫", "🐫", "🦘"
            };

            Random random = new Random();
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if(textBlock.Name != "TimerTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }

            //Start timer after displaying animal Emojis in grid
            timer.Start();
            timeElapsed = 0;
            matchesFound = 0;

        }

        private void AnimalClicked(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if(textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
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
