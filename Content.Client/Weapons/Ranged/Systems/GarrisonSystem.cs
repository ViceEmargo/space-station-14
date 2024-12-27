using Robust.Client.Player;
using Robust.Client.State;
using Content.Client.Gameplay;
using Robust.Client.Input;
using Robust.Client.Graphics;
using Robust.Shared.Utility;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Weapons.Ranged;
using Content.Shared.Weapons.Ranged.Systems;
using Content.Shared.Weapons.Ranged.Events;
using Content.Shared.Buckle.Components;

namespace Content.Client.Weapons.Ranged.Systems;

public sealed partial class GarrisonSystem : SharedGarrisonSystem
{
    [Dependency] private readonly IInputManager _inputManager = default!;
    [Dependency] private readonly IEyeManager _eyeManager = default!;
    [Dependency] protected readonly SharedTransformSystem _transformSystem = default!;
    [Dependency] private readonly IPlayerManager _player = default!;
    [Dependency] private readonly IStateManager _state = default!;

    public override void Initialize()
    {
        UpdatesOutsidePrediction = true;
        base.Initialize();
    }

    public override void Update(float frameTime)
    {
        //if (_timing.IsFirstTimePredicted)
          //  return;

        var entityNull = _player.LocalEntity;

        if (entityNull == null)
            return;

        var entity = entityNull.Value;


        if (!TryComp<BuckleComponent>(entity, out var buckle))
            return;

        if (!TryComp<GarrisonComponent>(buckle.BuckledTo, out var garrison))
            return;

        if (!TryComp<GunComponent>(buckle.BuckledTo, out var gun))
            return;

        var mousePos = _eyeManager.PixelToMap(_inputManager.MouseScreenPosition);

        NetEntity? target = null;
        if (_state.CurrentState is GameplayStateBase screen)
            target = GetNetEntity(screen.GetClickedEntity(mousePos));

        var coordinates = _transformSystem.ToCoordinates(entity, mousePos);

    }
}

