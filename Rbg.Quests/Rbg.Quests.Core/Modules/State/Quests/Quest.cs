using System.Linq;

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
            return Progress.Criteria.All(x => x.Value.IsSatisfied);
        }

        public QuestDto ToDto()
        {
            return new QuestDto
            {
                Id = Id
            };
        }
    }

    // TODO: consider how we can handle changed quest requirements over time 
    // e.g. if we change the 3rd criterion of 3 and progress has been made against 2 of them (or even the 3rd)
    // what should we do etc.
    public class QuestCriterion
    {
        public string Id { get; private set; }

        private QuestCriterion(string questContentId)
        {
        }

        public static QuestCriterion From( /* QuestContent questContent */)
        {
            // TODO: generate ID from quest content ID (e.g. "Quest001_Criterion002")
            return new QuestCriterion(string.Empty);
        }

        public int Count { get; set; }

        public bool IsSatisfied { get; set; }
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
