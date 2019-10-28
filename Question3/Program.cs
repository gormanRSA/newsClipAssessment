using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Question3
{
    class Program
    {
        private static string ConString;

        private static String[] strDetails = new string[2];

        static void Main(string[] args)
        {
            setConnection();

            Console.WriteLine("Please select the branch:\n1.Johannesburg\n2.Cape Town\n3.Bloemfontein");

            setBranch(Console.ReadLine());

            Console.WriteLine("Please select the shift:\n1.Day\n2.Night");

            setShift(Console.ReadLine());


            getData(strDetails[0],strDetails[1]);

            Console.ReadLine();



        }

        private static void setConnection()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            ConString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\newsclipDB.mdf;Integrated Security=True";
        }

        private static void getData(String strBranch, String strShift)
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {
                String CmdString = "EXEC dbo.spgetBranchShift '"+strBranch+"', '"+strShift+"'";
                con.Open();
                SqlCommand cmd = new SqlCommand(CmdString, con);
                //cmd.Parameters.Add(new SqlParameter("Branch", "Johannesburg"));
                //cmd.Parameters.Add(new SqlParameter("Shift", "Day"));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    Console.WriteLine();
                    Console.WriteLine("SQL Output:");
                    Console.WriteLine();
                    Console.WriteLine("UserName\tFullName\tBranch\tShift");
                    Console.WriteLine("=========================================================");

                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}",
                            reader[0], reader[1], reader[2], reader[3]));
                    }
                }

            }

            
        }

        private static void setBranch(string response)
        {
            switch (response)
            {
                case "1":
                    strDetails[0] = "Johannesburg";
                    break;
                case "2":
                    strDetails[0] = "Cape Town";
                    break;
                case "3":
                    strDetails[0] = "Bloemfontein";
                    break;
                default:
                    Console.WriteLine("Wrong selection made, try again");
                    break;
            }
        }

        private static void setShift(string response)
        {
            switch (response)
            {
                case "1":
                    strDetails[1] = "Day";
                    break;
                case "2":
                    strDetails[1] = "Night";
                    break;
                default:
                    Console.WriteLine("Wrong selection made, try again");
                    break;
            }
        }
    }
}
