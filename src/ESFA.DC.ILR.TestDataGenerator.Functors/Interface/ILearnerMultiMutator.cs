using System.Collections.Generic;

namespace DCT.TestDataGenerator.Functor
{
    public interface ILearnerMultiMutator
    {
        /// <summary>
        /// Functors can specify what type of learner to create that will be passed back into the Mutate function
        /// If multiple learners are required to test a rule then create multiple mutate functions (if required - they could all be the same).
        /// Each learner generated can manipulate the options to be used as well.
        /// </summary>
        /// <param name="dataCache">Reference data cache</param>
        /// <returns></returns>
        IEnumerable<LearnerTypeMutator> LearnerMutators(ILearnerCreatorDataCache dataCache);

        /// <summary>
        /// Which rule (as per the valdiation specification) does this functor operate to e.g. GivenName_01
        /// The length of the name is important as this will be concatonated with the learner number to form a learner ref number
        /// </summary>
        /// <returns></returns>
        string RuleName();

        /// <summary>
        /// Certain rules require a very specific "time of year" for the file preparation time. Many rules simply don't care.
        /// </summary>
        /// <returns></returns>
        FilePreparationDateRequired FilePreparationDate();
    }
}
