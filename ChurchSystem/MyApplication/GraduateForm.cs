using MyApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApplication
{
    public partial class GraduateForm : Form
    {
        public GraduateForm()
        {
            InitializeComponent();
        }

        private void SetImg()
        {
            try
            {
                if (txtImage.Text != "")
                {
                    pictureBox1.Image = Image.FromFile(txtImage.Text.Replace(@"\", @"\\"));
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch
            {

            }
        }

        private void Clear()
        {
            try
            {
                txtName.Clear();
                txtGeadeYear.Clear();
                txtPresent.Clear();
                txtAddress.Clear();
                txtPhone.Clear();
                txtImage.Clear();
                txtYear.Clear();
                txtSearch.Clear();
                txtNote.Clear();
                pictureBox1.Image = null;



                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Graduates
                               select new
                               {
                                   x.Id,
                                   x.StudentName,
                                   x.GraduateYear,
                                   x.Grade,
                                   x.Percent,
                                   x.Collage.CollageName,
                                   x.Stage.StageName,
                                   x.Mobile,
                                   x.Addres,
                                   x.Note,
                                   x.ImagePath
                               };

                    
                    // ****************************** //
                    var cbxCollage = db.Collages.OrderBy(x => x.CollageName).ToList();
                    cbxCollage1.DataSource = cbxCollage;
                    cbxCollage1.DisplayMember = "CollageName";
                    cbxCollage1.ValueMember = "Id";

                    var cbxCollage33 = db.Collages.OrderBy(x => x.CollageName).ToList();
                    cbxCollage2.DataSource = cbxCollage33;
                    cbxCollage2.DisplayMember = "CollageName";
                    cbxCollage2.ValueMember = "Id";

                    // ****************************** //
                    var cbxStage = db.Stages.OrderBy(x => x.StageName).ToList();
                    cbxStage1.DataSource = cbxStage;
                    cbxStage1.DisplayMember = "StageName";
                    cbxStage1.ValueMember = "Id";

                    var cbxStage33 = db.Stages.OrderBy(x => x.StageName).ToList();
                    cbxStage2.DataSource = cbxStage33;
                    cbxStage2.DisplayMember = "StageName";
                    cbxStage2.ValueMember = "Id";
                    // ********************** //

                    cbxGrade1.SelectedIndex = -1;
                    cbxGrade2.SelectedIndex = -1;
                    cbxStage1.SelectedIndex = -1;
                    cbxStage2.SelectedIndex = -1;
                    cbxCollage1.SelectedIndex = -1;
                    cbxCollage2.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.StudentName).ToList();

                    this.Text = "اجمالى عدد الخريجين   " + data.Count().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchGrade()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Graduates.Where(x => x.Grade == cbxGrade2.SelectedItem.ToString())
                               select new
                               {
                                   x.Id,
                                   x.StudentName,
                                   x.GraduateYear,
                                   x.Grade,
                                   x.Percent,
                                   x.Collage.CollageName,
                                   x.Stage.StageName,
                                   x.Mobile,
                                   x.Addres,
                                   x.Note,
                                   x.ImagePath
                               };

                    dataGridView1.DataSource = data.OrderBy(x => x.StudentName).ToList();

                    this.Text = "اجمالى عدد الخريجين   " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchText()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Graduates.Where(x => x.StudentName.Contains(txtSearch.Text) || x.Mobile.Contains(txtSearch.Text) || x.Addres.Contains(txtSearch.Text) || x.Note.Contains(txtSearch.Text))
                               select new
                               {
                                   x.Id,
                                   x.StudentName,
                                   x.GraduateYear,
                                   x.Grade,
                                   x.Percent,
                                   x.Collage.CollageName,
                                   x.Stage.StageName,
                                   x.Mobile,
                                   x.Addres,
                                   x.Note,
                                   x.ImagePath
                               };

                    dataGridView1.DataSource = data.OrderBy(x => x.StudentName).ToList();

                    this.Text = "اجمالى عدد الخريجين   " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchYear()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int year = int.Parse(txtYear.Text);
                    var data = from x in db.Graduates.Where(x => x.GraduateYear == year)
                               select new
                               {
                                   x.Id,
                                   x.StudentName,
                                   x.GraduateYear,
                                   x.Grade,
                                   x.Percent,
                                   x.Collage.CollageName,
                                   x.Stage.StageName,
                                   x.Mobile,
                                   x.Addres,
                                   x.Note,
                                   x.ImagePath
                               };

                    dataGridView1.DataSource = data.OrderBy(x => x.StudentName).ToList();

                    this.Text = "اجمالى عدد الخريجين   " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchCollage()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)cbxCollage2.SelectedValue;

                    var data = from x in db.Graduates.Where(x => x.CollageId == id)
                               select new
                               {
                                   x.Id,
                                   x.StudentName,
                                   x.GraduateYear,
                                   x.Grade,
                                   x.Percent,
                                   x.Collage.CollageName,
                                   x.Stage.StageName,
                                   x.Mobile,
                                   x.Addres,
                                   x.Note,
                                   x.ImagePath
                               };

                    dataGridView1.DataSource = data.OrderBy(x => x.StudentName).ToList();

                    this.Text = "اجمالى عدد الخريجين   " + data.Count().ToString();

                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchStage()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)cbxStage2.SelectedValue;

                    var data = from x in db.Graduates.Where(x => x.StageId == id)
                               select new
                               {
                                   x.Id,
                                   x.StudentName,
                                   x.GraduateYear,
                                   x.Grade,
                                   x.Percent,
                                   x.Collage.CollageName,
                                   x.Stage.StageName,
                                   x.Mobile,
                                   x.Addres,
                                   x.Note,
                                   x.ImagePath
                               };

                    dataGridView1.DataSource = data.OrderBy(x => x.StudentName).ToList();

                    this.Text = "اجمالى عدد الخريجين   " + data.Count().ToString();

                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        private void GraduateForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "الاسم";
                dataGridView1.Columns[2].HeaderText = "سنة التخرج";
                dataGridView1.Columns[3].HeaderText = "التقدير";
                dataGridView1.Columns[4].HeaderText = "النسبة";
                dataGridView1.Columns[5].HeaderText = "الكلية";
                dataGridView1.Columns[6].HeaderText = "المرحلة";
                dataGridView1.Columns[7].HeaderText = "الهاتف";
                dataGridView1.Columns[8].HeaderText = "العنوان";
                dataGridView1.Columns[9].HeaderText = "ملاحظات";
                dataGridView1.Columns[10].HeaderText = "الصورة";

                MainForm frm = new MainForm();
                this.Icon = frm.Icon;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtImage_TextChanged(object sender, EventArgs e)
        {
            SetImg();
        }

        private void txtImage_Click(object sender, EventArgs e)
        {
            txtImage.Text = MsgFrom.GetImgPath();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Length >= 3)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var grade = new Graduate
                        {
                            StudentName = txtName.Text,
                            GraduateYear = int.Parse(txtGeadeYear.Text),
                            Grade = cbxGrade1.SelectedItem.ToString(),
                            Percent = int.Parse(txtPresent.Text),
                            CollageId = (int)cbxCollage1.SelectedValue,
                            StageId = (int)cbxStage1.SelectedValue,
                            Mobile = txtPhone.Text,
                            Addres = txtAddress.Text,
                            ImagePath = txtImage.Text,
                            Note = txtNote.Text

                        };
                        db.Graduates.Add(grade);
                        db.SaveChanges();
                        MsgFrom.Added();
                        Clear();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    var grade = db.Graduates.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Graduates.Remove(grade);
                        db.SaveChanges();
                        MsgFrom.Removed();
                        Clear();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Length >= 3)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        var grade = db.Graduates.FirstOrDefault(x => x.Id == id);

                        grade.StudentName = txtName.Text;
                        grade.GraduateYear = int.Parse(txtGeadeYear.Text);
                        grade.Grade = cbxGrade1.SelectedItem.ToString();
                        grade.Percent = int.Parse(txtPresent.Text);
                        grade.CollageId = (int)cbxCollage1.SelectedValue;
                        grade.StageId = (int)cbxStage1.SelectedValue;
                        grade.Mobile = txtPhone.Text;
                        grade.Addres = txtAddress.Text;
                        grade.ImagePath = txtImage.Text;
                        grade.Note = txtNote.Text;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(grade).State = EntityState.Modified;
                            db.SaveChanges();
                            MsgFrom.Updated();
                            Clear();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtGeadeYear.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                cbxGrade1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtPresent.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                cbxCollage1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                cbxStage1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtPhone.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                txtAddress.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtNote.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtImage.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();

                if (txtImage.Text != "")
                {
                    pictureBox1.Image = Image.FromFile(txtImage.Text.Replace(@"\", @"\\"));
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxGrade2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchGrade();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchText();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            SearchYear();
        }

        private void cbxCollage2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchCollage();
        }

        private void cbxStage2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchStage();
        }

        private void txtPresent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGeadeYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
