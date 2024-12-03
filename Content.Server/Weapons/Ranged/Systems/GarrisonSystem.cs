using System.Linq;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Systems;
using Content.Shared.Verbs;
using Robust.Shared.Containers;

namespace Content.Server.Weapons.Ranged.Systems;

public abstract partial class GarrisonSystem : SharedGarrisonSystem
{

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<GarrisonComponent, GetVerbsEvent<AlternativeVerb>>(OnAlternativeVerb);
    }

    /// <summary>
    /// Perform actions based on Alt-Clicking onto an entity that has a GarrisonComponent
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="component"></param>
    /// <param name="args"></param>
    private void OnAlternativeVerb(EntityUid uid, GarrisonComponent component, GetVerbsEvent<AlternativeVerb> args)
    {
        Log.Debug(uid + "entity");
        Log.Debug(component + "component");
        TryInsertEntity(uid, args.User, component);
    }
}
