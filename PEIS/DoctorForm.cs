using System;
using System.ComponentModel;
using System.Windows.Forms;
using Dapper;

namespace PEIS
{
    public partial class DoctorForm : Form
    {
        public int mode = 1;
        public Person p = null;

        // 1:五官科 2：外科 3：内科 4：其他 5：总结 6：复审
        public DoctorForm(int _mode,Person _person)
        {
            InitializeComponent();
            mode = _mode;
            p = _person;
        }

        private void DoctorForm_Load(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                propertyGrid1.SelectedObject = AllValue.Mapping<Five, Person>(p);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                Person newp = AllValue.Mapping<Person, Five>((Five)propertyGrid1.SelectedObject);
                newp.b_id = p.b_id;
                Console.WriteLine(newp.f_eyeL);
                try
                {
                    AllValue.db.Execute(
                        "UPDATE person SET f_eyeL=@f_eyeL, f_eyeR=@f_eyeR, f_eyeL_correct=@f_eyeL_correct, f_eyeR_correct=@f_eyeR_correct, f_eye_otherill=@f_eye_otherill, f_eye_colorpic=@f_eye_colorpic, f_eye_color=@f_eye_color, f_earL=@f_earL, f_earR=@f_earR, f_earill=@f_earill, f_nosesmell=@f_nosesmell, f_face=@f_face, f_throat=@f_throat, f_orallip=@f_orallip, f_oraltooth=@f_oraltooth, f_other=@f_other, f_eye_op=@f_eye_op, f_oto_op=@f_oto_op, f_oral_op=@f_oral_op WHERE b_id=@b_id",
                        newp);
                    MessageBox.Show("更新成功");
                    this.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show("更新失败");
                }
            }
        }
    }
}