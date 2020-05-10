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
    public partial class formMonHoc : Form
    {
        string constr = ConnectString.GetConnectionString();
        public formMonHoc()
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

                using (SqlCommand cmd = new SqlCommand("select * from MonHoc", cnn))
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
                                //đổi cột mã khoa ở bảng môn học thành tên khoa
                                if (tb.Rows[i][3].ToString().TrimEnd() == rowKhoa[0].ToString().TrimEnd())
                                {
                                    tb.Rows[i][3] = rowKhoa[1];
                                    break;
                                }

                            }
                        }


                        dgvSV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


                        dgvSV.DataSource = tb;
                        dgvSV.Columns[0].HeaderText = "Mã Môn";
                        dgvSV.Columns[1].HeaderText = "Tên Môn";
                        dgvSV.Columns[2].HeaderText = "Số tín chỉ";       
                        dgvSV.Columns[3].HeaderText = "Khoa";
                       
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
            tbMaMon.ResetText();
            tbTenMon.ResetText();        
            tbSoTC.ResetText();         
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            tbMaMon.Enabled = true;
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
            if (tbTenMon.Text.Trim() == "")
            {
                MessageBox.Show("Tên môn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }        
            if (tbSoTC.Text.Trim() == "")
            {
                MessageBox.Show("Số tín chỉ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                long sotc = Convert.ToInt64(tbSoTC.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Số tín chỉ là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
                 
            return true;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieu() == false)
                return;

            string query = "select * from MonHoc";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "mh");
                    DataTable dt = ds.Tables["mh"];

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["maMon"].ToString().Trim() == tbMaMon.Text.Trim())
                        {
                            row["tenMon"] = tbTenMon.Text.Trim();                          
                            row["soTinChi"] = tbSoTC.Text.Trim();                         
                            row["maKhoaFK"] = cbbMaKhoa.SelectedValue;
                            break;
                        }
                    }
                    da.Update(ds, "mh");
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
            if (dgvSV.CurrentRow.Cells["maMon"].Value.ToString() != "")
            {
                tbMaMon.Text = dgvSV.CurrentRow.Cells["maMon"].Value.ToString().Trim();
                tbMaMon.Enabled = false;
                tbTenMon.Text = dgvSV.CurrentRow.Cells["tenMon"].Value.ToString().Trim();             
                tbSoTC.Text = dgvSV.CurrentRow.Cells["soTinChi"].Value.ToString().Trim();           
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
                    string query = @"delete from MonHoc where maMon=N'"+tbMaMon.Text.Trim()+"'";
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
                string query = "select * from MonHoc";
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "mh");
                    DataTable dt = ds.Tables["mh"];
                    DataRow row = dt.NewRow();
                    row["maMon"] = tbMaMon.Text.Trim();
                    row["tenMon"] = tbTenMon.Text.Trim();               
                    row["soTinChi"] = tbSoTC.Text.Trim();
                    row["maKhoaFK"] = cbbMaKhoa.SelectedValue;
 
                    dt.Rows.Add(row);
                    da.Update(ds, "mh");
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
            if (tbTimKiemTheoTen.Text.Trim() == "nhập tên hoặc mã môn")
            {
                string tim_sql = @"select maMon,tenMon,soTinChi,tenKhoa as N'maKhoaFK'
                                        from MonHoc left join Khoa on MonHoc.maKhoaFK = Khoa.maKhoa
                                    where MonHoc.maKhoaFK=N'" + cbbTimTheoKhoa.SelectedValue + "'";
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
            else if (tbTimKiemTheoTen.Text.Trim() != "nhập tên hoặc mã môn")//tìm theo ten môn||ma môn kết hợp với tìm theo khoa
            {

                
                //tìm kiếm theo mã môn kết hợp với mã khoa              
                string tim_sql = @"select maMon,tenMon,soTinChi,tenKhoa as N'maKhoaFK'
                                    from MonHoc left join Khoa on MonHoc.maKhoaFK = Khoa.maKhoa
                                    where maMon=N'" + tbTimKiemTheoTen.Text.Trim() + "' and maKhoa=N'" + cbbTimTheoKhoa.SelectedValue + "'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);
                    //nếu tìm theo mã mà ko có kết quả nào thì tìm theo tên kết hợp theo khoa
                    if (tb.Rows.Count < 1)
                    {
                        
                        string tim = @"select maMon,tenMon,soTinChi,tenKhoa as N'maKhoaFK'
                                   from MonHoc left join Khoa on MonHoc.maKhoaFK = Khoa.maKhoa
                                    where " + "tenMon like N'%" + tbTimKiemTheoTen.Text.Trim() + "%'" + " and maKhoa=N'" + cbbTimTheoKhoa.SelectedValue + "'";
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


                        tbTimKiemTheoTen.Text = "nhập tên hoặc mã môn"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                        refresh();
                        return;
                    }
                    dgvSV.DataSource = tb;
                    tbTimKiemTheoTen.Text = "nhập tên hoặc mã môn"; tbTimKiemTheoTen.ForeColor = Color.Gray;
                    refresh();
                }
            }


        }

        private void tbTimKiemTheoTen_Enter(object sender, EventArgs e)
        {
            if (tbTimKiemTheoTen.Text == "nhập tên hoặc mã môn")
            {
                tbTimKiemTheoTen.Text = "";
                tbTimKiemTheoTen.ForeColor = Color.Black;
            }
        }

        private void tbTimKiemTheoTen_Leave(object sender, EventArgs e)
        {
            if (tbTimKiemTheoTen.Text == "")
            {
                tbTimKiemTheoTen.Text = "nhập tên hoặc mã môn";
                tbTimKiemTheoTen.ForeColor = Color.Gray;
            }
        }

        private void tbNgaysinh_Click(object sender, EventArgs e)
        {

        }

      
    }
}
