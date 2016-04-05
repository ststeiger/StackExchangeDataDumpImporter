
// using SharpCompress.Archive;
// using SharpCompress.Common;


namespace EfficientJsonImporter
{


    public class SevenZip
    {


        private static string MapProjectPath(string path)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            string basePath = System.IO.Path.GetDirectoryName(ass.Location);
            basePath = System.IO.Path.Combine(basePath, "../../");
            path = System.IO.Path.Combine(basePath, path);
            return System.IO.Path.GetFullPath(path);
        } // End Function MapProjectPath 


        private static long? entryTotal;
        private static long partTotal;
        private static long totalSize;


        public static void ExtractFile(string fileName)
        {
            entryTotal = null;
            partTotal = 0;
            totalSize = 0;

            string toPath = MapProjectPath("DataDump");
            if (!System.IO.Directory.Exists(toPath))
                System.IO.Directory.CreateDirectory(toPath);
            ExtractFile(fileName, toPath);
        }
            
        public static void ExtractFile(string fileName, string toPath)
        {
            using (SharpCompress.Archive.IArchive archive = SharpCompress.Archive.ArchiveFactory.Open(fileName))
            {
                totalSize = archive.TotalSize;

                archive.EntryExtractionBegin += archive_EntryExtractionBegin;
                archive.FilePartExtractionBegin += archive_FilePartExtractionBegin;
                archive.CompressedBytesRead += archive_CompressedBytesRead;

                foreach (SharpCompress.Archive.IArchiveEntry entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                        // entry.WriteToDirectory(toPath, options);
                        SharpCompress.Archive.IArchiveEntryExtensions.
                        WriteToDirectory(entry, toPath, SharpCompress.Common.ExtractOptions.ExtractFullPath | SharpCompress.Common.ExtractOptions.Overwrite);
                } // Next entry 

            } // End Using archive

        } // End Sub ExtractFile


        public static void archive_CompressedBytesRead(object sender, SharpCompress.Common.CompressedBytesReadEventArgs e)
        {
            System.Console.WriteLine("Read Compressed File Part Bytes: {0} Percentage: {1}%",
                e.CurrentFilePartCompressedBytesRead, CreatePercentage(e.CurrentFilePartCompressedBytesRead, partTotal));

            string percentage = entryTotal.HasValue ? CreatePercentage(e.CompressedBytesRead, entryTotal.Value).ToString() : "Unknown";
            System.Console.WriteLine("Read Compressed File Entry Bytes: {0} Percentage: {1}%", e.CompressedBytesRead, percentage);
        }


        private static int CreatePercentage(long n, long d)
        {
            return (int)(((double)n / (double)d) * 100);
        }


        public static void archive_FilePartExtractionBegin(object sender, SharpCompress.Common.FilePartExtractionBeginEventArgs e)
        {
            // this.
            partTotal = e.Size;
            System.Console.WriteLine("Initializing File Part Extraction: " + e.Name);
        }


        public static void archive_EntryExtractionBegin(object sender, SharpCompress.Common.ArchiveExtractionEventArgs<SharpCompress.Archive.IArchiveEntry> e)
        {
            // this.
            entryTotal = e.Item.Size;
            System.Console.WriteLine("Initializing File Entry Extraction: " + e.Item.Key);
        }


    } // End Class SevenZip 


} // End Namespace EfficientJsonImporter 
