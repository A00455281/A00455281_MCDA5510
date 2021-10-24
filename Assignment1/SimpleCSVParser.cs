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
                        String invalidFieldName = string.Empty;
                        bool isInvalidRecord = isRowValid(fields,out invalidFieldName);
                        if (isInvalidRecord)
                        {
                            string log = "File Path:" + fileName + "\tFound Invalid value for field '" + invalidFieldName + "' for record number:" + rowCount;
                            Program.log.Info(log);
                            parserData.totalInvalidCount++;
                        } else 
                        {
                            parserData.totalValidCount++;
                            parserData.data.Add(string.Join(", ", fields));
                        }
                    }
                    return parserData;
                }

            }
            catch (IOException ioe)
            {
                Program.log.Error(ioe.Message);
                Console.WriteLine(ioe.StackTrace);
            }
            return null;
        }

        public bool isRowValid(string[] fields, out String invalidFieldName)
        {
            bool isInvalidRecord = false;
            int index = 0;
            foreach (string field in fields)
            {
                if (string.IsNullOrEmpty(field))
                {
                    isInvalidRecord = true;
                    break;
                }
                index++;
            }
            invalidFieldName = isInvalidRecord ? getFieldNameByIndex(index) : string.Empty;
            return isInvalidRecord;
        }

        public string getFieldNameByIndex(int index)
        {
            String[] fieldNames = { "First Name", "Last Name", "Street Number", "Street", "City", "Province", "Country", "Postal Code", "Phone Number", "email Address" };
            return index < fieldNames.Length ? fieldNames[index] : string.Empty;
        }
    }
}