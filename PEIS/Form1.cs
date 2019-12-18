using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using MySql.Data.MySqlClient;

namespace PEIS
{
    public class AllValue
    {
        public static IDbConnection db = null;
        public static Manager loginUser = null;
        public static Form loginForm = null;
        public static R Mapping<R, T>(T model)
        {
            R result = Activator.CreateInstance<R>();
            foreach(PropertyInfo info in typeof(R).GetProperties())
            {
                PropertyInfo pro = typeof(T).GetProperty(info.Name);
                if (pro != null)
                    info.SetValue(result, pro.GetValue(model));
            }
            return result;
        }
    }

    public partial class Form1 : Form
    {
        public IDbConnection db = AllValue.db;

        public Form1()
        {
            InitializeComponent();
            db = AllValue.db;
            AllValue.loginForm = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var serve = textBox1.Text.Trim();
            var connString = String.Format("Server={0}; Database=peis; Uid=peis; Pwd=peis;", serve);
            AllValue.db = new MySqlConnection(connString);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var serve = textBox1.Text.Trim();
            var connString = String.Format("Server={0}; Database=peis; Uid=peis; Pwd=peis;", serve);
            AllValue.db = new MySqlConnection(connString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var signForm = new SignForm();
            signForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var account = textBox2.Text.Trim();
            var password = textBox3.Text.Trim();
//
//            var login = new Manager(password, account);
            var sqlstr = String.Format("SELECT * FROM manager WHERE m_account = '{0}' AND m_password = '{1}';", account,
                password);
            try
            {
                var getlist = AllValue.db.Query<Manager>(sqlstr).ToList();
                if (getlist.Count == 1)
                {
                    MessageBox.Show("登录成功");
                    AllValue.loginUser = getlist[0];
                    var au = AllValue.loginUser.m_authority;
                    if (au == 1)
                    {
                        var superForm = new SuperManagerForm();
                        superForm.Show();
                    }else if (au == 0)
                    {
                        var viewForm = new OnlyViewForm();
                        viewForm.Show();
                    }else if (au == 2)
                    {
                        var inputForm = new BasicInputForm("b");
                        inputForm.Show();
                    }else if (au == 3)
                    {
                        var inputForm = new BasicInputForm("f");
                        inputForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("用户名不正确或者密码错误");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("服务器连接失败");
            }
        }
    }
}