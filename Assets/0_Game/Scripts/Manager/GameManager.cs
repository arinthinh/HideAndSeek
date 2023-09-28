using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField] private float _initializeDelayTime; // As second

    private const string GAME_SCENE_NAME = "Game";
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private async void Start()
    {
        await UniTask.Yield();
        await Initialize();
    }

    private async UniTask Initialize()
    {
        // Load game scene game
        var loadSceneOperator = SceneManager.LoadSceneAsync(GAME_SCENE_NAME);
        loadSceneOperator.allowSceneActivation = false;
        while (loadSceneOperator.progress >= 0.9f)
        {
            await UniTask.Yield();
        }
        // Initialize stuffs
        
        
        await UniTask.Delay(TimeSpan.FromSeconds(_initializeDelayTime));
        loadSceneOperator.allowSceneActivation = true;
        await UniTask.Yield();
    }
    
}
