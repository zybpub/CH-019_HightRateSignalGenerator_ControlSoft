using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace CH_019高刷信号源控制器
{ /// <summary>
  /// SuperSerialPort
  /// </summary>
    public class SuperSerialPort
    {
        private SerialPort CommPort = new SerialPort();

        public SuperSerialPort()
        {
            CommPort.DataReceived += serialport_DataReceived;
        }

        private StringBuilder SerialDataReceived = new StringBuilder();

        private void serialport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //读取数据
            int length = CommPort.BytesToRead;
            byte[] buffers = new byte[length];
            CommPort.Read(buffers, 0, length);
            //调用委托事件
            DataReceived.Invoke(buffers);
        }

        #region 公有属性
        /// <summary>
        /// 端口
        /// </summary>
        public string PortName
        {
            get { return CommPort.PortName; }
        }

        /// <summary>
        /// 获取 端口列表 
        /// </summary>
        public List<string> GetPortNames()
        {
            return SerialPort.GetPortNames().ToList<string>();
        }


        /// <summary>
        /// 获取 奇偶校验位 列表 
        /// </summary>
        /// <returns></returns>
        public List<string> GetParitys()
        {
            List<string> list = new List<string>();
            foreach (string str in Enum.GetNames(typeof(Parity)))
            {
                list.Add(str);
            }
            return list;
        }

        /// <summary>
        /// 获取 停止位 列表 
        /// </summary>
        /// <returns></returns>
        public List<string> GetStopBits()
        {
            List<string> list = new List<string>();
            foreach (string str in Enum.GetNames(typeof(StopBits)))
            {
                list.Add(str);
            }
            return list;
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        public string Open()
        {
            if (!CommPort.IsOpen)
            {
                try
                {
                    CommPort.Open();
                    return "OK";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "OK";
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            if (CommPort.IsOpen)
            {
                CommPort.Close();
            }
        }
        #endregion

        #region 设置串口信息
        /// <summary>
        /// 设置串口信息
        /// </summary>
        public string SetPortInfo(string PortName, string Parity, string Stop, string Data, string BaudRate)
        {
            try
            {
            CommPort.PortName = PortName;
            CommPort.Parity = (Parity)Enum.Parse(typeof(Parity), Parity);
            CommPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Stop);
            CommPort.DataBits = int.Parse(Data);
            CommPort.BaudRate = int.Parse(BaudRate);
                return "OK";
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region HexToByte
        /// <summary>
        /// HexToByte
        /// </summary>
        public byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            return comBuffer;
        }
        #endregion

        #region ByteToHex
        /// <summary>
        /// ByteToHex
        /// </summary>
        public string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            foreach (byte data in comByte)
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0')+" ");
            return builder.ToString().ToUpper();
        }
        #endregion

        #region 接收事件
        /// <summary>
        /// 
        /// </summary>
        public Action<byte[]> DataReceived { get; set; }
        #endregion

        #region 发送事件
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmd">十六进制字符串</param>
        public void Send(string cmd)
        {
            if (!string.IsNullOrEmpty(cmd) && CommPort.IsOpen)
            {
                byte[] serOrder = HexToByte(cmd);

                CommPort.Write(serOrder, 0, serOrder.Length);
            }
        }

        /// <summary>
        /// byte数组
        /// </summary>
        /// <param name="cmdbytes"></param>
        public void Send(byte[] cmdbytes)
        {
            if (cmdbytes?.Length > 0 && CommPort.IsOpen)
            {
                CommPort.Write(cmdbytes, 0, cmdbytes.Length);

            }
        }
        #endregion

    }
}

/*
调用：

SuperSerialPort ssp = new SuperSerialPort();
ssp.SetPortInfo("COM4", "None", "One", "8", "9600");
ssp.DataReceived = new Action<byte[]>(DataReceived);
/// <summary>
/// 回调方法
/// </summary>
/// <param name="res"></param>
 private void DataReceived(byte[] res)
{
    //处理串口返回的数据
}
*/