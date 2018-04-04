using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCT.TestDataGenerator;
using DCT.TestDataGenerator.Functor;

namespace ESFA.DC.ILR.TestDataGenerator.Console
{
    public class Program
    {
        private const string supported = "-supported";
        private static int UKPRN = 90000064;

        private static List<ILearnerMultiMutator> _functors = new List<ILearnerMultiMutator>(100);

        public static void Main(string[] args)
        {
            var cache = new DataCache();
            var rfp = new RuleToFunctorParser(cache);
            rfp.CreateFunctors(AddFunctor);

            string folder = @".\";
            CheckForCommandLine(args, "-path", ref folder);
            uint scale = 1;
            CheckForCommandLine(args, "-scale", ref scale);
            CheckForCommandLine(args, supported);

            List<ActiveRuleValidity> rules = new List<ActiveRuleValidity>(100);
            for (int i = 0; i != args.Length; ++i)
            {
                if (args[i].ToLower() == "-rules")
                {
                    for (int j = i + 1; j < args.Length - 1; j += 2)
                    {
                        string name = args[j];
                        bool valid = false;
                        if (bool.TryParse(args[j + 1], out valid))
                        {
                            rules.Add(new ActiveRuleValidity()
                            {
                                RuleName = args[j],
                                Valid = valid
                            });
                        }
                    }
                }
            }

            if (rules.Count > 0)
            {
                XmlGenerator.CreateAllFiles(rfp, rules, UKPRN, folder, scale);
            }
            else
            {
                DisplaySupported();
            }
        }

        private static void AddFunctor(ILearnerMultiMutator i)
        {
            _functors.Add(i);
        }

        private static void CheckForCommandLine(string[] args, string v, ref string s)
        {
            for (int i = 0; i < args.Length - 1; ++i)
            {
                if (args[i].ToLower() == v)
                {
                    s = args[i + 1];
                    break;
                }
            }
        }

        private static void CheckForCommandLine(string[] args, string v)
        {
            for (int i = 0; i < args.Length - 1; ++i)
            {
                if (args[i].ToLower() == v)
                {
                    if (v == supported)
                    {
                        DisplaySupported();
                    }

                    break;
                }
            }
        }

        private static void DisplaySupported()
        {
            foreach (ILearnerMultiMutator f in _functors)
            {
                System.Console.Write($"{f.RuleName()} false ");
            }
        }

        private static bool CheckForCommandLine(string[] args, string flag, ref uint value)
        {
            string t = string.Empty;
            for (int i = 0; i < args.Length - 1; ++i)
            {
                if (args[i].ToLower() == flag)
                {
                    t = args[i + 1];
                    break;
                }
            }

            uint ti = 0;
            bool result = uint.TryParse(t, out ti);
            if (result)
            {
                value = ti;
            }

            return result;
        }
    }
}
