using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged.Events;
using Robust.Shared.Containers;
using Content.Shared.ActionBlocker;
using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Content.Shared.DragDrop;
using Content.Shared.Mobs;

namespace Content.Shared.Weapons.Ranged.Systems;

public abstract partial class SharedGarrisonSystem : EntitySystem
{
    [Dependency] private readonly SharedContainerSystem _container = default!;
    [Dependency] private readonly SharedDoAfterSystem _doAfter = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GarrisonComponent, ComponentStartup>(OnComponentStartup);
    }

    public bool TryInsertEntity(EntityUid entity, EntityUid toInsert, GarrisonComponent component)
    {
        _container.Insert(toInsert, component.PilotSlot);
        return true;
    }

    private void OnComponentStartup(Entity<GarrisonComponent> entity, ref ComponentStartup args)
    {
        entity.Comp.PilotSlot = _container.EnsureContainer<ContainerSlot>(entity, entity.Comp.PilotSlotId);
    }
}
