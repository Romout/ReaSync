using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReaSync
{
    public static class PathExtended
    {
        private const int FILE_ATTRIBUTE_DIRECTORY = 0x10;
        private const int FILE_ATTRIBUTE_NORMAL = 0x80;
        private const int MaximumPath = 260;

        public static string GetRelativePath(string fromPath, string toPath)
        {
            var fromAttribute = GetPathAttribute(fromPath);
            var toAttribute = GetPathAttribute(toPath);

            var stringBuilder = new StringBuilder(MaximumPath);
            if (PathRelativePathTo(
                stringBuilder,
                fromPath,
                fromAttribute,
                toPath,
                toAttribute) == 0)
            {
                throw new ArgumentException("Paths must have a common prefix.");
            }

            return stringBuilder.ToString();
        }

        private static int GetPathAttribute(string path)
        {
            var directory = new DirectoryInfo(path);
            if (directory.Exists)
            {
                return FILE_ATTRIBUTE_DIRECTORY;
            }

            var file = new FileInfo(path);
            if (file.Exists)
            {
                return FILE_ATTRIBUTE_NORMAL;
            }

            throw new FileNotFoundException(
                "A file or directory with the specified path was not found.",
                path);
        }

        [DllImport("shlwapi.dll", SetLastError = true)]
        private static extern int PathRelativePathTo(
            StringBuilder pszPath,
            string pszFrom,
            int dwAttrFrom,
            string pszTo,
            int dwAttrTo);
    }
}
