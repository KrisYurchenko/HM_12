using UnityEngine;

namespace DefaultNamespace
{
    public class WaitState : IAiState
    {
        private float _waitTime;
        private readonly Vector3 _previousDirection;
        private WanderState _nextState;

        public WaitState(float waitTime, Vector3 previousDirection)
        {
            _waitTime = waitTime;
            _previousDirection = previousDirection;
        }
        
        
        public Vector3 GetDirection(Vector3 transformPosition)
        {
            return Vector3.zero;
        }
        
        public void OnUpdate(float deltaTime)
        {
            _waitTime -= deltaTime;
            if (_waitTime < 0f)
            {
                _nextState = new WanderState(-_previousDirection);
            }
        }

        public IAiState GetNextState()
        {
            return _nextState;
        }
    }
}