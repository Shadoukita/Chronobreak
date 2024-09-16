namespace Spells
{
    public class GravesSmokeGrenadeBoom : SpellScript
    {
        float[] effect0 = { 4.5f, 4.5f, 4.5f, 4.5f, 4.5f };
        int[] effect1 = { 5, 5, 5, 5, 5 };
        int[] effect2 = { 60, 110, 160, 210, 260 };
        /*
        //TODO: Uncomment and fix
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID, HitResult hitResult)
        {
            float dmg; // UNITIALIZED
            Vector3 targetPos = GetUnitPosition(target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            TeamId teamID = GetTeamID(owner);
            float buffDuration = this.effect0[level - 1];
            SpellEffectCreate(out particle, out _, "Graves_SmokeGrenade_Boom.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            float aD = GetFlatPhysicalDamageMod(owner);
            float bonusDamage = aD * 0.6f;
            float totalDamage = bonusDamage + dmg;
            float remainder = buffDuration % 0.5f;
            float ticks = buffDuration - remainder;
            float tickDamage = totalDamage / ticks; // UNUSED
            Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 50, false, true, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.GravesSmokeGrenade(), 1, 1, this.effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, attacker);
                if(unit is Champion)
                {
                    string name = GetUnitSkinName(unit);
                    string checkName = "Nocturne";
                    if(checkName == name)
                    {
                        AddBuff(attacker, unit, new Buffs.GravesSmokeGrenadeSecretPassive(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, true);
                    }
                }
            }
        }
        */
    }
}
namespace Buffs
{
    public class GravesSmokeGrenadeBoom : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Nearsight_glb.troy", },
            BuffName = "GravesSmokeGrenade",
            BuffTextureName = "GravesSmokeGrenade.dds",
        };
        float sightReduction;
        public GravesSmokeGrenadeBoom(float sightReduction = default)
        {
            this.sightReduction = sightReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.sightReduction);
            IncPermanentFlatBubbleRadiusMod(owner, sightReduction);
        }
        public override void OnDeactivate(bool expired)
        {
            float sightReduction = this.sightReduction * -1;
            IncPermanentFlatBubbleRadiusMod(owner, sightReduction);
        }
        public override void OnUpdateStats()
        {
            ApplyNearSight(attacker, owner, 0.25f);
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.GravesSmokeGrenadeBoomSlow)) == 0)
            {
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (attacker is Champion)
            {
                float distance = DistanceBetweenObjects(attacker, owner);
                if (distance < 800)
                {
                    TeamId teamID = GetTeamID_CS(owner);
                    Region pineapple = AddUnitPerceptionBubble(teamID, 75, attacker, 1, default, default, false); // UNUSED
                }
            }
        }
    }
}