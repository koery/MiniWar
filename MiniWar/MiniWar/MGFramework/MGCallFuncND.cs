namespace MGFramework
{
    public class MGCallFuncND : MGAction
    {
        #region Delegates

        public delegate void Callback(MGNode node, object data);

        #endregion

        protected Callback _c;
        protected object Data;

        public static MGCallFuncND ActionWithTarget(Callback c, object data)
        {
            return new MGCallFuncND
                       {
                           _c = c,
                           Data = data
                       };
        }

        public override void Step(float dt)
        {
            _c(Target, Data);
            _isEnd = true;
        }
    }
}