using System.IO;
using WindowsFormsApp1;

namespace WittEyE.Filesmanagment
{
    static class FilesUtlity
    {
        static string AbsolutePath = null;
        public static string GetAbsolutePath(this string relativePath)
        {
            if (AbsolutePath == null)
            {
                FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
                AbsolutePath = _dataRoot.Directory.FullName + @"\..\..\";
            }
            return Path.Combine(AbsolutePath, relativePath);
        }
    }
}
