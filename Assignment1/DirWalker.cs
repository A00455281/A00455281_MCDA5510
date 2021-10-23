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
            string[] fileList = Directory.GetFiles(path);
            this.allFileList.AddRange(fileList);
        }

        public void writeCSVFile()
        {
            int totalInvalidRecords = 0;
            Console.WriteLine("Total Files in Dir:" + this.allFileList.Count);
            using (FileStream fs = new FileStream(@"C:\SMU\Software Developement\test.csv", FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("First Name, Last Name, Street Number, Street, City, Province, Country, Postal Code, Phone Number, email Address");
                foreach (string filePath in this.allFileList)
                {
                    //**** Processing CSV *******
                    ParserData parserData = new SimpleCSVParser().parse(filePath);
                    totalInvalidRecords += parserData.totalInvalidCount;
                    foreach (string data in parserData.data)
                    {
                        sw.WriteLine(data);
                    }
                    //****** End processing of CSV *****
                }
            }
            Console.WriteLine("Total Invalid Records in Dir:" + totalInvalidRecords);
        }
    }
}
