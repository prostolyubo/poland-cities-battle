using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer target;

    public event Action<int, int> OnFrame;
    public List<SpriteAnimationState> states;
    int stateId;
    int frame;
    float lapsed;
    public bool manualUpdate;

    public void SetFirstState()
    {
        SetState(1, 0);
    }

    public void SetState(int state, int startFrame = 0)
    {
        lapsed = 0;
        stateId = state;
        frame = startFrame;
        enabled = true;
    }

    public void Step()
    {
        var state = states[stateId];
        int relativeFrame = frame % state.sprites.Count;
        target.sprite = state.sprites[relativeFrame];
        OnFrame?.Invoke(stateId, frame);
        lapsed = state.frameTime;
        if (state.passOn && relativeFrame == state.sprites.Count - 1)
        {
            SetState(++stateId % states.Count, 0);
            return;
        }
        else if (!manualUpdate)
        {
            enabled = state.loop || relativeFrame < state.sprites.Count - 1;
        }
        frame++;
    }

    public void Update()
    {
        if (manualUpdate)
            return;

        while (lapsed > 0)
        {
            lapsed -= Time.deltaTime;
            return;
        }
        Step();
    }

    [Serializable]
    public class SpriteAnimationState
    {
        public List<Sprite> sprites;
        public float frameTime;
        public bool loop, passOn;
    }
}
