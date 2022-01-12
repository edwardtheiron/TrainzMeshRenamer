// For Directory.GetFiles and Directory.GetDirectories
// For File.Exists, Directory.Exists
using System;
using System.IO;
using System.Collections;
using System.Linq;

public class RecursiveFileProcessor
{
    public static void Main()
    {
        string path = "D:\\Modeling SSD\\temp2";

        if (File.Exists(path))
        {
            // This path is a file
            ProcessFile(path);
        }
        else if (Directory.Exists(path))
        {
            // This path is a directory
            ProcessDirectory(path);
        }
        else
        {
            Console.WriteLine("{0} is not a valid file or directory.", path);
        }

    }

    // Process all files in the directory passed in, recurse on any directories
    // that are found, and process the files they contain.
    public static void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string[] fileEntries = Directory.GetFiles(targetDirectory);
        foreach (string fileName in fileEntries)
        {
            ProcessFile(fileName);
        }
        // Recurse into subdirectories of this directory.
        string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        foreach (string subdirectory in subdirectoryEntries)
            ProcessDirectory(subdirectory);
    }

    // Insert logic for processing found files here.
    public static void ProcessFile(string path)
    {
        //Console.WriteLine("ProcessFile called ");
        string[] lodtemplate = new string[5];
        string[] lodprefix = new string[5];
        for (int i = 0; i < 5; i++)
        {
            //Console.WriteLine("lodtemplate entered ");
            lodtemplate[i] = "_" + Convert.ToString(i) + ".obj";

            switch(i)
            {
                case 1:
                    lodprefix[i] = "1_0032_";
                    break;
                case 2:
                    lodprefix[i] = "2_0064_";
                    break;
                case 3:
                    lodprefix[i] = "3_0256_";
                    break;
                case 4:
                    lodprefix[i] = "4_1024_";
                    break;
                default:
                    break;
            }

            /*
            if (i * 32 < 100)
            {
                lodprefix[i] = i + "_00" + (i * 32).ToString() + "_";
            }
            else
            {
                lodprefix[i] = i + "_0" + (i * 32).ToString() + "_";
            }
            */
        }

        // main thing here
        for (int i = 1; i < 5; i++) // moving only from 1 to 4
        {
            if (path.EndsWith(lodtemplate[i])) // checks for _i.obj
            {
                string fileName = Path.GetFileName(path); // cuts path
                string[] cut = fileName.Split(lodtemplate[i]); // cuts _i.obj
                //Console.WriteLine(Path.GetFullPath(path));
                //Console.WriteLine(" \n" + lodprefix[i] + cut[0] + " \n");
                if (File.Exists(Path.GetDirectoryName(path) + "\\" + lodprefix[i] + cut[0] + ".obj"))
                {
                    Console.WriteLine("File {0} already exists!", lodprefix[i] + cut[0] + ".obj");
                }
                else if (File.Exists(path))
                {
                    File.Move(path, Path.GetDirectoryName(path) + "\\" + lodprefix[i] + cut[0] + ".obj"); // actual renaming
                }
                else
                {
                    Console.WriteLine("File not found. \n");
                }
            }
        }            
    }
}
