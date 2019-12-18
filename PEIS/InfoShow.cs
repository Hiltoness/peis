using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PEIS
{
    public partial class InfoShow : Form
    {
        public Person p = null;
        public InfoShow(Person _person)
        {
            InitializeComponent();
            p = _person;
        }

        private void InfoShow_Load(object sender, EventArgs e)
        {
            this.Text = p.b_name;
            label1.Text = p.getbasic();
            var pic = p.b_image;
            if (!string.IsNullOrEmpty(pic))
            {
                //直接返Base64码转成数组
                byte[] imageBytes = Convert.FromBase64String(pic);
                //读入MemoryStream对象
                MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                memoryStream.Write(imageBytes, 0, imageBytes.Length);
                //转成图片
                Image image = Image.FromStream(memoryStream);
                //memoryStream.Close();//不要加上这一句否则就不对了
                // 将图片放置在 PictureBox 中
                this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                this.pictureBox1.Image = image;
            }
        }
    }
}