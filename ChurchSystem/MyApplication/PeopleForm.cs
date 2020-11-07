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
    public partial class PeopleForm : Form
    {
        public PeopleForm()
        {
            InitializeComponent();
        }

        //private string ReverseText(string text)
        //{
        //    char[] arr = text.ToCharArray();
        //    Array.Reverse(arr);
        //    return new string(arr);
        //}

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
                txtPhone.Clear();
                txtEmail.Clear();
                txtImage.Clear();
                txtYear.Clear();
                txtSearch.Clear();
                txtWork.Clear();
                txtNote.Clear();
                txtIdentityNumber.Clear();

                pictureBox1.Image = null;

                dateTimePicker3.Visible = false;
                label25.Visible = false;

                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Peoples
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    
                    var cbxHouse = db.Houses.OrderBy(x => x.HouseName).ToList();
                    cbxHouse1.DataSource = cbxHouse;
                    cbxHouse1.DisplayMember = "HouseName";
                    cbxHouse1.ValueMember = "Id";

                    var cbxHouse33 = db.Houses.OrderBy(x => x.HouseName).ToList();
                    cbxHouse2.DataSource = cbxHouse33;
                    cbxHouse2.DisplayMember = "HouseName";
                    cbxHouse2.ValueMember = "Id";

                    // *************************************** //
                    var cbxQualification = db.Levels.OrderBy(x => x.LevelName).ToList();
                    cbxQualification1.DataSource = cbxQualification;
                    cbxQualification1.DisplayMember = "LevelName";
                    cbxQualification1.ValueMember = "Id";

                    var cbxQualification33 = db.Levels.OrderBy(x => x.LevelName).ToList();
                    cbxQualification2.DataSource = cbxQualification33;
                    cbxQualification2.DisplayMember = "LevelName";
                    cbxQualification2.ValueMember = "Id";

                    // *************************************** //
                    var cbxCollage = db.Collages.OrderBy(x => x.CollageName).ToList();
                    cbxCollage1.DataSource = cbxCollage;
                    cbxCollage1.DisplayMember = "CollageName";
                    cbxCollage1.ValueMember = "Id";

                    var cbxCollage33 = db.Collages.OrderBy(x => x.CollageName).ToList();
                    cbxCollage2.DataSource = cbxCollage33;
                    cbxCollage2.DisplayMember = "CollageName";
                    cbxCollage2.ValueMember = "Id";

                    // *************************************** //
                    var cbxArea = db.Areas.OrderBy(x => x.AreaName).ToList();
                    cbxArea2.DataSource = cbxArea;
                    cbxArea2.DisplayMember = "AreaName";
                    cbxArea2.ValueMember = "Id";
                    // *************************************** //
                    var cbxTown33 = db.Towns.OrderBy(x => x.TownName).ToList();
                    cbxTown.DataSource = cbxTown33;
                    cbxTown.DisplayMember = "TownName";
                    cbxTown.ValueMember = "Id";
                    // *************************************** //
                    var cbxArea33 = db.Areas.OrderBy(x => x.AreaName).ToList();
                    cbxArea1.DataSource = cbxArea33;
                    cbxArea1.DisplayMember = "AreaName";
                    cbxArea1.ValueMember = "Id";

                    cbxGender1.SelectedIndex = -1;
                    cbxGender2.SelectedIndex = -1;

                    cbxSocial1.SelectedIndex = -1;
                    cbxSocial2.SelectedIndex = -1;

                    cbxHouse2.SelectedIndex = -1;

                    cbxQualification1.SelectedIndex = -1;
                    cbxQualification2.SelectedIndex = -1;

                    cbxCollage1.SelectedIndex = -1;
                    cbxCollage2.SelectedIndex = -1;

                    cbxArea2.SelectedIndex = -1;
                    cbxTown.SelectedIndex = -1;

                    dataGridView1.DataSource = data.OrderBy(x => x.HouseName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();
                    txtName.Focus();
                }
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
                    int id = int.Parse(cbxQualification2.SelectedValue.ToString());
                    var data = from x in db.Peoples.Where(x => x.LevelId == id)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchHouse()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)cbxHouse2.SelectedValue;

                    var data = from x in db.Peoples.Where(x => x.HouseId == id)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchGender()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Peoples.Where(x => x.Gender == cbxGender2.Text)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchSocialState()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    var data = from x in db.Peoples.Where(x => x.SocialStatus == cbxSocial2.Text)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchCollage()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = int.Parse(cbxCollage2.SelectedValue.ToString());
                    var data = from x in db.Peoples.Where(x => x.CollageId == id)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
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
                    int id = int.Parse(cbxArea2.SelectedValue.ToString());
                    var data = from x in db.Peoples.Where(x => x.House.AreaId == id)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchTown()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = int.Parse(cbxTown.SelectedValue.ToString());
                    var data = from x in db.Peoples.Where(x => x.House.Area.TownId == id)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
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
                    var data = from x in db.Peoples.Where(x => x.PeopleName.Contains(txtSearch.Text) || x.Mobile.Contains(txtSearch.Text) || x.Email.Contains(txtSearch.Text) || x.Work.Contains(txtSearch.Text) || x.Note.Contains(txtSearch.Text))
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch// (Exception ex)
            {
                //MessageBox.Show(ex.Message);
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

                    var data = from x in db.Peoples.Where(x => x.Birthdate.Year == year)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

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

                    var data = from x in db.Peoples.Where(x => x.Birthdate == dateTimePicker2.Value.Date)
                               select new
                               {
                                   x.Id,
                                   x.PeopleName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.Mobile,
                                   x.SocialStatus,
                                   x.Gender,
                                   x.Collage.CollageName,
                                   x.Level.LevelName,
                                   x.Work,
                                   x.IdentityNumber,
                                   x.Birthdate,
                                   x.Email,
                                   x.Note,
                                   x.ImagePath

                               };
                    dataGridView1.DataSource = data.OrderBy(x => x.PeopleName).ToList();

                    this.Text = "اجمالى عدد المواطنين " + data.Count().ToString();

                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void PeopleForm_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
                dataGridView1.Columns[0].HeaderText = "م";
                dataGridView1.Columns[1].HeaderText = "الاسم";
                dataGridView1.Columns[2].HeaderText = "المنزل";
                dataGridView1.Columns[3].HeaderText = "المنطقة";
                dataGridView1.Columns[4].HeaderText = "القرية";
                dataGridView1.Columns[5].HeaderText = "هاتف";
                dataGridView1.Columns[6].HeaderText = "الحالة";
                dataGridView1.Columns[7].HeaderText = "النوع";
                dataGridView1.Columns[8].HeaderText = "الكلية";
                dataGridView1.Columns[9].HeaderText = "الصف";
                dataGridView1.Columns[10].HeaderText = "العمل";
                dataGridView1.Columns[11].HeaderText = "الرقم القومى";
                dataGridView1.Columns[12].HeaderText = "تاريخ الميلاد";
                dataGridView1.Columns[13].HeaderText = "البريد";
                dataGridView1.Columns[14].HeaderText = "ملاحظات";
                dataGridView1.Columns[15].HeaderText = "الصورة";

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
                if(txtName.Text.Length >= 3)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        var people = new People
                        {
                            HouseId = (int)cbxHouse1.SelectedValue,
                            CollageId = (int)cbxCollage1.SelectedValue,
                            LevelId = (int)cbxQualification1.SelectedValue,
                            PeopleName = txtName.Text,
                            Mobile = txtPhone.Text,
                            Email = txtEmail.Text,
                            IdentityNumber = txtIdentityNumber.Text,
                            Work = txtWork.Text,
                            Birthdate = dateTimePicker1.Value.Date,
                            ImagePath = txtImage.Text,
                            Gender = cbxGender1.SelectedItem.ToString(),
                            SocialStatus = cbxSocial1.SelectedItem.ToString(),
                            Note = txtNote.Text
                        };
                        db.Peoples.Add(people);
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

        private void txtImage_Click(object sender, EventArgs e)
        {
            string path = MsgFrom.GetImgPath();
            txtImage.Text = path != "" ? path : txtImage.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    var people = db.Peoples.FirstOrDefault(x => x.Id == id);
                    if (MsgFrom.DoRemove() == DialogResult.Yes)
                    {
                        db.Peoples.Remove(people);
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
                        var people = db.Peoples.FirstOrDefault(x => x.Id == id);

                        people.HouseId = (int)cbxHouse1.SelectedValue;
                        people.CollageId = (int)cbxCollage1.SelectedValue;
                        people.LevelId = (int)cbxQualification1.SelectedValue;
                        people.PeopleName = txtName.Text;
                        people.Mobile = txtPhone.Text;
                        people.Email = txtEmail.Text;
                        people.IdentityNumber = txtIdentityNumber.Text;
                        people.Work = txtWork.Text;
                        people.Birthdate = dateTimePicker1.Value.Date;
                        people.ImagePath = txtImage.Text;
                        people.Gender = cbxGender1.SelectedItem.ToString();
                        people.SocialStatus = cbxSocial1.SelectedItem.ToString();
                        people.Note = txtNote.Text;

                        if (MsgFrom.DoUpdate() == DialogResult.Yes)
                        {
                            db.Entry(people).State = EntityState.Modified;
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
                cbxHouse1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                cbxSocial1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                cbxGender1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                cbxCollage1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                cbxQualification1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtWork.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                txtIdentityNumber.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[12].Value;
                txtEmail.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
                txtNote.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
                txtImage.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString();

                if(txtImage.Text != "")
                {
                    SetImg();
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

        private void cbxQualification2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchLevel();
        }

        private void cbxHouse2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchHouse();
        }

        private void cbxSocial2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchSocialState();
        }

        private void cbxGender2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchGender();
        }

        private void cbxArea2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchArea();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchText();
        }

        private void txtYear_TextChanged(object sender, EventArgs e)
        {
            SearchYear();
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

        private void txtIdentityNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cbxCollage2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchCollage();
        }

        private void cbxTown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchTown();
        }

        private void cbxArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using(AppDbContext db = new AppDbContext())
                {
                    int id = (int)cbxArea1.SelectedValue;
                    var cbx = db.Houses.Where(x => x.AreaId == id).OrderBy(x => x.HouseName).ToList();
                    cbxHouse1.DataSource = cbx;
                    cbxHouse1.DisplayMember = "HouseName";
                    cbxHouse1.ValueMember = "Id";
                }
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if(label25.Visible && dateTimePicker3.Visible)
                {
                    using (AppDbContext db = new AppDbContext())
                    {
                        int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        var people = db.Peoples.FirstOrDefault(x => x.Id == id);
                        if (MsgFrom.DoRemove("هل انت متأكد من رغبتك فى نقل الفرد الى الوفيات ؟") == DialogResult.Yes)
                        {

                            var death = new Death
                            {
                                HouseId = people.HouseId,
                                DeceasedName = people.PeopleName,
                                DeathDate = dateTimePicker3.Value.Date,
                                Note = people.Note,
                                Fifteen = false,
                                Forty = false,
                                Annual = false,
                                FifteenDate = dateTimePicker3.Value.Date.AddDays(15),
                                FortyDate = dateTimePicker3.Value.Date.AddDays(40),
                                AnnualDate = dateTimePicker3.Value.Date.AddYears(1)
                            };
                            db.Deaths.Add(death);

                            db.Peoples.Remove(people);

                            db.SaveChanges();
                            MsgFrom.Removed("تم نقل الفرد الى الوفيات");
                            Clear();
                        }

                    }
                }
                else
                {
                    dateTimePicker3.Visible = true;
                    label25.Visible = true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
