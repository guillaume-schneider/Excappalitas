using UnityEngine;

namespace Excappalitas.AI.States
{
    public class Idle : IState<AIManager> 
    {
        public static Idle Instance 
        {
            get 
            {
                lock(padlock) 
                {
                    if(_instance == null) 
                    {
                        _instance = new Idle();
                    }
                    return _instance;
                }
            }
        }
        private static Idle _instance;
        private static readonly object padlock = new object();

        private Transform _transform;

        private Idle() {}
        public void Enter(AIManager entity) 
        {
            _transform = entity.transform;
            Debug.Log("Enter Idle.");
        }

        public void Execute(AIManager entity) 
        {
            // Debug.Log("Idle Exec.");
            if(entity.GetComponent<FieldOfView>().VisibleTargets.Count > 0) 
            {
                entity.StateMachine.ChangeState (Pursuit.Instance);
            }
        }

        public void Exit(AIManager entity) 
        {
            Debug.Log("Leave Idle");
        }

        // public bool OnMessage(Utils.Telegram message) => false;
        
        
    }
}