using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCT.TestDataGenerator;
using DCT.TestDataGenerator.Functor;
using FluentAssertions;
using Xunit;

namespace DCT.TestDataGenerator.Test
{
    public class XmlGeneratorTests
    {
        private List<ILearnerMultiMutator> _functors = new List<ILearnerMultiMutator>(100);

        [Fact]
        public void CreateAllXml_NullRules_NullArgumentToException()
        {
            int UKPRN = 8;
            XmlGenerator generator = new XmlGenerator(null, UKPRN);
            Action call = () => { generator.CreateAllXml(null, 1, XmlGenerator.ESFA201819Namespace); };
            call.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void CreateAllXml_EmptyRulesList_KeyNotFoundException()
        {
            int UKPRN = 8;
            RuleToFunctorParser rfp = new RuleToFunctorParser(new DataCache());
            XmlGenerator generator = new XmlGenerator(rfp, UKPRN);
            List<ActiveRuleValidity> rules = new List<ActiveRuleValidity>();
            Action call = () => { generator.CreateAllXml(rules, 1, XmlGenerator.ESFA201819Namespace).ToList(); };
            call.Should().Throw<KeyNotFoundException>();
            
        }

        [Fact]
        public void CreateAllXml_LearnerOutputSize_FunctorDesiredLearnerCount()
        {
            _functors = new List<ILearnerMultiMutator>(100);
            int UKPRN = 8;
            var cache = new DataCache();
            RuleToFunctorParser rfp = new RuleToFunctorParser(cache);
            rfp.CreateFunctors(AddFunctor);
            XmlGenerator generator = new XmlGenerator(rfp, UKPRN);
            List<ActiveRuleValidity> rules = new List<ActiveRuleValidity>() { new ActiveRuleValidity() { RuleName = "ULN_03", Valid = false } }; ;
            var result = generator.CreateAllXml(rules, 1, XmlGenerator.ESFA201819Namespace).ToList();
            
            result.Count.Should().Be(_functors.Where(s => s.RuleName() == rules[0].RuleName).First().LearnerMutators(cache).ToList().Count());
        }

        private void AddFunctor(ILearnerMultiMutator i)
        {
            _functors.Add(i);
        }
    }
}
