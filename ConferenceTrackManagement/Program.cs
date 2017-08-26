using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConferenceTrackManagement.Processor;
using Repository.Helpers;

namespace ConferenceTrackManagement.Application
{
    public class Program
    {
      
        public static List<string> Outputs = new List<string> {};

        static void Main(string[] args)
        {
            string Directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            bool InputFileFound = false;
            string[] Inputs = null;

            //This input can be taken from user
            int NumberOfTracks = 2;

            try
            {
                Inputs = System.IO.File.ReadAllLines(GetFilePath(true));
                InputFileFound = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Input file - " + ex.Message.ToString());
                InputFileFound = false;
            }
            if (InputFileFound)
            {
                List<Track> Tracks = new List<Track> { };
                InputProcessor inputProcessor = new InputProcessor(Inputs,NumberOfTracks);
                Tracks = inputProcessor.Process();
                OutputProcessor outputProcessor = new OutputProcessor(Tracks);
                Outputs = outputProcessor.Process();
                if (Outputs.Count > 1)  WriteOutputToFile();

                Console.WriteLine("Output file is available at " + GetFilePath(false));
            }
            Console.ReadLine();
        }

        static string GetFilePath(Boolean isInput = false)
        {
            var homePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(homePath, isInput ? Constants.InputFileName : Constants.OutputFileName);
            return filePath;
        }


        /// <summary>
        /// Method to write output list to output.txt file         
        /// </summary>

        public static void WriteOutputToFile()
        {      
                string Directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                string OutPutFile = @"" + Directory + "/Output.txt";

                using (FileStream fs = File.OpenWrite((GetFilePath(false))))
                {

                    foreach (string o in Outputs)
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(o);
                        fs.Write(info, 0, info.Length);
                        byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                        fs.Write(newline, 0, newline.Length);
                    }
                }
            }
        }   
}
