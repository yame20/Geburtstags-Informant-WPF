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
using System.Windows.Shapes;

namespace Geburtstags_Informant_WPF
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button_createProfile_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBox_firstName.Text) || string.IsNullOrEmpty(txtBox_lastName.Text))
            {
                MessageBox.Show("Bitte gebe einen Vor- und Nachnamen ein!");
            }
            else if (!DateTime.TryParse(txtBox_birthDate.Text, out DateTime birthDate))
            {
                MessageBox.Show("Inkorrektes Datum! \n" +
                                "Bitte gebe ein korrektes Datum ein. (TT.MM.JJJJ)");
            }
            else
            {
                ProfileManager.CreateProfile(txtBox_firstName.Text, txtBox_lastName.Text, Convert.ToDateTime(txtBox_birthDate.Text));
                ProfileManager.AllProfiles.Add(ProfileManager.CurrentProfile);
                Close();
            }
        }
    }
}
