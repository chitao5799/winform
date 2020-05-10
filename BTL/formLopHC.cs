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
    public partial class formLopHC : Form
    {
        string constr = ConnectString.GetConnectionString();
        public formLopHC()
        {
            InitializeComponent();
        }
        void Load_DB_toGridView()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                DataTable tbKhoa,tGiangVien;

                using (SqlCommand cmd = new SqlCommand("select * from Khoa", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        tbKhoa = new DataTable();
                        ad.Fill(tbKhoa);
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

                using (SqlCommand cmd = new SqlCommand("select * from LopHanhChinh", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);

                        int i = 0;
                        //đổi cột mã khoa ở bảng lớp thành tên khoa
                        for (i = 0; i < tb.Rows.Count; i++)
                        {
                            foreach (DataRow rowKhoa in tbKhoa.Rows)
                            {
                               
                                if (tb.Rows[i][2].ToString().TrimEnd() == rowKhoa[0].ToString().TrimEnd())
                                {
                                    tb.Rows[i][2] = rowKhoa[1];
                                    break;
                                }

                            }
                        }


                        i = 0;
                        //đổi cột mã giảng viên ở bảng lớp thành tên giảng viên
                        for (i = 0; i < tb.Rows.Count; i++)
                        {
                            foreach (DataRow rowGV in tGiangVien.Rows)
                            {

                                if (tb.Rows[i][1].ToString().TrimEnd() == rowGV[0].ToString().TrimEnd())
                                {
                                    tb.Rows[i][1] = rowGV[1];
                                    break;
                                }

                            }
                        }

                        dgvHC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        dgvHC.DataSource = tb;
                        dgvHC.Columns[0].HeaderText = "Mã Lớp";
                        dgvHC.Columns[1].HeaderText = "Tên Giảng Viên";       
                        dgvHC.Columns[2].HeaderText = "Khoa";
                        dgvHC.Columns[3].HeaderText = "Năm Nhập Học";
                        dgvHC.ReadOnly = true;
                     
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
        void Load_combo_Khoa_GV()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                string query = @"select * from GiangVien where maKhoaFK=N'"+cbbMaKhoa.SelectedValue+"'";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);
                        //load tên giảng viên vào combobox phần chi tiết 
                        cbbMaGV.ResetText();
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
            tbNamNhapHoc.ResetText();
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
            if (tbMaLop.Text.Trim()=="")
            {
                MessageBox.Show("mã lớp không được để trống!", "Thông báo", MessageBoxButtons.OK);
                return false;
            }

            if (tbNamNhapHoc.Text.Trim() == "")
            {
                MessageBox.Show("Năm nhập học sinh viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                int namnhaphoc = Convert.ToInt32(tbNamNhapHoc.Text);
                if (namnhaphoc <= 0)
                {
                    MessageBox.Show("Năm nhập học của viên phải là số nguyên lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Năm nhập học của viên phải là số nguyên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieu() == false)
                return;

            string query = "select * from LopHanhChinh";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "lhc");
                    DataTable dt = ds.Tables["lhc"];

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["maLopHC"].ToString().Trim() == tbMaLop.Text.Trim())
                        {
                            row["maGVChuNhiemFK"] = cbbMaGV.SelectedValue;
                            row["maKhoaFK"] = cbbMaKhoa.SelectedValue;
                            row["namNhapHocCuaSV"] = tbNamNhapHoc.Text.Trim();
                            break;
                        }
                    }
                    da.Update(ds, "lhc");
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
            if (dgvHC.CurrentRow.Cells["maLopHC"].Value.ToString() != "")
            {
                tbMaLop.Text = dgvHC.CurrentRow.Cells["maLopHC"].Value.ToString().Trim();
                tbNamNhapHoc.Text = dgvHC.CurrentRow.Cells["namNhapHocCuaSV"].Value.ToString().Trim();
                tbMaLop.Enabled = false;
 
                int j; 
                for (j = 0; j < cbbMaKhoa.Items.Count; j++)
                {
                    DataRowView rows = (DataRowView)cbbMaKhoa.Items[j];
                   
                    if (rows.Row["tenKhoa"].ToString() == dgvHC.CurrentRow.Cells["maKhoaFK"].Value.ToString())
                    {
                        cbbMaKhoa.SelectedValue = rows.Row["maKhoa"];
                        break;
                    }
                }

                
                for (j = 0; j < cbbMaGV.Items.Count; j++)
                {
                    DataRowView rows = (DataRowView)cbbMaGV.Items[j];

                    if (rows.Row["tenGV"].ToString() == dgvHC.CurrentRow.Cells["maGVChuNhiemFK"].Value.ToString())
                    {
                        cbbMaGV.SelectedValue = rows.Row["maGV"];
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
                    string query = @"delete from LopHanhChinh where maLopHC=N'"+tbMaLop.Text.Trim()+"'";
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
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                string query = "select * from LopHanhChinh";
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "lhc");
                    DataTable dt = ds.Tables["lhc"];
                    DataRow row = dt.NewRow();
                    row["maLopHC"] = tbMaLop.Text.Trim();
                    row["maKhoaFK"] = cbbMaKhoa.SelectedValue;
                    row["maGVChuNhiemFK"] = cbbMaGV.SelectedValue;
                    row["namNhapHocCuaSV"] = tbNamNhapHoc.Text.Trim();
                    dt.Rows.Add(row);
                    da.Update(ds, "lhc");
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
            if (tbTimKiemTheoTen.Text.Trim() == "nhập mã lớp" )
            {
                string tim_sql = @"select maLopHC,GiangVien.tenGV as N'maGVChuNhiemFK',tenKhoa as N'maKhoaFK',namNhapHocCuaSV
                                        from LopHanhChinh left join Khoa on LopHanhChinh.maKhoaFK = Khoa.maKhoa                                                
                                                        left join GiangVien on LopHanhChinh.maGVChuNhiemFK = GiangVien.maGV
                                    where LopHanhChinh.maKhoaFK=N'" + cbbTimTheoKhoa.SelectedValue + "'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);
                    dgvHC.DataSource = tb;      
                    refresh();
                }
            }
            else if (tbTimKiemTheoTen.Text.Trim() != "nhập mã lớp" )//tìm theo ma lop 
            {

                string tim_sql = @"select maLopHC,GiangVien.tenGV as N'maGVChuNhiemFK',tenKhoa as N'maKhoaFK',namNhapHocCuaSV
                                        from LopHanhChinh left join Khoa on LopHanhChinh.maKhoaFK = Khoa.maKhoa                                                
                                                        left join GiangVien on LopHanhChinh.maGVChuNhiemFK = GiangVien.maGV
                                    where LopHanhChinh.maLopHC=N'" + tbTimKiemTheoTen.Text.Trim() + "'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);                  
                    dgvHC.DataSource = tb;
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
            Load_combo_Khoa_GV();
        }
    }
}
