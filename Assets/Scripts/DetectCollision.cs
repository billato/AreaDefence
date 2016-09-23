 using UnityEngine;
 using System.Collections;
 
 public class DetectCollision : MonoBehaviour
 {
 
     //public GameObject playerExplosion;
 
 
     void OnTriggerEnter (Collider other) 
     {
         Debug.Log ("Collision Detected");
 
         //if (other.tag == "Missile")
         //{
            // Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			Debug.LogError ("TERRRAIN touched by " + other.gameObject.tag);

			//Destroy(other.gameObject);


         //}
     }
 }