using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VSM
{
    public partial class vsmWindow : Form
    {

        private byte[] revBuf;
        private byte[] sendBuf;

        Process currentProcess = Process.GetCurrentProcess();
        public vsmWindow()
        {
            InitializeComponent();

            DownLink = new Thread(Recive);
            DownLink.Start();

        }

        //-----------------------------winform 부분------------------------------------

        public void stanagePacketPint()
        {

        }

        public void MavlinkPacketPint()
        {

        }



        //----------------------------VSM 부분    -------------------------------------

        public void ProtocolChecker(byte[] buf) // 프로토콜 구별 함수 (Thread)
        {
            string marker = "0x" + BitConverter.ToUInt32(buf, 0).ToString("X");
            //Console.WriteLine(marker);

            if (marker.Equals("0x48"))
            {

            }
            else if (marker.Equals("0xFD"))
            {

                Console.WriteLine("\n\n");
                Console.WriteLine("\nSCM에서 수신받은 MAVLink Byte 값\n");
                Console.WriteLine(" magic  len incompat  compat  seq  sysid  comid  msgid              Palyload                 checksum");


                uint[] DecodeBuf = MAVLink_Decode(buf);

                if (revML1.InvokeRequired)
                {
                    revML1.Invoke(new Action(() => { revML1.Text = "STX" + ": " + DecodeBuf[0]; }));
                }
                else
                {
                    revML1.Text = "STX" + ": " + DecodeBuf[0];
                }

                if (revML2.InvokeRequired)
                {
                    revML2.Invoke(new Action(() => { revML2.Text = "INC FLAGS" + ": " + DecodeBuf[1]; }));
                }
                else
                {
                    revML2.Text = "INC FLAGS" + ": " + DecodeBuf[1];
                }

                if (revML3.InvokeRequired)
                {
                    revML3.Invoke(new Action(() => { revML3.Text = "CMP FLAGS" + ": " + DecodeBuf[2]; }));
                }
                else
                {
                    revML3.Text = "CMP FLAGS" + ": " + DecodeBuf[2];
                }

                if (revML4.InvokeRequired)
                {
                    revML4.Invoke(new Action(() => { revML4.Text = "LEN" + ": " + DecodeBuf[3]; }));
                }
                else
                {
                    revML4.Text = "LEN" + ": " + DecodeBuf[3];
                }

                if (revML5.InvokeRequired)
                {
                    revML5.Invoke(new Action(() => { revML5.Text = "SEQ" + ": " + DecodeBuf[4]; }));
                }
                else
                {
                    revML5.Text = "SEQ" + ": " + DecodeBuf[4];
                }

                if (revML6.InvokeRequired)
                {
                    revML6.Invoke(new Action(() => { revML6.Text = "SYS ID" + ": " + DecodeBuf[5]; }));
                }
                else
                {
                    revML6.Text = "SYS ID" + ": " + DecodeBuf[5];
                }

                if (revML7.InvokeRequired)
                {
                    revML7.Invoke(new Action(() => { revML7.Text = "COMP ID" + ": " + DecodeBuf[6]; }));
                }
                else
                {
                    revML7.Text = "COMP ID" + ": " + DecodeBuf[6];
                }

                if (revML8.InvokeRequired)
                {
                    revML8.Invoke(new Action(() => { revML8.Text = "MSG ID" + ": " + DecodeBuf[7]; }));
                }
                else
                {
                    revML8.Text = "MSG ID" + ": " + DecodeBuf[7];
                }

                if (revML9.InvokeRequired)
                {
                    revML9.Invoke(new Action(() =>
                    {
                        revML9.Text = "DATA" + ": ";
                        for (int i = 8; i < DecodeBuf.Length - 1; ++i)
                        {
                            revML9.Text += $"{ UintToDouble(DecodeBuf[i]).ToString("F3") + "   "}";

                        }

                    }));
                }
                else
                {
                    revML9.Text = "DATA" + ": ";
                    if (DecodeBuf[7] == 30)
                    {
                        for (int i = 8; i < DecodeBuf.Length - 1; ++i)
                        {
                            revML9.Text += $"{ (UintToDouble(DecodeBuf[i]) * (180 / 3.14)).ToString("F3") + "   "}";

                        }
                    }
                    if (DecodeBuf[7] == 33)
                    {
                        for (int i = 8; i < DecodeBuf.Length - 1; ++i)
                        {
                            revML9.Text += $"{ (UintToDouble(DecodeBuf[i]) * Math.Pow(10, -7)).ToString("F3") + "   "}";

                        }
                    }
                    if (DecodeBuf[7] == 230)
                    {
                        for (int i = 8; i < DecodeBuf.Length - 1; ++i)
                        {
                            revML9.Text += $"{ (UintToDouble(DecodeBuf[i]) / 100).ToString("F3") + "   "}";

                        }
                    }
                    if (DecodeBuf[7] == 147)
                    {

                        revML9.Text += $"{ (UintToDouble(DecodeBuf[8]) / (100 / 90)).ToString("F3") + "   "}";
                        revML9.Text += $"{ (UintToDouble(DecodeBuf[9]) / 1000).ToString("F3") + "   "}";
                        revML9.Text += $"{ (UintToDouble(DecodeBuf[10]) / 100).ToString("F3") + "   "}";
                    }
                    if (DecodeBuf[7] == 291)
                    {
                        for (int i = 8; i < DecodeBuf.Length - 1; ++i)
                        {
                            revML9.Text += $"{ UintToDouble(DecodeBuf[i]).ToString("F3") + "   "}";

                        }
                    }
                }

                if (revML10.InvokeRequired)
                {
                    revML10.Invoke(new Action(() => { revML10.Text = "CHECKSUM" + ": " + DecodeBuf[DecodeBuf.Length - 1]; }));
                }
                else
                {
                    revML10.Text = "CHECKSUM" + ": " + DecodeBuf[DecodeBuf.Length - 1];
                }


                for (int i = 0; i < DecodeBuf.Length; ++i)
                {

                    if (i > 7 && i < DecodeBuf.Length)
                    {

                        Console.Write(" " + UintToDouble(DecodeBuf[i]) + " ");
                    }
                    else
                    {

                        Console.Write("   " + DecodeBuf[i] + "  ");
                    }
                }


                MAVLinkToSTANAG(DecodeBuf);


            }

        }



        void paint(uint[] _SendPacket) //STANAG-4586 패킷 표시
        {
            if (revStanag1.InvokeRequired)
            {
                revStanag1.Invoke(new Action(() => { revStanag1.Text = "START" + ": " + _SendPacket[0]; }));
            }
            else
            {
                revStanag1.Text = "START" + ": " + _SendPacket[0];
            }

            if (revStanag2.InvokeRequired)
            {
                revStanag2.Invoke(new Action(() => { revStanag2.Text = "SOURCE*" + ": " + _SendPacket[1]; }));
            }
            else
            {
                revStanag2.Text = "SOURCE*" + ": " + _SendPacket[1];
            }


            if (revStanag3.InvokeRequired)
            {
                revStanag3.Invoke(new Action(() => { revStanag3.Text = "DEST*" + ": " + _SendPacket[2]; }));
            }
            else
            {
                revStanag3.Text = "DEST*" + ": " + _SendPacket[2];
            }

            if (revStanag4.InvokeRequired)
            {
                revStanag4.Invoke(new Action(() => { revStanag4.Text = "MSG LEN" + ": " + _SendPacket[3]; }));
            }
            else
            {
                revStanag4.Text = "MSG LEN" + ": " + _SendPacket[3];
            }

            if (revStanag5.InvokeRequired)
            {
                revStanag5.Invoke(new Action(() => { revStanag5.Text = "SOURCE ID" + ": " + _SendPacket[4]; }));
            }
            else
            {
                revStanag5.Text = "SOURCE ID" + ": " + _SendPacket[4];
            }

            if (revStanag6.InvokeRequired)
            {
                revStanag6.Invoke(new Action(() => { revStanag6.Text = "DEST ID" + ": " + _SendPacket[5]; }));
            }
            else
            {
                revStanag6.Text = "DEST ID" + ": " + _SendPacket[5];
            }
            if (revStanag7.InvokeRequired)
            {
                revStanag7.Invoke(new Action(() => { revStanag7.Text = "MSG ID" + ": " + _SendPacket[6]; }));
            }
            else
            {
                revStanag7.Text = "MSG ID" + ": " + _SendPacket[6];
            }

            if (revStanag8.InvokeRequired)
            {
                revStanag8.Invoke(new Action(() => { revStanag8.Text = "MSG TYPE" + ": " + _SendPacket[7]; }));
            }
            else
            {
                revStanag8.Text = "MSG TYPE" + ": " + _SendPacket[7];
            }

            if (revStanag9.InvokeRequired)
            {
                revStanag9.Invoke(new Action(() => { revStanag9.Text = "PROPERITES" + ": " + _SendPacket[8]; }));
            }
            else
            {
                revStanag9.Text = "PROPERITES" + ": " + _SendPacket[8];
            }

            if (revStanag10.InvokeRequired)
            {
                revStanag10.Invoke(new Action(() =>
                {
                    revStanag10.Text = "DATA" + ": ";
                    if (_SendPacket[6] == 4000)
                    {

                        DateTime dateTime = timestempToDate(_SendPacket);
                        revStanag10.Text += $"{ _SendPacket[9] + "   "}";
                        revStanag10.Text += $"{ dateTime.ToString("HH:mm:ss") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[11]) * ((2 * Math.PI) / 65536)).ToString("F3") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[12]) * ((2 * Math.PI) / 65536)).ToString("F3") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[13]) * ((2 * Math.PI) / 65536)).ToString("F3") + "   "}";
                    }

                    if (_SendPacket[6] == 4002)
                    {
                        DateTime dateTime = timestempToDate(_SendPacket);
                        revStanag10.Text += $"{ _SendPacket[9] + "   "}";
                        revStanag10.Text += $"{ dateTime.ToString("HH:mm:ss") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[11]) * ((2 * Math.PI) / 65536)).ToString("F3") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[12]) * ((2 * Math.PI) / 65536)).ToString("F3") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[13]) * 1000).ToString("F3") + "   "}";
                    }

                    if (_SendPacket[6] == 4003) //speed
                    {
                        DateTime dateTime = timestempToDate(_SendPacket);
                        revStanag10.Text += $"{ _SendPacket[9] + "   "}";
                        revStanag10.Text += $"{ dateTime.ToString("HH:mm:ss") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[11]) * 100).ToString("F3") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[12]) * 100).ToString("F3") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[13]) * 100).ToString("F3") + "   "}";
                    }

                    if (_SendPacket[6] == 12101)
                    {
                        DateTime dateTime = timestempToDate(_SendPacket);
                        revStanag10.Text += $"{ _SendPacket[9] + "   "}";
                        revStanag10.Text += $"{ dateTime.ToString("HH:mm:ss") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[11]) - 273.15).ToString("F3") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[12]) / 0.9).ToString("F3") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[13]) / 1.05).ToString("F3") + "   "}";
                    }
                    
                  
                    if (_SendPacket[6] == 3999)
                    {
                        DateTime dateTime = timestempToDate(_SendPacket);
                        revStanag10.Text += $"{ _SendPacket[9] + "   "}";
                        revStanag10.Text += $"{ dateTime.ToString("HH:mm:ss") + "   "}";
                        revStanag10.Text += $"{ (UintToDouble(_SendPacket[11])).ToString("F3") + "   "}";
                       
                    }

                }));
            }
            else
            {
                revStanag10.Text = "DATA" + ": ";
                for (int i = 9; i < _SendPacket.Length - 1; ++i)
                {
                    revStanag10.Text += $"{ _SendPacket[i] + "   "}";

                }
            }

            if (revStanag11.InvokeRequired)
            {
                revStanag11.Invoke(new Action(() => { revStanag11.Text = "CHECKSUM" + ": " + _SendPacket[_SendPacket.Length - 1]; }));
            }
            else
            {
                revStanag11.Text = "CHECKSUM" + ": " + _SendPacket[_SendPacket.Length - 1];
            }
        }

        private void AuthorizationCHecker() // 수신받은 데이터가 STANAG일 경우 사용
        {

        }

        private void MAVLinkToSTANAG(uint[] _DecodeBuf) // MAVLINK에서 STANAG-4586으로 프로토콜 변환
        {
            uint msgID = _DecodeBuf[7];

            if (msgID == 30)
            {
                uint[] message = Create_Inertial_States(_DecodeBuf);

                uint[] SendPacket = STANAG_Packet(message, 4000, (UInt16)_DecodeBuf[5]);

                byte[] sendData = STANAG_Encode(SendPacket);

                Console.WriteLine("\n\nMAVLink를 STANAG-4586으로 변환\n");
                Console.WriteLine(" start sourceP destP  msgLen  sourceID  destID  msgID  msgType  properties  vecotr  time  roll  pitch  heading  checksum");

                for (int i = 0; i < SendPacket.Length; ++i)
                {

                    Console.Write("   " + SendPacket[i] + "  ");

                }
                SendByGCS(sendData);
                paint(SendPacket);
            }

            if (msgID == 33)
            {
                uint[] message = Create_Inertial_States_Position(_DecodeBuf);

                uint[] SendPacket = STANAG_Packet(message, 4002, (UInt16)_DecodeBuf[5]);

                byte[] sendData = STANAG_Encode(SendPacket);

                Console.WriteLine("\n\nMAVLink를 STANAG-4586으로 변환\n");
                Console.WriteLine(" start sourceP destP  msgLen  sourceID  destID  msgID2  msgType  properties  vecotr  time  alt long  alt  checksum");
                for (int i = 0; i < SendPacket.Length; ++i)
                {

                    Console.Write("   " + SendPacket[i] + "  ");

                }
                paint(SendPacket);
                SendByGCS(sendData);
            }
            if (msgID == 230)
            {
                uint[] message = Create_Inertial_States_speed(_DecodeBuf);

                uint[] SendPacket = STANAG_Packet(message, 4003, (UInt16)_DecodeBuf[5]);

                byte[] sendData = STANAG_Encode(SendPacket);

                Console.WriteLine("\n\nMAVLink를 STANAG-4586으로 변환\n");
                Console.WriteLine(" start sourceP destP  msgLen  sourceID  destID  msgID3  msgType  properties  vecotr  time  USpeed  VSpeed  WSpeed  checksum");
                for (int i = 0; i < SendPacket.Length; ++i)
                {

                    Console.Write("   " + SendPacket[i] + "  ");

                }
                SendByGCS(sendData);
                paint(SendPacket);
            }
            if (msgID == 147)
            {
                uint[] message = Create_Battery_Report(_DecodeBuf);

                uint[] SendPacket = STANAG_Packet(message, 12101, (UInt16)_DecodeBuf[5]);

                byte[] sendData = STANAG_Encode(SendPacket);

                Console.WriteLine("\n\nMAVLink를 STANAG-4586으로 변환\n");
                Console.WriteLine(" start sourceP destP  msgLen  sourceID  destID  msgID4  msgType  properties  vecotr  time  roll  pitch  heading  checksum");
                for (int i = 0; i < SendPacket.Length; ++i)
                {

                    Console.Write("   " + SendPacket[i] + "  ");

                }
                SendByGCS(sendData);
                paint(SendPacket);
            }


            if (msgID == 291)
            {
                uint[] message = Create_Electric_Motor_Status(_DecodeBuf);

                uint[] SendPacket = STANAG_Packet(message, 3999, (UInt16)_DecodeBuf[5]);

                byte[] sendData = STANAG_Encode(SendPacket);

                Console.WriteLine("\n\nMAVLink를 STANAG-4586으로 변환\n");
                Console.WriteLine(" start sourceP destP  msgLen  sourceID  destID  msgID5  msgType  properties  vecotr time  Index  TimeUsec rpm  checksum");
                for (int i = 0; i < SendPacket.Length; ++i)
                {

                    Console.Write("   " + SendPacket[i] + "  ");

                }
                //SendByGCS(sendData);
                paint(SendPacket);

            }

            Console.WriteLine("\n\n");
        }
        DateTime timestempToDate(uint[] _SendPacket)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTime = unixEpoch.AddSeconds(_SendPacket[10]).ToLocalTime();
            return dateTime;
        }
        private void STANAGtoMAVLink() //stanag에서 mavlink로 변환(현재는 불가능)
        {

        }

        public static uint DoubleToUint(double value)
        {
            long longValue = BitConverter.DoubleToInt64Bits(value);
            // 최상위 비트를 부호 비트로 사용
            bool isNegative = value < 0;
            uint uintValue = (uint)(longValue >> 32);
            if (isNegative)
            {
                uintValue |= 0x80000000; // 최상위 비트를 1로 설정하여 음수 표시
            }
            return uintValue;
        }

        public static double UintToDouble(uint value)
        {
            // 최상위 비트를 체크하여 음수 여부 확인
            bool isNegative = (value & 0x80000000) != 0;
            uint actualValue = value & 0x7FFFFFFF; // 최상위 비트를 제거하여 실제 값 추출
            long longValue = ((long)actualValue) << 32;
            double doubleValue = BitConverter.Int64BitsToDouble(longValue);
            return isNegative ? -doubleValue : doubleValue;
        }


        //----------------------------STANAG 부분 -------------------------------------

        private byte[] STANAG_Encode(uint[] _packet)
        {
            byte[] sendBuf = new byte[_packet.Length * sizeof(uint)];
            int cnt = 0;

            for (int i = 0; i < _packet.Length; ++i)
            {
                byte[] uintToByte = BitConverter.GetBytes(_packet[i]);
                for (int j = 0; j < 4; ++j)
                {
                    sendBuf[cnt + j] = uintToByte[j];

                }

                cnt += 4;
            }

            return sendBuf;
        }

        private void STANAG_Decode()
        {

        }
        private uint[] STANAG_Packet(uint[] Data, UInt16 _messagID, UInt16 _sourcID)
        {
            uint[] PacketBuf = new uint[11 + Data.Length];
            UInt16 start = 0x45; //프로토콜 종류 표시
            UInt16 sourcePort;//UDP
            UInt16 destinationPort;//UDP  
            UInt16 messageLength;
            UInt16 sourceID; //출발지 ID
            UInt16 destinationID; //목적지 ID
            UInt16 messagID;
            UInt16 messageType; //push 0, pull 1
            UInt16 messageProperties;  // ACK, Version32, Cecksum0 총 15bitmap // 일단 보류
            uint[] data;
            UInt16 optionalChecksum;

            sourcePort = (UInt16)DownLinkPort;
            destinationPort = (UInt16)DownLinkPort;
            messageLength = (UInt16)Data.Length;
            sourceID = _sourcID;
            destinationID = 255; //GCS를 255로 임시 정의함
            messagID = _messagID;
            messageType = 0;
            messageProperties = 0; //보류
            data = Data; // 임시로 변경
            optionalChecksum = 0;

            PacketBuf[0] = start;
            PacketBuf[1] = sourcePort;
            PacketBuf[2] = destinationPort;
            PacketBuf[3] = messageLength;
            PacketBuf[4] = sourceID;
            PacketBuf[5] = destinationID;
            PacketBuf[6] = messagID;
            PacketBuf[7] = messageType;
            PacketBuf[8] = messageProperties;

            for (int i = 0; i < messageLength; ++i)
            {
                PacketBuf[9 + i] = data[i];

            }

            PacketBuf[PacketBuf.Length - 1] = optionalChecksum;

            return PacketBuf;
        }
        //사용 가능한 필드만 작성했습니다.

        enum PAYLOAD_TYPE
        {
            NotSpecified = 0,
            FixedCamera = 6,
        }
        enum VEHICLE_TYPE //사용자가 정의해야함
        {
            QUADROTOR = 0,
            FIXED_WING = 1
        }
        private void Create_CUCS_Authorisation_Request() //#1
        {
            uint PresenceVector;
            uint TimeStamp;
            uint VehicleType; //VEHICLE_TYPE
            uint PayloadType; //PAYLOAD_TYPE
            uint RequestedFlightMode;//FLIGHT_MODE

        }

        private void Create_VSM_Authorisation_Request() //#2 o
        {
            uint PresenceVector;
            uint TimeStamp;
            uint VehicleType; //VEHICLE_TYPE
            uint PayloadType; //PAYLOAD_TYPE
            uint FlightModesGranted;//FLIGHT_MODE
        }
        enum MOTOR_CMD
        {
            Stop = 0,
            Start = 1,
            Enable_Run = 2
        }
        private void Create_Electric_Motor_Command() //직접 만든 메시지 o
        {
            uint PresenceVector;
            uint TimeStamp;
            uint MotorNumber;
            uint MotorCommand; //MotorCMD 이용


        }
        enum FLIGHT_MODE
        {
            Flight_Director = 2,
            Wyapoint = 11,
            Loiter = 12,
            Autopilot = 15
        }
        private void Create_Vehicle_Operating_Mode_Command() //#2016
        {
            uint PresenceVector;
            uint TimeStamp;
            uint SelectFlightPathControlMode; //FLIGHT_MODE
        }

        private uint[] Create_Inertial_States(uint[] _buf) //#4000 o 항공기 자세상태
        {

            //BAM = rad * 128 / 3.14 (rad to BAM)
            DateTime now = DateTime.Now;
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Unix 타임스탬프로 변환
            uint unixTimestamp = (uint)(now.ToUniversalTime() - unixEpoch).TotalSeconds;

            uint[] buf = new uint[5];
            uint PresenceVector;
            uint TimeStamp;
            uint Roll; //BAM
            uint Pitch; //BAM
            uint Heading; //BAM

            PresenceVector = 0x00;
            TimeStamp = unixTimestamp;

            Roll = DoubleToUint(UintToDouble(_buf[8]) * (65536 / (2 * Math.PI)));
            Pitch = DoubleToUint(UintToDouble(_buf[9]) * (65536 / (2 * Math.PI)));
            Heading = DoubleToUint(UintToDouble(_buf[10]) * (65536 / (2 * Math.PI)));



            //Console.WriteLine(UintToDouble(_buf[10]) * (Math.Pow(2, 16) / (2 * Math.PI)) + "wwwwww");

            buf[0] = PresenceVector;
            buf[1] = TimeStamp;
            buf[2] = Roll;
            buf[3] = Pitch;
            buf[4] = Heading;


            return buf;
        }

        private uint[] Create_Inertial_States_Position(uint[] _buf) //#4002 o ksg 항공기 위도 및 경도
        {
            //BAM = rad * 128 / 3.14 (rad to BAM)
            DateTime now = DateTime.Now;
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Unix 타임스탬프로 변환
            uint unixTimestamp = (uint)(now.ToUniversalTime() - unixEpoch).TotalSeconds;

            uint[] buf = new uint[5];
            uint PresenceVector;
            uint TimeStamp;
            uint Latitude; //단위  BAM
            uint Longitude; //BAM
            uint Altitude; // m

            PresenceVector = 0x00;
            TimeStamp = unixTimestamp;
 

            // 위도, 경도, 고도 값을 변환하여 저장하는 코드
            Latitude = DoubleToUint(UintToDouble(_buf[8]) * (65536 / (2 * Math.PI))); //degE7 -> BAM
            Longitude = DoubleToUint(UintToDouble(_buf[9]) * (65536 / (2 * Math.PI))); //degE7 -> BAM
            Altitude = DoubleToUint(UintToDouble(_buf[10]) / 1000); //mm - m
            
            // 배열에 값 저장하는 코드
            buf[0] = PresenceVector;
            buf[1] = TimeStamp;
            buf[2] = Latitude;
            buf[3] = Longitude;
            buf[4] = Altitude;

            return buf;
        }

        private uint[] Create_Inertial_States_speed(uint[] _buf) //#4003 항공기 속도
        {
            //BAM = rad * 128 / 3.14 (rad to BAM)
            DateTime now = DateTime.Now;
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Unix 타임스탬프로 변환
            uint unixTimestamp = (uint)(now.ToUniversalTime() - unixEpoch).TotalSeconds;

            uint[] buf = new uint[5];
            uint PresenceVector;
            uint TimeStamp;
            uint USpeed; // 전후
            uint VSpeed; // 좌우
            uint WSpeed; // 위아래

            PresenceVector = 0x00;
            TimeStamp = unixTimestamp;


            USpeed = DoubleToUint(UintToDouble(_buf[8]) / 100);
            VSpeed = DoubleToUint(UintToDouble(_buf[9]) / 100);
            WSpeed = DoubleToUint(UintToDouble(_buf[10]) / 100);

            buf[0] = PresenceVector;
            buf[1] = TimeStamp;
            buf[2] = USpeed;
            buf[3] = VSpeed;
            buf[4] = WSpeed;


            return buf;
        }


        private uint[] Create_Battery_Report(uint[] _buf) //#12101 o ksg
        {
            DateTime now = DateTime.Now;
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Unix 타임스탬프로 변환
            uint unixTimestamp = (uint)(now.ToUniversalTime() - unixEpoch).TotalSeconds;

            uint[] buf = new uint[6];
            uint PresenceVector;
            uint TimeStamp;
            uint BusVoltage; //V
            uint BatteryCurrent;  //A
            //uint BatteryChargeRemaining; //%
            uint BatteryTemperature; //K

            PresenceVector = 0x00;
            TimeStamp = unixTimestamp;

            // 버스 전압, 배터리 전류, 잔여 충전량 및 온도를 변환하여 저장하는 코드
            BatteryTemperature = DoubleToUint(UintToDouble(_buf[8]) + 273.15);
            BatteryCurrent = DoubleToUint(UintToDouble(_buf[9]) * 0.9);
            BusVoltage = DoubleToUint(UintToDouble(_buf[10]) * 1.05);          
            //BatteryChargeRemaining = (uint)(_buf[2] * 0.95);
            

            // 배열에 값 저장하는 코드
            buf[0] = PresenceVector;
            buf[1] = TimeStamp;
            buf[2] = BatteryTemperature;
            buf[3] = (uint)BatteryCurrent;
            //buf[4] = BatteryChargeRemaining;
            buf[4] = BusVoltage;

            return buf;
        }

        private uint[] Create_Message_Acknowledgement() //#17000 o ksg
        {
            DateTime now = DateTime.Now;
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Unix 타임스탬프로 변환
            uint unixTimestamp = (uint)(now.ToUniversalTime() - unixEpoch).TotalSeconds;

            uint[] buf = new uint[3];
            uint presenceVector;
            uint TimeStamp;
            uint AcknowledgementType;

            presenceVector = 0x00;
            TimeStamp = unixTimestamp;

            // AcknowledgementType 설정하는 코드
            // 예시: 특정 조건에 따라 AcknowledgementType을 다르게 설정
            bool isMessageReceived = true; // 메시지가 수신되었는지 여부
            bool isError = false; // 오류 발생 여부

            if (isError)
            {
                AcknowledgementType = 2; // 2는 오류 응답을 나타내는 부분
            }
            else if (!isMessageReceived)
            {
                AcknowledgementType = 3; // 3은 메시지가 수신되지 않았음을 나타내는 부분
            }
            else
            {
                AcknowledgementType = 1; // 1은 정상적인 응답을 나타내는 부분
            }

            // 배열에 값 저장하는 코드
            buf[0] = presenceVector;
            buf[1] = TimeStamp;
            buf[2] = AcknowledgementType;

            return buf;
        }

        private uint[] Create_Electric_Motor_Status(uint[] _buf) //직접 만든 메시지 o ksg #3999
        {
            DateTime now = DateTime.Now;
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Unix 타임스탬프로 변환
            uint unixTimestamp = (uint)(now.ToUniversalTime() - unixEpoch).TotalSeconds;

            uint[] buf = new uint[5];
            uint presenceVector;
            uint TimeStamp;
            uint Index;
            uint TimeUsec; // us
            uint Rpm; //RPM
            //float Voltage; //V
            //float Current; //A

            presenceVector = 0x00;
            TimeStamp = unixTimestamp;

            // Index 설정 (예: 첫 번째 모터로 가정)
            Index = 1;

            // TimeUsec 계산: 현재 시간을 마이크로초 단위로 설정
            TimeUsec = (uint)(DateTime.UtcNow - unixEpoch).TotalMilliseconds * 1000;


            Rpm = _buf[8];
            // RPM 계산식: 예시로 랜덤값을 사용 (10에서 1000 사이의 값)
            // Random random = new Random();
            //Rpm = (uint)random.Next(10, 1001); // 랜덤으로 RPM 값 생성

            // Voltage 계산식: 예시로 배터리 전압을 랜덤으로 설정 (12V ~ 24V)
            //Voltage = 12.0f + (float)random.NextDouble() * (24.0f - 12.0f); // 12V에서 24V 사이의 전압 생성

            // Current 계산식: 예를 들어, 전압의 10%로 설정 (부하에 따라 다를 수 있음)
            // Current = Voltage * 0.1f;

            // 배열에 값 저장하는 코드
            buf[0] = presenceVector;
            buf[1] = TimeStamp;
            //buf[2] = Index;
            //buf[3] = TimeUsec;
            buf[2] = Rpm;
            //buf[5] = (uint)(Voltage * 100);
            //buf[6] = (uint)(Current * 100);

            return buf;
        }

        //----------------------------MAVLink 부분-------------------------------------

        private void MAVLink_Encode()
        {

        }

        private uint[] MAVLink_Decode(byte[] _reciveBuf)
        {
            uint[] buf = new uint[_reciveBuf.Length / (sizeof(byte) * 4)];
            int cnt = 0;

            for (int i = 0; i < _reciveBuf.Length / (sizeof(byte) * 4); ++i)
            {
                buf[i] = BitConverter.ToUInt32(_reciveBuf, cnt);
                cnt += 4;
            }

            return buf;
        }

        private void MAVLink_Packet()
        {
            uint magic; //0xFD
            uint len; //메시지 크기
            uint incompat_flags; //0x00
            uint compat_flags; //0x00
            uint seq; //메시지 순서
            uint sysid; //시스템 id (송신자 id ex) A가 B한테 보내면 A id를 기입)
            uint compid; //컴포넌트 id (A의 중앙제어기, 카메라 등의 각각의 ID)
            uint msgid; //메시지 id 
            byte[] payload; //데이터 값 (메시지 값의 타입이 섞여있어 byte로변경, 원래는 uint타입임)
            uint checksum; //체크섬 (일단, 0으로 대체)
            //uint[] signature; 미사용
        }

        private void Create_Heart_Beat()//#0번
        {
            uint type;
            uint autopilot;
            uint base_mode;
            uint custom_mode;
            uint system_status;
            uint mavlink_version;
        }

        private void Create_MAV_CMD_COMPONENT_ARM_DISARM() //#400번(CMD)
        {

        }

        private void Create_Attitude() //#30번
        {
            float roll; //rad
            float pitch; //rad
            float yaw; //rad
        }

        private void Create_Global_Position_Int()// #33
        {
            int lat; //degE7
            int lon; //degE7
            int alt; //mm
        }

        private void Create_Battery_Status() //#147
        {
            int temperature; //cdegC
            uint voltages; //mV
            int current_battery; //cA
            int battery_remaining; //%
        }

        private void Create_Command_Ack() //#77
        {
            //uint command; // MAV_CMD
            uint result; //MAV_RESULT
        }

        private void Create_Esc_Status() //#291
        {
            uint index;
            uint time_usec; //us
            uint rpm; //rpm
            uint voltage; //V
            uint current; //A
        }
        //MAVLink 열거형
        enum MAV_TYPE
        {
            MAV_TYPE_GENERIC = 0,
            MAV_TYPE_FIXED_WING = 1,
            MAV_TYPE_QUADROTOR = 2,
            MAV_TYPE_COAXIAL = 3,
            MAV_TYPE_HELICOPTER = 4,
            MAV_TYPE_ANTENNA_TRACKER = 5,
            MAV_TYPE_GCS = 6,
            MAV_TYPE_AIRSHIP = 7,
            MAV_TYPE_FREE_BALLOON = 8,
            MAV_TYPE_ROCKET = 9,
            MAV_TYPE_GROUND_ROVER = 10,
            MAV_TYPE_SURFACE_BOAT = 11,
            MAV_TYPE_SUBMARINE = 12,
            MAV_TYPE_HEXAROTOR = 13,
            MAV_TYPE_OCTOROTOR = 14,
            MAV_TYPE_TRICOPTER = 15,
            MAV_TYPE_FLAPPING_WING = 16,
            MAV_TYPE_KITE = 17,
            MAV_TYPE_ONBOARD_CONTROLLER = 18,
            MAV_TYPE_VTOL_TAILSITTER_DUOROTOR = 19,
            MAV_TYPE_VTOL_TAILSITTER_QUADROTOR = 20,
            MAV_TYPE_VTOL_TILTROTOR = 21,
            MAV_TYPE_VTOL_FIXEDROTOR = 22,
            MAV_TYPE_VTOL_TAILSITTER = 23,
            MAV_TYPE_VTOL_TILTWING = 24,
            MAV_TYPE_VTOL_RESERVED5 = 25,
            MAV_TYPE_GIMBAL = 26,
            MAV_TYPE_ADSB = 27,
            MAV_TYPE_PARAFOIL = 28,
            MAV_TYPE_DODECAROTOR = 29,
            MAV_TYPE_CAMERA = 30,
            MAV_TYPE_CHARGING_STATION = 31,
            MAV_TYPE_FLARM = 32,
            MAV_TYPE_SERVO = 33,
            MAV_TYPE_ODID = 34,
            MAV_TYPE_DECAROTOR = 35,
            MAV_TYPE_BATTERY = 36,
            MAV_TYPE_PARACHUTE = 37,
            MAV_TYPE_LOG = 38,
            MAV_TYPE_OSD = 39,
            MAV_TYPE_IMU = 40,
            MAV_TYPE_GPS = 41,
            MAV_TYPE_WINCH = 42,
            MAV_TYPE_GENERIC_MULTIROTOR = 43,
            MAV_TYPE_ILLUMINATOR = 44

        }

        enum MAV_AUTOPILOT
        {
            MAV_AUTOPILOT_GENERIC = 0,
            MAV_AUTOPILOT_RESERVED = 1,
            MAV_AUTOPILOT_SLUGS = 2,
            MAV_AUTOPILOT_ARDUPILOTMEGA = 3,
            MAV_AUTOPILOT_OPENPILOT = 4,
            MAV_AUTOPILOT_GENERIC_WAYPOINTS_ONLY = 5,
            MAV_AUTOPILOT_GENERIC_WAYPOINTS_AND_SIMPLE_NAVIGATION_ONLY = 6,
            MAV_AUTOPILOT_GENERIC_MISSION_FULL = 7,
            MAV_AUTOPILOT_INVALID = 8,
            MAV_AUTOPILOT_PPZ = 9,
            MAV_AUTOPILOT_UDB = 10,
            MAV_AUTOPILOT_FP = 11,
            MAV_AUTOPILOT_PX4 = 12,
            MAV_AUTOPILOT_SMACCMPILOT = 13,
            MAV_AUTOPILOT_AUTOQUAD = 14,
            MAV_AUTOPILOT_ARMAZILA = 15,
            MAV_AUTOPILOT_AEROB = 16,
            MAV_AUTOPILOT_ASLUAV = 17,
            MAV_AUTOPILOT_SMARTAP = 18,
            MAV_AUTOPILOT_AIRRAILS = 19,
            MAV_AUTOPILOT_REFLEX = 20

        }

        enum MAV_MODE_FLAG
        {
            MAV_MODE_FLAG_CUSTOM_MODE_ENABLED = 1,
            MAV_MODE_FLAG_TEST_ENABLED = 2,
            MAV_MODE_FLAG_AUTO_ENABLED = 4,
            MAV_MODE_FLAG_GUIDED_ENABLED = 8,
            MAV_MODE_FLAG_STABILIZE_ENABLED = 16,
            MAV_MODE_FLAG_HIL_ENABLED = 32,
            MAV_MODE_FLAG_MANUAL_INPUT_ENABLED = 64,
            MAV_MODE_FLAG_SAFETY_ARMED = 128

        }

        enum MAV_STATE
        {
            MAV_STATE_UNINIT = 0,
            MAV_STATE_BOOT = 1,
            MAV_STATE_CALIBRATING = 2,
            MAV_STATE_STANDBY = 3,
            MAV_STATE_ACTIVE = 4,
            MAV_STATE_CRITICAL = 5,
            MAV_STATE_EMERGENCY = 6,
            MAV_STATE_POWEROFF = 7,
            MAV_STATE_FLIGHT_TERMINATION = 8

        }

        enum MAV_SYS_STATUS_SENSOR
        {
            MAV_SYS_STATUS_SENSOR_3D_GYRO = 1,
            MAV_SYS_STATUS_SENSOR_3D_ACCEL = 2,
            MAV_SYS_STATUS_SENSOR_3D_MAG = 4,
            MAV_SYS_STATUS_SENSOR_ABSOLUTE_PRESSURE = 8,
            MAV_SYS_STATUS_SENSOR_DIFFERENTIAL_PRESSURE = 16,
            MAV_SYS_STATUS_SENSOR_GPS = 32,
            MAV_SYS_STATUS_SENSOR_OPTICAL_FLOW = 64,
            MAV_SYS_STATUS_SENSOR_VISION_POSITION = 128,
            MAV_SYS_STATUS_SENSOR_LASER_POSITION = 256,
            MAV_SYS_STATUS_SENSOR_EXTERNAL_GROUND_TRUTH = 512,
            MAV_SYS_STATUS_SENSOR_ANGULAR_RATE_CONTROL = 1024,
            MAV_SYS_STATUS_SENSOR_ATTITUDE_STABILIZATION = 2048,
            MAV_SYS_STATUS_SENSOR_YAW_POSITION = 4096,
            MAV_SYS_STATUS_SENSOR_Z_ALTITUDE_CONTROL = 8192,
            MAV_SYS_STATUS_SENSOR_XY_POSITION_CONTROL = 16384,
            MAV_SYS_STATUS_SENSOR_MOTOR_OUTPUTS = 32768,
            MAV_SYS_STATUS_SENSOR_RC_RECEIVER = 65536,
            MAV_SYS_STATUS_SENSOR_3D_GYRO2 = 131072,
            MAV_SYS_STATUS_SENSOR_3D_ACCEL2 = 262144,
            MAV_SYS_STATUS_SENSOR_3D_MAG2 = 524288,
            MAV_SYS_STATUS_GEOFENCE = 1048576,
            MAV_SYS_STATUS_AHRS = 2097152,
            MAV_SYS_STATUS_TERRAIN = 4194304,
            MAV_SYS_STATUS_REVERSE_MOTOR = 8388608,
            MAV_SYS_STATUS_LOGGING = 16777216,
            MAV_SYS_STATUS_SENSOR_BATTERY = 33554432,
            MAV_SYS_STATUS_SENSOR_PROXIMITY = 67108864,
            MAV_SYS_STATUS_SENSOR_SATCOM = 134217728,
            MAV_SYS_STATUS_PREARM_CHECK = 268435456,
            MAV_SYS_STATUS_OBSTACLE_AVOIDANCE = 536870912,
            MAV_SYS_STATUS_SENSOR_PROPULSION = 1073741824,
            MAV_SYS_STATUS_EXTENSION_USED = 2147483647

        }

        enum MAV_FRAME
        {
            MAV_FRAME_GLOBAL = 0,
            MAV_FRAME_LOCAL_NED = 1,
            MAV_FRAME_MISSION = 2,
            MAV_FRAME_GLOBAL_RELATIVE_ALT = 3,
            MAV_FRAME_LOCAL_ENU = 4,
            MAV_FRAME_GLOBAL_INT = 5,
            MAV_FRAME_GLOBAL_RELATIVE_ALT_INT = 6,
            MAV_FRAME_LOCAL_OFFSET_NED = 7,
            MAV_FRAME_BODY_NED = 8,
            MAV_FRAME_BODY_OFFSET_NED = 9,
            MAV_FRAME_GLOBAL_TERRAIN_ALT = 10,
            MAV_FRAME_GLOBAL_TERRAIN_ALT_INT = 11,
            MAV_FRAME_BODY_FRD = 12,
            MAV_FRAME_RESERVED_13 = 13,
            MAV_FRAME_RESERVED_14 = 14,
            MAV_FRAME_RESERVED_15 = 15,
            MAV_FRAME_RESERVED_16 = 16,
            MAV_FRAME_RESERVED_17 = 17,
            MAV_FRAME_RESERVED_18 = 18,
            MAV_FRAME_RESERVED_19 = 19,
            MAV_FRAME_LOCAL_FRD = 20,
            MAV_FRAME_LOCAL_FLU = 21

        }

        enum MAV_RESULT
        {
            MAV_RESULT_ACCEPTED = 0,
            MAV_RESULT_TEMPORARILY_REJECTED = 1,
            MAV_RESULT_DENIED = 2,
            MAV_RESULT_UNSUPPORTED = 3,
            MAV_RESULT_FAILED = 4,
            MAV_RESULT_IN_PROGRESS = 5,
            MAV_RESULT_CANCELLED = 6,
            MAV_RESULT_COMMAND_LONG_ONLY = 7,
            MAV_RESULT_COMMAND_INT_ONLY = 8,
            MAV_RESULT_COMMAND_UNSUPPORTED_MAV_FRAME = 9

        }
        enum MAV_BATTERY_FUNCTION
        {
            MAV_BATTERY_FUNCTION_UNKNOWN = 0,
            MAV_BATTERY_FUNCTION_ALL = 1,
            MAV_BATTERY_FUNCTION_PROPULSION = 2,
            MAV_BATTERY_FUNCTION_AVIONICS = 3,
            MAV_BATTERY_FUNCTION_PAYLOAD = 4

        }

        enum MAV_BATTERY_TYPE
        {
            MAV_BATTERY_TYPE_UNKNOWN = 0,
            MAV_BATTERY_TYPE_LIPO = 1,
            MAV_BATTERY_TYPE_LIFE = 2,
            MAV_BATTERY_TYPE_LION = 3,
            MAV_BATTERY_TYPE_NIMH = 4

        }


        enum MAV_BATTERY_CHARGE_STATE
        {
            MAV_BATTERY_CHARGE_STATE_UNDEFINED = 0,
            MAV_BATTERY_CHARGE_STATE_OK = 1,
            MAV_BATTERY_CHARGE_STATE_LOW = 2,
            MAV_BATTERY_CHARGE_STATE_CRITICAL = 3,
            MAV_BATTERY_CHARGE_STATE_EMERGENCY = 4,
            MAV_BATTERY_CHARGE_STATE_FAILED = 5,
            MAV_BATTERY_CHARGE_STATE_UNHEALTHY = 6,
            MAV_BATTERY_CHARGE_STATE_CHARGING = 7

        }

        enum MAV_BATTERY_MODE
        {
            MAV_BATTERY_MODE_UNKNOWN = 0,
            MAV_BATTERY_MODE_AUTO_DISCHARGING = 1,
            MAV_BATTERY_MODE_HOT_SWAP = 2,
        }

        enum MAV_BATTERY_FAULT
        {
            MAV_BATTERY_FAULT_DEEP_DISCHARGE = 1,
            MAV_BATTERY_FAULT_SPIKES = 2,
            MAV_BATTERY_FAULT_CELL_FAIL = 4,
            MAV_BATTERY_FAULT_OVER_CURRENT = 8,
            MAV_BATTERY_FAULT_OVER_TEMPERATURE = 16,
            MAV_BATTERY_FAULT_UNDER_TEMPERATURE = 32,
            MAV_BATTERY_FAULT_INCOMPATIBLE_VOLTAGE = 64,
            MAV_BATTERY_FAULT_INCOMPATIBLE_FIRMWARE = 128,
            BATTERY_FAULT_INCOMPATIBLE_CELLS_CONFIGURATION = 256
        }

        enum PRECISION_LAND_MODE
        {
            PRECISION_LAND_MODE_DISABLED = 0,
            PRECISION_LAND_MODE_OPPORTUNISTIC = 1,
            PRECISION_LAND_MODE_REQUIRED = 2
        }

        enum MAV_MODE
        {
            MAV_MODE_PREFLIGHT = 0,
            MAV_MODE_MANUAL_DISARMED = 64,
            MAV_MODE_TEST_DISARMED = 66,
            MAV_MODE_STABILIZE_DISARMED = 80,
            MAV_MODE_GUIDED_DISARMED = 88,
            MAV_MODE_AUTO_DISARMED = 92,
            MAV_MODE_MANUAL_ARMED = 192,
            MAV_MODE_TEST_ARMED = 194,
            MAV_MODE_STABILIZE_ARMED = 208,
            MAV_MODE_GUIDED_ARMED = 216,
            MAV_MODE_AUTO_ARMED = 220

        }
        //--------------------------DataLink 부분 --------------------------------------
        private Thread DownLink; // UA -> VSM (수신용)
        private static int UpLinkPort = 40000;
        private static int DownLinkPort = 30000;
        private static UdpClient client;
        private static IPEndPoint endPoint;
        IAsyncResult asyncResult;

        public static void SendByUAV(byte[] buf) //무인기로 패킷 전송
        {

            Console.WriteLine("send button\n");
            try
            {
                using (UdpClient client = new UdpClient())
                {

                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), UpLinkPort); //  항공기 ip주소
                    client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    client.Send(buf, buf.Length, endPoint);


                    Console.WriteLine("UAV로 send 성공");
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void SendByGCS(byte[] buf) //GCS로 패킷 전송
        {

            Console.WriteLine("send button\n");
            try
            {
                using (UdpClient client = new UdpClient())
                {

                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.23"), 1006); //  미션플래너 ip주소
                    client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    client.Send(buf, buf.Length, endPoint);


                    Console.WriteLine("GCS로 send 성공");
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        private void Recive()
        {
            //Console.WriteLine("recv");
            if (client != null)
            {
                Debug.WriteLine("이미 UDP 소켓이 생성되어있음..");
            }
            long usedMemory = currentProcess.WorkingSet64;
            Console.WriteLine($"사용 중인 메모리: {usedMemory / (1024 * 1024)} MB");
            Console.WriteLine("수신시작");
            client = new UdpClient();
            endPoint = new IPEndPoint(IPAddress.Any, DownLinkPort); //IPAddress.Any

            client.Client.Bind(endPoint); //특정 IP에게 수신시 bind 사용

            while (true)
            {
                if (client == null) { return; }

                try
                {
                    usedMemory = currentProcess.WorkingSet64;

                    byte[] bytes = client.Receive(ref endPoint);
                    string message = Encoding.UTF8.GetString(bytes, 0, bytes.Length);


                    //Console.WriteLine("Receive\n");

                    /*foreach (byte data in bytes)
                    {
                        Console.WriteLine("-----------" + data + "-----------------");
                    }*/
                    ProtocolChecker(bytes);
                    Console.WriteLine($"사용 중인 메모리: {usedMemory / (1024 * 1024)} MB");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
    }
}