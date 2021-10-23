using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using log4net;
using log4net.Config;

namespace Asg1
{
    class SimpleCSVParser
    {

        public ParserData parse(String fileName)
        {
            try
            {
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    ParserData parserData = new ParserData();
                    if(!parser.EndOfData) parser.ReadFields();
                    int rowCount = 1;
                    while (!parser.EndOfData)
                    {
                        rowCount++;
                        string[] fields = parser.ReadFields();
                        bool isInvalidRecord = isRowValid(fields);
                        if (isInvalidRecord)
                        {
                            parserData.totalInvalidCount++;
                        } else 
                        {
                            parserData.data.Add(string.Join(", ", fields));
                        }
                    }
                    return parserData;
                }

            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.StackTrace);
            }
            return null;
        }

        public bool isRowValid(string[] fields)
        {
            bool isInvalidRecord = false;
            foreach (string field in fields)
            {
                if (string.IsNullOrEmpty(field))
                {
                    //Program.log.Info(("Invalid Records Found at {0}.few fields are empty.", fileName));
                    isInvalidRecord = true;
                    break;
                }
            }
            return isInvalidRecord;
        }
    }
}