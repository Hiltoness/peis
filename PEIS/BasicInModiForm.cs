using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Dapper;

namespace PEIS
{
    public partial class BasicInModiForm : Form
    {
        public int mode = -1;
        public string pic = "";

        public BasicInModiForm(int _mode = -1)
        {
            InitializeComponent();
            mode = _mode;
        }

        private void BasicInModiForm_Load(object sender, EventArgs e)
        {
            if (mode == -1)
            {
                this.Text = "人员基本信息录入";
            }
            else
            {
                this.Text = "人员基本信息修改";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "请选择人员头像图片文件";
            openfile.Filter = "头像图片(*.jpg;*.bmp;*png)|*.jpeg;*.jpg;*.bmp;*.png|AllFiles(*.*)|*.*";
            if (DialogResult.OK == openfile.ShowDialog())
            {
                try
                {
                    FileInfo fi = new FileInfo(openfile.FileName);
                    if (fi.Length / 1024 / 1024 >= 0.8)
                    {
                        MessageBox.Show("图像文件不能大于800k");
                    }
                    else
                    {
                        Bitmap bmp = new Bitmap(openfile.FileName);
                        pictureBox1.Image = bmp;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        MemoryStream ms = new MemoryStream();
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        byte[] arr = new byte[ms.Length];
                        ms.Position = 0;
                        ms.Read(arr, 0, (int) ms.Length);
                        ms.Close();
                        pic = Convert.ToBase64String(arr);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("打开头像文件失败");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text.Trim();
            var sex = comboBox2.Text.Trim();
            var birth = dateTimePicker1.Value;
            var marriage = comboBox1.Text.Trim() == "是" ? 1 : 0;
            var edu = textBox5.Text.Trim();
            var nation = textBox6.Text.Trim();
            var birthplace = textBox12.Text.Trim();
            var phone = textBox11.Text.Trim();
            var college = textBox10.Text.Trim();
            var idcard = textBox9.Text.Trim();
            var addr = textBox8.Text.Trim();
            var bingshi = textBox7.Text;
            var occ = textBox2.Text.Trim();
            var email = textBox3.Text.Trim();
            var new_person = new Person(name, sex, birth, marriage, pic, edu, nation, occ, birthplace, phone, email,
                addr, bingshi,
                college, idcard);
            if (name == "")
            {
                MessageBox.Show("请输入姓名");
                return;
            }

            try
            {
                AllValue.db.Execute(
                    "INSERT INTO person(b_name, b_sex, b_birth, b_marriage, b_image, b_educated, b_nation, b_occupation, b_birthplace, b_phone, b_email, b_address, b_anamnesis, b_college, b_idcard) VALUES (@b_name, @b_sex, @b_birth, @b_marriage, @b_image, @b_educated, @b_nation, @b_occupation, @b_birthplace, @b_phone, @b_email, @b_address, @b_anamnesis, @b_college, @b_idcard)",
                    new_person);
                var ok = MessageBox.Show("创建成功,是否清空录入下一个", "录入成功", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (ok == DialogResult.OK)
                {
                    button3_Click(sender, e);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("Failed");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            pic = "";
            pictureBox1.Image = null;
        }
    }
}