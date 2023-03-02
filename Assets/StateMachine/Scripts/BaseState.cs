namespace StateMachine
{
    /// <summary>
    /// 狀態基底
    /// </summary>
    public abstract class BaseState
    {
        //控制者
        protected StateController controller = null;
        
        public BaseState(StateController controller)
        {
            this.controller = controller;
        }

        public virtual void Begin()
        {}

        public virtual void StateUpdate()
        {}

        public virtual void End()
        {}
    }
}
