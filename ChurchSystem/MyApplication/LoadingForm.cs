using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApplication
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

            MainForm frm = new MainForm();
            this.Icon = frm.Icon;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value++;
                label2.Text = "%" + progressBar1.Value.ToString();

                switch (progressBar1.Value)
                {
                    case 5:
                        label1.Text = "كنيسة السيدة العذراء مريم";
                        break;
                    case 10:
                        label1.Text = "والشهيد العظيم مارجرجس بالشواولة";
                        break;
                    case 15:
                        label1.Text = "بسم الاب والابن والروح القدس الاله الواحد امين";
                        break;
                    case 20:
                        label1.Text = "نظام الادارة الكنسية";
                        break;
                    case 25:
                        label1.Text = "شاشات القرى والمناطق";
                        break;
                    case 30:
                        label1.Text = "شاشات المنازل وشعب الكنيسة";
                        break;
                    case 35:
                        label1.Text = "شاشات الاحتفالات والبحث المتعدد";
                        break;
                    case 40:
                        label1.Text = "شاشات الضبط وصلاحيات المستخدمين";
                        break;
                    case 45:
                        label1.Text = "تعداد سكاني لشعب الكنيسة";
                        break;
                    case 55:
                        label1.Text = "قائمة لابناءنا الخريجين والمتفوقين";
                        break;
                    case 60:
                        label1.Text = "قائمة شهريه لافتقاد رعية الكنيسة";
                        break;
                    case 65:
                        label1.Text = "قائمة شهريه للتغيب عن الاعتراف";
                        break;
                    case 70:
                        label1.Text = "قائمة للعزاء والجنازات";
                        break;
                    case 75:
                        label1.Text = "نظام صلاحيات للمستخدمين";
                        break;
                    case 80:
                        label1.Text = "شاشة تسجيل خدام ومعلمين الكنيسة";
                        break;
                    case 85:
                        label1.Text = "تم التطوير من خلال الابن المبارك";
                        break;
                    case 90:
                        label1.Text = "مينا نصحى وهبه";
                        break;
                    case 95:
                        label1.Text = "تحت اشراف : الاب صموئيل جرجس راعي الكنيسة";
                        break;
                    case 100:
                        this.Close();
                        break;
                }
            }
            catch
            {

            }
        }
    }
}
