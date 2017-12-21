namespace GoodGameDeals.Domain.Interactors {
    using System;
    using System.Reactive.Disposables;

    public abstract class AbstractInteractor<T, Params> {
        private readonly CompositeDisposable disposables;

        public AbstractInteractor() {
            this.disposables = new CompositeDisposable();
        }

        public abstract IObservable<T> UseCaseObservable(Params parameters);

        public void Dispose() {
            if (!this.disposables.IsDisposed) {
                this.disposables.Dispose();
            }
        }
    }
}
