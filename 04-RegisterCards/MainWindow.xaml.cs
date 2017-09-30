/* Registers an user and his/her graphic card into a database.
 * 
 * WIP -- fix path issues when opening db file
 *     -- fix SQL INSERT executing twice (or more)
 *     -- improve exception handling ?
 */

using System;
using System.Data.SQLite;
using System.Windows;

namespace cardregister
{

    public partial class MainWindow : Window
    {
        /*
         * SQLite variables.
         */
        SQLiteCommand command;
        SQLiteDataReader reader;
        SQLiteConnection cdbConnect;

        public MainWindow()
        {
            InitializeComponent();

            // TODO: solve relative/absolute path issue
            cdbConnect = new SQLiteConnection(@"Data Source = C:\INSERT_YOUR_PATH\cards.db; UseUTF16Encoding=True;");
            cdbConnect.Open();

            populateBox("cuscountry", "country", comboCountry);
            populateBox("cardseries", "series", comboSeries);

            comboSeries.SelectionChanged += comboSeries_SelectionChanged;
            button.Click += button_Click;
            Closing += MainWindow_Closing;
        }

        /*
         * Populates the 3rd combobox according to the 2nd.
         */
        private void comboSeries_SelectionChanged(object sender, EventArgs e)  // https://stackoverflow.com/questions/32088327/populate-combobox-depending-on-another-combobox
        {
            comboRef.Items.Clear();                                            // reset references for new selection
            int id = comboSeries.SelectedIndex + 1;                            // items start at 1 (no joke) in database
                                                                               // index is needed to retrieve specific references
            command = new SQLiteCommand();                                     // get proper references
            command.Connection = cdbConnect;
            command.CommandText = @"SELECT refname FROM cardref WHERE series = @id";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            reader = command.ExecuteReader();

            while (reader.Read())                                              // populate the 3rd combobox
            {
                comboRef.Items.Add(reader["refname"]);
            }
        }

        /*
         * Attempt from user to send form.
         */
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (checkForm() == false)
                return;

            sendData();
        }

        /*
         * Populates combobox.
         */
        private void populateBox(string table, string column, 
            System.Windows.Controls.ComboBox combobox)
        {
            string sql = "SELECT * FROM " + table;
            command = new SQLiteCommand(sql, cdbConnect);
            reader = command.ExecuteReader();

            while (reader.Read())                                               //add items to combobox according to
            {                                                                   // fetched items from db
                combobox.Items.Add(reader[column]);
            }
        }

        /*
         * Ensures proper usage. 
         */
        private bool checkForm()
        {
            if ((FName.Text == "") || (LName.Text == ""))                      // check textboxes
            {
                MessageBox.Show("Type in a full name.");
                return false;
            }

            if (comboCountry.SelectedItem == null)                             // check combobox
            {
                MessageBox.Show("Select a country.");
                return false;
            }

            if ((comboSeries.SelectedItem == null) ||                          // check combobox
                 (comboRef.SelectedItem == null))
            {
                MessageBox.Show("Register your graphic card's series AND reference.");
                return false;
            }

            return true;
        }

        /*
         * Sends data to database.
         */
        private void sendData()
        {
            command = new SQLiteCommand();                                    // retrieve selected reference in database
            command.Connection = cdbConnect;
            command.CommandText = @"SELECT refID FROM cardref WHERE refname = @id";
            command.Parameters.Add(new SQLiteParameter("@id", comboRef.SelectedItem));
            reader = command.ExecuteReader();

            int country = comboCountry.SelectedIndex + 1;                     // fix index
            int series = comboSeries.SelectedIndex + 1;

            try
            {
                while (reader.Read())                                         // cannot find a simpler way to get a single id?
                {
                    command = new SQLiteCommand(); 
                    command.Connection = cdbConnect;
                    command.CommandText = @"INSERT INTO customers (lastname, firstname, country, cseries, cref) VALUES (@lastname, @firstname, @country, @cseries, @cref)";

                    command.Parameters.Add(new SQLiteParameter("@lastname", LName.Text));
                    command.Parameters.Add(new SQLiteParameter("@firstname", FName.Text));
                    command.Parameters.Add(new SQLiteParameter("@country", country));
                    command.Parameters.Add(new SQLiteParameter("@cseries", series));
                    command.Parameters.Add(new SQLiteParameter("@cref", reader["refID"]));

                    int done = command.ExecuteNonQuery();

                    if (done == 1)                                            // if commit succeeded
                    {
                        MessageBox.Show("User successfully registered.");
                    }
                }

            }
            catch (Exception ex)                                              // if commit failed
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Dispose();
            }
            return;
        }

        /*
         * Closes database when program is closing.
         */
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cdbConnect.Close();
        }
    }
}
