using Game339.Shared.Infastructure.Diagnostics;

namespace Game.Runtime.Infastructure
{
    public class UnityGameLogger : IGameLog
    {
        public void Info(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public void Warn(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public void Error(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}
