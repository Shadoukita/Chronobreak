namespace Spells
{
    public class CassiopeiaNoxiousBlast : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 25, 38.33f, 51.66f, 65, 78.33f };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, true, (Champion)owner);
            float nextBuffVars_PoisonPerTick = effect0[level - 1];
            AddBuff(attacker, other3, new Buffs.CassiopeiaNoxiousBlast(nextBuffVars_PoisonPerTick), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CassiopeiaNoxiousBlast : BuffScript
    {
        float poisonPerTick;
        TeamId teamOfOwner;
        EffectEmitter particle;
        EffectEmitter particle2;
        float[] effect0 = { 0.15f, 0.175f, 0.2f, 0.225f, 0.25f };
        public CassiopeiaNoxiousBlast(float poisonPerTick = default)
        {
            this.poisonPerTick = poisonPerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.poisonPerTick);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle, out particle2, "CassNoxiousSnakePlane_green.troy", "CassNoxiousSnakePlane_red.troy", teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellEffectCreate(out particle, out _, "CassNoxious_tar.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                float nextBuffVars_DamagePerTick = poisonPerTick;
                AddBuff(attacker, unit, new Buffs.CassiopeiaNoxiousBlastPoison(nextBuffVars_DamagePerTick), 1, 1, 3.25f, BuffAddType.REPLACE_EXISTING, BuffType.POISON, 0, true, false, false);
                if (unit is Champion)
                {
                    float nextBuffVars_MoveSpeedMod = effect0[level - 1];
                    AddBuff(attacker, attacker, new Buffs.CassiopeiaNoxiousBlastHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}