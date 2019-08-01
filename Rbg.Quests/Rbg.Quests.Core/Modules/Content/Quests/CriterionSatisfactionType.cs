namespace Rbg.Quests.Core.Modules.Content.Quests
{
    /**
     * How the criterion should be satisfied when checked;
     * against a target/threshold Y, the count X can be:
     *
     * X == Y, X >= Y, X <= Y, X > Y, X < Y
     */
    public enum CriterionSatisfactionType
    {
        // hit the bullseye with exactly 3 darts
        CountMatchesTargetExactly = 0,

        // e.g. >=, kill 10 or more monsters
        CountMatchesOrExceedsTarget = 1,

        // e.g. <=, complete the mission in 60 seconds or less
        CountMatchesOrUndercutsTarget = 2,

        // e.g. score over 10,000 points
        CountExceedsTarget = 3,

        // complete the race in under 60 seconds
        CountUndercutsTarget = 4
    }
}
