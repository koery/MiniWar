namespace MGFramework
{
    public class MGCallFuncN : MGAction
    {
        #region Delegates

        public delegate void Callback(MGNode node);

        #endregion

        protected Callback _c;

        public static MGCallFuncN ActionWithTarget(Callback c)
        {
            return new MGCallFuncN
                       {
                           _c = c
                       };
        }

        public override void Step(float dt)
        {
            _c(Target);
            _isEnd = true;
        }
    }
}