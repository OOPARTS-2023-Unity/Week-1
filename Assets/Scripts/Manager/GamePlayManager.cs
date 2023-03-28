using UnityEngine;
using System;

public class GamePlayManager
{
    public bool isWin { get; private set; }

    public bool isDead { get; private set; }

    public GamePlayManager()
    {
        ResetState();
    }

    // OnPlayerWin(float score)
    public event Action<float> OnPlayerWin;
    // OnPlayerDead()
    public event Action OnPlayerDead;
    // OnRestart()
    public event Action OnRestart;

    public void Init()
    {
        GameManager.Instance.OnSceneChanged +=
            ((sceneType) =>
            {
                if (sceneType == GameManager.SceneType.Game)
                {
                    ResetState();
                }
            });

        ResetState();
    }

    public void ResetState()
    {
        Debug.Log("Reset Play");
        isWin = false;
        isDead = false;
    }

    public void Replay()
    {
        ResetState();
        OnRestart?.Invoke();
    }

    public void PlayerWin(float score)
    {
        isWin = true;
        OnPlayerWin?.Invoke(score);
    }

    public void PlayerDead()
    {
        isDead = true;
        OnPlayerDead?.Invoke();
    }
}
