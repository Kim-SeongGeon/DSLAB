using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace xplane_data_test
{
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
    class MAVLink
    {
        private Packet revPacket; // 받은 패킷
        private Packet sendPacket; //보낼 패킷
        mavlink_heartbeat_t heartbeat = new mavlink_heartbeat_t();
        /* private static uint magic = 0xFD;
         private static uint len = 0;
         private static uint incompat_flags = 0x00; //비호환성 플래그 비활성화
         private static uint compat_flags = 0x00;
         private static uint seq = 0;
         private static uint sysid = 1; // 1~255
         private static uint compid = 1; // 1~255, FC = 1, CAM =2
         private static UInt32 msgid;
         private static uint[]payload = new uint[len];
         private static UInt16 checksum = 0; //일단 미사용*/
        //private static uint[]signature = new uint[13];
        //msg
        public MAVLink()
        {
            //test용 _ GCS에서 #1을 요청해서 VSM에서 Heartbeat를 수신받음
            test_packet();
        }
        private void test_packet() // 수신받은 PACK Decoder
        {
            revPacket.magic = 0xFD;
            revPacket.len = 9;
            revPacket.incompat_flags = 0x00;
            revPacket.compat_flags = 0x00;
            revPacket.seq = 0;
            revPacket.sysid = 255; //GCS
            revPacket.compid = 1;
            revPacket.msgid = 0; //heartbeat
            
            revPacket.checksum = 0;
        }

        public void encoder(byte[] payload) 
        {
            sendPacket.magic = 0xFD;
            sendPacket.len = sizeof(int);
            sendPacket.incompat_flags = 0x00;
            sendPacket.compat_flags = 0x00;
            sendPacket.seq = 0;
            sendPacket.sysid = 2;
            sendPacket.msgid = 0;
            //sendPacket.payload;
          
        }
        public void decoder()
        {

        }
       
      

        private void HEARTBEAT(uint ytpe, uint autopilot, uint base_mode, uint custom_mode, uint system_status, uint mavlink_version) //(#0)
        {
            
            uint[] buf = new uint[6];
            uint type = (uint)MAV_TYPE.MAV_TYPE_QUADROTOR; 
            /*uint autopilot = (uint)MAV_AUTOPILOT.MAV_AUTOPILOT_INVALID;
            uint base_mode = (uint)MAV_MODE_FLAG.MAV_MODE_FLAG_MANUAL_INPUT_ENABLED;
            uint custom_mode = 0;
            uint system_status = (uint)MAV_STATE.;
            uint mavlink_version;*/

            
        }
        private void encode_heartbeat()
        {
            uint []buffer = new uint[255];
            //UInt16 len
        }
        private void ATTITUDE() //#30
        {
            UInt32 time_boot_ms;
            float roll = 0;
            float pitch = 0;
            float yaw = 0;
            float rollspeed = 0;
            float pitchspeed = 0;
            float yawspeed = 0;
        }

        private void GLOBAL_POSITION_INT() //#33
        {
            UInt32 time_boot_ms = 0;
            Int32 lat = 0;
            Int32 lon = 0;
            Int32 alt = 0;
            Int32 relative_alt = 0;
            Int16 VX = 0;
            Int16 VY = 0;
            Int16 VZ = 0;
            Int16 hdg = 0;
        }

        private void ALTITUDE() //#141
        {
            UInt64 TIME_USEC = 0;
            float altitude_monotonic = 0;
            float altitude_amsl = 0;
            float altitude_local = 0;
            float altitude_relative = 0;
            float altitude_terrain = 0;
            float altitude_clearance = 0;
        }

    }
}
