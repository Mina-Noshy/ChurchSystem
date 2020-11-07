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
    public partial class DeathForm : Form
    {
        public DeathForm()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            try
            {
                txtName.Clear();
                txtSearch.Clear();
                txtDay.Clear();
                txtNote.Clear();

                chx15.Checked = false;
                chx40.Checked = false;
                chx360.Checked = false;

                rdo15.Checked = false;
                rdo40.Checked = false;
                rdo365.Checked = false;

                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Deaths
                               select new
                               {
                                   x.Id,
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.Note,
                                   x.FifteenDate,
                                   x.Fifteen,
                                   x.FortyDate,
                                   x.Forty,
                                   x.AnnualDate,
                                   x.Annual
                               };
                    
                    var cbx = db.Houses.OrderBy(x => x.HouseName).ToList();

                    cbxHome1.DataSource = cbx;
                    cbxHome1.DisplayMember = "HouseName";
                    cbxHome1.ValueMember = "Id";

                    var cbx33 = db.Houses.OrderBy(x => x.HouseName).ToList();

                    cbxHome2.DataSource = cbx33;
                    cbxHome2.DisplayMember = "HouseName";
                    cbxHome2.ValueMember = "Id";

                    cbxHome1.SelectedIndex = -1;
                    cbxHome2.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الوفيات  " + data.Count().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchHouse()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)cbxHome2.SelectedValue;

                    var data = from x in db.Deaths.Where(x => x.HouseId == id)
                               select new
                               {
                                   x.Id,
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.Note,
                                   x.FifteenDate,
                                   x.Fifteen,
                                   x.FortyDate,
                                   x.Forty,
                                   x.AnnualDate,
                                   x.Annual
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الوفيات  " + data.Count().ToString();

                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchText()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Deaths.Where(x => x.DeceasedName.Contains(txtSearch.Text) || x.Note.Contains(txtSearch.Text) || x.House.HouseName.Contains(txtSearch.Text))
                               select new
                               {
                                   x.Id,
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.Note,
                                   x.FifteenDate,
                                   x.Fifteen,
                                   x.FortyDate,
                                   x.Forty,
                                   x.AnnualDate,
                                   x.Annual
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الوفيات  " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchDeathDate()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {

                    var data = from x in db.Deaths.Where(x => x.DeathDate == dateTimePicker2.Value.Date)
                               select new
                               {
                                   x.Id,
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.Note,
                                   x.FifteenDate,
                                   x.Fifteen,
                                   x.FortyDate,
                                   x.Forty,
                                   x.AnnualDate,
                                   x.Annual
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الوفيات  " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchDay()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int day = int.Parse(txtDay.Text);

                    DateTime date = DateTime.Now.Date.AddDays(day * -1);

                    var data = from x in db.Deaths.Where(x => x.DeathDate == date)
                               select new
                               {
                                   x.Id,
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.Note,
                                   x.FifteenDate,
                                   x.Fifteen,
                                   x.FortyDate,
                                   x.Forty,
                                   x.AnnualDate,
                                   x.Annual
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الوفيات  " + data.Count().ToString();

                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void Search15()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Deaths.Where(x => !x.Fifteen)
                               select new
                               {
                                   x.Id,
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.Note,
                                   x.FifteenDate,
                                   x.Fifteen,
                                   x.FortyDate,
                                   x.Forty,
                                   x.AnnualDate,
                                   x.Annual
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الوفيات  " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search40()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Deaths.Where(x => !x.Forty)
                               select new
                               {
                                   x.Id,
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.Note,
                                   x.FifteenDate,
                                   x.Fifteen,
                                   x.FortyDate,
                                   x.Forty,
                                   x.AnnualDate,
                                   x.Annual
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الوفيات  " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search365()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Deaths.Where(x => !x.Annual)
                               select new
                               {
                                   x.Id,
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.Note,
                                   x.FifteenDate,
                                   x.Fifteen,
                                   x.FortyDate,
                                   x.Forty,
                                   x.AnnualDate,
                                   x.Annual
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الوفيات  " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeathForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "المتوفى";
                dataGridView1.Columns[2].HeaderText = "المنزل";
                dataGridView1.Columns[3].HeaderText = "المنطقة";
                dataGridView1.Columns[4].HeaderText = "القرية";
                dataGridView1.Columns[5].HeaderText = "هاتف";
                dataGridView1.Columns[6].HeaderText = "تاريخ الوفاة";
                dataGridView1.Columns[7].HeaderText = "ملاحظات";

                dataGridView1.Columns[8].HeaderText = "تاريخ النصف شهرى";
                dataGridView1.Columns[9].HeaderText = "نصف شهرى";

                dataGridView1.Columns[10].HeaderText = "تاريخ الاربعين";
                dataGridView1.Columns[11].HeaderText = "الاربعين";

                dataGridView1.Columns[12].HeaderText = "تاريخ السنويه";
                dataGridView1.Columns[13].HeaderText = "السنوىة";


                //dataGridView1.Columns[1].CellTemplate.ValueType = typeof(bool);
                //dataGridView1.Columns[2].CellTemplate.ValueType = typeof(bool);
                //dataGridView1.Columns[3].CellTemplate.ValueType = typeof(bool);

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
                        var death = new Death
                        {
                            HouseId = (int)cbxHome1.SelectedValue,
                            DeceasedName = txtName.Text,
                            DeathDate = dateTimePicker1.Value.Date,
                            Note = txtNote.Text,
                            Fifteen = (bool)chx15.Checked,
                            Forty = (bool)chx40.Checked,
                            Annual = (bool)chx360.Checked,
                            FifteenDate = dateTimePicker1.Value.Date.AddDays(15),
                            FortyDate = dateTimePicker1.Value.Date.AddDays(40),
                            AnnualDate = dateTimePicker1.Value.Date.AddYears(1)
                        };
                        db.Deaths.Add(death);
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
                    var death = db.Deaths.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Deaths.Remove(death);
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
                        var death = db.Deaths.FirstOrDefault(x => x.Id == id);

                        death.HouseId = (int)cbxHome1.SelectedValue;
                        death.DeceasedName = txtName.Text;
                        death.DeathDate = dateTimePicker1.Value.Date;
                        death.Note = txtNote.Text;
                        death.Fifteen = (bool)chx15.Checked;
                        death.Forty = (bool)chx40.Checked;
                        death.Annual = (bool)chx360.Checked;
                        death.FifteenDate = dateTimePicker1.Value.Date.AddDays(15);
                        death.FortyDate = dateTimePicker1.Value.Date.AddDays(40);
                        death.AnnualDate = dateTimePicker1.Value.Date.AddYears(1);

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(death).State = EntityState.Modified;
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
                cbxHome1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[6].Value;
                txtNote.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                chx15.Checked = (bool)dataGridView1.CurrentRow.Cells[9].Value;
                chx40.Checked = (bool)dataGridView1.CurrentRow.Cells[11].Value;
                chx360.Checked = (bool)dataGridView1.CurrentRow.Cells[13].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxHome2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchHouse();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchText();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            SearchDeathDate();
        }

        private void txtDay_TextChanged(object sender, EventArgs e)
        {
            SearchDay();
        }

        private void rdo15_CheckedChanged(object sender, EventArgs e)
        {
            Search15();
        }

        private void rdo40_CheckedChanged(object sender, EventArgs e)
        {
            Search40();
        }

        private void rdo365_CheckedChanged(object sender, EventArgs e)
        {
            Search365();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NextEventForm frm = new NextEventForm();
            frm.Show();
        }

        private void txtDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
