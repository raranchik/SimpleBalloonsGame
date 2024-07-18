using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Core.Input.Base
{
    public class InputsRunner : ITickable
    {
        [Inject] private readonly IReadOnlyList<IInputHandler> m_Handlers;

        public void Tick()
        {
            if (m_Handlers.Count <= 0)
            {
                return;
            }

            foreach (var inputHandler in m_Handlers)
            {
                inputHandler.Run();
            }
        }
    }
}