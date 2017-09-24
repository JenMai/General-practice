using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace cardregister
{
    /*
     * WIP -- need to fix path issues when opening db file
     * (among other things)
     */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SQLiteConnection cdbConnect;
            cdbConnect = new SQLiteConnection(@"Data Source = **ABSOLUTE PATH**\cards.db; UseUTF16Encoding=True;");
            cdbConnect.Open();

            string sql = "SELECT * FROM cuscountry";
            SQLiteCommand command = new SQLiteCommand(sql, cdbConnect);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                comboCountry.Items.Add(reader["country"]);
            }
        }
    }
}