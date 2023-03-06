using UnityEngine;

namespace StateMachine
{
    public class MainGameState : BaseState
    {        
        public MainGameState(StateController controller) : base(controller)
        {}

        public override void Begin()
        {}

        public override void StateUpdate()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                controller.SetState(new LoadingState(controller));
            }
        }

        public override void End()
        {}
    }
}
