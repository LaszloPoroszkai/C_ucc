using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;


namespace SeekAndArchive
{
    class Program
    {
        static void Main(string[] args)
        {
            String file = args[0];
            String path = args[1];
            int counter = 0;

            DateTime lastModified = new DateTime(1900, 01, 01, 00, 00, 00);

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(2);

            var timer = new System.Threading.Thread((e) =>
            {
                Console.WriteLine("Hit CTRL+X to stop");
                while (true)
                {
                    DateTime tempMod = Archiver(path, file, lastModified, counter);
                    if (tempMod > lastModified)
                    {
                        counter += 1;
                        lastModified = tempMod;
                    }
                    System.Threading.Thread.Sleep(3000);
                }
            });

            timer.Start();
            timer.Join();
        }

        public static DateTime Archiver(String path, String file, DateTime lastModified, int counter)
        {
            String[] allFiles = Directory.GetFiles(path, file, SearchOption.AllDirectories);

            foreach (String p in allFiles)
            {
                FileInfo info = new FileInfo(p);
                if (info.LastWriteTimeUtc > lastModified)
                {
                    string toBeZipped = @"C:\Users\MoxSapphire\source\repos\C_ucc\TW_projects\LookupCollections\";
                    string zipPath = @"C:\Users\MoxSapphire\source\repos\C_ucc\TW_projects\backup" + counter + ".zip";

                    ZipFile.CreateFromDirectory(toBeZipped, zipPath);
                }
                return info.LastWriteTimeUtc;
            }
            return new DateTime(1900, 01, 01, 00, 00, 00);
        }
    }
}