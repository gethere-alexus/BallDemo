using APIs.Activatable;

namespace MVPBase
{
    public abstract class PresenterBase<TModel, TView> : IActivatable where TModel : ModelBase where TView : ViewBase
    {
        public abstract void LinkPresenter(TModel model, TView view);
        public abstract void Enable();
        public abstract void Disable();
    }
}