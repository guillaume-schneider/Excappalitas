using Excappalitas.AI.States;
using Excappalitas.AI.Strategy;
using Excappalitas.LevelLogic.Generator;
using Excappalitas.Interactable;
using UnityEngine;

namespace Excappalitas.AI
{
    public class AIManager : MonoBehaviour
    {
        public StateMachine<AIManager> StateMachine { get { return _stateMachine; } }

        public AIStats Stats { get { return _stats; } }
        public AstarStrategy Astar { get { return _astarStrategy; } }
        public PursuitStrategy Pursuit { get { return _pursuitStrategy; } }
        public bool IsBlocked { get { return _isBlocked; } }

        public bool IsLogActivated = true;
        public bool CanMove = true;
        public Vector2 SizeCollisionDetection;
        public float OffSetCollisionDetection;

        private bool _isBlocked;

        private StateMachine<AIManager> _stateMachine;
        private AIStats _stats;

        private TargetFinder targetFinder;
        private AstarStrategy _astarStrategy;
        private PursuitStrategy _pursuitStrategy;
        
        public AIManager()
        {
            _stateMachine = new StateMachine<AIManager>(this, Idle.Instance);
        }

        private void OnEnable() {
            GeneratorInteractable.OnGeneratorActivated += OnAlarmEvent;
        }

        private void OnDisable() {
            GeneratorInteractable.OnGeneratorActivated -= OnAlarmEvent;
        }

        private void Start () {
            _astarStrategy = GetComponent<AstarStrategy>();
            _pursuitStrategy = GetComponent<PursuitStrategy>();
            _stats = GetComponent<AIStats>();
            targetFinder = GetComponent<TargetFinder>();
        }

        private void Update() 
        {
            _stateMachine.Update();
            UpdateMoveRestriction ();
            Log(IsLogActivated);    
            HasCollisionWith(LayerMask.GetMask("Obstacle"));
        }

        public bool HasCollisionWith (LayerMask layer, Vector2? size = null) {
            if (size != null) SizeCollisionDetection = size.Value;
            return (_isBlocked = Physics2D.OverlapBox (transform.position + Vector3.up * OffSetCollisionDetection, SizeCollisionDetection, transform.eulerAngles.z - 90, layer));
        }

        public Transform GetCurrentTarget() {
            return targetFinder.ChosenTarget;
        }

        public bool IsObstacleBetweenAIAndTarget() {
            return IsObstacleBetweenAIAndTarget(targetFinder.ChosenTarget.position);
        }

        private bool IsObstacleBetweenAIAndTarget(Vector2 target) {
            return Physics2D.Linecast(transform.position, target, LayerMask.GetMask("Obstacle"));
        }

        private void UpdateMoveRestriction () {
            _stats.MoveSpeed = (CanMove) ? _stats.MaxSpeed : 0f;
        }

        private void Log(bool active) {
            if (active) {
                LogMovementOff();
            }
        }

        private void LogMovementOff () {
            if (!CanMove)
                Debug.Log ("WARNING: AI movement desactivated.");
        }

        private void OnAlarmEvent(Transform interactable, Transform interacter) {
            _stateMachine.ChangeState(Excappalitas.AI.States.Pursuit.Instance);  
            targetFinder.SetTarget(interacter);     
        }

#if UNITY_EDITOR
        private void OnDrawGizmos () {
            Gizmos.matrix = Matrix4x4.TRS (transform.position, transform.rotation, Vector3.one);
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireCube (Vector2.up * OffSetCollisionDetection, SizeCollisionDetection);
        }
#endif
        
    }

}