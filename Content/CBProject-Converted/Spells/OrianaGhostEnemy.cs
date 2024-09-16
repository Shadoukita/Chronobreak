namespace Buffs
{
    public class OrianaGhostEnemy : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
        };
        ObjAIBase caster;
        EffectEmitter temp;
        bool pickUp; // UNUSED
        float[] effect0 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public override void OnActivate()
        {
            Vector3 currentPos = GetUnitPosition(owner);
            ObjAIBase caster = GetBuffCasterUnit();
            this.caster = caster;
            if (caster != owner)
            {
                SpellEffectCreate(out temp, out _, "Oriana_Ghost_bind.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, currentPos, owner, default, currentPos, false, default, default, false);
            }
            pickUp = false;
        }
        public override void OnDeactivate(bool expired)
        {
            AttackableUnit caster;
            caster = this.caster; //
            if (caster != owner)
            {
                SpellEffectRemove(temp);
            }
            bool dropBall = false;
            if (expired)
            {
                dropBall = true;
            }
            else if (IsDead(owner))
            {
                dropBall = true;
            }
            if (dropBall)
            {
                caster = this.caster;
                TeamId teamID = GetTeamID_CS(caster);
                Vector3 targetPos = GetUnitPosition(owner);
                Minion other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, default, true, (Champion)caster);
                AddBuff(attacker, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            float distance = DistanceBetweenObjects(attacker, owner);
            if (distance > 1250)
            {
                pickUp = true;
                SpellBuffClear(owner, nameof(Buffs.OrianaGhostEnemy));
                Vector3 castPos = GetUnitPosition(owner);
                AddBuff(attacker, attacker, new Buffs.OrianaReturn(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                SpellCast(attacker, attacker, attacker.Position3D, attacker.Position3D, 4, SpellSlotType.ExtraSlots, 1, false, true, false, false, false, true, castPos);
            }
            else
            {
                bool noRender = GetNoRender(owner);
                if (owner is Champion && noRender)
                {
                    SpellBuffClear(owner, nameof(Buffs.OrianaGhostEnemy));
                }
            }
        }
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncPercentMovementSpeedMod(owner, effect0[level - 1]);
        }
    }
}