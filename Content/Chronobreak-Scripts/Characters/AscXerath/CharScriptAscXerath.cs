/*namespace CharScripts
{
    internal class CharScriptAscXerath : CharScript
    {
        public override void OnActivate()
        {
            AddBuff("ResistantSkinDragon", 25000.0f, 1, null, Owner, Owner);
            var buff = AddBuff("AscBuffTransfer", 5.7f, 1, null, Owner, Owner);
            ApiEventManager.OnBuffDeactivated.AddListener(this, buff, OnBuffDeactivation, true);
        }

        public void OnBuffDeactivation(Buff buff)
        {
            AddBuff("AscXerathControl", 999999.0f, 1, null, buff.TargetUnit, buff.TargetUnit as ObjAIBase);
        }
        public override void OnDeactivate()
        {
        }
        public override void OnUpdate(float diff)
        {
        }
    }
}*/
