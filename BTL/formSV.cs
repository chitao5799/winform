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
    public partial class formSV : Form
    {
        string constr = ConnectString.GetConnectionString();
        public formSV()
        {
            InitializeComponent();
        }
        void Load_DB_toGridView()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {

                using (SqlCommand cmd = new SqlCommand("select * from SinhVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);
                      
                        dgvSV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        //foreach (DataGridViewColumn col in dgvSV.Columns)
                        //    col.SortMode = DataGridViewColumnSortMode.NotSortable;

                        dgvSV.DataSource = tb;
                        dgvSV.Columns[0].HeaderText = "Mã Sinh Viên";
                        dgvSV.Columns[1].HeaderText = "Họ Tên";
                        dgvSV.Columns[2].HeaderText = "Ngày Sinh (mm/dd/yy)";
                        dgvSV.Columns[3].HeaderText = "Giới Tính";
                        dgvSV.Columns[4].HeaderText = "Địa Chỉ";
                        dgvSV.Columns[5].HeaderText = "Số ĐT";
                        dgvSV.Columns[6].HeaderText = "Email";
                        dgvSV.Columns[7].HeaderText = "Lớp HC";
                      
                       
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
       
        void Load_combo_lop()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from LopHanhChinh", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ad.Fill(tb);                      
                        cbbMaLopHC.DataSource = tb;
                        cbbMaLopHC.DisplayMember = "maLopHC";
                        cbbMaLopHC.ValueMember = "maLopHC";
                    }
                }
            }

        }
        void refresh()
        {
            tbMaSV.ResetText();
            tbHoTen.ResetText();
            tbNgaysinh.Text = "mm/dd/yyyy"; tbNgaysinh.ForeColor = Color.Gray;
            tbDiaChi.ResetText();
            tbDiaChi.ResetText();
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            tbSoDt.ResetText();
            tbEmail.ResetText();      
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            tbMaSV.Enabled = true;
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
            Load_combo_lop();

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
        //private int kiemtra() // kiểm tra xam khóa chính, mã sv có tồn tại không không - có return 1.
        //{
        //    string pk = tbMaSv.Text.Trim();
        //    string sql = "select * from SinhVien where maSV='" + pk.ToString() + "'";
        //    using (SqlConnection cnn = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sql, cnn))
        //        {
        //            using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
        //            {
        //                DataTable tb = new DataTable();
        //                ad.Fill(tb);
        //                if (tb.Rows.Count > 0)
        //                    return 1;
        //                else return 0;
        //            }
        //        }
        //    }
        //}

        //hàm kiểm tra tính đúng đắn của dữ liệu nhập vào trong các textbox.
        private bool KiemTraDuLieu()
        {
            if (tbHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Họ tên sinh viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbNgaysinh.Text.Trim() == "")
            {
                MessageBox.Show("Ngày sinh sinh viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ sinh viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbSoDt.Text.Trim() == "")
            {
                MessageBox.Show("Số điện thoại sinh viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbEmail.Text.Trim() == "")
            {
                MessageBox.Show("Email sinh viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
          

            try
            {
                long sodt = Convert.ToInt64(tbSoDt.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số điện thoại chỉ gồm các chữ số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

          

            if (!rdoNam.Checked && !rdoNu.Checked)
            {
                MessageBox.Show("Giới tính giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string email = tbEmail.Text.Trim();
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                MessageBox.Show("Địa chỉ Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                string ns = tbNgaysinh.Text.Trim();
                string[] arr = ns.Split('/');
                string[] arrTemp = arr[2].Split(' ');
                arr[2] = arrTemp[0];
                new DateTime(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
            }
            catch (Exception)
            {
                MessageBox.Show("Ngày sinh của sinh viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieu() == false)
                return;

            string query = "select * from SinhVien";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "sv");
                    DataTable dt = ds.Tables["sv"];

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["maSV"].ToString().Trim() == tbMaSV.Text.Trim())
                        {
                            row["tenSV"] = tbHoTen.Text.Trim();
                            row["ngaySinh"] = tbNgaysinh.Text.Trim();
                            if (rdoNam.Checked)
                                row["gioiTinh"] = "Nam";
                            else
                                row["gioiTinh"] = "Nữ";
                            row["diaChi"] = tbDiaChi.Text.Trim();
                            row["soDienThoai"] = tbSoDt.Text.Trim();
                            row["email"] = tbEmail.Text.Trim();                 
                            row["maLopHC_FK"] = cbbMaLopHC.SelectedValue;
                            break;
                        }
                    }
                    da.Update(ds, "sv");
                    Load_DB_toGridView();
                    refresh();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Ngày sinh không hợp lệ, lỗi là:" + err.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void dgvSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //kiểm tra xem có click vào dòng mới ở dưới cùng không.        
            if (dgvSV.CurrentRow.Cells["maSV"].Value.ToString() != "")
            {
                tbMaSV.Text = dgvSV.CurrentRow.Cells["maSV"].Value.ToString().Trim();
                tbMaSV.Enabled = false;
                tbHoTen.Text = dgvSV.CurrentRow.Cells["tenSV"].Value.ToString().Trim();
                tbNgaysinh.Text = dgvSV.CurrentRow.Cells["ngaySinh"].Value.ToString(); tbNgaysinh.ForeColor = Color.Black;
                tbDiaChi.Text = dgvSV.CurrentRow.Cells["diaChi"].Value.ToString().Trim();
                tbSoDt.Text = dgvSV.CurrentRow.Cells["soDienThoai"].Value.ToString().Trim();
                tbEmail.Text = dgvSV.CurrentRow.Cells["email"].Value.ToString().Trim();
                cbbMaLopHC.SelectedValue = dgvSV.CurrentRow.Cells["maLopHC_FK"].Value.ToString();
              
                if (dgvSV.CurrentRow.Cells["gioiTinh"].Value.ToString().Trim().Equals("nam", StringComparison.InvariantCultureIgnoreCase))
                    rdoNam.Checked = true;
                else rdoNu.Checked = true;

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
                    string query = @"delete from SinhVien where maSV=N'"+tbMaSV.Text.Trim()+"'";
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
                string query = "select * from SinhVien";
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "sv");
                    DataTable dt = ds.Tables["sv"];
                    DataRow row = dt.NewRow();
                    row["maSV"] = tbMaSV.Text.Trim();
                    row["tenSV"] = tbHoTen.Text.Trim();
                    row["ngaySinh"] = tbNgaysinh.Text.Trim();
                    if (rdoNam.Checked)
                        row["gioiTinh"] = "Nam";
                    else
                        row["gioiTinh"] = "Nữ";
                    row["diaChi"] = tbDiaChi.Text.Trim();
                    row["soDienThoai"] = tbSoDt.Text.Trim();
                    row["email"] = tbEmail.Text.Trim();
                    row["maLopHC_FK"] = cbbMaLopHC.SelectedValue;
 
                    dt.Rows.Add(row);
                    da.Update(ds, "sv");
                    Load_DB_toGridView();
                    refresh();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Thêm thất bại, lỗi là:" + err.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //tìm kiếm theo khoa
            if(tbTimKiemTheoTen.Text.Trim() == "nhập tên hoặc mã sv" && tbTimTheoLop.Text.Trim() == "")
            {     
                string tim_sql = @"select maSV,tenSV,ngaySinh,gioiTinh,diaChi,sv.soDienThoai,email,maLopHC_FK,namNhapHocCuaSV,tenKhoa
                                    from SinhVien sv,LopHanhChinh lhc, Khoa
                                    where sv.maLopHC_FK=lhc.maLopHC and lhc.maKhoaFK= Khoa.maKhoa and maKhoa =N'" + cbbTimTheoKhoa.SelectedValue+"'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);
                    dgvSV.DataSource = tb;
                    dgvSV.Columns[8].HeaderText = "Năm nhập học";
                    dgvSV.Columns[9].HeaderText = "Khoa";                  
                    refresh();
                }
            }
            else if(tbTimKiemTheoTen.Text.Trim() != "nhập tên hoặc mã sv" && tbTimTheoLop.Text.Trim() == "")//tìm theo ten sv||ma sv kết hợp với tìm theo khoa
            {

                //string tim_sql = @"select * from SinhVien where maSV=N'" + tbTimKiemTheoTen.Text.Trim() + "'";
                //tìm kiếm theo mã sv             
                string tim_sql = @"select maSV,tenSV,ngaySinh,gioiTinh,diaChi,sv.soDienThoai,email,maLopHC_FK,namNhapHocCuaSV,tenKhoa
                                from SinhVien sv,LopHanhChinh lhc, Khoa
                                where sv.maLopHC_FK=lhc.maLopHC and lhc.maKhoaFK= Khoa.maKhoa and sv.maSV=N'" + tbTimKiemTheoTen.Text.Trim() + "'";//+ " and maKhoa=N'" + cbbTimTheoKhoa.SelectedValue + "'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);

                    //nếu tìm theo mã mà ko có kết quả nào thì tìm theo tên và khoa
                    if (tb.Rows.Count < 1)
                    {
                        //string tim = @"select * from SinhVien where " + "tenSV like N'%" + tbTimKiemTheoTen.Text.Trim() + "%'";
                        string tim = @"select maSV,tenSV,ngaySinh,gioiTinh,diaChi,sv.soDienThoai,email,maLopHC_FK,namNhapHocCuaSV,tenKhoa
                        from SinhVien sv,LopHanhChinh lhc, Khoa
                        where sv.maLopHC_FK=lhc.maLopHC and lhc.maKhoaFK= Khoa.maKhoa and tenSV like N'%" + tbTimKiemTheoTen.Text.Trim() + "%'"+ " and maKhoa=N'" + cbbTimTheoKhoa.SelectedValue + "'"; 
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(tim, cnn);
                        DataTable t = new DataTable();
                        da.Fill(t);
                        dgvSV.DataSource = t;
                        dgvSV.Columns[8].HeaderText = "Năm nhập học";
                        dgvSV.Columns[9].HeaderText = "Khoa";
                        if (t.Rows.Count < 1)
                        {
                            MessageBox.Show("Không có kết quả trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        
                     
                        tbTimKiemTheoTen.Text = "nhập tên hoặc mã sv"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                        refresh();
                        return;
                    }
                    dgvSV.DataSource = tb;
                    dgvSV.Columns[8].HeaderText = "Năm nhập học";
                    dgvSV.Columns[9].HeaderText = "Khoa";
                    tbTimKiemTheoTen.Text = "nhập tên hoặc mã sv"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                    refresh();
                }
            }
            else if(tbTimKiemTheoTen.Text.Trim() == "nhập tên hoặc mã sv" && tbTimTheoLop.Text.Trim()!="")//tìm sv theo lớp hc
            {
                string tim_sql = @"select maSV,tenSV,ngaySinh,gioiTinh,diaChi,sv.soDienThoai,email,maLopHC_FK,namNhapHocCuaSV,tenKhoa
                                    from SinhVien sv,LopHanhChinh lhc, Khoa
                                    where sv.maLopHC_FK=lhc.maLopHC and lhc.maKhoaFK= Khoa.maKhoa and lhc.maLopHC=N'" + tbTimTheoLop.Text.Trim() + "'"; //+"and maKhoa=N'"+cbbTimTheoKhoa.SelectedValue+"'"               
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);
                    dgvSV.DataSource = tb;
                    dgvSV.Columns[8].HeaderText = "Năm nhập học";
                    dgvSV.Columns[9].HeaderText = "Khoa";
                    if (tb.Rows.Count < 1)
                    {
                        MessageBox.Show("Không có kết quả trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                  
                    tbTimTheoLop.Text = "";
                    refresh();
                }
            }
            else//tìm theo cả tên sv + tên lớp + tên khoa
            {
               
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                   
                        //string tim = @"select * from SinhVien where " + "tenSV like N'%" + tbTimKiemTheoTen.Text.Trim() + "%'";
                        string tim = @"select maSV,tenSV,ngaySinh,gioiTinh,diaChi,sv.soDienThoai,email,maLopHC_FK,namNhapHocCuaSV,tenKhoa
                                        from SinhVien sv,LopHanhChinh lhc, Khoa
                                        where sv.maLopHC_FK=lhc.maLopHC and lhc.maKhoaFK= Khoa.maKhoa and sv.maLopHC_FK=N'" + tbTimTheoLop.Text.Trim() +
                                            "' and maKhoa=N'" + cbbTimTheoKhoa.SelectedValue + "' and tenSV like N'%" + tbTimKiemTheoTen.Text.Trim() + "%'";
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = new SqlCommand(tim, cnn);
                        DataTable t = new DataTable();
                        da.Fill(t);
                       dgvSV.DataSource = t;
                        if (t.Rows.Count < 1)
                        {
                            MessageBox.Show("Không có kết quả trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        
                        tbTimKiemTheoTen.Text = "nhập tên hoặc mã sv"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                        tbTimTheoLop.Text = "";
                        refresh();
                  
                }
            } 
        }

        private void tbTimKiemTheoTen_Enter(object sender, EventArgs e)
        {
            if (tbTimKiemTheoTen.Text == "nhập tên hoặc mã sv")
            {
                tbTimKiemTheoTen.Text = "";
                tbTimKiemTheoTen.ForeColor = Color.Black;
            }
        }

        private void tbTimKiemTheoTen_Leave(object sender, EventArgs e)
        {
            if (tbTimKiemTheoTen.Text == "")
            {
                tbTimKiemTheoTen.Text = "nhập tên hoặc mã sv";
                tbTimKiemTheoTen.ForeColor = Color.Gray;
            }
        }

        private void tbNgaysinh_Click(object sender, EventArgs e)
        {

        }

        private void tbNgaysinh_Enter(object sender, EventArgs e)
        {
            if (tbNgaysinh.Text == "mm/dd/yyyy")
            {
                tbNgaysinh.Text = "";
                tbNgaysinh.ForeColor = Color.Black;
            }
        }

        private void tbNgaysinh_Leave(object sender, EventArgs e)
        {

            if (tbNgaysinh.Text == "")
            {
                tbNgaysinh.Text = "mm/dd/yyyy";
                tbNgaysinh.ForeColor = Color.Gray;
            }
        }
    }
}
