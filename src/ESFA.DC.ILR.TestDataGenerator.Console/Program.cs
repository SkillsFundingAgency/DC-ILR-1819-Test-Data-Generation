using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DCT.TestDataGenerator;
using DCT.TestDataGenerator.Functor;

namespace ESFA.DC.ILR.TestDataGenerator.Console
{
    public class Program
    {
        private const string supportedKeyword = "-supported";
        private const string helpKeyword = "-help";
        private const string pathKeyword = "-path";
        private const string scaleKeyword = "-scale";
        private const string rulesKeyword = "-rules";
        private const string namespaceKeyword = "-ns";
        private const string defaultNamespace = "ESFA/ILR/2018-19";

        private static int UKPRN = 90000064;

        private static List<ILearnerMultiMutator> _functors = new List<ILearnerMultiMutator>(100);

        public static void Main(string[] args)
        {
            var cache = new DataCache();
            var rfp = new RuleToFunctorParser(cache);
            rfp.CreateFunctors(AddFunctor);

            string folder = @".\";
            CheckForCommandLine(args, pathKeyword, ref folder);
            string ns = defaultNamespace;
            CheckForCommandLine(args, namespaceKeyword, ref ns);
            uint scale = 1;
            CheckForCommandLine(args, scaleKeyword, ref scale);
            CheckForCommandLine(args, supportedKeyword);
            CheckForCommandLine(args, helpKeyword);

            List<ActiveRuleValidity> rules = new List<ActiveRuleValidity>(100);
            for (int i = 0; i != args.Length; ++i)
            {
                if (args[i].ToLower() == rulesKeyword)
                {
                    for (int j = i + 1; j < args.Length - 1; j += 2)
                    {
                        string name = args[j].ToLower();
                        var ienum = _functors.Where(s => s.RuleName().ToLower() == name);
                        bool found = ienum.Count() > 0;
                        if (!found)
                        {
                            break;
                        }

                        bool valid = false;
                        if (bool.TryParse(args[j + 1], out valid))
                        {
                            rules.Add(new ActiveRuleValidity()
                            {
                                RuleName = ienum.First().RuleName(),
                                Valid = valid
                            });
                        }
                    }

                    break;
                }
            }

            if (rules.Count > 0)
            {
                System.Console.WriteLine($"{XmlGenerator.CreateAllFiles(rfp, rules, UKPRN, folder, scale, ns)}");
            }
            else
            {
                DisplayUsage();
            }
        }

        private static void DisplayUsage()
        {
            Assembly assem = typeof(Program).Assembly;
            System.Console.WriteLine("Assembly name: {0}", assem.FullName);
            System.Console.WriteLine($"  {rulesKeyword} <rule_name0> <false|true> <rule_name1> <false|true> ... <rule_namen> <false|true|>");
            System.Console.WriteLine("     - Required parameter. Which rules to generate learners for. Each rule has a mandatory 'validity' flag.");
            System.Console.WriteLine("       Does it generate data that passes the rule (true) or data that will cause the rule to fail");
            System.Console.WriteLine("       and generate output when run through a validation engine (false)");
            System.Console.WriteLine("       Spelling is important but is case insensitive.");
            System.Console.WriteLine($"  [{namespaceKeyword} <argument>]");
            System.Console.WriteLine($"    - Optional parameter. Defaults to {defaultNamespace}");
            System.Console.WriteLine($"  [{supportedKeyword}]");
            System.Console.WriteLine($"    - Optional parameter. Lists all of the rule names that are supported");
            System.Console.WriteLine($"      (and for easy formatting also prints the word 'false')");
            System.Console.WriteLine($"      Exits the application and doesn't do any further work");
            System.Console.WriteLine($"  [{pathKeyword} <argument>]");
            System.Console.WriteLine($"    - Optional parameter. A path to where the files should be stored upon");
            System.Console.WriteLine($"      generation. Failure to find the path will result in underhandled");
            System.Console.WriteLine($"      path not found exceptions");
            System.Console.WriteLine($"  [{scaleKeyword} <argument>]");
            System.Console.WriteLine($"    - Optional parameter. How many times the rules selected should be");
            System.Console.WriteLine($"      invoked to write unique learners to the output files");
            System.Console.WriteLine($"  [{helpKeyword}]");
            System.Console.WriteLine($"    - Optional parameter. Displays this text.");
            System.Console.WriteLine($"  Examples:");
            System.Console.WriteLine($"    {assem.GetName().CultureName} -scale 10 -path d:\\ -rules Accm_01 false");
        }

        private static void AddFunctor(ILearnerMultiMutator i)
        {
            _functors.Add(i);
        }

        private static void CheckForCommandLine(string[] args, string v, ref string s)
        {
            for (int i = 0; i < args.Length - 1; ++i)
            {
                if (args[i].ToLower().TrimEnd() == v)
                {
                    s = args[i + 1];
                    break;
                }
            }
        }

        private static void CheckForCommandLine(string[] args, string v)
        {
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].ToLower() == v)
                {
                    if (v == supportedKeyword)
                    {
                        DisplaySupported();
                        Environment.Exit(0);
                    }

                    if (v == helpKeyword)
                    {
                        DisplayUsage();
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
