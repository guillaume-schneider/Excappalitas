using UnityEngine;

namespace Excappalitas.AI.States {
    public class Pursuit : IState<AIManager>
    {
        public static Pursuit Instance 
        {
            get 
            {
                lock(padlock) 
                {
                    if(_instance == null) 
                    {
                        _instance = new Pursuit();
                    }
                    return _instance;
                }
            }
        }
        private static Pursuit _instance;
        private static readonly object padlock = new object();

        private StrategyMachine _pursuitMachine;

        private Pursuit() {
            _pursuitMachine = new StrategyMachine();
        }

        public void Enter(AIManager entity) 
        {
            Debug.Log("Enter pursuit.");
            _pursuitMachine.Strategy = entity.Astar;
        }

        public void Execute(AIManager entity) 
        {
            if (entity.IsObstacleBetweenAIAndTarget() || entity.IsBlocked)
                _pursuitMachine.Strategy = entity.Astar;
            else _pursuitMachine.Strategy = entity.Pursuit;
            _pursuitMachine.ExecuteStrategy();
        }

        public void Exit(AIManager entity) 
        {
            Debug.Log("Leave pursuit");
        }

        // public bool OnMessage(Utils.Telegram message) => false;
    }
}