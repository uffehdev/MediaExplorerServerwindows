using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MediaExplorerServer
{
    class MediaFinder
    {
        public List<string> lstWithFiles;
        List<string> lstWithFilesTypes;

        public MediaFinder() {
            lstWithFiles = new List<string>();

            lstWithFilesTypes = new List<string>() { "*.avi", "*.mov", "*.mp4" };
        }

        public void FindMediaOnComputer(string[] HardDisks) {

            for (int i = 0; i < HardDisks.Length; i++)
            {
                string directoryToSearch = @"E:\HemLaddning\Download";//HardDisks[i];
                foreach (string s in lstWithFilesTypes)
                {
                    string[] files = Directory.GetFileSystemEntries(directoryToSearch, s);
                    for (int a = 0; a < files.Length; a++) lstWithFiles.Add(files[a]);
                }
            }

        }

    }
}
