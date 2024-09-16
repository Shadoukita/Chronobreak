namespace Chronobreak.GameServer
{
    public partial class Game
    {
        public static class Time
        {
            /// <summary>
            /// How many times the server will tick per second
            /// </summary>
            internal static float TickRate { get; private set; }
            internal static float NextTimeSyncTimer { get; set; }

            /// <summary>
            /// Total time the game has been going for in milliseconds
            /// </summary>
            public static float GameTime { get; internal set; }
            /// <summary>
            /// DeltaTime in Milliseconds
            /// </summary>
            public static float DeltaTime { get; private set; }
            /// <summary>
            /// DeltaTime in Milliseconds
            /// </summary>
            public static float DeltaTimeMilliseconds { get; private set; }
            /// <summary>
            /// DeltaTime in Millisenconds
            /// </summary>
            public static float DeltaTimeSeconds { get; private set; }


            static Time()
            {
                //League public servers run at 30fps while competitive run at 60fps.
                SetTicksPerSecond(30);
                NextTimeSyncTimer = 10 * 1000;
            }

            internal static void SetTickRate(float milliseconds)
            {
                TickRate = milliseconds;
            }

            internal static void SetTicksPerSecond(int count)
            {
                TickRate = 1000.0f / count;
            }

            internal static void Update(float deltaTime)
            {
                DeltaTime = DeltaTimeMilliseconds = deltaTime;
                DeltaTimeSeconds = DeltaTime / 1000.0f;
                NextTimeSyncTimer += DeltaTime;

                //GameTime += DeltaTime;

                // By default, synchronize the game time between server and clients every 10 seconds
                if (NextTimeSyncTimer >= 10 * 1000)
                {
                    PacketNotifier.NotifySynchSimTimeS2C(GameTime);
                    NextTimeSyncTimer = 0;
                }
            }
        }
    }
}
