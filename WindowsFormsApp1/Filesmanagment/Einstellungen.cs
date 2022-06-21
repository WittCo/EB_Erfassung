using System.IO;
using System.Windows.Forms;

namespace WittEyE.Filesmanagment
{
    public static class Einstellungen
    {
        private readonly static string configFilePath = @"config\config.txt".GetAbsolutePath();
        private static string[] stringlist;

        public static void ReadFromFile()
        {
            if (File.Exists(configFilePath))
            {
                stringlist = File.ReadAllLines(configFilePath);
            }
            else
            {
                MessageBox.Show("Konfigurationsdatei könnte nicht gefunden werden");
            }
        }

        private static void WriteToFile()
        {
            File.WriteAllLines(configFilePath, stringlist);
        }

        public static bool updateConfigFile(this string updatedValue, int index)
        {
            if (File.Exists(configFilePath))
            {
                stringlist[index] = updatedValue;
                WriteToFile();
                return true;
            }
            else
            {
                MessageBox.Show("Konfigurationsdatei wurde gelöscht");
                return false;
            }
        }

        public static void updateUI(out string BildOrdner, out string EBOrdner, out string ExcelOrdner)
        {
            BildOrdner = stringlist[0];
            EBOrdner = stringlist[1];
            ExcelOrdner = stringlist[2];
        }

    }
}
