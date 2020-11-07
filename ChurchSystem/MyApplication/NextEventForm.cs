using MyApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApplication
{
    public partial class NextEventForm : Form
    {
        public NextEventForm()
        {
            InitializeComponent();
        }

        private void SearchNextEvent()
        {
            try
            {
                using (AppDbContext db = new AppDbContext())
                {
                    DateTime date = DateTime.Now.AddDays(7);

                    var data = from x in db.Deaths.Where(x => (x.FifteenDate <= date && x.FifteenDate >= DateTime.Now) || ( x.FortyDate <= date && x.FortyDate >= DateTime.Now) || (x.AnnualDate <= date && x.AnnualDate >= DateTime.Now))
                               select new
                               {
                                   x.DeceasedName,
                                   x.House.HouseName,
                                   x.House.Area.AreaName,
                                   x.House.Area.Towns.TownName,
                                   x.House.Mobile,
                                   x.DeathDate,
                                   x.FifteenDate,
                                   x.FortyDate,
                                   x.AnnualDate,
                                   x.Note
                               };

                    dataGridView1.DataSource = data.ToList();
                    this.Text = "اجمالى عدد الجنازات " + data.Count().ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NextEventForm_Load(object sender, EventArgs e)
        {
            SearchNextEvent();

            dataGridView1.Columns[0].HeaderText = "الاسم";
            dataGridView1.Columns[1].HeaderText = "المنزل";
            dataGridView1.Columns[2].HeaderText = "المنطقة";
            dataGridView1.Columns[3].HeaderText = "القرية";
            dataGridView1.Columns[4].HeaderText = "الهاتف";
            dataGridView1.Columns[5].HeaderText = "تاريخ الوفاة";
            dataGridView1.Columns[6].HeaderText = "النصف شهرى";
            dataGridView1.Columns[7].HeaderText = "الاربعين";
            dataGridView1.Columns[8].HeaderText = "السنوىة";
            dataGridView1.Columns[9].HeaderText = "ملاحظات";

        }
    }
}
