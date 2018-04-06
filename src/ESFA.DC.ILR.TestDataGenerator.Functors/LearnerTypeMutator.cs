using DCT.ILR.Model;

namespace DCT.TestDataGenerator.Functor
{
    /// <summary>
    /// Function to mutate the learner that will have been created.
    /// </summary>
    /// <param name="learner">The ILR Learner that will be serialised to the file</param>
    /// <param name="valid">is the function supposed to mutate the learner to valid or invalid?</param>
    public delegate void Mutate(MessageLearner learner, bool valid);

    /// <summary>
    /// Function to mutate the progression and destination reocrd that will have been created.
    /// </summary>
    /// <param name="learner">The learner's prog and dest record to be mutated.</param>
    /// <param name="valid">Is the function supposed to generate valid or invalid data</param>
    public delegate void MutateProgression(MessageLearnerDestinationandProgression learner, bool valid);

    public delegate void MutateOptions(GenerationOptions options);

    /// <summary>
    /// Each functor specifies combination of a default learner type, the options to use when generating that learner and finally how to mutate that learner for testing
    /// The functor can also mark other metadata roudn the learner that will be generated in the output file
    /// </summary>
    public class LearnerTypeMutator
    {
        public LearnerTypeMutator()
        {
            this.ExclusionRecord = false;
            this.InvalidLines = 1;
            this.ValidLines = 0;
        }

        /// <summary>
        /// Gets or sets what is the base type of learner that should be generated. LearnerTypes are generally aligned to the funding models but they don't have to be (they could be a subtype for example)
        /// </summary>
        public LearnerTypeRequired LearnerType { get; set; }

        /// <summary>
        /// Gets or sets the Mutate function is called after the default learner has been generated. The Mutate function will contain the code required to make it valid / invalid for the rule
        /// that is being tested. Often the "valid" code branch will be empty but might need code to mutate a learner from a default learner type to a specific test learner for the rule
        /// </summary>
        public Mutate DoMutateLearner { get; set; }

        /// <summary>
        /// Gets or sets progression and Destination records can also be generated (depending on the options selected). These are a top level entity within the ILR
        /// </summary>
        public MutateProgression DoMutateProgression { get; set; }

        /// <summary>
        /// Gets or sets the options to be used before the default learner is created. This can be useful for creating FAMs or LD FAMs for example by default and then mutating them afterwards
        /// </summary>
        public MutateOptions DoMutateOptions { get; set; }

        /// <summary>
        /// Gets or sets if the data is valid (and hence clean) how many rows are expected in the error detail report. If data is clean then zero rows are expected but occasionaly valid lines may generate other warnings or similar
        /// </summary>
        public int ValidLines { get; set; }

        /// <summary>
        /// Gets or sets invalid data may often generate more than one row in the output
        /// </summary>
        public int InvalidLines { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether exclusion records are deliberately created and are NOT expected to generate rule error output for invalid rows (as the rule should then exclude them i fthe data / rule is working correctly)
        /// </summary>
        public bool ExclusionRecord { get; set; }
    }
}
