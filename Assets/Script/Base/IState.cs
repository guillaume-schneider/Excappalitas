using UnityEngine;

namespace Excappalitas
{
    /// <sumary> Base structure of a state </sumary>
    public interface IState<T> where T : new()
    {
        /// <sumary> Enter transition of the state </sumary>
        public abstract void Enter(T entity);

        /// <sumary> Method executed each tick </sumary>
        public abstract void Execute(T entity);

        /// <sumary> Exit transition of the state </sumary>
        public abstract void Exit(T entity);

        /// <sumary> Reception of an event </sumary>
        // public abstract bool OnMessage(Utils.Telegram message);
    }

}
