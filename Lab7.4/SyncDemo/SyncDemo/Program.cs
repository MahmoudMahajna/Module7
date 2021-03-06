﻿using System;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace SyncDemo
{
    class Program
    { 
        static void Main(string[] args)
        {
            try
            {
                var proccessId = Process.GetCurrentProcess().Id.ToString();
                using (Mutex SyncFileMutex = new Mutex(false, "UniqueMutexName"))
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        SyncFileMutex.WaitOne();

                        //Nice
                        File.AppendAllText(@"C:\temp\data.txt", proccessId);

                        //Not wrapping the release in try-catch-finaly
                        SyncFileMutex.ReleaseMutex();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }
        }
    }
}
