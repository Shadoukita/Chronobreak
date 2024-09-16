namespace Spells
{
    public class Overdrive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 90f, 90f, 90f, 18f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "PiltoverCustomsBlitz", },
        };
        float[] effect0 = { 0.16f, 0.2f, 0.24f, 0.28f, 0.32f };
        float[] effect1 = { 0.3f, 0.38f, 0.46f, 0.54f, 0.62f };
        int[] effect2 = { 8, 8, 8, 8, 8 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            float nextBuffVars_AttackSpeedMod = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.Overdrive(nextBuffVars_AttackSpeedMod, nextBuffVars_MoveSpeedMod), 1, 1, effect2[level - 1], BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Overdrive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Overdrive",
            BuffTextureName = "Blitzcrank_Overdrive.dds",
        };
        float attackSpeedMod;
        float moveSpeedMod;
        int blitzcrankID;
        EffectEmitter one;
        EffectEmitter two;
        EffectEmitter three;
        EffectEmitter four;
        EffectEmitter five;
        EffectEmitter six;
        EffectEmitter seven;
        EffectEmitter eight;
        EffectEmitter classicOverdrive;
        EffectEmitter wheelOne;
        EffectEmitter wheelTwo;
        public Overdrive(float attackSpeedMod = default, float moveSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.moveSpeedMod);
            blitzcrankID = GetSkinID(owner);
            if (blitzcrankID == 4)
            {
                SpellEffectCreate(out one, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_L_1", default, owner, default, default, false);
                SpellEffectCreate(out two, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_L_2", default, owner, default, default, false);
                SpellEffectCreate(out three, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_L_3", default, owner, default, default, false);
                SpellEffectCreate(out four, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_L_4", default, owner, default, default, false);
                SpellEffectCreate(out five, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_R_1", default, owner, default, default, false);
                SpellEffectCreate(out six, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_R_2", default, owner, default, default, false);
                SpellEffectCreate(out seven, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_R_3", default, owner, default, default, false);
                SpellEffectCreate(out eight, out _, "SteamGolem_Piltover_Overdrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_PIPE_R_4", default, owner, default, default, false);
                SpellEffectCreate(out classicOverdrive, out _, "Overdrive_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                SpellEffectCreate(out wheelOne, out _, "SteamGolem_Piltover_Overdrive_Tires.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BL_wheel", default, owner, default, default, false);
                SpellEffectCreate(out wheelTwo, out _, "SteamGolem_Piltover_Overdrive_Tires.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BR_wheel", default, owner, default, default, false);
            }
            else
            {
                SpellEffectCreate(out classicOverdrive, out _, "Overdrive_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (blitzcrankID == 4)
            {
                SpellEffectRemove(one);
                SpellEffectRemove(two);
                SpellEffectRemove(three);
                SpellEffectRemove(four);
                SpellEffectRemove(five);
                SpellEffectRemove(six);
                SpellEffectRemove(seven);
                SpellEffectRemove(eight);
                SpellEffectRemove(wheelOne);
                SpellEffectRemove(wheelTwo);
                SpellEffectRemove(classicOverdrive);
            }
            else
            {
                SpellEffectRemove(classicOverdrive);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }
    }
}