using Core.Base.Map;
using VContainer;
using VContainer.Unity;

namespace Core.Balloons.Presets
{
    public class BalloonsPresetsInitializer : IInitializable
    {
        private readonly IBalloonPreset[] m_Presets;
        [Inject] private readonly BaseMap<string, IBalloonPreset> m_PresetsMap;

        public BalloonsPresetsInitializer(IBalloonPreset[] presets)
        {
            m_Presets = presets;
        }

        public void Initialize()
        {
            foreach (var preset in m_Presets)
            {
                m_PresetsMap.Add(preset.Id, preset);
            }
        }
    }
}