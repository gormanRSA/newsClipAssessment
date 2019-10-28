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
//
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Question2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string ConString;


        DataTable userTable;
        DataTable branchTable;
        DataTable shiftTable;

        
        enum mode
        {
            NONE,
            ADD,
            EDIT,
            DELETE
        }

        enum tabs
        {
            MAIN,
            USERS,
            BRANCHES,
            SHIFTS
        }

        private tabs currentTab;
        private mode currentMode;

        struct rowStruct{
            int id;
            string Username, Fullname, Branch, Shift;
        }

        

        public MainWindow()
        {
            InitializeComponent();
            setConnection();
            fillGrids();
        }



        /*Helper Functions*/

        //Setup Connection string with relative path to DB
        private void setConnection()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            ConString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\newsclipDB.mdf;Integrated Security=True";
        }

        //Fill all the grids with the database entries
        private void fillGrids()
        {
            string CmdString = string.Empty;
            //Fill Users table
            using (SqlConnection con = new SqlConnection(ConString))
            {
                //Fill Users dataGrid
                CmdString = "EXEC dbo.allbutID";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                userTable = new DataTable("Employee");
                sda.Fill(userTable);
                dgUser.ItemsSource = userTable.DefaultView;
                

                //Fill Branches dataGrid
                CmdString = "EXEC dbo.spBranches";
                cmd = new SqlCommand(CmdString, con);
                sda = new SqlDataAdapter(cmd);
                branchTable = new DataTable("Branches");
                sda.Fill(branchTable);
                dgBranches.ItemsSource = branchTable.DefaultView;

                //Fill Shifts dataGrid
                CmdString = "EXEC dbo.spShifts";
                cmd = new SqlCommand(CmdString, con);
                sda = new SqlDataAdapter(cmd);
                shiftTable = new DataTable("Shifts");
                sda.Fill(shiftTable);
                dgShift.ItemsSource = shiftTable.DefaultView;
            }
        }

        //Enable/Disable the buttons on the right
        private void toggleRightButtons(bool isEnabled)
        {
            addButton.IsEnabled = isEnabled;
            editButton.IsEnabled = isEnabled;
            deleteButton.IsEnabled = isEnabled;
        }

        //Check that all textboxes are filled in
        private bool textFilled()
        {
            if((txtUserName.Text.Length == 0)||(txtName.Text.Length == 0)||(txtBranch.Text.Length == 0)||(txtShift.Text.Length == 0))
            {
                return false;
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                return true;
            }
        }

        //Fill textBoxes with text
        private void filltextBox(String Username, String Name, String Branch, String Shift)
        {
            txtUserName.Text = Username;
            txtName.Text = Name;
            txtBranch.Text = Branch;
            txtShift.Text = Shift;
        }

        //Saved procedure command execute
        private void spDeleteCommand(int ID)
        {
            try
            {
                using (var sc = new SqlConnection(ConString))
                using (var cmd = sc.CreateCommand())
                {
                    sc.Open();
                    cmd.CommandText = "EXEC spDelete";
                    cmd.Parameters.AddWithValue("@Id", ID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("SQL error" + e);
            }
        }





        /*Events*/

        //When tabs are changed...
        private void MainTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (mainTabs.SelectedIndex)
            {
                case 0:
                    currentTab = tabs.MAIN;
                    break;
                case 1:
                    {
                        currentTab = tabs.USERS;
                        pnlRightBtns.IsEnabled = true;
                        break;
                    }
                    
                case 2:
                    {
                        currentTab = tabs.BRANCHES;
                        pnlRightBtns.IsEnabled = true;
                        break;
                    }
                case 3:
                    {
                        currentTab = tabs.SHIFTS;
                        pnlRightBtns.IsEnabled = true;
                        break;
                    }
            }
        }
        //Add button pressed..
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            leftPanel.IsEnabled = true;
            toggleRightButtons(false);
            currentMode = mode.ADD;
            lblMode.Content = "Mode: " + currentMode.ToString();
        }
        //Cancel button pressed..
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            currentMode = mode.NONE;
            lblMode.Content = "";
            lblID.Content = "";
            filltextBox("", "", "", "");
            leftPanel.IsEnabled = false;
            toggleRightButtons(true);
        }
        //When maintab is loaded
        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            pnlRightBtns.IsEnabled = false;
            leftPanel.IsEnabled = false;
        }

        

        //Delete button pressed
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    
}
