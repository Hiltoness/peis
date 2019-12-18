using System.ComponentModel;

namespace PEIS
{
    public class Five
    {
        [Category("眼"), DisplayName("左眼视力")] public float f_eyeL { get; set; }
        [Category("眼"), DisplayName("右眼视力")] public float f_eyeR { get; set; }
        [Category("眼"), DisplayName("左眼矫正度数")] public float f_eyeL_correct { get; set; }
        [Category("眼"), DisplayName("右眼矫正度数")] public float f_eyeR_correct { get; set; }
        [Category("眼"), DisplayName("其他眼病")] public string f_eye_otherill { get; set; }

        [Category("眼"), DisplayName("彩色图案及编码")]
        public string f_eye_colorpic { get; set; }

        [Category("眼"), DisplayName("单颜色识别")] public string f_eye_color { get; set; }
        [Category("耳"), DisplayName("左耳听力")] public float f_earL { get; set; }
        [Category("耳"), DisplayName("右耳听力")] public float f_earR { get; set; }
        [Category("耳"), DisplayName("耳疾")] public string f_earill { get; set; }
        [Category("鼻"), DisplayName("嗅觉")] public string f_nosesmell { get; set; }
        [Category("颜面部"), DisplayName("面部")] public string f_face { get; set; }
        [Category("颜面部"), DisplayName("咽喉")] public string f_throat { get; set; }
        [Category("口腔"), DisplayName("唇")] public string f_orallip { get; set; }
        [Category("口腔"), DisplayName("门齿")] public string f_oraltooth { get; set; }
        [Category("其他"), DisplayName("其他")] public string f_other { get; set; }

        [Category("医生意见(签字)"), DisplayName("1.眼科")]
        public string f_eye_op { get; set; }

        [Category("医生意见(签字)"), DisplayName("2.耳鼻喉科")]
        public string f_oto_op { get; set; }

        [Category("医生意见(签字)"), DisplayName("3.口腔科")]
        public string f_oral_op { get; set; }
    }

    public class Wai
    {
    }
}