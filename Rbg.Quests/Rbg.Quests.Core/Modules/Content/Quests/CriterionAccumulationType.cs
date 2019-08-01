namespace Rbg.Quests.Core.Modules.Content.Quests
{
    /**
     * When the criterion has a count applied to it, should the count
     * be incremented cumulatively (e.g. total score) or should only higher
     * or lower values be counted (e.g. top score for a level)?
     */
    public enum CriterionAccumulationType
    {
        Cumulative = 0,
        AcceptHigherValuesOnly = 1,
        AcceptLowerValuesOnly = 2,
        AcceptAnyValue = 3,
        
        // do not count cumulatively at all, but only increment if threshold is beaten
        // e.g. "score 10 or more points 3 times in a row"
        SatisfyThresholdOrZero = 4
    }
}
