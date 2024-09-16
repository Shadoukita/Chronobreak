namespace Buffs
{
    public class KennenMarkofStorm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "KennenMarkOfStorm",
            BuffTextureName = "Kennen_MarkOfStorm.dds",
        };
        bool doOnce;
        int count;
        EffectEmitter globeTwo;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            this.doOnce = false;
            int level = GetLevel(owner); // UNUSED
            count = GetBuffCountFromAll(owner, nameof(Buffs.KennenMarkofStorm));
            bool doOnce = false; // UNUSED
            if (count == 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.KennenParticleHolder(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (count == 2)
            {
                this.doOnce = true;
                SpellBuffRemove(owner, nameof(Buffs.KennenParticleHolder), (ObjAIBase)owner);
                SpellEffectCreate(out globeTwo, out _, "kennen_mos2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            }
            if (count >= 3)
            {
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.KennenMoSDiminish)) == 0)
                {
                    BreakSpellShields(owner);
                    IncPAR(attacker, 25, PrimaryAbilityResourceType.Energy);
                    SpellEffectCreate(out _, out _, "kennen_mos_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
                    ApplyStun(attacker, owner, 1.25f);
                    SpellBuffRemoveStacks(owner, attacker, nameof(Buffs.KennenMarkofStorm), 0);
                    if (target is Champion)
                    {
                        AddBuff(attacker, owner, new Buffs.KennenMoSDiminish(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
                else
                {
                    BreakSpellShields(owner);
                    IncPAR(attacker, 25, PrimaryAbilityResourceType.Energy);
                    SpellEffectCreate(out _, out _, "kennen_mos_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
                    ApplyStun(attacker, owner, 0.6f);
                    SpellBuffRemoveStacks(owner, attacker, nameof(Buffs.KennenMarkofStorm), 0);
                    if (target is Champion)
                    {
                        AddBuff(attacker, owner, new Buffs.KennenMoSDiminish(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (doOnce)
            {
                SpellEffectRemove(globeTwo);
            }
        }
    }
}