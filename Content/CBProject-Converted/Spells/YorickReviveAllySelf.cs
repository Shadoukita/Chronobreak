namespace Buffs
{
    public class YorickReviveAllySelf : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "yorick_ult_revive_tar.troy", "", },
            BuffName = "YorickOmenPreDeath",
            BuffTextureName = "YorickOmenOfDeath.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        bool isKarthus;
        bool isKogMaw;
        EffectEmitter particle3;
        EffectEmitter particle4;
        EffectEmitter particle;
        public override void OnActivate()
        {
            isKarthus = false;
            isKogMaw = false;
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle3, out particle4, "yorick_ult_01_teamID_green.troy", "yorick_ult_01_teamID_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle, out particle, "yorick_ult_02.troy", default, teamID, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathDefied)) > 0)
            {
                isKarthus = true;
                SpellBuffRemove(owner, nameof(Buffs.DeathDefied), (ObjAIBase)owner, 0);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurpriseReady)) > 0)
            {
                isKogMaw = true;
                SpellBuffRemove(owner, nameof(Buffs.KogMawIcathianSurpriseReady), (ObjAIBase)owner, 0);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle3);
            SpellEffectRemove(particle4);
            if (expired)
            {
                if (isKarthus)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                if (isKogMaw)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            else
            {
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRADelayLich)) == 0 && GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRADelayKogMaw)) == 0)
                {
                    if (isKarthus)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                    if (isKogMaw)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (owner is Champion)
            {
                becomeZombie = true;
            }
        }
        public override void OnZombie(ObjAIBase attacker)
        {
            AddBuff((ObjAIBase)owner, attacker, new Buffs.YorickUltPetActive(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.INVISIBILITY);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.COMBAT_ENCHANCER);
            if (isKarthus)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.YorickRADelayLich(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else if (isKogMaw)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.YorickRADelayKogMaw(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.YorickRADelay(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            TeamId teamID = GetTeamID_CS(owner);
            foreach (Champion unit in GetChampions(teamID, nameof(Buffs.YorickRARemovePet), true))
            {
                SpellBuffClear(unit, nameof(Buffs.YorickRARemovePet));
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRADelay)) > 0)
            {
                damageAmount = 0;
            }
        }
    }
}