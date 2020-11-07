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
    public partial class ConfessionForm : Form
    {
        public ConfessionForm()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            try
            {
                txtNote.Clear();

                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Confessions
                               select new
                               {
                                   x.Id,
                                   x.People.PeopleName,
                                   x.People.House.HouseName,
                                   x.People.House.Area.AreaName,
                                   x.People.House.Area.Towns.TownName,
                                   x.People.House.Mobile,
                                   x.LastConfessionDate,
                                   x.Note
                               };
                    
                    var cbx = db.Peoples.OrderBy(x => x.PeopleName).ToList();

                    cbxPeople.DataSource = cbx;
                    cbxPeople.DisplayMember = "PeopleName";
                    cbxPeople.ValueMember = "Id";

                    // ************************************ //
                    var cbxArea = db.Areas.OrderBy(x => x.AreaName).ToList();
                    cbxArea1.DataSource = cbxArea;
                    cbxArea1.DisplayMember = "AreaName";
                    cbxArea1.ValueMember = "Id";

                    cbxArea1.SelectedIndex = -1;
                    cbxPeople.SelectedIndex = -1;
                    cbxMounthDone.SelectedIndex = -1;
                    cbxMounthUnDone.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد الاعترافات  " + data.Count().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchMounthDone()
        {
            try
            {

                using (AppDbContext db = new AppDbContext())
                {
                    int month = int.Parse(cbxMounthDone.SelectedItem.ToString());
                    int year = DateTime.Now.Year;

                    var data = from x in db.Confessions.Where(x => x.LastConfessionDate.Month == month && x.LastConfessionDate.Year == year)
                               select new
                               {
                                   x.Id,
                                   x.People.PeopleName,
                                   x.People.House.HouseName,
                                   x.People.House.Area.AreaName,
                                   x.People.House.Area.Towns.TownName,
                                   x.People.House.Mobile,
                                   x.LastConfessionDate,
                                   x.Note
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد الاعترافات  " + data.Count().ToString();

                    var cbx = db.Peoples.OrderBy(x => x.PeopleName).ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchMounthUnDone()
        {
            try
            {

                using (AppDbContext db = new AppDbContext())
                {
                    int month = int.Parse(cbxMounthUnDone.SelectedItem.ToString());
                    int year = DateTime.Now.Year;

                    var data = from x in db.Confessions.Where(x => x.LastConfessionDate.Month != month && x.LastConfessionDate.Year == year)
                               select new
                               {
                                   x.Id,
                                   x.People.PeopleName,
                                   x.People.House.HouseName,
                                   x.People.House.Area.AreaName,
                                   x.People.House.Area.Towns.TownName,
                                   x.People.House.Mobile,
                                   x.LastConfessionDate,
                                   x.Note
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد الاعترافات  " + data.Count().ToString();

                    var cbx = db.Peoples.OrderBy(x => x.PeopleName).ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void ConfessionForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "الشخص";
                dataGridView1.Columns[2].HeaderText = "المنزل";
                dataGridView1.Columns[3].HeaderText = "المنطقة";
                dataGridView1.Columns[4].HeaderText = "القرية";
                dataGridView1.Columns[5].HeaderText = "هاتف";
                dataGridView1.Columns[6].HeaderText = "تاريخ الاعتراف";
                dataGridView1.Columns[7].HeaderText = "ملاحظات";

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
                if (cbxPeople.SelectedIndex != -1)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var conf = new Confession
                        {
                            PeopleId = (int)cbxPeople.SelectedValue,
                            LastConfessionDate = dateTimePicker1.Value.Date,
                            Note = txtNote.Text,
                        };
                        db.Confessions.Add(conf);
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
                    var conf = db.Confessions.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Confessions.Remove(conf);
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
                if (cbxPeople.SelectedIndex != -1)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        var conf = db.Confessions.FirstOrDefault(x => x.Id == id);

                        conf.PeopleId = (int)cbxPeople.SelectedValue;
                        conf.LastConfessionDate = dateTimePicker1.Value.Date;
                        conf.Note = txtNote.Text;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(conf).State = EntityState.Modified;
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
                cbxPeople.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[6].Value;
                txtNote.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxMounth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxMounthDone.SelectedIndex != 0)
            {
                SearchMounthDone();
            }
        }

        private void cbxMounthUnDone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMounthUnDone.SelectedIndex != 0)
            {
                SearchMounthUnDone();
            }
        }

        private void cbxArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)cbxArea1.SelectedValue;
                    var cbx = db.Peoples.Where(x => x.House.AreaId == id).OrderBy(x => x.PeopleName).ToList();
                    cbxPeople.DataSource = cbx;
                    cbxPeople.DisplayMember = "PeopleName";
                    cbxPeople.ValueMember = "Id";
                }
            }
            catch
            {

            }
        }
    }
}
