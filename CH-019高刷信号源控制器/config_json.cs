    class config_json
    {
        public static string config_file_name = "config.json";
        public static string serial_portname = "COM1";
        public static int baudrate = 115200;

    public static string vga视频格式, vga_r, vga_g, vga_b, vga_内置图索引, vga_BMP文件索引, vga_图案组索引, vga_左声道频率, vga_右声道频率, vga_音乐文件索引;
    public static bool vga_EDID开关, vga_osd开关, vga_RGB纯色图案, vga_内置图案, vga_BMP图案, vga_图案组, vga_静音, vga_正弦波, vga_WAV音乐文件;

    public static string dp视频格式, dp_r, dp_g, dp_b, dp_内置图索引, dp_BMP文件索引, dp_图案组索引, dp_左声道频率, dp_右声道频率, dp_音乐文件索引;
    public static bool dp_hdcp开关, dp_edid开关, dp_osd开关, dp_RGB纯色图案, dp_内置图案, dp_BMP图案, dp_图案组, dp_静音, dp_正弦波, dp_WAV音乐文件;

    public static string hdmi视频格式, hdmi_r, hdmi_g, hdmi_b, hdmi_内置图索引, hdmi_BMP文件索引, hdmi_图案组索引, hdmi_左声道频率, hdmi_右声道频率, hdmi_音乐文件索引;
    public static bool hdmi_hdcp开关, hdmi_edid开关, hdmi_osd开关, hdmi_cec开关, hdmi_arc开关, hdmi_RGB纯色图案, hdmi_内置图案, hdmi_BMP图案, hdmi_图案组, hdmi_静音, hdmi_正弦波, hdmi_WAV音乐文件;

    public static bool crosstalk_vga, crosstalk_dp, crosstalk_hdmi;
    public static string crosstalk_back_r, crosstalk_back_g, crosstalk_back_b, crosstalk_box_r, crosstalk_box_g, crosstalk_box_b;


    public static bool create_dafault_configfile()
        {
            if (!System.IO.File.Exists( config_file_name))
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(config_file_name, true))
                {
                    writer.WriteLine("{}");
                    writer.Close();

                }
            }

            string json = System.IO.File.ReadAllText(config_file_name);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            jsonObj["serial_portname"] = config_json.serial_portname;
            jsonObj["baudrate"] = config_json.baudrate;

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(config_file_name, output);
            return true;
        }
        public static bool save(string key, string value)
        {
            try
            {
                string json = System.IO.File.ReadAllText( config_file_name);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                jsonObj[key] = value;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(config_file_name, output);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ReadAll()
        {

            if (System.IO.File.Exists(config_file_name) == false)
            {
                create_dafault_configfile();
                return false;
            }

            System.IO.StreamReader file = System.IO.File.OpenText( config_file_name);
            Newtonsoft.Json.JsonTextReader reader = new Newtonsoft.Json.JsonTextReader(file);
            Newtonsoft.Json.Linq.JObject jsonObject =
                            (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.Linq.JToken.ReadFrom(reader);

            if (jsonObject["serial_portname"] != null) serial_portname = (string)jsonObject["serial_portname"];
            if (jsonObject["baudrate"] != null) baudrate= (int)jsonObject["baudrate"];

            //vga
        if (jsonObject["vga视频格式"] != null) vga视频格式 = (string)jsonObject["vga视频格式"];
        if (jsonObject["vga_r"] != null) vga_r = (string)jsonObject["vga_r"];
        if (jsonObject["vga_g"] != null) vga_g = (string)jsonObject["vga_g"];
        if (jsonObject["vga_b"] != null) vga_b = (string)jsonObject["vga_b"];
        
        if (jsonObject["vga_内置图索引"] != null) vga_内置图索引 = (string)jsonObject["vga_内置图索引"];
        if (jsonObject["vga_BMP文件索引"] != null) vga_BMP文件索引 = (string)jsonObject["vga_BMP文件索引"];
        if (jsonObject["vga_图案组索引"] != null) vga_图案组索引 = (string)jsonObject["vga_图案组索引"];
        if (jsonObject["vga_左声道频率"] != null) vga_左声道频率 = (string)jsonObject["vga_左声道频率"];
        if (jsonObject["vga_右声道频率"] != null) vga_右声道频率 = (string)jsonObject["vga_右声道频率"];
        if (jsonObject["vga_音乐文件索引"] != null) vga_音乐文件索引 = (string)jsonObject["vga_音乐文件索引"];

        if (jsonObject["vga_EDID开关"] != null) vga_EDID开关 = (bool)jsonObject["vga_EDID开关"];
        if (jsonObject["vga_osd开关"] != null) vga_osd开关 = (bool)jsonObject["vga_osd开关"];
        if (jsonObject["vga_RGB纯色图案"] != null) vga_RGB纯色图案 = (bool)jsonObject["vga_RGB纯色图案"];
        if (jsonObject["vga_内置图案"] != null) vga_内置图案 = (bool)jsonObject["vga_内置图案"];
        if (jsonObject["vga_BMP图案"] != null) vga_BMP图案 = (bool)jsonObject["vga_BMP图案"];
        if (jsonObject["vga_图案组"] != null) vga_图案组 = (bool)jsonObject["vga_图案组"];
        if (jsonObject["vga_静音"] != null) vga_静音 = (bool)jsonObject["vga_静音"];
        if (jsonObject["vga_正弦波"] != null) vga_正弦波 = (bool)jsonObject["vga_正弦波"];
        if (jsonObject["vga_WAV音乐文件"] != null) vga_WAV音乐文件 = (bool)jsonObject["vga_WAV音乐文件"];

        //dp
        if (jsonObject["dp视频格式"] != null) dp视频格式 = (string)jsonObject["dp视频格式"];
        if (jsonObject["dp_r"] != null) dp_r = (string)jsonObject["dp_r"];
        if (jsonObject["dp_g"] != null) dp_g = (string)jsonObject["dp_g"];
        if (jsonObject["dp_b"] != null) dp_b = (string)jsonObject["dp_b"];
        if (jsonObject["dp_内置图索引"] != null) dp_内置图索引 = (string)jsonObject["dp_内置图索引"];
        if (jsonObject["dp_BMP文件索引"] != null) dp_BMP文件索引 = (string)jsonObject["dp_BMP文件索引"];
        if (jsonObject["dp_图案组索引"] != null) dp_图案组索引 = (string)jsonObject["dp_图案组索引"];
        if (jsonObject["dp_左声道频率"] != null) dp_左声道频率 = (string)jsonObject["dp_左声道频率"];
        if (jsonObject["dp_右声道频率"] != null) dp_右声道频率 = (string)jsonObject["dp_右声道频率"];
        if (jsonObject["dp_音乐文件索引"] != null) dp_音乐文件索引 = (string)jsonObject["dp_音乐文件索引"];

        if (jsonObject["dp_hdcp开关"] != null) dp_hdcp开关 = (bool)jsonObject["dp_hdcp开关"];
        if (jsonObject["dp_edid开关"] != null) dp_edid开关 = (bool)jsonObject["dp_edid开关"];
        if (jsonObject["dp_osd开关"] != null) dp_osd开关 = (bool)jsonObject["dp_osd开关"];
        if (jsonObject["dp_RGB纯色图案"] != null) dp_RGB纯色图案 = (bool)jsonObject["dp_RGB纯色图案"];
        if (jsonObject["dp_内置图案"] != null) dp_内置图案 = (bool)jsonObject["dp_内置图案"];
        if (jsonObject["dp_BMP图案"] != null) dp_BMP图案 = (bool)jsonObject["dp_BMP图案"];
        if (jsonObject["dp_图案组"] != null) dp_图案组 = (bool)jsonObject["dp_图案组"];
        if (jsonObject["dp_静音"] != null) dp_静音 = (bool)jsonObject["dp_静音"];
        if (jsonObject["dp_正弦波"] != null) dp_正弦波 = (bool)jsonObject["dp_正弦波"];
        if (jsonObject["dp_WAV音乐文件"] != null) dp_WAV音乐文件 = (bool)jsonObject["dp_WAV音乐文件"];

        //hdmi
        if (jsonObject["hdmi视频格式"] != null) hdmi视频格式 = (string)jsonObject["hdmi视频格式"];
        if (jsonObject["hdmi_r"] != null) hdmi_r = (string)jsonObject["hdmi_r"];
        if (jsonObject["hdmi_g"] != null) hdmi_g = (string)jsonObject["hdmi_g"];
        if (jsonObject["hdmi_b"] != null) hdmi_b = (string)jsonObject["hdmi_b"];
        if (jsonObject["hdmi_内置图索引"] != null) hdmi_内置图索引 = (string)jsonObject["hdmi_内置图索引"];
        if (jsonObject["hdmi_BMP文件索引"] != null) hdmi_BMP文件索引 = (string)jsonObject["hdmi_BMP文件索引"];
        if (jsonObject["hdmi_图案组索引"] != null) hdmi_图案组索引 = (string)jsonObject["hdmi_图案组索引"];
        if (jsonObject["hdmi_左声道频率"] != null) hdmi_左声道频率 = (string)jsonObject["hdmi_左声道频率"];
        if (jsonObject["hdmi_右声道频率"] != null) hdmi_右声道频率 = (string)jsonObject["hdmi_右声道频率"];
        if (jsonObject["hdmi_音乐文件索引"] != null) hdmi_音乐文件索引 = (string)jsonObject["hdmi_音乐文件索引"];

        if (jsonObject["hdmi_EDID开关"] != null) hdmi_edid开关 = (bool)jsonObject["hdmi_EDID开关"];
        if (jsonObject["hdmi_osd开关"] != null) hdmi_osd开关 = (bool)jsonObject["hdmi_osd开关"];
        if(jsonObject["hdmi_cec开关"] != null) hdmi_cec开关 = (bool)jsonObject["hdmi_cec开关"];
        if (jsonObject["hdmi_arc开关"] != null) hdmi_arc开关 = (bool)jsonObject["hdmi_arc开关"];
        if (jsonObject["hdmi_RGB纯色图案"] != null) hdmi_RGB纯色图案 = (bool)jsonObject["hdmi_RGB纯色图案"];
        if (jsonObject["hdmi_内置图案"] != null) hdmi_内置图案 = (bool)jsonObject["hdmi_内置图案"];
        if (jsonObject["hdmi_BMP图案"] != null) hdmi_BMP图案 = (bool)jsonObject["hdmi_BMP图案"];
        if (jsonObject["hdmi_图案组"] != null) hdmi_图案组 = (bool)jsonObject["hdmi_图案组"];
        if (jsonObject["hdmi_静音"] != null) hdmi_静音 = (bool)jsonObject["hdmi_静音"];
        if (jsonObject["hdmi_正弦波"] != null) hdmi_正弦波 = (bool)jsonObject["hdmi_正弦波"];
        if (jsonObject["hdmi_WAV音乐文件"] != null) hdmi_WAV音乐文件 = (bool)jsonObject["hdmi_WAV音乐文件"];

        //crosstalk
        if (jsonObject["crosstalk_vga"] != null) crosstalk_vga = (bool)jsonObject["crosstalk_vga"];
        if (jsonObject["crosstalk_dp"] != null) crosstalk_dp = (bool)jsonObject["crosstalk_dp"];
        if(jsonObject["crosstalk_hdmi"] != null) crosstalk_hdmi = (bool)jsonObject["crosstalk_hdmi"];
        if (jsonObject["crosstalk_back_r"] != null) crosstalk_back_r = (string)jsonObject["crosstalk_back_r"];
        if (jsonObject["crosstalk_back_g"] != null) crosstalk_back_g = (string)jsonObject["crosstalk_back_g"];
        if (jsonObject["crosstalk_back_b"] != null) crosstalk_back_b = (string)jsonObject["crosstalk_back_b"];
        if (jsonObject["crosstalk_box_r"] != null) crosstalk_box_r = (string)jsonObject["crosstalk_box_r"];
        if (jsonObject["crosstalk_box_g"] != null) crosstalk_box_g = (string)jsonObject["crosstalk_box_g"];
        if (jsonObject["crosstalk_box_b"] != null) crosstalk_box_b = (string)jsonObject["crosstalk_box_b"];

    file.Close();
            return true;
        }
    }
