using System;
using UnityEngine;

namespace GameStates {
    public static class State {
    
        public enum GameStates {
            Alive,
            Dead
        };
        
        public static GameStates CurrentState;
        
        private static void StateSwitch() {
            switch (CurrentState) {
                case GameStates.Alive:
                    break;
                case GameStates.Dead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}