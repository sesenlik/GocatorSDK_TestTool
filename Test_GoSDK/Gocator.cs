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
using Lmi3d.GoSdk.Messages;

namespace GocatorHelper
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

        public StitchParam()
        {
            
        }
        public StitchParam(bool enable, float X, float Y, float Z, float Rx, float Ry, float Rz, bool mirrored)
        {
            this.Enable = enable;
            this.X = X; this.Y = Y; this.Z = Z; this.Rx = Rx; this.Ry = Ry; this.Rz = Rz;
            this.Mirrored = mirrored;
        }
    }
    public class ResultData
    {
        public GoStamp Stamp { get; set; }
        public MeasurementData[] Measurements { get; set; }
    }
    public class MeasurementData
    {
        public double Value { get; set; }
        public uint Id { get; set; }
        public string Name { get; set; }
        public string ToolName { get; set; }
        public string Decision { get; set; }
    }
    public class Gocator
    {
        private string IPAddress { get; set; }
        private KIpAddress ipAddress { get; set; }
        private GoSystem System { get; set; }
        private GoSensor Sensor { get; set; }
        private bool Initialized { get; set; }
        private bool FoundSensor { get; set; }
        private bool Connected { get; set; }
        private bool DataEnabled { get; set; }
        public Gocator(string IPAddress)
        {
            this.IPAddress = IPAddress;
            this.Init();
            this.FindSensor();
            this.Connect();
            this.EnableData(true);
            var test = this.Sensor.FileExists("TubMeasurement.job");
            //this.Sensor.DeleteFile("TubMeasurement.job");
            this.Sensor.UploadFile("D:\\empty.job", "DENEME12345.job");
            test = this.Sensor.FileExists("DENEME12345.job");
            //var test = this.Sensor.DefaultJob;
        }

        private void MainLoop()
        {
            if (!this.Init()) return;
            if (!this.FindSensor()) return;
            if (!this.Connect()) return;
            if (!this.EnableData(true)) return;
            bool Ready = this.Initialized && this.FoundSensor && this.Connected && this.DataEnabled;
        }

        bool Init()
        {
            try
            {
                KApiLib.Construct();
                GoSdkLib.Construct();
                this.System = new GoSystem();
                this.Initialized = true;
                return true;
            }
            catch (Exception)
            {
                this.Initialized = false;
                return false;
                //throw;
            }
            

            
        }

        bool FindSensor()
        {
            try
            {
                this.ipAddress = KIpAddress.Parse(this.IPAddress);
                this.Sensor = this.System.FindSensorByIpAddress(this.ipAddress);
            }
            catch (Exception)
            {

                //throw;
            }
            

            return true;
        }

        bool Connect()
        {
            try
            {
                this.Sensor.Connect();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            

            
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

        private string GetToolName(GoTools tools, uint id)
        {
            string toolName = "";
            //Find out ToolName
            for (int i = 0; i < tools.ToolCount; i++)
            {
                GoTool tool = tools.GetTool(i);
                for (int j = 0; j < tool.MeasurementCount; j++)
                {
                    GoMeasurement meas = tool.GetMeasurement(j);
                    if (meas.Id == id)
                    {
                        toolName = tool.Name;
                    }

                    if (toolName != "")
                    {
                        break;
                    }
                }
                if (toolName != "")
                {
                    break;
                }
            }
            return toolName;
        }
        public ResultData WaitMeasurementResult()
        {
            GoTools tools = this.Sensor.Tools;
            ResultData resultData = new ResultData();

            List<MeasurementData> measurements = new List<MeasurementData>();
            UInt32 id;
            GoDataSet dataSet = new GoDataSet();
            dataSet = this.System.ReceiveData(30000000);
            // retrieve tools handle
            //collection_tools = sensor.Tools;

            for (UInt32 i = 0; i < dataSet.Count; i++)
            {

                GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);
                switch (dataObj.MessageType)
                {
                    case GoDataMessageType.Stamp:
                        {
                            GoStampMsg stampMsg = (GoStampMsg)dataObj;
                            for (UInt32 j = 0; j < stampMsg.Count; j++)
                            {
                                GoStamp stamp = stampMsg.Get(j);
                                resultData.Stamp = stamp;
                                Console.WriteLine("Frame Index = {0}", stamp.FrameIndex);
                                Console.WriteLine("Time Stamp = {0}", stamp.Timestamp);
                                Console.WriteLine("Encoder Value = {0}", stamp.Encoder);
                                Console.WriteLine("");
                            }
                        }
                        break;
                    case GoDataMessageType.Measurement:
                        {
                            GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;
                            for (UInt32 k = 0; k < measurementMsg.Count; ++k)
                            {
                                GoMeasurementData measurementData = measurementMsg.Get(k);

                                Console.WriteLine("ID: {0}", measurementMsg.Id);

                                //1. Retrieve Id
                                id = measurementMsg.Id;
                                //2. Retrieve the measurement from the set of tools using measurement ID
                                GoMeasurement measurement = tools.FindMeasurementById(id);
                                var toolName = this.GetToolName(tools, id);

                                MeasurementData measData = new MeasurementData();
                                measData.Id = id;
                                measData.ToolName = toolName;
                                measData.Name = measurement.Name;
                                measData.Value = measurementData.Value;
                                measData.Decision = measurementData.Decision.ToString();
                                measurements.Add(measData);

                                Console.WriteLine("Tool Name is : {0}", toolName);
                                //3. Print the measurement name 
                                Console.WriteLine("Measurement Name is : {0}", measurement.Name);
                                Console.WriteLine("Value: {0}", measurementData.Value);
                                Console.WriteLine("Decision: {0}", measurementData.Decision);
                            }
                        }
                        break;
                }
            }
            resultData.Measurements = measurements.ToArray();
            return resultData;
        }
    }
}
