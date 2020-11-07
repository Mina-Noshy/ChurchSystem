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
    public partial class TownForm : Form
    {
        public TownForm()
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
                    dataGridView1.DataSource = db.Towns.OrderBy(x => x.TownName).ToList();
                }
            }
            catch(Exception ex)
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
                        var town = new Town
                        {
                            TownName = textBox1.Text
                        };
                        db.Towns.Add(town);
                        db.SaveChanges();
                        MsgFrom.Added();
                        Clear();
                    }
                }
            }
            catch(Exception ex)
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
                    int id = (int) dataGridView1.CurrentRow.Cells[0].Value;
                    var town = db.Towns.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Towns.Remove(town);
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
                        var town = db.Towns.FirstOrDefault(x => x.Id == id);
                        town.TownName = textBox1.Text;
                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(town).State = EntityState.Modified;
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

        private void TownsForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "القرية";
                dataGridView1.Columns[2].Visible = false;

                MainForm frm = new MainForm();
                this.Icon = frm.Icon;
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
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
