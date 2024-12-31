using Robust.Client.Player;
using Robust.Client.State;
using Content.Client.Gameplay;
using Robust.Client.Input;
using Robust.Shared.Input;
using Robust.Client.Graphics;
using Robust.Client.GameObjects;
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
    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly InputSystem _inputSystem = default!;

    public override void Initialize()
    {
        UpdatesOutsidePrediction = true;
        base.Initialize();
    }

    public override void Update(float frameTime)
    {

        if (!_timing.IsFirstTimePredicted)
            return;

        var useKey = EngineKeyFunctions.Use;

        if (_inputSystem.CmdStates.GetState(useKey) != BoundKeyState.Down)
        {
            return;
        }

        var entityNull = _player.LocalEntity;

        if (entityNull == null)
            return;

        var entity = entityNull.Value;

        // Try to find a Buckle component on entity trying to pilot
        if (!TryComp<BuckleComponent>(entity, out var buckle))
            return;
        // Try to find a Garrison component attached to the entity of which the pilot is buckled to
        if (!TryComp<GarrisonComponent>(buckle.BuckledTo, out var garrison))
            return;
        // Try to find the Gun component attached to the entity of which the pilot is buckled to
        if (!TryComp<GunComponent>(buckle.BuckledTo, out var gun))
            return;

        var mousePos = _eyeManager.PixelToMap(_inputManager.MouseScreenPosition);

        NetEntity? target = null;
        if (_state.CurrentState is GameplayStateBase screen)
            target = GetNetEntity(screen.GetClickedEntity(mousePos));

        var coordinates = _transformSystem.ToCoordinates(entity, mousePos);

        _entity.RaisePredictiveEvent(new RequestShootEvent
        {
            Target = target,
            Coordinates = GetNetCoordinates(coordinates),
            Gun = GetNetEntity(gun.Owner),
            isGarrison = true
        });

    }
}

