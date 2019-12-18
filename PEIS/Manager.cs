namespace PEIS
{
    public class Manager
    {
        public Manager()
        {
        }

        public Manager(string m_password, string m_account)
        {
            this.m_password = m_password;
            this.m_account = m_account;
        }

        public Manager(string m_account, string m_password, string m_name, string m_hospital, string m_phone)
        {
            this.m_password = m_password;
            this.m_account = m_account;
            this.m_name = m_name;
            this.m_hospital = m_hospital;
            this.m_phone = m_phone;
        }

        public int m_id { get; set; }
        public string m_password { get; set; }
        public string m_account { get; set; }
        public string m_name { get; set; }
        public int m_authority { get; set; }
        public string m_hospital { get; set; }
        public string m_phone { get; set; }
    }
}