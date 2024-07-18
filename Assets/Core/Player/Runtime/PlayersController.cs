using System.Collections.Generic;
using System.Linq;
using Core.Base.Map;
using Core.Base.Save;
using VContainer;
using VContainer.Unity;

namespace Core.Player
{
    public class PlayersController : IInitializable
    {
        [Inject] private readonly SaveHelper m_SaveHelper;
        private PlayerData m_Current;
        private BaseMap<string, PlayerData> m_All;

        public void Initialize()
        {
            m_Current = m_SaveHelper.Read<PlayerData>(PlayerDefinition.CurrentPlayerSaveKey);
            var all = m_SaveHelper.Read<PlayersData>(PlayerDefinition.AllPlayersSaveKey) ?? new PlayersData();
            m_All = new PlayersMap();
            if (all.Datas == null)
            {
                return;
            }

            foreach (var data in all.Datas)
            {
                m_All.Add(data.Name, data);
            }
        }

        public PlayerData GetCurrent()
        {
            return m_Current;
        }

        public List<PlayerData> GetAll()
        {
            return m_All.GetValues().ToList();
        }

        public void UpdateCurrent(PlayerData value)
        {
            if (!m_All.Contains(value.Name))
            {
                m_All.Add(value.Name, value);
            }

            var data = m_All.GetValue(value.Name);
            m_Current = data;

            m_SaveHelper.Write(PlayerDefinition.CurrentPlayerSaveKey, m_Current);
        }

        public void UpdateAll(List<PlayerData> value)
        {
            foreach (var data in value)
            {
                if (!m_All.Contains(data.Name))
                {
                    m_All.Add(data.Name, data);
                }

                var d = m_All.GetValue(data.Name);
                d.BestScore = data.BestScore;
            }

            var all = new PlayersData()
            {
                Datas = m_All.GetValues().ToArray(),
            };
            m_SaveHelper.Write(PlayerDefinition.AllPlayersSaveKey, all);
        }

        public bool Exist(string value)
        {
            return m_All.Contains(value);
        }

        public void Save()
        {
            m_SaveHelper.SaveAll();
        }
    }
}