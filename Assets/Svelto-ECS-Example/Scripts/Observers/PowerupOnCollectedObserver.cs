using Svelto.ECS.Example.Components.Powerup;
using Svelto.ECS.Example.Observables.Powerup;
using Svelto.Observer.InterNamespace;
using System.Collections.Generic;

namespace Svelto.ECS.Example.Observers.Powerup
{
	public class PowerupOnCollectedObserver:Observer<PlayerPowerupType, PowerupActions>
	{
		public PowerupOnCollectedObserver(PowerupCollectedObservable observable): base(observable)
		{}

		protected override PowerupActions TypeMap(ref PlayerPowerupType dispatchNotification)
		{
			return _targetTypeToPowerupAction[dispatchNotification];
		}

        readonly Dictionary<PlayerPowerupType, PowerupActions> _targetTypeToPowerupAction = new Dictionary<PlayerPowerupType, PowerupActions>
        {
            { PlayerPowerupType.Ammo, PowerupActions.ammoCollected },
            { PlayerPowerupType.Health, PowerupActions.healthCollected },
            { PlayerPowerupType.Score, PowerupActions.scoreCollected },
        };
    }

	public enum PowerupActions
	{
		ammoCollected,
		healthCollected,
		scoreCollected
	}
}