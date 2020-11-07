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
    public partial class LackForm : Form
    {
        public LackForm()
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
                    var data = from x in db.Lacks
                               select new
                               {
                                   x.Id,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.LastLackDate,
                                   x.Note
                               };
                    

                    var cbx = db.Houses.OrderBy(x => x.HouseName).ToList();
                    cbxHome1.DataSource = cbx;
                    cbxHome1.DisplayMember = "HouseName";
                    cbxHome1.ValueMember = "Id";
                    // *************************************** //
                    var cbxArea = db.Areas.OrderBy(x => x.AreaName).ToList();
                    cbxArea1.DataSource = cbxArea;
                    cbxArea1.DisplayMember = "AreaName";
                    cbxArea1.ValueMember = "Id";

                    var cbxArea33 = db.Areas.OrderBy(x => x.AreaName).ToList();
                    cbxArea2.DataSource = cbxArea33;
                    cbxArea2.DisplayMember = "AreaName";
                    cbxArea2.ValueMember = "Id";


                    cbxHome1.SelectedIndex = -1;
                    cbxArea1.SelectedIndex = -1;
                    cbxArea2.SelectedIndex = -1;
                    cbxMounthDone.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الافتقادات  " + data.Count().ToString();
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
                    int id = (int)cbxArea2.SelectedValue;
                    int month = int.Parse(cbxMounthDone.SelectedItem.ToString());
                    int year = DateTime.Now.Year;

                    var data = from x in db.Lacks.Where(x => x.LastLackDate.Month == month && x.LastLackDate.Year == year && x.House.AreaId == id)
                               select new
                               {
                                   x.Id,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.LastLackDate,
                                   x.Note
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الافتقادات  " + data.Count().ToString();

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
                    int id = (int)cbxArea2.SelectedValue;
                    int month = int.Parse(cbxMounthUnDone.SelectedItem.ToString());
                    int year = DateTime.Now.Year;

                    var data = from x in db.Lacks.Where(x => x.LastLackDate.Month != month && x.LastLackDate.Year == year && x.House.AreaId == id)
                               select new
                               {
                                   x.Id,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.LastLackDate,
                                   x.Note
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد الافتقادات  " + data.Count().ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void LackForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "المنزل";
                dataGridView1.Columns[2].HeaderText = "المنطقة";
                dataGridView1.Columns[3].HeaderText = "القرية";
                dataGridView1.Columns[4].HeaderText = "هاتف";
                dataGridView1.Columns[5].HeaderText = "تاريخ الافتقاد";
                dataGridView1.Columns[6].HeaderText = "ملاحظات";

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
                if (cbxHome1.SelectedIndex != -1)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var lack = new Lack
                        {
                            HouseId = (int)cbxHome1.SelectedValue,
                            LastLackDate = dateTimePicker1.Value.Date,
                            Note = txtNote.Text,
                        };
                        db.Lacks.Add(lack);
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
                    var lack = db.Lacks.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Lacks.Remove(lack);
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
                if (cbxHome1.SelectedIndex != -1)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        var lack = db.Lacks.FirstOrDefault(x => x.Id == id);

                        lack.HouseId = (int)cbxHome1.SelectedValue;
                        lack.LastLackDate = dateTimePicker1.Value.Date;
                        lack.Note = txtNote.Text;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(lack).State = EntityState.Modified;
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

        private void cbxMounth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxMounthDone.SelectedIndex != 0 && cbxArea2.SelectedIndex != -1)
            {
                SearchMounthDone();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbxHome1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[5].Value;
                txtNote.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void cbxArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)cbxArea1.SelectedValue;
                    var cbx = db.Houses.Where(x => x.AreaId == id).OrderBy(x => x.HouseName).ToList();
                    cbxHome1.DataSource = cbx;
                    cbxHome1.DisplayMember = "HouseName";
                    cbxHome1.ValueMember = "Id";
                }
            }
            catch
            {

            }
        }

        private void cbxMounthUnDone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMounthUnDone.SelectedIndex != 0 && cbxArea2.SelectedIndex != -1)
            {
                SearchMounthUnDone();
            }
        }

    }
}
