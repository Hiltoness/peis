using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using ExcelDataReader;

namespace PEIS
{
    public partial class BasicInputForm : Form
    {
        public string mode = "b";
        public List<Person> PlList = null;

        public BasicInputForm(string _mode)
        {
            InitializeComponent();
            mode = _mode;
        }

        public void showPerson(string sqlstr)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("b_name", "姓名");
            dataGridView1.Columns[0].DataPropertyName = "b_name";
            dataGridView1.Columns.Add("b_sex", "性别");
            dataGridView1.Columns[1].DataPropertyName = "b_sex";
            dataGridView1.Columns.Add("b_birth", "出生日期");
            dataGridView1.Columns[2].DataPropertyName = "b_birth";
            dataGridView1.Columns.Add("b_nation", "民族");
            dataGridView1.Columns[3].DataPropertyName = "b_nation";
            dataGridView1.Columns.Add("b_phone", "联系电话");
            dataGridView1.Columns[4].DataPropertyName = "b_phone";
            dataGridView1.Columns.Add("b_idcard", "身份证号");
            dataGridView1.Columns[5].DataPropertyName = "b_idcard";
            DataGridViewButtonColumn btncol = new DataGridViewButtonColumn();
            btncol.HeaderText = "编辑";
            btncol.Width = 50;
            dataGridView1.Columns.Add(btncol);
            var getlist = AllValue.db.Query<Person>(sqlstr).ToList();
            dataGridView1.DataSource = getlist;
            this.PlList = getlist;
            for (int i = 0; i < dataGridView1.ColumnCount - 2; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        public void showPerson2(List<Person> plist)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("b_name", "姓名");
            dataGridView1.Columns[0].DataPropertyName = "b_name";
            dataGridView1.Columns.Add("b_sex", "性别");
            dataGridView1.Columns[1].DataPropertyName = "b_sex";
            dataGridView1.Columns.Add("b_birth", "出生日期");
            dataGridView1.Columns[2].DataPropertyName = "b_birth";
            dataGridView1.Columns.Add("b_nation", "民族");
            dataGridView1.Columns[3].DataPropertyName = "b_nation";
            dataGridView1.Columns.Add("b_phone", "联系电话");
            dataGridView1.Columns[4].DataPropertyName = "b_phone";
            dataGridView1.Columns.Add("b_idcard", "身份证号");
            dataGridView1.Columns[5].DataPropertyName = "b_idcard";
            DataGridViewButtonColumn btncol = new DataGridViewButtonColumn();
            btncol.HeaderText = "编辑";
            btncol.Width = 50;
            dataGridView1.Columns.Add(btncol);
            dataGridView1.DataSource = plist;
            this.PlList = plist;
            for (int i = 0; i < dataGridView1.ColumnCount - 2; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void BasicInputForm_Load(object sender, EventArgs e)
        {
            this.Text = "基本信息录入员 " + AllValue.loginUser.m_name;
            showPerson("SELECT * FROM person;");
            if (mode != "b")
            {
                toolStripButton1.Visible = false;
                toolStripButton3.Visible = false;
            }

            if (mode == "f" || mode == "w" || mode == "n" || mode == "o" || mode == "s" || mode == "c")
            {
                批量导入ToolStripMenuItem.Visible = true;
            }
        }

        private void BasicInputForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AllValue.loginForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var inp = new BasicInModiForm();
            inp.Show();
        }

        private void 个人信息设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editme = new SignForm(AllValue.loginUser.m_id);
            editme.ShowDialog();
        }

        public void renew()
        {
            showPerson("SELECT * FROM person;");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            showPerson("SELECT * FROM person;");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            var srhtxt = textBox1.Text.Trim();
            var stype = comboBox1.Text;
            if (e.KeyCode == Keys.Enter)
            {
                if (stype == "姓名")
                {
                    var sql = String.Format("SELECT * FROM person WHERE b_name like '%{0}%'", srhtxt);
                    var getlist = AllValue.db.Query<Person>(sql).ToList();
                    showPerson2(getlist);
                }
                else if (stype == "身份证号")
                {
                    var sql = String.Format("SELECT * FROM person WHERE b_idcard like '%{0}%'", srhtxt);
//                    Console.WriteLine(sql);
                    var getlist = AllValue.db.Query<Person>(sql).ToList();
                    showPerson2(getlist);
                }
                else
                {
                    var sql = String.Format("SELECT * FROM person WHERE b_name like '%{0}%'", srhtxt);
                    var getlist = AllValue.db.Query<Person>(sql).ToList();
                    showPerson2(getlist);
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "请选择要录入的Excel表格";
            fileDialog.Filter = "资料表格(*.xls;*.xlsx)|*.xls;*.xlsx|AllFiles(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                IExcelDataReader excelDataReader = null;
                var filepath = fileDialog.FileName;
                Console.WriteLine(fileDialog.FileName);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                int index = filepath.LastIndexOf('.');
                string extensionName = filepath.Substring(index + 1);
                if (extensionName == "xls")
                {
                    excelDataReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }

                if (extensionName == "xlsx")
                {
                    excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                var configuration = new ExcelDataSetConfiguration
                    {ConfigureDataTable = tableReader => new ExcelDataTableConfiguration {UseHeaderRow = true}};
                var result = excelDataReader.AsDataSet(configuration);
                var table = result.Tables[0];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var row = table.Rows[i];
                    var new_person = new Person(row["b_name"].ToString(), row["b_sex"].ToString(),
                        Convert.ToDateTime(row["b_birth"]), Convert.ToInt32(row["b_marriage"]), "",
                        row["b_educated"].ToString(), row["b_nation"].ToString(), row["b_occupation"].ToString(),
                        row["b_birthplace"].ToString(), row["b_phone"].ToString(), row["b_email"].ToString(),
                        row["b_address"].ToString(), row["b_anamnesis"].ToString(),
                        row["b_college"].ToString(), row["b_idcard"].ToString());
                    var findid = AllValue.db.Query("SELECT * FROM person WHERE b_idcard=@b_idcard", new_person).ToList()
                        .Count;
                    if (findid == 0)
                    {
                        try
                        {
                            AllValue.db.Execute(
                                "INSERT INTO person(b_name, b_sex, b_birth, b_marriage, b_image, b_educated, b_nation, b_occupation, b_birthplace, b_phone, b_email, b_address, b_anamnesis, b_college, b_idcard) VALUES (@b_name, @b_sex, @b_birth, @b_marriage, @b_image, @b_educated, @b_nation, @b_occupation, @b_birthplace, @b_phone, @b_email, @b_address, @b_anamnesis, @b_college, @b_idcard)",
                                new_person);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            MessageBox.Show("添加过程中出现失败");
                        }
                    }
                    else
                    {
                        try
                        {
                            AllValue.db.Execute(
                                "UPDATE person SET b_name=@b_name, b_sex=@b_sex, b_birth=@b_birth, b_marriage=@b_marriage, b_image=@b_image, b_educated=@b_educated, b_nation=@b_nation, b_occupation=@b_occupation, b_birthplace=@b_birthplace, b_phone=@b_phone, b_email=@b_email, b_address=@b_address, b_anamnesis=@b_anamnesis, b_college=@b_college, b_idcard=@b_idcard WHERE b_idcard=@b_idcard;",
                                new_person);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            MessageBox.Show("添加过程中出现失败");
                        }
                    }
                }

                MessageBox.Show("添加结束，请刷新查看结果");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Person index = this.PlList[e.RowIndex];
            if (e.ColumnIndex == 6)
            {
                switch (this.mode)
                {
                    case "f":
                        var doctorform = new DoctorForm(1, index);
                        doctorform.ShowDialog();
                        renew();
                        break;
                }
            }
            else
            {
                var info = new InfoShow(index);
                info.ShowDialog();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void 批量导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "请选择要录入的Excel表格";
            fileDialog.Filter = "资料表格(*.xls;*.xlsx)|*.xls;*.xlsx|AllFiles(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                IExcelDataReader excelDataReader = null;
                var filepath = fileDialog.FileName;
                Console.WriteLine(fileDialog.FileName);
                FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                int index = filepath.LastIndexOf('.');
                string extensionName = filepath.Substring(index + 1);
                if (extensionName == "xls")
                {
                    excelDataReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }

                if (extensionName == "xlsx")
                {
                    excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                var configuration = new ExcelDataSetConfiguration
                    {ConfigureDataTable = tableReader => new ExcelDataTableConfiguration {UseHeaderRow = true}};
                var result = excelDataReader.AsDataSet(configuration);
                var table = result.Tables[0];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    var row = table.Rows[i];
                    var new_person = new Person(row["b_name"].ToString(), row["b_sex"].ToString(),
                        Convert.ToDateTime(row["b_birth"]), Convert.ToInt32(row["b_marriage"]), "",
                        row["b_educated"].ToString(), row["b_nation"].ToString(), row["b_occupation"].ToString(),
                        row["b_birthplace"].ToString(), row["b_phone"].ToString(), row["b_email"].ToString(),
                        row["b_address"].ToString(), row["b_anamnesis"].ToString(),
                        row["b_college"].ToString(), row["b_idcard"].ToString());
                    var findid = AllValue.db.Query("SELECT * FROM person WHERE b_idcard=@b_idcard", new_person).ToList()
                        .Count;
                    if (findid == 0)
                    {
                        try
                        {
                            AllValue.db.Execute(
                                "INSERT INTO person(b_name, b_sex, b_birth, b_marriage, b_image, b_educated, b_nation, b_occupation, b_birthplace, b_phone, b_email, b_address, b_anamnesis, b_college, b_idcard) VALUES (@b_name, @b_sex, @b_birth, @b_marriage, @b_image, @b_educated, @b_nation, @b_occupation, @b_birthplace, @b_phone, @b_email, @b_address, @b_anamnesis, @b_college, @b_idcard)",
                                new_person);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            MessageBox.Show("添加过程中出现失败");
                        }
                    }
                    else
                    {
                        try
                        {
                            AllValue.db.Execute(
                                "UPDATE person SET b_name=@b_name, b_sex=@b_sex, b_birth=@b_birth, b_marriage=@b_marriage, b_image=@b_image, b_educated=@b_educated, b_nation=@b_nation, b_occupation=@b_occupation, b_birthplace=@b_birthplace, b_phone=@b_phone, b_email=@b_email, b_address=@b_address, b_anamnesis=@b_anamnesis, b_college=@b_college, b_idcard=@b_idcard WHERE b_idcard=@b_idcard;",
                                new_person);
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception);
                            MessageBox.Show("添加过程中出现失败");
                        }
                    }
                }

                MessageBox.Show("添加结束，请刷新查看结果");
            }
        }
    }
}