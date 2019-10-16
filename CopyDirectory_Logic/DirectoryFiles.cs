using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace CopyDirectory_Logic
{
    public static class DirectoryFiles
    {
       


        static Dictionary<string, string> FileFolder = new Dictionary<string, string>();
        static Dictionary<string, string> OnlyFiles = new Dictionary<string, string>();
        static Dictionary<string, string> OnlyFolder = new Dictionary<string, string>();
        static Dictionary<string, string> Subfolder = new Dictionary<string, string>();





        // List all folders and files of directory
        private static string[]  GetDirectoryandFiles(string _path, bool IsSourcePath) //DirectoryEnum _directoryenum)
        {
            string[] filefolderpath = new string[] { };
            try
            {
                
                switch (IsSourcePath)
                {
                    case true:
                        {
                            //Get all files and folder in path
                            filefolderpath = Directory.GetFileSystemEntries(_path);
                            break;
                        }


                    case false:
                        {
                            filefolderpath = Directory.GetFileSystemEntries(_path);

                            break;
                        }

                }
               
            }
            catch(Exception ex)
            {
                var a = ex.Message;
            }

            return filefolderpath;

        }

        /*Retrieve files extension in order to seperate the two.
          This would allow user to quickly identify files from folder in Console app */
        private static List<string> RetrievingExtension(string _path, bool IsSourcePath)
        {
            List<string> fileExtension = new List<string>();
            foreach(var i in GetDirectoryandFiles(_path, IsSourcePath))
            {
                string extension = Path.GetExtension(i);
                if (extension.Equals(string.Empty))
                {
                    fileExtension.Add("Folder");
                    
                }
                else
                {
                    fileExtension.Add("File");
                }
            }
            return fileExtension;
        }

        //Seperating Files from Folders
        public static Dictionary<string,string> SeperatingFilesFromFolders(string _path, bool IsSourcePath)
        {
            
            for (int i = 0; i < GetDirectoryandFiles(_path, IsSourcePath).Length; i++)
            {

                FileFolder.Add(GetDirectoryandFiles(_path, IsSourcePath)[i].ToString(), RetrievingExtension(_path, IsSourcePath)[i].ToString());
            }
          
            return FileFolder;
        }

        //When user selectsfiles to copy, it will display only files in the DIR
        public static Dictionary<string,string> UserSelectedFiles()
        {
             foreach(var i in FileFolder)
            {
                if(i.Value =="File")
                {
                    OnlyFiles.Add(i.Key, i.Value);
                }
            }
            return OnlyFiles;
        }
        
        //The selected file will be copied
        public static bool UserSelectsFileToCopy(int filenumber, Dictionary<string,string> onlyfiles, string Targetpath)
        {
            bool FileCopied = false;
            // User selects based on numbering
            string filetobecopied = onlyfiles.ElementAt(filenumber).Key;
            List<string> retrieveFileName = filetobecopied.Split("\\").ToList();
            string newlocation = Targetpath + "\\" + retrieveFileName.LastOrDefault();


            //checks if file has been copied.
            if (!File.Exists(newlocation))
            {   //copy only for other location
                
                File.Copy(filetobecopied, newlocation);
                FileCopied = true;
            }

            
            return FileCopied;
        }


        //When user selectsfiles to copy, it will display only files in the DIR
        public static Dictionary<string, string> UserSelectedFolder()
        {
            foreach (var i in FileFolder)
            {
                if (i.Value == "Folder" +
                    "")
                {
                    OnlyFiles.Add(i.Key, i.Value);
                }
            }
            return OnlyFiles;
        }

        //The selected folder will be copied with all files in it
        public static bool UserSelectsFolderToCopy(int foldernumber, Dictionary<string,string> onlyfolders, string Targetpath)
        {
            bool Copiedsuccess = false;
            bool FilesCopied = false;
            bool SubFoldersCopied = false;

            string foldertobecopied = onlyfolders.ElementAt(foldernumber).Key;
            var splitfolderpath = foldertobecopied.Split("\\");
            //create target
            var target = Directory.CreateDirectory(Targetpath + "\\" + splitfolderpath.LastOrDefault());
            //Get folder user picked
          
            //Get all files in folder
            var a = new DirectoryInfo(foldertobecopied);
            //copying files to target dir recursively
            for(int i=0; i<=a.GetFiles().Length-1; i++)
            {
                OnlyFolder.Add(a.GetFiles().ElementAt(i).FullName, a.GetFiles().ElementAt(i).Extension);
                UserSelectsFileToCopy(i, OnlyFolder, target.ToString());
                
                FilesCopied = true;
            }

            

            //Copying subfolder
            for(int i=0; i<=a.GetDirectories().Length-1; i++)
            {
                Subfolder.Clear();
                Subfolder.Add(a.GetDirectories().ElementAt(i).FullName,"");
                UserSelectsFolderToCopy(i, Subfolder, target.ToString());
                SubFoldersCopied = true;
            }


            if(FilesCopied && SubFoldersCopied)
            {
                Copiedsuccess = true;
            }

            return Copiedsuccess;
        }

    }
}
