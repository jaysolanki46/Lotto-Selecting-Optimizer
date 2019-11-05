using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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


        string connetionString = @"Data Source=localhost;Initial Catalog=Lotto-Selecting-Optimizer;Integrated Security=True";
        SqlConnection cnn;
        SqlCommand cmd;
        SqlDataReader reader;
        String sql = "";
       
        public LottoInterface()
        {
            InitializeComponent();
        }

        private void LottoInterface_Load(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            LblTicketNumber.Text = generateTicketNumber();
            populateNumbers();
            cnn.Close();
            
        }

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
                        MessageBox.Show("Pick only 6 numbers as a group", "Warning");
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
                var finalButton = new CustomButton();
                finalButton.Text = val.ToString();
                finalButton.Dock = DockStyle.Fill;
                finalButton.BackColor = Color.LightGreen;
                this.tableLayoutPanelGroup.Controls.Add(finalButton);
            }

            if(counter == 6) { lblExcellent.Visible = true; } else { lblExcellent.Visible = false; }
        }

        private void BtnBuy_Click(object sender, EventArgs e)
        {

            string ticketNumber = "";
            string customerGroup = "";
            string binaryGroup = "";

            if(counter == 6)
            {
                foreach (string str in group)
                {
                    binaryGroup += toBinaryNumber(str); 
                    customerGroup += str + ",";
                }

                ticketNumber = LblTicketNumber.Text.Replace("-", "");
                customerGroup = customerGroup.TrimEnd(',');


                cnn.Open();
                sql = "INSERT INTO Tickets values (@ticketNumber, @customerGroup, @binaryGroup)";
                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@ticketNumber", ticketNumber);
                cmd.Parameters.AddWithValue("@customerGroup", customerGroup);
                cmd.Parameters.AddWithValue("@binaryGroup", binaryGroup);
                cmd.ExecuteNonQuery();
                cnn.Close();

                LottoThankYouPage thankYouPage = new LottoThankYouPage();
                thankYouPage.Show();
                this.Hide();

            } else
            {
                MessageBox.Show("Please select atleast 6 choice !");
            }
        }


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
                var b = new CustomButton();
                b.Text = (i + 1).ToString();
                b.Name = string.Format("b_{0}", i + 1);
                b.Click += b_Click;
                b.Dock = DockStyle.Fill;
                b.BackColor = Color.LightGray;
                this.tableLayoutPanelLottoBalls.Controls.Add(b);
            }
        }

        private string generateTicketNumber()
        {
            string ticketNumber = "";
            sql = "SELECT TOP 1 TicketNumber FROM Tickets ORDER BY LottoId DESC";
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ticketNumber = (Convert.ToInt64(reader.GetValue(0).ToString()) + 1).ToString();
            }

            ticketNumber = ticketNumber.Insert(5, "-");
            return ticketNumber;
        }

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
    }
}
