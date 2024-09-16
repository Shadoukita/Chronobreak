namespace Spells
{
    public class UdyrTigerStance : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.15f, 0.2f, 0.25f, 0.3f, 0.35f };
        float[] effect1 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrBearStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrBearStance), owner, 0);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrPhoenixStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrPhoenixStance), owner, 0);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTurtleStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTurtleStance), owner, 0);
            }
            float cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 1.5f;
            float currentCD = GetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            float nextBuffVars_activeAttackSpeed = effect0[level - 1];
            float nextBuffVars_passiveAttackSpeed = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.UdyrTigerStance(nextBuffVars_passiveAttackSpeed), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.UdyrTigerPunch(nextBuffVars_activeAttackSpeed), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "TigerStance.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            AddBuff(owner, owner, new Buffs.UdyrTigerShred(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class UdyrTigerStance : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrTigerStance",
            BuffTextureName = "Udyr_TigerStance.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 1,
        };
        int casterID; // UNUSED
        EffectEmitter tiger;
        float passiveAttackSpeed;
        int[] effect0 = { 40, 90, 140, 190, 240 };
        float[] effect1 = { 0.15f, 0.2f, 0.25f, 0.3f, 0.35f };
        public UdyrTigerStance(float passiveAttackSpeed = default)
        {
            this.passiveAttackSpeed = passiveAttackSpeed;
        }
        public override void OnActivate()
        {
            casterID = PushCharacterData("UdyrTiger", owner, false);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out tiger, out _, "tigerpelt.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, true, default, default, false, false);
            OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, true);
            //RequireVar(this.passiveAttackSpeed);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(tiger);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTigerPunch)) == 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTigerShred), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, passiveAttackSpeed);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (charVars.HitOnce)
            {
                charVars.HitOnce = false;
                TeamId teamID = GetTeamID_CS(owner); // UNUSED
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float baseDamage = effect0[level - 1];
                float tAD = GetTotalAttackDamage(owner);
                float dotDamage = tAD * 1.7f;
                dotDamage += baseDamage;
                dotDamage *= 0.25f;
                float nextBuffVars_DotDamage = dotDamage;
                if (target is ObjAIBase)
                {
                    if (target is not BaseTurret)
                    {
                        AddBuff(attacker, target, new Buffs.UdyrTigerPunchBleed(nextBuffVars_DotDamage), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    }
                }
                SpellEffectRemove(charVars.Lhand);
                SpellEffectRemove(charVars.Rhand);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 0)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                passiveAttackSpeed = effect1[level - 1];
            }
        }
    }
}