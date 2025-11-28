using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System.Text.Json;


// Starter, going to make it more dynamica
class Program
{   
     // Grab win32 we going to need it
    [DllImport("user32.dll")]
    static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    // Class for the config file
    public class AppSettings
    {
        public string BrowserType { get; set; }
        public string BrowserLocation { get; set; }

    }


    static void Main(string[] args)
    {
        //Lets grab our config file we gots
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Lets store our steps JSON file for when we execute out scripts
        string stepsJson = File.ReadAllText("stepsFile.json");
     
        IConfiguration systemConfig = configBuilder.Build();

        

        // Make this a loop later
        string browserType = systemConfig["AppSettings:BrowserType"];
        string browserLocation = systemConfig["AppSettings:BrowserLocation"];


        // deserialize the JSON
        var steps = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(stepsJson);

        //Push it into the Class so we can use it later
        var settings = systemConfig
            .GetSection("AppSettings")
            .Get<AppSettings>();

        //We need to parse the steps file


        // Open up youtube
        //Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", "--new-window https://www.youtube.com/watch?v=81tWAoRcngo&list=RDx3hoYr2dZfY&index=3");

        //Thread.Sleep(2000);

        //We are going to iterate through the JSON file 
        foreach (var step in steps!) //JCV WHY ! ARE YOU MAD
        {
            Console.WriteLine($"{step.Key}");

            foreach (var pair in step.Value)
            {
                string key = pair.Key;
                string url = pair.Value;

                Console.WriteLine($"{key}:{url}");
                Process.Start(browserLocation, "--new-window " + url);
            }

        }

        //Process.Start(browserLocation + "--new-window");

        // Grab all of the processes that are edge and youtube then lets go move it to my lfet side
        Process[] procs = Process.GetProcessesByName(browserType);
        foreach (var p in procs)
        {
            if (!string.IsNullOrEmpty(p.MainWindowTitle) &&
                p.MainWindowTitle.Contains("Youtube"))
            {
                Console.WriteLine("This window is: " + p.MainWindowTitle);

                //MoveWindow(p.MainWindowHandle, 1920, 0, 1920, 1080, true);

                break;
            }

        }


    }






}