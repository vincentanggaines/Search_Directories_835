using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Search_Directories_835
{
    // For Directory.GetFiles and Directory.GetDirectories
    // For File.Exists, Directory.Exists
    public class RecursiveFileProcessor
    {
        public static void Main()
        {
            // Nuclear Iradiated Hard Code Penetrator
            string[] args = new string[] { @"\\usmanw01fs01\is\IT Shared\File Processing\Verity2\835\Transfer to Artiva Prod" };
            
            // Create the list of Accounts
            string AccountList = @"\\usmanw01fs01\is\IT Shared\Vincent\HCENPTACCT.txt";
            var AcctlistFile = File.ReadAllLines(AccountList);
            var AcctList = new List<string>(AcctlistFile);

            foreach (string path in args)
            {
                if (File.Exists(path))
                {
                    // This path is a file
                    ProcessFile(path, AcctList);
                }
                else if (Directory.Exists(path))
                {
                    // This path is a directory
                    ProcessDirectory(path, AcctList);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                }
            }
        }


        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory, List<string> AcctList)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName, AcctList);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, AcctList);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path, List<string> AcctList)
        {
            ScanFile(path, AcctList);
            Console.WriteLine("Processed file '{0}'.", path);
        }

        // Scan the file and wtite an entry if found.
        public static void ScanFile(string path, List<string> AcctList)
        {
            string found = @"\\usmanw01fs01\is\IT Shared\Vincent\HCENPTACCT_Found.txt";
            foreach (var line in File.ReadAllLines(path))
            {
                foreach (var account in AcctList)
                    if (line.Contains(account))
                    {
                        // Write File name + Account in FOUND file.
                        using (StreamWriter fileFound = File.AppendText(found))

                        { fileFound.WriteLine(path + "|" + account); }
                    }


            }
            Console.WriteLine("Processed file '{0}'.", path);
        }
    }
}
