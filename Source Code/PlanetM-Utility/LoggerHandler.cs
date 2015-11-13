using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace PlanetM_Utility
{
    public static class LogWrapper
    {
        public static void LogError(Exception ex)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            Logger.Write(string.Format("{0}: {1}.{2} ErrorMessage: {3} StackTrace: {4} Source: {5}", ((System.Reflection.MemberInfo)(methodBase)).MemberType, (((System.Reflection.MemberInfo)(methodBase)).DeclaringType).FullName, methodBase.Name, ex.Message, ex.StackTrace, ex.Source), "ERROR");
            if (ex.InnerException != null)
                Logger.Write(string.Format("InnerException Details :: ErrorMessage: {0} StackTrace: {1} Source: {2}", ex.InnerException.Message, ex.InnerException.StackTrace, ex.InnerException.Source), "ERROR");
        }
        public static void LogError(string strErrorMsg)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            Logger.Write(string.Format("{0}: {1}.{2} ErrorMessage: {3}", ((System.Reflection.MemberInfo)(methodBase)).MemberType, (((System.Reflection.MemberInfo)(methodBase)).DeclaringType).FullName, methodBase.Name, strErrorMsg), "ERROR");
        }
        public static void LogInfo(string info)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            Logger.Write(string.Format("{0}: {1}.{2} Message: {3}", ((System.Reflection.MemberInfo)(methodBase)).MemberType, (((System.Reflection.MemberInfo)(methodBase)).DeclaringType).FullName, methodBase.Name, info), "INFO");
        }
        public static void LogWarning(string warning)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            Logger.Write(string.Format("{0}: {1}.{2} Message: {3}", ((System.Reflection.MemberInfo)(methodBase)).MemberType, (((System.Reflection.MemberInfo)(methodBase)).DeclaringType).FullName, methodBase.Name, warning), "WARNING");
        }

        public static void LogApplicationDetails()
        {
            Logger.Write(string.Format("Starting the Application : {0} - Version : {1} , Application Executable : {2}", Application.ProductName, Application.ProductVersion, Application.ExecutablePath), "INFO");
            Logger.Write(string.Format("Machine Name : {0}, User Name : {1}, User Domain Name : {2}, OS Version : {3}, Processor Count : {4}", Environment.MachineName, Environment.UserName, Environment.UserDomainName, Environment.OSVersion, Environment.ProcessorCount), "INFO");

            StringBuilder sbConnDetails = new StringBuilder();
            if (System.Configuration.ConfigurationManager.ConnectionStrings["PlanetM"] != null)
                sbConnDetails.Append("ConnectionString : " + System.Configuration.ConfigurationManager.ConnectionStrings["PlanetM"].ConnectionString);
            else
                sbConnDetails.Append("ConnectionString : " + Configuration.ReadConfig("ConnectionString"));
            sbConnDetails.Append(" DBSource : " + Configuration.ReadConfig("DBSource") + " DBFilePath : " + Configuration.ReadConfig("DBFilePath") + " DBFileName : " + Configuration.ReadConfig("DBFileName"));

            Logger.Write(string.Format("Connection Details :: {0}", sbConnDetails), "INFO");
        }
    }
}
