﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lotto_Selecting_Optimizer
{
    public partial class LottoThankYouPage : Form
    {
        public LottoThankYouPage()
        {
            InitializeComponent();
        }

        private void LinkLabelShopAgain_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LottoInterface lottoInterface = new LottoInterface();
            lottoInterface.Show();
            this.Hide();
        }
    }
}
