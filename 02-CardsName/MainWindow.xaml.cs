using System;
using System.Windows;
using System.Windows.Input;

namespace CardValue
{
    /* 
     * Interactions to input a value and get a card.
     */
    public partial class MainWindow : Window
    {
        string[] suit = new string[] { "Clubs", "Spades", "Diamonds", "Hearts" };   // Cards and their ranks
        string[] rank = new string[] { "2", "3", "4", "5", "6", "7", "8",
                                       "9","10", "Jack", "Queen", "King", "Ace" };
        int input, suitVal, rankVal;

        public MainWindow()
        {
            /* 
             * Event handlers.
             */
            InitializeComponent();
            inputBox.Text = null;
            button.Click += click;
            inputBox.KeyDown += new KeyEventHandler(input_Key);
        }

        /* 
         * Key press event to get value.
         */
        private void input_Key(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                get_Value();
            }
        }

        /* 
         * Click event to get value.
         */
        private void click(object sender, RoutedEventArgs e)
        {
            get_Value();
        }

        /* 
         * Checks and converts value.
         */
        private void get_Value()
        {
            /* 
             * Ensure proper usage
             */
            if (inputBox.Text == "")
            {
                MessageBox.Show("Type in an integer value.");
                return;
            }
            else if (int.TryParse(inputBox.Text, out input) == false)           // if chars are alphabetic or special
            {                                                                   // source: https://stackoverflow.com/questions/15399323/validating-whether-a-textbox-contains-only-numbers
                MessageBox.Show("No alphabetic or special character allowed.");
                return;
            }

            int.TryParse(inputBox.Text, out input);

            if ((input < 0) || (input > 51))
            {
                MessageBox.Show("Integer must be between 0 and 51 inclusive.");
                return;
            }

            /* 
             * The actual conversion
             */

            suitVal = input / 13;                                              // get a value between 0 and 3 = the card suit
            rankVal = input % 13;                                              // get a value between 0 and 12 = the card rank

            if (rankVal < 12)
            {
                labelResult.Content = "You picked a " + rank[rankVal]
                                      + " of " + suit[suitVal] + ".";
                return;
            }
            if ((rankVal == 12) && (suitVal != 1))                             // handling grammar
            {
                labelResult.Content = "You picked an Ace of " + suit[suitVal] + ".";
                return;
            }
            else                                                               // kekeke
            {
                labelResult.Content = "I don't share your greed,\n " +
                                      "the only card I need is \n" +
                                      "the ACE OF SPAAAADES! \n" +
                                      "THE ACE OF SPAAAADES!";
                return;
            }

        }
    }
}