using MyApplication.Models;
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
    public partial class UpdateLevelForm : Form
    {
        public UpdateLevelForm()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var cbxLevel = db.Levels.OrderBy(x => x.LevelName).ToList();
                    cbxLevelFrom.DataSource = cbxLevel;
                    cbxLevelFrom.DisplayMember = "LevelName";
                    cbxLevelFrom.ValueMember = "Id";

                    var cbxLevel33 = db.Levels.OrderBy(x => x.LevelName).ToList();
                    cbxLevelTo.DataSource = cbxLevel33;
                    cbxLevelTo.DisplayMember = "LevelName";
                    cbxLevelTo.ValueMember = "Id";

                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        private void UpdateLevelForm_Load(object sender, EventArgs e)
        {
            Clear();
            MainForm frm = new MainForm();
            this.Icon = frm.Icon;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(cbxLevelFrom.SelectedIndex != -1 && cbxLevelTo.SelectedIndex != -1)
                {
                    using(AppDbContext db = new AppDbContext())
                    {
                        int idFrom = (int)cbxLevelFrom.SelectedValue, idTo = (int)cbxLevelTo.SelectedValue;

                        var data = db.Peoples.Where(x => x.LevelId == idFrom).ToList();
                        data.ForEach(x => x.LevelId = idTo);
                        db.SaveChanges();
                        MessageBox.Show("تم نقل الطلاب الى المستوي التالى");
                    }
                }
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxKgFrom.SelectedIndex != -1 && cbxKgTo.SelectedIndex != -1)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        string levelFrom = cbxKgFrom.SelectedItem.ToString(), levelTo = cbxKgTo.SelectedItem.ToString();

                        var data = db.Nurseries.Where(x => x.Level == levelFrom).ToList();
                        data.ForEach(x => x.Level = levelTo);
                        db.SaveChanges();
                        MessageBox.Show("تم نقل الاطفال الى المستوى التالى");
                    }
                }
            }
            catch
            {

            }
        }
    }
}
