using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data.Common;
using System.Xml.Linq;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ------------------------------- EXAMPLE 0 ------------------------------- //

            con.Open();

            string sql = "Select EMPLOYEE_ID, LAST_NAME, FIRST_NAME from Employees Order by FIRST_NAME";

            // Создать объект Command.
            OracleCommand cmd = new OracleCommand();

            // Match Command с Connection.
            cmd.Connection = con;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        string emp_ID      = reader.GetString(reader.GetOrdinal("EMPLOYEE_ID"));
                        string empName     = reader.GetString(reader.GetOrdinal("FIRST_NAME"));
                        string empLastName = reader.GetString(reader.GetOrdinal("LAST_NAME"));
                        string fullName    = emp_ID + ") " + empName + " " + empLastName;

                        Console.WriteLine("-----------");
                        Console.WriteLine("EmpName:" + fullName);
                        textBox1.AppendText(fullName + Environment.NewLine);
                    }
                }
            }
            MessageBox.Show("Connceted Successful");
            con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void showEmployees_Click(object sender, EventArgs e)
        {

            // works with disconnected environment
            OracleDataAdapter adp = new OracleDataAdapter("Select * from employees", con);
            
            // this table stores records
            DataTable dt = new DataTable();

            adp.Fill(dt);
            dataGridView1.DataSource = dt.Select("")
                                          .Where(x => x.Field<int>("employee_id") < 110)
                                          .CopyToDataTable();

            // ------------------------------- EXAMPLE 1 ------------------------------- //

            // LINQ query on a DataTable
            var result = dt.Select("")
                           .FirstOrDefault(myRow => (int)myRow["employee_id"] == 110);

            MessageBox.Show(result["last_name"] + " " + result["first_name"].ToString()); // here can access from any column

            // ------------------------------- EXAMPLE 2 ------------------------------- //
            var result2 = dt.AsEnumerable()
                            .Where(myRow => myRow.Field<int>("employee_id") == 100);

            if (result2 != null)
            {
                foreach (var res in result2)
                {
                    MessageBox.Show(res["employee_id"].ToString());
                }
            }

            // ------------------------------- EXAMPLE 3 ------------------------------- //
            var result3 = dt.AsEnumerable()
                            .Where(x => x.Field<string>("last_name") == "Chen");
            if (result3 != null)
            {
                foreach (var res in result2)
                {
                    MessageBox.Show(res["employee_id"] + " " + res["last_name"]);
                }
            }

            // ------------------------------- EXAMPLE 4 ------------------------------- //
            var result4 = dt.AsEnumerable().ToList();
            IEnumerable<DataRow> query = from emp in result4 where emp.Field<string>("last_name")
                                                                      .Contains("Chen") select emp;
            foreach (DataRow row in query) { 
                Console.WriteLine(row[0].ToString());
            }


            DataTable dt2 = dt.Select("")
                              .Where(x => x.Field<int>("employee_id") < 110)
                              .OrderByDescending(x => x.Field<int>("employee_id"))
                              .CopyToDataTable();

            dataGridView2.DataSource = dt2;


            // ------------------------------- EXAMPLE 5 ------------------------------- //
            Console.WriteLine("== Some 5  query == ");


            string[] selectedColumns = new[] { "first_name", "last_name" };
            DataTable dt3 = new DataView(dt).ToTable(false, selectedColumns);

            var result5 = dt3.Select("");

            if (result5 != null)
            {
                foreach (DataRow row in result5)
                {
                    Console.WriteLine(row[0] + " " + row.Field<string>("last_name").ToString());
                }
            }
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GenerateXML_Click(object sender, EventArgs e)
        {

            // XML File Path
            const string FILE_PATH = "C:\\Users\\Usernamers\\Desktop\\tansh\\foo3.xml";

            // works with disconnected environment
            OracleDataAdapter adp = new OracleDataAdapter("select * from departments", con);

            // this table stores records
            DataTable dt = new DataTable();
            adp.Fill(dt);

            string[] selectedColumns = new[] { "department_id", "department_name" };

            DataTable ToXML_DataTable = new DataView(dt)
                                    .ToTable(false, selectedColumns)
                                    .Select("")
                                    .OrderByDescending(x => x.Field<string>("department_name"))
                                    .Where(x => x.Field<string>("department_name").Contains("A"))
                                    .CopyToDataTable();

            ToXML_DataTable.TableName = "DEPARTMENT";    

            new XDocument( new XElement("root") ).Save(FILE_PATH);  // create if not exist

            DataSet ds = new DataSet();
            ds.Tables.Add(ToXML_DataTable);

            string dsXml = ds.GetXml();       // get XML from data set

            using (System.IO.StreamWriter fs = new System.IO.StreamWriter(FILE_PATH)) 
            {
                ds.WriteXml(fs);
            }

            MessageBox.Show("XML File generated successful!" + Environment.NewLine + "On the file Path: " + FILE_PATH);

        }
    }
}
