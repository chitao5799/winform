using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BTL
{
    public partial class formLopTC : Form
    {
        string constr = ConnectString.GetConnectionString();
        public formLopTC()
        {
            InitializeComponent();
            
           /// ko hiểu tại sao thêm cái này chương trình bị dừng đột ngột khi nhập năm học
           
            //DateTime currentDateTime = DateTime.Now;
            //int nowYear = currentDateTime.Year;
            //string[] arrNamHocSource = new string[8];
            //int years = nowYear - 4;
            //for (int i = 0; i < 7; i++)
            //{
            //    arrNamHocSource[i] = (years.ToString() + '-' + (years + 1).ToString()).ToString();
            //    years++;
            //}
            //AutoCompleteStringCollection varAutoCompleteed = new AutoCompleteStringCollection();
            //varAutoCompleteed.AddRange(arrNamHocSource);

            //tbNamHoc.AutoCompleteCustomSource = varAutoCompleteed;
            //tbNamHoc.AutoCompleteMode = AutoCompleteMode.Suggest;
            //tbNamHoc.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        void Load_DB_toGridView()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                DataTable tbKhoa,tGiangVien,tMonHoc;

                using (SqlCommand cmd = new SqlCommand("select * from Khoa", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        tbKhoa = new DataTable();
                        ad.Fill(tbKhoa);
                    }
                }

                using (SqlCommand cmd = new SqlCommand("select * from MonHoc", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        tMonHoc = new DataTable();
                        ad.Fill(tMonHoc);
                    }
                }

                using (SqlCommand cmd = new SqlCommand("select * from GiangVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        tGiangVien = new DataTable();
                        ad.Fill(tGiangVien);
                    }
                }

                using (SqlCommand cmd = new SqlCommand("select * from LopTinChi", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);

                        int i = 0;
                        //đổi cột mã khoa ở bảng lớp tín chỉ thành tên khoa
                        for (i = 0; i < tb.Rows.Count; i++)
                        {
                            foreach (DataRow rowKhoa in tbKhoa.Rows)
                            {
                               
                                if (tb.Rows[i][5].ToString().TrimEnd() == rowKhoa[0].ToString().TrimEnd())
                                {
                                    tb.Rows[i][5] = rowKhoa[1];
                                    break;
                                }

                            }
                        }

                        i = 0;
                        //đổi cột mã môn ở bảng lớp tín chỉ thành tên môn
                        for (i = 0; i < tb.Rows.Count; i++)
                        {
                            foreach (DataRow rowMon in tMonHoc.Rows)
                            {

                                if (tb.Rows[i][1].ToString().TrimEnd() == rowMon[0].ToString().TrimEnd())
                                {
                                    tb.Rows[i][1] = rowMon[1];
                                    break;
                                }

                            }
                        }

                        i = 0;
                        //đổi cột mã giảng viên ở bảng lớp tín chỉ thành tên giảng viên
                        for (i = 0; i < tb.Rows.Count; i++)
                        {
                            foreach (DataRow rowGV in tGiangVien.Rows)
                            {

                                if (tb.Rows[i][2].ToString().TrimEnd() == rowGV[0].ToString().TrimEnd())
                                {
                                    tb.Rows[i][2] = rowGV[1];
                                    break;
                                }

                            }
                        }

                        dgvSV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        dgvSV.DataSource = tb;
                        dgvSV.Columns[0].HeaderText = "Mã Lớp";
                        dgvSV.Columns[1].HeaderText = "Tên Môn";
                        dgvSV.Columns[2].HeaderText = "Tên Giảng Viên";       
                        dgvSV.Columns[3].HeaderText = "Học kỳ";
                        dgvSV.Columns[4].HeaderText = "Năm Học";
                        dgvSV.Columns[5].HeaderText = "Khoa";
                        dgvSV.ReadOnly = true;
                     
                    }
                }
            }
        }
        void Load_combo_Khoa()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Khoa", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        //load tên khoa vào combobox phần tìm kiếm
                        cbbTimTheoKhoa.DataSource = tb;
                        cbbTimTheoKhoa.DisplayMember = "tenKhoa"; 
                        cbbTimTheoKhoa.ValueMember = "maKhoa";
                    }
                }
            }

        }
        void Load_combo_Khoa2()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Khoa", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        //load tên khoa vào combobox phần chi tiết 
                        cbbMaKhoa.DataSource = tb;
                        cbbMaKhoa.DisplayMember = "tenKhoa";
                        cbbMaKhoa.ValueMember = "maKhoa";
                    }
                }
            }

        }
        void Load_combo_Khoa_Mon()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                string query = @"select * from MonHoc where maKhoaFK=N'" + cbbMaKhoa.SelectedValue + "'";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        cbbMaMon.ResetText();
                        //load tên môn vào combobox phần chi tiết 
                        cbbMaMon.DataSource = tb;
                        cbbMaMon.DisplayMember = "tenMon";
                        cbbMaMon.ValueMember = "maMon";
                    }
                }
            }

        }
        void Load_combo_Khoa_GV()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from GiangVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        //load tên giảng viên vào combobox phần chi tiết 
                        cbbMaGV.DataSource = tb;
                        cbbMaGV.DisplayMember = "tenGV";
                        cbbMaGV.ValueMember = "maGV";
                    }
                }
            }

        }
        void refresh()
        {
            tbMaLop.ResetText();
            tbNamHoc.ResetText();                
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            tbMaLop.Enabled = true;
        }




        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void formSV_Load(object sender, EventArgs e)
        {
            Load_DB_toGridView();
            Load_combo_Khoa();
            Load_combo_Khoa2();
            Load_combo_Khoa_Mon();
            Load_combo_Khoa_GV();

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
 
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }
      

        //hàm kiểm tra tính đúng đắn của dữ liệu nhập vào trong các textbox.
        private bool KiemTraDuLieu()
        {
            if (tbNamHoc.Text.Trim() == "")
            {
                MessageBox.Show("Năm học không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }        
                  
            return true;
        }
        private string checkNamHoc(string namHocInput)
        {
            string[] arrTextNamHoc = namHocInput.Trim().Split('-');
            string namHoc = "";
            if (arrTextNamHoc.Length == 2)
            {
                int truoc, sau;
                try
                {
                    truoc = Convert.ToInt32(arrTextNamHoc[0].Trim());
                    sau = Convert.ToInt32(arrTextNamHoc[1].Trim());
                    if (sau - truoc == 1)
                    {
                        namHoc = arrTextNamHoc[0].Trim() + '-' + arrTextNamHoc[1].Trim();

                    }

                    else
                    {
                        MessageBox.Show("Năm học không hợp lệ, phải là <yyyy>-<yyyy+1>!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return "";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Năm học không hợp lệ, phải là <yyyy>-<yyyy+1>!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return "";
                }

            }
            else
            {
                MessageBox.Show("Năm học không hợp lệ, phải là <yyyy>-<yyyy+1>!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return "";
            }
            return namHoc;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieu() == false)
                return;
            string namHoc = checkNamHoc(tbNamHoc.Text.Trim());
            if (namHoc == "")
                return;
            string query = "select * from LopTinChi";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ltc");
                    DataTable dt = ds.Tables["ltc"];

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["maLopTC"].ToString().Trim() == tbMaLop.Text.Trim())
                        {
                            row["namHoc"] = namHoc;                          
                            row["hocKy"] = numericUpDownHocky.Value;
                            row["maMonFK"] = cbbMaMon.SelectedValue;
                            row["maGiangVienFK"] = cbbMaGV.SelectedValue;
                            row["maKhoaFK"] = cbbMaKhoa.SelectedValue;
                            break;
                        }
                    }
                    da.Update(ds, "ltc");
                    Load_DB_toGridView();
                    refresh();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Sửa thất bại, lỗi là:" + err.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void dgvSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //kiểm tra xem có click vào dòng mới ở dưới cùng không.        
            if (dgvSV.CurrentRow.Cells["maLopTC"].Value.ToString() != "")
            {
                tbMaLop.Text = dgvSV.CurrentRow.Cells["maLopTC"].Value.ToString().Trim();
                tbMaLop.Enabled = false;
                tbNamHoc.Text = dgvSV.CurrentRow.Cells["namHoc"].Value.ToString().Trim();             
                numericUpDownHocky.Value =Convert.ToInt32(dgvSV.CurrentRow.Cells["hocKy"].Value.ToString().Trim());  
                
                int j; 
                for (j = 0; j < cbbMaKhoa.Items.Count; j++)
                {
                    DataRowView rows = (DataRowView)cbbMaKhoa.Items[j];
                   
                    if (rows.Row["tenKhoa"].ToString() == dgvSV.CurrentRow.Cells["maKhoaFK"].Value.ToString())
                    {
                        cbbMaKhoa.SelectedValue = rows.Row["maKhoa"];
                        break;
                    }
                }

                
                for (j = 0; j < cbbMaGV.Items.Count; j++)
                {
                    DataRowView rows = (DataRowView)cbbMaGV.Items[j];

                    if (rows.Row["tenGV"].ToString() == dgvSV.CurrentRow.Cells["maGiangVienFK"].Value.ToString())
                    {
                        cbbMaGV.SelectedValue = rows.Row["maGV"];
                        break;
                    }
                }

                for (j = 0; j < cbbMaMon.Items.Count; j++)
                {
                    DataRowView rows = (DataRowView)cbbMaMon.Items[j];

                    if (rows.Row["tenMon"].ToString() == dgvSV.CurrentRow.Cells["maMonFK"].Value.ToString())
                    {
                        cbbMaMon.SelectedValue = rows.Row["maMon"];
                        break;
                    }
                }

                btnSua.Enabled = true;             
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
            }
            else
            {
                refresh();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có muốn xóa không?", "Thong bao", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {

                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    string query = @"delete from LopTinChi where maLopTC=N'"+tbMaLop.Text.Trim()+"'";
                    using (SqlCommand cmd =new SqlCommand(query,cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        Load_DB_toGridView();
                        refresh();
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieu() == false)
                return;
            string namHoc = checkNamHoc(tbNamHoc.Text.Trim());
            if (namHoc == "")
                return;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                string query = "select * from LopTinChi";
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ltc");
                    DataTable dt = ds.Tables["ltc"];
                    DataRow row = dt.NewRow();
                    row["maLopTC"] = tbMaLop.Text.Trim();
                    row["namHoc"] = namHoc;
                    row["hocKy"] = numericUpDownHocky.Value;
                    row["maKhoaFK"] = cbbMaKhoa.SelectedValue;
                    row["maMonFK"] = cbbMaMon.SelectedValue;
                    row["maGiangVienFK"] = cbbMaGV.SelectedValue;

                    dt.Rows.Add(row);
                    da.Update(ds, "ltc");
                    Load_DB_toGridView();
                    refresh();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Không thể thêm, lỗi là:" + err.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            Load_DB_toGridView();
            refresh();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            /* left join MonHoc on LopTinChi.maMonFK = MonHoc.maMon
                                                        left join GiangVien on LopTinChi.maLopTC = GiangVien.maGV*/
            //tìm kiếm theo khoa
            if (tbTimKiemTheoTen.Text.Trim() == "nhập mã lớp" && tbTimTheoNam.Text=="")
            {
                string tim_sql = @"select maLopTC,MonHoc.tenMon as N'maMonFK',GiangVien.tenGV as N'maGiangVienFK',hocKy,namHoc,tenKhoa as N'maKhoaFK'
                                        from LopTinChi left join Khoa on LopTinChi.maKhoaFK = Khoa.maKhoa
                                                        left join MonHoc on LopTinChi.maMonFK = MonHoc.maMon
                                                        left join GiangVien on LopTinChi.maLopTC = GiangVien.maGV
                                    where LopTinChi.maKhoaFK=N'" + cbbTimTheoKhoa.SelectedValue + "'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);
                    dgvSV.DataSource = tb;      
                    refresh();
                }
            }
            else if (tbTimKiemTheoTen.Text.Trim() != "nhập mã lớp" )//tìm theo ma lop 
            {

                string tim_sql = @"select maLopTC,MonHoc.tenMon as N'maMonFK',GiangVien.tenGV as N'maGiangVienFK',hocKy,namHoc,tenKhoa as N'maKhoaFK'
                                        from LopTinChi left join Khoa on LopTinChi.maKhoaFK = Khoa.maKhoa
                                                        left join MonHoc on LopTinChi.maMonFK = MonHoc.maMon
                                                        left join GiangVien on LopTinChi.maLopTC = GiangVien.maGV
                                    where LopTinChi.maLopTC=N'" + tbTimKiemTheoTen.Text.Trim() + "'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);                  
                    dgvSV.DataSource = tb;
                    tbTimKiemTheoTen.Text = "nhập mã lớp"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                    refresh();
                }
            }
            else if (tbTimKiemTheoTen.Text.Trim() == "nhập mã lớp" && tbTimTheoNam.Text != "")//tìm theo năm học 
            {
                string[] arrNam = tbTimTheoNam.Text.Split('-');
                try
                {
                    arrNam[0] = arrNam[0].Trim();
                    arrNam[1] = arrNam[1].Trim();
                    int nam1 = Convert.ToInt32(arrNam[0]);
                    int nam2 = Convert.ToInt32(arrNam[1]);
                    if (nam2 - nam1 != 1)
                    {
                        MessageBox.Show("Năm học chưa đúng, phải là: <yyyy>-<yyyy+1>", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Năm học không hợp lệ, phải là: <yyyy>-<yyyy+1>", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tim_sql = @"select maLopTC,MonHoc.tenMon as N'maMonFK',GiangVien.tenGV as N'maGiangVienFK',hocKy,namHoc,tenKhoa as N'maKhoaFK'
                                        from LopTinChi left join Khoa on LopTinChi.maKhoaFK = Khoa.maKhoa
                                                        left join MonHoc on LopTinChi.maMonFK = MonHoc.maMon
                                                        left join GiangVien on LopTinChi.maLopTC = GiangVien.maGV
                                    where LopTinChi.namHoc like N'" + arrNam[0]+"%-%"+arrNam[1] + "%'"+ "and LopTinChi.maKhoaFK=N'"+cbbTimTheoKhoa.SelectedValue+"'";
             
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);
                    dgvSV.DataSource = tb;
                    tbTimKiemTheoTen.Text = "nhập mã lớp"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                    refresh();
                }
            }

        }

        private void tbTimKiemTheoTen_Enter(object sender, EventArgs e)
        {
            if (tbTimKiemTheoTen.Text == "nhập mã lớp")
            {
                tbTimKiemTheoTen.Text = "";
                tbTimKiemTheoTen.ForeColor = Color.Black;
            }
        }

        private void tbTimKiemTheoTen_Leave(object sender, EventArgs e)
        {
            if (tbTimKiemTheoTen.Text == "")
            {
                tbTimKiemTheoTen.Text = "nhập mã lớp";
                tbTimKiemTheoTen.ForeColor = Color.Gray;
            }
        }

        //private void tbNgaysinh_Click(object sender, EventArgs e)
        //{

        //}

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbbMaKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_combo_Khoa_Mon();
        }
    }
}
