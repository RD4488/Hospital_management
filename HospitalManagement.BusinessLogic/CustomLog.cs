using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.BusinessLogic
{
    public static class CustomLog
    {
        public static void WriteDataInTextFile(this string message)
        {
            string fileName = @$"D:\DemoGitLab\HotelManagement\{DateTime.UtcNow.ToString("d")}_Logs.txt";
            File.AppendAllText(fileName, message + Environment.NewLine);
        }
    }
}
