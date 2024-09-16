namespace Spells
{
    public class BantamTrap : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "AstronautTeemo", },
        };
        public override bool CanCast()
        {
            //bool canMove = GetCanMove(owner); // UNUSED
            //bool canCast = GetCanCast(owner); // UNUSED
            int count = GetBuffCountFromAll(owner, nameof(Buffs.TeemoMushrooms));
            return count > 1;
        }
        public override void SelfExecute()
        {
            float duration = GetBuffRemainingDuration(owner, nameof(Buffs.TeemoMushrooms));
            if (duration > 40)
            {
                SpellBuffRemove(owner, nameof(Buffs.TeemoMushrooms), owner, charVars.MushroomCooldown);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.TeemoMushrooms), owner, 0);
            }
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Minion other3 = SpawnMinion("Noxious Trap", "TeemoMushroom", "idle.lua", targetPos, teamID, true, true, false, false, true, false, 0, true, false, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.BantamTrap(), 1, 1, 600, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.SharedWardBuff(), 1, 1, 600, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class BantamTrap : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Noxious Trap",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
        };
        bool activated;
        int teemoSkinID;
        Fade iD; // UNUSED
        bool hasParticle;
        EffectEmitter a;
        int[] effect0 = { 50, 100, 150 };
        float[] effect1 = { -0.3f, -0.4f, -0.5f };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if (type == BuffType.FEAR)
                {
                    returnValue = false;
                }
                else if (type == BuffType.CHARM)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SILENCE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLEEP)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SLOW)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SNARE)
                {
                    returnValue = false;
                }
                else if (type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else if (type == BuffType.TAUNT)
                {
                    returnValue = false;
                }
                else if (type == BuffType.BLIND)
                {
                    returnValue = false;
                }
                else if (type == BuffType.SUPPRESSION)
                {
                    returnValue = false;
                }
                else if (type == BuffType.COMBAT_DEHANCER)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.attackSpeedMod);
            activated = false;
            teemoSkinID = GetSkinID(owner);
            if (teemoSkinID == 4)
            {
                iD = PushCharacterFade(owner, 0.3f, 1.5f);
            }
            else if (teemoSkinID == 5)
            {
                iD = PushCharacterFade(owner, 0.5f, 1.5f);
            }
            else
            {
                iD = PushCharacterFade(owner, 0.3f, 1.5f);
            }
            if (teemoSkinID == 5)
            {
                hasParticle = false;
                if (RandomChance() < 0.3f)
                {
                    SpellEffectCreate(out a, out _, "TeemoEaster2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    hasParticle = true;
                }
                else if (RandomChance() < 0.3f)
                {
                    SpellEffectCreate(out a, out _, "TeemoEaster3.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    hasParticle = true;
                }
            }
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if (teemoSkinID == 5 && hasParticle == default)
            {
                SpellEffectRemove(a);
            }
            if (!IsDead(owner))
            {
                ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.7f);
        }
        public override void OnUpdateActions()
        {
            if (lifeTime >= 2)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Stealth(), 1, 1, 600, BuffAddType.RENEW_EXISTING, BuffType.INVISIBILITY, 0, true, false, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.BantamArmor(), 1, 1, 600, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                activated = true;
            }
            if (activated)
            {
                foreach (AttackableUnit unit1 in GetClosestUnitsInArea(attacker, owner.Position3D, 160, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    activated = false;
                    TeamId teamID = GetTeamID_CS(attacker);
                    TeamId mushroomTeamID = GetTeamID_CS(owner);
                    SpellBuffRemove(owner, nameof(Buffs.Stealth), (ObjAIBase)owner, 0);
                    Vector3 ownerPos = GetUnitPosition(owner);
                    AddPosPerceptionBubble(mushroomTeamID, 700, ownerPos, 4, default, false);
                    SpellEffectCreate(out _, out _, "ShroomMine.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, false, false, false, false);
                    int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    int nextBuffVars_DamagePerTick = effect0[level - 1];
                    float nextBuffVars_MoveSpeedMod = effect1[level - 1];
                    int nextBuffVars_AttackSpeedMod = 0;
                    foreach (AttackableUnit unit2 in GetUnitsInArea(attacker, owner.Position3D, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        BreakSpellShields(unit2);
                        AddBuff(attacker, unit2, new Buffs.BantamTrapTarget(nextBuffVars_DamagePerTick), 1, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.POISON, 0, true, false, false);
                        AddBuff(attacker, unit2, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                    }
                    iD = PushCharacterFade(owner, 1, 0.75f);
                    ApplyDamage((ObjAIBase)owner, owner, 500, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
                }
            }
        }
    }
}