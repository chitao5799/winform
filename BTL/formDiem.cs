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
using System.Windows.Forms;
using System.Collections;

namespace BTL
{
    public partial class formDiem : Form
    {
        string constr = ConnectString.GetConnectionString();
        public formDiem()
        {
            InitializeComponent();
            dgvDiem.SortCompare += new DataGridViewSortCompareEventHandler( this.dgvDiem_SortCompare);
            Controls.Add(this.dgvDiem);
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from LopTinChi", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);
                        string[] arrSource = new string[tb.Rows.Count];
                        int i = 0;
                        for (i = 0; i < tb.Rows.Count; i++)
                        {
                            arrSource[i] = tb.Rows[i][0].ToString().Trim();

                        }
                        AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
                        allowedTypes.AddRange(arrSource);

                        tbMaLop.AutoCompleteCustomSource = allowedTypes;
                        tbMaLop.AutoCompleteMode = AutoCompleteMode.Suggest;
                        tbMaLop.AutoCompleteSource = AutoCompleteSource.CustomSource;

                        tbTimKiemTheoMa.AutoCompleteCustomSource = allowedTypes;
                        tbTimKiemTheoMa.AutoCompleteMode = AutoCompleteMode.Suggest;
                        tbTimKiemTheoMa.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                using (SqlCommand cmd = new SqlCommand("select * from SinhVien", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);
                        string[] arrSource = new string[tb.Rows.Count];
                        int i = 0;
                        for (i = 0; i < tb.Rows.Count; i++)
                        {
                            arrSource[i] = tb.Rows[i][0].ToString().Trim();

                        }
                        AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
                        allowedTypes.AddRange(arrSource);
                        tbMaSV.AutoCompleteCustomSource = allowedTypes;
                        tbMaSV.AutoCompleteMode = AutoCompleteMode.Suggest;
                        tbMaSV.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
            }
        }
        void Load_DB_toGridView()
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {

                //string query = @"select maLopTC_PK,maSV_PK,tenSV,ngaySinh,diemChuyenCan,diemGiuaKy,diemThi,diemTBC  
                //                from BangDiem left join SinhVien on BangDiem.maSV_PK=SinhVien.maSV";
                //string query = @"select maLopTC_PK,tenSV,ngaySinh,diemChuyenCan,diemGiuaKy,diemThi,diemTBC  
                //                from BangDiem left join SinhVien on BangDiem.maSV_PK=SinhVien.maSV";
                string query = @"select maLopTC_PK,maSV_PK,tenSV,ngaySinh,YEAR(getdate())-YEAR(ngaySinh) as N'tuổi',diemChuyenCan,diemGiuaKy,diemThi,diemTBC  
                                from BangDiem left join SinhVien on BangDiem.maSV_PK=SinhVien.maSV";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);
                        
                        dgvDiem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        dgvDiem.DataSource = tb;
                        dgvDiem.Columns[0].HeaderText = "Mã Lớp";
                        dgvDiem.Columns[1].HeaderText = "Mã SV";
                        dgvDiem.Columns[2].HeaderText = "Tên Sinh Viên";
                        dgvDiem.Columns[3].HeaderText = "Ngày sinh";
                        dgvDiem.Columns[4].HeaderText = "Tuổi";
                        dgvDiem.Columns[5].HeaderText = "Điểm chuyên cần";
                        dgvDiem.Columns[6].HeaderText = "Điểm giữa kỳ";
                        dgvDiem.Columns[7].HeaderText = "Điểm thi";
                        dgvDiem.Columns[8].HeaderText = "Điểm TB";
                        dgvDiem.Columns[1].Visible = false;
                        dgvDiem.ReadOnly = true;
                     
                    }
                }
            }
        }
     
        void refresh()
        {
            tbMaLop.ResetText();
            tbMaSV.ResetText();
            tbDiemThi.Text = "0";
            tbDiemCC.Text = "0";
            tbDiemGK.Text = "0";
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            tbMaLop.Enabled = true;
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
                   
            if (tbMaSV.Text.Trim() == "")
            {
                MessageBox.Show("Mã sinh viên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbMaLop.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbDiemCC.Text.Trim() == "")
            {
                MessageBox.Show("Điểm chuyên cần không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            } 
            if (tbDiemGK.Text.Trim() == "")
            {
                MessageBox.Show("Điểm giữa kỳ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbDiemThi.Text.Trim() == "")
            {
                MessageBox.Show("Điểm thi không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            try
            {
                double diemcc = Convert.ToDouble(tbDiemCC.Text);
                if (diemcc < 0 || diemcc > 10)
                {
                    MessageBox.Show("Điểm chuyên cần phải lớn hơn 0 và bé hơn 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Điểm chuyên cần phải là số thực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                double diemgk = Convert.ToDouble(tbDiemGK.Text);
                if (diemgk < 0 || diemgk > 10)
                {
                    MessageBox.Show("Điểm giữa kỳ phải lớn hơn 0 và bé hơn 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Điểm giữa kỳ phải là số thực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                double diemthi = Convert.ToDouble(tbDiemThi.Text);
                if (diemthi < 0 || diemthi > 10)
                {
                    MessageBox.Show("Điểm thi phải lớn hơn 0 và bé hơn 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Điểm thi phải là số thực!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (KiemTraDuLieu() == false)
                return;

            string query = "select * from BangDiem";
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "diem");
                    DataTable dt = ds.Tables["diem"];
                  
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["maLopTC_PK"].ToString().Trim() == tbMaLop.Text.Trim() && row["maSV_PK"].ToString().Trim() == tbMaSV.Text.Trim())
                        {                                                  
                            row["diemChuyenCan"] = tbDiemCC.Text.Trim();
                            row["diemGiuaKy"] = tbDiemGK.Text.Trim();
                            row["diemThi"] = tbDiemThi.Text.Trim();
                            break;
                        }
                    }
                    da.Update(ds, "diem");
                    //Load_DB_toGridView();
                   
                }
                catch (Exception err)
                {
                    MessageBox.Show("Sửa thất bại, lỗi là:" + err.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            // thay đổi dữ liệu của row được sửa của  datagridview
            using (SqlConnection cnn = new SqlConnection(constr))
            {

                string query2 = @"select maLopTC_PK,maSV_PK,tenSV,ngaySinh,diemChuyenCan,diemGiuaKy,diemThi,diemTBC  
                                from BangDiem left join SinhVien on BangDiem.maSV_PK=SinhVien.maSV
                                where BangDiem.maLopTC_PK=N'" + tbMaLop.Text.Trim()+ "' and BangDiem.maSV_PK=N'"+ tbMaSV.Text.Trim()+"'";
                using (SqlCommand cmd = new SqlCommand(query2, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                    {
                        DataTable tb = new DataTable();
                        ada.Fill(tb);
                        if (tb.Rows.Count > 0)
                        {
                            string[] rowChanged = new string[8];
                       
                            rowChanged[0] = tb.Rows[0]["maLopTC_PK"].ToString();
                            rowChanged[1] = tb.Rows[0]["maSV_PK"].ToString();
                            rowChanged[2] = tb.Rows[0]["tenSV"].ToString();
                            rowChanged[3] = tb.Rows[0]["ngaySinh"].ToString();
                            rowChanged[4] = tb.Rows[0]["diemChuyenCan"].ToString();
                            rowChanged[5] = tb.Rows[0]["diemGiuaKy"].ToString();
                            rowChanged[6] = tb.Rows[0]["diemThi"].ToString();
                            rowChanged[7] = tb.Rows[0]["diemTBC"].ToString();

                            dgvDiem.Rows[rowSelected].SetValues(rowChanged);
                        }
                        
                    }
                }
            }
            refresh();
            //
        }
        int rowSelected=-1;
        private void dgvSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //kiểm tra xem có click vào dòng mới ở dưới cùng không.        
            if (dgvDiem.CurrentRow.Cells["maLopTC_PK"].Value.ToString() != "")
            {
                tbMaLop.Text = dgvDiem.CurrentRow.Cells["maLopTC_PK"].Value.ToString().Trim();
                tbMaLop.Enabled = false;
                tbMaSV.Text = dgvDiem.CurrentRow.Cells["maSV_PK"].Value.ToString().Trim();
                tbMaSV.Enabled = false;
                tbDiemCC.Text = dgvDiem.CurrentRow.Cells["diemChuyenCan"].Value.ToString().Trim();
                tbDiemGK.Text = dgvDiem.CurrentRow.Cells["diemGiuaKy"].Value.ToString().Trim();
                tbDiemThi.Text = dgvDiem.CurrentRow.Cells["diemThi"].Value.ToString().Trim();
                rowSelected=dgvDiem.CurrentRow.Index;
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
                    string query = @"delete from BangDiem where maLopTC_PK=N'" + tbMaLop.Text.Trim()+ "' and maSV_PK=N'"+tbMaSV.Text.Trim()+"'";
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
                string query = "select * from BangDiem";
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = new SqlCommand(query, cnn);
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(da);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "diem");
                    DataTable dt = ds.Tables["diem"];
                    DataRow row = dt.NewRow();
                    row["maLopTC_PK"] = tbMaLop.Text.Trim();
                    row["diemChuyenCan"] = tbDiemCC.Text.Trim();               
                    row["maSV_PK"] = tbMaSV.Text.Trim();
                    row["diemGiuaKy"] = tbDiemGK.Text.Trim();
                    row["diemThi"] = tbDiemThi.Text.Trim();
                    row["diemTBC"] = 0;
                    dt.Rows.Add(row);
                    da.Update(ds, "diem");
                    Load_DB_toGridView();
                    refresh();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Không thể thêm,Sinh viên "+tbMaSV.Text.Trim()+" đã có trong lớp "+tbMaLop.Text.Trim()+" hoặc mã sinh viên hay mã hớp không hợp lệ.\n Lỗi là:" + err.Message,
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
                string tim_sql = @"select maLopTC_PK,maSV_PK,tenSV,ngaySinh,diemChuyenCan,diemGiuaKy,diemThi,diemTBC
                                    from BangDiem left join SinhVien on BangDiem.maSV_PK=SinhVien.maSV
                                    where maLopTC_PK=N'" + tbTimKiemTheoMa.Text.Trim() + "'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
                    DataTable tb = new DataTable();
                    sda.Fill(tb);                  
                    dgvDiem.DataSource = tb;
                    tbTimKiemTheoMa.Text = "nhập mã lớp"; tbTimKiemTheoMa.ForeColor = Color.Gray;
                    refresh();
                }

        }

        private void tbTimKiemTheoTen_Enter(object sender, EventArgs e)
        {
            if (tbTimKiemTheoMa.Text == "nhập mã lớp")
            {
                tbTimKiemTheoMa.Text = "";
                tbTimKiemTheoMa.ForeColor = Color.Black;
            }
        }

        private void tbTimKiemTheoTen_Leave(object sender, EventArgs e)
        {
            if (tbTimKiemTheoMa.Text == "")
            {
                tbTimKiemTheoMa.Text = "nhập mã lớp";
                tbTimKiemTheoMa.ForeColor = Color.Gray;
            }
        }

        //private void tbNgaysinh_Click(object sender, EventArgs e)
        //{

        //}

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private class RowComparer : System.Collections.IComparer
        {
            private static int sortOrderModifier = 1;

            //public RowComparer()
            //{
            //   /// MessageBox.Show("vào hàm khở tạo so sánh");//ok
            //    //if (sortOrder == SortOrder.Descending)
            //    //{
            //    //    sortOrderModifier = -1;
            //    //}
            //    //else if (sortOrder == SortOrder.Ascending)
            //    //{
            //    //    sortOrderModifier = 1;
            //    //}
            //}

             int IComparer.Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;
                MessageBox.Show("có vào đây so sánh");//not
                string[] arrNameSV_1 = DataGridViewRow1.Cells[2].Value.ToString().Trim().Split(' ');
                string[] arrNameSV_2 = DataGridViewRow1.Cells[2].Value.ToString().Trim().Split(' ');
                // Try to sort based on the Last Name column.
                int CompareResult = System.String.Compare(
                       arrNameSV_1[arrNameSV_1.Length-1].Trim(),
                        arrNameSV_2[arrNameSV_2.Length-1].Trim()
                    );

                // If the Last Names are equal, sort based on the First Name.
                if (CompareResult == 0)
                {
                    CompareResult = System.String.Compare(
                        DataGridViewRow1.Cells[3].Value.ToString(),
                        DataGridViewRow2.Cells[3].Value.ToString());
                }
                return CompareResult * sortOrderModifier;
            }

            public int Compare(object x, object y)
            {
                throw new NotImplementedException();
            }
        }

        private void dgvDiem_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            MessageBox.Show("có vào so sánh");
            // Try to sort based on the cells in the current column.
            e.SortResult = System.String.Compare(
                e.CellValue1.ToString(), e.CellValue2.ToString());

            // If the cells are equal, sort based on the tensv column.
            string[] arrNameSV_1 = dgvDiem.Rows[e.RowIndex1].Cells["tenSV"].Value.ToString().Trim().Split(' ');
            string[] arrNameSV_2 = dgvDiem.Rows[e.RowIndex1].Cells["tenSV"].Value.ToString().Trim().Split(' ');
            if (e.SortResult == 0 && e.Column.Name != "tenSV")
            {
                e.SortResult = System.String.Compare(
                    arrNameSV_1[arrNameSV_1.Length - 1].Trim(),
                        arrNameSV_2[arrNameSV_2.Length - 1].Trim()
                        );
            }
            e.Handled = true;
        }

        private void tbMaLop_TextChanged(object sender, EventArgs e)
        {
            //string tim_sql = @"select MonHoc.tenMon
            //    from LopTinChi,MonHoc
            //    where LopTinChi.maMonFK=MonHoc.maMon and LopTinChi.maLopTC='" + tbMaLop.Text.Trim() + "'";
            //using (SqlConnection cnn = new SqlConnection(constr))
            //{
            //    SqlDataAdapter sda = new SqlDataAdapter();
            //    sda.SelectCommand = new SqlCommand(tim_sql, cnn);
            //    DataTable tb = new DataTable();
            //    sda.Fill(tb);
            //    if (tb.Rows.Count > 0)
            //        tbMonHoc.Text = tb.Rows[0]["tenMon"].ToString();
            //    else
            //        tbMonHoc.Text = "";
            //    tbMonHoc.ReadOnly=true;
            //}
        }

        private void tbDiemCC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbDiemCC.Text.Trim() != "") { 
                float.Parse(tbDiemCC.Text.Trim());
                double diemcc = Convert.ToDouble(tbDiemCC.Text.Trim());
                if (diemcc < 0 || diemcc > 10)
                {
                    MessageBox.Show("Điểm chuyên cần phải lớn hơn 0 và bé hơn 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Không được nhập ký tự",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbDiemGK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbDiemGK.Text.Trim() != "")
                {
                    float.Parse(tbDiemGK.Text.Trim());
                    double diemcc = Convert.ToDouble(tbDiemGK.Text.Trim());
                    if (diemcc < 0 || diemcc > 10)
                    {
                        MessageBox.Show("Điểm giữa kỳ phải lớn hơn 0 và bé hơn 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Không được nhập ký tự",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbDiemThi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tbDiemThi.Text.Trim() != "")
                {
                    float.Parse(tbDiemThi.Text.Trim());
                    double diemcc = Convert.ToDouble(tbDiemThi.Text.Trim());
                    if (diemcc < 0 || diemcc > 10)
                    {
                        MessageBox.Show("Điểm thi phải lớn hơn 0 và bé hơn 10!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Không được nhập ký tự",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void DGV_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        //{
        //    string s1 = e.CellValue1.ToString().Substring(0, 6) +
        //                e.CellValue1.ToString().Substring(6).PadLeft(5, '0');
        //    string s2 = e.CellValue2.ToString().Substring(0, 6) +
        //                e.CellValue2.ToString().Substring(6).PadLeft(5, '0');

        //    e.SortResult = s1.CompareTo(s2);
        //    e.Handled = true;
        //}
    }
}
