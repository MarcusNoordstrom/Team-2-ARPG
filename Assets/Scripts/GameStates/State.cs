using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GameStates {
    public static class State {
        public enum GameStates {
            Alive,
            Paused,
            Dead
        };

        public static GameStates CheckState;
    }
}