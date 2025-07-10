using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_VisionMaster.Enums
{
    public class LogEnum
    {
        public enum LogType
        {
            Info,
            Warning,
            Error,
            Debug
        }
        public enum LogLevel
        {
            Low,
            Medium,
            High
        }
        public enum LogSource
        {
            Application,
            System,
            Security,
            Custom
        }
        public enum LogDestination
        {
            Console,
            File,
            Database,
            RemoteServer
        }
        public enum LogAction
        {
            Create,
            Read,
            Update,
            Delete
        }
        public enum LogStatus
        {
            Success,
            Failure,
            Pending
        }
        public enum LogCategory
        {
            Performance,
            Security,
            Application,
            System
        }
        public enum LogPriority
        {
            Low,
            Normal,
            High,
            Critical
        }
     
    }
}
