namespace Spells
{
    public class HallucinateApplicator : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class HallucinateApplicator : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hallucinate",
            BuffTextureName = "Jester_HallucinogenBomb.dds",
            IsPetDurationBuff = true,
        };
        float damageAmount;
        float damageDealt;
        float damageTaken;
        float shacoDamageTaken;
        public HallucinateApplicator(float damageAmount = default, float damageDealt = default, float damageTaken = default, float shacoDamageTaken = default)
        {
            this.damageAmount = damageAmount;
            this.damageDealt = damageDealt;
            this.damageTaken = damageTaken;
            this.shacoDamageTaken = shacoDamageTaken;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAmount);
            //RequireVar(this.damageDealt);
            //RequireVar(this.damageTaken);
            //RequireVar(this.shacoDamageTaken);
            SetStunned(owner, true);
            SetTargetable(owner, false);
            SetNoRender(owner, true);
            Vector3 ownerPos = GetUnitPosition(owner);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "HallucinatePoof.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
            SetNoRender(owner, false);
            SetTargetable(owner, true);
            Vector3 pos1 = GetRandomPointInAreaUnit(owner, 250, 50);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            Pet other1 = CloneUnitPet(owner, nameof(Buffs.Hallucinate), 18, pos1, 0, 0, true);
            TeamId teamID = GetTeamID_CS(owner);
            float nextBuffVars_DamageAmount = damageAmount;
            float nextBuffVars_DamageDealt = damageDealt;
            float nextBuffVars_DamageTaken = damageTaken;
            float nextBuffVars_shacoDamageTaken = shacoDamageTaken; // UNUSED
            AddBuff((ObjAIBase)owner, other1, new Buffs.HallucinateFull(nextBuffVars_DamageAmount, nextBuffVars_DamageDealt, nextBuffVars_DamageTaken), 1, 1, 18, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other1, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other1, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other1, new Buffs.Backstab(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            SpellEffectCreate(out _, out _, "HallucinatePoof.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, default, target, default, default, true, false, false, false, false);
            SetStealthed(other1, false);
            Vector3 pos2 = GetRandomPointInAreaUnit(owner, 250, 50);
            TeleportToPosition(owner, pos2);
            SpellEffectCreate(out _, out _, "HallucinatePoof.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
        }
    }
}