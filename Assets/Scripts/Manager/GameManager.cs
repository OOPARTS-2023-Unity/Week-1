using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 매니저 인스턴스
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                SetUpInstance();
            }
            return _instance;
        }
    }

    // 매니저 초기화
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentScene = SceneType.MainMenu;
        InitOtherManagers();
    }

    private static void SetUpInstance()
    {
        _instance = FindObjectOfType<GameManager>();
        if (_instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = "GameManager";
            _instance = gameObj.AddComponent<GameManager>();
            DontDestroyOnLoad(gameObj);
        }
    }

    private static UIManager _uiManager = new UIManager();
    public static UIManager UI
    {
        get
        {
            return _uiManager;
        }
    }

    private static GamePlayManager _gamePlayManager = new GamePlayManager();
    public static GamePlayManager Play
    {
        get
        {
            return _gamePlayManager;
        }
    }

    private void InitOtherManagers()
    {
        // 다른 매니저 추가됐을 때 여기에 초기화 코드 작성
        _uiManager.Init();
        _gamePlayManager.Init();
    }

    // 게임 종료
    public void ExitGame()
    {
        Application.Quit();
    }

    // 씬 관리
    public enum SceneType
    {
        MainMenu,
        Option,
        Game
    }

    private SceneType currentScene;

    public void ChangeScene(SceneType sceneType)
    {
        currentScene = sceneType;
        SceneManager.LoadScene(sceneType.ToString());
        OnSceneChanged?.Invoke(sceneType);
    }

    public event Action<SceneType> OnSceneChanged;
}