using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.IO;

namespace xplane_data_test
{
    public partial class SCM : System.Windows.Forms.Form
    {
        //Xserver 필드
        private static int listenPort = 49000; //X-Plane에서 설정한 포트
        private static Dictionary<int, List<double>> Xserver; //Xplane 데이터를 저장하기위한 Dictionary, <int = index number, List<double>> = 실제 데이터 값>

        //private byte[] recive;
        private string protocolKind = ""; // x-plane -> protocol로 변환전 변환할 protocol를 확정하기 위해 사용  
        private static MAVLink MAV;
        Thread reciveT;
        Thread StartSCM;

        private static UdpClient client;
        private static IPEndPoint endPoint;
        private static int UpLinkPort = 40000;
        private static int DownLinkPort = 30000;

        private static byte[] reciveBuf; //VSM에서 받은 데이터

        //File 
        private String directoryName = @"C:\Users\minsung\Desktop\SCM_v0.2\SCM_v0.1\SCM_v0.1\protocalData.txt";

        //Mavlink
        private uint[] recivePacket; //VSM으로부터 수신받은 디코딩 된 패킷
        private byte[] sendPacket; //SCM에서 생성되어 VSM으로 보낼 인코딩후 패킷

        
        public SCM()
        {
            InitializeComponent();
            ProtocolBox.SelectedIndex = 0; //초기 실행시 기본값은 STANAG4586 이다.
            protocolKind = ProtocolBox.SelectedItem.ToString(); //초기 protocolKind를 설정 (STANAG 4586)

            // xServer 

            Thread testT = new Thread(stest);
            testT.Start();
            //여기까지



            Thread BeginReceive = new Thread(BeginReceiveStart);
            BeginReceive.Start();


        }


        private void stest() // xData test용
        {

            //test를 위한 임시 데이터 
            Xserver = new Dictionary<int, List<double>>();
            List<double> test1 = new List<double> { 80.8, 150.33, 90.22, 0, 0, 0, 0, 0 }; //17 roll, pitch, heading
            List<double> test2 = new List<double> { 37.430, 127.12, 0.07, 0, 0, 0, 0, 0 }; //20 x,x,alt /global_position
            List<double> test3 = new List<double> { 0, 0, 0, 0.56, 0.1, 0.04, 0, 0 }; //21 ground, air, xspeed
            List<double> test4 = new List<double> { 2369.1, 0, 0, 0, 0, 0, 0, 0 };//50 Oil temperature x
            List<double> test5 = new List<double> { 0.5 };//54 battery volatage batterylevel
            List<double> test6 = new List<double> { 0.11 };//53 batterary amperage current
            List<double> test7 = new List<double> { 297.45 };//37 engine rpm x
            ArrayList testArray = new ArrayList() { test1, test2, test3, test4, test5, test6, test7 };
            int[] testIndex = new int[] { 17, 20, 21, 50, 54, 53, 37 };

            //Xserver에  키, Value 등록
            for (int i = 0; i < 7; ++i)
            {
                Xserver.Add(testIndex[i], new List<double>());
                foreach (double j in (List<double>)testArray[i])
                {
                    Xserver[testIndex[i]].Add(j);

                }
            }


            /* foreach (double i in test)
             {
                 Xserver[21].Add(i);
             }*/
            while (true)
            {
                //test data


                Console.WriteLine("\n\n---------------------시작------------------------------");
                foreach (List<double> data in Xserver.Values)
                {
                    for (int j = 0; j < data.Count; j++) // 4byte씩 8개 총 32개
                    {

                        Console.Write("  " + data[j] + " ");

                    }
                    Console.WriteLine("\n\n");
                }
                Console.WriteLine("\n");

                Console.WriteLine("---------------------끝------------------------------\n\n");
                System.Threading.Thread.Sleep(1000);
            }
        }
        //------------------------winForm------------------------
        private void ProtocolBox_SelectedIndexChanged(object sender, EventArgs e) //ComboBox에서 프로토콜을 선택시 발생하는 이벤트
        {
            protocolKind = ProtocolBox.SelectedItem.ToString();
        }
        
        
        private void Start_B_Click(object sender, EventArgs e) //SCM 작동 시작
        {


            //byte[] recivePacket = new byte[255]; // 수신받은 패킷을 저장하는 배열
            reciveT = new Thread(recive);
            reciveT.Start(); // VSM으로 부터 데이터 수신 시작
            StartSCM = new Thread(SCM_Start);
            StartSCM.Start();
            Start_B.Visible = false;
            End_B.Visible = true;
            ProtocolBox.Enabled = false;

        }
        private void End_B_Click(object sender, EventArgs e) //SCM 작동 종료
        {
            Start_B.Visible = true;
            End_B.Visible = false;
            ProtocolBox.Enabled = true;
            reciveT.Abort();
            StartSCM.Abort();
            client.Close();
        }
        private void SCM_Start()
        {
            int cnt = 0;
            while (true)
            {
                if (cnt > 4)
                {
                    cnt = 0;
                }
                if (protocolKind.Equals("STANAG 4586"))
                { //id = 1
                    Console.WriteLine("a");
                    protocolKind = ProtocolBox.SelectedItem.ToString();
                }
                else if (protocolKind.Equals("MAVLink"))
                {
                    //id 2
                    Console.WriteLine("Protocol Checker : " + protocolKind);
                    int[] msg = { 30, 33, 230, 147, 291 };//test 변수
                    int msgID = msg[cnt];


                    if (msgID == 30)
                    {
                        //MAVLink_Decode(reciveBuf);                       

                        uint[] Packet = Create_Packet(Create_Attitude(), 30);
                        Console.WriteLine("X-Plane 데이터(단위: degree)를 이용해 MAVLink 메시지(단위: radian)를 생성 및 패킷 생성 ");
                        Console.WriteLine(" magic  len incompat  compat  seq  sysid  comid  msgid  roll      pitch     yaw     checksum");

                        for (int i = 0; i < Packet.Length; ++i)
                        {
                            if (i > 7 && i < Packet.Length)
                            {
                                Console.Write(" " + UintToDouble(Packet[i]) + " ");
                            }
                            else
                            {
                                Console.Write("   " + Packet[i] + "  ");
                            }

                        }
                        Console.WriteLine("\n");
                        sendPacket = MAVLink_Encode(Packet);
                        send(sendPacket);
                    }

                    if (msgID == 33)
                    {
                        uint[] Packet = Create_Packet(Create_GlobalPositionInt(), 33);
                        Console.WriteLine("X-Plane 데이터(단위: degree)를 이용해 MAVLink 메시지(단위: radian)를 생성 및 패킷 생성 ");
                        Console.WriteLine(" magic  len incompat  compat  seq  sysid  comid  msgid  lat      long    alt    checksum");
                        for (int i = 0; i < Packet.Length; ++i)
                        {
                            if (i > 7 && i < Packet.Length)
                            {
                                Console.Write("  " + UintToDouble(Packet[i]) + " ");
                                continue;
                            }
                            Console.Write("   " + Packet[i] + "  ");
                        }
                        Console.WriteLine("\n");
                        sendPacket = MAVLink_Encode(Packet); // 이걸 보내면 끝!!
                        send(sendPacket);
                    }

                    if (msgID == 230)//임시
                    {
                        uint[] Packet = Create_Packet(Create_DronSpeed(), 230);
                        Console.WriteLine("X-Plane 데이터(단위: degree)를 이용해 MAVLink 메시지(단위: radian)를 생성 및 패킷 생성 ");
                        Console.WriteLine(" magic  len incompat  compat  seq  sysid  comid  msgid    vX      vY       vZ      checksum");
                        for (int i = 0; i < Packet.Length; ++i)
                        {
                            if (i > 7 && i < Packet.Length)
                            {
                                Console.Write("  " + UintToDouble(Packet[i]) + " ");
                                continue;
                            }
                            Console.Write("   " + Packet[i] + "  ");
                        }
                        Console.WriteLine("\n");
                        sendPacket = MAVLink_Encode(Packet); // 이걸 보내면 끝!!
                        send(sendPacket);
                    }

                    if (msgID == 147)
                    {
                        uint[] Packet = Create_Packet(Create_BatteryStatus(), 147);
                        Console.WriteLine("X-Plane 데이터(단위: degree)를 이용해 MAVLink 메시지(단위: radian)를 생성 및 패킷 생성 ");
                        Console.WriteLine(" magic  len incompat  compat  seq  sysid  comid  msgid  temp      volt      battery   checksum");
                        for (int i = 0; i < Packet.Length; ++i)
                        {
                            if (i > 7 && i < Packet.Length)
                            {
                                Console.Write("  " + UintToDouble(Packet[i]) + " ");
                                continue;
                            }
                            Console.Write("   " + Packet[i] + "  ");
                        }
                        Console.WriteLine("\n");
                        sendPacket = MAVLink_Encode(Packet); // 이걸 보내면 끝!!
                        send(sendPacket);
                    }

                    if (msgID == 291)
                    {
                        uint[] Packet = Create_Packet(Create_EscStatus(), 291);
                        Console.WriteLine("X-Plane 데이터(단위: degree)를 이용해 MAVLink 메시지(단위: radian)를 생성 및 패킷 생성 ");
                        Console.WriteLine(" magic  len incompat  compat  seq  sysid  comid  msgid     rpm     checksum");
                        for (int i = 0; i < Packet.Length; ++i)
                        {
                            if (i > 7 && i < Packet.Length)
                            {
                                Console.Write("  " + UintToDouble(Packet[i]) + " ");
                                continue;
                            }
                            Console.Write("   " + Packet[i] + "  ");
                        }
                        Console.WriteLine("\n");
                        sendPacket = MAVLink_Encode(Packet); // 이걸 보내면 끝!!
                        send(sendPacket);
                    }

                    cnt++;


                    /*  uint[] recivBuf = MAVLink_Decode(sendBuf);
                      uint[] test = new uint[1];
                      test[0]= recivBuf[8];
                      Console.WriteLine(UintArrayToFloat(test) ); */
                    //VSM으로 부터 받은 수신 패킷을 저장 하고  수신받은 패킷의 정보를 기반으로 명령을 수행 또는 응답메시지를 매핑하여 VSM단으로 송신한다.
                    //작동을 시작하면 VSM과 송수신을 시작하게되고 패킷을 디코딩
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("protocol 선택 확인 요망");
                }
            }

        }

        //----------------------------DataLink---------------------------
        private static void recive() // VSM으로 부터 패킷을 수신
        {


            // Console.WriteLine("recv");
            if (client != null)
            {
                Debug.WriteLine("이미 UDP 소켓이 생성되어있음..");
            }

            client = new UdpClient();
            endPoint = new IPEndPoint(IPAddress.Any, UpLinkPort);

            client.Client.Bind(endPoint); //특정 IP에게 수신시 bind 사용

            while (true)
            {
                if (client == null) { return; }

                try
                {

                    reciveBuf = client.Receive(ref endPoint);

                    Console.WriteLine("Receive\n");

                    foreach (byte data in reciveBuf)
                    {
                        Console.WriteLine("-----------" + data + "-----------------");
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

        }

        private void send(byte[] buf) // VSM으로 패킷을 전송
        {

            //Console.WriteLine("send\n");
            try
            {
                using (UdpClient Server = new UdpClient())
                {

                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.7"), DownLinkPort); //현재 컴퓨터 ip 작성
                    Server.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    Server.Send(buf, buf.Length, endPoint);


                    Console.WriteLine("VSM으로 send 성공\n");
                    Console.WriteLine("\n------------------------------------------------------------------------------\n");
                    Server.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }


        }

       

        /*private byte[] XpalentoMAVLink(Dictionary<int, List<Double>> serverData, byte[] revData)  //X-Plane에서 MAVLink로 변환 <PAYLOAD를 생성하는 부분>
        {
            byte[] TransBuf = new byte[255]; //메시지 변환후 byte 값을 저장
            if (revData[7] == 0) // MAVLink HeartBeat
            {            
                mavlink_heartbeat_t heartbeat = new mavlink_heartbeat_t();
                heartbeat.type = (uint)MAV_TYPE.MAV_TYPE_QUADROTOR;
                heartbeat.autopilot = (uint)MAV_AUTOPILOT.MAV_AUTOPILOT_INVALID;
                heartbeat.base_mode = (uint)MAV_MODE_FLAG.MAV_MODE_FLAG_MANUAL_INPUT_ENABLED;

                if (serverData.ContainsKey(35) && (double)serverData[35][0] > 0.0)
                {
                    heartbeat.system_status = (uint)MAV_STATE.MAV_STATE_ACTIVE;
                    
                    
                }
                else if(serverData.ContainsKey(35) && (double)serverData[35][0] <= 0.0)
                {
                    heartbeat.system_status = (uint)MAV_STATE.MAV_STATE_STANDBY;
                }
                 TransBuf = StructureToByteArray(heartbeat);
               
            }

            return TransBuf; 
        }*/

        /*public static byte[] StructureToByteArray<T>(T obj)
        {
            int len = Marshal.SizeOf(obj);
            byte[] arr = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }*/




        //--------------------------------XServer 부분--------------------------------
        public struct UdpState //UdpClient와 IPEndPoint 구조체 정의
        {
            public UdpClient UdpClient;
            public IPEndPoint IPEndPoint;
        }

        private void BeginReceiveStart() //Xserver 실행
        {
            System.Net.IPEndPoint e = new IPEndPoint(IPAddress.Any, listenPort); // 모든 IP 주소에서 수신할 수 있도록 IPEndPoint 인스턴스를 생성하고, 지정된 포트로 바인딩
            UdpClient u = new System.Net.Sockets.UdpClient(e); // 새로운 UdpClient 인스턴스를 생성하고 지정된 엔드포인트에 바인딩
            UdpState s = new UdpState() { UdpClient = u, IPEndPoint = e }; // 새로운 UdpState 인스턴스를 생성하고 UdpClient와 엔드포인트로 초기화

            u.BeginReceive(new AsyncCallback(ReceiveCallback), s);// 비동기적으로 데이터를 수신하기 시작하고, 수신된 데이터를 처리하기 위해 ReceiveCallback 메서드를 사용


            Console.ReadKey(true); // 콘솔 창을 닫기 전에 키 입력을 기다림
        }



        protected static void ReceiveCallback(IAsyncResult ar) //데이터를 수신후 Dictionary에 저장하는 메소드, 비동기 작업 결과를 매개변수로 넘겨야함
        {
            // Receive the datagram
            Console.WriteLine("\n------------------------------------------------------------------------------\n");
            Console.WriteLine("X-Data Server에서 데이터 저장 완료");
            Console.Write("UdpDatagramReceived START!");
            try
            {
                //// 비동기 작업 상태에서 UdpClient와 IPEndPoint 인스턴스를 가져옴
                UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).UdpClient;
                IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).IPEndPoint;

                Byte[] receiveBytes = u.EndReceive(ar, ref e);
                Xserver = new Dictionary<int, List<double>>();

                //xplaneData ex) 68 65 84 65 60 18 0 0 0 171 103 81 191 187 243 46 190 103 246 45 67 156 246 26 67 47 231 26 67 0 192 121 196 0 192 121 196 85 254 151 193
                //받오는 바이트는 총 41 byte를 받아옴 Head(5), msgType(4), Data(32)
                int cur = 5; // 맨 앞에 5자리는 D A T A * 이 후 4 byte는 메시지 번호이며, 나머지는 데이터이다.
                int itemCnt = (receiveBytes.Length - cur) / 36;
                for (int i = 0; i < itemCnt; i++)
                {
                    var dataIndex = BitConverter.ToInt32(receiveBytes, cur); //메시지 Index 변수이며, 수신받은 Byte를 cur의 위치부터 4Byte를를 읽어 INT32형으로 변환시킴
                    Xserver.Add(dataIndex, new List<double>()); // 메시지 번호를 key값에 저장 및 Value를 List<float> 생성
                    Console.Write("\n{0} => ", dataIndex);
                    cur += 4; // 데이터 값 하나당 4Byte이므로 4씩 증가
                    for (int j = 1; j < 9; j++) // 4byte씩 8개 총 32개
                    {
                        var dataFloat = BitConverter.ToSingle(receiveBytes, cur); // 실제 데이터 값 변수이며, 수신받은 Byte를 cur의 위치부터 4Byte를를 읽어 부동소수로 변환 
                        Xserver[dataIndex].Add(dataFloat); //데이터를 Dictionary의 Value 부분에 List 형식으로 추가
                        Console.Write("{0},", dataFloat);
                        cur += 4; // 다음 데이터를 추가하기 위해 4Byte 증가
                    }
                }

                // Begin receiving next datagram
                u.BeginReceive(new AsyncCallback(ReceiveCallback), ar.AsyncState);
            }
            catch (Exception ex) // 오류 발생시 실행할 코드
            {
                Console.Write(ex.ToString());
            }
            Console.WriteLine("\nUdpDatagramReceived End!\n");

            /*  foreach (int s in xplaneData.Keys) 
              {
                  Console.WriteLine(s);
              }*/
        }

        //----------------------------MAVLink------------------------------------

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct mavlink_heartbeat_t
        {
            public uint type;
            public uint autopilot;
            public uint base_mode;
            public uint custom_mode;
            public uint system_status;
            public uint mavlink_version;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Packet
        {
            public uint magic;
            public uint len;
            public uint incompat_flags;
            public uint compat_flags;
            public uint seq;
            public uint sysid;
            public uint compid;
            public UInt32 msgid;
            public uint[] payload;
            public UInt16 checksum;
            public uint[] signature;
        }

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

        private byte[] MAVLink_Encode(uint[] _buf)
        {
            byte[] buf = new byte[_buf.Length * sizeof(uint)];
            int cnt = 0;


            for (int i = 0; i < _buf.Length; ++i)
            {
                byte[] uintToByte = BitConverter.GetBytes(_buf[i]);
                for (int j = 0; j < 4; ++j)
                {
                    buf[cnt + j] = uintToByte[j];

                }

                cnt += 4;
            }

            return buf;
        }
        private uint[] MAVLink_Decode(byte[] _reciveBuf) // 2진수를 정수로 변환시킨 후 MAVLink Packet으로 저장
        {
            uint[] buf = new uint[_reciveBuf.Length / (sizeof(byte) * 4)];
            int cnt = 0;

            //여기부터 수정합시다
            for (int i = 0; i < _reciveBuf.Length / (sizeof(byte) * 4); ++i)
            {
                buf[i] = BitConverter.ToUInt32(_reciveBuf, cnt);
                cnt += 4;
            }

            return buf;
        }

        private void Create_HeartBeat()
        {

        }
        private uint[] Create_Attitude() //#30
        {
            uint[] buf = new uint[3];
            try
            {
                //rad = deg * 3.14/180 (deg to rad)
                double roll = (Xserver[17][1]) * (3.14 / 180);//(float)Xserver[17][1];
                double pitch = (Xserver[17][0]) * (3.14 / 180);//(float)Xserver[17][0];
                double yaw = (Xserver[17][2]) * (3.14 / 180);

                buf[0] = DoubleToUint(roll);
                buf[1] = DoubleToUint(pitch);
                buf[2] = DoubleToUint(yaw);
            }
            catch (Exception e1)
            {

            }


            return buf;
        }
        //임시 인덱스
        private uint[] Create_GlobalPositionInt() //33
        {
            uint[] buf = new uint[3];
            try
            {
                double lat = ((Xserver[20][0])) * Math.Pow(10, 7);
                double lon = ((Xserver[20][1])) * Math.Pow(10, 7);
                double alt = ((Xserver[20][2])) * 304.8; // ftmsl ->  mm 단위변환

                buf[0] = DoubleToUint(lat);
                buf[1] = DoubleToUint(lon);
                buf[2] = DoubleToUint(alt);
            }
            catch (Exception e)
            {

            }


            return buf;
        }

        private uint[] Create_DronSpeed() //99
        {
            uint[] buf = new uint[3];
            try
            {
                //m/s -> cm/s
                double vX = (Xserver[21][3]) * 100;
                double vY = (Xserver[21][4]) * 100;
                double vZ = (Xserver[21][5]) * 100;

                buf[0] = DoubleToUint(vX);
                buf[1] = DoubleToUint(vY);
                buf[2] = DoubleToUint(vZ);
            }
            catch (Exception e)
            {

            }


            return buf;
        }

        private uint[] Create_BatteryStatus() //147
        {
            uint[] buf = new uint[3];
            try
            {
                double temperature = (Xserver[50][0]) * (100 / 90); //deg -> cdegC
                double voltages = (Xserver[54][0]) * 1000; //V -.> mV
                double currentBattery = (Xserver[53][0]) * 100; //A -> cA

                buf[0] = DoubleToUint(temperature);
                buf[1] = DoubleToUint(voltages);
                buf[2] = DoubleToUint(currentBattery);
            }
            catch (Exception e)
            {

            }


            return buf;
        }
        private uint[] Create_EscStatus() //291
        {
            uint[] buf = new uint[1];
            try
            {
                double EngineRPM = ((Xserver[37][0]));

                buf[0] = DoubleToUint(EngineRPM);
            }
            catch (Exception e)
            {

            }


            return buf;
        }

        public static uint DoubleToUint(double value)
        {
            long longValue = BitConverter.DoubleToInt64Bits(value);
            uint uintValue = (uint)(longValue >> 32); // 상위 32비트를 uint로 변환
            return uintValue;
        }


        public static double UintToDouble(uint value)
        {
            long longValue = ((long)value) << 32;
            // uint 값을 long의 상위 32비트로 이동
            double doubleValue = BitConverter.Int64BitsToDouble(longValue);
            return doubleValue;
        }
        /*public static float UintArrayToFloat(uint[] uintArray) //임시
        {
            byte[] bytes = new byte[uintArray.Length / sizeof(uint)];
            Buffer.BlockCopy(uintArray, 0, bytes, 0, bytes.Length);
            return BitConverter.ToSingle(bytes, 0);
        }*/

        private uint[] Create_Packet(uint[] messageData, uint _msgid)
        {
            String[] setData = new string[4];
            uint[] resultBuf;

            uint magic; //0xFD
            uint len = (uint)messageData.Length;
            uint incompat_flags;
            uint compat_flags;
            uint seq;
            uint sysid;
            uint comid;
            uint msgid;
            uint[] payload;
            uint checksum;


            resultBuf = new uint[9 + len];

            if (File.Exists(directoryName)) //Uav 설정된 데이터를 가져옴
            {
                using (StreamReader reader = new StreamReader(directoryName))
                {
                    String Dbuf = reader.ReadLine();
                    setData = Dbuf.Split(',');
                }
            }

            // 패킷값 초기화 부분
            if (setData[0].Equals("MAVLink2"))
            {
                magic = 0xFD;
                resultBuf[0] = magic;

            }

            resultBuf[1] = len;

            if (setData[1].Equals("미사용"))
            {
                incompat_flags = 0x00;
                resultBuf[2] = incompat_flags;
            }

            if (setData[2].Equals("미사용"))
            {
                compat_flags = 0x00;
                resultBuf[3] = compat_flags;
            }

            seq = 0;
            resultBuf[4] = seq;

            sysid = uint.Parse(setData[3]);
            resultBuf[5] = sysid;

            comid = uint.Parse(setData[4]);
            resultBuf[6] = comid;

            msgid = _msgid;
            resultBuf[7] = msgid;

            payload = messageData;

            for (int i = 0; i < len; ++i)
            {
                resultBuf[8 + i] = payload[i];

            }

            checksum = 0;
            resultBuf[resultBuf.Length - 1] = checksum;


            return resultBuf;
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            Setting set = new Setting();
            set.Show();
        }
    }
}