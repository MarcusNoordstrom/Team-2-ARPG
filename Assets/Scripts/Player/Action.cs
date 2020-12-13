using UnityEngine;

namespace Player {
    public class Action : MonoBehaviour {
        IAction _currentAction;

        public void StartAction(IAction action) {
            print(_currentAction);
            if(_currentAction == action) return;
            _currentAction?.ActionToStart();
            _currentAction = action;
        }
    }
}