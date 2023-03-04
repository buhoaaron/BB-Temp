using UnityEngine;

namespace StateMachine
{
    public class LoadingState : BaseState
    {        
        public LoadingState(StateController controller) : base(controller)
        {}

        public override void Begin()
        {}

        public override void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                controller.SetState(new BonusGameState(controller));
            }
        }

        public override void End()
        {}
    }
}
