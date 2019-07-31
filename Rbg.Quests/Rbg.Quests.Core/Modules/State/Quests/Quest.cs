using System.Collections.Generic;

namespace Rbg.Quests.Core.Modules.State.Quests
{
    /**
     * An IQuest is the STATE of a Quest, the current progress of the player's actions
     * against the Quest's criteria, which can change constantly during play, and which must be
     * persisted with the resolution of each request/event.
     *
     * Contrast with IQuestContent, which is the CONTENT of the Quest, which is immutable
     * unless altered in the data persistence layer by the game's creators. 
     */
    public interface IQuest
    {
        // id etc.
        string Id { get; }

        // progress << all stored in this object
        QuestProgress Progress { get; }

        // methods
        bool IsComplete();

        QuestDto ToDto();
    }

    public interface IQuestContent
    {
    }

    public class Quest : IQuest
    {
        private QuestDto _dto;

        private Quest(QuestDto dto)
        {
            _dto = dto;
        }

        public static Quest From(QuestDto dto)
        {
            return new Quest(dto);
        }

        public string Id => _dto.Id;

        public QuestProgress Progress { get; set; }

        public bool IsComplete()
        {
            return Progress.Criteria.TrueForAll(x => x.IsSatisfied);
        }

        public QuestDto ToDto()
        {
            return new QuestDto
            {
                Id = Id
            };
        }
    }

    public class QuestProgress
    {
        // TODO: this probably doesn't want to be a list. Probably better Dictionary<string, QuestCriterion>
        public List<QuestCriterion> Criteria { get; } = new List<QuestCriterion>();
    }

    // TODO: consider how we can handle changed quest requirements over time 
    // e.g. if we change the 3rd criterion of 3 and progress has been made against 2 of them (or even the 3rd)
    // what should we do etc.
    public class QuestCriterion
    {
        public string Id { get; private set; }

        private QuestCriterion()
        {
            /* */
        }

        public static QuestCriterion From( /* QuestCriterionContentDto dto */)
        {
            // set ID in here and setup all fields
            return new QuestCriterion( /* dto */);
        }

        // TODO: should these even be here? It's content, not state
        public CriterionAcculumationType AccumulationType { get; }
        public CriterionSatisfactionType SatisfactionType { get; }

        public int Count { get; set; }

        public bool IsSatisfied { get; set; }
    }

    /**
     * When the criterion has a count applied to it, should the count
     * be incremented cumulatively (e.g. total score) or should only higher
     * or lower values be counted (e.g. top score for a level)?
     */
    public enum CriterionAcculumationType
    {
        Cumulative = 0,
        AcceptHigherValuesOnly = 1,
        AcceptLowerValuesOnly = 2,
        AcceptAnyValue = 3
    }

    /**
     * How the criterion is satisfied when checked;
     * against a target Y, the count X can be:
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

        // don't lose 3 hostages
        CountUndercutsTarget = 4
    }

    public interface IDto
    {
        // properties for DTOs
        string Id { get; set; }

        int Version { get; set; }
    }

    public class QuestDto : IDto /*TODO: , ISerializable */
    {
        public string Id { get; set; }

        public int Version { get; set; }
    }
}
