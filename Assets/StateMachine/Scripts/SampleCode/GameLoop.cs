using UnityEngine;
using StateMachine;

namespace StateMachine
{
    public class GameLoop : MonoBehaviour
    {
        private StateController controller;

        private void Start()
        {
            controller = new StateController();
            controller.SetState(new MainGameState(controller));
        }

        private void Update()
        {
            controller.StateUpdate();
        }
    }
}
