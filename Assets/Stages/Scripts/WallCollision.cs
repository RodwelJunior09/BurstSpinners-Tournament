using UnityEngine;

public class WallCollision : MonoBehaviour
{
    string playerTag;
    Animator myAnimator;
    [SerializeField, Range(1, 5)] float timeToDestroy = 1.5f; 
    private void Start() {
        myAnimator = GetComponent<Animator>();
        playerTag = FindObjectOfType<Player>().tag;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag(playerTag))
        {
            if (this.gameObject.CompareTag("Wall#1"))
                myAnimator.SetTrigger("DropWall#1");

            if (this.gameObject.CompareTag("Wall#2"))
                myAnimator.SetTrigger("DropWall#2");

            if (this.gameObject.CompareTag("Wall#3"))
                myAnimator.SetTrigger("DropWall#3");

            if (this.gameObject.CompareTag("Wall#4"))
                myAnimator.SetTrigger("DropWall#4");
            
            Destroy(this.gameObject, timeToDestroy);
        }
    }
}
