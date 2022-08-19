using System;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public sealed class PathPositionDOTweenActionBase : PathDOTweenActionBase
    {
        [SerializeField]
        private Vector3[] positions;
        public Vector3[] Positions => positions;

        public override string DisplayName => "Move to Path Positions" ;

        public override void ResetCache(GameObject target)
        {
        }

        protected override Vector3[] GetPathPositions()
        {
            return positions;
        }
    }
}
