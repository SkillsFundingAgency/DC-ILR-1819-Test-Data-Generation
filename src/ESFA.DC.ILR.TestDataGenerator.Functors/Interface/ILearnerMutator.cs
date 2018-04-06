using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    public enum LearnerTypeRequired
    {
        CommunityLearning = 10,
        YP1619 = 25,
        Adult = 35,
        Apprenticeships = 36,
        ESF = 70,
        OtherAdult = 81,
        OtherYP1619 = 82,
        NonFunded = 99
    }

    public enum FilePreparationDateRequired
    {
        None,
        December,
        January
    }

    public interface ILearnerMutator
    {
        void MutateGenerationOptions(GenerationOptions options);

        /// <summary>
        /// The required learner will have been created with base conformant data. The mutate function allows the functor to enrich the data or deliberately make it "non-conformant"
        /// </summary>
        /// <param name="learner"></param>
        /// <param name="valid"></param>
        void Mutate(MessageLearner learner, bool valid);

        /// <summary>
        /// Functors can specify what type of learner to create that will be passed backinto the Mutate function
        /// If multiple learners are required to test a rule then create multiple functors
        /// </summary>
        /// <returns></returns>
        LearnerTypeRequired LearnerType();

        /// <summary>
        /// Which rule (as per the valdiation specification) does this functor operate to e.g. GivenName_01
        /// </summary>
        /// <returns>A string literal that should be short enough that generated learner ref numbers will not be too long (8 digits maximum).</returns>
        string RuleName();

        /// <summary>
        /// Certain rules require a very specific "time of year" for the file preparation time. Many rules simply don't care.
        /// </summary>
        /// <returns>A January, December or "don't care" file date</returns>
        FilePreparationDateRequired FilePreparationDate();
    }
}
