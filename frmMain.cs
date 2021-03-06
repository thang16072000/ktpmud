﻿using System;
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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private string taikhoan;
        private string loaitk;
        private void frmMain_Load(object sender, EventArgs e)
        {
            var fn = new frmDangnhap();
            fn.ShowDialog(); //cho load from đăng nhập khi form main được gọi
            // sau khi form đăng nhập được tắt, lấy tài khoản đã đăng nhập
           taikhoan = fn.tendangnhap;
            loaitk = fn.loaitk;
            if (loaitk.Equals("admin"))
            {
                //Neu la admin
                //ẩn 2 menu chấm điểm và đăng ký môn, chỉ ddeere lại menu quản lý
                chamDiemToolStripMenuItem.Visible = false;
                dangKyToolStripMenuItem.Visible = false;
            }
            else
            {
                //nếu không phải admin thì ẩn menu quản lý
                quanLyToolStripMenuItem.Visible = false;

                if (loaitk.Equals ( "gv"))
                {
                    //neesu la giao vien
                    dangKyToolStripMenuItem.Visible = false;
                }
                else
                {
                    chamDiemToolStripMenuItem.Visible = false;
                }

            }

            frmWelcome f = new frmWelcome();
            AddForm(f);
        }
        private void AddForm(Form f)
        {
            this.pnlContent.Controls.Clear();
            f.TopLevel = false;
            f.AutoScroll = true;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            this.Text = f.Text;
            this.pnlContent.Controls.Add(f);
            f.Show();
        }

        private void thoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sinhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSSV f = new frmDSSV();
            AddForm(f);
        }

        private void monHocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSMH f = new frmDSMH();
            AddForm(f);
        }

        private void giaoVienToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDSGV f = new frmDSGV();
            AddForm(f);
        }

       

        private void lopHocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDSLopHoc f = new frmDSLopHoc();
            AddForm(f);
        }

        private void dangKyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmDangkyMonhoc(taikhoan).ShowDialog();
        }
    }
}
