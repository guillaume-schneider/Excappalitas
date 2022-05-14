using System;
using UnityEngine;

namespace Excappalitas {
    /// <sumary> 
    /// FSM attached to an entity to add a behavior.
    /// This FSM is based on the state, i.e. that the states
    /// is in charge of the transition and how state machine work
    /// </sumary>
    public class StateMachine<T> where T : new()
    {
        private IState<T> _currentState;
        private IState<T> _globalState;
        private IState<T> _previousState;
        private T _owner;

        public StateMachine(T owner) 
        {
            _owner = owner;
        }

        public StateMachine(T owner, IState<T> currentState) 
            : this(owner)
        {
            _currentState = currentState;
        }

        public StateMachine(T owner, IState<T> currentState, IState<T> globalState) 
            : this(owner, currentState)
        {
            _globalState = globalState;
        }
        
        /// <sumary>
        /// Execute current state and global state, i.e. this method
        /// should be placed on the Update loop
        /// </sumary>
        public void Update() 
        {
            if (_currentState != null) _currentState.Execute(_owner);
            if (_globalState != null) _globalState.Execute(_owner);
        }

        /// <sumary>
        /// Change the current state with the state passed on to
        /// <sumary/>
        public void ChangeState(IState<T> newState) 
        {
            if (newState == null) throw new NullReferenceException(
                "<StateMachine>: The new state is null in ChangeState method");

            if (newState == _currentState) return;
            
            _previousState = _currentState;
            _currentState.Exit (_owner);
            _currentState = newState;
            _currentState.Enter (_owner);
        }

        /// <sumary>
        /// Change the current state to the previous state
        /// See : <see cref="ChangeState"/>
        /// </sumary>
        public void RevertToPreviousState() 
        {
            if (_previousState == null) throw new NullReferenceException(
                "<StateMachine>: Previous state not found.");            
            ChangeState(_previousState);
        }

        /// <sumary>
        /// Compare the state argument with the current state
        /// if the current state is the argument, the method
        /// return true
        /// </sumary>
        public bool IsInState(IState<T> state) 
        {
            if (_currentState.GetType() == state.GetType()) 
            {
                return true;
            }
            return false;
        }
        
    }

}
