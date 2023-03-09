using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public GameObject interactableFeddbackGUI;

    public GameObject normalShotCrosshairIndicator;
    public GameObject headshotCrosshairIndicato;
    public float shotIndicatorStayTime = .1f;

    void Awake()
    {
        instance = this;
    }

    public IEnumerator ShowShotIndicator()
    {
        normalShotCrosshairIndicator.SetActive(true);
        yield return new WaitForSeconds(shotIndicatorStayTime);
        normalShotCrosshairIndicator.SetActive(false);
    }
}
