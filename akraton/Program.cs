using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Extensions.Configuration;

// Starter, going to make it more dynamica
class Program
{   
     // Grab win32 we going to need it
    [DllImport("user32.dll")]
    static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    public class AppSettings
    {
        public string BrowserType { get; set; }

    }

    static void Main(string[] args)
    {
        //Lets grab our config file we gots
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


        IConfiguration config = builder.Build();

        string browserType = config["AppSettings:BrowserType"];


        //Push it into the Class so we can use it later
        var settings = config
            .GetSection("AppSettings")
            .Get<AppSettings>();



        // Open up youtube
        Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", "--new-window https://www.youtube.com/watch?v=81tWAoRcngo&list=RDx3hoYr2dZfY&index=3");

        Thread.Sleep(2000);

        // Grab all of the processes that are edge and youtube then lets go move it to my lfet side
        Process[] procs = Process.GetProcessesByName(browserType);
        foreach (var p in procs)
        {
            if (!string.IsNullOrEmpty(p.MainWindowTitle) &&
                p.MainWindowTitle.Contains("Youtube"))
            {
                Console.WriteLine("This winsow is: " + p.MainWindowTitle);

                MoveWindow(p.MainWindowHandle, 1920, 0, 1920, 1080, true);

                break;
            }

        }


    }






}