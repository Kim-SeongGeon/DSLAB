using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;

namespace VSM
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct sPacket
    {
        public UInt16 sourcePort;//UDP
        public UInt16 destinationPor;//UDP  
        public UInt16 messageLength;
        public  UInt16 sourceID; //출발지 ID
        public UInt16 destinationID; //목적지 ID
        public UInt16 messagID;
        public UInt16 messageType; //push 0, pull 1
        public UInt16 messageProperties;  // ACK, Version32, Cecksum0 총 15bitmap
        public UInt16[]data;
        public UInt16 optionalChecksum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CUCSAuthorisationResponse //#1
    {
    
        public UInt32 PresenceVector;
        public UInt64 TimeStamp;
        public Int32 VSM_ID;
        public Int32 DataLinkID;
        public UInt16 VehicleType;
        public UInt16 VehicleSubtype;
        public uint RequestedHandoverLoI;
        public uint RequestedHandoverAccess;
        public uint RequestedFlightMode;
        public UInt16 ControlledStation16;
        public UInt16 ComponentNumber;
        public UInt16 SubComponentNumber;
        public uint PayloadType;
        public uint AssetMode;
        public uint WVDLTCM;
        public uint CUCSType;
        public UInt16 CUCSSubtype;
        public uint PresenceVectorSupport;
        public UInt16 ControlledStation32;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VSMAuthorisationResponse //#2
    {
        public UInt32 PresenceVector;
        public UInt64 TimeStamp;
        public Int32 VSM_ID;
        public Int32 DataLinkID;
        public Byte AccessAuthorized;
        public Byte AccessGranted;
        public Byte LOI_Authorized;
        public Byte LOI_Granted;
        public Byte FlightModesGranted;
    }

    

    class DLI
    {   // 1 - 8, 2 - 16 ,3 - 32, 4 - 32, 5 - 64
        //push 주기적, pull 응답
      
        public DLI() // STANAG4586 메시지에 대한 정의
        {
           
            
        }

        public static void encoder()
        {

        }
        public void decoder()
        {

        }

      


















        //일단 보류
        public UInt16 Wrapper(UInt16 _SourcePort, UInt16 _DestinationPort, UInt16 _PacketLength, UInt16 _UDPchecksum, UInt16 _Reserved,
        UInt16 _MessageLength, UInt16 _SourceID, UInt16 _DestinationID, UInt16 _MessageType, UInt16 _MessageProperties, UInt16 _Data)
        {
            UInt16 sourcePort = _SourcePort;//UDP
            UInt16 destinationPort = _DestinationPort;//UDP
            UInt16 packetLength = _PacketLength;//UDP
            UInt16 UDPchecksum = _UDPchecksum;//UDP
            UInt16 reserved = _Reserved; // 모르겠음 0
            UInt16 messageLength = _MessageLength;
            UInt16 sourceID = _SourceID; //출발지 ID
            UInt16 destinationID = _DestinationID; //목적지 ID
            UInt16 messageType = _MessageType; //push 0, pull 1
            UInt16 messageProperties = MessageProperties();
            UInt16 data = _Data;
            UInt16 optionalChecksum = OptionalChecksum();
            return 0; //Hex Format
        }

        public UInt16 MessageProperties()
        {
            return 0;
        }

        public UInt16 OptionalChecksum()
        {

            return 0;
        }

        class VSMAuthorisationResponse //#2 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_ID;
            Int32 DataLinkID;
            Byte AccessAuthorized;
            Byte AccessGranted;
            Byte LOI_Authorized;
            Byte LOI_Granted;
            Byte FlightModesGranted;



            public VSMAuthorisationResponse()
            {
                FieldAdd();
                response();
            }
            public void response() // 장치 검색 
            {
                PresenceVector = 0XFFFFFF;
                TimeStamp = (UInt64)(((DateTimeOffset)dt).ToUnixTimeSeconds());
                VSM_ID = 10;
                DataLinkID = 0;
                AccessAuthorized = 0;
                AccessGranted = 0;
                LOI_Authorized = 0;
                LOI_Granted = 0;
                FlightModesGranted = 0;
                field.Clear();
                FieldAdd();
            }

            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("0002.00", PresenceVector);
                field.Add("00021.01", TimeStamp);
                field.Add("00021.04", VSM_ID);
                field.Add("00021.05", DataLinkID);
                field.Add("00021.13", AccessAuthorized);
                field.Add("00021.09", AccessGranted);
                field.Add("00021.06", LOI_Authorized);
                field.Add("00021.07", LOI_Granted);
                field.Add("00021.16", FlightModesGranted);
            }


        }

        class VehicleID //#3 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_ID;
            Int32 VehicleID_Update;
            UInt16 VehicleType;
            UInt16 VehicleSubtype;
            Byte OwningID;
            String TailNumber;
            String MissionID;
            String ATC_CallSign; // 필요없음
            UInt16 ConfigurationChecksum;


            public VehicleID()
            {
                FieldAdd();
            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("0003.00", PresenceVector);
                field.Add("0020.01", TimeStamp);
                field.Add("0020.04", VSM_ID);
                field.Add("0020.05", VehicleID_Update);
                field.Add("0020.06", VehicleType);
                field.Add("0020.07", VehicleSubtype);
                field.Add("0020.08", OwningID);
                field.Add("0020.09", TailNumber);
                field.Add("0020.10", MissionID);
                field.Add("0020.11", ATC_CallSign);
                field.Add("0020.12", ConfigurationChecksum);
            }
        }

        class VehicleConfigurationCommand //#2000 필요없음
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte EnergyStorageUnit;
            UInt16 InitialPropulsionEnergy;

            public VehicleConfigurationCommand()
            {
                
            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("2000.00", PresenceVector);
                field.Add("0040.01", TimeStamp);
                field.Add("2000.01", EnergyStorageUnit);
                field.Add("0040.04", InitialPropulsionEnergy);
            }

        }

        class VehicleConfiguration //#3000 pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            UInt64 ConfigurationID;
            float PropulsionFuelCapacity;
            float PropulsionBatteryCapacity;
            UInt16 MaximumIndicatedAirspeed;
            UInt16 OptimumCruiseIndicatedAirspeed;
            UInt16 OptimumEnduranceIndicatedAirspeed;
            Byte MaximumLoadFactor;
            float GrossWeight;
            float X_CG;
            Byte NumberOfEngines;
            Byte FuelDensity;
            public VehicleConfiguration()
            {
                FieldAdd();
            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3000.00", PresenceVector);
                field.Add("0100.01", TimeStamp);
                field.Add("0100.05", ConfigurationID);
                field.Add("0100.06", PropulsionFuelCapacity);
                field.Add("0100.07", PropulsionBatteryCapacity);
                field.Add("0100.08", MaximumIndicatedAirspeed);
                field.Add("0100.09", OptimumCruiseIndicatedAirspeed);
                field.Add("0100.10", OptimumEnduranceIndicatedAirspeed);
                field.Add("0100.11", MaximumLoadFactor);
                field.Add("0100.12", GrossWeight);
                field.Add("0100.13", X_CG);
                field.Add("0100.14", NumberOfEngines);
                field.Add("3000.01", FuelDensity);
            }
        }

        class VehicleOperationModeReport //#3001 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte SelectFlightPathControlMode;
            public VehicleOperationModeReport()
            {
                
            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3001.00", PresenceVector);
                field.Add("0106.01", TimeStamp);
                field.Add("0106.04", SelectFlightPathControlMode);
            }
        }

        class VehicleOperatingStates //#3002 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 CommandedAltitude;
            Byte AltitudeType;
            Int16 CommandedHeading;
            Int16 CommandedCourse;
            Int16 CommandedTurnRate;
            Int16 CommandedRollRate;
            UInt16 CommandedSpeed;
            Byte SpeedType;
            SByte PowerLevel;
            UInt16 BingoEnergy;
            UInt16 CurrentPropulsionEnergyLevel;
            UInt16 CurrentPropulsionEnergyUsageRate;
            Int16 CommandedRoll;
            Byte AltitudeCommandType;
            Byte HeadingCommandType;
            Byte UAState;
            Int16 ThrustDirection;
            Byte Thrust;
            Byte Loiter_WaypointValidity;
            Int32 CommandedLoiterPositionLatitude;
            Int32 CommandedLoiterPositionLongitude;
            Byte AltitudeChangeBehaviour;
            UInt16 CommandedWaypointNumber;

            public VehicleOperatingStates()
            {
                

            }

            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3002.00", PresenceVector);
                field.Add("0104.01", TimeStamp);
                field.Add("0104.04", CommandedAltitude);
                field.Add("0104.05", AltitudeType);
                field.Add("0104.06", CommandedHeading);
                field.Add("0104.07", CommandedCourse);
                field.Add("0104.08", CommandedTurnRate);
                field.Add("0104,09", CommandedRollRate);
                field.Add("0104.10", CommandedSpeed);
                field.Add("0104.11", SpeedType);
                field.Add("0104.12", PowerLevel);
                field.Add("0104.21", BingoEnergy);
                field.Add("0104.16", CurrentPropulsionEnergyLevel);
                field.Add("0104.17", CurrentPropulsionEnergyUsageRate);
                field.Add("0104.18", CommandedRoll);
                field.Add("0104.19", AltitudeCommandType);
                field.Add("0104.20", HeadingCommandType);
                field.Add("3002.01", UAState);
                field.Add("3002.02", ThrustDirection);
                field.Add("3002.03", Thrust);
                field.Add("3002.04", Loiter_WaypointValidity);
                field.Add("3002.05", CommandedLoiterPositionLatitude);
                field.Add("3002.06", CommandedLoiterPositionLongitude);
                field.Add("3004.02", AltitudeChangeBehaviour);
                field.Add("3002.07", CommandedWaypointNumber);
            }
        }

        class LoiterConfigurationReport //#3004 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;
            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Byte LoiterType;
            UInt16 LoiterRadius;
            UInt16 LoiterLength;
            Byte LoiterLengthUnits;
            Int16 LoiterBearing;
            Byte LoiterDirection;
            Int32 LoiterAltitude;
            Byte AltitudeType;
            UInt16 LoiterSpeed;
            Byte SpeedType;
            Byte FlyingBehaviour;
            UInt16 LoiterDuration;
            Byte LoiterDurationUnits;

            public LoiterConfigurationReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3004.00", PresenceVector);
                field.Add("3004.06", TimeStamp);
                field.Add("3004.07", LoiterType);
                field.Add("3004.08", LoiterRadius);
                field.Add("3004.09", LoiterLength);
                field.Add("3004.03", LoiterLengthUnits);
                field.Add("3004.10", LoiterBearing);
                field.Add("3004.11", LoiterDirection);
                field.Add("3004.12", LoiterAltitude);
                field.Add("3004.13", AltitudeType);
                field.Add("3004.14", LoiterSpeed);
                field.Add("3004.15", SpeedType);
                field.Add("3004.01", FlyingBehaviour);
                field.Add("3004.04", LoiterDuration);
                field.Add("3004.05", LoiterDurationUnits);
            }
        }

        class FlightTerminationModeReport //#3005 push/pull 비행종료응답
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte ReportedFlightTerminationState;
            Byte ReportedFlightTerminationMode;

            public FlightTerminationModeReport()
            {

            }

            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3005.00", PresenceVector);
                field.Add("0108.01", TimeStamp);
                field.Add("0108.04", ReportedFlightTerminationState);
                field.Add("0108.04", ReportedFlightTerminationMode);
            }
        }

        class EngineOperationStates //#3007 push/pull 엔진작동상태
        {
            Dictionary<String, Object> field = new Dictionary<String, Object>();
            DateTime dt = DateTime.Now;

            UInt64 PresenceVector;
            UInt64 TimeStamp;
            UInt16 EngineNumber;
            Byte EngineStatus;
            Byte ReportedEngineCommand;
            Byte ReverseThrustPowerStatus;
            Byte ReportedReversThrust;
            Byte IgnitionSwitchPowerStatus;
            Byte IgnitionSwitchActivation;
            UInt16 EnginePowerSetting;
            Byte EngineSpeed1Type;
            UInt16 EngineSpeed1;
            Byte EngineSpeed2Type;
            UInt16 EngineSpeed2;
            Byte EngineSpeed3Type;
            UInt16 EngineSpeed3;
            SByte PropellerPitchAngle;
            Byte OutputPowerStatus;
            Byte EngineTemperature1Type;
            UInt16 EngineTemperature1;
            Byte EngineTemperature2Type;
            UInt16 EngineTemperature2;
            Byte EngineTemperature3Type;
            UInt16 EngineTemperature3;
            Byte EngineTemperature4Type;
            UInt16 EngineTemperature4;
            Byte EnginePressure1Type;
            UInt16 EnginePressure1;
            Byte EnginePressure1Status;
            Byte EnginePressure2Type;
            UInt16 EnginePressure2;
            Byte EnginePressure2Status;
            Byte FireDetectionSensor;
            UInt32 EngineEnergyFlow;
            Byte EngineBodyTemperatureStatus;
            Byte ExhaustGasTemperatureStatus;
            Byte CoolantTemperatureStatus;
            Byte LubricantTemperatureStatus;
            public EngineOperationStates()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3007.00", PresenceVector);
                field.Add("0105.01", TimeStamp);
                field.Add("0105.04", EngineNumber);
                field.Add("0105.05", EngineStatus);
                field.Add("0105.06", ReportedEngineCommand);
                field.Add("3007.01", ReverseThrustPowerStatus);
                field.Add("3007.02", ReportedReversThrust);
                field.Add("3007.03", IgnitionSwitchPowerStatus);
                field.Add("3007.04", IgnitionSwitchActivation);
                field.Add("0105.07", EnginePowerSetting);
                field.Add("3007.12", EngineSpeed1Type);
                field.Add("0105.08", EngineSpeed1);
                field.Add("3007.13", EngineSpeed2Type);
                field.Add("3007.14", EngineSpeed2);
                field.Add("3007.15", EngineSpeed3Type);
                field.Add("3007.16", EngineSpeed3);
                field.Add("3007.25", PropellerPitchAngle);
                field.Add("0105.10", OutputPowerStatus);
                field.Add("3007.18", EngineTemperature1Type);
                field.Add("1005.11", EngineTemperature1);
                field.Add("3007.19", EngineTemperature2Type);
                field.Add("3007.20", EngineTemperature2);
                field.Add("3007.21", EngineTemperature3Type);
                field.Add("3007.22", EngineTemperature3);
                field.Add("3007.23", EngineTemperature4Type);
                field.Add("3007.24", EngineTemperature4);
                field.Add("3007.05", EnginePressure1Type);
                field.Add("3007.06", EnginePressure1);
                field.Add("3007.07", EnginePressure1Status);
                field.Add("3007.08", EnginePressure2Type);
                field.Add("3007.09", EnginePressure2);
                field.Add("3007.10", EnginePressure2Status);
                field.Add("0105.16", FireDetectionSensor);
                field.Add("3007.11", EngineEnergyFlow);
                field.Add("0105.11", EngineBodyTemperatureStatus);
                field.Add("0105.12", ExhaustGasTemperatureStatus);
                field.Add("0105.13", CoolantTemperatureStatus);
                field.Add("0105.15", LubricantTemperatureStatus);
            }

        }

        class AirAndGroundRelativeStates //#3009 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;
            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Int16 AngleOfAttack;
            Int16 AngleOfSideslip;
            UInt16 TrueAirspeed;
            UInt16 IndicatedAirspeed;
            UInt16 OutsideAirTemp;
            Int16 UWind;
            Int16 VWind;
            UInt16 AltimeterSetting;
            Int32 BarometricAltitude;
            Int16 BarometricAltitudeRate;
            Int32 PressureAltitude;
            Int32 AGLAltitude;
            Int32 WGS84Altitude;
            Int16 UGround;
            Int16 VGround;
            public AirAndGroundRelativeStates()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3009.00", PresenceVector);
                field.Add("0102.01", TimeStamp);
                field.Add("0102.04", AngleOfAttack);
                field.Add("0102.05", AngleOfSideslip);
                field.Add("0102.06", TrueAirspeed);
                field.Add("0102.07", IndicatedAirspeed);
                field.Add("0102.08", OutsideAirTemp);
                field.Add("0102.09", UWind);
                field.Add("0102.10", VWind);
                field.Add("0102.11", AltimeterSetting);
                field.Add("0102.12", BarometricAltitude);
                field.Add("0102.13", BarometricAltitudeRate);
                field.Add("0102.14", PressureAltitude);
                field.Add("0102.15", AGLAltitude);
                field.Add("0102.16", WGS84Altitude);
                field.Add("0102.17", UGround);
                field.Add("0102.18", VGround);
            }
        }

        class BodyRelativeSensedStates //3010 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Int16 XBodyAccel;
            Int16 YBodyAccel;
            Int16 ZBodyAccel;
            Int16 RollRate;
            Int16 PitchRate;
            Int16 TawRate;

            public BodyRelativeSensedStates()
            {
                FieldAdd();
            }

            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3010.00", PresenceVector);
                field.Add("3010.01", TimeStamp);
                field.Add("3010.04", XBodyAccel);
                field.Add("3010.05", YBodyAccel);
                field.Add("3010.06", ZBodyAccel);
                field.Add("3010.07", RollRate);
                field.Add("3010.08", PitchRate);
                field.Add("3010.09", TawRate);
            }

        }

        class UA_StickStatus //#3011 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            SByte LateralStick;
            SByte LongitudinalStick;
            SByte RotationalStick;
            Byte ThrottleStickEngine1;
            SByte PitchStickEngine1;
            Byte ThrottleStickEngine2;
            SByte PitchStickEngine2;
            Byte ThrottleStickEngine3;
            SByte PitchStickEngine3;
            Byte ThrottleStickEngine4;
            SByte PitchStickEngine4;

            public UA_StickStatus()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3011.00", PresenceVector);
                field.Add("3011.01", TimeStamp);
                field.Add("3011.02", LateralStick);
                field.Add("3011.03", LongitudinalStick);
                field.Add("3011.04", RotationalStick);
                field.Add("3011.05", ThrottleStickEngine1);
                field.Add("3011.06", PitchStickEngine1);
                field.Add("3011.07", ThrottleStickEngine2);
                field.Add("3011.08", PitchStickEngine2);
                field.Add("3011.09", ThrottleStickEngine3);
                field.Add("3011.10", PitchStickEngine3);
                field.Add("3011.11", ThrottleStickEngine4);
                field.Add("3011.12", PitchStickEngine4);
            }
        }

        class RelativeRoute_Waypoint_LocationReport //#3016 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Byte RouteSlaveToLocationIdentifier;
            Int32 LocationLatitudeYaxisZero;
            Int32 LocationLatitudeXaxisZero;
            Byte LocationAltitudeType;
            Int32 LocationAltitude;
            Int16 Orientation;
            String RouteID;
            UInt32 SlaveToLocationOffset;
            Byte UARelativeAltitudeType;
            Int32 UARelativeAltitude;
            Int16 SlaveToLocationLookingAngle;

            public RelativeRoute_Waypoint_LocationReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("3016.00", PresenceVector);
                field.Add("3016.01", TimeStamp);
                field.Add("3016.02", RouteSlaveToLocationIdentifier);
                field.Add("3016.03", LocationLatitudeYaxisZero);
                field.Add("3016.04", LocationLatitudeXaxisZero);
                field.Add("3016.05", LocationAltitudeType);
                field.Add("3016.06", LocationAltitude);
                field.Add("3016.07", Orientation);
                field.Add("3016.08", RouteID);
                field.Add("3016.09", SlaveToLocationOffset);
                field.Add("3016.10", UARelativeAltitudeType);
                field.Add("3016.11", UARelativeAltitude);
                field.Add("3016.12", SlaveToLocationLookingAngle);
            }
        }

        class InertialStates //#4000 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 Latitude;
            Int32 Longitude;
            Int32 Altitude;
            Byte AltitudeType;
            Int16 USpeed;
            Int16 VSpeed;
            Int16 WSpeed;
            Int16 UAccel;
            Int16 VAccel;
            Int16 WAccel;
            Int16 Roll;
            Int16 Pitch;
            Int16 Heading;
            Int16 RollRate;
            Int16 PitchRate;
            Int16 TurnRate;
            Int16 MagneticVariation;

            public InertialStates()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("4000.00", PresenceVector);
                field.Add("0101.01", TimeStamp);
                field.Add("0101.04", Latitude);
                field.Add("0101.05", Longitude);
                field.Add("0101.06", Altitude);
                field.Add("0101.07", AltitudeType);
                field.Add("0101.08", USpeed);
                field.Add("0101.09", VSpeed);
                field.Add("0101.10", WSpeed);
                field.Add("0101.11", UAccel);
                field.Add("0101.12", VAccel);
                field.Add("0101.13", WAccel);
                field.Add("0101.14", Roll);
                field.Add("0101.15", Pitch);
                field.Add("0101.16", Heading);
                field.Add("0101.17", RollRate);
                field.Add("0101.18", PitchRate);
                field.Add("0101.19", TurnRate);
                field.Add("0101.20", MagneticVariation);
            }
        }

        class FromToNextWaypointStates //#4001 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Byte AltitudeType;
            Byte SpeedType;
            Int32 FromWaypointLatitude;
            Int32 FromWaypointLongitude;
            Int32 FromWaypointAltitude;
            UInt64 FromWaypointTime;
            UInt16 FromWaypointNumber;
            Int32 ToWaypointLatitude;
            Int32 ToWaypointLongitude;
            Int32 ToWaypointAltitude;
            UInt16 ToWaypointSpeed;
            UInt64 ToWaypointTime;
            UInt16 ToWaypointNumber;
            Int32 NextWaypointLatitude;
            Int32 NextWaypointLongitude;
            Int32 NextWaypointAltitude;
            Int32 NextWaypointSpeed;
            UInt64 NextWaypointTime;
            UInt16 NextWaypointNumber;
            Byte LoiterConfigurationReportValidity; //To Waypoint
            public FromToNextWaypointStates()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("4001.00", PresenceVector);
                field.Add("0110.01", TimeStamp);
                field.Add("0110.04", AltitudeType);
                field.Add("0110.05", SpeedType);
                field.Add("0110.06", FromWaypointLatitude);
                field.Add("0110.07", FromWaypointLongitude);
                field.Add("0110.08", FromWaypointAltitude);
                field.Add("0110.09", FromWaypointTime);
                field.Add("0110.10", FromWaypointNumber);
                field.Add("0110.11", ToWaypointLatitude);
                field.Add("0110.12", ToWaypointLongitude);
                field.Add("0110.13", ToWaypointAltitude);
                field.Add("0110.14", ToWaypointSpeed);
                field.Add("0110.15", ToWaypointTime);
                field.Add("0110.16", ToWaypointNumber);
                field.Add("0110.17", NextWaypointLatitude);
                field.Add("0110.18", NextWaypointLongitude);
                field.Add("0110.19", NextWaypointAltitude);
                field.Add("0110.20", NextWaypointSpeed);
                field.Add("0110.21", NextWaypointTime);
                field.Add("0110.22", NextWaypointNumber);
                field.Add("0110.23", LoiterConfigurationReportValidity);
            }
        }


        class ElectricalReport //#12001 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte BusID;
            Byte BusVoltage;
            UInt16 BusCurrent;
            UInt32 BatteryTimeRemaining;

            public ElectricalReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("12001.00", PresenceVector);
                field.Add("12001.01", TimeStamp);
                field.Add("12001.02", BusID);
                field.Add("12001.03", BusVoltage);
                field.Add("12001.04", BusCurrent);
                field.Add("12001.05", BatteryTimeRemaining);
            }
        }

        class BatteryReport //#12101 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte BankSelected;
            Byte ConnectionStatus;
            UInt16 BusVoltage;
            Int16 BatteryCurrent;
            UInt16 BatteryChargeRemaining;
            UInt16 BatteryTemperature;

            public BatteryReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("12101.00", PresenceVector);
                field.Add("12101.01", TimeStamp);
                field.Add("12101.02", BankSelected);
                field.Add("12101.03", ConnectionStatus);
                field.Add("12101.04", BusVoltage);
                field.Add("12101.05", BatteryCurrent);
                field.Add("12101.06", BatteryChargeRemaining);
                field.Add("12101.07", BatteryTemperature);
            }
        }

        class MissionTransferCommand //#13000 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            String MissionID;
            Byte MissionPlanMode;
            UInt16 WaypointNumber;
            String RouteID;
            UInt32 ActitvityID;

            public MissionTransferCommand()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("13000.00", PresenceVector);
                field.Add("0800.01", TimeStamp);
                field.Add("0800.04", MissionID);
                field.Add("0800.05", MissionPlanMode);
                field.Add("0800.06", WaypointNumber);
                field.Add("13000.01", RouteID);
                field.Add("13000.02", ActitvityID);
            }
        }

        class UA_Route //#13001 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            UInt16 InitialWaypointNumber;
            String RouteID;
            Byte RouteType;
            UInt32 ActivityID;

            public UA_Route()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("13001.00", PresenceVector);
                field.Add("0801.01", TimeStamp);
                field.Add("0801.04", InitialWaypointNumber);
                field.Add("0801.05", RouteID);
                field.Add("0801.06", RouteType);
                field.Add("13001.01", ActivityID);
            }
        }

        class UA_PositionWaypoint //#13002 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            UInt16 WaypointNumber;
            Int32 WaypointToLatitudeOrRelativeY;
            Int32 WaypointToLatitudeOrRelativeX;
            Byte LocationType;
            Int32 WaypointToAltitude;
            Byte WaypointAltitudeType;
            Byte AltitudeChangeBehaviour;
            UInt16 WaypointToSpeed;
            Byte WaypointSpeedType;
            UInt16 NextWaypoint;
            Byte TurnType;
            Byte OptionalMessagesForWaypoint;
            Byte WaypointType;
            Byte LimitType;
            UInt16 LoopLimit;
            UInt64 ArrivalTime;
            UInt32 ActivityID;

            public UA_PositionWaypoint()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("13002.00", PresenceVector);
                field.Add("0802.01", TimeStamp);
                field.Add("0802.04", WaypointNumber);
                field.Add("0802.05", WaypointToLatitudeOrRelativeY);
                field.Add("0802.06", WaypointToLatitudeOrRelativeX);
                field.Add("0802.07", LocationType);
                field.Add("0802.08", WaypointToAltitude);
                field.Add("0802.09", WaypointAltitudeType);
                field.Add("0802.20", AltitudeChangeBehaviour);
                field.Add("0802.10", WaypointToSpeed);
                field.Add("0802.11", WaypointSpeedType);
                field.Add("0802.12", NextWaypoint);
                field.Add("0802.16", TurnType);
                field.Add("13002.01", OptionalMessagesForWaypoint);
                field.Add("0802.17", WaypointType);
                field.Add("0802.18", LimitType);
                field.Add("0802.19", LoopLimit);
                field.Add("0802.15", ArrivalTime);
                field.Add("13002.02", ActivityID);
            }
        }

        class UA_LoiterWaypoint //#13003 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            UInt16 WaypointNumber;
            Byte WaypointLoiterType;
            UInt16 LoiterRadius;
            UInt16 LoiterLength;
            Byte LoiterLengthUnits;
            Int16 LoiterBearing;
            Byte LoiterDirection;
            UInt16 LoiterSpeed;
            Byte SpeedType;
            Byte FlyingBehaviour;
            UInt16 LoiterDuration;
            Byte LoiterDurationUnits;
            UInt32 ActivityID;

            public UA_LoiterWaypoint()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("13003.00", PresenceVector);
                field.Add("0803.01", TimeStamp);
                field.Add("0803.04", WaypointNumber);
                field.Add("0803.06", WaypointLoiterType);
                field.Add("0803.07", LoiterRadius);
                field.Add("0803.08", LoiterLength);
                field.Add("13003.02", LoiterLengthUnits);
                field.Add("0803.09", LoiterBearing);
                field.Add("0803.10", LoiterDirection);
                field.Add("13003.03", LoiterSpeed);
                field.Add("13003.04", SpeedType);
                field.Add("13003.01", FlyingBehaviour);
                field.Add("13003.05", LoiterDuration);
                field.Add("13003.06", LoiterDurationUnits);
                field.Add("13003.07", ActivityID);
            }
        }

        class MissionUploadDownloadStatus //#14000 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            String MissionID;
            Byte Status;
            Byte PercentComplete;
            UInt16 WaypointRequest;
            Byte MissionPlanMode;

            public MissionUploadDownloadStatus()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("14000.00", PresenceVector);
                field.Add("0900.01", TimeStamp);
                field.Add("14000.01", MissionID);
                field.Add("0900.04", Status);
                field.Add("0900.05", PercentComplete);
                field.Add("14000.02", WaypointRequest);
                field.Add("14000.03", MissionPlanMode);
            }
        }

        class SubsystemStatusAlert //#16000 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 DataLinkID;
            Byte Priority;
            Int32 SubsystemStateReportReference;
            Byte SubsystemID;
            Byte AlertGroup;
            UInt16 ControlledStation;//1~16
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber; 
            Byte Type;
            Int32 AlertID;
            String Text;
            SByte Persistence;
            UInt16 ControlledStation32; //17~32
            Byte MessageType;
            Byte AppendOrder;
            Byte AppendTotalNumber;

            public SubsystemStatusAlert()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("16000.00", PresenceVector);
                field.Add("1100.01", TimeStamp);
                field.Add("1100.11", DataLinkID);
                field.Add("1100.04", Priority);
                field.Add("1100.05", SubsystemStateReportReference);
                field.Add("1100.06", SubsystemID);
                field.Add("16000.01", AlertGroup);
                field.Add("16000.02", ControlledStation);
                field.Add("16000.03", ComponentNumber);
                field.Add("16000.04", SubComponentNumber);
                field.Add("1100.07", Type);
                field.Add("1100.08", AlertID);
                field.Add("1100.09", Text);
                field.Add("1100.10", Persistence);
                field.Add("16000.05", ControlledStation32);
                field.Add("1100.12", MessageType);
                field.Add("1100.13", AppendOrder);
                field.Add("1100.14", AppendTotalNumber);
            }
        }

        class SubsystemStatusReport //#16001 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt32 TimeStamp;
            Byte SubsystemID;
            Byte SubsystemState;
            Int32 SubsystemStateReportReference;
            Byte ReportTextType;
            String ReportTextReference;
            UInt16 ControlledStation;
            UInt16 ControlledStation32;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;

            public SubsystemStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("16001.00", PresenceVector);
                field.Add("1101.01", TimeStamp);
                field.Add("1101.04", SubsystemID);
                field.Add("1101.05", SubsystemState);
                field.Add("1101.06", SubsystemStateReportReference);
                field.Add("16001.01", ReportTextType);
                field.Add("16001.02", ReportTextReference);
                field.Add("16001.03", ControlledStation);
                field.Add("16001.04", ControlledStation32);
                field.Add("16001.05", ComponentNumber);
                field.Add("16001.06", SubComponentNumber);
            }
        }

        class MessageAcknowledgement  // #17000 pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            UInt64 OriginalMessageTimeStamp;
            UInt16 OriginalMessageType;
            Byte AcknowledgementType;

            public MessageAcknowledgement()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("17000.00", PresenceVector);
                field.Add("1400.01", TimeStamp);
                field.Add("1400.06", OriginalMessageTimeStamp);
                field.Add("1400.08", OriginalMessageType);
                field.Add("17000.01", AcknowledgementType);
            }
        }

        class ScheduleMessageUpdateCommand //#17001 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            UInt16 RequestedMessageType;
            UInt16 Frequency;
            UInt32 ActivityID;

            public ScheduleMessageUpdateCommand()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("17001.00", PresenceVector);
                field.Add("1402.01", TimeStamp);
                field.Add("1402.04", RequestedMessageType);
                field.Add("1402.05", Frequency);
                field.Add("17001.01", ActivityID);
            }
        }

        class GenericInformationRequestMessage //#17002 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            UInt16 StationNumber;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            UInt16 MessageType;
            UInt64 RequestedFieldPresenceVector;
            UInt32 ActivityID;
            UInt16 StationNumber32;

            public GenericInformationRequestMessage()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("17002.00", PresenceVector);
                field.Add("1403.01", TimeStamp);
                field.Add("1403.06", StationNumber);
                field.Add("17002.01", ComponentNumber);
                field.Add("17002.02", SubComponentNumber);
                field.Add("1403.07", MessageType);
                field.Add("17002.04", RequestedFieldPresenceVector);
                field.Add("17002.05", ActivityID);
                field.Add("17002.03", StationNumber32);
            }
        }

        class FileTransferNotification //#17003 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte FileType;
            String FileName;

            public FileTransferNotification()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("17003.00", PresenceVector);
                field.Add("17003.01", TimeStamp);
                field.Add("17003.02", FileType);
                field.Add("17003.03", FileName);
            }
        }

        class MeteorologicalTableReport //#18300 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            UInt64 PublishTime;
            UInt64 ForecastTime;
            Int32 Latitude;
            Int32 Longitude;
            Byte NumberOfLevels;
            Int32 Altitude;
            Int16 USpeed;
            Int16 VSpeed;
            UInt16 Temperature;
            Byte Humidity;

            public MeteorologicalTableReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("18300.00", PresenceVector);
                field.Add("18300.01", TimeStamp);
                field.Add("18300.02", PublishTime);
                field.Add("18300.03", ForecastTime);
                field.Add("18300.04", Latitude);
                field.Add("18300.05", Longitude);
                field.Add("18300.06", NumberOfLevels);
                field.Add("18300.07", Altitude);
                field.Add("18300.08", USpeed);
                field.Add("18300.09", VSpeed);
                field.Add("18300.10", Temperature);
                field.Add("18300.11", Humidity);
            }
        }

        class GPS_StatusReport //#18500 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Byte NumberOfSatellites;
            Byte GPSMode;
            Byte GPSStatus;
            Byte PDOP;
            Byte HDOP;
            Byte VDOP;
            Byte Reserved;
            UInt64 GPSTime;

            public GPS_StatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("18500.00", PresenceVector);
                field.Add("18500.01", TimeStamp);
                field.Add("18500.02", NumberOfSatellites);
                field.Add("18500.03", GPSMode);
                field.Add("18500.04", GPSStatus);
                field.Add("18500.05", PDOP);
                field.Add("18500.06", HDOP);
                field.Add("18500.07", VDOP);
                field.Add("18500.08", Reserved);
                field.Add("18500.09", GPSTime);
            }
        }

        class DataLinkConfigurationAssignmentMessage //#28001 pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_VehicleID;
            Byte AccessAuthorized;
            Byte AccessGranted;
            Byte AccessRequested;
            Byte TerminalType;
            Byte DataLinkType;
            UInt16 DataLinkSubtype;
            String DataLinkName;
            Byte AntennaType;
            Byte PedestalIndex;
            UInt16 VehicleType;
            UInt16 VehicleSubtype;
            Byte DataLinkControlAvailability;

            public DataLinkConfigurationAssignmentMessage()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("28001.00", PresenceVector);
                field.Add("0500.01", TimeStamp);
                field.Add("28001.06", VSM_VehicleID);
                field.Add("28001.01", AccessAuthorized);
                field.Add("28001.02", AccessGranted);
                field.Add("28001.03", AccessRequested);
                field.Add("0500.07", TerminalType);
                field.Add("0500.08", DataLinkType);
                field.Add("28001.05", DataLinkSubtype);
                field.Add("0500.09", DataLinkName);
                field.Add("0500.10", AntennaType);
                field.Add("28001.04", PedestalIndex);
                field.Add("0500.11", VehicleType);
                field.Add("0500.12", VehicleSubtype);
                field.Add("28000.07", DataLinkControlAvailability);
            }
        }

        class DLToVehicleIDReport //#32000 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Int32 CommunicationVehicleID_Uplink;
            Byte UplinkCommunicationState;
            Int32 CommunicationVehicleID_Downlink;
            Byte DownlinkCommunicationState;

            public DLToVehicleIDReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32000.00", PresenceVector);
                field.Add("32000.01", TimeStamp);
                field.Add("32000.02", CommunicationVehicleID_Uplink);
                field.Add("32000.03", UplinkCommunicationState);
                field.Add("32000.04", CommunicationVehicleID_Downlink);
                field.Add("32000.05", DownlinkCommunicationState);
            }
        }

        class DLA_ConfigurationStatusReport //#32001 pull (7085)
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            
            //Byte STANAG

            public DLA_ConfigurationStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {

            }
        }

        class DLF_ConfigurationStatusReport //#32002 pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            public DLF_ConfigurationStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {

            }
        }

        class DLCountorlStatusReport //#32003 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte TransmitterState;
            Byte TransmitAttenuationState;
            Byte ReceiverState;
            Byte ActiveAntennaPedestalIndex;
            Byte CommunicationSecurityState;
            Byte CommunicationSecurityKeyIndex;

            public DLCountorlStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32003.00", PresenceVector);
                field.Add("32003.01", TimeStamp);
                field.Add("32003.02", TransmitterState);
                field.Add("32003.03", TransmitAttenuationState);
                field.Add("32003.04", ReceiverState);
                field.Add("32003.05", ActiveAntennaPedestalIndex);
                field.Add("0501.13", CommunicationSecurityState);
                field.Add("32003.07", CommunicationSecurityKeyIndex);
            }
        }

        class RFCommandProcessingStatusReport //#32004 pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte CommandsFromRF;

            public RFCommandProcessingStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32004.00", PresenceVector);
                field.Add("32004.01", TimeStamp);
                field.Add("32004.02", CommandsFromRF);
            }
        }

        class LegacyDLStatusReport //#32005 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Byte AddressedTerminal;
            Byte DataLinkState;
            Byte AntennaState;
            UInt16 ReportedChannel;
            Byte ReportedPrimaryHopPattern;
            float ReportedForwardLinkCarrierFrequency;
            float ReportedReturnLinkCarrierFrequency;
            Int16 DownlinkStatus;
            Byte CommunicationSecurityState;
            Byte LinkChannelPriorityState;

            public LegacyDLStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32005.00", PresenceVector);
                field.Add("32005.01", TimeStamp);
                field.Add("0501.05", AddressedTerminal);
                field.Add("0501.06", DataLinkState);
                field.Add("0501.07", AntennaState);
                field.Add("0501.08", ReportedChannel);
                field.Add("0501.09", ReportedPrimaryHopPattern);
                field.Add("0501.10.", ReportedForwardLinkCarrierFrequency);
                field.Add("0501.11.", ReportedReturnLinkCarrierFrequency);
                field.Add("0501.12.", DownlinkStatus);
                field.Add("0501.13.", CommunicationSecurityState);
                field.Add("0501.14.", LinkChannelPriorityState);
            }
        }

        class LegacyDLControlCommandStatus //#32006 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte AddressedTerminal;
            Byte ReportedDemandedDataLinkState;
            Byte ReportedDemandedAntennaMode;
            Byte ReportedDemandedCommunicationSecurityMode;

            public LegacyDLControlCommandStatus()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32006.00", PresenceVector);
                field.Add("32006.01", TimeStamp);
                field.Add("0502.05", AddressedTerminal);
                field.Add("0502.06", ReportedDemandedDataLinkState);
                field.Add("0502.07", ReportedDemandedAntennaMode);
                field.Add("0502.08", ReportedDemandedCommunicationSecurityMode);
            }
        }

        class AntennaPedestalLocationStatusReport //#32100 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt32 TimeStamp;
            SByte PedestalIndex;
            Int32 AntennaPedestalLatitude;
            Int32 AntennaPedestalLongitude;
            Int32 AntennaPedestalAltitude;
            Byte AltitudeType;
            UInt16 AntennaPedestalGroundSpeed;
            Int16 AntennaPedestalVerticalSpeed;
            Int16 AntennaPedestalCourse;
            Int16 AntennaPedestalHeading;
            Int16 AntennaPedestalPitch;
            Int16 AntennaPedestalRoll;

            public AntennaPedestalLocationStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32100.00", PresenceVector);
                field.Add("32100.01", TimeStamp);
                field.Add("32100.10", PedestalIndex);
                field.Add("0503.11", AntennaPedestalLatitude);
                field.Add("0503.12", AntennaPedestalLongitude);
                field.Add("0503.13", AntennaPedestalAltitude);
                field.Add("32100.11", AltitudeType);
                field.Add("32100.05", AntennaPedestalGroundSpeed);
                field.Add("32100.06", AntennaPedestalVerticalSpeed);
                field.Add("32100.07", AntennaPedestalCourse);
                field.Add("32100.12", AntennaPedestalHeading);
                field.Add("32100.08", AntennaPedestalPitch);
                field.Add("32100.09", AntennaPedestalRoll);

            }
        }

        class AntennaControlStatusReport //#32101 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte PedestalIndex;
            Byte PedestalState;
            Byte AntennaPointingMode;
            Byte AntennaTypeMode;
            Byte AntennaType;
            Byte PedestalLocationSource;

            public AntennaControlStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32101.00", PresenceVector);
                field.Add("32101.01", TimeStamp);
                field.Add("32101.02", PedestalIndex);
                field.Add("32101.03", PedestalState);
                field.Add("32101.04", AntennaPointingMode);
                field.Add("32101.07", AntennaTypeMode);
                field.Add("32101.05", AntennaType);
                field.Add("32101.06", PedestalLocationSource);
            }
        }

        class AntennaPositionReport //#32102 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Byte PedestalIndex;
            Int16 ReportedAntennaAzimuth;
            Int16 ReportedAntennaElevation;
            Int16 ReportedAzimuthSlewRate;
            Int16 ReportedElevationSlewRate;
            Int32 ReportedRemoteAntennaLatitude;
            Int32 ReportedRemoteAntennaLongitude;
            Int32 ReportedRemoteAntennaAltitude;
            Byte AltitudeType;
            UInt16 ReportedRemoteAntennaGroundSpeed;
            Int16 ReportedRemoteAntennaCourse;
            Int16 ReportedRemoteAntennaVerticalSpeed;

            public AntennaPositionReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32102.00", PresenceVector);
                field.Add("32102.01", TimeStamp);
                field.Add("32102.02", PedestalIndex);
                field.Add("0503.07", ReportedAntennaAzimuth);
                field.Add("0503.08", ReportedAntennaElevation);
                field.Add("0503.09", ReportedAzimuthSlewRate);
                field.Add("0503.10", ReportedElevationSlewRate);
                field.Add("32102.07", ReportedRemoteAntennaLatitude);
                field.Add("32102.08", ReportedRemoteAntennaLongitude);
                field.Add("32102.09", ReportedRemoteAntennaAltitude);
                field.Add("32102.13", AltitudeType);
                field.Add("32102.10", ReportedRemoteAntennaGroundSpeed);
                field.Add("32102.11", ReportedRemoteAntennaCourse);
                field.Add("32102.12", ReportedRemoteAntennaVerticalSpeed);
            }
        }

        class LegacyPedestalStatusReport //#32103 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt16 TimeStamp;
            Byte AddressedPedestal;
            Byte PedestalModeState;
            float ReportedAntennAzimuth;
            float ReportedAntennaElevation;
            float ReportedAntennaAzimuthSlewRate;
            float ReportedAntennaElevationSlewRate;
            float ReportedCDTLatitude;
            float ReportedCDTLongitude;
            float ReportedCDTAltitude;

            public LegacyPedestalStatusReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32103.00", PresenceVector);
                field.Add("32103.01", TimeStamp);
                field.Add("0503.05", AddressedPedestal);
                field.Add("0503.06", PedestalModeState);
                field.Add("0503.07", ReportedAntennAzimuth);
                field.Add("0503.08", ReportedAntennaElevation);
                field.Add("0503.09", ReportedAntennaAzimuthSlewRate);
                field.Add("0503.10", ReportedAntennaElevationSlewRate);
                field.Add("0503.11", ReportedCDTLatitude);
                field.Add("0503.12", ReportedCDTLongitude);
                field.Add("0503.13", ReportedCDTAltitude);
            }
        }

        class LinkHealthStatus //#32200 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte BitModeStatus;

            public LinkHealthStatus()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32200.00", PresenceVector);
                field.Add("32200.01", TimeStamp);
                field.Add("32200.02", BitModeStatus);
            }
        }

        class DLFallbackStatus //#32201 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte FallbackStatus;

            public DLFallbackStatus()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32201.00", PresenceVector);
                field.Add("32201.01", TimeStamp);
                field.Add("32201.02", FallbackStatus);
            }
        }

        class DL_UDP_Count //#32300 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte UDPMessageCount;

            public DL_UDP_Count()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32300.00", PresenceVector);
                field.Add("32300.01", TimeStamp);
                field.Add("32300.02", UDPMessageCount);
            }
        }

        class UA_IP_DisclosureMessage //#32301 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            UInt16 ControlledStation;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            Byte StreamID;
            Byte StreamNType;
            Byte LengthOfURL;
            String UniformResourceIdentifier;
            UInt16 ControlledStation32;

            public UA_IP_DisclosureMessage()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32301.00", PresenceVector);
                field.Add("32301.01", TimeStamp);
                field.Add("32301.02", ControlledStation);
                field.Add("32301.03", ComponentNumber);
                field.Add("32301.04", SubComponentNumber);
                field.Add("32301.05", StreamID);
                field.Add("32301.06", StreamNType);
                field.Add("32301.07", LengthOfURL);
                field.Add("32301.08", UniformResourceIdentifier);
                field.Add("32301.09", ControlledStation32);
            }
        }

        class DL_in_ControlReport //#32302 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Int32 DataLinkID;
            Int32 InControlCUCSID;
            Byte ControllingLink;
            UInt16 ControlledStation;
            UInt16 ControlledComponentNumber;
            UInt16 ControlledSubComponentNumber;
            UInt16 ControlledStationNumber32;

            public DL_in_ControlReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32302.00", PresenceVector);
                field.Add("32302.01", TimeStamp);
                field.Add("32302.02", DataLinkID);
                field.Add("32302.03", InControlCUCSID);
                field.Add("32302.04", ControllingLink);
                field.Add("32302.05", ControlledStation);
                field.Add("32302.06", ControlledComponentNumber);
                field.Add("32302.07", ControlledSubComponentNumber);
                field.Add("32302.08", ControlledStationNumber32);
            }
        }

        class LostLinkDelayTimerReport //#32400 push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte LostLinkDelayTimerStatus;
            Byte CurrentDelayTime;

            public LostLinkDelayTimerReport()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("32400.00", PresenceVector);
                field.Add("32400.01", TimeStamp);
                field.Add("32400.02", LostLinkDelayTimerStatus);
                field.Add("32400.03", CurrentDelayTime);
            }
        }

        class FieldConfigurationIntegerResponse //#41000 Push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_ID;
            Int32 DataLink_ID;
            UInt16 StationNumber;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            UInt32 RequestedMessage;
            Byte RequestedField;
            Byte FieldSupported;
            Int32 DefaultValue;
            Int32 MaxValue;
            Int32 MinValue;
            Int32 MaxDisplayValue;
            Int32 MinDisplayValue;
            Int32 MinimumDisplayResolution;
            Int32 HighCautionLimit;
            Int32 HighWarningLimit;
            Int32 LowCautionLimit;
            Int32 LowWarningLimit;
            String HelpText;
            Byte SubsystemID;
            Byte RouteType;
            UInt16 StationNumber32;
            Byte WarningAndCautionLimitsUsage;
            Byte DisableAlertCaution;

            public FieldConfigurationIntegerResponse()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("41000.00", PresenceVector);
                field.Add("1300.01", TimeStamp);
                field.Add("1300.04", VSM_ID);
                field.Add("1300.05", DataLink_ID);
                field.Add("1300.06", StationNumber);
                field.Add("41000.01", ComponentNumber);
                field.Add("41000.02", SubComponentNumber);
                field.Add("1300.07", RequestedMessage);
                field.Add("1300.08", RequestedField);
                field.Add("1300.09", FieldSupported);
                field.Add("41000.03", DefaultValue);
                field.Add("1300.10", MaxValue);
                field.Add("1300.11", MinValue);
                field.Add("1300.12", MaxDisplayValue);
                field.Add("1300.13", MinDisplayValue);
                field.Add("1300.14", MinimumDisplayResolution);
                field.Add("1300.15", HighCautionLimit);
                field.Add("1300.16", HighWarningLimit);
                field.Add("1300.17", LowCautionLimit);
                field.Add("1300.18", LowWarningLimit);
                field.Add("1300.19", HelpText);
                field.Add("1300.20", SubsystemID);
                field.Add("1300.21", RouteType);
                field.Add("41000.05", StationNumber32);
                field.Add("41000.04", WarningAndCautionLimitsUsage);
                field.Add("41000.06", DisableAlertCaution);
            }
        }

        class FieldConfigurationFloatResponse //#41001 Push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_ID;
            Int32 DataLink_ID;
            UInt16 StationNumber;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            UInt32 RequestedMessage;
            Byte RequestedField;
            Byte FieldSupported;
            float DefaultValue;
            float MaxValue;
            float MinValue;
            float MaxDisplayValue;
            float MinDisplayValue;
            float MinimumResolution;
            float HighCautionLimit;
            float HighWarningLimit;
            float LowCautionLimit;
            float LowWarningLimit;
            String HelpText;
            Byte SubsystemID;
            Byte RouteType;
            UInt16 StationNumber32;
            Byte WarningAndCautionLimitsUsage;
            Byte DisableAlertCaution;
            public FieldConfigurationFloatResponse()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("41001.00", PresenceVector);
                field.Add("1301.01", TimeStamp);
                field.Add("1301.04", VSM_ID);
                field.Add("1301.05", DataLink_ID);
                field.Add("1301.06", StationNumber);
                field.Add("41001.01", ComponentNumber);
                field.Add("41001.02", SubComponentNumber);
                field.Add("1301.07", RequestedMessage);
                field.Add("1301.08", RequestedField);
                field.Add("1301.09", FieldSupported);
                field.Add("41001.03", DefaultValue);
                field.Add("1301.10", MaxValue);
                field.Add("1301.11", MinValue);
                field.Add("1301.12", MaxDisplayValue);
                field.Add("1301.13", MinDisplayValue);
                field.Add("1301.14", MinimumResolution);
                field.Add("1301.15", HighCautionLimit);
                field.Add("1301.16", HighWarningLimit);
                field.Add("1301.17", LowCautionLimit);
                field.Add("1301.18", LowWarningLimit);
                field.Add("1301.19", HelpText);
                field.Add("1301.20", SubsystemID);
                field.Add("1301.21", RouteType);
                field.Add("41001.04", StationNumber32);
                field.Add("41001.05", WarningAndCautionLimitsUsage);
                field.Add("41001.06", DisableAlertCaution);
            }
        }
        class FieldConfigurationEnumeratedResponse //#41002 Push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_ID;
            Int32 DataLinkID;
            UInt16 StationNumber;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            UInt32 RequestedMessage;
            Byte RequestedField;
            Byte FieldSupported;
            Byte DefaultValue;
            Byte VehicleSpecificEnumerationCount;
            Byte VehicleSpecificEnumerationIndex;
            String VehicleSpecificEnumerationText;
            String VehicleSpecificHelpText;
            Byte RemoveEnumeratedIndex;
            Byte RouteType;
            UInt16 StationNumber32;

            public FieldConfigurationEnumeratedResponse()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("41002.00", PresenceVector);
                field.Add("1302.01", TimeStamp);
                field.Add("1302.04", VSM_ID);
                field.Add("1302.05", DataLinkID);
                field.Add("1302.06", StationNumber);
                field.Add("41002.01", ComponentNumber);
                field.Add("41002.02", SubComponentNumber);
                field.Add("1302.07", RequestedMessage);
                field.Add("1302.08", RequestedField);
                field.Add("1302.09", FieldSupported);
                field.Add("41002.03", DefaultValue);
                field.Add("1302.10", VehicleSpecificEnumerationCount);
                field.Add("1302.11", VehicleSpecificEnumerationIndex);
                field.Add("1302.12", VehicleSpecificEnumerationText);
                field.Add("1302.13", VehicleSpecificHelpText);
                field.Add("41002.05", RemoveEnumeratedIndex);
                field.Add("1302.14", RouteType);
                field.Add("31002.04", StationNumber32);
            }
        }

        class FieldConfigurationUnsignedResponse //#41003 Push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt32 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_ID;
            Int32 DataLinkID;
            UInt16 StationNumber;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            UInt32 RequestedMessage;
            Byte RequestedField;
            Byte FieldSupported;
            UInt32 DefaultValue;
            UInt32 MaxValue;
            UInt32 MinValue;
            UInt32 MaxDisplayValue;
            UInt32 MinDisplayValue;
            UInt32 MinimumDisplayResolution;
            UInt32 HighCautionLimit;
            UInt32 HighWarningLimit;
            UInt32 LowCautionLimit;
            UInt32 LowWarningLimit;
            String HelpText;
            Byte SubsystemID;
            Byte RouteType;
            UInt16 StationNumber32;
            Byte WarningAndCautionLimitsUsage;
            Byte DisableAlertCaution;
            public FieldConfigurationUnsignedResponse()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("41003.00", PresenceVector);
                field.Add("41003.05", TimeStamp);
                field.Add("41003.06", VSM_ID);
                field.Add("41003.07", DataLinkID);
                field.Add("41003.08", StationNumber);
                field.Add("41003.01", ComponentNumber);
                field.Add("41003.02", SubComponentNumber);
                field.Add("41003.09", RequestedMessage);
                field.Add("41003.10", RequestedField);
                field.Add("41003.11", FieldSupported);
                field.Add("41003.24", DefaultValue);
                field.Add("41003.12", MaxValue);
                field.Add("41003.13", MinValue);
                field.Add("41003.14", MaxDisplayValue);
                field.Add("41003.15", MinDisplayValue);
                field.Add("41003.16", MinimumDisplayResolution);
                field.Add("41003.17", HighCautionLimit);
                field.Add("41003.18", HighWarningLimit);
                field.Add("41003.19", LowCautionLimit);
                field.Add("41003.20", LowWarningLimit);
                field.Add("41003.21", HelpText);
                field.Add("41003.25", SubsystemID);
                field.Add("41003.03", RouteType);
                field.Add("41003.04", StationNumber32);
                field.Add("41003.22", WarningAndCautionLimitsUsage);
                field.Add("41003.23", DisableAlertCaution);
            }
        }

        class FieldConfigurationCharacterResponse //#41004 Push/pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_ID;
            Int32 DataLinkID;
            UInt16 StationNumber;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            UInt32 RequestedMessage;
            Byte RequestedField;
            Byte FieldSupported;
            String HelpText;
            Byte SubsystemID;
            Byte RouteType;
            UInt16 StationNumber32;

            public FieldConfigurationCharacterResponse()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("41004.00", PresenceVector);
                field.Add("41004.05", TimeStamp);
                field.Add("41004.06", VSM_ID);
                field.Add("41004.07", DataLinkID);
                field.Add("41004.08", StationNumber);
                field.Add("41004.01", ComponentNumber);
                field.Add("41004.02", SubComponentNumber);
                field.Add("41004.09", RequestedMessage);
                field.Add("41004.10", RequestedField);
                field.Add("41004.11", FieldSupported);
                field.Add("41004.12", HelpText);
                field.Add("41004.13", SubsystemID);
                field.Add("41004.03", RouteType);
                field.Add("41004.04", StationNumber32);
            }
        }

        class ConfigurationComplete //#41005 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            Int32 VSM_ID;
            Int32 DataLinkID;
            UInt16 StationNumber;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            UInt16 VehicleType;
            UInt16 VehicleSubtype;
            UInt16 StationNumber32;

            public ConfigurationComplete()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("41005.00", PresenceVector);
                field.Add("1203.01", TimeStamp);
                field.Add("1203.04", VSM_ID);
                field.Add("1203.05", DataLinkID);
                field.Add("1203.06", StationNumber);
                field.Add("41005.01", ComponentNumber);
                field.Add("41005.02", SubComponentNumber);
                field.Add("1203.07", VehicleType);
                field.Add("1203.08", VehicleSubtype);
                field.Add("41005.03", StationNumber32);
            }
        }

        class ReportControllableElementMessage //42003 push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            UInt16 ControllableElementID;
            String Name;
            Byte InputType;

            public ReportControllableElementMessage()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("42003.00", PresenceVector);
                field.Add("42003.01", TimeStamp);
                field.Add("42003.02", ControllableElementID);
                field.Add("42003.03", Name);
                field.Add("42003.04", InputType);
            }
        }

        class VSM_ServicesReportMessage //#43000 pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            String VSM_HomeWebPageURL;
            String FTP_URL;
            public VSM_ServicesReportMessage()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("43000.00", PresenceVector);
                field.Add("1304.01", TimeStamp);
                field.Add("1304.04", VSM_HomeWebPageURL);
                field.Add("1304.05", FTP_URL);
            }
        }

        class UA_RouteConfiguration //#43001 pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte RouteType;
            Byte Instances;
            UInt16 MaximumNumberWaypoints;
            Byte LocationTypeSupport;

            public UA_RouteConfiguration()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("43001.00", PresenceVector);
                field.Add("43001.01", TimeStamp);
                field.Add("43001.02", RouteType);
                field.Add("43001.03", Instances);
                field.Add("43001.04", MaximumNumberWaypoints);
                field.Add("43001.05", LocationTypeSupport);
            }
        }

        class RemoteWindowsGUI_Definition //#43002 Pull
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            Byte GUI_ReferenceID;
            Byte ParentGUI_ReferenceID;
            Byte SubsystemID;
            String Text;
            Byte Enable;
            public RemoteWindowsGUI_Definition()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("43002.00", PresenceVector);
                field.Add("43002.01", TimeStamp);
                field.Add("43002.02", GUI_ReferenceID);
                field.Add("43002.03", ParentGUI_ReferenceID);
                field.Add("43002.04", SubsystemID);
                field.Add("43002.05", Text);
                field.Add("43002.06", Enable);
            }
        }

        class FieldChangeIntegerCommand //#44000 Push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            UInt16 RequestedMessage;
            Byte RequetedField;
            Int32 CommandedValue;

            public FieldChangeIntegerCommand()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("44000.00", PresenceVector);
                field.Add("44000.01", TimeStamp);
                field.Add("44000.02", RequestedMessage);
                field.Add("44000.03", RequetedField);
                field.Add("44000.04", CommandedValue);
            }
        }
        class FieldChangeFloatCommand //#44001 Push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            UInt16 RequestedMessage;
            Byte RequetedField;
            float CommandedValue;

            public FieldChangeFloatCommand()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("44001.00", PresenceVector);
                field.Add("44001.01", TimeStamp);
                field.Add("44001.02", RequestedMessage);
                field.Add("44001.03", RequetedField);
                field.Add("44001.04", CommandedValue);
            }
        }
        class FieldChangeEnumeratedCommand //#44002 Push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            Byte PresenceVector;
            UInt64 TimeStamp;
            UInt16 RequestedMessage;
            Byte RequetedField;
            Int32 CommandedEnumerationIdexValue;

            public FieldChangeEnumeratedCommand()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("44002.00", PresenceVector);
                field.Add("44002.01", TimeStamp);
                field.Add("44002.02", RequestedMessage);
                field.Add("44002.03", RequetedField);
                field.Add("44002.04", CommandedEnumerationIdexValue);
            }
        }
        class MessageParameterAvailability //#44003 Push
        {
            Dictionary<String, object> field = new Dictionary<String, object>();
            DateTime dt = DateTime.Now;

            UInt16 PresenceVector;
            UInt64 TimeStamp;
            UInt16 StationNumber;
            UInt16 ComponentNumber;
            UInt16 SubComponentNumber;
            UInt32 ReportedMessage;
            Byte ReportedField;
            Byte FieldAvailable;
            Byte ReportedEnumeratedIndex;
            SByte EnumeratedIndexEnable;
            UInt16 StationNumber32;
            Byte RouteType;
            public MessageParameterAvailability()
            {

            }
            private void FieldAdd() //초기값 추가 및 값 업데이트
            {
                field.Add("44003.00", PresenceVector);
                field.Add("1303.01", TimeStamp);
                field.Add("1303.05", StationNumber);
                field.Add("44003.01", ComponentNumber);
                field.Add("44003.02", SubComponentNumber);
                field.Add("1303.06", ReportedMessage);
                field.Add("1303.07", ReportedField);
                field.Add("1303.08", FieldAvailable);
                field.Add("1303.09", ReportedEnumeratedIndex);
                field.Add("1303.10", EnumeratedIndexEnable);
                field.Add("44003.03", StationNumber32);
                field.Add("1303.12", RouteType);
            }
        }
    }
}
