using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Tools;
using Lmi3d.GoSdk.Outputs;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using Gocator;


static class Constants
{
    public const string SENSOR_IP = "127.0.0.1"; // IP of the sensor used for sensor connection GoSystem_FindSensorByIpAddress() call.
}

namespace Test_GoSDK
{
    internal class Program
    {
        static int Main(string[] args)
        {
            try
            {
                //testFunctions();

                Gocator.Gocator gocator = new Gocator.Gocator("127.0.0.1");

                StitchParam[] stitchParams = new StitchParam[5];
                stitchParams[0] = new StitchParam(true, 100.001f, 200.002f, 300.003f, 400.004f, 500.005f, 600.006f, true);
                stitchParams[1] = new StitchParam(true, 100.101f, 200.202f, 300.303f, 400.404f, 500.505f, 600.606f, true);
                stitchParams[2] = new StitchParam(true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
                stitchParams[3] = new StitchParam(true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
                stitchParams[4] = new StitchParam(true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);

                gocator.AddSurfaceStitch("Deneme123456", stitchParams.Length - 1, false, true, true, stitchParams);
            }
            catch (KException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            // wait for ENTER key
            Console.WriteLine("\nPress ENTER to continue");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            return 1;
        }


        public static void testFunctions()
        {
            try
            {
                KApiLib.Construct();
                GoSdkLib.Construct();
                GoSystem system = new GoSystem();
                GoSensor sensor;

                KIpAddress ipAddress = KIpAddress.Parse(Constants.SENSOR_IP);
                sensor = system.FindSensorByIpAddress(ipAddress);


                sensor.Connect();

                system.EnableData(true);

                //retrieve setup handle
                GoSetup setup = sensor.Setup;
                //retrieve tools handle
                GoTools tools = sensor.Tools;


                GocatorHelper.ClearAllTools(sensor);


                //for (int i = 0; i < sensor.Tools.ToolCount; i++)
                //{
                //    GoTool test = sensor.Tools.GetTool(0);
                //}

                //// add ProfilePosition tool, retreive tool handle
                //GoProfilePosition profilePositionTool = (GoProfilePosition)tools.AddTool(GoToolType.ProfilePosition);
                //// set name for tool
                //profilePositionTool.Name = "TEEEEEEEEEEEEEEEEEEEST";
                //// add Z measurement for ProfilePosition tool
                //GoProfilePositionZ zProfileMeasurementTop = profilePositionTool.ZMeasurement;
                //zProfileMeasurementTop.Enabled = true;
                //zProfileMeasurementTop.Id = 1;
                ////set ProfilePosition feature to top
                //GoProfileFeature profileFeature = profilePositionTool.Feature;
                //profileFeature.FeatureType = GoProfileFeatureType.MaxZ;
                //// set the ROI to fill the entire active area
                //GoProfileRegion regionTop = profileFeature.Region;
                //regionTop.X = setup.GetTransformedDataRegionX(GoRole.Main);
                //regionTop.Z = setup.GetTransformedDataRegionZ(GoRole.Main);
                //regionTop.Height = setup.GetTransformedDataRegionHeight(GoRole.Main);
                //regionTop.Width = setup.GetTransformedDataRegionWidth(GoRole.Main);
                //// enable Ethernet output for measurement tool
                //GoOutput output = sensor.Output;
                //GoEthernet ethernetOutput = output.GetEthernet();
                //ethernetOutput.ClearAllSources();
                //ethernetOutput.AddSource(GoOutputSource.Measurement, 0);


                //GoTool SurfaceStitchTool = tools.AddToolByName("SurfaceStitch");


                //GoExtTool SurfaceStitch = (GoExtTool)tools.AddToolByName("SurfaceStitch");
                //SurfaceStitch.DisplayName = "test";

                //GoExtParamInt SurfaceCount = (GoExtParamInt)SurfaceStitch.FindParameterById("CaptureCount");
                //SurfaceCount.Value = 12;

                //GoExtParamBool EnforceFrameOrder = (GoExtParamBool)SurfaceStitch.FindParameterById("EnforceFrameOrder");
                //EnforceFrameOrder.Value = false;

                //GoExtParamBool ResetOnStart = (GoExtParamBool)SurfaceStitch.FindParameterById("ResetOnStart");
                //ResetOnStart.Value = true;

                //GoExtParamBool BilinearInterpolation = (GoExtParamBool)SurfaceStitch.FindParameterById("BilinearInterpolation");
                //BilinearInterpolation.Value = true;

                StitchParam[] stitchParams = new StitchParam[5];
                stitchParams[0] = new StitchParam(true, 100.001f, 200.002f, 300.003f, 400.004f, 500.005f, 600.006f, true);
                stitchParams[1] = new StitchParam(true, 100.101f, 200.202f, 300.303f, 400.404f, 500.505f, 600.606f, true);
                stitchParams[2] = new StitchParam(true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
                stitchParams[3] = new StitchParam(true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
                stitchParams[4] = new StitchParam(true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);

                GocatorHelper.AddSurfaceStitch(sensor, "Deneme12222", stitchParams.Length - 1, false, true, true, stitchParams);

                //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 1, true, 100.001f, 200.002f, 300.003f, 400.004f, 500.005f, 600.006f, true);
                //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 2, true, 100.101f, 200.202f, 300.303f, 400.404f, 500.505f, 600.606f, true);
                //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 3, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
                //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 11, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
                //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 13, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);

                //switch (SurfaceCount.Type)
                //{
                //    case GoExtParamType.EXT_PARAM_TYPE_INT:

                //        break;
                //    case GoExtParamType.EXT_PARAM_TYPE_BOOL:

                //        break;

                //    default:
                //        break;
                //}







                //for (int i = 0; i < SurfaceStitch.ParameterCount; i++)
                //{
                //    GoExtParam par = SurfaceStitch.GetParameter(i);
                //}


                GoTool test = sensor.Tools.GetTool(0);
            }
            catch (KException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            
        }

        public string GetIdFromName(GoExtParam par, string name, int parCount)
        {
            for (int i = 0; i < parCount; i++)
            {
                //GoExtParam par = surfExtTool.GetParameter(i);
            }
            return "";
        }

        
    }

    

    public class GocatorHelper
    {
        public static void ClearAllTools(GoSensor sensor)
        {
            sensor.Tools.ClearTools();
        }

        public static void AddSurfaceStitch(GoSensor sensor, string DisplayName, int SurfaceCount, bool EnforceFrameOrder, bool ResetOnStart, bool BilinearInterpolation, StitchParam[] StitchCoordinates)
        {
            //retrieve tools handle
            GoTools tools = sensor.Tools;

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
                SetStitchCoordinate(SurfaceStitch, i+1, Enable, X, Y, Z, Rx, Ry, Rz, Mirrored);
            }

            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 1, true, 100.001f, 200.002f, 300.003f, 400.004f, 500.005f, 600.006f, true);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 2, true, 100.101f, 200.202f, 300.303f, 400.404f, 500.505f, 600.606f, true);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 3, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 11, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
            //GocatorHelper.SetStitchCoordinate(SurfaceStitch, 13, true, 100.201f, 200.302f, 300.403f, 400.504f, 500.605f, 600.706f, true);
        }

        public static void SetStitchCoordinate(GoExtTool tool, int surfaceIndex, bool enable, float X, float Y, float Z, float Rx, float Ry, float Rz, bool mirrored)
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
