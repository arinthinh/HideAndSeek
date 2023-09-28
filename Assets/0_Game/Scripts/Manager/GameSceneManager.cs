using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private async void Start()
    {
        await UniTask.Yield();
        GameplayController.Instance.Initialize();
        UIManager.Instance.GetView<ScreenMain>().Show();
    }
}
