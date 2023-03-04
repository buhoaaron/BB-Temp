using UnityEngine;

namespace StateMachine
{
    public class BonusGameState : BaseState
    {        
        public BonusGameState(StateController controller) : base(controller)
        {}

        public override void Begin()
        {}

        public override void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                controller.SetState(new MainGameState(controller));
            }
        }

        public override void End()
        {}
    }
}
