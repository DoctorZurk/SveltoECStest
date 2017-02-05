﻿using Svelto.ECS.Example.Components.Player;
using Svelto.ECS.Example.Observables.Enemies;
using Svelto.Observer.InterNamespace;
using System.Collections.Generic;

namespace Svelto.ECS.Example.Observers.HUD
{
	public class ScoreOnEnemyKilledObserver:Observer<PlayerTargetType, ScoreActions>
	{
		public ScoreOnEnemyKilledObserver(EnemyKilledObservable observable): base(observable)
		{}

		protected override ScoreActions TypeMap(ref PlayerTargetType dispatchNotification)
		{
			return _targetTypeToScoreAction[dispatchNotification];
		}

        readonly Dictionary<PlayerTargetType, ScoreActions> _targetTypeToScoreAction = new Dictionary<PlayerTargetType, ScoreActions>
        {
            { PlayerTargetType.Bear, ScoreActions.bearKilled },
            { PlayerTargetType.Bunny, ScoreActions.bunnyKilled },
            { PlayerTargetType.Hellephant, ScoreActions.HellephantKilled },
        };
    }

	public enum ScoreActions
	{
		bunnyKilled,
		bearKilled,
		HellephantKilled
	}
}