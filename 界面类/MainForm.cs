using HTTP.功能类;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HTTP
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            列表_封包数据.Click += 封包数据_左键;
            this.Height = 325;
            this.Width = 666;
            初始化右键菜单();
            封包数据_添加("名称", "方式", "长度", "数据");
            封包数据_添加("名称", "方式", "长度", "数据");


            this.Text = "封包助手 -> " + 封包类.网络层.Version();
            封包回调类 testCallBack = new 封包回调类();
            封包类.网络层 syNet = new 封包类.网络层();

            syNet.BindCallback(testCallBack);
            syNet.BindPort(9527);
            bool b = syNet.Start();
            bool a = syNet.LoadDriver(true);
            if (a && b)
            {
                Debug.Write("中间件启动：");
                Debug.WriteLine(b);
                syNet.AddProcessName("DNF.exe");
                Debug.WriteLine("开始监控DNF.exe");
            }
            else
            {
                Debug.WriteLine("加载驱动失败，无法拦截进程【注意，需要管理员权限（请检查），win7请安装 KB3033929 补丁】");
            }
        }

        private void 初始化右键菜单()
        {
            ContextMenuStrip 右键菜单 = new ContextMenuStrip();

            ToolStripMenuItem 删除项 = new ToolStripMenuItem("删除选中项");
            删除项.Click += 封包数据_删除;
            右键菜单.Items.Add(删除项);
            右键菜单.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem 快捷项 = new ToolStripMenuItem("快捷方式");
            ToolStripMenuItem 子项_进图发送 = new ToolStripMenuItem("进图发送");
            子项_进图发送.Click += (sender, e) =>
            {
                if (列表_封包数据.SelectedItems.Count > 0)
                {
                    列表_封包数据.SelectedItems[0].SubItems[1].Text = "进图发送";
                }
            };
            ToolStripMenuItem 子项_出图发送 = new ToolStripMenuItem("出图发送");
            子项_出图发送.Click += (sender, e) =>
            {
                if (列表_封包数据.SelectedItems.Count > 0)
                {
                    列表_封包数据.SelectedItems[0].SubItems[1].Text = "出图发送";
                }
            };
            ToolStripMenuItem 子项_过图发送 = new ToolStripMenuItem("过图发送");
            子项_过图发送.Click += (sender, e) =>
            {
                if (列表_封包数据.SelectedItems.Count > 0)
                {
                    列表_封包数据.SelectedItems[0].SubItems[1].Text = "过图发送";
                }
            };
            ToolStripMenuItem 子项_循环发送 = new ToolStripMenuItem("循环发送");
            子项_循环发送.Click += (sender, e) =>
            {
                if (列表_封包数据.SelectedItems.Count > 0)
                {
                    列表_封包数据.SelectedItems[0].SubItems[1].Text = "循环发送";
                }
            };
            ToolStripMenuItem 子项_发服务器 = new ToolStripMenuItem("发服务器");
            子项_发服务器.Click += (sender, e) =>
            {
                if (列表_封包数据.SelectedItems.Count > 0)
                {
                    列表_封包数据.SelectedItems[0].SubItems[1].Text = "发服务器";
                }
            };

            快捷项.DropDownItems.Add(子项_进图发送);
            快捷项.DropDownItems.Add(子项_出图发送);
            快捷项.DropDownItems.Add(子项_过图发送);
            快捷项.DropDownItems.Add(子项_循环发送);
            快捷项.DropDownItems.Add(子项_发服务器);
            右键菜单.Items.Add(快捷项);
            右键菜单.Items.Add(new ToolStripSeparator());

            列表_封包数据.ContextMenuStrip = 右键菜单;
        }

        private void 封包数据_左键(object sender, EventArgs e)
        {
            if (列表_封包数据.SelectedItems.Count > 0)
            {
                ListViewItem 选中项 = 列表_封包数据.SelectedItems[0];
                文本框_名称.Text = $"{选中项.SubItems[0].Text}";
                文本框_数据.Text = $"{选中项.SubItems[3].Text}";
            }
        }

        private void 封包数据_删除(object sender, EventArgs e)
        {
            if (列表_封包数据.SelectedItems.Count > 0)
            {
                DialogResult 结果 = MessageBox.Show(
                    "确定要删除选中的项目吗？",
                    "确认删除",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (结果 == DialogResult.Yes)
                {
                    for (int i = 列表_封包数据.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        列表_封包数据.Items.Remove(列表_封包数据.SelectedItems[i]);
                    }

                    文本框_数据.Text = string.Empty;
                    文本框_名称.Text = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("请先选择要删除的项目。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 选择_云包系统_CheckedChanged(object sender, EventArgs e)
        {
            if (选择_云包系统.Checked)
            {
                选择_抓包系统.Checked = false;
                this.Height = 610;
                this.Width = 666;
            }
            else
            {
                this.Height = 325;
                this.Width = 666;
            }
        }

        private void 选择_抓包系统_CheckedChanged(object sender, EventArgs e)
        {

            if (选择_抓包系统.Checked)
            {
                选择_云包系统.Checked = false;
                this.Height = 325;
                this.Width = 905;
            }
            else
            {
                this.Height = 325;
                this.Width = 666;
            }
        }

        public void 封包数据_添加(string 名称, string 快捷方式, string 长度, string 数据)
        {
            列表_封包数据.Items.Add(new ListViewItem(new[] { 名称, 快捷方式, 长度, 数据 }));
        }

        private void 按钮_修改_Click(object sender, EventArgs e)
        {
            if (列表_封包数据.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选中要修改的行");
                return;
            }

            if (string.IsNullOrWhiteSpace(文本框_名称.Text))
            {
                MessageBox.Show("名称不能为空");
                return;
            }

            ListViewItem 选中项 = 列表_封包数据.SelectedItems[0];
            选中项.SubItems[0].Text = 文本框_名称.Text.Trim();
            选中项.SubItems[3].Text = 文本框_数据.Text.Trim();
        }
    }

}
