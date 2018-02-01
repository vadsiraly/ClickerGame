using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ClickerEngine.PowerNames
{
    public static class PowerNamer
    {
        private static string[] PowerNames;

        static PowerNamer()
        {
            string file;
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("ClickerEngine.Resources.power_names.txt"))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    file = reader.ReadToEnd();
                }
            }

            PowerNames = file.Replace("\r\n", "\n").Split('\n');
        }

        public static string GetName(int power)
        {
            if (power < 3) return "";
            int i = (power / 3) - 1;
            return PowerNames[i];
        }
    }
}
