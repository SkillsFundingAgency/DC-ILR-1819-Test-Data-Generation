using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace DCT.TestDataGenerator
{
    public class FileWriter
    {
        private const string ESFA201819Namespace = "ESFA/ILR/2018-19";
        private const string SFA201718Namespace = "SFA/ILR/2017-18";
        private const string ESFA1819Year = "<Year>1819</Year>";
        private const string SFA1718Year = "<Year>1718</Year>";
        private const string ILRFileName1819 = "1819";
        private const string ILRFileName1718 = "1718";

        public static void WriteXmlFiles(string folder, Dictionary<string, string> dictionary, string ns, Encoding encoding = null, bool bom = false, bool zip = false)
        {
            if (encoding == null)
            {
                encoding = new UTF8Encoding(bom);
            }

            foreach (var kvp in dictionary)
            {
                string filename = $"{folder}{kvp.Key}";
                WriteFile(filename, kvp.Value, encoding, bom, zip);
                if (string.Equals(ns, SFA201718Namespace, StringComparison.OrdinalIgnoreCase))
                {
                    string xml1718Content = kvp.Value;
                    xml1718Content = xml1718Content.Replace(ESFA201819Namespace, SFA201718Namespace);
                    xml1718Content = xml1718Content.Replace(ESFA1819Year, SFA1718Year);
                    WriteFile(filename.Replace(ILRFileName1819, ILRFileName1718), xml1718Content, encoding, bom, zip);
                }
            }
        }

        public static string OutputControlFile(string folder, IEnumerable<FileRuleLearner> allLearners)
        {
            string filename = $"{folder}control-{DateTime.Now:yyyyMMdd-HHmmss}.csv";
            using (TextWriter sw = new StreamWriter(filename))
            {
                var t = typeof(FileRuleLearner);
                var props = t.GetProperties();
                foreach (var prop in props)
                {
                    sw.Write(prop.Name + ",");
                }

                sw.WriteLine(string.Empty);
                foreach (var frl in allLearners)
                {
                    foreach (var prop in props)
                    {
                        sw.Write(prop.GetValue(frl) + ",");
                    }

                    sw.WriteLine(string.Empty);
                }
            }

            return filename;
        }

        private static void WriteFile(string filename, string contents, Encoding encoding, bool bom, bool zip)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                WriteStringToStreamWithEncoding(contents, encoding, bom, fileStream);
            }

            if (!zip)
            {
                return;
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    ZipArchiveEntry newEntry = archive.CreateEntry(Path.GetFileName(filename));
                    using (Stream streamOut = newEntry.Open())
                    {
                        WriteStringToStreamWithEncoding(contents, encoding, bom, streamOut);
                    }
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                using (FileStream fileStream = new FileStream(filename.Replace(".xml", ".zip"), FileMode.Create))
                {
                    memoryStream.CopyTo(fileStream);
                }
            }
        }

        private static void WriteStringToStreamWithEncoding(string contents, Encoding encoding, bool bom, Stream fileStream)
        {
            if (bom)
            {
                byte[] preamble = encoding.GetPreamble();
                fileStream.Write(preamble, 0, preamble.Length);
            }

            byte[] data = encoding.GetBytes(contents);
            fileStream.Write(data, 0, data.Length);
        }
    }
}
