using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApplication
{
    public partial class MsgFrom : Form
    {
        public MsgFrom()
        {
            InitializeComponent();
        }

        public static void Added(string msg = "تم الاضافه بنجاح")
        {
            MessageBox.Show(msg);
        }

        public static void Removed(string msg = "تم الحذف بنجاح")
        {
            MessageBox.Show(msg);
        }

        public static void Updated(string msg = "تم التعديل بنجاح")
        {
            MessageBox.Show(msg);
        }

        public static DialogResult DoRemove(string msg = "هل تريد اتمام عمليه الحذف ؟")
        {
          return MessageBox.Show(msg, "الرجاء الانتباه", MessageBoxButtons.YesNo);
        }

        public static DialogResult DoUpdate(string msg = "هل تريد اتمام عمليه التعديل ؟")
        {
            return MessageBox.Show(msg, "الرجاء الانتباه", MessageBoxButtons.YesNo);
        }

        public static string GetImgPath()
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                op.Filter = "Images |*.PNG; *.JPG; *.GIF ";
                op.Title = "Mina Noshy : 01111257052";
                op.Multiselect = false;
                op.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                return op.FileName;
            }
            else
                return "";
        }

        private void MsgFrom_Load(object sender, EventArgs e)
        {

        }
    }
}
