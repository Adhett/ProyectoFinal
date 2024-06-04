using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerUI : MonoBehaviour
{

    public GameObject bossPanel;
    public GameObject muros;

    public static FreezerUI instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        bossPanel.SetActive(false);
        muros.SetActive(false);
    }
    public void BossActivator()
    {
        bossPanel.SetActive(true);
        muros.SetActive(true);
    }
}
