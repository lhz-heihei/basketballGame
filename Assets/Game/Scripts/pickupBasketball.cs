using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pickupBasketball : MonoBehaviour
{
    public Player _player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "basketball")
        {
            _player.currentState = Player.CharacterState.Dribbling;
            Collider basketball_collider = other.GetComponent<SphereCollider>();
            basketball_collider.enabled = false;
            GameObject righthand = GameObject.FindGameObjectWithTag("rightHand");
            other.transform.parent = righthand.transform;
            other.transform.localPosition = new Vector3(0, 0, _player.hand_baskrtball_distance);
            
        }
    }
}
