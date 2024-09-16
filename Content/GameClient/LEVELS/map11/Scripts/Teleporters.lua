local a, b, c, d, e, f, g, h, i
a = {}
b = {}
b.Name = "BlueLeft"
b.Team = TEAM_ORDER
b.Start = Make3DPoint(2600, 95, 4600)
b.End = Make3DPoint(2700, 0, 5000)
c = {}
c.Name = "BlueRight"
c.Team = TEAM_ORDER
c.Start = Make3DPoint(4600, 95, 2800)
c.End = Make3DPoint(5000, 100, 2900)
d = {}
d.Name = "PurpleLeft"
d.Team = TEAM_CHAOS
d.Start = Make3DPoint(10200, 95, 12200)
d.End = Make3DPoint(9800, 95, 12000)
e = {}
e.Name = "PurpleRight"
e.Team = TEAM_CHAOS
e.Start = Make3DPoint(12300, 95, 10300)
e.End = Make3DPoint(12200, 95, 9800)
a[1] = b
a[2] = c
a[3] = d
a[4] = e
TeleporterDefinitions = a
SpawnTeleporters = function()
    local j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, A
    j, k, l = ipairs(TeleporterDefinitions)
    for m, n in j, k, l do
        o =
            SpawnMinion(
            "S5Test_BaseTeleporter_" .. n.Name,
            "S5Test_BaseTeleporter",
            "Idle.lua",
            n.Start,
            n.Team,
            false,
            true,
            true,
            true,
            true,
            true
        )
        FaceDirection(o, n.End)
        p =
            SpawnMinion(
            "S5Test_BaseTeleporter_" .. n.Name,
            "S5Test_BaseTeleporter",
            "Idle.lua",
            n.End,
            n.Team,
            false,
            true,
            true,
            true,
            true,
            true
        )
        FaceDirection(p, n.Start)
        LinkVisibility(o, p)
    end
end
