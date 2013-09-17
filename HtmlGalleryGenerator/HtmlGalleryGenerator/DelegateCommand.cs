using System;
using System.Windows.Input;

namespace HtmlGalleryGenerator
{
    public class DelegateCommand : DelegateCommand<object>
    {
        #region public constructors

        public DelegateCommand(Action execute)
            : base(execute != null ? x => execute() : (Action<object>) null)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
            : base(execute != null ? x => execute() : (Action<object>) null,
                canExecute != null ? x => canExecute() : (Predicate<object>) null)
        {
        }

        #endregion
    }

    public class DelegateCommand<T> : ICommand
    {
        #region public constructors

        public DelegateCommand(Action<T> execute)
            : this(execute, x => true)
        {
        }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (canExecute == null) throw new ArgumentNullException("canExecute");
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region public methods

        public void Execute(object parameter = null)
        {
            _execute((T) parameter);
        }

        public bool CanExecute(object parameter = null)
        {
            return _canExecute((T) parameter);
        }

        public virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        #endregion

        #region public methods

        public event EventHandler CanExecuteChanged;

        #endregion

        #region private fields

        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        #endregion
    }
}