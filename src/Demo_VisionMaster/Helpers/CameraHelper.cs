using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_VisionMaster.Views;
using MvCamCtrl.NET;
using VisionDesigner;
using VisionDesigner.Camera;
using static System.Net.Mime.MediaTypeNames;

namespace Demo_VisionMaster.Helpers
{
    public class CameraHelper
    {
        public CameraHelper() { }

        CCameraTool tool  = new CCameraTool();

        private string _strDevConnectedSerialNumber;
        private bool _bDevConnectedChangedIgnore ;
        private int _nDevConnectedIdx;


        public void GetListDevice() 
        {
            try
            {
                CCameraTool.EnumDevices(Convert.ToUInt32((MVD_TRANSFER_LAYER_TYPE)5));
                DeviceListRefresh();
            }
            catch (MvdException ex)
            {
                ViewHome.Instance.UpdateNotification("Enum failed, ErrorCode is: 0x" + ex.ErrorCode.ToString("X"));
            }
            catch (Exception)
            {
                ViewHome.Instance.UpdateNotification("Enum failed with standard exception");
            }
        }

        public void SelectDevice(int deviceIndex) 
        {
            ViewHome.Instance.RefectListDevice();
            if (-1 == deviceIndex)
            {
                return;
            }

            if (_bDevConnectedChangedIgnore)
            {
                _nDevConnectedIdx = deviceIndex;
                _bDevConnectedChangedIgnore = false;
                return;
            }

            if (_nDevConnectedIdx == deviceIndex && _nDevConnectedIdx != 0)
            {
                ViewHome.Instance.UpdateNotification("Device already opened.");
                return;
            }

            if (tool == null)
            {
                ViewHome.Instance.UpdateNotification("Please attach a CameraTool object firstly.");
                return;
            }

            string text = "open";
            if (_nDevConnectedIdx != 0)
            {
                try
                {
                    tool.CloseDevice();
                    _nDevConnectedIdx = 0;
                    _strDevConnectedSerialNumber = null;
                    ViewHome.Instance.UpdateNotification("");
                    text = "switch";
                }
                catch (MvdException ex)
                {
                    ViewHome.Instance.UpdateNotification("Release opened device failed, ErrorCode is: 0x" + ex.ErrorCode.ToString("X"));
                    return;
                }
                catch (Exception)
                {
                    ViewHome.Instance.UpdateNotification("Release opened device failed with standard exception");
                    return;
                }
            }

            if (deviceIndex == 0)
            {
                return;
            }

            try
            {
                tool.SelectDevice(deviceIndex - 1);
                tool.OpenDevice(1u, 0);
                _nDevConnectedIdx = deviceIndex;
                CDeviceInfo connectedDeviceInfo = tool.ConnectedDeviceInfo;
                if (connectedDeviceInfo.TLayerType == MVD_TRANSFER_LAYER_TYPE.MVD_GIGE_DEVICE)
                {
                    CGigeDeviceInfo cGigeDeviceInfo = connectedDeviceInfo as CGigeDeviceInfo;
                    _strDevConnectedSerialNumber = cGigeDeviceInfo.SerialNumber;
                }
                else if (connectedDeviceInfo.TLayerType == MVD_TRANSFER_LAYER_TYPE.MVD_USB_DEVICE)
                {
                    CUSBDeviceInfo cUSBDeviceInfo = connectedDeviceInfo as CUSBDeviceInfo;
                    _strDevConnectedSerialNumber = cUSBDeviceInfo.SerialNumber;
                }
                ViewHome.Instance.UpdateNotification("Successfully " + text + " device.");
            }
            catch (MvdException ex3)
            {
                ViewHome.Instance.UpdateNotification("Open device failed, ErrorCode is: 0x" + ex3.ErrorCode.ToString("X"));
            }
            catch (Exception)
            {
                ViewHome.Instance.UpdateNotification("Open device failed with standard exception");
            }
            ViewHome.Instance.DefautListDevice();
        }
        public void openCamera() 
        {
            if (_nDevConnectedIdx != 0)
            {
                try
                {
                    tool.StartGrab();
                    ViewHome.Instance.UpdateNotification("Start grabbing successfully.");
                }
                catch (MvdException ex)
                {
                    ViewHome.Instance.UpdateNotification("Start grabbing failed, ErrorCode is: 0x" + ex.ErrorCode.ToString("X"));
                }
                catch (Exception)
                {
                    ViewHome.Instance.UpdateNotification("Start grabbing failed with standard exception");
                }

                if (MVD_CAMERA_TOOL_STATUS.MVD_DEVICE_GRABING == tool.RunStatus)
                {
                    //DisableCtrlWhenStart();
                }
            }
        }
        public void closeCamera()
        {
            if (_nDevConnectedIdx != 0)
            {
                try
                {
                    tool.StopGrab();
                    ViewHome.Instance.UpdateNotification("Start grabbing successfully.");
                }
                catch (MvdException ex)
                {
                    ViewHome.Instance.UpdateNotification("Start grabbing failed, ErrorCode is: 0x" + ex.ErrorCode.ToString("X"));
                }
                catch (Exception)
                {
                    ViewHome.Instance.UpdateNotification("Start grabbing failed with standard exception");
                }

                if (MVD_CAMERA_TOOL_STATUS.MVD_DEVICE_GRABING == tool.RunStatus)
                {
                    //DisableCtrlWhenStart();
                }
            }
        }

        public CMvdImage GetImage() 
        {
            try
            {
                CMvdImage cFrameImage = new CMvdImage();
                tool.CameraGrabResult.GetOneFrameTimeout(ref cFrameImage);
                return cFrameImage;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        
        }
        private void DeviceListRefresh()
        {
            ViewHome.Instance.RefectListDevice();
            int num = 0;
            bool flag = true;
            try
            {
                ViewHome.Instance.UpdateListDevice("< None >");
                foreach (CDeviceInfo deviceInfo in CCameraTool.DeviceInfoList)
                {
                    if (flag)
                    {
                        num++;
                    }

                    if (deviceInfo.TLayerType == MVD_TRANSFER_LAYER_TYPE.MVD_GIGE_DEVICE)
                    {
                        CGigeDeviceInfo cGigeDeviceInfo = deviceInfo as CGigeDeviceInfo;
                        if (cGigeDeviceInfo.UserDefinedName != "")
                        {
                            byte[] bytes = Encoding.Default.GetBytes(cGigeDeviceInfo.UserDefinedName);
                            if (MVD_IsTextUTF8(bytes))
                            {
                                string @string = Encoding.UTF8.GetString(bytes);
                                ViewHome.Instance.UpdateListDevice("GEV: " + @string + " (" + cGigeDeviceInfo.SerialNumber + ")");
                            }
                            else
                            {
                                ViewHome.Instance.UpdateListDevice("GEV: " + cGigeDeviceInfo.UserDefinedName + " (" + cGigeDeviceInfo.SerialNumber + ")");
                            }
                        }
                        else
                        {
                            ViewHome.Instance.UpdateListDevice("GEV: " + cGigeDeviceInfo.ManufacturerName + " " + cGigeDeviceInfo.ModelName + " (" + cGigeDeviceInfo.SerialNumber + ")");
                        }

                        if (cGigeDeviceInfo.SerialNumber == _strDevConnectedSerialNumber)
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        if (deviceInfo.TLayerType != MVD_TRANSFER_LAYER_TYPE.MVD_USB_DEVICE)
                        {
                            continue;
                        }

                        CUSBDeviceInfo cUSBDeviceInfo = deviceInfo as CUSBDeviceInfo;
                        if (cUSBDeviceInfo.UserDefinedName != "")
                        {
                            byte[] bytes2 = Encoding.Default.GetBytes(cUSBDeviceInfo.UserDefinedName);
                            if (MVD_IsTextUTF8(bytes2))
                            {
                                string string2 = Encoding.UTF8.GetString(bytes2);
                                ViewHome.Instance.UpdateListDevice("U3V: " + string2 + " (" + cUSBDeviceInfo.SerialNumber + ")");
                            }
                            else
                            {
                                ViewHome.Instance.UpdateListDevice("U3V: " + cUSBDeviceInfo.UserDefinedName + " (" + cUSBDeviceInfo.SerialNumber + ")");
                            }
                        }
                        else
                        {
                            ViewHome.Instance.UpdateListDevice("U3V: " + cUSBDeviceInfo.ManufacturerName + " " + cUSBDeviceInfo.ModelName + " (" + cUSBDeviceInfo.SerialNumber + ")");
                        }

                        if (cUSBDeviceInfo.SerialNumber == _strDevConnectedSerialNumber)
                        {
                            flag = false;
                        }
                    }
                }
            }
            catch (MvdException ex)
            {
                ViewHome.Instance.UpdateNotification("Enum failed, ErrorCode is: 0x" + ex.ErrorCode.ToString("X"));
            }
            catch (Exception)
            {
                ViewHome.Instance.UpdateNotification("Enum failed with standard exception");
            }

            if (flag)
            {
                num = 0;
            }
            ViewHome.Instance.DefautListDevice();
        }
        private bool MVD_IsTextUTF8(byte[] pcText)
        {
            int num = 0;
            bool flag = true;
            byte b = 0;
            for (int i = 0; i < pcText.Length; i++)
            {
                b = pcText[i];
                if ((b & 0x80u) != 0)
                {
                    flag = false;
                }

                if (num == 0)
                {
                    if (b < 128)
                    {
                        continue;
                    }

                    if (b >= 252 && b <= 253)
                    {
                        num = 6;
                    }
                    else if (b >= 248)
                    {
                        num = 5;
                    }
                    else if (b >= 240)
                    {
                        num = 4;
                    }
                    else if (b >= 224)
                    {
                        num = 3;
                    }
                    else
                    {
                        if (b < 192)
                        {
                            return false;
                        }

                        num = 2;
                    }

                    num--;
                }
                else
                {
                    if ((b & 0xC0) != 128)
                    {
                        return false;
                    }

                    num--;
                }
            }

            if (flag)
            {
                return false;
            }

            if (num != 0)
            {
                return false;
            }

            return true;
        }


//Mutiple Handle Device---------------------------------------------------------------------------------------------


        List<CCameraTool> cs = new List<CCameraTool>();
        List<CMvdImage> images = new List<CMvdImage>();
        Dictionary<string, int> dic = new Dictionary<string, int>();

        public Dictionary<string, int> ListDevice() 
        {
            if (dic == null) return null;
            return dic;
        }
        public List<CMvdImage> Listimages()
        {
            if (images == null) return null;
            return images;
        }
        public List<CCameraTool> CamTool() 
        {
            if (cs == null) return null;
            return cs;
        }
        public void CreateDicCam() 
        {
            int num = 0;
            bool flag = true;
            CCameraTool.EnumDevices(Convert.ToUInt32((MVD_TRANSFER_LAYER_TYPE)5));
            foreach (CDeviceInfo deviceInfo in CCameraTool.DeviceInfoList)
            {
                if (flag)
                {
                    num++;
                }
                if (deviceInfo.TLayerType == MVD_TRANSFER_LAYER_TYPE.MVD_GIGE_DEVICE)
                {
                    CGigeDeviceInfo cGigeDeviceInfo = deviceInfo as CGigeDeviceInfo;
                    if (cGigeDeviceInfo.UserDefinedName != "")
                    {
                        byte[] bytes = Encoding.Default.GetBytes(cGigeDeviceInfo.UserDefinedName);
                        if (MVD_IsTextUTF8(bytes))
                        {
                            string @string = Encoding.UTF8.GetString(bytes);
                            string info = "GEV: " + @string + " (" + cGigeDeviceInfo.SerialNumber + ")";
                            dic.Add(info, num);
                        }
                        else
                        {
                            string info = "GEV: " + cGigeDeviceInfo.UserDefinedName + " (" + cGigeDeviceInfo.SerialNumber + ")";
                            dic.Add(info, num);
                        }
                    }
                    else
                    {
                        string info = "GEV: " + cGigeDeviceInfo.ManufacturerName + " " + cGigeDeviceInfo.ModelName + " (" + cGigeDeviceInfo.SerialNumber + ")";
                        dic.Add(info, num);
                    }
                    if (cGigeDeviceInfo.SerialNumber == _strDevConnectedSerialNumber)
                    {
                        flag = false;
                    }
                }
                else
                {
                    if (deviceInfo.TLayerType != MVD_TRANSFER_LAYER_TYPE.MVD_USB_DEVICE)
                    {
                        continue;
                    }
                    CUSBDeviceInfo cUSBDeviceInfo = deviceInfo as CUSBDeviceInfo;
                    if (cUSBDeviceInfo.UserDefinedName != "")
                    {
                        byte[] bytes2 = Encoding.Default.GetBytes(cUSBDeviceInfo.UserDefinedName);
                        if (MVD_IsTextUTF8(bytes2))
                        {
                            string string2 = Encoding.UTF8.GetString(bytes2);
                            string info = "U3V: " + string2 + " (" + cUSBDeviceInfo.SerialNumber + ")";
                            dic.Add(info, num);
                        }
                        else
                        {
                            string info = "U3V: " + cUSBDeviceInfo.UserDefinedName + " (" + cUSBDeviceInfo.SerialNumber + ")";
                            dic.Add(info, num);
                        }
                    }
                    else
                    {
                        string info = "U3V: " + cUSBDeviceInfo.ManufacturerName + " " + cUSBDeviceInfo.ModelName + " (" + cUSBDeviceInfo.SerialNumber + ")";
                        dic.Add(info, num);
                    }
                    if (cUSBDeviceInfo.SerialNumber == _strDevConnectedSerialNumber)
                    {
                        flag = false;
                    }
                }
            }
        }

        public void MutipleConnectCam(List<string> DefaultDevice, Dictionary<string, int> dic) 
        {
            if (DefaultDevice == null || dic == null) { return; }
            foreach (string device in DefaultDevice) 
            {
                CCameraTool CamTool = new CCameraTool();
                var index = dic.Where(x => x.Key == device).Select(x => x.Value).FirstOrDefault();
                CamTool.SelectDevice(index -1);
                CamTool.OpenDevice(1u, 0);
                _nDevConnectedIdx = index;
                CDeviceInfo connectedDeviceInfo = CamTool.ConnectedDeviceInfo;
                if (connectedDeviceInfo.TLayerType == MVD_TRANSFER_LAYER_TYPE.MVD_GIGE_DEVICE)
                {
                    CGigeDeviceInfo cGigeDeviceInfo = connectedDeviceInfo as CGigeDeviceInfo;
                    _strDevConnectedSerialNumber = cGigeDeviceInfo.SerialNumber;
                }
                else if (connectedDeviceInfo.TLayerType == MVD_TRANSFER_LAYER_TYPE.MVD_USB_DEVICE)
                {
                    CUSBDeviceInfo cUSBDeviceInfo = connectedDeviceInfo as CUSBDeviceInfo;
                    _strDevConnectedSerialNumber = cUSBDeviceInfo.SerialNumber;
                }
                ViewHome.Instance.UpdateNotification("Successfully " + device + " device.");
                cs.Add(CamTool);
            }
        }

        public void MutipleReadImage() 
        {
            images = new List<CMvdImage>();
            foreach (var item in cs)
            {
                item.StartGrab();
                CMvdImage cFrameImage = new CMvdImage();
                item.CameraGrabResult.GetOneFrameTimeout(ref cFrameImage);
                images.Add(cFrameImage);
                item.StopGrab();
            }
        }
    }
}
