using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using System.Collections.Generic;
using Chronobreak.GameServer.Inventory;

namespace GameServerCore.NetInfo
{
    public class ClientInfo
    {
        public static readonly Dictionary<TeamId, byte> CurrentTeamSpawnIndex = [];
        public long PlayerId { get; private set; }
        public int ClientId { get; set; } = -1;
        public bool IsMatchingVersion { get; set; }
        /// <summary>
        /// False if the client sent a StartGame request.
        /// </summary>
        public bool IsDisconnected { get; set; }
        /// <summary>
        /// True if the client sent a Handshake request.
        /// </summary>
        public bool IsStartedClient { get; set; }
        public int SkinNo { get; private set; }
        public string[] SummonerSkills { get; private set; }
        public string Name { get; private set; }
        public string Rank { get; private set; }
        public short Ribbon { get; private set; }
        public int Icon { get; private set; }
        public TeamId Team { get; private set; }
        public byte InitialSpawnIndex { get; private set; }

        private Champion _champion;
        public Champion Champion
        {
            get => _champion;
            set
            {
                _champion = value;
                _champion.UpdateSkin(SkinNo);
            }
        }

        public RuneInventory Runes { get; private set; }
        public TalentInventory Talents { get; private set; }

        public ClientInfo
        (
            string rank,
            TeamId team,
            short ribbon,
            int icon,
            int skinNo,
            string name,
            string[] summonerSkills,
            long playerId,
            RuneInventory? runes = null,
            TalentInventory? talents = null
        )
        {
            Rank = rank;
            Team = team;
            Ribbon = ribbon;
            Icon = icon;
            SkinNo = skinNo;
            IsMatchingVersion = true;
            Name = name;
            SummonerSkills = summonerSkills;
            PlayerId = playerId;
            IsDisconnected = true;
            IsStartedClient = false;
            Runes = runes ?? new RuneInventory();
            Talents = talents ?? new TalentInventory();

            CurrentTeamSpawnIndex.TryAdd(team, 0);
            InitialSpawnIndex = CurrentTeamSpawnIndex[team]++;
        }
    }
}
