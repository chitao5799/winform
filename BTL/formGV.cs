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
    public partial class formGV : Form
    {
        string constr = ConnectString.GetConnectionString();
        public formGV()
        {
            InitializeComponent();
        }
        void Load_DB_toGridView()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                DataTable tbKhoa;

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
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);
                        int i = 0;
                        for (i = 0; i < tb.Rows.Count; i++)
                        {
                            foreach (DataRow rowKhoa in tbKhoa.Rows)
                            {
                                //đổi cột mã khoa ở bảng giảng viên thành tên khoa
                                if (tb.Rows[i][9].ToString().TrimEnd() == rowKhoa[0].ToString().TrimEnd())
                                {
                                    tb.Rows[i][9] = rowKhoa[1];
                                    break;
                                }

                            }
                        }


                        dgvSV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                        dgvSV.DataSource = tb;
                        dgvSV.Columns[0].HeaderText = "Mã Giảng Viên";
                        dgvSV.Columns[1].HeaderText = "Họ Tên";
                        dgvSV.Columns[2].HeaderText = "Ngày Sinh (mm/dd/yy)";
                        dgvSV.Columns[3].HeaderText = "Giới Tính";
                        dgvSV.Columns[4].HeaderText = "Địa Chỉ";
                        dgvSV.Columns[5].HeaderText = "Số ĐT";
                        dgvSV.Columns[6].HeaderText = "Email";
                        dgvSV.Columns[7].HeaderText = "Học hàm";
                        dgvSV.Columns[8].HeaderText = "Học vị";
                        dgvSV.Columns[9].HeaderText = "Khoa";
                       
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
                        //load tên khoa vào combobox phần chi tiết về gv
                        cbbMaKhoa.DataSource = tb;
                        cbbMaKhoa.DisplayMember = "tenKhoa";
                        cbbMaKhoa.ValueMember = "maKhoa";
                    }
                }
            }

        }
        void refresh()
        {
            tbMaGV.ResetText();
            tbHoTen.ResetText();
            tbNgaysinh.Text = "mm/dd/yyyy"; tbNgaysinh.ForeColor = Color.Gray;
            tbDiaChi.ResetText();
            tbDiaChi.ResetText();
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            tbSoDt.ResetText();
            tbEmail.ResetText();
            tbHocHam.ResetText();
            tbHocVi.ResetText();
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            tbMaGV.Enabled = true;
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
            if (tbHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Họ tên giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbNgaysinh.Text.Trim() == "")
            {
                MessageBox.Show("Ngày sinh giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Địa chỉ giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbSoDt.Text.Trim() == "")
            {
                MessageBox.Show("Số điện thoại giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbEmail.Text.Trim() == "")
            {
                MessageBox.Show("Email giảng viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if(!rdoNam.Checked && !rdoNu.Checked)
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
                long sodt = Convert.ToInt64(tbSoDt.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số điện thoại chỉ gồm các chữ số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            DateTime ngaysinh = new DateTime();
            try
            {
                string ns = tbNgaysinh.Text.Trim();
                string[] arr = ns.Split('/');
                string[] arrTemp = arr[2].Split(' ');
                arr[2] = arrTemp[0];
                ngaysinh=new DateTime(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
            }
            catch (Exception)
            {
                MessageBox.Show("Ngày sinh của giảng viên không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if( DateTime.Now.Year-ngaysinh.Year<=22)
            {
                MessageBox.Show("Tuổi của giảng viên phải lớn hơn 22 tuổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieu() == false)
                return;

            string query = "select * from GiangVien";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "gv");
                    DataTable dt = ds.Tables["gv"];

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["maGV"].ToString().Trim() == tbMaGV.Text.Trim())
                        {
                            row["tenGV"] = tbHoTen.Text.Trim();
                            row["ngaySinh"] = tbNgaysinh.Text.Trim();
                            if (rdoNam.Checked)
                                row["gioiTinh"] = "Nam";
                            else
                                row["gioiTinh"] = "Nữ";
                            row["diaChi"] = tbDiaChi.Text.Trim();
                            row["soDienThoai"] = tbSoDt.Text.Trim();
                            row["email"] = tbEmail.Text.Trim();
                            row["hocHam"] = tbHocHam.Text.Trim();
                            row["hocVi"] = tbHocVi.Text.Trim();
                            row["maKhoaFK"] = cbbMaKhoa.SelectedValue;
                            break;
                        }
                    }
                    da.Update(ds, "gv");
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
            if (dgvSV.CurrentRow.Cells["maGV"].Value.ToString() != "")
            {
                tbMaGV.Text = dgvSV.CurrentRow.Cells["maGV"].Value.ToString().Trim();
                tbMaGV.Enabled = false;
                tbHoTen.Text = dgvSV.CurrentRow.Cells["tenGV"].Value.ToString().Trim();
                tbNgaysinh.Text = dgvSV.CurrentRow.Cells["ngaySinh"].Value.ToString(); tbNgaysinh.ForeColor = Color.Black;
                tbDiaChi.Text = dgvSV.CurrentRow.Cells["diaChi"].Value.ToString().Trim();
                tbSoDt.Text = dgvSV.CurrentRow.Cells["soDienThoai"].Value.ToString();
                tbEmail.Text = dgvSV.CurrentRow.Cells["email"].Value.ToString().Trim();
                tbHocHam.Text = dgvSV.CurrentRow.Cells["hocHam"].Value.ToString().Trim();
                tbHocVi.Text = dgvSV.CurrentRow.Cells["hocVi"].Value.ToString().Trim();
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
                    string query = @"delete from GiangVien where maGV=N'"+tbMaGV.Text.Trim()+"'";
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
                string query = "select * from GiangVien";
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "gv");
                    DataTable dt = ds.Tables["gv"];
                    DataRow row = dt.NewRow();
                    row["maGV"] = tbMaGV.Text.Trim();
                    row["tenGV"] = tbHoTen.Text.Trim();
                    row["ngaySinh"] = tbNgaysinh.Text.Trim();
                    if (rdoNam.Checked)
                        row["gioiTinh"] = "Nam";
                    else
                        row["gioiTinh"] = "Nữ";
                    row["diaChi"] = tbDiaChi.Text.Trim();
                    row["soDienThoai"] = tbSoDt.Text.Trim();
                    row["email"] = tbEmail.Text.Trim();
                    row["hocHam"] = tbHocHam.Text.Trim();
                    row["hocVi"] = tbHocVi.Text.Trim();
                    row["maKhoaFK"] = cbbMaKhoa.SelectedValue;
 
                    dt.Rows.Add(row);
                    da.Update(ds, "gv");
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
            //tìm kiếm theo khoa
            if (tbTimKiemTheoTen.Text.Trim() == "nhập tên hoặc mã gv")
            {
                string tim_sql = @"select maGV,tenGV,ngaySinh,gioiTinh,diaChi,GiangVien.soDienThoai,email,hocHam,hocVi,tenKhoa as N'maKhoaFK'
                                        from GiangVien left join Khoa on GiangVien.maKhoaFK = Khoa.maKhoa
                                    where GiangVien.maKhoaFK=N'" + cbbTimTheoKhoa.SelectedValue + "'";
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
            else if (tbTimKiemTheoTen.Text.Trim() != "nhập tên hoặc mã gv")//tìm theo ten sv||ma sv kết hợp với tìm theo khoa
            {

                
                //tìm kiếm theo mã sv kết hợp với mã khoa              
                string tim_sql = @"select maGV,tenGV,ngaySinh,gioiTinh,diaChi,GiangVien.soDienThoai,email,hocHam,hocVi,tenKhoa as N'maKhoaFK'
                                    from GiangVien left join Khoa on GiangVien.maKhoaFK = Khoa.maKhoa
                                    where maGV=N'" + tbTimKiemTheoTen.Text.Trim() + "' and maKhoa=N'" + cbbTimTheoKhoa.SelectedValue + "'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);
                    //nếu tìm theo mã mà ko có kết quả nào thì tìm theo tên kết hợp theo khoa
                    if (tb.Rows.Count < 1)
                    {
                        
                        string tim = @"select maGV,tenGV,ngaySinh,gioiTinh,diaChi,GiangVien.soDienThoai,email,hocHam,hocVi,tenKhoa as N'maKhoaFK'
                                    from GiangVien left join Khoa on GiangVien.maKhoaFK = Khoa.maKhoa
                                    where " + "tenGV like N'%" + tbTimKiemTheoTen.Text.Trim() + "%'" + " and maKhoa=N'" + cbbTimTheoKhoa.SelectedValue + "'";
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


                        tbTimKiemTheoTen.Text = "nhập tên hoặc mã gv"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                        refresh();
                        return;
                    }
                    dgvSV.DataSource = tb;
                    tbTimKiemTheoTen.Text = "nhập tên hoặc mã gv"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                    refresh();
                }
            }


        }

        private void tbTimKiemTheoTen_Enter(object sender, EventArgs e)
        {
            if (tbTimKiemTheoTen.Text == "nhập tên hoặc mã gv")
            {
                tbTimKiemTheoTen.Text = "";
                tbTimKiemTheoTen.ForeColor = Color.Black;
            }
        }

        private void tbTimKiemTheoTen_Leave(object sender, EventArgs e)
        {
            if (tbTimKiemTheoTen.Text == "")
            {
                tbTimKiemTheoTen.Text = "nhập tên hoặc mã gv";
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
