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
    public SpriteAnimator moudPrefab;
    
    private void Awake()
    {
        controller.OnUseTriggered += HandleUse;
        manager = FindObjectOfType<RoundManager>(); // Game Jam! Time is running out
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
        StartCoroutine(DiggingBeginRoutine());
    }

    private IEnumerator DiggingBeginRoutine()
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
        root.gameObject.SetActive(false);
    }
    private IEnumerator DiggingEndRoutine(GameObject instance)
    {
        float left = digTime/4f;
        neutralBottom.SetActive(true);
        diggingBottom.SetActive(false);
        while (left > 0)
        {
            left -= Time.deltaTime;
            root.position -= (Vector3.down * Time.deltaTime * digHeight * 4f);
            yield return null;
        }
        body.bodyType = RigidbodyType2D.Dynamic;
        Destroy(instance);
        FinishMove();
    }

    private Action<int, int> HandleFrameChange(GameObject instance)
    {
        return (state, frame) =>
        {
            if (frame == 3)
            {
                root.gameObject.SetActive(true);
                StartCoroutine(DiggingEndRoutine(instance));
            }
        };
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
        SpriteAnimator instance = Instantiate(moudPrefab);
        instance.OnFrame += HandleFrameChange(instance.gameObject);
        instance.transform.position = hit.point;
        root.transform.position = new Vector3(hit.point.x, hit.point.y - digHeight * digTime);
    }

    public void FinishMove()
    {
        callback?.Invoke();
        callback = null;
        
    }
}
