using System;
using System.IO;

namespace AInfrastructure
{
    public class SystemFilePath
    {
        public static string UserFolder 
            => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary>
        /// Gets the Absolute path 
        /// </summary>
        /// <returns>The absloute path.</returns>
        /// <param name="relativePath">Relative path to user path.</param>
        public static string GetUsersPath(string relativePath) 
            => Path.Combine(UserFolder, relativePath);

        public static void CreateDirectory(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                var parentDirectory = fileInfo.Directory;
                var parentDirName = parentDirectory.FullName;

                if (!Directory.Exists(parentDirName))
                {
                    Directory.CreateDirectory(parentDirName);
                }

                FileStream stream = null;
                try
                {
                    stream = File.Create(filePath);
                }
                finally
                {
                    stream?.Close();
                }

            }
        }
    }
}
