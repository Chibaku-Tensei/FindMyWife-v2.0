using System.Collections;
using System.Collections.Generic;
using Base;
using Base.Pattern;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class InitializeState : GameState
    {
        private AsyncOperation mainGameSceneOperation = null;
        public override void EnterStateBehaviour(float dt, GameState fromState)
        {
            SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);
            mainGameSceneOperation = SceneManager.LoadSceneAsync("MainGameScene", LoadSceneMode.Additive);
        }

        public override void CheckExitTransition()
        {
            if (mainGameSceneOperation.progress >= .9f)
            {
                GameStateController.EnqueueTransition<StartState>();
            }
        }

        public override void UpdateBehaviour(float dt)
        {
            
        }
    }
}

