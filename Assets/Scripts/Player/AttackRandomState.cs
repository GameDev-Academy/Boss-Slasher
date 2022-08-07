
using UnityEngine;

namespace Player
{
    public class AttackRandomState : StateMachineBehaviour
    {
        private static readonly int IdAttackHash = Animator.StringToHash("IdAttack");

        public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            base.OnStateMachineEnter(animator, stateMachinePathHash);
            animator.SetInteger(IdAttackHash, Random.Range(0,4));
        }
    }
}
