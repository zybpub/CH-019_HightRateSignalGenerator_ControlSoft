using System;
using System.Windows.Forms;

namespace CH_019高刷信号源控制器
{
    public partial class Form1 : Form
    {
        SuperSerialPort ssp;
        string temp;
        byte[] vga参数设置, dp参数设置, hdmi参数设置,command;
        byte[] CrossTalk图案设置;
        byte[] crosstalk1 = new byte[] { 0, 0, 0, 255, 255, 255 };
        byte[] crosstalk2 = new byte[] { 0, 0, 0, 230, 230, 230 };
        byte[] crosstalk3 = new byte[] { 0, 0, 0, 209, 209, 209 };
        byte[] crosstalk4 = new byte[] { 0, 0, 0, 186, 186, 186 };
        byte[] crosstalk5 = new byte[] { 0, 0, 0, 158, 158, 158 };
        byte[] crosstalk6 = new byte[] { 0, 0, 0, 0, 0, 0 };
        byte[] crosstalk7 = new byte[] { 0, 0, 0, 115, 82, 66 };
        byte[] crosstalk8 = new byte[] { 0, 0, 0, 194, 154, 130 };
        byte[] crosstalk9 = new byte[] { 0, 0, 0, 94, 122, 156 };
        byte[] crosstalk10 = new byte[] { 0, 0, 0, 89, 107, 66 };
        byte[] crosstalk11 = new byte[] { 0, 0, 0, 130, 128, 176 };
        byte[] crosstalk12 = new byte[] { 0, 0, 0, 99, 189, 168 };
        byte[] crosstalk13 = new byte[] { 0, 0, 0, 217, 120, 41 };
        byte[] crosstalk14 = new byte[] { 0, 0, 0, 79, 92, 163 };
        byte[] crosstalk15 = new byte[] { 0, 0, 0, 194, 84, 97 };
        byte[] crosstalk16 = new byte[] { 0, 0, 0, 92, 61, 107 };
        byte[] crosstalk17 = new byte[] { 0, 0, 0, 158, 186, 64 };
        byte[] crosstalk18 = new byte[] { 0, 0, 0, 230, 161, 46 };
        byte[] crosstalk19 = new byte[] { 0, 0, 0, 51, 61, 150 };
        byte[] crosstalk20 = new byte[] { 0, 0, 0, 71, 148, 71 };
        byte[] crosstalk21 = new byte[] { 0, 0, 0, 176, 48, 59 };
        byte[] crosstalk22 = new byte[] { 0, 0, 0, 237, 199, 33 };
        byte[] crosstalk23 = new byte[] { 0, 0, 0, 186, 84, 145 };
        byte[] crosstalk24 = new byte[] { 0, 0, 0, 0, 133, 163 };
        byte[] crosstalk25 = new byte[] { 0, 0, 0, 255, 0, 0 };
        byte[] crosstalk26 = new byte[] { 0, 0, 0, 0, 255, 0 };
        byte[] crosstalk27 = new byte[] { 0, 0, 0, 0, 0, 255 };
        byte[] crosstalk28 = new byte[] { 0, 0, 0, 0, 255, 255 };
        byte[] crosstalk29 = new byte[] { 0, 0, 0, 255, 0, 255 };
        byte[] crosstalk30 = new byte[] { 0, 0, 0, 255, 255, 0 };

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
            CheckForIllegalCrossThreadCalls = false;
            config_json.ReadAll();
            foreach (string com in System.IO.Ports.SerialPort.GetPortNames()) //自动获取串行口名称
            {
                cbb_serial_portname.Items.Add(com);
            }
            // cbb_serial_portname.SelectedIndex = 0;
            cbb_serial_portname.Text = config_json.serial_portname;
            ssp = new SuperSerialPort();
            temp = ssp.SetPortInfo(cbb_serial_portname.Text, "None", "One", "8", "115200");

            cbb_vga视频格式.SelectedIndex = 0;
            cbb_hdmi视频格式.SelectedIndex = 0;
            cbb_dp视频格式.SelectedIndex = 0;
            set_command();

            read_config_data();
            upate_crosstalk_color();
        }
        void upate_crosstalk_color() {

            panel_back.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt16(tb_crosstalk_back_r.Text), Convert.ToInt16(tb_crosstalk_back_g.Text), Convert.ToInt16(tb_crosstalk_back_b.Text));
            panel_box.BackColor = System.Drawing.Color.FromArgb( Convert.ToInt16(tb_crosstalk_box_r.Text), Convert.ToInt16(tb_crosstalk_box_g.Text), Convert.ToInt16(tb_crosstalk_box_b.Text));
        }

       void  read_config_data()
        {
            cbb_serial_portname.Text = config_json.serial_portname;

            //vga
            cbb_vga视频格式.SelectedIndex =Convert.ToInt16( config_json.vga视频格式);
            cb_vga_edid开关.Checked = config_json.vga_EDID开关;
            cb_vga_osd开关.Checked = config_json.vga_osd开关;
            rb_vga_RGB纯色图案.Checked = config_json.vga_RGB纯色图案;
            rb_vga_内置图案.Checked = config_json.vga_内置图案;
            rb_vga_BMP图案.Checked = config_json.vga_BMP图案;
            rb_vga_图案组.Checked = config_json.vga_图案组;
            rb_vga_静音.Checked = config_json.vga_静音;
            rb_vga_正弦波.Checked = config_json.vga_正弦波;
            rb_vga_WAV音乐文件.Checked = config_json.vga_WAV音乐文件;

            tb_vga_r.Text = config_json.vga_r;
            tb_vga_g.Text = config_json.vga_g;
            tb_vga_b.Text = config_json.vga_b;
            tb_vga_内置图索引.Text = config_json.vga_内置图索引;
            tb_vga_BMP文件索引.Text = config_json.vga_BMP文件索引;
            tb_vga_图案组索引.Text = config_json.vga_图案组索引;

            tb_vga_左声道频率.Text = config_json.vga_左声道频率;
            tb_vga_右声道频率.Text = config_json.vga_右声道频率;
            tb_vga_音乐文件索引.Text = config_json.vga_音乐文件索引;

            //dp
            cbb_dp视频格式.SelectedIndex = Convert.ToInt16(config_json.dp视频格式);
            cb_dp_hdcp开关.Checked = config_json.dp_hdcp开关;
            cb_dp_edid开关.Checked = config_json.dp_edid开关;
            cb_dp_osd开关.Checked = config_json.dp_osd开关;
            rb_dp_RGB纯色图案.Checked = config_json.dp_RGB纯色图案;
            rb_dp_内置图案.Checked = config_json.dp_内置图案;
            rb_dp_BMP图案.Checked = config_json.dp_BMP图案;
            rb_dp_图案组.Checked = config_json.dp_图案组;
            rb_dp_静音.Checked = config_json.dp_静音;
            rb_dp_正弦波.Checked = config_json.dp_正弦波;
            rb_dp_WAV音乐文件.Checked = config_json.dp_WAV音乐文件;

            tb_dp_r.Text = config_json.dp_r;
            tb_dp_g.Text = config_json.dp_g;
            tb_dp_b.Text = config_json.dp_b;
            tb_dp_内置图索引.Text = config_json.dp_内置图索引;
            tb_dp_BMP文件索引.Text = config_json.dp_BMP文件索引;
            tb_dp_图案组索引.Text = config_json.dp_图案组索引;

            tb_dp_左声道频率.Text = config_json.dp_左声道频率;
            tb_dp_右声道频率.Text = config_json.dp_右声道频率;
            tb_dp_音乐文件索引.Text = config_json.dp_音乐文件索引;

            //hdmi
            cbb_hdmi视频格式.SelectedIndex = Convert.ToInt16(config_json.hdmi视频格式);
            cb_hdmi_hdcp开关.Checked = config_json.hdmi_hdcp开关;
            cb_hdmi_edid开关.Checked = config_json.hdmi_edid开关;
            cb_hdmi_cec开关.Checked = config_json.hdmi_cec开关;
            cb_hdmi_arc开关.Checked = config_json.hdmi_arc开关;
            cb_hdmi_osd开关.Checked = config_json.hdmi_osd开关;
            rb_hdmi_RGB纯色图案.Checked = config_json.hdmi_RGB纯色图案;
            rb_hdmi_内置图案.Checked = config_json.hdmi_内置图案;
            rb_hdmi_BMP图案.Checked = config_json.hdmi_BMP图案;
            rb_hdmi_图案组.Checked = config_json.hdmi_图案组;
            rb_hdmi_静音.Checked = config_json.hdmi_静音;
            rb_hdmi_正弦波.Checked = config_json.hdmi_正弦波;
            rb_hdmi_WAV音乐文件.Checked = config_json.hdmi_WAV音乐文件;

            tb_hdmi_r.Text = config_json.hdmi_r;
            tb_hdmi_g.Text = config_json.hdmi_g;
            tb_hdmi_b.Text = config_json.hdmi_b;
            tb_hdmi_内置图索引.Text = config_json.hdmi_内置图索引;
            tb_hdmi_BMP文件索引.Text = config_json.hdmi_BMP文件索引;
            tb_hdmi_图案组索引.Text = config_json.hdmi_图案组索引;

            tb_hdmi_左声道频率.Text = config_json.hdmi_左声道频率;
            tb_hdmi_右声道频率.Text = config_json.hdmi_右声道频率;
            tb_hdmi_音乐文件索引.Text = config_json.hdmi_音乐文件索引;


            //crosstalk

             rb_crosstalk_vga.Checked = config_json.crosstalk_vga;
            rb_crosstalk_dp.Checked = config_json.crosstalk_dp;
            rb_crosstalk_hdmi.Checked = config_json.crosstalk_hdmi;
           tb_crosstalk_back_r.Text = config_json.crosstalk_back_r;
            tb_crosstalk_back_g.Text = config_json.crosstalk_back_g;
             tb_crosstalk_back_b.Text = config_json.crosstalk_back_b;
             tb_crosstalk_box_r.Text = config_json.crosstalk_box_r;
             tb_crosstalk_box_g.Text = config_json.crosstalk_box_g;
             tb_crosstalk_box_b.Text = config_json.crosstalk_box_b;

        }


        void set_command() {
            vga参数设置 = new byte[31];
            for (int i = 0; i < 31; i++) { vga参数设置[i] = 0; }
            vga参数设置[0] = 0xaa;
            vga参数设置[1] = 0xcb;
            vga参数设置[3] = 0x10;
            vga参数设置[5] = 0x18; //data length

            dp参数设置 = new byte[31];
            for (int i = 0; i < 31; i++) { dp参数设置[i] = 0; }
            dp参数设置[0] = 0xaa;
            dp参数设置[1] = 0xcb;
            dp参数设置[3] = 0x20;
            dp参数设置[5] = 0x18; //data length

            hdmi参数设置 = new byte[31];
            for (int i = 0; i < 31; i++) { hdmi参数设置[i] = 0; }
            hdmi参数设置[0] = 0xaa;
            hdmi参数设置[1] = 0xcb;
            hdmi参数设置[3] = 0x30;
            hdmi参数设置[5] = 0x18; //data length

            CrossTalk图案设置 = new byte[13];
            for (int i = 0; i < 13; i++) { CrossTalk图案设置[i] = 0; }
            CrossTalk图案设置[0] = 0xaa;
            CrossTalk图案设置[1] = 0xcb;
            CrossTalk图案设置[5] = 0x06;
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
         
            temp= ssp.SetPortInfo(cbb_serial_portname.Text, "None", "One", "8", "115200");
            if (temp!= "OK")
            {
                // MessageBox.Show("端口打开失败：" + temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_reply.Text += "端口打开失败：" + temp + "\r\n";
            }
            
            ssp.DataReceived = new Action<byte[]>(DataReceived);
            temp = ssp.Open();
            if (temp == "OK") {
                btn_open.Enabled = false;
                btn_close.Enabled = true;
                //MessageBox.Show("端口打开成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_reply.Text += "端口打开成功!" + "\r\n";
            }
            else
            {
                //MessageBox.Show("端口打开失败：" + temp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_reply.Text += "端口打开失败：" + temp + "\r\n";
            }
          
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string json = System.IO.File.ReadAllText(config_json.config_file_name);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

           

            jsonObj["serial_portname"] = cbb_serial_portname.Text;

            //vga
            jsonObj["vga视频格式"] = cbb_dp视频格式.SelectedIndex;
            jsonObj["vga_r"] = tb_vga_r.Text;
            jsonObj["vga_g"] = tb_vga_g.Text;
            jsonObj["vga_b"] = tb_vga_b.Text;
            jsonObj["vga_内置图索引"] = tb_vga_内置图索引.Text;
            jsonObj["vga_BMP文件索引"] = tb_vga_BMP文件索引.Text;
            jsonObj["vga_图案组索引"] = tb_vga_图案组索引.Text;
            jsonObj["vga_左声道频率"] = tb_vga_左声道频率.Text;
            jsonObj["vga_右声道频率"] = tb_vga_右声道频率.Text;
            jsonObj["vga_音乐文件索引"] = tb_vga_音乐文件索引.Text;

            jsonObj["vga_EDID开关"] = cb_vga_edid开关.Checked.ToString();
            jsonObj["vga_osd开关"] = cb_vga_osd开关.Checked.ToString();
            jsonObj["vga_RGB纯色图案"] = rb_vga_RGB纯色图案.Checked.ToString();
            jsonObj["vga_内置图案"] = rb_vga_内置图案.Checked.ToString();
            jsonObj["vga_BMP图案"] = rb_vga_BMP图案.Checked.ToString();
            jsonObj["vga_图案组"] = rb_vga_图案组.Checked.ToString();
            jsonObj["vga_静音"] = rb_vga_静音.Checked.ToString();
            jsonObj["vga_正弦波"] = rb_vga_正弦波.Checked.ToString();
            jsonObj["vga_WAV音乐文件"] = rb_vga_WAV音乐文件.Checked.ToString();


            //dp
            jsonObj["dp视频格式"] = cbb_dp视频格式.SelectedIndex;
            jsonObj["dp_r"] = tb_dp_r.Text;
            jsonObj["dp_g"] = tb_dp_g.Text;
            jsonObj["dp_b"] = tb_dp_b.Text;
            jsonObj["dp_内置图索引"] = tb_dp_内置图索引.Text;
            jsonObj["dp_BMP文件索引"] = tb_dp_BMP文件索引.Text;
            jsonObj["dp_图案组索引"] = tb_dp_图案组索引.Text;
            jsonObj["dp_左声道频率"] = tb_dp_左声道频率.Text;
            jsonObj["dp_右声道频率"] = tb_dp_右声道频率.Text;
            jsonObj["dp_音乐文件索引"] = tb_dp_音乐文件索引.Text;

            jsonObj["dp_hdcp开关"] = cb_dp_hdcp开关.Checked.ToString();
            jsonObj["dp_edid开关"] = cb_dp_edid开关.Checked.ToString();
            jsonObj["dp_osd开关"] = cb_dp_osd开关.Checked.ToString();
            jsonObj["dp_RGB纯色图案"] = rb_dp_RGB纯色图案.Checked.ToString();
            jsonObj["dp_内置图案"] = rb_dp_内置图案.Checked.ToString();
            jsonObj["dp_BMP图案"] = rb_dp_BMP图案.Checked.ToString();
            jsonObj["dp_图案组"] = rb_dp_图案组.Checked.ToString();
            jsonObj["dp_静音"] = rb_dp_静音.Checked.ToString();
            jsonObj["dp_正弦波"] = rb_dp_正弦波.Checked.ToString();
            jsonObj["dp_WAV音乐文件"] = rb_dp_WAV音乐文件.Checked.ToString();


            //hdmi
            jsonObj["hdmi视频格式"] = cbb_dp视频格式.SelectedIndex;
            jsonObj["hdmi_r"] = tb_hdmi_r.Text;
            jsonObj["hdmi_g"] = tb_hdmi_g.Text;
            jsonObj["hdmi_b"] = tb_hdmi_b.Text;
            jsonObj["hdmi_内置图索引"] = tb_hdmi_内置图索引.Text;
            jsonObj["hdmi_BMP文件索引"] = tb_hdmi_BMP文件索引.Text;
            jsonObj["hdmi_图案组索引"] = tb_hdmi_图案组索引.Text;
            jsonObj["hdmi_左声道频率"] = tb_hdmi_左声道频率.Text;
            jsonObj["hdmi_右声道频率"] = tb_hdmi_右声道频率.Text;
            jsonObj["hdmi_音乐文件索引"] = tb_hdmi_音乐文件索引.Text;

            jsonObj["hdmi_hdcp开关"] = cb_hdmi_hdcp开关.Checked.ToString();
            jsonObj["hdmi_EDID开关"] = cb_hdmi_edid开关.Checked.ToString();
            jsonObj["hdmi_osd开关"] = cb_hdmi_osd开关.Checked.ToString();
            jsonObj["hdmi_cec开关"] = cb_hdmi_cec开关.Checked.ToString();
            jsonObj["hdmi_arc开关"] = cb_hdmi_arc开关.Checked.ToString();
            jsonObj["hdmi_RGB纯色图案"] = rb_hdmi_RGB纯色图案.Checked.ToString();
            jsonObj["hdmi_内置图案"] = rb_hdmi_内置图案.Checked.ToString();
            jsonObj["hdmi_BMP图案"] = rb_hdmi_BMP图案.Checked.ToString();
            jsonObj["hdmi_图案组"] = rb_hdmi_图案组.Checked.ToString();
            jsonObj["hdmi_静音"] = rb_hdmi_静音.Checked.ToString();
            jsonObj["hdmi_正弦波"] = rb_hdmi_正弦波.Checked.ToString();
            jsonObj["hdmi_WAV音乐文件"] = rb_hdmi_WAV音乐文件.Checked.ToString();


        //crosstalk
        jsonObj["crosstalk_vga"] = rb_crosstalk_vga.Checked.ToString();
            jsonObj["crosstalk_dp"] = rb_crosstalk_dp.Checked.ToString();
            jsonObj["crosstalk_hdmi"] = rb_crosstalk_hdmi.Checked.ToString();
            jsonObj["crosstalk_back_r"] = tb_crosstalk_back_r.Text;
            jsonObj["crosstalk_back_g"] = tb_crosstalk_back_g.Text;
            jsonObj["crosstalk_back_b"] = tb_crosstalk_back_b.Text;
            jsonObj["crosstalk_box_r"] = tb_crosstalk_box_r.Text;
            jsonObj["crosstalk_box_g"] = tb_crosstalk_box_g.Text;
            jsonObj["crosstalk_box_b"] = tb_crosstalk_box_b.Text;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(config_json.config_file_name, output);
            config_json.ReadAll();
            MessageBox.Show("保存成功！");
        }
        /// <summary>
        /// 回调方法
        /// </summary>
        /// <param name="res"></param>
        private void DataReceived(byte[] res)
        {
            System.Threading.Thread.Sleep(200);
            tb_reply.Text +="收到数据："+ ssp.ByteToHex(res)+"\r\n";
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


        private void btn_固件版本信息读取_Click(object sender, EventArgs e)
        {
            command=new byte[] { 0xaa, 0xcb, 0, 2, 0 ,0};
            tb_reply.Text += "发送指令：" + ssp.ByteToHex(command) + "\r\n";
            ssp.Send(command);
        }

        private void btn_CPU温度读取_Click(object sender, EventArgs e)
        {
            command = new byte[] { 0xaa, 0xcb, 0, 3, 0,0 };
            tb_reply.Text += "发送指令：" + ssp.ByteToHex(command) + "\r\n";
            ssp.Send(command);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            vga_图案类型_changed();
        }
        void vga_图案类型_changed() {
            tb_vga_BMP文件索引.Enabled = false;tb_vga_内置图索引.Enabled = false;tb_vga_图案组索引.Enabled = false;
            tb_vga_r.Enabled = false;tb_vga_g.Enabled = false;tb_vga_b.Enabled = false;
            if (rb_vga_RGB纯色图案.Checked) { vga参数设置[14] = 1; tb_vga_r.Enabled = true;tb_vga_g.Enabled = true;tb_vga_b.Enabled = true; }
            if (rb_vga_内置图案.Checked) { vga参数设置[14] = 2; tb_vga_内置图索引.Enabled = true; }

            if (rb_vga_BMP图案.Checked) { vga参数设置[14] = 3;tb_vga_BMP文件索引.Enabled = true; }
            if (rb_vga_图案组.Checked) { vga参数设置[14] = 4; tb_vga_图案组索引.Enabled = true; }
        }
        void dp_图案类型_changed()
        {
            tb_dp_BMP文件索引.Enabled = false; tb_dp_内置图索引.Enabled = false; tb_dp_图案组索引.Enabled = false;
            tb_dp_r.Enabled = false; tb_dp_g.Enabled = false; tb_dp_b.Enabled = false;
            if (rb_dp_RGB纯色图案.Checked) { dp参数设置[14] = 1; tb_dp_r.Enabled = true; tb_dp_g.Enabled = true; tb_dp_b.Enabled = true; }
            if (rb_dp_内置图案.Checked) { dp参数设置[14] = 2; tb_dp_内置图索引.Enabled = true; }

            if (rb_dp_BMP图案.Checked) { dp参数设置[14] = 3; tb_dp_BMP文件索引.Enabled = true; }
            if (rb_dp_图案组.Checked) { dp参数设置[14] = 4; tb_dp_图案组索引.Enabled = true; }
        }
        void hdmi_图案类型_changed()
        {
            tb_hdmi_BMP文件索引.Enabled = false; tb_hdmi_内置图索引.Enabled = false; tb_hdmi_图案组索引.Enabled = false;
            tb_hdmi_r.Enabled = false; tb_hdmi_g.Enabled = false; tb_hdmi_b.Enabled = false;
            if (rb_hdmi_RGB纯色图案.Checked) { hdmi参数设置[14] = 1; tb_hdmi_r.Enabled = true; tb_hdmi_g.Enabled = true; tb_hdmi_b.Enabled = true; }
            if (rb_hdmi_内置图案.Checked) { hdmi参数设置[14] = 2; tb_hdmi_内置图索引.Enabled = true; }

            if (rb_hdmi_BMP图案.Checked) { hdmi参数设置[14] = 3; tb_hdmi_BMP文件索引.Enabled = true; }
            if (rb_hdmi_图案组.Checked) { hdmi参数设置[14] = 4; tb_hdmi_图案组索引.Enabled = true; }
        }

        private void rb_内置图案_CheckedChanged(object sender, EventArgs e)
        {
            vga_图案类型_changed();
        }

        private void rb_BMP图案_CheckedChanged(object sender, EventArgs e)
        {
            vga_图案类型_changed();
        }

        private void rb_图案组_CheckedChanged(object sender, EventArgs e)
        {
            vga_图案类型_changed();
        }



        private void rb_静音_CheckedChanged(object sender, EventArgs e)
        {
            vga_伴音类型_changed();
        }

        private void rb_正弦波_CheckedChanged(object sender, EventArgs e)
        {
            vga_伴音类型_changed();
        }

        private void rb_WAV音乐文件_CheckedChanged(object sender, EventArgs e)
        {
            vga_伴音类型_changed();
        }
        void vga_伴音类型_changed() {
            tb_vga_左声道频率.Enabled = false;tb_vga_右声道频率.Enabled = false;tb_vga_音乐文件索引.Enabled = false;
            if (rb_vga_静音.Checked) { vga参数设置[22] = 0; }
            if (rb_vga_正弦波.Checked) { vga参数设置[22] = 1; tb_vga_左声道频率.Enabled = true; tb_vga_右声道频率.Enabled = true; }
            if (rb_vga_WAV音乐文件.Checked) { vga参数设置[22] = 2; tb_vga_音乐文件索引.Enabled = true; }
        }
        void dp_伴音类型_changed()
        {
            tb_dp_左声道频率.Enabled = false; tb_dp_右声道频率.Enabled = false; tb_dp_音乐文件索引.Enabled = false;
            if (rb_dp_静音.Checked) { dp参数设置[22] = 0; }
            if (rb_dp_正弦波.Checked) { dp参数设置[22] = 1; tb_dp_左声道频率.Enabled = true; tb_dp_右声道频率.Enabled = true; }
            if (rb_dp_WAV音乐文件.Checked) { dp参数设置[22] = 2; tb_dp_音乐文件索引.Enabled = true; }
        }
        void hdmi_伴音类型_changed()
        {
            tb_hdmi_左声道频率.Enabled = false; tb_hdmi_右声道频率.Enabled = false; tb_hdmi_音乐文件索引.Enabled = false;
            if (rb_hdmi_静音.Checked) { hdmi参数设置[22] = 0; }
            if (rb_hdmi_正弦波.Checked) { hdmi参数设置[22] = 1; tb_hdmi_左声道频率.Enabled = true; tb_hdmi_右声道频率.Enabled = true; }
            if (rb_hdmi_WAV音乐文件.Checked) { hdmi参数设置[22] = 2; tb_hdmi_音乐文件索引.Enabled = true; }
        }
        private void btn_VGA参数设置_Click(object sender, EventArgs e)
        {
            vga参数设置[6] = Convert.ToByte( cbb_vga视频格式.SelectedIndex);
            vga参数设置[10] = Convert.ToByte(cb_vga_edid开关.Checked);
            vga参数设置[13] = Convert.ToByte(cb_vga_osd开关.Checked);

            //图案
            vga参数设置[15] = Convert.ToByte(tb_vga_内置图索引.Text);
            vga参数设置[16] = Convert.ToByte(tb_vga_BMP文件索引.Text);
            vga参数设置[17] = Convert.ToByte(tb_vga_r.Text);
            vga参数设置[18] = Convert.ToByte(tb_vga_g.Text);
            vga参数设置[19] = Convert.ToByte(tb_vga_b.Text);
            vga参数设置[20] = Convert.ToByte(tb_vga_图案组索引.Text);
            vga参数设置[21] = Convert.ToByte(cb_vga_滚动条开关.Checked);

            //伴音
            vga参数设置[23] = Convert.ToByte(tb_vga_左声道频率.Text);
            vga参数设置[24] = Convert.ToByte(tb_vga_右声道频率.Text);
            vga参数设置[25] = Convert.ToByte(tb_vga_音乐文件索引.Text);
            vga参数设置[30] = count_sum(vga参数设置);
            //0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30
            //AA CB 00 10 00 18 00 00 00 00 01 00 00 01 00 01 01 64 64 00 01 00 01 01 02 01 00 00 00 00 EA
            tb_reply.Text += "发送指令：" + ssp.ByteToHex(vga参数设置) + "\r\n";
            ssp.Send(vga参数设置);

        }
        byte count_sum(byte[] b)
        {
            byte sum = 0;
            for (int i = 6; i < b.Length - 1; i++) {
                sum += b[i];
            }
            return sum;
        }

        private void btn_VGA参数读取_Click(object sender, EventArgs e)
        {
            ssp.Send(new byte[] { 0xaa,0xcb,0,0x11,0});
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rb_dp_RGB纯色图案_CheckedChanged(object sender, EventArgs e)
        {
            dp_图案类型_changed();
        }

        private void rb_dp_内置图案_CheckedChanged(object sender, EventArgs e)
        {
            dp_图案类型_changed();
        }

        private void rb_dp_BMP图案_CheckedChanged(object sender, EventArgs e)
        {
            dp_图案类型_changed();
        }

        private void rb_dp_图案组_CheckedChanged(object sender, EventArgs e)
        {
            dp_图案类型_changed();
        }

        private void btn_hdmi参数设置_Click(object sender, EventArgs e)
        {
            hdmi参数设置[6] = Convert.ToByte(cbb_hdmi视频格式.SelectedIndex);
            hdmi参数设置[9] = Convert.ToByte(cb_hdmi_hdcp开关.Checked);
            hdmi参数设置[10] = Convert.ToByte(cb_hdmi_edid开关.Checked);
            hdmi参数设置[11] = Convert.ToByte(cb_hdmi_cec开关.Checked);
            hdmi参数设置[12] = Convert.ToByte(cb_hdmi_arc开关.Checked);
            hdmi参数设置[13] = Convert.ToByte(cb_hdmi_osd开关.Checked);


            //图案
            hdmi参数设置[15] = Convert.ToByte(tb_hdmi_内置图索引.Text);
            hdmi参数设置[16] = Convert.ToByte(tb_hdmi_BMP文件索引.Text);
            hdmi参数设置[17] = Convert.ToByte(tb_hdmi_r.Text);
            hdmi参数设置[18] = Convert.ToByte(tb_hdmi_g.Text);
            hdmi参数设置[19] = Convert.ToByte(tb_hdmi_b.Text);
            hdmi参数设置[20] = Convert.ToByte(tb_hdmi_图案组索引.Text);
            hdmi参数设置[21] = Convert.ToByte(cb_hdmi_滚动条开关.Checked);

            //伴音
            hdmi参数设置[23] = Convert.ToByte(tb_hdmi_左声道频率.Text);
            hdmi参数设置[24] = Convert.ToByte(tb_hdmi_右声道频率.Text);
            hdmi参数设置[25] = Convert.ToByte(tb_hdmi_音乐文件索引.Text);
            hdmi参数设置[30] = count_sum(hdmi参数设置);

            tb_reply.Text +="发送指令："+ ssp.ByteToHex(hdmi参数设置) + "\r\n";
            ssp.Send(hdmi参数设置);
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            dp_伴音类型_changed();
        }

        private void rb_dp_正弦波_CheckedChanged(object sender, EventArgs e)
        {
            dp_伴音类型_changed();
        }

        private void rb_dp_WAV音乐文件_CheckedChanged(object sender, EventArgs e)
        {
            dp_伴音类型_changed();
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            hdmi_伴音类型_changed();
        }

        private void rb_hdmi_正弦波_CheckedChanged(object sender, EventArgs e)
        {
            hdmi_伴音类型_changed();
        }

        private void rb_hdmi_WAV音乐文件_CheckedChanged(object sender, EventArgs e)
        {
            hdmi_伴音类型_changed();
        }

        private void rb_hdmi_RGB纯色图案_CheckedChanged(object sender, EventArgs e)
        {
            hdmi_图案类型_changed();
        }

        private void rb_hdmi_内置图案_CheckedChanged(object sender, EventArgs e)
        {
            hdmi_图案类型_changed();
        }

        private void rb_hdmi_BMP图案_CheckedChanged(object sender, EventArgs e)
        {
            hdmi_图案类型_changed();
        }

        private void rb_hdmi_图案组_CheckedChanged(object sender, EventArgs e)
        {
            hdmi_图案类型_changed();
        }

        private void btn_hdmi参数读取_Click(object sender, EventArgs e)
        {
            ssp.Send(new byte[] { 0xaa, 0xcb, 0, 0x31, 0 });
        }

        private void btn_DP参数读取_Click(object sender, EventArgs e)
        {
            ssp.Send(new byte[] { 0xaa, 0xcb, 0, 0x21, 0 });
        }

        private void btn_DP状态读取_Click(object sender, EventArgs e)
        {
            ssp.Send(new byte[] { 0xaa, 0xcb, 0, 0x23, 0 });
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btn_crosstalk01_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk1;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk02_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk2;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk03_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk3;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk04_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk4;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk05_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk5;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk06_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk6;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk07_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk7;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk08_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk8;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk09_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk9;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk10_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk10;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk11_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk11;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk12_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk12;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk13_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk13;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk14_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk14;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk15_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk15;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk16_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk16;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_HDMI状态读取_Click(object sender, EventArgs e)
        {
            ssp.Send(new byte[] { 0xaa, 0xcb, 0, 0x33, 0 });
        }

        private void btn_crosstalk17_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk17;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk18_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk18;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk19_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk19;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk20_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk20;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk21_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk21;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk22_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk22;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk23_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk23;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk24_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk24;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk25_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk25;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk26_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk26;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk27_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk27;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk28_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk28;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk29_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk29;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void btn_crosstalk30_Click(object sender, EventArgs e)
        {
            byte[] tempbyte = crosstalk30;
            tb_crosstalk_back_r.Text = tempbyte[0].ToString();
            tb_crosstalk_back_g.Text = tempbyte[1].ToString();
            tb_crosstalk_back_b.Text = tempbyte[2].ToString();
            tb_crosstalk_box_r.Text = tempbyte[3].ToString();
            tb_crosstalk_box_g.Text = tempbyte[4].ToString();
            tb_crosstalk_box_b.Text = tempbyte[5].ToString();

            send_cross_command();
        }

        private void tb_crosstalk_back_r_TextChanged(object sender, EventArgs e)
        {
            upate_crosstalk_color();
        }

        private void tb_crosstalk_back_g_TextChanged(object sender, EventArgs e)
        {
            upate_crosstalk_color();
        }

        private void tb_crosstalk_back_b_TextChanged(object sender, EventArgs e)
        {
            upate_crosstalk_color();
        }

        private void tb_crosstalk_box_r_TextChanged(object sender, EventArgs e)
        {
            upate_crosstalk_color();
        }

        private void tb_crosstalk_box_g_TextChanged(object sender, EventArgs e)
        {
            upate_crosstalk_color();
        }

        private void tb_crosstalk_box_b_TextChanged(object sender, EventArgs e)
        {
            upate_crosstalk_color();
        }

        private void btn_log_clear_Click(object sender, EventArgs e)
        {
            tb_reply.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            send_cross_command();
        }

        void send_cross_command()
        {
            if (rb_crosstalk_vga.Checked) CrossTalk图案设置[3] = 0x15;
            if (rb_crosstalk_dp.Checked) CrossTalk图案设置[3] = 0x25;
            if (rb_crosstalk_hdmi.Checked) CrossTalk图案设置[3] = 0x35;
            CrossTalk图案设置[6] = Convert.ToByte(tb_crosstalk_back_r.Text);
            CrossTalk图案设置[7] = Convert.ToByte(tb_crosstalk_back_g.Text);
            CrossTalk图案设置[8] = Convert.ToByte(tb_crosstalk_back_b.Text);
            CrossTalk图案设置[9] = Convert.ToByte(tb_crosstalk_box_r.Text);
            CrossTalk图案设置[10] = Convert.ToByte(tb_crosstalk_box_g.Text);
            CrossTalk图案设置[11] = Convert.ToByte(tb_crosstalk_box_b.Text);
            CrossTalk图案设置[12] = count_sum(CrossTalk图案设置);
            tb_reply.Text += "发送指令：" + ssp.ByteToHex(CrossTalk图案设置) + "\r\n";
            ssp.Send(CrossTalk图案设置);
        }

        private void btn_DP参数设置_Click(object sender, EventArgs e)
        {
            dp参数设置[6] = Convert.ToByte(cbb_dp视频格式.SelectedIndex);
            dp参数设置[9] = Convert.ToByte(cb_dp_hdcp开关.Checked);
            dp参数设置[10] = Convert.ToByte(cb_dp_edid开关.Checked);
            dp参数设置[13] = Convert.ToByte(cb_dp_osd开关.Checked);

            //图案
            dp参数设置[15] = Convert.ToByte(tb_dp_内置图索引.Text);
            dp参数设置[16] = Convert.ToByte(tb_dp_BMP文件索引.Text);
            dp参数设置[17] = Convert.ToByte(tb_dp_r.Text);
            dp参数设置[18] = Convert.ToByte(tb_dp_g.Text);
            dp参数设置[19] = Convert.ToByte(tb_dp_b.Text);
            dp参数设置[20] = Convert.ToByte(tb_dp_图案组索引.Text);
            dp参数设置[21] = Convert.ToByte(cb_dp_滚动条开关.Checked);

            //伴音
            dp参数设置[23] = Convert.ToByte(tb_dp_左声道频率.Text);
            dp参数设置[24] = Convert.ToByte(tb_dp_右声道频率.Text);
            dp参数设置[25] = Convert.ToByte(tb_dp_音乐文件索引.Text);
            dp参数设置[30] = count_sum(dp参数设置);

            tb_reply.Text += "发送指令：" + ssp.ByteToHex(dp参数设置) + "\r\n";
            ssp.Send(dp参数设置);
        }
    }
}
