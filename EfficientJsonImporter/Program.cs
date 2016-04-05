
using SharpCompress.Archive;
using SharpCompress.Common;

namespace EfficientJsonImporter
{


    class MainClass
    {

        public static void Extract7Zip(string fileName, string toPath)
        {
            using (var archive = ArchiveFactory.Open(fileName))
            {
                archive.EntryExtractionBegin += archive_EntryExtractionBegin;
                archive.FilePartExtractionBegin += archive_FilePartExtractionBegin;
                archive.CompressedBytesRead += archive_CompressedBytesRead;

                // .Where(entry => !entry.IsDirectory)
                foreach (var entry in archive.Entries)
                {
                    if (!entry.IsDirectory)
                        entry.WriteToDirectory(toPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                }
            }

        }

        public static void archive_CompressedBytesRead(object sender, CompressedBytesReadEventArgs e)
        {
            /*
            System.Console.WriteLine("Read Compressed File Part Bytes: {0} Percentage: {1}%",
                e.CurrentFilePartCompressedBytesRead, CreatePercentage(e.CurrentFilePartCompressedBytesRead, partTotal));

            string percentage = entryTotal.HasValue ? CreatePercentage(e.CompressedBytesRead, entryTotal.Value).ToString() : "Unknown";
            System.Console.WriteLine("Read Compressed File Entry Bytes: {0} Percentage: {1}%", e.CompressedBytesRead, percentage);
             * */
        }


        public static void archive_FilePartExtractionBegin(object sender, FilePartExtractionBeginEventArgs e)
        {
            // this.partTotal = e.Size;
            System.Console.WriteLine("Initializing File Part Extraction: " + e.Name);
        }


        public static void archive_EntryExtractionBegin(object sender, ArchiveExtractionEventArgs<IArchiveEntry> e)
        {
            // this.entryTotal = e.Item.Size;
            System.Console.WriteLine("Initializing File Entry Extraction: " + e.Item.Key);
        }


        public static void Main(string[] args)
        {
            System.Collections.Generic.List<DataDump> ls = DataDumpArchive.GetPossibleStackExchangeDataDumps();
            // EfficientXmlImport.HorribleFirstAttempt();

            string fn = @"D:\username\Documents\Downloads\startups.stackexchange.com.7z";
            Extract7Zip(fn, @"D:\username\Documents\Visual Studio 2013\Projects\StackExchangeDataDumpImporter\EfficientJsonImporter\dataDump");

            EfficientXmlImport.Test();

            System.Console.WriteLine(ls);

            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class MainClass


} // End Namespace EfficientJsonImporter 
