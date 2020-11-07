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
    public partial class AreaForm : Form
    {
        public AreaForm()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            try
            {
                textBox1.Clear();

                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Areas
                               select new
                               {
                                   x.Id,
                                   x.AreaName,
                                   x.Towns.TownName
                               };

                    var cbx = db.Towns.OrderBy(x => x.TownName).ToList();

                    comboBox1.DataSource = cbx;
                    comboBox1.DisplayMember = "TownName";
                    comboBox1.ValueMember = "Id";

                    comboBox1.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.AreaName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AreaForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "المنطقة";
                dataGridView1.Columns[2].HeaderText = "القرية";

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
                if (textBox1.Text.Length >= 3)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var area = new Area
                        {
                            AreaName = textBox1.Text,
                            TownId = (int)comboBox1.SelectedValue
                        };
                        db.Areas.Add(area);
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
                    var area = db.Areas.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Areas.Remove(area);
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
                        var area = db.Areas.FirstOrDefault(x => x.Id == id);
                        area.AreaName = textBox1.Text;
                        area.TownId = (int)comboBox1.SelectedValue;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(area).State = EntityState.Modified;
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
                comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)comboBox1.SelectedValue;

                    var data = from x in db.Areas.Where(y => y.TownId == id)
                               select new
                               {
                                   x.Id,
                                   x.AreaName,
                                   x.Towns.TownName
                               };

                    var cbx = db.Towns.OrderBy(x => x.TownName).ToList();

                    dataGridView1.DataSource = data.OrderBy(x => x.AreaName).ToList();
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }
    }
}
