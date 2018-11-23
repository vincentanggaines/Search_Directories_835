using System;
using System.IO;

namespace Split_Text_Files
{
    class Split_Text_Files
    {
        static void Main(string[] args)
        {
            // Get the Input/ Output Directories and file.
            string inputFile = "\\\\usmanw01fs01\\is\\IT Shared\\Ranger\\split file\\" + "DQ040R1_2014081907345448_XES.txt";
            string outputPath = "\\\\usmanw01fs01\\is\\IT Shared\\Vincent\\Text Files\\";
            string tempFile = "\\\\usmanw01fs01\\is\\IT Shared\\Vincent\\Text Files\\" + "Temp.txt";
            string accountNo, label, newFile;
            accountNo = "";
            label = "";
            newFile = "";

            // Write each directory name to a file.
            // Read the file and check for the list of Accounts
            try
            {
                using (var reader = new StreamReader(inputFile))
                {

                    var line = reader.ReadLine();
                    if (!reader.EndOfStream)
                    {
                        // create a new temp file for account
                        CreateEmptyFile(tempFile);
                    }

                    while (!reader.EndOfStream)
                    {
                        // Is this a new account?
                        label = line.Substring(2, 13);
                        // new file?
                        newFile = "";
                        if (label == "PREPARED FOR:")
                        {
                            newFile = "yes";
                        }
                        // get account name

                        if (label == "ACCOUNT NO.  ")
                        {
                            accountNo = line.Substring(17, 13);
                        }

                        // Have a new file?
                        if (newFile.Length > 0)
                        {
                            // have new account name from file?
                            if (accountNo.Length > 0)
                            {
                                // rename temp file to account name
                                if (File.Exists(outputPath + accountNo))
                                {
                                    System.IO.File.Move(outputPath + accountNo, outputPath + accountNo + ".1");
                                }
                                System.IO.File.Move(tempFile, outputPath + accountNo);
                            }
                            accountNo = "";
                            // create a new temp file for account
                            CreateEmptyFile(tempFile);
                        }

                        // Write to temp file, line
                        using (StreamWriter temp = File.AppendText(tempFile))

                        { temp.WriteLine(line); }

                        line = reader.ReadLine();
                    }

                }

            }
            catch (Exception e)
            {

            }
        }
        public static void CreateEmptyFile(string filename)
        {
            File.Create(filename).Dispose();
        }
    }
}
