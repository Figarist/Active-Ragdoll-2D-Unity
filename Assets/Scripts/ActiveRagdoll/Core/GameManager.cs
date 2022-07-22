using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ActiveRagdoll.Core
{
    public class GameManager : MonoBehaviour
    {
        public const float DefaultTimeScale = 1f;
        public const float DefaultFixedScale = 0.02f;
        public Gradient gradient;
        public GameObject fire;

        private void Awake()
        {
            EventManager.AddListener<GameOverEvent>(GameOver);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<GameOverEvent>(GameOver);
        }

        private void GameOver(GameOverEvent gameOverEvent)
        {
            switch (gameOverEvent.TypeOfGameOver)
            {
                case TypeOfGameOver.Damage:
                    print("умер смертью храбрых");
                    break;
                case TypeOfGameOver.Fall:
                    break;
                case TypeOfGameOver.Stuck:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}