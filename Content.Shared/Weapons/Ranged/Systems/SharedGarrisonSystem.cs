using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Systems;
using Content.Shared.Weapons.Ranged.Events;
using Content.Shared.Buckle;
using Content.Shared.Buckle.Components;
using Robust.Shared.Containers;
using Content.Shared.ActionBlocker;
using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Content.Shared.DragDrop;
using Content.Shared.Mobs;
using Content.Shared.Timing;
using Robust.Shared.Timing;

namespace Content.Shared.Weapons.Ranged.Systems;

public abstract partial class SharedGarrisonSystem : EntitySystem
{
    [Dependency] private readonly SharedContainerSystem _container = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    [Dependency] private readonly SharedBuckleSystem _buckle = default!;
    [Dependency] private readonly SharedGunSystem _gun = default!;
    [Dependency] protected readonly IGameTiming _timing = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GarrisonComponent, ComponentStartup>(OnComponentStartup);
        //SubscribeLocalEvent<GarrisonComponent, StrappedEvent>(OnPilotBuckled);
    }

    public bool TryInsertEntity(EntityUid entity, EntityUid toInsert, GarrisonComponent? component = null)
    {
        if (!Resolve(entity, ref component))
            return false; // GarrisonComponent could not be found on this Entity.
        _container.Insert(toInsert, component.PilotSlot);
        return true;
    }

    private void OnComponentStartup(Entity<GarrisonComponent> entity, ref ComponentStartup args)
    {
        entity.Comp.PilotSlot = _container.EnsureContainer<ContainerSlot>(entity, entity.Comp.PilotSlotId);
    }

    private void AttemptShoot(Entity<GunComponent> entity)
    {

    }
}
