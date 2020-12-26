using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_3layers
{
    public partial class frmDSSV : Form
    {
        public frmDSSV()
        {
            InitializeComponent();
        }

        private void frmDSSV_Load(object sender, EventArgs e)
        {
            LoadDSSV();
        }
        private void LoadDSSV()
        {
          //  List<CustomParameter> lstPara = new List<CustomParameter>();
            //load all danh sach sinh vien khi form dc load 
            dgvSinhVien.DataSource = new Database().SelectData("exec SelectAllSinhVien");
            // dat ten cot 
            dgvSinhVien.Columns["masinhvien"].HeaderText = "Mã SV";
            dgvSinhVien.Columns["hoten"].HeaderText = "Họ tên";
            dgvSinhVien.Columns["nsinh"].HeaderText = "Ngày sinh";
            dgvSinhVien.Columns["gt"].HeaderText = "Giới tính";
            dgvSinhVien.Columns["quequan"].HeaderText = "Quê quán";
            dgvSinhVien.Columns["diachi"].HeaderText = "Địa chỉ";
            dgvSinhVien.Columns["email"].HeaderText = "Email";
            dgvSinhVien.Columns["dienthoai"].HeaderText = "Điện thoại";
        }
        private void dgvSinhVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //ys tưởng : khi double click vào sinh viên trên datagreidview sẽ hiện ra form cập nhật thông tin sinh viên. để cập nhật được sinh viên ta cần laasy được mã sinh viên 
            if (e.RowIndex >=0)
            {
                var msv= dgvSinhVien.Rows[e.RowIndex].Cells["masinhvien"].Value.ToString();
                // cần truyền mã sinh viên này vào fom sinh viên 
                new frmSinhVien(msv).ShowDialog();

                //sau khi form ftm sinh viên dược đóng lại, cần load lại danh sách sinh viên
                LoadDSSV();


            }
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            new frmSinhVien(null).ShowDialog();// nếu thêm mới sinh viên => msv null

        }
    }
}
