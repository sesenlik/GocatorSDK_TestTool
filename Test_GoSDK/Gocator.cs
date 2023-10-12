using Lmi3d.GoSdk.Tools;
using Lmi3d.GoSdk;
using Lmi3d.Zen.Io;
using Lmi3d.Zen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_GoSDK;

namespace Gocator
{
    public class StitchParam
    {
        public bool Enable { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Rx { get; set; }
        public float Ry { get; set; }
        public float Rz { get; set; }
        public bool Mirrored { get; set; }

        public StitchParam(bool enable, float X, float Y, float Z, float Rx, float Ry, float Rz, bool mirrored)
        {
            this.Enable = enable;
            this.X = X; this.Y = Y; this.Z = Z; this.Rx = Rx; this.Ry = Ry; this.Rz = Rz;
            this.Mirrored = mirrored;
        }
    }
    public class Gocator
    {
        private string IPAddress { get; set; }
        private KIpAddress ipAddress { get; set; }
        private GoSystem System { get; set; }
        private GoSensor Sensor { get; set; }
        public Gocator(string IPAddress)
        {
            this.IPAddress = IPAddress;
            this.Init();
            this.FindSensor();
            this.Connect();
            this.EnableData(true);
        }



        bool Init()
        {
            KApiLib.Construct();
            GoSdkLib.Construct();
            this.System = new GoSystem();

            return true;
        }

        bool FindSensor()
        {
            this.ipAddress = KIpAddress.Parse(this.IPAddress);
            this.Sensor = this.System.FindSensorByIpAddress(this.ipAddress);

            return true;
        }

        bool Connect()
        {
            this.Sensor.Connect();

            return true;
        }

        bool EnableData(bool enable)
        {
            this.System.EnableData(enable);

            return true;
        }

        public void ClearAllTools()
        {
            this.Sensor.Tools.ClearTools();
        }

        public void AddSurfaceStitch(string DisplayName, int SurfaceCount, bool EnforceFrameOrder, bool ResetOnStart, bool BilinearInterpolation, StitchParam[] StitchCoordinates)
        {
            //retrieve tools handle
            GoTools tools = this.Sensor.Tools;

            GoExtTool SurfaceStitch = (GoExtTool)tools.AddToolByName("SurfaceStitch");
            SurfaceStitch.DisplayName = DisplayName;

            GoExtParamInt _SurfaceCount = (GoExtParamInt)SurfaceStitch.FindParameterById("CaptureCount");
            _SurfaceCount.Value = SurfaceCount;

            GoExtParamBool _EnforceFrameOrder = (GoExtParamBool)SurfaceStitch.FindParameterById("EnforceFrameOrder");
            _EnforceFrameOrder.Value = EnforceFrameOrder;

            GoExtParamBool _ResetOnStart = (GoExtParamBool)SurfaceStitch.FindParameterById("ResetOnStart");
            _ResetOnStart.Value = ResetOnStart;

            GoExtParamBool _BilinearInterpolation = (GoExtParamBool)SurfaceStitch.FindParameterById("BilinearInterpolation");
            _BilinearInterpolation.Value = BilinearInterpolation;

            for (int i = 0; i < StitchCoordinates.Length; i++)
            {
                bool Enable = StitchCoordinates[i].Enable;
                float X = StitchCoordinates[i].X;
                float Y = StitchCoordinates[i].Y;
                float Z = StitchCoordinates[i].Z;
                float Rx = StitchCoordinates[i].Rx;
                float Ry = StitchCoordinates[i].Ry;
                float Rz = StitchCoordinates[i].Rz;
                bool Mirrored = StitchCoordinates[i].Mirrored;
                SetStitchCoordinate(SurfaceStitch, i + 1, Enable, X, Y, Z, Rx, Ry, Rz, Mirrored);
            }

            GoTool test = this.Sensor.Tools.GetTool(0);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 1, true, 100.001f, 200.002f, 300.003f, 400.004f, 500.005f, 600.006f, true);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 2, true, 100.101f, 200.202f, 300.303f, 400.404f, 500.505f, 600.606f, true);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 3, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 11, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 13, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
        }

        private void SetStitchCoordinate(GoExtTool tool, int surfaceIndex, bool enable, float X, float Y, float Z, float Rx, float Ry, float Rz, bool mirrored)
        {
            GoExtParamBool CaptureParams = (GoExtParamBool)tool.FindParameterById("CaptureParams" + surfaceIndex.ToString());
            CaptureParams.Value = enable;

            GoExtParamFloat CaptureXOffset = (GoExtParamFloat)tool.FindParameterById("Capture" + surfaceIndex.ToString() + "XOffset");
            CaptureXOffset.Value = X;

            GoExtParamFloat CaptureYOffset = (GoExtParamFloat)tool.FindParameterById("Capture" + surfaceIndex.ToString() + "YOffset");
            CaptureYOffset.Value = Y;

            GoExtParamFloat CaptureZOffset = (GoExtParamFloat)tool.FindParameterById("Capture" + surfaceIndex.ToString() + "ZOffset");
            CaptureZOffset.Value = Z;

            GoExtParamFloat CaptureXAngle = (GoExtParamFloat)tool.FindParameterById("Capture" + surfaceIndex.ToString() + "XAngle");
            CaptureXAngle.Value = Rx;

            GoExtParamFloat CaptureYAngle = (GoExtParamFloat)tool.FindParameterById("Capture" + surfaceIndex.ToString() + "YAngle");
            CaptureYAngle.Value = Ry;

            GoExtParamFloat CaptureZAngle = (GoExtParamFloat)tool.FindParameterById("Capture" + surfaceIndex.ToString() + "ZAngle");
            CaptureZAngle.Value = Rz;

            GoExtParamBool CaptureMirror = (GoExtParamBool)tool.FindParameterById("Capture" + surfaceIndex.ToString() + "Mirror");
            CaptureMirror.Value = mirrored;

        }
    }
}
