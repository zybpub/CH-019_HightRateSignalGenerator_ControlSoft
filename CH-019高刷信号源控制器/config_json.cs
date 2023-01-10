    class config_json
    {
        public static string config_file_name = "config_color.json";
        public static string serial_portname = "COM1";
        public static int baudrate = 115200;
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
            file.Close();
            return true;
        }
    }
