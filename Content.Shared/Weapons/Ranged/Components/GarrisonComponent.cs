using Content.Shared.Weapons.Ranged.Events;
using Content.Shared.Weapons.Ranged.Systems;
using Robust.Shared.Audio;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;

namespace Content.Shared.Weapons.Ranged.Components;
[RegisterComponent, NetworkedComponent]
public sealed partial class GarrisonComponent : Component
{
    /// <summary>
    /// The slot the operator is stored in.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)]
    public ContainerSlot PilotSlot = default!;

    [ViewVariables(VVAccess.ReadOnly)]
    public readonly string PilotSlotId = "pilot-slot";

    [DataField]
    public float InsertDelay = 2;
}
