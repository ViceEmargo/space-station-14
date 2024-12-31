using System.Linq;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Systems;
using Content.Shared.Verbs;
using Content.Shared.Buckle;
using Content.Shared.Buckle.Components;
using Robust.Shared.Containers;

namespace Content.Server.Weapons.Ranged.Systems;

public sealed partial class GarrisonSystem : SharedGarrisonSystem
{
    [Dependency] private readonly SharedBuckleSystem _buckle = default!;
    [Dependency] private readonly SharedGunSystem _gun = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GarrisonComponent, GetVerbsEvent<AlternativeVerb>>(OnAlternativeVerb);
        SubscribeLocalEvent<GarrisonComponent, StrappedEvent>(OnPilotBuckled);
    }

    private void OnAlternativeVerb(EntityUid uid, GarrisonComponent component, GetVerbsEvent<AlternativeVerb> args)
    {
        //TryInsertEntity(uid, args.User, component);
    }
    private void OnPilotBuckled(Entity<GarrisonComponent> entity, ref StrappedEvent args)
    {
        Log.Debug(args.Buckle.Owner + " is now piloting " + entity.Owner);
        entity.Comp.IsPiloted = true;
    }
}
