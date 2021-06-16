
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; // for Marshal
using NewElectronicTechnology.SynView;
using System.IO;

namespace WindowsFormsApp1
{
    public class CCamera3
    {
        private const int NumberOfBuffers = 10;
        private LvSystem     m_pSystem;
        private LvInterface  m_pInterface;
        private LvDevice     m_pDevice;
        private LvStream     m_pStream;
        private LvRenderer   m_pRenderer;
        private LvBuffer[]   m_Buffers;
        private IntPtr       m_hDisplayWnd;
        private LvEvent      m_pEvent;
        private bool         m_bDoProcessing;

        //-----------------------------------------------------------------------------
        // CCamera constructor

        public CCamera3()
        { 
            m_pSystem     = null;
            m_pInterface  = null;
            m_pDevice     = null;
            m_pStream     = null;
            m_pRenderer   = null;
            m_pEvent      = null;
            m_Buffers     = new LvBuffer[NumberOfBuffers]; 
            for (int i=0; i<NumberOfBuffers; i++)
                m_Buffers[i] = null;

            m_hDisplayWnd = (IntPtr) 0;
            m_bDoProcessing = false;
        }

        //-----------------------------------------------------------------------------

        public void OpenCamera(IntPtr hDisplayWnd, LvSystem pSystem)
        {
            try
            {
                if (m_pDevice != null) CloseCamera();

                m_pSystem = pSystem;
                m_hDisplayWnd = hDisplayWnd;

                LvInterface pInterface = null;
                LvDevice pDevice = null;
                pSystem.UpdateInterfaceList();

                pSystem.OpenInterface("GigE Interface", ref pInterface);

                pInterface.UpdateDeviceList();
                pInterface.OpenDevice("SVGTL:80-6C-BC-30-21-AB", ref pDevice, LvDeviceAccess.Control);

                // The #error line below is intentionally inserted to the code in case you
                // generate the code from streamable or all writable features.
                // #error Review the feature settings code and remove the unnecessary items!
                // Before removing this line from the code, go carefully through all the feature
                // settings below and leave there only those, which really need to be set.
                LvDeviceFtr Ftr_DeviceSensorClock = 0;
                LvDeviceFtr Ftr_DeviceInitialDelay = 0;
                LvDeviceFtr Ftr_Binning = 0;
                LvDeviceFtr Ftr_RawBayerByp = 0;
                LvDeviceFtr Ftr_TestPatternGeneratorSelector = 0;
                LvDeviceFtr Ftr_TestPattern = 0;
                LvDeviceFtr Ftr_FrameBufferEnable = 0;
                LvDeviceFtr Ftr_ShutterMode = 0;
                LvDeviceFtr Ftr_TriggerLength = 0;
                LvDeviceFtr Ftr_Strobe1Source = 0;
                LvDeviceFtr Ftr_Strobe1Output = 0;
                LvDeviceFtr Ftr_Strobe1Invert = 0;
                LvDeviceFtr Ftr_Strobe1Delay = 0;
                LvDeviceFtr Ftr_Strobe1Length = 0;
                LvDeviceFtr Ftr_Strobe2Source = 0;
                LvDeviceFtr Ftr_Strobe2Output = 0;
                LvDeviceFtr Ftr_Strobe2Invert = 0;
                LvDeviceFtr Ftr_Strobe2Delay = 0;
                LvDeviceFtr Ftr_Strobe2Length = 0;
                LvDeviceFtr Ftr_AeTarget = 0;
                LvDeviceFtr Ftr_AeMinTime = 0;
                LvDeviceFtr Ftr_AeMaxTime = 0;
                LvDeviceFtr Ftr_AeSpeed = 0;
                LvDeviceFtr Ftr_AeWindow = 0;
                LvDeviceFtr Ftr_UserEepromDataSelector = 0;
                LvDeviceFtr Ftr_TransferRequestMode = 0;
                LvDeviceFtr Ftr_ExpTogEnable = 0;
                LvDeviceFtr Ftr_ExpTogExposureTime = 0;
                LvDeviceFtr Ftr_RoiTogEnable = 0;
                LvDeviceFtr Ftr_BccEnable = 0;

                // --- Device Control ---
                pDevice.SetEnum(LvDeviceFtr.DeviceScanType, (UInt32)LvDeviceScanType.Areascan);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "DeviceSensorClock", ref Ftr_DeviceSensorClock);
                pDevice.SetInt(Ftr_DeviceSensorClock, 26);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "DeviceInitialDelay", ref Ftr_DeviceInitialDelay);
                pDevice.SetInt(Ftr_DeviceInitialDelay, 100);
                // --- Image Format Control ---
                pDevice.SetEnum(LvDeviceFtr.RegionSelector, (UInt32)LvRegionSelector.Region0);
                pDevice.SetInt(LvDeviceFtr.Width, 3664);
                pDevice.SetInt(LvDeviceFtr.Height, 2748);
                pDevice.SetInt(LvDeviceFtr.OffsetX, 0);
                pDevice.SetInt(LvDeviceFtr.OffsetY, 0);
                pDevice.SetEnum(LvDeviceFtr.RegionSelector, (UInt32)LvRegionSelector.Region0);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Binning", ref Ftr_Binning);
                pDevice.SetEnumStr(Ftr_Binning, "x1");
                pDevice.SetBool(LvDeviceFtr.ReverseX, false);
                pDevice.SetBool(LvDeviceFtr.ReverseY, false);
                pDevice.SetEnum(LvDeviceFtr.PixelFormat, (UInt32)LvPixelFormat.BayerGR8);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "RawBayerByp", ref Ftr_RawBayerByp);
                pDevice.SetBool(Ftr_RawBayerByp, false);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "TestPatternGeneratorSelector", ref Ftr_TestPatternGeneratorSelector);
                pDevice.SetEnumStr(Ftr_TestPatternGeneratorSelector, "Sensor");
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "TestPattern", ref Ftr_TestPattern);
                pDevice.SetInt(Ftr_TestPattern, 0);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "TestPatternGeneratorSelector", ref Ftr_TestPatternGeneratorSelector);
                pDevice.SetEnumStr(Ftr_TestPatternGeneratorSelector, "FPGA");
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "TestPattern", ref Ftr_TestPattern);
                pDevice.SetInt(Ftr_TestPattern, 0);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "TestPatternGeneratorSelector", ref Ftr_TestPatternGeneratorSelector);
                pDevice.SetEnumStr(Ftr_TestPatternGeneratorSelector, "FPGA");
                // --- Acquisition ---
                pDevice.SetEnum(LvDeviceFtr.AcquisitionMode, (UInt32)LvAcquisitionMode.Continuous);
                pDevice.SetInt(LvDeviceFtr.AcquisitionFrameCount, 8);
                pDevice.SetInt(LvDeviceFtr.AcquisitionBurstFrameCount, 4);
                pDevice.SetFloat(LvDeviceFtr.AcquisitionFrameRate, 0.000000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "FrameBufferEnable", ref Ftr_FrameBufferEnable);
                pDevice.SetBool(Ftr_FrameBufferEnable, false);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "ShutterMode", ref Ftr_ShutterMode);
                pDevice.SetEnumStr(Ftr_ShutterMode, "RollingShutter");
                // --- Trigger ---
                pDevice.SetEnum(LvDeviceFtr.TriggerSelector, (UInt32)LvTriggerSelector.FrameStart);
                pDevice.SetEnum(LvDeviceFtr.TriggerMode, (UInt32)LvTriggerMode.On);
                pDevice.SetEnum(LvDeviceFtr.TriggerSource, (UInt32)LvTriggerSource.Software);
                pDevice.SetEnum(LvDeviceFtr.TriggerActivation, (UInt32)LvTriggerActivation.RisingEdge);
                pDevice.SetFloat(LvDeviceFtr.TriggerDelay, 0.000000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "TriggerLength", ref Ftr_TriggerLength);
                pDevice.SetFloat(Ftr_TriggerLength, 4.000000);
                pDevice.SetEnumStr(LvDeviceFtr.TriggerDivider, "SubsamplingOff");
                pDevice.SetEnum(LvDeviceFtr.TriggerSelector, (UInt32)LvTriggerSelector.FrameBurstStart);
                pDevice.SetEnum(LvDeviceFtr.TriggerMode, (UInt32)LvTriggerMode.Off);
                pDevice.SetEnum(LvDeviceFtr.TriggerSource, (UInt32)LvTriggerSource.Software);
                pDevice.SetEnum(LvDeviceFtr.TriggerActivation, (UInt32)LvTriggerActivation.RisingEdge);
                pDevice.SetFloat(LvDeviceFtr.TriggerDelay, 0.000000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "TriggerLength", ref Ftr_TriggerLength);
                pDevice.SetFloat(Ftr_TriggerLength, 4.000000);
                pDevice.SetEnum(LvDeviceFtr.TriggerSelector, (UInt32)LvTriggerSelector.FrameBurstStart);
                // --- Strobe 1 ---
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe1Source", ref Ftr_Strobe1Source);
                pDevice.SetEnumStr(Ftr_Strobe1Source, "Trigger");
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe1Output", ref Ftr_Strobe1Output);
                pDevice.SetEnumStr(Ftr_Strobe1Output, "Pulse");
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe1Invert", ref Ftr_Strobe1Invert);
                pDevice.SetBool(Ftr_Strobe1Invert, false);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe1Delay", ref Ftr_Strobe1Delay);
                pDevice.SetFloat(Ftr_Strobe1Delay, 4.000000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe1Length", ref Ftr_Strobe1Length);
                pDevice.SetFloat(Ftr_Strobe1Length, 1000.000000);
                // --- Strobe 2 ---
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe2Source", ref Ftr_Strobe2Source);
                pDevice.SetEnumStr(Ftr_Strobe2Source, "Exposure");
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe2Output", ref Ftr_Strobe2Output);
                pDevice.SetEnumStr(Ftr_Strobe2Output, "Static");
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe2Invert", ref Ftr_Strobe2Invert);
                pDevice.SetBool(Ftr_Strobe2Invert, true);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe2Delay", ref Ftr_Strobe2Delay);
                pDevice.SetFloat(Ftr_Strobe2Delay, 4.000000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "Strobe2Length", ref Ftr_Strobe2Length);
                pDevice.SetFloat(Ftr_Strobe2Length, 1000.000000);
                // --- Exposure ---
                pDevice.SetEnum(LvDeviceFtr.ExposureMode, (UInt32)LvExposureMode.Timed);
                pDevice.SetFloat(LvDeviceFtr.ExposureTime, 18000);
                pDevice.SetEnum(LvDeviceFtr.ExposureAuto, (UInt32)LvExposureAuto.Off);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "AeTarget", ref Ftr_AeTarget);
                pDevice.SetFloat(Ftr_AeTarget, 0.300000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "AeMinTime", ref Ftr_AeMinTime);
                pDevice.SetFloat(Ftr_AeMinTime, 10.000000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "AeMaxTime", ref Ftr_AeMaxTime);
                pDevice.SetFloat(Ftr_AeMaxTime, 1000000.000000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "AeSpeed", ref Ftr_AeSpeed);
                pDevice.SetFloat(Ftr_AeSpeed, 3.000000);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "AeWindow", ref Ftr_AeWindow);
                pDevice.SetEnumStr(Ftr_AeWindow, "Full");
                // --- Digital I/O Control ---
                pDevice.SetEnum(LvDeviceFtr.LineSelector, (UInt32)LvLineSelector.Line1);
                pDevice.SetBool(LvDeviceFtr.LineInverter, false);
                pDevice.SetEnum(LvDeviceFtr.LineSelector, (UInt32)LvLineSelector.Line2);
                pDevice.SetBool(LvDeviceFtr.LineInverter, false);
                pDevice.SetEnum(LvDeviceFtr.LineSelector, (UInt32)LvLineSelector.Line3);
                pDevice.SetBool(LvDeviceFtr.LineInverter, false);
                pDevice.SetEnum(LvDeviceFtr.LineSource, (UInt32)LvLineSource.Off);
                pDevice.SetEnum(LvDeviceFtr.LineSelector, (UInt32)LvLineSelector.Line4);
                pDevice.SetBool(LvDeviceFtr.LineInverter, false);
                pDevice.SetEnum(LvDeviceFtr.LineSource, (UInt32)LvLineSource.Off);
                pDevice.SetEnum(LvDeviceFtr.LineSelector, (UInt32)LvLineSelector.Line4);
                pDevice.SetEnum(LvDeviceFtr.UserOutputSelector, (UInt32)LvUserOutputSelector.UserOutput1);
                pDevice.SetBool(LvDeviceFtr.UserOutputValue, false);
                pDevice.SetEnum(LvDeviceFtr.UserOutputSelector, (UInt32)LvUserOutputSelector.UserOutput2);
                pDevice.SetBool(LvDeviceFtr.UserOutputValue, false);
                pDevice.SetEnum(LvDeviceFtr.UserOutputSelector, (UInt32)LvUserOutputSelector.UserOutput3);
                pDevice.SetBool(LvDeviceFtr.UserOutputValue, false);
                pDevice.SetEnum(LvDeviceFtr.UserOutputSelector, (UInt32)LvUserOutputSelector.UserOutput4);
                pDevice.SetBool(LvDeviceFtr.UserOutputValue, false);
                pDevice.SetEnum(LvDeviceFtr.UserOutputSelector, (UInt32)LvUserOutputSelector.UserOutput4);
                pDevice.SetInt(LvDeviceFtr.UserOutputValueAll, 0x0);
                // --- Analog Control ---
                pDevice.SetEnum(LvDeviceFtr.GainSelector, (UInt32)LvGainSelector.AnalogAll);
                pDevice.SetFloat(LvDeviceFtr.Gain, 1.500000);
                pDevice.SetEnum(LvDeviceFtr.GainSelector, (UInt32)LvGainSelector.DigitalAll);
                pDevice.SetFloat(LvDeviceFtr.Gain, 1.000000);
                pDevice.SetEnumStr(LvDeviceFtr.GainSelector, "DigitalRed");
                pDevice.SetFloat(LvDeviceFtr.Gain, 1.000000);
                pDevice.SetEnumStr(LvDeviceFtr.GainSelector, "DigitalGreen");
                pDevice.SetFloat(LvDeviceFtr.Gain, 1.000000);
                pDevice.SetEnumStr(LvDeviceFtr.GainSelector, "DigitalBlue");
                pDevice.SetFloat(LvDeviceFtr.Gain, 1.000000);
                pDevice.SetEnumStr(LvDeviceFtr.GainSelector, "DigitalGreen2Corr");
                pDevice.SetFloat(LvDeviceFtr.Gain, 1.000000);
                pDevice.SetEnumStr(LvDeviceFtr.GainSelector, "DigitalGreen2Corr");
                pDevice.SetFloat(LvDeviceFtr.BlackLevel, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.BalanceRatioSelector, (UInt32)LvBalanceRatioSelector.Red);
                pDevice.SetFloat(LvDeviceFtr.BalanceRatio, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.BalanceRatioSelector, (UInt32)LvBalanceRatioSelector.Green);
                pDevice.SetFloat(LvDeviceFtr.BalanceRatio, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.BalanceRatioSelector, (UInt32)LvBalanceRatioSelector.Blue);
                pDevice.SetFloat(LvDeviceFtr.BalanceRatio, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.BalanceRatioSelector, (UInt32)LvBalanceRatioSelector.Blue);
                pDevice.SetEnum(LvDeviceFtr.BalanceWhiteAuto, (UInt32)LvBalanceWhiteAuto.Off);
                pDevice.SetFloat(LvDeviceFtr.Gamma, 0.500000);
                // --- LUT Control ---
                pDevice.SetEnum(LvDeviceFtr.LUTSelector, (UInt32)LvLUTSelector.Red);
                pDevice.SetBool(LvDeviceFtr.LUTEnable, false);
                // --- Network Interface ---
                pDevice.SetInt(LvDeviceFtr.GevInterfaceSelector, 0);
                pDevice.SetBool(LvDeviceFtr.GevCurrentIPConfigurationDHCP, true);
                pDevice.SetBool(LvDeviceFtr.GevCurrentIPConfigurationPersistentIP, false);
                pDevice.SetInt(LvDeviceFtr.GevInterfaceSelector, 0);
                // GevSupportedOptionSelector has 27 entries, the source code above shown for first 20 only.
                pDevice.SetEnum(LvDeviceFtr.GevSupportedOptionSelector, (UInt32)LvGevSupportedOptionSelector.UserDefinedName);
                // --- Stream Channel ---
                pDevice.SetInt(LvDeviceFtr.GevStreamChannelSelector, 0);
                pDevice.SetInt(LvDeviceFtr.GevSCPSPacketSize, 500);
                pDevice.SetInt(LvDeviceFtr.GevSCPD, 20000);
           
                // --- User Set Control ---
                pDevice.SetEnum(LvDeviceFtr.UserSetSelector, (UInt32)LvUserSetSelector.Default);
                // --- Color Transformation Control ---
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationSelector, (UInt32)LvColorTransformationSelector.RGBtoRGB);
                pDevice.SetBool(LvDeviceFtr.ColorTransformationEnable, true);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain00);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain01);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain02);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain10);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain11);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain12);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain20);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain21);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain22);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Offset0);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Offset1);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Offset2);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Offset2);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationSelector, (UInt32)LvColorTransformationSelector.RGBtoYUV);
                pDevice.SetBool(LvDeviceFtr.ColorTransformationEnable, true);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain00);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain01);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain02);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain10);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain11);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain12);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain20);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain21);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Gain22);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Offset0);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Offset1);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Offset2);
                pDevice.SetFloat(LvDeviceFtr.ColorTransformationValue, 0.000000);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationValueSelector, (UInt32)LvColorTransformationValueSelector.Offset2);
                pDevice.SetEnum(LvDeviceFtr.ColorTransformationSelector, (UInt32)LvColorTransformationSelector.RGBtoYUV);
                // --- Event Control ---
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "AcquisitionStart");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "AcquisitionEnd");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "FrameTrigger");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "Line1RisingEdge");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "Line2RisingEdge");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "Line1FallingEdge");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "Line2FallingEdge");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "Ping");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "Error");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnumStr(LvDeviceFtr.EventSelector, "CustomModule");
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnum(LvDeviceFtr.EventSelector, (UInt32)LvEventSelector.LvLog);
                pDevice.SetEnum(LvDeviceFtr.EventNotification, (UInt32)LvEventNotification.Off);
                pDevice.SetEnum(LvDeviceFtr.EventSelector, (UInt32)LvEventSelector.LvLog);
                // --- User Eeprom Data ---
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "UserEepromDataSelector", ref Ftr_UserEepromDataSelector);
                pDevice.SetInt(Ftr_UserEepromDataSelector, 0);
                // --- Transfer Request Control ---
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "TransferRequestMode", ref Ftr_TransferRequestMode);
                pDevice.SetBool(Ftr_TransferRequestMode, false);
                // --- Exposure Toggle ---
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "ExpTogEnable", ref Ftr_ExpTogEnable);
                pDevice.SetBool(Ftr_ExpTogEnable, false);
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "ExpTogExposureTime", ref Ftr_ExpTogExposureTime);
                pDevice.SetFloat(Ftr_ExpTogExposureTime, 100.000000);
                // --- ROI Toggle ---
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "RoiTogEnable", ref Ftr_RoiTogEnable);
                pDevice.SetBool(Ftr_RoiTogEnable, false);
                // --- BCC Control ---
                pDevice.GetFeatureByName(LvFtrGroup.DeviceRemote, "BccEnable", ref Ftr_BccEnable);
                pDevice.SetBool(Ftr_BccEnable, true);
                // --- GenTL Device Module ---
                pDevice.SetEnum(LvDeviceFtr.LvGevDeviceStreamCaptureMode, (UInt32)LvGevDeviceStreamCaptureMode.SystemDefault);
                pDevice.SetInt(LvDeviceFtr.StreamSelector, 0);
                // --- Unified Processing ---
                pDevice.SetEnum(LvDeviceFtr.LvUniProcessMode, (UInt32)LvUniProcessMode.Auto);
                pDevice.SetBool(LvDeviceFtr.LvUniProcessEnableInPlace, false);
                pDevice.SetEnum(LvDeviceFtr.LvUniPixelFormat, (UInt32)LvPixelFormat.BGRA8);
                pDevice.SetEnum(LvDeviceFtr.LvUniBayerDecoderAlgorithm, (UInt32)LvBayerDecoderAlgorithm.BilinearInterpolation);
                pDevice.SetEnum(LvDeviceFtr.LvUniProcessExecution, (UInt32)LvUniProcessExecution.OnBufferPtrQuery);
                // --- LUT Control and White Balance ---
                pDevice.SetEnum(LvDeviceFtr.LvUniLUTMode, (UInt32)LvUniLUTMode.Generated);
                pDevice.SetEnum(LvDeviceFtr.LvUniLUTSelector, (UInt32)LvUniLUTSelector.Red);
                pDevice.SetInt(LvDeviceFtr.LvUniLUTIndex, 0);
                pDevice.SetEnum(LvDeviceFtr.LvUniLUTSelector, (UInt32)LvUniLUTSelector.Green);
                pDevice.SetInt(LvDeviceFtr.LvUniLUTIndex, 0);
                pDevice.SetEnum(LvDeviceFtr.LvUniLUTSelector, (UInt32)LvUniLUTSelector.Blue);
                pDevice.SetInt(LvDeviceFtr.LvUniLUTIndex, 0);
                pDevice.SetEnum(LvDeviceFtr.LvUniLUTSelector, (UInt32)LvUniLUTSelector.Red);
                pDevice.SetFloat(LvDeviceFtr.LvUniBrightness, 1.000000);
                pDevice.SetFloat(LvDeviceFtr.LvUniContrast, 1.000000);
                pDevice.SetFloat(LvDeviceFtr.LvUniGamma, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.LvUniBalanceRatioSelector, (UInt32)LvUniBalanceRatioSelector.Red);
                pDevice.SetFloat(LvDeviceFtr.LvUniBalanceRatio, 1.119000);
                pDevice.SetEnum(LvDeviceFtr.LvUniBalanceRatioSelector, (UInt32)LvUniBalanceRatioSelector.Green);
                pDevice.SetFloat(LvDeviceFtr.LvUniBalanceRatio, 1.000000);
                pDevice.SetEnum(LvDeviceFtr.LvUniBalanceRatioSelector, (UInt32)LvUniBalanceRatioSelector.Blue);
                pDevice.SetFloat(LvDeviceFtr.LvUniBalanceRatio, 1.202000);
                pDevice.SetEnum(LvDeviceFtr.LvUniBalanceRatioSelector, (UInt32)LvUniBalanceRatioSelector.Red);
                pDevice.SetEnum(LvDeviceFtr.LvUniBalanceWhiteAuto, (UInt32)LvUniBalanceWhiteAuto.Off);
                // --- Color Transformation Control ---
                pDevice.SetEnum(LvDeviceFtr.LvUniColorTransformationMode, (UInt32)LvUniColorTransformationMode.Generated);
                pDevice.SetEnum(LvDeviceFtr.LvUniColorTransformationSelector, (UInt32)LvUniColorTransformationSelector.RGBtoRGB);
                pDevice.SetEnum(LvDeviceFtr.LvUniColorTransformationValueSelector, (UInt32)LvUniColorTransformationValueSelector.Gain00);
                pDevice.SetEnum(LvDeviceFtr.LvUniColorTransformationSelector, (UInt32)LvUniColorTransformationSelector.RGBtoRGB);
                pDevice.SetFloat(LvDeviceFtr.LvUniSaturation, 1.000000);

                m_pInterface = pInterface;
                m_pDevice = pDevice;

                m_pDevice.SetEnum(LvDeviceFtr.LvUniProcessMode, (int)LvUniProcessMode.Auto);
                SetOptimalUniPixelFormat();

                m_pDevice.OpenStream("", ref m_pStream);
                m_pStream.OpenEvent(LvEventType.NewBuffer, ref m_pEvent);
                for (int i = 0; i < NumberOfBuffers; i++)
                    m_pStream.OpenBuffer((IntPtr)0, 0, (IntPtr)0, 0, ref m_Buffers[i]);
                m_pStream.SetInt32(LvStreamFtr.LvPostponeQueueBuffers, 3);

               
                m_pStream.OpenRenderer(ref m_pRenderer);
                m_pRenderer.SetEnum(LvRendererFtr.LvRenderType, (UInt32)LvRenderType.ScaleToFit);
                m_pRenderer.SetWindow(m_hDisplayWnd);
          

                m_pEvent.OnEventNewBuffer += new LvEventNewBufferHandler(NewBufferEventHandler);
                m_pEvent.SetCallbackNewBuffer(true, (IntPtr)0);
                m_pEvent.StartThread();
            }
            catch (LvException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        //-----------------------------------------------------------------------------
        // Starts acquisition

        public void StartAcquisition()
        {
            try
            {
                m_pStream.FlushQueue(LvQueueOperation.AllToInput);
                if (m_pDevice == null) return;
                m_pDevice.AcquisitionStart();                             
            }
            catch (LvException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //-----------------------------------------------------------------------------
        // Stops acquisition

        public void StopAcquisition()
        {
            try
            {
                if (m_pStream == null) return;
                m_pDevice.AcquisitionStop();          
            }
            catch (LvException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void Triggr()
        {
            m_pDevice.CmdExecute(LvDeviceFtr.TriggerSoftware);

        }

        //-----------------------------------------------------------------------------
        // Closes the cameras

        public void CloseCamera()
        {
            try
            {
                if (m_pDevice == null) return;
                if (IsAcquiring()) StopAcquisition();
                m_pEvent.StopThread();                
                m_pEvent.SetCallbackNewBuffer(false, (IntPtr)0);      
                m_pStream.CloseEvent(ref m_pEvent);       
                m_pStream.CloseRenderer(ref m_pRenderer);
                m_pStream.FlushQueue(LvQueueOperation.AllDiscard);
                for (int i = 0; i < NumberOfBuffers; i++)
                    if (m_Buffers[i] != null)
                        m_pStream.CloseBuffer(ref m_Buffers[i]);
                m_pDevice.CloseStream(ref m_pStream);     
                m_pInterface.CloseDevice(ref m_pDevice); 
                m_pSystem.CloseInterface(ref m_pInterface); 
            }
            catch (LvException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //-----------------------------------------------------------------------------
        // Utility function for enabling/disabling buttons

        public bool IsOpen()
        {
            return m_pDevice != null;
        }

        //-----------------------------------------------------------------------------
        // Utility function for enabling/disabling buttons

        public bool IsAcquiring()
        {
            Boolean iIsAcquiring = false;
            try
            {
                if (m_pDevice == null) return false;
                m_pDevice.GetBool(LvDeviceFtr.LvDeviceIsAcquiring, ref iIsAcquiring);
            }
            catch (LvException)
            {
                // no message
            }
            return iIsAcquiring;
        }

        //-----------------------------------------------------------------------------
        // Switches ON/OFF image processing done in the callback

        public void SetProcessing(bool bDoProcessing)
        {
            m_bDoProcessing = bDoProcessing;
        }

        //-----------------------------------------------------------------------------
        // Determines optimal pixel format for the uni-process

        void SetOptimalUniPixelFormat()
        {
            UInt32 PixelFormat = (UInt32)LvPixelFormat.Mono8;
            m_pDevice.GetEnum(LvDeviceFtr.PixelFormat, ref PixelFormat);
            UInt32 UniPixelFormat = PixelFormat;
            switch ((LvPixelFormat)PixelFormat)
            {
                case LvPixelFormat.Mono8:
                case LvPixelFormat.Mono10:
                case LvPixelFormat.Mono12:
                case LvPixelFormat.Mono16:
                    UniPixelFormat = (UInt32)LvPixelFormat.Mono8;
                    break;

                case LvPixelFormat.BayerGR8:
                case LvPixelFormat.BayerRG8:
                case LvPixelFormat.BayerGB8:
                case LvPixelFormat.BayerBG8:
                case LvPixelFormat.BayerGR10:
                case LvPixelFormat.BayerRG10:
                case LvPixelFormat.BayerGB10:
                case LvPixelFormat.BayerBG10:
                case LvPixelFormat.BayerGR12:
                case LvPixelFormat.BayerRG12:
                case LvPixelFormat.BayerGB12:
                case LvPixelFormat.BayerBG12:
                case LvPixelFormat.RGB8Packed:
                case LvPixelFormat.RGBA8Packed:
                    UniPixelFormat = (UInt32)LvPixelFormat.BGRA8Packed;
                    break;
            }
            m_pDevice.SetEnum(LvDeviceFtr.LvUniPixelFormat, UniPixelFormat);
        }

        //-----------------------------------------------------------------------------

        public void Repaint()
        {
            try
            {
                if (m_pRenderer != null)
                    m_pRenderer.Repaint();
            }
            catch (LvException)
            {
                // no message
            }
        }

        //-----------------------------------------------------------------------------

        void NewBufferEventHandler(System.Object sender, LvNewBufferEventArgs e)
        {
            try
            {
                if (e.Buffer == null) return;

                IntPtr pData = (IntPtr) 0;
                Int64 iImageOffset = 0;
                e.Buffer.GetPtr(LvBufferFtr.UniBase, ref pData);
                e.Buffer.GetInt(LvBufferFtr.UniImageOffset, ref iImageOffset);
                pData = (IntPtr)(pData.ToInt64() + iImageOffset);
                if (m_bDoProcessing && pData != (IntPtr)0)
                {
                    // we will do some easy processing - invert the pixel values in an area
                    Int32 iWidth = 0;
                    Int32 iHeight = 0;
                    Int32 iLinePitch = 0;
                    UInt32 iPixelFormat = 0;    // LvPixelFormat enumeration value
                    m_pDevice.GetInt32(LvDeviceFtr.Width, ref iWidth);
                    m_pDevice.GetInt32(LvDeviceFtr.Height, ref iHeight);
                    m_pDevice.GetEnum(LvDeviceFtr.LvUniPixelFormat, ref iPixelFormat);
                    m_pDevice.GetInt32(LvDeviceFtr.LvUniLinePitch, ref iLinePitch);
                    Int32 iBytesPerPixel = (Int32)((iPixelFormat & 0x00FF0000) >> 16) / 8;
                    for (Int32 j = 0; j < (iHeight / 2); j++)
                    {
                        Int32 BaseOffset = (iHeight / 4 + j) * iLinePitch + (iWidth / 4) * iBytesPerPixel;
                        for (Int32 i = 0; i < (iBytesPerPixel*iWidth / 2); i += iBytesPerPixel)
                        {
                            for (Int32 k = 0; k < iBytesPerPixel; k++)
                            {
                                // In C# we can use the Marshal class to access data pointed 
                                // by unmanaged pointers. However, this way of accessing unmanaged 
                                // data is ineffective (may slow down the acquisition).
                                Byte Pixel = Marshal.ReadByte(pData, BaseOffset + i + k);
                                Pixel = (Byte)~Pixel;
                                Marshal.WriteByte(pData, BaseOffset + i + k, Pixel);
                            }
                        }
                    }
                }

                m_pRenderer.DisplayImage(e.Buffer);
                e.Buffer.Queue();
                SaveImg(e);
            }
            catch (LvException)
            {
                // no message
            }
        }

        public void SaveImg(LvNewBufferEventArgs e)
        {
            string name = DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss");

            string fileName = "HU" + name + ".jpg";
            string path = Path.Combine(Environment.CurrentDirectory + "//IBC/Technik - Witt IBC Bilder", fileName);

            e.Buffer.SaveImageToJpgFile(path, 100);

        
            //e.Buffer.SaveImageToBmpFile(@"C:\Users\Aufschrauberportal\AWICO\Technik - Witt IBC Bilder\HU" + name + ".bmp");
        }

        //-----------------------------------------------------------------------------


    }
}
