using System.Collections.Generic;
using Rbg.Quests.Core.Modules.Content.Quests;

namespace Rbg.Quests.Core.Modules.State.Quests
{
    public class QuestProgress
    {
        public Dictionary<string, QuestCriterion> Criteria { get; } = new Dictionary<string, QuestCriterion>();

        // TODO:
        public static QuestProgress From(QuestContent questContent)
        {
            return new QuestProgress();
        }

        private QuestProgress()
        {
        }
    }
}
