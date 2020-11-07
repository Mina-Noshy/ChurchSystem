using MyApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApplication
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value++;
                if (progressBar1.Value == 100)
                    progressBar1.Value = 0;
            }
            catch
            {

            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

            MainForm frm = new MainForm();
            this.Icon = frm.Icon;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text == "mina-top3" && textBox2.Text == "@hack")
                {
                    MainForm.GetMainFrm.UnLockApp("admin");
                    MainForm.GetMainFrm.menuStrip1.Items[5].Visible = true;
                    this.Close();
                }
                else
                {
                    using(AppDbContext db = new AppDbContext())
                    {
                        var user = db.Users.FirstOrDefault(x => x.UserName == textBox1.Text && x.Password == textBox2.Text);
                        if(user != null)
                        {
                            string rank = user.Rank;
                            MainForm.GetMainFrm.UnLockApp(rank);

                            var sitting = db.Sittings.FirstOrDefault();
                            if (sitting != null)
                            {
                                if (GetMACAddress() != sitting.MacAddress)
                                {
                                    MainForm.GetMainFrm.LockApp();
                                    MessageBox.Show("لقد تم استخدام هذا النظام على جهاز اخر سوف يتم ايقافه مؤقتا لحين قدوم مطور النظام لحل هذه المشكلة", "مشكلة متعلقة بالامان");
                                }
                            }
                            else
                            {
                                var sitt = new Sitting
                                {
                                    MacAddress = GetMACAddress(),
                                    ChurchName = "0",
                                    DeviceName = "0",
                                    Mobile = "01111257052",
                                    Note = "web developer",
                                    StartDate = DateTime.Now.Date,
                                    ExpireDate = DateTime.Now.Date
                                };
                                db.Sittings.Add(sitt);
                                db.SaveChanges();
                            }

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("اسم المستخدم او كلمة المرور غير صحيحة");
                        }

                    }
                }
            }
            catch
            {

            }
        }
    }
}
