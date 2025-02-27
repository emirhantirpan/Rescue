using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CollectableItems : MonoBehaviour
{
    public Player player;

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            IncreaseScore();
        }
    }
    public void IncreaseScore()
    {
        player.score += 10;
    }
}
