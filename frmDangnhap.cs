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
    public partial class frmDangnhap : Form
    {
        public frmDangnhap()
        {
            InitializeComponent();
        }
        public string tendangnhap = "";
       public string loaitk ;
      //  public string matkhau = "";
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            #region ktra_rbuoc
            // kiểm tra ràng buộc:
            if (cbbLoaiTaiKhoan.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản");
                return;
            }
            if(string.IsNullOrEmpty(txtTendangnhap.Text))
            {
                MessageBox.Show("Nhập tài khoản","Tài khoản không được để trống");
                txtTendangnhap.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtMatkhau.Text))
            {
                MessageBox.Show("Nhập mật khẩu", "Mật khẩu không thể để trống");

            }
#endregion

            tendangnhap = txtTendangnhap.Text;
             loaitk = "";
            #region swtk 
            switch (cbbLoaiTaiKhoan.Text)
            {
                case "Quản trị viên":
                    loaitk = "admin";
                    break;
                case "Giáo viên":
                    loaitk = "gv";
                    break;
                case "Sinh viên":
                    loaitk = "sv";
                    break;
            }

            #endregion

            List<CustomParameter> lst = new List<CustomParameter>()
            {
                new CustomParameter()
                {
                    key="@loaitaikhoan",
                    value=loaitk
                },
                   new CustomParameter()
                {
                    key="@taikhoan",
                    value=txtTendangnhap.Text
                },
                      new CustomParameter()
                {
                    key="@matkhau",
                    value=txtMatkhau.Text
                },

            };
            var rs = new Database().SelectData("dangnhap", lst);
            if (rs.Rows.Count > 0)
            {
                MessageBox.Show("Đã đăng nhập thành công");
                  this.Hide();
            }
            else
            {
                MessageBox.Show("Kiểm tra lại tên đăng nhập hoặc mật khẩu","Sai tên đăng nhập hoặc mật khẩu");
            }


         
        }

        private void frmDangnhap_Load(object sender, EventArgs e)
        {

        }
    }
}
