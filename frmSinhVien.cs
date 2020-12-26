using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_3layers
{
    public partial class frmSinhVien : Form
    {
        public frmSinhVien(string msv)
        {
            this.msv = msv; // truyền lại mã sv khi form chạy
            InitializeComponent();
        }
        private string msv;
        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(msv)) // nếu msv không có thì thêm mới sv
            {
                this.Text = "Thêm mới sinh viên ";
            }    
            else
            {
                this.Text = "Cập nhật thông tin sinh viên";
                // lấy thông tin chi tiết của 1 sinh viên dựa vào msv
                //msv là mã sinh viên đã đưuọc triuyeefn từ form dssv
                var r = new Database().Select("selectSV '"+msv+"'");
          //      MessageBox.Show(r[0].ToString());
                txtHo.Text = r["ho"].ToString();
                txtTendem.Text = r["tendem"].ToString();
                txtTen.Text = r["ten"].ToString();
                mtbNgaysinh.Text = r["ngsinh"].ToString();
                if(int.Parse(r["gioitinh"].ToString())==1)
                {
                    rbtNam.Checked = true;
                }
                else
                {
                    rbtNu.Checked = true;
                }
                txtQuequan.Text = r["quequan"].ToString();
                txtDiachi.Text = r["diachi"].ToString();
                txtDienthoai.Text = r["dienthoai"].ToString();
                txtEmail.Text = r["email"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* 
             * btnLuu= button1 sẽ xử lý 1 trong2 tình huống sau
             * trường hợp 1: nếu mã sinh viên không có giá trị==> thêm mới sinh viên.
             * trường hợp 2 nếu mã sinh viên có giá trị thì cập nhật sinh viên
             * ý tường: 
             *          cho dù thêm mới hay cập nhật thì đều cần các giá trị như họ, tên dệm, ngày sinh, giới tính
             *          quê quán, địa chỉ, điện thoại, email==> các giá trị này dùng cho cả 2 trường hợp 
             *          riêng cập nhật sinh viên cần quan tâm mã sinh viên
             *          
             * 
             */

            string sql = "";
            string ho = txtHo.Text;
            string tendem = txtTendem.Text;
            string ten = txtTen.Text;
          
            DateTime ngaysinh;
            try
            {
                ngaysinh = DateTime.ParseExact(mtbNgaysinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            }catch(Exception)
            {
                MessageBox.Show("Ngày sinh không hợp lệ ");
                mtbNgaysinh.Select();
                return;
            }
            //vì ngày sinh trong masket bot được set theo dạng dd//mm/yyyy
            // nên cần chuyển ở csdt sang dạng này
            string gioitinh =rbtNam.Checked ? "1" : "0"; //toán tử 2 ngôi
            //nếu radiobuttonNam được check thì chọn 1 và nguwoci lại chọn 0
            string quequan = txtQuequan.Text;
            string diachi = txtDiachi.Text;
            string dienthoai = txtDienthoai.Text;
            string email = txtEmail.Text;
            List<CustomParameter> lstPara = new List<CustomParameter>(); // khai báo một danh sách tham số class customparameter

            if ( string.IsNullOrEmpty(msv)) //Nếu thêm mới sih viên
            {
                sql = "ThemMoiSV"; // gọi tới procedure cập nhật sinh viên
            }    
            else //nếu cập nhật sinh viên
            {
                sql = "updateSV";
                lstPara.Add(new CustomParameter()
                {
                    key = "@masinhvien",
                    value = msv
                });
            }
            lstPara.Add(new CustomParameter()
            {
                key = "@ho",
                value = ho
            }) ;
            lstPara.Add(new CustomParameter()
            {
                key = "@tendem",
                value = tendem
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@ten",
                value = ten
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@ngaysinh",
                value = ngaysinh.ToString("yyyy-MM-dd")
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@gioitinh",
                value = gioitinh
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@quequan",
                value = quequan
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@diachi",
                value = diachi
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@dienthoai",
                value = dienthoai
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@email",
                value = email
            });
            var rs = new Database().ExeCute(sql,lstPara); //truywwfn 2 tham số là câu lệnh sql và danh sách các tham số
            if (rs == 1)// nếu thực thi thành công
            {
                if (string.IsNullOrEmpty(msv)) // nếu thêm mới
                {
                    MessageBox.Show("Thêm mới sinh viên thành công");
                }
                else //nếu cập nhật
                {
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công");
                }
                this.Dispose(); //đóng forem sau khi thực thi thành công
            }
            else // nếu không thành công
            {
                MessageBox.Show("Thực thi thất bại");
            }                
        }
    }
}
