namespace Buffs
{
    public class LeblancMIApplicator : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Fade iD; // UNUSED
        public override void OnActivate()
        {
            iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
            Vector3 ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out _, out _, "LeBlanc_MirrorImagePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, default, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            iD = PushCharacterFade(owner, 1, 0);
            if (!IsDead(owner))
            {
                Vector3 pos1 = GetRandomPointInAreaUnit(owner, 250, 50);
                int level = 1; // UNUSED
                Pet other1 = CloneUnitPet(owner, nameof(Buffs.LeblancMI), 8, pos1, 0, 0, true);
                AddBuff(other1, other1, new Buffs.LeblancPassiveCooldown(), 1, 1, 60, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)owner, other1, new Buffs.LeblancMIFull(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellEffectCreate(out _, out _, "LeBlanc_MirrorImagePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, default, other1, default, default, false, false, false, false, false);
                SpellEffectCreate(out _, out _, "LeBlanc_MirrorImagePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}