using System;
using System.Windows.Forms;

namespace CH_019高刷信号源控制器
{
    public partial class Form1 : Form
    {
        SuperSerialPort ssp;
        string temp;
        public Form1()
        {
            InitializeComponent();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            config_json.ReadAll();
            foreach (string com in System.IO.Ports.SerialPort.GetPortNames()) //自动获取串行口名称
            {
                cbb_serial_portname.Items.Add(com);
            }
            // cbb_serial_portname.SelectedIndex = 0;
            cbb_serial_portname.Text = config_json.serial_portname;
            ssp = new SuperSerialPort();
            temp = ssp.SetPortInfo(cbb_serial_portname.Text, "None", "One", "8", "115200");
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
         
            temp= ssp.SetPortInfo(cbb_serial_portname.Text, "None", "One", "8", "115200");
            if (temp!= "OK")
            {
                MessageBox.Show("端口打开失败：" + temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            ssp.DataReceived = new Action<byte[]>(DataReceived);
            temp = ssp.Open();
            if (temp == "OK") {
                btn_open.Enabled = false;
                btn_close.Enabled = true;
                MessageBox.Show("端口打开成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("端口打开失败：" + temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            config_json.save("serial_portname", cbb_serial_portname.Text);
            MessageBox.Show("保存成功！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="res"></param>
        private void DataReceived(byte[] res)
        {
            //处理串口返回的数据
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            try { ssp.Close();
                btn_open.Enabled = true;
                btn_close.Enabled = false;
            } catch(Exception ex) {
                MessageBox.Show("端口关闭失败:" + ex.Message, "提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btn_sendstring_Click(object sender, EventArgs e)
        {
            ssp.Send(tb_command_string.Text + "\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ssp.Send(ssp.HexToByte(tb_hexstring.Text));
        }
    }
}
