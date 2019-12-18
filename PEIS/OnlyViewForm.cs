using System;
using System.Windows.Forms;

namespace PEIS
{
    public partial class OnlyViewForm : Form
    {
        public OnlyViewForm()
        {
            InitializeComponent();
        }

        private void OnlyViewForm_Load(object sender, EventArgs e)
        {
            this.Text = "初始用户 " + AllValue.loginUser.m_name;
            label1.Text = AllValue.loginUser.m_name + " 您好！ 您目前只有查看体检人员基本信息的权限，请告知管理员予以分配权限。";
        }

        private void OnlyViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AllValue.loginForm.Show();
        }
    }
}