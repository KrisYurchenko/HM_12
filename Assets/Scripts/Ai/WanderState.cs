using UnityEngine;

namespace DefaultNamespace
{
    public class WanderState : IAiState {
        private readonly Vector3 _direction;
        private IAiState _nextState;

        public WanderState(Vector3 direction)
        {
            _direction = direction;
        }

        public Vector3 GetDirection(Vector3 transformPosition)
        {
            if (BlockChecker.HasBlockInDirection(transformPosition, _direction))
            {
                return _direction;
            }
            else
            {
                _nextState = new WaitState(2f, _direction);
                return Vector3.zero;
            }
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public IAiState GetNextState()
        {
            return _nextState;
        }
    }
}