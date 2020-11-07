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
    public partial class HouseForm : Form
    {
        public HouseForm()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            try
            {
                textBox1.Clear();
                textBox2.Clear();

                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Houses
                               select new
                               {
                                   x.Id,
                                   x.HouseName,
                                   x.Mobile,
                                   x.Area.AreaName,
                                   x.Area.Towns.TownName
                               };

                    var cbxArea = db.Areas.OrderBy(x => x.AreaName).ToList();

                    cbxArea1.DataSource = cbxArea;
                    cbxArea1.DisplayMember = "AreaName";
                    cbxArea1.ValueMember = "Id";
                    // *************************************** //
                    var cbxArea33 = db.Areas.OrderBy(x => x.AreaName).ToList();
                    cbxArea2.DataSource = cbxArea33;
                    cbxArea2.DisplayMember = "AreaName";
                    cbxArea2.ValueMember = "Id";

                    cbxArea2.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد المنازل  " + data.Count().ToString();
                    textBox1.Focus();
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchArea()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)cbxArea2.SelectedValue;

                    var data = from x in db.Houses.Where(x => x.AreaId == id)
                               select new
                               {
                                   x.Id,
                                   x.HouseName,
                                   x.Mobile,
                                   x.Area.AreaName,
                                   x.Area.Towns.TownName
                               };

                    var cbxArea = db.Areas.OrderBy(x => x.AreaName).ToList();

                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد المنازل  " + data.Count().ToString();
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }


        private void HouseForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "منزل السيد";
                dataGridView1.Columns[2].HeaderText = "هاتف";
                dataGridView1.Columns[3].HeaderText = "المنطقة";
                dataGridView1.Columns[4].HeaderText = "القرية";

                MainForm frm = new MainForm();
                this.Icon = frm.Icon;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length >= 3)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var house = new House
                        {
                            HouseName = textBox1.Text,
                            Mobile = textBox2.Text,
                            AreaId = (int)cbxArea1.SelectedValue
                        };
                        db.Houses.Add(house);
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
                    var house = db.Houses.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Houses.Remove(house);
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
                if (textBox1.Text.Length >= 3)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        var house = db.Houses.FirstOrDefault(x => x.Id == id);
                        house.HouseName = textBox1.Text;
                        house.Mobile = textBox2.Text;
                        house.AreaId = (int)cbxArea1.SelectedValue;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(house).State = EntityState.Modified;
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
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                cbxArea1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxArea2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchArea();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
