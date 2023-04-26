using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CharacterController : MonoBehaviour
{
    GameObject character;
    CharacterDirector.PlayerState state;

    void Update()
    {
        state = GameDirector.Instance.Character.State;
        character = GameDirector.Instance.Character.PlayableCharacter;

        if (state == CharacterDirector.PlayerState.King)
        {
            character.GetComponent<KingController>().Tick();
        }

        else if(state == CharacterDirector.PlayerState.Murder)
        {
            character.GetComponent<MurderController>().Tick();
        }
    }
}
