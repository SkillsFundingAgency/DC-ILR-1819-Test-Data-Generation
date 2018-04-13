using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DCT.ILR.Model;
using DCT.TestDataGenerator;
using DCT.TestDataGenerator.Functor;

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

        public static void WriteXmlFiles(string folder, Dictionary<string, string> dictionary, string ns)
        {
            foreach (var kvp in dictionary)
            {
                string filename = $"{folder}{kvp.Key}";
                File.WriteAllText(filename, kvp.Value);
                // ooo. This is a bit stinky.
                if (ns.ToLower() != ESFA201819Namespace.ToLower())
                {
                    var xml1718Content = kvp.Value;
                    xml1718Content = xml1718Content.Replace(ESFA201819Namespace, SFA201718Namespace);
                    xml1718Content = xml1718Content.Replace(ESFA1819Year, SFA1718Year);
                    System.IO.File.WriteAllText(filename.Replace(ILRFileName1819, ILRFileName1718), xml1718Content);
                }
            }
        }

        public static string OutputControlFile(string folder, IEnumerable<FileRuleLearner> allLearners)
        {
            string filename = $"{folder}control-{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.csv";
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
    }
}
