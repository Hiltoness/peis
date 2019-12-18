using System;

namespace PEIS
{
    public class Person
    {
        public Person()
        {
        }

        public Person(string bName, string bSex, DateTime bBirth, int bMarriage, string bImage, string bEducated,
            string bNation, string bOccupation, string bBirthplace, string bPhone, string bEmail, string bAddress,
            string bAnamnesis, string bCollege, string bIdcard)
        {
            b_name = bName;
            b_sex = bSex;
            b_birth = bBirth;
            b_marriage = bMarriage;
            b_image = bImage;
            b_educated = bEducated;
            b_nation = bNation;
            b_occupation = bOccupation;
            b_birthplace = bBirthplace;
            b_phone = bPhone;
            b_email = bEmail;
            b_address = bAddress;
            b_anamnesis = bAnamnesis;
            b_college = bCollege;
            b_idcard = bIdcard;
        }

        public int b_id { get; set; }
        public string b_name { get; set; }
        public string b_sex { get; set; }
        public DateTime b_birth { get; set; }
        public int b_marriage { get; set; }
        public string b_image { get; set; }
        public string b_educated { get; set; }
        public string b_nation { get; set; }
        public string b_occupation { get; set; }
        public string b_birthplace { get; set; }
        public string b_phone { get; set; }
        public string b_email { get; set; }
        public string b_address { get; set; }
        public string b_anamnesis { get; set; }
        public string b_college { get; set; }
        public string b_idcard { get; set; }
        public float f_eyeL { get; set; }
        public float f_eyeR { get; set; }
        public float f_eyeL_correct { get; set; }
        public float f_eyeR_correct { get; set; }
        public string f_eye_otherill { get; set; }
        public string f_eye_colorpic { get; set; }
        public string f_eye_color { get; set; }
        public float f_earL { get; set; }
        public float f_earR { get; set; }
        public string f_earill { get; set; }
        public string f_nosesmell { get; set; }
        public string f_face { get; set; }
        public string f_throat { get; set; }
        public string f_orallip { get; set; }
        public string f_oraltooth { get; set; }
        public string f_other { get; set; }
        public string f_eye_op { get; set; }
        public string f_oto_op { get; set; }
        public string f_oral_op { get; set; }
        public float w_height { get; set; }
        public float w_weight { get; set; }
        public string w_skin { get; set; }
        public string w_lymph { get; set; }
        public string w_thyroid { get; set; }
        public string w_spine { get; set; }
        public string w_arm_and_leg { get; set; }
        public string w_arthrosis { get; set; }
        public string w_flatfoot { get; set; }
        public string w_others { get; set; }
        public string w_advice { get; set; }
        public float n_blood { get; set; }
        public int n_heartrate { get; set; }
        public string n_grow { get; set; }
        public string n_nerve { get; set; }
        public string n_breath { get; set; }
        public string n_heartvessel { get; set; }
        public string n_liver { get; set; }
        public string n_spleen { get; set; }
        public string n_kidney { get; set; }
        public string n_others { get; set; }
        public string n_advice { get; set; }
        public string o_blood { get; set; }
        public string o_liver { get; set; }
        public string o_piss { get; set; }
        public string o_chest { get; set; }
        public string o_othercheck { get; set; }
        public string o_stutter { get; set; }
        public string o_outlook { get; set; }
        public string o_advice { get; set; }
        public string o_sign { get; set; }
        public string s_conclusion { get; set; }
        public string s_sign { get; set; }
        public string s_advice { get; set; }
        public string s_hospital { get; set; }
        public DateTime s_time { get; set; }
        public string c_advice { get; set; }
        public string c_sign { get; set; }
        public string c_note { get; set; }

        public string getbasic()
        {
            var str = String.Format(
                "基本信息：\n\n学院：{12}   身份证号：{13}\n姓名：{0}    性别：{1}    出生日期：{2}\n是否结婚：{3}   文化程度：{4}   民族：{5}\n职业：{6}   籍贯：{7}   联系电话：{8}\n电子邮箱：{9}\n通讯地址：{10}\n病史：{11}",
                b_name, b_sex, b_birth, b_marriage == 1 ? "是" : "否", b_educated, b_nation, b_occupation, b_birthplace,
                b_phone,
                b_email, b_address, b_anamnesis, b_college, b_idcard);
            return str;
        }
    }
}