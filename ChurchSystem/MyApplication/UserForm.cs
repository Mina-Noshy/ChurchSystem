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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            try
            {
                txtUser.Clear();
                txtPass.Clear();

                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Users
                               select new
                               {
                                   x.Id,
                                   x.UserName,
                                   x.Password,
                                   x.Rank,
                                   x.EntryDate
                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.UserName).ToList();

                    cbxRank.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void UserForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "اسم المستخدم";
                dataGridView1.Columns[2].HeaderText = "كلمة المرور";
                dataGridView1.Columns[3].HeaderText = "الرتبة";
                dataGridView1.Columns[4].HeaderText = "تاريخ الاضافة";

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
                if (txtUser.Text.Length >= 3 && cbxRank.SelectedIndex != 0 && cbxRank.SelectedIndex != -1)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var user = new User
                        {
                            UserName = txtUser.Text,
                            Password = txtPass.Text,
                            Rank = cbxRank.SelectedItem.ToString(),
                            EntryDate = DateTime.Now.Date
                        };
                        db.Users.Add(user);
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
                    var user = db.Users.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Users.Remove(user);
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
                if (txtUser.Text.Length >= 3 && cbxRank.SelectedIndex != 0 && cbxRank.SelectedIndex != -1)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        var user = db.Users.FirstOrDefault(x => x.Id == id);

                        user.UserName = txtUser.Text;
                        user.Password = txtPass.Text;
                        user.Rank = cbxRank.SelectedItem.ToString();

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(user).State = EntityState.Modified;
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
                txtUser.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cbxRank.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
