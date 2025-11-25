using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

// Starter, going to make it more dynamica
class Program
{   
     // Grab win32 we going to need it
    [DllImport("user32.dll")]
    static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

    static void Main(string[] args)


    {
        // Open up youtube
        Process.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", "--new-window https://www.youtube.com/watch?v=81tWAoRcngo&list=RDx3hoYr2dZfY&index=3");

        Thread.Sleep(2000);

        // Grab all of the processes that are edge and youtube then lets go move it to my lfet side
        Process[] procs = Process.GetProcessesByName("msedge");
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