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
    public partial class frmLopHoc : Form
    {
        public frmLopHoc(string malophoc)
        {
            this.malophoc = malophoc;
            InitializeComponent();
        }
        private string malophoc;
        private Database db;
        private string nguoithuchien = "admin";
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmLopHoc_Load(object sender, EventArgs e)
        {
            db = new Database();
            List<CustomParameter> lst = new List<CustomParameter>()
            {
                new CustomParameter()
                {
                    key = "@tukhoa",
                    value=""
                }
            };
            //load dữ liệu cho  2 combobox môn học và giáo viên
            cbbMonhoc.DataSource = db.SelectData("Selectallmonhoc",lst);
            cbbMonhoc.DisplayMember = "tenmonhoc";// thuoocj tinsh hieern thij cua combobox
            cbbMonhoc.ValueMember = "mamonhoc"; //gia tri (key) cua combobox
            cbbMonhoc.SelectedIndex = -1;

            cbbGiaoVien.DataSource = db.SelectData("SelectallGV", lst);
            cbbGiaoVien.DisplayMember = "hoten";// thuoocj tinsh hieern thij cua combobox
            cbbGiaoVien.ValueMember = "magiaovien"; //gia tri (key) cua combobox
            cbbGiaoVien.SelectedIndex = -1; //set combobox không chọn giá trị nào

            if (string.IsNullOrEmpty(malophoc))
            {
                this.Text = "Thêm mới 1 lớp học";
            }
            else
            {
                this.Text = "Cập nhật lớp học này";
                var r = db.Select("exec selectLopHoc'" + malophoc + "'");
                cbbGiaoVien.SelectedValue = r["magiaovien"].ToString();
                cbbMonhoc.SelectedValue = r["mamonhoc"].ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            // bắt buộc phải chọn không thì không cho lưu =))
            if(cbbMonhoc.SelectedIndex < 0)
            {
                MessageBox.Show("Chọn môn học");
                return;
            }
            if(cbbGiaoVien.SelectedIndex < 0)
            {
                MessageBox.Show("Chọn giáo viên");
                return;
            }//kết thuc 
            List<CustomParameter> lst = new List<CustomParameter>();
            if (string.IsNullOrEmpty(malophoc))
            {
                sql = "insertLopHoc";
                lst.Add(new CustomParameter()
                {
                   
                    key = "@nguoitao",
                    value = nguoithuchien
                }) ;
            }
            else
            {
                sql = "updateLopHoc";

                lst.Add(new CustomParameter()
                {
                    key = "@nguoicapnhat",
                    value = nguoithuchien
                });
                lst.Add(new CustomParameter()
                {
                    key = "@malophoc",
                    value = malophoc
                });
                lst.Add(new CustomParameter()
                {
                    key = "@mamonhoc",
                    value = cbbMonhoc.SelectedValue.ToString()
                    });
                lst.Add(new CustomParameter()
                {
                    key = "@magiaovien",
                    value = cbbGiaoVien.SelectedValue.ToString()
                });
                var kq = db.ExeCute(sql,lst);
                if (kq == 1)
                {
                    if (string.IsNullOrEmpty(malophoc))
                    {
                        MessageBox.Show("Thêm lớp học thành công");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật lớp họcc thành công");
                    }
                    this.Dispose();
                }
               
                else
                {
                    MessageBox.Show("Lưu thất bại");
                }
            }
        }
    }
}
