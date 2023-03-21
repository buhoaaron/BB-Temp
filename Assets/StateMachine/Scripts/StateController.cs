namespace StateMachine
{
    /// <summary>
    /// 狀態控制者
    /// </summary>
    public class StateController
    {
        protected BaseState currentState = null;
        protected bool isBegin = false;
        protected bool isDebugEnabled = true;
        public virtual void SetState(BaseState state)
        {
            //通知前一個狀態結束
            if (currentState != null)
            {
                if (isDebugEnabled)
                    UnityEngine.Debug.Log("State End: " + currentState);

                currentState.End();
            }

            currentState = state;

            isBegin = false;
        }

        public virtual void StateUpdate()
        {
            //通知狀態開始
            if (currentState != null && !isBegin)
            {
                if (isDebugEnabled)
                    UnityEngine.Debug.Log("State Begin: " + currentState);

                currentState.Begin();

                isBegin = true;
            }
            //狀態刷新
            if (currentState != null)
            {
                if (isDebugEnabled)
                    UnityEngine.Debug.Log("State Update: " + currentState);

                currentState.StateUpdate();
            }
        }

        public void SetDebugEnabled(bool enabled)
        {
            isDebugEnabled = enabled;
        }
    }
}
