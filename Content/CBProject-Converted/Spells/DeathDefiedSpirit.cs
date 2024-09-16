namespace Buffs
{
    public class DeathDefiedSpirit : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Death Defied",
            BuffTextureName = "Lich_Untransmutable.dds",
        };
        Fade fade; // UNUSED
        float lichAP;
        public DeathDefiedSpirit(float lichAP = default)
        {
            this.lichAP = lichAP;
        }
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            fade = PushCharacterFade(owner, 0.5f, 0);
            //RequireVar(this.lichAP);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, lichAP);
        }
    }
}