namespace Spells
{
    public class BansheesVeil : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class BansheesVeil : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "BansheesVeil",
            BuffTextureName = "066_Sindoran_Shielding_Amulet.dds",
        };
        bool willRemove;
        EffectEmitter a;
        public BansheesVeil(bool willRemove = default)
        {
            this.willRemove = willRemove;
        }
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            if (willRemove)
            {
                return owner.Team == attacker.Team;
            }
            else if (duration == 37037)
            {
                SpellEffectCreate(out _, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                willRemove = true;
                return false;
            }
            return true;
        }
        public override void OnActivate()
        {
            //RequireVar(this.willRemove);
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.XerathAscended)) > 0)
            {
                SpellEffectCreate(out a, out _, "bansheesveil_buf_tempXerath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CENTER_LOC", default, owner, default, default, false, true, false, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "bansheesveil_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, true, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
        }
        public override void OnUpdateStats()
        {
            if (willRemove)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BansheesVeilTimer(), 1, 1, 45, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.BansheesVeil), (ObjAIBase)owner, 0);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (willRemove)
            {
                damageAmount = 0;
            }
        }
        public override void OnBeingSpellHit(ObjAIBase attacker, Spell spell, SpellScriptMetadata spellVars)
        {
            SetTriggerUnit(attacker);
            ObjAIBase owner = GetBuffCasterUnit();
            if (owner.Team != attacker.Team)
            {
                bool isAttack = GetIsAttackOverride(spell);
                if (!isAttack)
                {
                    if (!spellVars.DoesntBreakShields)
                    {
                        willRemove = true;
                        SpellEffectCreate(out _, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                    }
                    else if (spellVars.DoesntBreakShields)
                    {
                    }
                    else if (!spellVars.DoesntTriggerSpellCasts)
                    {
                        willRemove = true;
                        SpellEffectCreate(out _, out _, "SpellEffect_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                    }
                }
            }
        }
    }
}