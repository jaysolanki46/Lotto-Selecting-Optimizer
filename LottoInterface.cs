using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public LottoInterface()
        {
            InitializeComponent();
        }

        private void LottoInterface_Load(object sender, EventArgs e)
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
                finalButton.BackColor = Color.LightGray;
                this.tableLayoutPanelGroup.Controls.Add(finalButton);
            }

            if(counter == 6) { lblExcellent.Visible = true; } else { lblExcellent.Visible = false; }
        }
    }
}
