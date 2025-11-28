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
        // Lets check to see what operating system we are running
        string os = "UNKNOWN";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            os = "Windows";
        }

        if (os != "Windows")
        {
            Console.WriteLine("ERROR 1: Currently we only support Windows. Sorry yall");
            return;
        }

        //Lets grab our config file we gots
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


        IConfiguration systemConfig = configBuilder.Build();


        // Lets store our steps JSON file for when we execute out scripts
        string stepsJson = File.ReadAllText("stepsFile.json");

        

        // Make this a loop later
        string browserType = systemConfig["AppSettings:BrowserType"];
        string browserLocation = systemConfig["AppSettings:BrowserLocation"];


        // deserialize the JSON
        var steps = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(stepsJson);

        //Push it into the Class so we can use it later
        var settings = systemConfig
            .GetSection("AppSettings")
            .Get<AppSettings>();


       
        //This moves the direction of where I tell it
        //Taking this function away, want to use it later however need to find a way to use it effectivley
        void moveMyWindow (string urlTitle)
        {
            string[] parts = urlTitle.Split('.');

            string windowTitle = parts[1];

            Console.WriteLine(windowTitle);

            //win32 stuff, grabs the group of windows then moves it to the cords
            Process[] windowProcs = Process.GetProcessesByName(browserType);
            foreach (var p in windowProcs)
            {

                Console.WriteLine("JCV:" + windowTitle);
                if (!string.IsNullOrEmpty(p.MainWindowTitle) &&
                    p.MainWindowTitle.Contains(windowTitle, StringComparison.OrdinalIgnoreCase)
)
                {
                    Console.WriteLine("This window is: " + windowTitle);

                    MoveWindow(p.MainWindowHandle, 5, 1, 5, 5, true);

                break;
            }

        }

        }

        //We are going to iterate through the JSON file 
        foreach (var step in steps!) 
        {
            Console.WriteLine($"{step.Key}");

            // If the user wants multiple tabs in one window we will combine it in on the batch end
            string combinedURL = "";

            foreach (var pair in step.Value)
            {
                string key = pair.Key;
                string url = pair.Value;

                Console.WriteLine($"{key}:{url}");
                combinedURL += url + " ";

            }
        
            Process.Start(browserLocation, "--new-window " + combinedURL);

            //move it to where the user wants it
            //Taking this out for now
            //moveMyWindow(combinedURL);
        }


    }


}