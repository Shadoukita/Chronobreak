namespace Spells
{
    public class HowlingGale : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
            int count = GetBuffCountFromAll(owner, nameof(Buffs.HowlingGale));
            if (count >= 1)
            {
                SpellBuffRemove(owner, nameof(Buffs.HowlingGale), owner, 0);
            }
            else
            {
                PlayAnimation("Spell1", 0, owner, false, false, false);
                Vector3 targetPos = GetSpellTargetPos(spell);
                FaceDirection(owner, targetPos);
                Vector3 castPos = GetPointByUnitFacingOffset(owner, 100, 0);
                Vector3 facePos = GetPointByUnitFacingOffset(owner, 200, 0);
                Vector3 nextBuffVars_CastPos = castPos;
                Vector3 nextBuffVars_FacePos = facePos;
                float nextBuffVars_LifeTime = 0;
                int nextBuffVars_Level = level;
                AddBuff(owner, owner, new Buffs.HowlingGale(nextBuffVars_FacePos, nextBuffVars_LifeTime, nextBuffVars_Level, nextBuffVars_CastPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SetTargetingType(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
            }
        }
    }
}
namespace Buffs
{
    public class HowlingGale : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Howling Gale",
            BuffTextureName = "Janna_HowlingGale.dds",
            SpellToggleSlot = 1,
        };
        Vector3 facePos;
        new float lifeTime;
        int level;
        Vector3 castPos;
        EffectEmitter particle;
        int[] effect0 = { 14, 13, 12, 11, 10 };
        public HowlingGale(Vector3 facePos = default, float lifeTime = default, int level = default, Vector3 castPos = default)
        {
            this.facePos = facePos;
            this.lifeTime = lifeTime;
            this.level = level;
            this.castPos = castPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.facePos);
            //RequireVar(this.lifeTime);
            //RequireVar(this.level);
            Vector3 castPos = GetUnitPosition(owner);
            this.castPos = castPos;
            int ownerSkinID = GetSkinID(owner);
            if (ownerSkinID == 3)
            {
                SpellEffectCreate(out particle, out _, "HowlingGale_Frost_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, this.castPos, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out particle, out _, "HowlingGale_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, this.castPos, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetingType(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Location, owner);
            SpellEffectRemove(particle);
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 castPos = this.castPos;
            Minion other1 = SpawnMinion("TestCube", "TestCubeRender", "idle.lua", this.castPos, teamID, false, true, false, true, false, true, 0, false, true, (Champion)owner);
            Vector3 unitPos2 = GetUnitPosition(other1);
            Vector3 facePos = this.facePos;
            FaceDirection(other1, facePos);
            float aPMod = GetFlatMagicDamageMod(owner);
            IncPermanentFlatMagicDamageMod(other1, aPMod);
            int nextBuffVars_Speed = 150;
            int nextBuffVars_Gravity = 45;
            int nextBuffVars_IdealDistance = 100;
            if (lifeTime <= 1)
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.HowlingGaleSpell));
            }
            else if (lifeTime <= 2)
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.HowlingGaleSpell1));
            }
            else if (lifeTime <= 3)
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.HowlingGaleSpell2));
            }
            else
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.HowlingGaleSpell3));
            }
            SpellCast((ObjAIBase)owner, default, facePos, facePos, 0, SpellSlotType.ExtraSlots, this.level, true, true, false, false, false, true, unitPos2);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float cooldownStat = GetPercentCooldownMod(owner);
            float cooldownMod = 1 + cooldownStat;
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldown = effect0[level - 1];
            cooldown *= cooldownMod;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldown);
        }
        public override void OnUpdateStats()
        {
            lifeTime = base.lifeTime;
        }
    }
}