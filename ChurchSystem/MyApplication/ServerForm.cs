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
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }
        private void Clear()
        {
            try
            {
                txtNote.Clear();
                txtImage.Clear();
                pictureBox1.Image = null;

                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Servers
                               select new
                               {
                                   x.Id,
                                   x.People.PeopleName,
                                   x.People.House.HouseName,
                                   x.People.House.Area.AreaName,
                                   x.People.House.Area.Towns.TownName,
                                   x.Rank,
                                   x.People.House.Mobile,
                                   x.JoinDate,
                                   x.Note,
                                   x.ImagePath
                               };
                    

                    var cbx = db.Peoples.OrderBy(x => x.PeopleName).ToList();

                    cbxPeople.DataSource = cbx;
                    cbxPeople.DisplayMember = "PeopleName";
                    cbxPeople.ValueMember = "Id";


                    cbxPeople.SelectedIndex = -1;
                    cbxRank1.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد الخدام  " + data.Count().ToString();
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
                    var data = from x in db.Servers.Where(x => x.People.PeopleName.Contains(txtSearch.Text) || x.Note.Contains(txtSearch.Text))
                               select new
                               {
                                   x.Id,
                                   x.People.PeopleName,
                                   x.People.House.HouseName,
                                   x.People.House.Area.AreaName,
                                   x.People.House.Area.Towns.TownName,
                                   x.Rank,
                                   x.People.House.Mobile,
                                   x.JoinDate,
                                   x.Note,
                                   x.ImagePath
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد الخدام  " + data.Count().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchRank()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Servers.Where(x => x.Rank == cbxRank2.SelectedItem.ToString())
                               select new
                               {
                                   x.Id,
                                   x.People.PeopleName,
                                   x.People.House.HouseName,
                                   x.People.House.Area.AreaName,
                                   x.People.House.Area.Towns.TownName,
                                   x.Rank,
                                   x.People.House.Mobile,
                                   x.JoinDate,
                                   x.Note,
                                   x.ImagePath
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد الخدام  " + data.Count().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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


        private void ServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "الخادم";
                dataGridView1.Columns[2].HeaderText = "المنزل";
                dataGridView1.Columns[3].HeaderText = "المنطقة";
                dataGridView1.Columns[4].HeaderText = "القرية";
                dataGridView1.Columns[5].HeaderText = "الرتبة";
                dataGridView1.Columns[6].HeaderText = "هاتف";
                dataGridView1.Columns[7].HeaderText = "تاريخ الانضمام";
                dataGridView1.Columns[8].HeaderText = "ملاحظات";
                dataGridView1.Columns[9].HeaderText = "الصورة";

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
                cbxPeople.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cbxRank1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtNote.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtImage.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
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
                        var server = new Server
                        {
                            PeopleId = (int)cbxPeople.SelectedValue,
                            Rank = cbxRank1.SelectedItem.ToString(),
                            JoinDate = DateTime.Now.Date,
                            Note = txtNote.Text,
                            ImagePath = txtImage.Text
                        };
                        db.Servers.Add(server);
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
                    var server = db.Servers.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Servers.Remove(server);
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
                        var server = db.Servers.FirstOrDefault(x => x.Id == id);

                        server.PeopleId = (int)cbxPeople.SelectedValue;
                        server.Rank = cbxRank1.SelectedItem.ToString();
                        server.JoinDate = DateTime.Now.Date;
                        server.Note = txtNote.Text;
                        server.ImagePath = txtImage.Text;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(server).State = EntityState.Modified;
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

        private void txtImage_TextChanged(object sender, EventArgs e)
        {
            SetImg();
        }

        private void txtImage_Click(object sender, EventArgs e)
        {
            string path = MsgFrom.GetImgPath();
            txtImage.Text = path != "" ? path : txtImage.Text;
        }

        private void cbxRank2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchRank();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchText();
        }
    }
}
