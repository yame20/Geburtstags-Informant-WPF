using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Xml;

namespace Geburtstags_Informant_WPF
{
    static class ProfileManager
    {
        public static Profile CurrentProfile { get; set; }
        public static ObservableCollection<Profile> AllProfiles { get; set; } = new ObservableCollection<Profile>();

        public static void CreateProfile(string firstName, string lastName, DateTime birthDate)
        {
            CurrentProfile = new Profile(firstName, lastName, birthDate);
            SaveProfile(CurrentProfile);
        }

        public static void CheckAllExistingProfiles()
        {
            foreach (string file in Directory.GetFiles(AppContext.BaseDirectory, "*.prof"))
            {
                LoadProfile(file);
                AllProfiles.Add(CurrentProfile);
            }
            CurrentProfile = null;
        }

        public static bool CheckIfProfileExist(string firstName, string lastName)
        {
            string filePath = AppContext.BaseDirectory + firstName + " " + lastName + ".prof";
            return File.Exists(filePath);
        }

        public static void SaveProfile(Profile profil)
        {
            string filePath = AppContext.BaseDirectory + profil.FirstName + " " + profil.LastName + ".prof";
            DataContractSerializer serializer = new DataContractSerializer(typeof(Profile));
            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    using (XmlWriter writer = XmlWriter.Create(stream, settings))
                    {
                        serializer.WriteObject(writer, profil);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void LoadProfile(string profilePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(Profile));
            
            try
            {
                using (FileStream stream = new FileStream(profilePath, FileMode.Open))
                {
                    CurrentProfile = (Profile)serializer.ReadObject(stream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void DeleteProfile(Profile profile)
        {
            if (profile != null)
            {
                string profilePath = AppContext.BaseDirectory + profile.FirstName + " " + profile.LastName + ".prof";
                AllProfiles.Remove(profile);
                File.Delete(profilePath);
            }
        }

        public static int ShowAgeInYear()
        {
            int age = DateTime.Now.Year - ProfileManager.CurrentProfile.Birthdate.Year;
            if (ProfileManager.CurrentProfile.Birthdate.Month > DateTime.Now.Month ||
                DateTime.Now.Month == ProfileManager.CurrentProfile.Birthdate.Month &&
                DateTime.Now.Day < ProfileManager.CurrentProfile.Birthdate.Day)
            {
                age--;
            }
            return age;
        }
    }
}
