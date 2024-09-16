namespace Buffs
{
    public class Backstab : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Backstab",
            BuffTextureName = "Jester_CarefulStrikes.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float[] effect0 = { -0.2f, -0.225f, -0.25f, -0.275f, -0.3f };
        float[] effect1 = { 0.2f, 0.225f, 0.25f, 0.275f, 0.3f };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.TwoShivPoison) && IsInFront(owner, target) && IsBehind(target, owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CastFromBehind(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FromBehind)) > 0)
                {
                    damageAmount *= 1.2f;
                    SpellEffectCreate(out _, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                }
                else
                {
                    if (IsInFront(owner, target))
                    {
                        if (IsBehind(target, owner))
                        {
                            damageAmount *= 1.2f;
                            SpellEffectCreate(out _, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                        }
                    }
                }
                TeamId teamID = GetTeamID_CS(owner);
                ObjAIBase attacker = GetChampionBySkinName("Shaco", teamID);
                int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float time = GetSlotSpellCooldownTime(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level >= 1)
                {
                    if (time <= 0)
                    {
                        if (hitResult != HitResult.HIT_Dodge)
                        {
                            if (hitResult != HitResult.HIT_Miss)
                            {
                                float nextBuffVars_MoveSpeedMod = effect0[level - 1];
                                float nextBuffVars_MissChance = effect1[level - 1];
                                AddBuff(attacker, target, new Buffs.TwoShivPoison(nextBuffVars_MoveSpeedMod, nextBuffVars_MissChance), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            if (target is ObjAIBase && target is not BaseTurret && IsInFront(owner, target) && IsBehind(target, owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FromBehind(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}