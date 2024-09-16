namespace Buffs
{
    public class TalonShadowAssaultBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TalonDisappear",
            BuffTextureName = "TalonShadowAssault.dds",
            SpellToggleSlot = 1,
        };
        Fade iD;
        EffectEmitter talon_ult_sound;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            float nextBuffVars_MoveSpeedMod = 0.4f;
            iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.TalonHaste(nextBuffVars_MoveSpeedMod), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            Vector3 ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out talon_ult_sound, out _, "talon_ult_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, default, default, ownerPos, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultMisOne), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.TalonHaste), (ObjAIBase)owner, 0);
            SetStealthed(owner, false);
            PopCharacterFade(owner, iD);
            SpellEffectRemove(talon_ult_sound);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            PopCharacterFade(owner, iD);
            SetStealthed(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultBuff), (ObjAIBase)owner, 0);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            PopCharacterFade(owner, iD);
            SetStealthed(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultBuff), (ObjAIBase)owner, 0);
        }
    }
}