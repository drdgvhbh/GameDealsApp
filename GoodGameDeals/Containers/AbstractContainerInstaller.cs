namespace GoodGameDeals.Containers {
    using Unity;

    public abstract class AbstractContainerInstaller {
        public IUnityContainer Container { get; }

        protected AbstractContainerInstaller() : this(new UnityContainer()) {
        }

        protected AbstractContainerInstaller(IUnityContainer container) {
            this.Container = container;

            this.RegisterViewModels();
            this.RegisterMappings();
            this.RegisterFactories();
            this.RegisterRepositories();
            this.RegisterInteractors();
            this.RegisterCaches();
        }

        protected virtual void RegisterInteractors() {
        }

        protected virtual void RegisterMappings() {
        }

        protected virtual void RegisterViewModels() {
        }

        protected virtual void RegisterFactories() {
        }

        protected virtual void RegisterRepositories() {
        }

        protected virtual void RegisterCaches() {
        }
    }
}
