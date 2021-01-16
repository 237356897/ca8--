using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Motion.Enginee;
using System.Runtime.InteropServices;
using System.IO;

namespace Desay
{
    public class CCIni
    {
        public static void FFWrite(string sectionName, string key, object value, string filepath)
        {
            CheckDirectory(filepath);
            WritePrivateProfileString(sectionName, key, value.ToString(), filepath);
        }

        public static bool FFRead(string sectionName, string key, string filepath, out string readstr)
        {
            bool res = false;
            readstr = "";
            CheckDirectory(filepath);
            try
            {
                if (File.Exists(filepath))
                {
                    readstr = GetString(filepath, sectionName, key);
                    if (readstr.Count() > 0)
                    {
                        res = true;
                    }
                }
                return res;
            }
            catch (Exception)
            {
                return false;
            }
        }




        private static void CheckDirectory(string filePath)
        {
            String fileDirectory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(fileDirectory)) Directory.CreateDirectory(fileDirectory);

            if (!File.Exists(filePath))
            {
                WritePrivateProfileString("TEST", "T1", "TEST", filePath);
            }


        }
        private static string GetString(String filePath, String sectionName, String key)
        {
            StringBuilder destinationBuffer = new StringBuilder(512);
            int lenght = 0;
            lenght = GetPrivateProfileString(sectionName, key, String.Empty, destinationBuffer, destinationBuffer.Capacity, filePath);
            return lenght == 0 ? string.Empty : destinationBuffer.ToString(0, lenght);
        }
        [DllImport(dllName, CharSet = CharSet.Unicode, EntryPoint = "WritePrivateProfileString")]
        private static extern Int32 WritePrivateProfileString(String sectionName, String keyName, String addString, String fileName);
        [DllImport(dllName, CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileString")]
        private static extern Int32 GetPrivateProfileString(String sectionName, String keyName, String defaultString, StringBuilder destinationBuffer, Int32 destinationBufferSize, String filename);
        private const string dllName = "kernel32";
    }
    public class Delay
    {
        public static Delay Instance = new Delay();

        private Delay() { }

        public CylinderDelay PositioningDelay = new CylinderDelay() { OriginTime = 0, MoveTime = 0, AlarmTime = 2000 };
        public CylinderDelay OpenClampDelay = new CylinderDelay() { OriginTime = 0, MoveTime = 0, AlarmTime = 2000 };
        public CylinderDelay TranslationDelay = new CylinderDelay() { OriginTime = 0, MoveTime = 0, AlarmTime = 2000 };
    }
}
