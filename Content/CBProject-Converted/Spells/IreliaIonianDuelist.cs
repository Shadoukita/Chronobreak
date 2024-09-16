namespace Buffs
{
    public class IreliaIonianDuelist : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "IreliaIonianDuelist",
            BuffTextureName = "Irelia_IonianFervor.dds",
        };
        EffectEmitter particle1;
        int lastCount;
        public override void OnActivate()
        {
            float totalBonus = 0;
            int count = GetBuffCountFromAll(attacker, nameof(Buffs.IreliaIonianDuelist));
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IreliaIonianDuelist)) > 0)
            {
                if (count == 1)
                {
                    totalBonus = 0 + 10;
                    SpellEffectCreate(out particle1, out _, "irelia_new_passive_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_BACK_2", default, owner, default, default, false);
                }
                if (count == 2)
                {
                    totalBonus = 0 + 25;
                    SpellEffectCreate(out particle1, out _, "irelia_new_passive_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_BACK_2", default, owner, default, default, false);
                }
                if (count == 3)
                {
                    if (lastCount != 3)
                    {
                        totalBonus = 0 + 40;
                        SpellEffectCreate(out particle1, out _, "irelia_new_passive_03.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_BACK_2", default, owner, default, default, false);
                    }
                }
                lastCount = count;
            }
            SetBuffToolTipVar(1, totalBonus);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
    }
}