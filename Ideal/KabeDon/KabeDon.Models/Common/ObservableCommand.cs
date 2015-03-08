using System;
using System.Reactive.Subjects;
using System.Windows.Input;

namespace KabeDon.Common
{
    /// <summary>
    /// <see cref="Execute(object)"/> が呼ばれたことを <see cref="IObservable{T}"/> を介してとれるようにしたもの。
    /// </summary>
    public class ObservableCommand : ICommand, IObservable<object>
    {
        private Subject<Null> _subject = new Subject<Null>();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _subject.OnNext(null);
        }

        public IDisposable Subscribe(IObserver<object> observer) => _subject.Subscribe(observer);
    }

    /// <summary>
    /// <see cref="Execute(object)"/> が呼ばれたことを <see cref="IObservable{T}"/> を介してとれるようにしたもの。
    /// </summary>
    public class ObservableCommand<T> : ICommand, IObservable<T>
    {
        private Subject<T> _subject = new Subject<T>();

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _subject.OnNext((T)parameter);
        }

        public IDisposable Subscribe(IObserver<T> observer) => _subject.Subscribe(observer);
    }
}
