using System;
using System.Linq;
using System.Collections.Generic;
using GameServerCore;
using Newtonsoft.Json;
using JO = Newtonsoft.Json.JsonObjectAttribute;
using MS = Newtonsoft.Json.MemberSerialization;

namespace Chronobreak.GameServer.GameObjects.StatsNS
{
    [JO(MS.OptIn)]
    public class MoveSpeed
    {
        [JsonProperty] private float _flatPerm;
        [JsonProperty] private float _flatBonusPerm;
        [JsonProperty] private float _percentBonusPerm;
        [JsonProperty] private List<float> _multiplicativeBonusesPerm = [];
        [JsonProperty] private float _slowResistPerm;

        [JsonProperty] private float _flatTemp;
        [JsonProperty] private float _flatBonusTemp;
        [JsonProperty] private float _percentBonusTemp;
        [JsonProperty] private List<float> _multiplicativeBonusesTemp = [];
        [JsonProperty] private float _slowResistTemp;

        private IEnumerable<float> _multiplicativeBonuses => _multiplicativeBonusesPerm.Concat(_multiplicativeBonusesTemp);

        public float Flat
        {
            get => _flatPerm + _flatTemp;
            internal set => SetValue(ref _flatPerm, value);
        }

        public float FlatBonus
        {
            get => _flatBonusPerm + _flatBonusTemp;
            internal set => SetValue(ref _flatBonusPerm, value);
        }

        public float PercentBonus
        {
            get => _percentBonusPerm + _percentBonusTemp;
            internal set => SetValue(ref _percentBonusPerm, value);
        }

        public float SlowResist
        {
            get => _slowResistPerm + _slowResistTemp;
            internal set => SetValue(ref _slowResistPerm, value);
        }

        private bool StatModified =>
            Extensions.COMPARE_EPSILON < Math.Abs(Flat)
            || Extensions.COMPARE_EPSILON < Math.Abs(FlatBonus)
            || Extensions.COMPARE_EPSILON < Math.Abs(PercentBonus)
            || _multiplicativeBonuses.Count() <= 0
            || Extensions.COMPARE_EPSILON < Math.Abs(SlowResist);

        private float _total;
        private bool toUpdate = true;
        public float Total => toUpdate ? _total = CalculateTrueMoveSpeed() : _total;

        public void IncBaseFlat(float by) => IncrementValue(ref _flatTemp, by);
        public void IncFlatBonus(float by) => IncrementValue(ref _flatBonusTemp, by);
        public void IncFlatBonusPerm(float by) => IncrementValue(ref _flatBonusPerm, by);
        public void IncBonusPercent(float by) => IncrementValue(ref _percentBonusTemp, by);
        public void IncBonusPercentPerm(float by) => IncrementValue(ref _percentBonusPerm, by);
        public void IncBonusMultiplicativePercent(float by)
        {
            if (by != 0)
            {
                _multiplicativeBonusesTemp.Add(by);
                toUpdate = true;
            }
        }
        public void IncBonusMultiplicativePercentPerm(float by)
        {
            if (by != 0)
            {
                _multiplicativeBonusesPerm.Add(by);
                toUpdate = true;
            }
        }

        public void ApplyStatModifier(MoveSpeed msMod)
        {
            if (msMod.StatModified)
            {
                _flatPerm += msMod.Flat;
                _flatBonusPerm += msMod.FlatBonus;
                foreach (var bonus in msMod._multiplicativeBonuses)
                {
                    _multiplicativeBonusesPerm.Add(bonus);
                }
                _slowResistPerm += msMod.SlowResist;
                toUpdate = true;
            }
        }

        public void RemoveStatModifier(MoveSpeed msMod)
        {
            if (msMod.StatModified)
            {
                _flatPerm -= msMod.Flat;
                _flatBonusPerm -= msMod.FlatBonus;
                foreach (var bonus in msMod._multiplicativeBonuses)
                {
                    _multiplicativeBonusesPerm.Remove(bonus);
                }
                _slowResistPerm -= msMod.SlowResist;
                toUpdate = true;
            }
        }

        private float CalculateTrueMoveSpeed()
        {
            float speed = (Flat + FlatBonus) * (1 + PercentBonus);

            if (_multiplicativeBonuses.Any())
            {
                var bonuses = _multiplicativeBonuses.Where(p => p > 0);
                foreach (var bonus in bonuses)
                {
                    speed *= 1 + bonus;
                }

                var slows = _multiplicativeBonuses.Where(p => p < 0).OrderDescending();
                float nextSlowStrength = 1; // the strongest one fully applied
                foreach (var slow in slows)
                {
                    //TODO: Determine the order of application of nextSlowStrength and _slowResist
                    speed *= 1 + slow * nextSlowStrength - SlowResist;
                    nextSlowStrength = 0.35f; // the others applied with 65% reduced effectiveness
                }
            }

            if (speed > 490.0f)
            {
                speed = speed * 0.5f + 230; // 230 = (415*(1-0.5f) + (490-415)*(0.8f-0.5f)
            }
            else if (speed >= 415.0f)
            {
                speed = speed * 0.8f + 83; // 83 = 415*(1-0.8f)
            }
            else if (speed < 220.0f)
            {
                speed = speed * 0.5f + 110; // 110 = 220*(1-0.5f)
            }

            return speed;
        }

        public void ClearSlows()
        {
            _multiplicativeBonusesPerm.RemoveAll(p => p < 0);
            toUpdate = true;
        }

        internal void ResetToPermanent()
        {
            _flatTemp = 0;
            _flatBonusTemp = 0;
            _percentBonusTemp = 0;
            _multiplicativeBonusesTemp.Clear();
            _slowResistTemp = 0;
            toUpdate = true;
        }

        private void IncrementValue(ref float value, float by)
        {
            value += by;
            toUpdate = by != 0 || toUpdate;
        }
        private void SetValue(ref float value, float to)
        {
            toUpdate = value != to;
            value = to;
        }
    }
}