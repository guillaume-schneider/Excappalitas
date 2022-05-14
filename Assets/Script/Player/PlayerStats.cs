using Excappalitas;
using UnityEngine;

namespace Excappalitas.Player {
    public class PlayerStats : EntityStats
    {
        public float MoveSpeed { get { return _moveSpeed; } }
        public float MaxSpeed { get { return _maxSpeed; } }

        [SerializeField] [Range (0, 30)]
        private float _moveSpeed = 3;
        [SerializeField]
        private float _maxSpeed = 10;
    }
}