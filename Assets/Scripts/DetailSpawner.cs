using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailSpawner : MonoBehaviour{
    public Sprite[] decals;

    Stack<DecalAnimator> freeSpawns = new Stack<DecalAnimator>();
    public AnimationCurve scaleAnim, alphaAnim;
    public float time;
    public Transform container;

    private void Awake()
    {
        DamageDealer.OnPlayerHit += HandleHit;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        HandleHit(Vector3.zero);
    }

    private void HandleHit(Vector3 position)
    {
        DecalAnimator spawn;
        if (freeSpawns.Count > 0)
            spawn = freeSpawns.Pop();
        else
            spawn = Spawn();

        spawn.Play(this, position, decals[UnityEngine.Random.Range(0, decals.Length)]);
    }

    private DecalAnimator Spawn()
    {
        return new GameObject().AddComponent<DecalAnimator>().Make();
    }

    private void Return(DecalAnimator decalAnimator)
    {
        decalAnimator.self.SetParent(container);
        freeSpawns.Push(decalAnimator);
    }

    [RequireComponent(typeof(SpriteRenderer))]
    private class DecalAnimator : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        public Transform self;

        public DecalAnimator Make()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "Details";
            self = transform;
            return this;
        }

        public void Play(DetailSpawner spawner, Vector3 position, Sprite sprite)
        {
            self.SetParent(null);
            self.position = position;
            spriteRenderer.sprite = sprite;
            StartCoroutine(PlayingRoutine(spawner));
        }
        
        private IEnumerator PlayingRoutine(DetailSpawner spawner)
        {
            float left = spawner.time;
            while (left > 0)
            {
                self.localScale = Vector3.one * spawner.scaleAnim.Evaluate(1 - left / spawner.time);
                spriteRenderer.color = new Color(1, 1, 1, spawner.alphaAnim.Evaluate(1 - left / spawner.time));
                left -= Time.deltaTime;
                yield return null;
            }
            spawner.Return(this);
        }
    }
}