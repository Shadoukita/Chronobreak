using Newtonsoft.Json;
using JO = Newtonsoft.Json.JsonObjectAttribute;
using MS = Newtonsoft.Json.MemberSerialization;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    [JO(MS.OptIn)]
    public class Stat
    {
        [JsonProperty] protected float _baseFlatPerm;
        [JsonProperty] protected float _basePercentPerm;
        [JsonProperty] protected float _flatBonusPerm;
        [JsonProperty] protected float _percentBonusPerm;

        [JsonProperty] protected float _baseFlatTemp;
        [JsonProperty] protected float _basePercentTemp;
        [JsonProperty] protected float _flatBonusTemp;
        [JsonProperty] protected float _percentBonusTemp;

        protected float _baseFlat => _baseFlatPerm + _baseFlatTemp;
        protected float _basePercent => _basePercentPerm + _basePercentTemp;
        protected float _flatBonus => _flatBonusPerm + _flatBonusTemp;
        protected float _percentBonus => _percentBonusPerm + _percentBonusTemp;

        protected float _totalFlat => _baseFlat + _flatBonus;
        protected float _totalPercent => _basePercent + _percentBonus;
        protected float _totalBase => _baseFlat * (1 + _basePercent);
        protected float _totalBonus => _flatBonus * (1 + _percentBonus);

        public void ApplyStatModifier(Stat statModifier)
        {
            _baseFlatPerm += statModifier._baseFlat;
            _basePercentPerm += statModifier._basePercent;
            _flatBonusPerm += statModifier._flatBonus;
            _percentBonusPerm += statModifier._percentBonus;
        }

        public void RemoveStatModifier(Stat statModifier)
        {
            _baseFlatPerm -= statModifier._baseFlat;
            _basePercentPerm -= statModifier._basePercent;
            _flatBonusPerm -= statModifier._flatBonus;
            _percentBonusPerm -= statModifier._percentBonus;
        }

        internal void ResetToPermanent()
        {
            _baseFlatTemp = 0;
            _basePercentTemp = 0;
            _flatBonusTemp = 0;
            _percentBonusTemp = 0;
        }
    }
}