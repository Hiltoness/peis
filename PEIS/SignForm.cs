using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Dapper;

namespace PEIS
{
    public partial class SignForm : Form
    {
        public IDbConnection db = AllValue.db;
        public int uid = -1;

        public SignForm(int _uid = -1)
        {
            InitializeComponent();
            uid = _uid;
        }

        private void add_Click()
        {
            var account = textBox1.Text.Trim();
            var password = textBox2.Text.Trim();
            var name = textBox3.Text.Trim();
            var hospital = textBox4.Text.Trim();
            var phone = textBox5.Text.Trim();

            var newManager = new Manager(account, password, name, hospital, phone);
            if (account == "" || password == "" || name == "" || hospital == "" || phone == "")
            {
                MessageBox.Show("不能出现空值");
                return;
            }

            try
            {
                db.Execute(
                    "INSERT INTO manager(m_account, m_name, m_hospital, m_phone, m_password) VALUES (@m_account,@m_name,@m_hospital,@m_phone,@m_password)",
                    newManager);
                MessageBox.Show("注册成功");
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("注册失败，请检查数据库连接或者注册值的合法性");
            }
        }

        private void editManager()
        {
            var account = textBox1.Text.Trim();
            var password = textBox2.Text.Trim();
            var name = textBox3.Text.Trim();
            var hospital = textBox4.Text.Trim();
            var phone = textBox5.Text.Trim();

            var newManager = new Manager(account, password, name, hospital, phone);
            newManager.m_id = uid;
            if (account == "" || password == "" || name == "" || hospital == "" || phone == "")
            {
                MessageBox.Show("不能出现空值");
                return;
            }

            try
            {
                AllValue.db.Execute(
                    "UPDATE manager SET m_account=@m_account, m_name=@m_name, m_password=@m_password, m_phone=@m_phone, m_hospital=@m_hospital WHERE m_id=@m_id",
                    newManager);
                MessageBox.Show("修改成功");
                this.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Failed");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (uid == -1)
            {
                add_Click();
            }
            else
            {
                editManager();
            }
        }

        private void SignForm_Load(object sender, EventArgs e)
        {
            if (uid != -1)
            {
                var manager = new Manager();
                manager.m_id = uid;
                var getM = AllValue.db.Query<Manager>("SELECT * FROM manager WHERE m_id=@m_id", manager).ToList()[0];
                textBox1.Text = getM.m_account;
                textBox2.Text = getM.m_password;
                textBox3.Text = getM.m_name;
                textBox4.Text = getM.m_hospital;
                textBox5.Text = getM.m_phone;
                button1.Text = "确认修改";
            }
        }
    }
}