using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private  float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Quieto Behaviour")]
    [SerializeField] private float quietoDuracion;
    private float quietoTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
                MoverEnDireccion(-1);
            else
                DirrecionChange();
        
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoverEnDireccion(1);
            else
                DirrecionChange();
        }
    }

    private void DirrecionChange()
    {
        anim.SetBool("moving", false);
        quietoTimer += Time.deltaTime;

        if(quietoTimer > quietoDuracion)
            movingLeft =!movingLeft;
    }
    private void MoverEnDireccion(int _direccion)
    {
        quietoTimer = 0;
        anim.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direccion,
            initScale.y, initScale.z);

        enemy.position = new Vector3 (enemy.position.x + Time.deltaTime * _direccion * speed,
            enemy.position.y, enemy.position.z);
    }
}
