local a, b, c, d, e, f, g
a = {}
b = {}
b.Name = "Top"
b.Pos = Make3DPoint(2721, 22.09, 11893.56)
c = {}
c.Name = "Bot"
c.Start = Make3DPoint(11861.1, -44.44, 3208.69)
a[1] = b
a[2] = c
LaneSigilDefinitions = a
SpawnLaneSigilEffects = function()
    local h, i, j, k, l, m, n, o
    h, i, j = ipairs(LaneSigilDefinitions)
    for k, l in h, i, j do
        m = {}
        m.Pos = l.Pos
        m.EffectName = "ItemMuramanaToggle.troy"
        m.TargetPos = l.Pos
        m.FOWVisibilityRadius = 10
        m.SendIfOnScreenOrDiscard = false
        m.PersistsThroughReconnect = true
        m.BindFlexToOwnerPAR = false
        m.FollowsGroundTilt = true
        m.FacesTarget = true
        m.HideFromSpectator = false
        m.TimeoutInFOW = 99999
        m.Scale = 0.5
        SpellEffectCreateFromTable(m)
    end
end
