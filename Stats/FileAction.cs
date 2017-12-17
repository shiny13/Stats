using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stats
{
    public class FileAction
    {
        #region Public Methods

        /// <summary>
        ///     The Record method takes in absolute file path and the values to be stored in that file. 
        ///     If the the file doesn't exist, then it creates the file and stores the values in comma separated format.
        /// </summary>
        /// <param name="filepath">A string of absolute file path input to store the values</param>
        /// <param name="value">A string of values to be stored in the file</param>
        public void Record(string filepath, string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                Help();
                return;
            }

            try
            {
                //Create the directories and the file as required if it doesn't exist
                if (!File.Exists(filepath))
                    CreateFileFromPath(filepath);
                
                //Modify the string value if needed before writing to file
                var modifiedValue = ModifyNumberString(filepath, value);

                //Append to the file
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.Write(modifiedValue);
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                Help();
            }
        }

        /// <summary>
        ///     The Summary method take in the absolute file path as parameter of a text file and shows the details of the values stored in the file.
        /// </summary>
        /// <param name="filepath">A string of absolute file path</param>
        public void Summary(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Help();
                return;
            }

            try
            {
                //Get all the values from file as one string
                string fileValue = File.ReadAllText(filepath);
                if (String.IsNullOrWhiteSpace(fileValue))
                {
                    Help();
                    return;
                } 

                //Split the string by comma and parse to List<double> to later utilize the Max, Min, Average, and Count functions
                var numbers = new List<double>(fileValue.Split(',').Select(s => double.Parse(s)));

                Console.WriteLine("+--------------+---------+");
                Console.WriteLine("| # of Entries | " + numbers.Count.ToString());
                Console.WriteLine("| Min. value   | " + numbers.Min().ToString());
                Console.WriteLine("| Max. value   | " + numbers.Max().ToString());
                Console.WriteLine("| Avg. value   | " + numbers.Average().ToString());
                Console.WriteLine("+--------------+---------+");
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                Help();
            }
        }

        /// <summary>
        ///     The Help method instructs the user how to the application by providing instructions and examples.
        /// </summary>
        public void Help()
        {
            Console.WriteLine("********** Instructions on using the application **********");
            Console.WriteLine("1. To record some values in a file please type: record <absolute file path> <values separated by space>");
            Console.WriteLine("2. To summarize the values in a file please type: summary <absolute file path>");
            Console.WriteLine("3. To watch the instructions again please type: help");
            Console.WriteLine("4. To exit the application please type: exit");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Examples on how to use the application:");
            Console.WriteLine("record C:\\Shahnawaz\\stats.txt 12.1");
            Console.WriteLine("record C:\\Shahnawaz\\stats.txt 4.8 20.0");
            Console.WriteLine("record C:\\Shahnawaz\\stats.txt 9.9");
            Console.WriteLine("summary C:\\Shahnawaz\\stats.txt");
            Console.WriteLine("+--------------+------+");
            Console.WriteLine("| # of Entries | 4    |");
            Console.WriteLine("| Min. value   | 4.8  |");
            Console.WriteLine("| Max. value   | 20.0 |");
            Console.WriteLine("| Avg. value   | 11.7 |");
            Console.WriteLine("+--------------+------+");
            Console.WriteLine("***********************************************************");

        }

        #endregion Public Methods

        #region Private Helper Methods

        /// <summary>
        ///     The CreateFileFromPath method takes the absolute file path and creates directories if required, then creates the file in the path specified
        /// </summary>
        /// <param name="filepath">The absolute file path</param>
        private void CreateFileFromPath(string filepath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filepath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.CreateText(filepath);
            } catch (Exception ex)
            {
                Help();
            }
        }

        /// <summary>
        ///     The ModifyNumberString method checks if the file is empty, if not empty it will add a comma in front of the string
        /// </summary>
        /// <param name="filepath">The absolute file path</param>
        /// <param name="value">The string value to be modified</param>
        /// <returns></returns>
        private string ModifyNumberString(string filepath, string value)
        {
            var modifiedValue = value.Replace(" ", ",");

            string existingValues = File.ReadAllText(filepath);
            if (!String.IsNullOrWhiteSpace(existingValues))
                modifiedValue = "," + modifiedValue;

            return modifiedValue;
        }

        #endregion Private Helper Methods
    }
}
