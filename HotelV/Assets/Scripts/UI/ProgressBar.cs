using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    //Used to add prog bar to mouse right click under UI, could (/should) be moved
    //to its own place
#if UNITY_EDITOR
    [MenuItem("sourceGO/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject GO = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        GO.transform.SetParent(Selection.activeGameObject.transform, false);
        GO.name = "Linear Progress Bar";
    }

#endif

    public float minimum;
    public float maximum;
    public float current;

    public Image mask;
    public Image fill;
    public Color color = Color.red;


    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
        fill.color = color;

    }
}
