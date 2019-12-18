using System;
using System.Windows.Forms;

namespace PEIS
{
    public partial class SuperManagerForm : Form
    {
        public SuperManagerForm()
        {
            InitializeComponent();
        }

        private void SuperManagerForm_Load(object sender, EventArgs e)
        {
            this.Text = "全局管理员 "+AllValue.loginUser.m_name;
        }

        private void SuperManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AllValue.loginForm.Show();
        }
    }
}