using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;


namespace MediaExplorerServer
{
    class MediaFinder
    {
        public delegate void AddDelegate(string name);
        public AddDelegate adder;
        public delegate void StatusUpdate(string txt);
        public StatusUpdate statusUpt;
        string curHdd;

        public List<string> lstWithFiles;
        List<string> lstWithFilesTypes;

        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
        Thread searchThread;
        

        public MediaFinder() {
            lstWithFiles = new List<string>();

            lstWithFilesTypes = new List<string>() { ".avi", ".mov", ".mp4" };
        }

        public void EndSearch() {
            if (searchThread != null)
            {
                statusUpt("Search stopped");
                searchThread.Abort();
            }
        }

        public void StartSearch() {
            lstWithFiles.Clear();
            
            EndSearch();
            searchThread = new Thread(FindMediaOnComputer);
            searchThread.IsBackground = true;
            searchThread.Start();
            
        }

        public void FindMediaOnComputer() {
            string[] drives = System.Environment.GetLogicalDrives();
            //string[] drives = new string[] { @"C:\" };
            foreach (string dr in drives)
            {
                if (dr != "Y:\\")
                {
                    System.IO.DriveInfo di = new System.IO.DriveInfo(dr);
                    if (!di.IsReady)
                    {
                        Console.WriteLine("The drive {0} could not be read", di.Name);
                        continue;
                    }

                    System.IO.DirectoryInfo rootDir = di.RootDirectory;
                    curHdd = rootDir.ToString();
                    statusUpt("Searching hdd: " + rootDir);
                    SearchInDirTree(rootDir);
                }
            }

            Environment.SpecialFolder[] specDirs = new Environment.SpecialFolder[] { Environment.SpecialFolder.MyDocuments, Environment.SpecialFolder.MyMusic, Environment.SpecialFolder.MyPictures, Environment.SpecialFolder.MyVideos, Environment.SpecialFolder.Favorites, Environment.SpecialFolder.Desktop };
            foreach (Environment.SpecialFolder sd in specDirs)
            {
                statusUpt("Searching special: " + sd.ToString());
                SearchInDirTree(new System.IO.DirectoryInfo(Environment.GetFolderPath(sd)));
            }


            foreach (string s in log)
            {
                //Alla filer som är restricted
            }
            statusUpt("Search is Done!");
            searchThread.Abort();
        }

        void SearchInDirTree(System.IO.DirectoryInfo root)
        {
            statusUpt("Searching " + curHdd + ": " + root);
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    try
                    {
                        if (!lstWithFiles.Contains(fi.FullName))
                        {
                            if (lstWithFilesTypes.Contains(fi.Extension))
                            {
                                lstWithFiles.Add(fi.FullName);
                                adder(fi.FullName);
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

                subDirs = root.GetDirectories();
                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    if (dirInfo.Name == "Program Files")
                    {
                        bool a = false;
                    }
                    if (dirInfo.Name == "Program Files (x86)" || dirInfo.Name == "Program Files" || dirInfo.Name == "Windows")
                    {

                    }
                    else { SearchInDirTree(dirInfo); }
                }
            }
        }
    }
}
