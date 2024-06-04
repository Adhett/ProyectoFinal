using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaiviour : MonoBehaviour
{
    public Transform[] transforms;

    private void Start()
    {
        var posicionInicial = Random.Range(0, transforms.Length);
        transform.position = transforms[posicionInicial].position;
    }
}

