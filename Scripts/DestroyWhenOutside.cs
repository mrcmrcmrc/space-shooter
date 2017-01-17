using UnityEngine;
using System.Collections;

public class DestroyWhenOutside : MonoBehaviour {

	void Update () {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
         //TODO: meteorlar için ayrıu bşr scrşpt oluşturursan ordan hallet

        if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
        (transform.position.y < min.y) || (transform.position.y > max.y))
        {
            gameObject.SetActive(false);
        }
   }
        
}
