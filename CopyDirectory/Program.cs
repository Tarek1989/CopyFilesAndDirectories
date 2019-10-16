using System;
using System.Collections.Generic;

namespace CopyDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
           
            // I felt that retrieving source path and inserting a target path was flexible.
            // Therefore i decided to let the user copy paste source path and insert target
            Console.WriteLine("Welcome to File&Folder Copying feauture");
            Console.WriteLine();
            Console.WriteLine("In order to initiate we require two important things from you:" + Environment.NewLine + "1)Source path (Just copy paste it)");
            string SourcePath =   Console.ReadLine();
            SourcePath.Replace(@"\",@"\\");
            Console.WriteLine("2) Target path (Just copy paste it)");
            string Targetpath = Console.ReadLine();
            Targetpath.Replace(@"\", @"\\");

            // retrieving files from source path adn target 
            Dictionary<string,string> sourcefiles =  CopyDirectory_Logic.DirectoryFiles.SeperatingFilesFromFolders(SourcePath,true);
            
            if (sourcefiles.Count == 0)
            {
                Console.WriteLine("You have inserted a false path. The program will now exit");
                Console.ReadLine();
                Environment.Exit(0);
            }
            int Numbering = 0;
            Console.WriteLine();
            foreach (KeyValuePair<string,string> i in sourcefiles)
            {
              
                Console.WriteLine(Numbering + " " + i.Key + " " + i.Value);
                Numbering += 1;
            }

            Console.WriteLine();
            Console.WriteLine("Do you wish to use the copy functionality Y / N");
            string CopyYorN = Console.ReadLine();
            if(CopyYorN == "Y" || CopyYorN == "y")
            {
                Console.WriteLine();
                Console.WriteLine("Chose File or Folder to Copy");
                string FileorFolder = Console.ReadLine();
                if (FileorFolder == "File" || FileorFolder == "file")
                {
                    Numbering = 0;
                    Dictionary<string, string> onlyfiles = CopyDirectory_Logic.DirectoryFiles.UserSelectedFiles();
                    Console.WriteLine();
                    foreach (KeyValuePair<string, string> i in onlyfiles)
                    {
                        
                        
                        Console.WriteLine(Numbering + " " + i.Key + " " + i.Value);
                        Numbering += 1;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Choose file you want to copy by typing number");
                    string FileNumber =  Console.ReadLine();
                    int value;
                    if(int.TryParse(FileNumber,out value))
                    {
                        if (value > Numbering || value < 0)
                        {
                            Console.WriteLine("No file with such number");
                        }
                        else
                        {
                            bool IfFileCopied = CopyDirectory_Logic.DirectoryFiles.UserSelectsFileToCopy(value, onlyfiles, Targetpath);
                            if(IfFileCopied)
                            {
                                Console.WriteLine("The file has been succesfully copied");

                            }
                            else
                            {
                                Console.WriteLine("The file has not been copied as the source path is the same as the target path");
                            }
                            
                        }

                    }
                    else
                    {
                        Console.WriteLine("Type in numeric format");
                    }
                    

                }
                else if (FileorFolder == "Folder" || FileorFolder == "folder")
                {
                    Numbering = 0;
                    Dictionary<string, string> onlyfolders = CopyDirectory_Logic.DirectoryFiles.UserSelectedFolder();
                    Console.WriteLine();
                    foreach (KeyValuePair<string, string> i in onlyfolders)
                    {
                        

                        Console.WriteLine(Numbering + " " + i.Key + " " + i.Value);
                        Numbering += 1;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Choose folder you want to copy by typing number");
                    string FolderNumber = Console.ReadLine();
                    int value;
                    if (int.TryParse(FolderNumber, out value))
                    {
                        if (value > Numbering || value < 0)
                        {
                            Console.WriteLine("No folder with such number");
                        }
                        else
                        {
                            bool IfFolderCopied = CopyDirectory_Logic.DirectoryFiles.UserSelectsFolderToCopy(value, onlyfolders, Targetpath);
                            if (IfFolderCopied)
                            {
                                Console.WriteLine("The folder has been succesfully copied");

                            }
                            else
                            {
                                Console.WriteLine("The folder has not been copied");
                            }

                        }

                    }
                    else
                    {
                        Console.WriteLine("Type in numeric format");
                    }
                }
                else
                {
                    Console.WriteLine("Please choose File or Folder");
                }
            }
            else
            {
                Console.Clear();
            }
           
            Console.ReadLine();
        }
    }
}
