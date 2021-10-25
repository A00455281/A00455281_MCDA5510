using System;
using System.Collections.Generic;
using System.IO;

namespace Asg1
{
    public class DirWalker
    {
        public List<String> allFileList;

        public DirWalker()
        {
            this.allFileList = new List<string>();
        }
        public void walk(String path)
        {

            string[] list = Directory.GetDirectories(path);

            if (list == null) return;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath);
                }
            }
            string[] fileList = Directory.GetFiles(path, "*.csv");
            this.allFileList.AddRange(fileList);
        }

        public void writeCSVFile()
        {
            try
            {
                int totalInvalidRecords = 0;
                int totalValidRecords = 0;
                Console.WriteLine("Total Files in Dir:" + this.allFileList.Count);
                String outputPath = System.IO.Path.GetFullPath(@"..\..\..\") + @"Output\finalOutput.csv";
                System.IO.File.Delete(outputPath);
                using (FileStream fs = new FileStream(outputPath, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("First Name, Last Name, Street Number, Street, City, Province, Country, Postal Code, Phone Number, email Address, date");
                    foreach (string filePath in this.allFileList)
                    {
                        //**** Processing CSV *******
                        ParserData parserData = new SimpleCSVParser().parse(filePath);
                        totalInvalidRecords += parserData.totalInvalidCount;
                        totalValidRecords += parserData.totalValidCount;
                        foreach (string data in parserData.data)
                        {
                            sw.WriteLine(data);
                        }
                        //****** End processing of CSV *****
                    }
                }
                Program.log.Info("All CSV files merged in single file successfully.");
                Program.log.Info("Total number of skipped rows: " + totalInvalidRecords);
                Program.log.Info("Total Valid Records in Dir: " + totalValidRecords);
                Console.WriteLine("Total Valid Records in Dir: " + totalValidRecords);
                Console.WriteLine("Total number of skipped rows:" + totalInvalidRecords);
            }
            catch(Exception e)
            {
                Program.log.Error(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
