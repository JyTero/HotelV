using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private NeedSOHolderSO needsHolderSO;
    [SerializeField]
    private GameObject loadingScreenGO;

    

    private void Awake()
    {
        needsHolderSO.CharacterNeedSOHolderStart();

        EndLoadingScreen();
    }

    private void EndLoadingScreen()
    {
        loadingScreenGO.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
