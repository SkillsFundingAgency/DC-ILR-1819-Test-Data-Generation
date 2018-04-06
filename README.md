# DC-ILR-1819-Test-Data-Generation
This project is designed to create test data for testing the valdiation rules associated with the ILR specification
The intent is to create a very wide set of test scenarios, so all combinations boundary conditions are tested.
This may not be true when it comes to all combinations of all learning aims that would meet a rule.

Validation rules (see the spec) are split into logical parts. "learner", "learning delivery", etc

What it can do:

a.	Generate ILR files for each of the learner validation rules except for:

  i.	Ignoring any “online only” rules (for now) [mostly UKPRN related]

  ii.	LearnFAMType_03 – there are actually no codes that can hit the test

  iii.	UKPRN_03 is to do with modifying the header on the file. Decided not to code this for now (the refactor vs the benefit is marginal at this point).

  iv.	Files generated depend on “does the rule require a file generated before or after January (or doesn’t care).

  v.	Files generated also depend on whether or not “specialist college” rules exist.

b.	Choose which rules to generate data for

  i.	Each rule can be active or inactive. Inactive will not output learners.

  ii.	Each rule can generate “valid” or “invalid” data

  iii.	Each rule can output extra exception records – where you do NOT expect to see the rule in the output for the rule 

  iv.	Each rule generates boundary condition data when / where it can

  v.	Each rule generates “complete” tests for things that are “lists” of valid codes (e.g. ethnicity). Not worked out what to do for more complex learning delivery rules yet

  vi.	Each rule will output at least one learner. Learners are given a LearnRefNumber that is formed as

    1.	HEXNumericalDigits RuleName RuleNumber e.g. “1DOB01”, “EFDOB12”

  vii.	The ULN’s generated are valid format and you can have as many as you want

  viii.	The tool can “scale” the output (as a quick hack for performance testing ideas later) – so ULN_03 (which outputs 7 learners) can output 7 times table number of learners. A scale factor of 2 is each learner is “added” into the file twice.

  ix.	Learners are unique enough – so each one will have a different ULN + LearnRefNumber.

  x.	Learners generally all have the same name with é embedded in the data.

  xi.	Lots of the same learn aim ref is used. Because mostly (for validation) it doesn’t matter.
