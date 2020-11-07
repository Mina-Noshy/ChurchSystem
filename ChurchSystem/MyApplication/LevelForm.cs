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
    public partial class LevelForm : Form
    {
        public LevelForm()
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
                    var data = from x in db.Levels
                               select new
                               {
                                   x.Id,
                                   x.LevelName,
                                   x.Stage.StageName
                               };

                    var cbx = db.Stages.OrderBy(x => x.StageName).ToList();

                    comboBox1.DataSource = cbx;
                    comboBox1.DisplayMember = "StageName";
                    comboBox1.ValueMember = "Id";

                    comboBox1.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.LevelName).ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void QualificationForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "المؤهل";
                dataGridView1.Columns[2].HeaderText = "المرحلة";

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
                        var qualif = new Level
                        {
                            LevelName = textBox1.Text,
                            StageId = (int)comboBox1.SelectedValue
                        };
                        db.Levels.Add(qualif);
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
                    var qualif = db.Levels.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Levels.Remove(qualif);
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
                        var qualif = db.Levels.FirstOrDefault(x => x.Id == id);
                        qualif.LevelName = textBox1.Text;
                        qualif.StageId = (int)comboBox1.SelectedValue;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(qualif).State = EntityState.Modified;
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
            catch (Exception ex)
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

                    var data = from x in db.Levels.Where(y => y.StageId == id)
                               select new
                               {
                                   x.Id,
                                   x.LevelName,
                                   x.Stage.StageName
                               };

                    var cbx = db.Stages.OrderBy(x => x.StageName).ToList();

                    dataGridView1.DataSource = data.OrderBy(x => x.LevelName).ToList();

                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }
    }
}
