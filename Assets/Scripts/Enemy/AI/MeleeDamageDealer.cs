using System;
using System.Linq;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Enemy.AI
{
    [Serializable]
    public class MeleeDamageDealer : Action, IDamageDealer
    {
        public int Damage => _damage;

        [SerializeField] private int _damage;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private float _clevage = 0.5f;
        [SerializeField] private float _attackDistance = 0.5f;

        private Collider[] _hits = new Collider[1];
        private AttackAnimationEvents _attackAnimationEvents;


        public override void OnAwake()
        {
            base.OnAwake();
            _attackAnimationEvents = GetComponent<AttackAnimationEvents>();
            _attackAnimationEvents.Initialize(this);
        }
        
        public bool Hit(out Collider hit)
        {
            var hitsCount = Physics.OverlapSphereNonAlloc(OverlapPoint(), _clevage, _hits, _targetLayerMask);

            hit = _hits.FirstOrDefault();
            
            PhysicsDebug.DrawDebug(OverlapPoint(), _clevage, 1f);


            return hitsCount > 0;
        }

        private Vector3 OverlapPoint()
        {
            var position = transform.position;
            var startPoint = new Vector3(position.x, position.y + 0.5f, position.z) + transform.forward * _attackDistance;
            return startPoint;
        }

    }
}