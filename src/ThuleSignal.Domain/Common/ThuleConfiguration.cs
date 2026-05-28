using System;

namespace ThuleSignal.Domain.Common
{
    public sealed class ThuleConfiguration
    {
        private static ThuleConfiguration? _instance;

        public string DefaultTrackType { get; set; } = "PODCAST";
        public int DefaultBitrate { get; set; } = 320;

        private ThuleConfiguration()
        {
            Console.WriteLine("[Singleton] Конфігурацію системи Thule-Signal успішно завантажено в пам'ять.");
        }

        public static ThuleConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThuleConfiguration();
                }
                return _instance;
            }
        }
    }
}