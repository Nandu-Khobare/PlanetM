using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbstractLayer;
using System.Net.NetworkInformation;
using System.Management;

namespace PlanetM_Utility
{
    public class PCInfoRetriever
    {
        public bool GetPCInfo(out PCInfo pcInfo, out string error)
        {
            pcInfo = new PCInfo();
            error = string.Empty;
            bool success = false;

            try
            {
                ManagementObjectCollection moList = null;
                ManagementObjectSearcher mos = null;

                mos = new ManagementObjectSearcher("Select * From Win32_processor");
                moList = mos.Get();
                string cpuid = string.Empty;
                foreach (ManagementObject mo in moList)
                {
                    if (mo["ProcessorID"] != null)
                        cpuid = mo["ProcessorID"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(cpuid))
                    pcInfo.ProcessorID = cpuid;
                else
                    pcInfo.ProcessorID = "NA";

                mos = new ManagementObjectSearcher(@"SELECT * FROM Win32_LogicalDisk ");
                moList = mos.Get();
                string hdid = string.Empty;
                foreach (ManagementObject mo in moList)
                {
                    if (mo["DeviceID"] != null && mo["DeviceID"].ToString().Equals(System.IO.Path.GetPathRoot(Environment.SystemDirectory).Replace("\\",""), StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (mo["VolumeSerialNumber"] != null)
                            hdid = mo["VolumeSerialNumber"].ToString();
                    }
                }

                if (!string.IsNullOrWhiteSpace(hdid))
                    pcInfo.HardDiskID = hdid;
                else
                    pcInfo.HardDiskID = "NA";

                mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                moList = mos.Get();
                string serial = string.Empty;
                foreach (ManagementObject mo in moList)
                {
                    if (mo["SerialNumber"] != null)
                        serial = mo["SerialNumber"].ToString();
                }
                if (!string.IsNullOrWhiteSpace(serial))
                    pcInfo.MotherboardID = serial;
                else
                    pcInfo.MotherboardID = "NA";

                string macaddress = string.Empty;
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macaddress = nic.GetPhysicalAddress().ToString();
                        if (!string.IsNullOrWhiteSpace(macaddress))
                            break;
                    }
                }
                if (!string.IsNullOrWhiteSpace(macaddress))
                    pcInfo.MACID = macaddress;
                else
                    pcInfo.MACID = "NA";

                success = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                LogWrapper.LogError(ex);
            }

            return success;
        }
    }
}
