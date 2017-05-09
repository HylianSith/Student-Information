using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace StudentInfoProj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Person> people = new List<Person>();
        private void Form1_Load(object sender, EventArgs e) // upon starting the application, loads the file, or creates it if it doesnt exist
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            if (!Directory.Exists(path + "\\User Information"))
                Directory.CreateDirectory(path + "\\User Information");
            if (!File.Exists(path + "\\User Information\\information.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter(path + "\\User Information\\information.xml", Encoding.UTF8);
                xW.WriteStartElement("People");
                xW.WriteEndElement();
                xW.Close();
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\User Information\\information.xml");
            foreach (XmlNode xNode in xDoc.SelectNodes("People/Person"))
            {
                Person p = new Person();
                p.UserID = xNode.SelectSingleNode("UserID").InnerText;
                p.FirstName = xNode.SelectSingleNode("FirstName").InnerText;
                p.LastName = xNode.SelectSingleNode("LastName").InnerText;
                p.Major = xNode.SelectSingleNode("Major").InnerText;
                people.Add(p);
                listView1.Items.Add(p.UserID);
            }
        }

        private void button2_Click(object sender, EventArgs e) //adds a user from the info in the box
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\User Information\\information.xml");
            XmlNode xNode = xDoc.SelectSingleNode("People");
            xNode.RemoveAll();
            bool pass = true;
            int temp;
            Person p = new Person();
            int.TryParse(UserIDTextBox.Text, out temp);

            if (UserIDTextBox.TextLength != 7)
            {
                pass = false;
                DisplayError("ID - Must be 7 numerical characters.");
            }

            if (string.IsNullOrEmpty(FirstNameTextBox.Text) || !System.Text.RegularExpressions.Regex.IsMatch(FirstNameTextBox.Text, "^[a-zA-Z]"))
            {
                pass = false;
                DisplayError("First Name");
            }

            if (string.IsNullOrEmpty(LastNameTextBox.Text) || !System.Text.RegularExpressions.Regex.IsMatch(LastNameTextBox.Text, "^[a-zA-Z]"))
            {
                pass = false;
                DisplayError("Last Name");
            }

            if (string.IsNullOrEmpty(MajorTextBox.Text) || !System.Text.RegularExpressions.Regex.IsMatch(MajorTextBox.Text, "^[a-zA-Z]"))
            {
                pass = false;
                DisplayError("Major");
            }

            if (pass)
            {

                p.UserID = UserIDTextBox.Text;
                p.FirstName = FirstNameTextBox.Text;
                p.LastName = LastNameTextBox.Text;
                p.LastName = LastNameTextBox.Text;
                p.Major = MajorTextBox.Text;

                ;
                people.Add(p);
                listView1.Items.Add(p.UserID);
                UserIDTextBox.Text = "";
                FirstNameTextBox.Text = "";
                LastNameTextBox.Text = "";
                MajorTextBox.Text = "";

            }


        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Remove(listView1.SelectedItems[0].ToString());
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) //When you select someone in the listview, it brings up their information
        {
            if (listView1.SelectedItems.Count > 0)
            {
                UserIDTextBox.Text = people[listView1.SelectedItems[0].Index].UserID;
                FirstNameTextBox.Text = people[listView1.SelectedItems[0].Index].FirstName;
                LastNameTextBox.Text = people[listView1.SelectedItems[0].Index].LastName;
                MajorTextBox.Text = people[listView1.SelectedItems[0].Index].Major;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //XmlDocument xDoc = 
            Remove(listView1.SelectedItems.ToString());

        }

        void Remove(string pos) //Removes Item
        {

            try
            {
                people.RemoveAt(listView1.SelectedItems[0].Index);
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
            catch { }

        }

        void Deactive(string pos) //Adds to Deactive
        {
            try
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                listView2.Items.Add(listView1.SelectedItems[0]);
                Properties.Settings.Default.Save();
            }
            catch { }
        }

        void Active(string active) //Adds to Active
        {
            try
            {
                listView2.Items.Remove(listView2.SelectedItems[0]);
                listView1.Items.Add(listView2.SelectedItems[0]); 
            }
            catch { }
        }

        class Person
        {
            public string UserID
            {
                get;
                set;
            }

            public string FirstName
            {
                get;
                set;
            }

            public string LastName
            {
                get;
                set;
            }

            public string Major
            {
                get;
                set;
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path + "\\User Information\\information.xml");
                XmlNode xNode = xDoc.SelectSingleNode("People");
                xNode.RemoveAll();
                bool pass = true;
                int temp;
                int.TryParse(UserIDTextBox.Text, out temp);
                if (UserIDTextBox.TextLength != 7)
                {
                    DisplayError("ID");
                    pass = false;
                }
                else { people[listView1.SelectedItems[0].Index].UserID = UserIDTextBox.Text; }
                if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
                {
                    DisplayError("First Name");
                    pass = false;
                }
                else { people[listView1.SelectedItems[0].Index].FirstName = FirstNameTextBox.Text; }
                if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
                {
                    DisplayError("Last Name");
                    pass = false;
                }
                else { people[listView1.SelectedItems[0].Index].LastName = LastNameTextBox.Text; }
                if (string.IsNullOrWhiteSpace(MajorTextBox.Text))
                {
                    DisplayError("Major");
                    pass = false;
                }
                else { people[listView1.SelectedItems[0].Index].Major = MajorTextBox.Text; }

                listView1.SelectedItems[0].Text = UserIDTextBox.Text;
            }
            else if (listView2.SelectedItems.Count > 0)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path + "\\User Information\\information.xml");
                XmlNode xNode = xDoc.SelectSingleNode("People");
                xNode.RemoveAll();
                bool pass = true;
                int temp;
                int.TryParse(UserIDTextBox.Text, out temp);
                if (UserIDTextBox.TextLength != 7)
                {
                    DisplayError("ID");
                    pass = false;
                }
                else { people[listView2.SelectedItems[0].Index].UserID = UserIDTextBox.Text; }
                if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text))
                {
                    DisplayError("First Name");
                    pass = false;
                }
                else { people[listView2.SelectedItems[0].Index].FirstName = FirstNameTextBox.Text; }
                if (string.IsNullOrWhiteSpace(LastNameTextBox.Text))
                {
                    DisplayError("Last Name");
                    pass = false;
                }
                else { people[listView2.SelectedItems[0].Index].LastName = LastNameTextBox.Text; }
                if (string.IsNullOrWhiteSpace(MajorTextBox.Text))
                {
                    DisplayError("Major");
                    pass = false;
                }
                else { people[listView2.SelectedItems[0].Index].Major = MajorTextBox.Text; }

                listView2.SelectedItems[0].Text = UserIDTextBox.Text;
            }
            else
            {
                MessageBox.Show("Please select a User");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(path + "\\User Information\\information.xml");
            XmlNode xNode = xDoc.SelectSingleNode("People");
            xNode.RemoveAll();
            foreach (Person p in people)
            {
                XmlNode xTop = xDoc.CreateElement("Person");
                XmlNode xID = xDoc.CreateElement("UserID");
                XmlNode xFirst = xDoc.CreateElement("FirstName");
                XmlNode xLast = xDoc.CreateElement("LastName");
                XmlNode xMajor = xDoc.CreateElement("Major");
                xID.InnerText = p.UserID;
                xFirst.InnerText = p.FirstName;
                xLast.InnerText = p.LastName;
                xMajor.InnerText = p.Major;
                xTop.AppendChild(xID);
                xTop.AppendChild(xFirst);
                xTop.AppendChild(xLast);
                xTop.AppendChild(xMajor);
                xDoc.DocumentElement.AppendChild(xTop);
            }
            xDoc.Save(path + "\\User Information\\information.xml");

        }

        public void DisplayError(string item)
        {
            MessageBox.Show("Invalid" + item);
        }
        public static bool IsAllLetters(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c))
                    return false;
            }
            return true;
        }

        private void searchButton_Click(object sender, EventArgs e) //User presses the search button
            // searches the list for a user with the UserID that matches whats in the searchbox.
        {
            bool checkPass = false;
            int temp;
            int.TryParse(searchBox.Text, out temp);
            foreach (Person p in people)
            {
                
                if (p.UserID == searchBox.Text)
                {
                    int index = people.FindIndex(pi => pi.UserID.Equals(searchBox.Text));

                    if (listView1.SelectedIndices.Count > 0) //deselects everything
                    {
                        for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                        {
                            listView1.Items[listView1.SelectedIndices[i]].Selected = false;
                            
                        }
                        
                    }
                    // selects the item in the list with the ID that was searched
                    listView1.Focus();
                    listView1.Items[index].Selected = true;
                    checkPass = true;

                }
                
            }
            
            if (searchBox.TextLength != 7) 
            {   
                MessageBox.Show("Please enter valid UserID");
            }
            else if (!checkPass)
            {
                MessageBox.Show("UserID not found");
            }
            else if (checkPass) { }
            else { MessageBox.Show("You broke it"); }
        }

        private void button5_Click(object sender, EventArgs e) //Deactivate Button
        {
            while (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("You Must Select a Student to Deactivate");
                }
                else
                {
                    ListViewItem removeActive = listView1.SelectedItems[0];
                    ListViewItem addToDeactive = (ListViewItem)removeActive.Clone();
                    listView2.Items.Add(addToDeactive);
                    listView1.Items.Remove(removeActive);
                    Deactive(listView1.SelectedItems.ToString());
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //Activate Button
        {
            while (listView2.SelectedItems.Count > 0)
            {
                if (listView2.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("You Must Select a Student to Activate");
                }
                else
                {
                    ListViewItem removeDeactive = listView2.SelectedItems[0];  //removes selected item from Deactivated Students
                    ListViewItem addToActive = (ListViewItem)removeDeactive.Clone(); //Adds Selected Item to Active Students
                    listView1.Items.Add(addToActive);  //Adds to ActiveStudents
                    listView2.Items.Remove(removeDeactive);
                    Active(listView2.SelectedItems.ToString()); //Permanately Removes from Deactive
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        }
}
            
