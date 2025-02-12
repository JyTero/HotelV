using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WorldClickHandler : MonoBehaviour
{
    public static WorldClickHandler Instance { get; private set; }

    [SerializeField]
    private UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                CharacterBase hitCharacter = hit.collider.gameObject.GetComponent<CharacterBase>();
                if (hitCharacter)
                {
                    uiManager.EnableCharacterUI(hitCharacter);
                }
                else
                    uiManager.DisableCharacterUI();
            }
        }
    }

}
