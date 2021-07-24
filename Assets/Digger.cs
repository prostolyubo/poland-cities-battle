using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    public WeaponController controller;
    public Rigidbody2D body;
    public Transform root;
    Action callback;
    public GameObject neutralBottom, diggingBottom;
    public float digTime;
    public float digHeight;
    public RoundManager manager;
    public PlayerController player;
    
    private void Awake()
    {
        controller.OnUseTriggered += HandleUse;
    }

    private void HandleUse(Action callback)
    {
        if (!player.isGrounded)
        {
            callback?.Invoke();
            return;
        }
        this.callback = callback;
        neutralBottom.SetActive(false);
        diggingBottom.SetActive(true);
        StartCoroutine(DiggingRoutine());
    }

    private IEnumerator DiggingRoutine()
    {
        float left = digTime;
        body.bodyType = RigidbodyType2D.Kinematic;
        while (left > 0)
        {
            left -= Time.deltaTime;
            root.position += (Vector3.down * Time.deltaTime * digHeight);
            yield return null;
        }
        Teleport();
        left = digTime;
        neutralBottom.SetActive(true);
        diggingBottom.SetActive(false);
        while (left > 0)
        {
            left -= Time.deltaTime;
            root.position -= (Vector3.down * Time.deltaTime * digHeight);
            yield return null;
        }
        body.bodyType = RigidbodyType2D.Dynamic;
        FinishMove();
    }

    private void Teleport()
    {
        Vector3 spawn = Vector3.Lerp(manager.map.leftBound.position, manager.map.rightBound.position, UnityEngine.Random.value);
        var hit = Physics2D.Raycast(spawn + Vector3.up * 10, Vector2.down);
        if(hit == null)
        {
            Teleport();
            return;
        }
        root.transform.position = new Vector3(hit.point.x, hit.point.y - digHeight * digTime);
    }

    public void FinishMove()
    {
        callback?.Invoke();
        callback = null;
        
    }
}
