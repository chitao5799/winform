using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL
{
    public partial class baocao : Form
    {
        string constr = ConnectString.GetConnectionString();
        public baocao()
        {
            InitializeComponent();
            Load_combo_Khoa();
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                // string query = "select * from LopTinChi";
                string query = @"select ltc.*
                   from LopTinChi ltc,BangDiem
                    where BangDiem.maLopTC_PK = ltc.maLopTC
                    group by BangDiem.maLopTC_PK,ltc.maLopTC,ltc.maKhoaFK,ltc.hocKy,ltc.maGiangVienFK,ltc.maMonFK,ltc.namHoc
                    having count(BangDiem.maSV_PK) > 0";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
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

                        tbMalop_SVlopTC.AutoCompleteCustomSource = allowedTypes;
                        tbMalop_SVlopTC.AutoCompleteMode = AutoCompleteMode.Suggest;
                        tbMalop_SVlopTC.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                using (SqlCommand cmd = new SqlCommand("select * from LopHanhChinh", cnn))
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

                        tbMalop_SVlopHC.AutoCompleteCustomSource = allowedTypes;
                        tbMalop_SVlopHC.AutoCompleteMode = AutoCompleteMode.Suggest;
                        tbMalop_SVlopHC.AutoCompleteSource = AutoCompleteSource.CustomSource;

                        tbMaLop_DSDiemLopHc.AutoCompleteCustomSource = allowedTypes;
                        tbMaLop_DSDiemLopHc.AutoCompleteMode = AutoCompleteMode.Suggest;
                        tbMaLop_DSDiemLopHc.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
            }

            DateTime currentTime =DateTime.Now;
            int currentYear = currentTime.Year;
            string[] arrSourceNamHoc = new string[12];
            int year = currentYear - 10;
            for (int i =0 ; i < 12; i++)
            {
                arrSourceNamHoc[i] = (year.ToString() + '-' + (year + 1).ToString()).ToString();
                year++;
            }
            AutoCompleteStringCollection varAutoComplete = new AutoCompleteStringCollection();
            varAutoComplete.AddRange(arrSourceNamHoc);

            tbNamhocDsLopTC.AutoCompleteCustomSource = varAutoComplete;
            tbNamhocDsLopTC.AutoCompleteMode = AutoCompleteMode.Suggest;
            tbNamhocDsLopTC.AutoCompleteSource = AutoCompleteSource.CustomSource;

            tbNamHoc_DSDiemLopHC.AutoCompleteCustomSource = varAutoComplete;
            tbNamHoc_DSDiemLopHC.AutoCompleteMode = AutoCompleteMode.Suggest;
            tbNamHoc_DSDiemLopHC.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
                        cbbKho_DSlopTC.DataSource = tb;
                        cbbKho_DSlopTC.DisplayMember = "tenKhoa";
                        cbbKho_DSlopTC.ValueMember = "maKhoa";
                    }
                }
            }

        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnXuat_SVlopHC_Click(object sender, EventArgs e)
        {
            if (tbMalop_SVlopHC.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp không được để trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable tb = new DataTable();

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("dsSVlopHC", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@malop", tbMalop_SVlopHC.Text.Trim());
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                       
                        ad.Fill(tb);
                        
                    }
                }
            }

            DS_SV_LopHC ds = new DS_SV_LopHC();
            ((TextObject)ds.Section2.ReportObjects["tbSiSo"]).Text =tb.Rows.Count.ToString();
            ds.SetDataSource(tb);
            InBaoCao bc = new InBaoCao();
            bc.CrystalReportViewer1.ReportSource = ds;//để có thể truy cập biến CrystalReportViewer1 trong InBaoCao.cs thì phải vào InBaoCao.Designer.cs 
                                                             //và thêm phương thức get/set của CrystalReportViewer1
            bc.ShowDialog();
        }

        private void btnXuat_SVlopTC_Click(object sender, EventArgs e)
        {
            if (tbMalop_SVlopTC.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp không được để trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable tb = new DataTable();
            string tenGV;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("dsSVlopTC", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@maloptc", tbMalop_SVlopTC.Text.Trim());
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(tb);
                    }
                }
                string query2 = @"select tenGV
                                from LopTinChi ltc inner join GiangVien gv on ltc.maGiangVienFK =gv.maGV
                                where maLopTC=N'" + tbMalop_SVlopTC.Text.Trim() + "'";
                using (SqlCommand cmd = new SqlCommand(query2, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        DataTable tbTemp = new DataTable();
                        ad.Fill(tbTemp);
                        tenGV = tbTemp.Rows[0]["tenGV"].ToString().Trim();
                    }
                }
            }

            DS_DIEM_LopTC ds = new DS_DIEM_LopTC();
            ((TextObject)ds.Section2.ReportObjects["tbSiso"]).Text = tb.Rows.Count.ToString();
            ((TextObject)ds.Section2.ReportObjects["tbGV"]).Text = tenGV;
            ds.SetDataSource(tb);
            InBaoCao bc = new InBaoCao();
            bc.CrystalReportViewer1.ReportSource = ds;
            bc.ShowDialog();
        }

        private string checkNamHoc(string namHocInput)
        {
            string[] arrTextNamHoc = namHocInput.Trim().Split('-');
            string namHoc="";
            if (arrTextNamHoc.Length == 2)
            {
                int truoc, sau;
                try
                {
                    truoc = Convert.ToInt32(arrTextNamHoc[0].Trim());
                    sau = Convert.ToInt32(arrTextNamHoc[1].Trim());
                    if (sau - truoc == 1)
                    {
                        namHoc= arrTextNamHoc[0].Trim() + '-' + arrTextNamHoc[1].Trim();
                       
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbNamhocDsLopTC.Text.Trim() == "")
            {
                MessageBox.Show("Năm học không được để trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string namHoc = checkNamHoc(tbNamhocDsLopTC.Text);
            if (namHoc == "")
                return;
           
            DataTable tb = new DataTable();

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("dsLopTCHocKy", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@hocky", numericUpDownDSLopTChocky.Value);
                    cmd.Parameters.AddWithValue("@namhoc", namHoc);
                    cmd.Parameters.AddWithValue("@makhoa", cbbKho_DSlopTC.SelectedValue);
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {

                        ad.Fill(tb);

                    }
                }
            }

            DS_LopTC_HocKy ds = new DS_LopTC_HocKy();
            ds.SetDataSource(tb);
            InBaoCao bc = new InBaoCao();
            bc.CrystalReportViewer1.ReportSource = ds;
            bc.ShowDialog();
        }

        private void btnXuatDSDiemLopHC_Click(object sender, EventArgs e)
        {
            if (tbNamHoc_DSDiemLopHC.Text.Trim() == "")
            {
                MessageBox.Show("Năm học không được để trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tbMaLop_DSDiemLopHc.Text.Trim() == "")
            {
                MessageBox.Show("Mã lớp không được để trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string namHoc = checkNamHoc(tbNamHoc_DSDiemLopHC.Text);
            if (namHoc == "")
                return;
            

            DataTable tableMain = new DataTable();
            tableMain.Columns.Add("maSV",typeof(string));
            tableMain.Columns.Add("tenSV", typeof(string));
            tableMain.Columns.Add("ngaySinh", typeof(DateTime));
            tableMain.Columns.Add("gioiTinh", typeof(string));
            tableMain.Columns.Add("diemTB", typeof(double));
            tableMain.Columns.Add("tongSoTC", typeof(int));
            tableMain.Columns.Add("soTCNo", typeof(int));
            tableMain.Columns.Add("xepLoai", typeof(string));

            DS_DIEM_LopHC_HocKy reportDSDiem = new DS_DIEM_LopHC_HocKy();
            ((TextObject)reportDSDiem.Section2.ReportObjects["tbLop"]).Text = tbMaLop_DSDiemLopHc.Text.Trim();
            ((TextObject)reportDSDiem.Section2.ReportObjects["tbHocKy"]).Text = numericUpDownHocKy_DSDiemLopHC.Value.ToString();
            ((TextObject)reportDSDiem.Section2.ReportObjects["tbNamHoc"]).Text = namHoc;
            using (SqlConnection cnn = new SqlConnection(constr))
            {

                string query0 = @"select gv.tenGV
                                from LopHanhChinh lhc,GiangVien gv
                                where lhc.maGVChuNhiemFK=gv.maGV and lhc.maLopHC=N'"+ tbMaLop_DSDiemLopHc.Text.Trim() + "'";
                using (SqlCommand cmd = new SqlCommand(query0, cnn))
                {
                    DataTable tbgv = new DataTable();
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(tbgv);
                        if(tbgv.Rows.Count>0)
                        ((TextObject)reportDSDiem.Section2.ReportObjects["tbTenGV"]).Text =tbgv.Rows[0]["tenGV"].ToString();
                    }
                }

                DataTable tb = new DataTable();
                string querySVLopHC = @"select * from SinhVien where maLopHC_FK=N'"+ tbMaLop_DSDiemLopHc.Text.Trim()+"'";
                using (SqlCommand cmd = new SqlCommand(querySVLopHC, cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        ad.Fill(tb);
                    }
                }
                 ((TextObject)reportDSDiem.Section2.ReportObjects["tbSiso"]).Text =tb.Rows.Count.ToString();
                int tongTinChi = 0, soTinChiNo = 0;
                double diemTBCongAllMonHoc = 0.0, tongDiemTBC = 0.0;
                foreach (DataRow row in tb.Rows)
                {
                    DataRow rowNew = tableMain.NewRow();
                    rowNew["maSV"] = row["maSV"];
                    rowNew["tenSV"] = row["tenSV"];
                    rowNew["ngaySinh"] = row["ngaySinh"];
                    rowNew["gioiTinh"] = row["gioiTinh"];

                    tongTinChi = 0; soTinChiNo = 0;
                    //lấy ds điểm các môn trong học kỳ của 1 sv theo mã sv
                    string quyery2 = @"select diemTBC,soTinChi
                                        from SinhVien sv,BangDiem bd,LopTinChi ltc,MonHoc mh
                                        where sv.maSV=bd.maSV_PK and bd.maLopTC_PK=ltc.maLopTC and ltc.maMonFK=mh.maMon
                                        and sv.maLopHC_FK=N'"+ tbMaLop_DSDiemLopHc.Text.Trim()+ "' and bd.maSV_PK=N'"+ row["maSV"]+
                                        "' and hocKy="+ numericUpDownHocKy_DSDiemLopHC.Value+ " and namHoc=N'"+ namHoc + "'";
                    DataTable tbDiemOfSV = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(quyery2, cnn))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            ad.Fill(tbDiemOfSV);
                        }
                    }
                    diemTBCongAllMonHoc = 0.0; tongDiemTBC = 0.0;
                    int j;
                  
                    for ( j= 0; j < tbDiemOfSV.Rows.Count; j++)
                    {
                        tongTinChi +=Convert.ToInt32(tbDiemOfSV.Rows[j]["soTinChi"].ToString());
                        tongDiemTBC +=(Convert.ToDouble(tbDiemOfSV.Rows[j]["diemTBC"].ToString())* Convert.ToDouble(tbDiemOfSV.Rows[j]["soTinChi"]));
                        if(Convert.ToDouble(tbDiemOfSV.Rows[j]["diemTBC"].ToString())<4.0)
                            soTinChiNo +=Convert.ToInt32(tbDiemOfSV.Rows[j]["soTinChi"]);
                    }

                    if(tongTinChi>0)
                        diemTBCongAllMonHoc = tongDiemTBC / tongTinChi;
                    rowNew["diemTB"] = diemTBCongAllMonHoc;
                    rowNew["tongSoTC"] = tongTinChi;
                    rowNew["soTCNo"] = soTinChiNo;
                    if (diemTBCongAllMonHoc < 1.5)
                        rowNew["xepLoai"] = "Kém";
                    else if (diemTBCongAllMonHoc<4.0)
                        rowNew["xepLoai"] = "Yếu";
                    else if (diemTBCongAllMonHoc < 6.5)
                        rowNew["xepLoai"] = "Trung Bình";
                    else if (diemTBCongAllMonHoc < 8.0)
                        rowNew["xepLoai"] = "Khá";
                    else if (diemTBCongAllMonHoc < 9.5)
                        rowNew["xepLoai"] = "Giỏi";
                    else
                        rowNew["xepLoai"] = "Xuất Sắc";

                    tableMain.Rows.Add(rowNew);
                }
            }

            DataSetQuanLyDiemSV dsSV = new DataSetQuanLyDiemSV();
            dsSV.Tables["DSDiemSVLopHC"].Merge(tableMain);
          
            reportDSDiem.SetDataSource(dsSV.Tables["DSDiemSVLopHC"]);
           // reportDSDiem.RecordSelectionFormula = "{DSDiemSVLopHC.gioiTinh}='Nữ'"; //ví dụ lọc kết quả
            InBaoCao bc = new InBaoCao();
            bc.CrystalReportViewer1.ReportSource = reportDSDiem;
            bc.ShowDialog();
        }
    }
}
