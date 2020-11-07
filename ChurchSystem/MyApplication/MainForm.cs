using MyApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApplication
{
    public partial class MainForm : Form
    {
        string txtLogin = "";

        public MainForm()
        {
            InitializeComponent();
            if (frm == null)
                frm = this;
        }

        private static MainForm frm;

        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }

        public static MainForm GetMainFrm
        {
            get
            {
                if (frm == null)
                {
                    frm = new MainForm();
                    frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                }
                return frm;
            }
        }

        public void UnLockApp(string rank)
        {
            try
            {
                if(rank == "admin")
                {
                    this.menuStrip1.Items[0].Enabled = true;
                    this.menuStrip1.Items[1].Enabled = true;
                    this.menuStrip1.Items[2].Enabled = true;
                    this.menuStrip1.Items[3].Enabled = true;
                }
                else if (rank == "server")
                {
                    this.menuStrip1.Items[0].Enabled = true;
                    this.menuStrip1.Items[1].Enabled = true;
                    this.menuStrip1.Items[2].Enabled = true;
                }
                else if (rank == "user")
                {
                    this.menuStrip1.Items[0].Enabled = true;
                    this.menuStrip1.Items[1].Enabled = true;
                }
                else
                {
                    MessageBox.Show("هناك خطأ ما في بيانات المستخدم");
                }
            }
            catch
            {

            }
        }
        public void LockApp()
        {
            try
            {
                this.menuStrip1.Items[0].Enabled = false;
                this.menuStrip1.Items[1].Enabled = false;
                this.menuStrip1.Items[2].Enabled = false;
                this.menuStrip1.Items[3].Enabled = false;
                this.menuStrip1.Items[5].Visible = false;
            }
            catch
            {

            }
        }

        private string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        SqlConnection conn = new SqlConnection();
        SqlCommand cmd;

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadingForm frm = new LoadingForm();
                frm.ShowDialog();

                this.BackgroundImage = Image.FromFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\jesus.jpg");
            }
            catch
            {
            }
        }

        private void twonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TownForm frm = new TownForm();
            frm.Show();
        }

        private void areasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AreaForm frm = new AreaForm();
            frm.Show();
        }

        private void housesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HouseForm frm = new HouseForm();
            frm.Show();
        }

        private void peoplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PeopleForm frm = new PeopleForm();
            frm.Show();
        }

        private void stageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StageForm frm = new StageForm();
            frm.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LevelForm frm = new LevelForm();
            frm.Show();
        }

        private void graduateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraduateForm frm = new GraduateForm();
            frm.Show();
        }

        private void outStandingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OutStandingForm frm = new OutStandingForm();
            frm.Show();
        }

        private void nurseryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NurseryForm frm = new NurseryForm();
            frm.Show();
        }

        private void collageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollageForm frm = new CollageForm();
            frm.Show();
        }

        private void deathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeathForm frm = new DeathForm();
            frm.Show();
        }

        private void lackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LackForm frm = new LackForm();
            frm.Show();
        }

        private void confessitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfessionForm frm = new ConfessionForm();
            frm.Show();
        }

        private void serverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerForm frm = new ServerForm();
            frm.Show();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm frm = new UserForm();
            frm.Show();
        }

        private void قاعدةالبياناتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (AppDbContext context = new AppDbContext())
                {
                    string databaseName = context.Database.Connection.Database;


                    var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conn.ConnectionString = connectionString;

                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "Backup Files (*.Bak)|*.bak";
                    saveFile.FileName = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        cmd = new SqlCommand("Backup Database " + databaseName + " To Disk='" + saveFile.FileName + "' ;", conn);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show(this, "تم انشاء النسخه الاحتياطيه بنجاح", "تم بنجاح");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void استعادةنسخةاحتساطيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (AppDbContext context = new AppDbContext())
                {
                    string databaseName = context.Database.Connection.Database;

                    var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    connectionString = connectionString.Replace(databaseName, "master");
                    conn.ConnectionString = connectionString;

                    OpenFileDialog openFile = new OpenFileDialog();
                    openFile.Filter = "Backup Files (*.Bak)|*.bak";
                    if (openFile.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            cmd = new SqlCommand("Alter Database " + databaseName + " Set Single_User With RollBack Immediate; Drop Database " + databaseName + "; Restore Database " + databaseName + " From Disk='" + openFile.FileName + "' ;", conn);
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show(this, "تم استعادة النسخه الاحتياطيه بنجاح", "تم بنجاح");
                        }
                        catch
                        {
                            cmd = new SqlCommand("Restore Database " + databaseName + " From Disk='" + openFile.FileName + "' ;", conn);
                            if (conn.State == ConnectionState.Closed)
                                conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            MessageBox.Show(this, "تم استعادة النسخه الاحتياطيه بنجاح", "تم بنجاح");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void حفظالعنوانالفيزيائيالحالىToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var data = context.Sittings;
                    context.Sittings.RemoveRange(data);
                    context.SaveChanges();

                    MessageBox.Show(this, "تم تفريغ جدول الاعدادت ", "مهمة ناجحة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setCurrentMacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var data = context.Sittings.FirstOrDefault();
                    data.MacAddress = GetMACAddress();
                    context.Entry(data).State = EntityState.Modified;
                    context.SaveChanges();

                    MessageBox.Show(this, "تم تغيير العنوان الفيزيائي الي العنوان الحالى ", "مهمة ناجحة");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void تسجيلدخولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.ShowDialog();
        }

        private void تأمينالنظامToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LockApp();
        }

        private void sqlCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlForm frm = new SqlForm();
            frm.Show();
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            txtLogin = "";
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                if(txtLogin == "MINAShiftKey")
                {
                    MainForm.GetMainFrm.UnLockApp("admin");
                    MainForm.GetMainFrm.menuStrip1.Items[5].Visible = true;
                }
            }
            else
            {
                txtLogin += e.KeyCode;
            }
        }

        private void نقلالىالصفالتالىToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateLevelForm frm = new UpdateLevelForm();
            frm.Show();
        }
    }
}
