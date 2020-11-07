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
    public partial class NurseryForm : Form
    {
        public NurseryForm()
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
                txtFather.Clear();
                txtPhone.Clear();
                txtAddress.Clear();
                txtImage.Clear();
                txtYear.Clear();
                txtSearch.Clear();
                txtNote.Clear();
                pictureBox1.Image = null;

                

                using (AppDbContext db = new AppDbContext())
                {
                    var data = db.Nurseries.ToList();

                    dataGridView1.DataSource = data.OrderBy(x => x.ChildName).ToList();

                    this.Text = "اجمالى عدد الاطفال  " + data.Count().ToString();

                }
                cbxLevel1.SelectedIndex = -1;
                cbxLevel2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchLevel()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = db.Nurseries.Where(x => x.Level == cbxLevel2.Text);

                    dataGridView1.DataSource = data.OrderBy(x => x.ChildName).ToList();

                    this.Text = "اجمالى عدد الاطفال  " + data.Count().ToString();

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
                    var data = db.Nurseries.Where(x => x.ChildName.Contains(txtSearch.Text) || x.FatherName.Contains(txtSearch.Text) || x.Mobile.Contains(txtSearch.Text) || x.Addres.Contains(txtSearch.Text));

                    dataGridView1.DataSource = data.OrderBy(x => x.ChildName).ToList();

                    this.Text = "اجمالى عدد الاطفال  " + data.Count().ToString();

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
                    int year = DateTime.Now.Year;
                    year -= int.Parse(txtYear.Text);

                    var data = db.Nurseries.Where(x => x.Birthdate.Year == year);
                    dataGridView1.DataSource = data.OrderBy(x => x.ChildName).ToList();

                    this.Text = "اجمالى عدد الاطفال  " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchBirthdate()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = db.Nurseries.Where(x => x.Birthdate == dateTimePicker2.Value.Date);
                    dataGridView1.DataSource = data.OrderBy(x => x.ChildName).ToList();

                    this.Text = "اجمالى عدد الاطفال  " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void NurseryForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "اسم الطفل";
                dataGridView1.Columns[2].HeaderText = "ولي الامر";
                dataGridView1.Columns[3].HeaderText = "الهاتف";
                dataGridView1.Columns[4].HeaderText = "العنوان";
                dataGridView1.Columns[5].HeaderText = "المرحلة";
                dataGridView1.Columns[6].HeaderText = "تاريخ الميلاد";
                dataGridView1.Columns[7].HeaderText = "ملاحظات";
                dataGridView1.Columns[8].HeaderText = "الصوره";

                MainForm frm = new MainForm();
                this.Icon = frm.Icon;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Length >= 3)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var child = new Nursery
                        {
                            ChildName = txtName.Text,
                            FatherName = txtFather.Text,
                            Addres = txtAddress.Text,
                            Level = cbxLevel1.SelectedItem.ToString(),
                            Mobile = txtPhone.Text,
                            Birthdate = dateTimePicker1.Value.Date,
                            ImagePath = txtImage.Text,
                            Note = txtNote.Text

                        };
                        db.Nurseries.Add(child);
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
                    var child = db.Nurseries.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Nurseries.Remove(child);
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
                        var child = db.Nurseries.FirstOrDefault(x => x.Id == id);

                        child.ChildName = txtName.Text;
                        child.FatherName = txtFather.Text;
                        child.Addres = txtAddress.Text;
                        child.Level = cbxLevel1.SelectedItem.ToString();
                        child.Mobile = txtPhone.Text;
                        child.Birthdate = dateTimePicker1.Value.Date;
                        child.ImagePath = txtImage.Text;
                        child.Note = txtNote.Text;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(child).State = EntityState.Modified;
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

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtFather.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtAddress.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                cbxLevel1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[6].Value;
                txtNote.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                txtImage.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

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

        private void txtImage_Click(object sender, EventArgs e)
        {
            txtImage.Text = MsgFrom.GetImgPath();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            SearchYear();
        }

        private void cbxLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchLevel();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchText();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            SearchBirthdate();
        }

        private void txtYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtImage_TextChanged(object sender, EventArgs e)
        {
            SetImg();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
