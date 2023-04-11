using System;
using System.Collections.Generic;
using System.IO;
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

namespace Geburtstags_Informant_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ProfileManager.CheckAllExistingProfiles();
            InitializeComponent();         
        }

        private void button_createNewProfile_Click(object sender, RoutedEventArgs e)
        {
            Window createProfileWindow = new Window1();
            createProfileWindow.Show();
        }

        private void button_deleteProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileManager.DeleteProfile(ProfileManager.CurrentProfile);
            ClearProfileRelatedData();
        }

        private void listBox_profileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProfileManager.CurrentProfile = (Profile)listBox_profileList.SelectedItem;
            FillProfileDetails();
            LoadLists();
        }

        private void FillProfileDetails()
        {
            lbl_profileFullName.Content = "Name: ";
            lbl_profileFullName_Content.Content = ProfileManager.CurrentProfile.FirstName + " " + ProfileManager.CurrentProfile.LastName;
            lbl_profileBirthdate.Content = "Geburtstag: ";
            lbl_profileBirthdate_Content.Content = ProfileManager.CurrentProfile.Birthdate.ToShortDateString();
            int age = ProfileManager.ShowAgeInYear();
            lbl_profileAge.Content = "Alter: ";
            lbl_profileAge_Content.Content = age;
        }

        private void ClearProfileRelatedData()
        {
            //Profile-PropertyLists
            listBox_plantList.ItemsSource = null;
            listBox_giftList.ItemsSource = null;
            //WPF ProfileDetails
            lbl_profileFullName.Content = null;
            lbl_profileFullName_Content.Content = null;
            lbl_profileBirthdate.Content = null;
            lbl_profileBirthdate_Content.Content = null;
            lbl_profileAge.Content = null;
            lbl_profileAge_Content.Content = null;
        }

        private void LoadLists()
        {
            listBox_plantList.ItemsSource = ProfileManager.CurrentProfile.PlantList;
            listBox_giftList.ItemsSource = from item in ProfileManager.CurrentProfile.GiftList
                                           orderby item.Year
                                           select item;
        }

        private void button_plantBox_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileManager.CurrentProfile != null)
            {
                ProfileManager.CurrentProfile.PlantList.Add(new Plant(txtBox_plantBox.Text));
                ProfileManager.SaveProfile(ProfileManager.CurrentProfile);
                LoadLists();
                txtBox_plantBox.Text = null;
            }
        }

        private void button_giftBox_Click(object sender, RoutedEventArgs e)
        {
            if (ProfileManager.CurrentProfile != null)
            {
                if (!int.TryParse(txtBox_giftYearBox.Text, out int year))
                {
                    MessageBox.Show("Bitte gebe das Jahr in Zahlen ein!");
                }
                else
                {
                    ProfileManager.CurrentProfile.GiftList.Add(new Gift(txtBox_giftNameBox.Text, Convert.ToInt32(txtBox_giftYearBox.Text)));
                    ProfileManager.SaveProfile(ProfileManager.CurrentProfile);
                    LoadLists();
                    txtBox_giftNameBox.Text = null;
                    txtBox_giftYearBox.Text = null;
                }
            }
        }

        private void button_deletePlant_Click(object sender, RoutedEventArgs e)
        {
            ProfileManager.CurrentProfile.PlantList.Remove((Plant)listBox_plantList.SelectedItem);
            ProfileManager.SaveProfile(ProfileManager.CurrentProfile);
            LoadLists();
        }

        private void button_deleteGift_Click(object sender, RoutedEventArgs e)
        {
            ProfileManager.CurrentProfile.GiftList.Remove((Gift)listBox_giftList.SelectedItem);
            ProfileManager.SaveProfile(ProfileManager.CurrentProfile);
            LoadLists();
        }
    }
}
