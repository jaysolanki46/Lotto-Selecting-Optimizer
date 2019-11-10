using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lotto_Selecting_Optimizer
{
    public partial class LottoInterface : Form
    {
        int counter = 0;
        ArrayList group = new ArrayList();
        int[] binary = new int[41];

        // Sql database connection
        string connetionString = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;
        SqlConnection cnn;
        SqlCommand cmd;
        SqlDataReader reader;
        String sql = "";
       
        public LottoInterface()
        {
            InitializeComponent();
        }

        // Generates customer interface 
        private void LottoInterface_Load(object sender, EventArgs e)
        {
            // Initialization of connection
            cnn = new SqlConnection(connetionString);
            LblTicketNumber.Text = generateTicketNumber();
            populateNumbers();

            // Reset binary group
            initializeBinaryValues();
        }

        // Customer choice click
        void b_Click(object sender, EventArgs e)
        {
            var b = sender as Button;

            if (b != null)
            {
                if(b.BackColor == Color.LightGray)
                {
                    if (counter < 6)
                    {
                        //MessageBox.Show(string.Format("{0} Clicked", b.Text));
                        b.BackColor = Color.CornflowerBlue;
                        counter++;

                        group.Add(b.Text);
                    }
                    else
                    {
                        MessageBox.Show("Pick only 6 numbers as a group", "Opps!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                } else if(b.BackColor == Color.CornflowerBlue)
                {
                    b.BackColor = Color.LightGray;
                    counter--;

                    group.Remove(b.Text);
                }
            }


            this.tableLayoutPanelGroup.Controls.Clear();

            var rowCount = 1;
            var columnCount = counter;

            this.tableLayoutPanelGroup.ColumnCount = columnCount;
            this.tableLayoutPanelGroup.RowCount = rowCount;
            tableLayoutPanelGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanelGroup.RowStyles.Add(new System.Windows.Forms.RowStyle());

            foreach (string val in group)
            {
                var finalButton = new Button();
                finalButton.Text = val.ToString();
                finalButton.Dock = DockStyle.Fill;
                finalButton.BackColor = Color.LightGray;
                this.tableLayoutPanelGroup.Controls.Add(finalButton);
            }

            if (counter == 6) { pictureBoxAlert.Visible = true; } else { pictureBoxAlert.Visible = false; }
        }

        // Customer buy
        private void BtnBuy_Click(object sender, EventArgs e)
        {
            string ticketNumber = "";
            string customerGroup = "";
            string binaryGroup = "";
            string binaryGroup40 = "";
            double decimalDigit = 0;

            if(counter == 6)
            {
                foreach (string str in group)
                {
                    // Convert to 40 digit binary
                    binaryGroup40 = toBinaryNumber40(str);

                    // Converts decimal to binary form by calling method toBinaryNumber
                    binaryGroup += toBinaryNumber(str); 

                    // Seperating customer choices by adding comma between decimals
                    customerGroup += str + ",";
                }

                // Convert into decimal digit
                decimalDigit = toDecimalDigit(binaryGroup40);

                // Formatting customer ticket number
                ticketNumber = LblTicketNumber.Text.Replace("-", "");

                // Removes comma from the end of customer group string
                customerGroup = customerGroup.TrimEnd(',');

                // Add customer numbers group in to database table Tickets
                try
                {
                    cnn.Open();
                    sql = "INSERT INTO Tickets values (@ticketNumber, @customerGroup, @binaryGroup, @binaryGroup40, @decimalDigit)";
                    cmd = new SqlCommand(sql, cnn);
                    cmd.Parameters.AddWithValue("@ticketNumber", ticketNumber);
                    cmd.Parameters.AddWithValue("@customerGroup", customerGroup);
                    cmd.Parameters.AddWithValue("@binaryGroup", binaryGroup);
                    cmd.Parameters.AddWithValue("@binaryGroup40", binaryGroup40);
                    cmd.Parameters.AddWithValue("@decimalDigit", decimalDigit);
                    cmd.ExecuteNonQuery();

                } catch(Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                } finally
                {
                    cnn.Close();
                }

                LottoThankYouPage thankYouPage = new LottoThankYouPage();
                thankYouPage.Show();
                this.Hide();

            } else
            {
                MessageBox.Show("Please select atleast 6 choices !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Populate all the numbers from 1 to 40
        private void populateNumbers()
        {
            var rowCount = 8;
            var columnCount = 5;

            this.tableLayoutPanelLottoBalls.ColumnCount = columnCount;
            this.tableLayoutPanelLottoBalls.RowCount = rowCount;

            this.tableLayoutPanelLottoBalls.ColumnStyles.Clear();
            this.tableLayoutPanelLottoBalls.RowStyles.Clear();

            for (int i = 0; i < columnCount; i++)
            {
                this.tableLayoutPanelLottoBalls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / columnCount));
            }
            for (int i = 0; i < rowCount; i++)
            {
                this.tableLayoutPanelLottoBalls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / rowCount));
            }

            for (int i = 0; i < rowCount * columnCount; i++)
            {
                var b = new Button();
                b.Text = (i + 1).ToString();
                b.Name = string.Format("b_{0}", i + 1);
                b.Click += b_Click;
                b.Dock = DockStyle.Fill;
                b.BackColor = Color.LightGray;
                this.tableLayoutPanelLottoBalls.Controls.Add(b);
            }
        }

        // Generates ticket number for new customer
        private string generateTicketNumber()
        {
            string ticketNumber = "";
            try
            {
                cnn.Open();
                sql = "SELECT TOP 1 TicketNumber FROM Tickets ORDER BY LottoId DESC";
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ticketNumber = (Convert.ToInt64(reader.GetValue(0).ToString()) + 1).ToString();
                }

                ticketNumber = ticketNumber.Insert(5, "-");

            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            } finally
            {
                cnn.Close();
            }            
            return ticketNumber;
        }

        // Convert decimal to binary form
        private string toBinaryNumber(string val)
        {
            string result = "";
            int number = Convert.ToInt32(val);
                      
            while (number > 1)
            {
                int remainder = number % 2;
                result = Convert.ToString(remainder) + result;
                number /= 2;
            }
            result = Convert.ToString(number) + result;

            return result;
        }

        // Convert numbers group to binary
        private string toBinaryNumber40(string val)
        {
           string result = "";

           for(int i = 1; i <= 40; i++)
            {
                if(i == Convert.ToInt16(val))
                {
                    binary[i] = 1;
                }
                result += binary[i].ToString();
            }

            return result;
        }

        // Convert binary to decimal
        private double toDecimalDigit(string binaryGroup40)
        {
            double digit = 0;
            int val = 0;

            for(int i = 0; i < binaryGroup40.Length; i++)
            {
                if(binaryGroup40[i].ToString() == "1")
                {
                    val = i + 1;
                    digit += Math.Pow(40, val);
                }
            }

            // Reset binary group
            initializeBinaryValues();

            return digit;
        }

        // Initialization of binary variable
        private void initializeBinaryValues()
        {
            for (int i = 1; i <= 40; i++)
            {
                binary[i] = 0;
            }
        }
    }
}
