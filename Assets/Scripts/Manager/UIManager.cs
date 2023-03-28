using System.Collections.Generic;
using UnityEngine;

using TMPro;

public enum CanvasType
{
    NONE,
    MAIN_MENU,
    OPTION,
    GAME_PLAY
}

[System.Serializable]
public struct CanvasPrefab
{
    public CanvasType type;
    public string canvasName;
}

public class UIManager
{
    // 캔버스 정보
    public readonly CanvasPrefab[] CANVAS_INFO_LIST =
    {
        new CanvasPrefab() {type=CanvasType.GAME_PLAY, canvasName="GameUICanvas"}
    };

    // 캔버스와 씬 바인딩 정보
    private readonly Dictionary<GameManager.SceneType, CanvasType> SCENE_CANVAS_INFO
        = new Dictionary<GameManager.SceneType, CanvasType>()
    {
        {GameManager.SceneType.Game, CanvasType.GAME_PLAY}
    };

    // 생성된 캔버스 오브젝트 담을 딕셔너리
    private Dictionary<CanvasType, GameObject> _canvasDict = new Dictionary<CanvasType, GameObject>();

    // 매니저 초기화
    public void Init()
    {
        foreach (CanvasPrefab c in CANVAS_INFO_LIST)
        {
            GameObject canvasObject = Object.Instantiate(GetPrefabResource(c.canvasName), null);
            Object.DontDestroyOnLoad(canvasObject);
            AddCanvas(c.type, canvasObject);
        }

        GameManager.Instance.OnSceneChanged += (sceneType) => ShowCanvas(SCENE_CANVAS_INFO[sceneType]);

        GameManager.Play.OnPlayerWin += ShowWin;
        GameManager.Play.OnPlayerDead += ShowDead;

        HideAllCanvas();
    }

    // 캔버스 딕셔너리에 캔버스 오브젝트 추가
    public void AddCanvas(CanvasType type, GameObject canvas)
    {
        _canvasDict.Add(type, canvas);
    }

    // 화면에 캔버스 보여주기
    public void ShowCanvas(CanvasType type)
    {
        HideAllCanvas();
        _canvasDict[type].SetActive(true);
    }

    // 딕셔너리에 있는 모든 캔버스 숨기기
    public void HideAllCanvas()
    {
        foreach (var c in _canvasDict)
        {
            c.Value.SetActive(false);
        }
    }

    // 리소스 폴더에서 프리팹 로드하기
    public GameObject GetPrefabResource(string name, string path = null)
    {
        string targetPath = "Prefabs/UI/" + (path == null ? name : path + name);
        GameObject go = Resources.Load<GameObject>(targetPath);

        if (go == null)
        {
            Debug.LogError($"Can't find prefab: {name}");
        }
        return go;
    }

    public void ShowWin(float score)
    {
        ShowCanvas(CanvasType.GAME_PLAY);
        _canvasDict[CanvasType.GAME_PLAY].GetComponentInChildren<TextMeshProUGUI>().text = "Win";
    }

    public void ShowDead()
    {
        ShowCanvas(CanvasType.GAME_PLAY);
        _canvasDict[CanvasType.GAME_PLAY].GetComponentInChildren<TextMeshProUGUI>().text = "Dead";
    }
}
