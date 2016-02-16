using UnityEngine;
using System.Collections;

public class TiledCollider : MonoBehaviour
{

    public Sprite sprite;
    bool ladder;
    GameObject spritefab;

    // Use this for initialization
    void Start()
    {
        MakeSpriteObject();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        ladder = collider.isTrigger;
        if (collider == null) return;
     
        Vector2 spriteSize = sprite.bounds.size;
        Vector2 boxSize = collider.bounds.size;
        Vector3 scale = transform.localScale;
        
        /*print(scale + "   <<>>   " + boxSize);
        float a = boxSize.x * scale.x, b = boxSize.y * scale.y;
        collider.size = new Vector2(a, b);
        collider.bounds.size.Set(a, b, 0);
        transform.localScale = new Vector3(0, 0);
        boxSize = collider.bounds.size;
        print(a+" <> "+b);*/
        float startWidth = boxSize.x;
        float startHeight = boxSize.y;
        //boxSize = new Vector2((spriteSize.x * (boxSize.x / spriteSize.x)), spriteSize.y * (boxSize.y / spriteSize.y));
        //collider.size = boxSize;
        //collider.size = new Vector2((spriteSize.x * (int)((int)boxSize.x / (int)spriteSize.x)), spriteSize.y * (int)((int)boxSize.y / (int)spriteSize.y));

        boxSize = collider.bounds.size;
        float i = 0, k = 0;
        for (i = spriteSize.y / 2; i < boxSize.y; i += spriteSize.y)
        {
            for (k = spriteSize.x / 2; k < boxSize.x; k += spriteSize.x)
            {
                Object thingy = Instantiate(spritefab, new Vector3(collider.bounds.min.x + k, collider.bounds.min.y + i, transform.position.z), transform.rotation);
                ((GameObject)thingy).transform.parent = this.transform;
            }
        }
        //float height = spriteSize.x * (Mathf.Round(boxSize.x / spriteSize.x));
        //float width = spriteSize.y * (Mathf.Round(boxSize.y / spriteSize.y));
        k -= spriteSize.x / 2;
        i -= spriteSize.y / 2;
        collider.size = new Vector2(k / scale.x, i / scale.y);
        collider.bounds.size.Set(k / scale.x, i / scale.y, 0);
        transform.position = new Vector3(transform.position.x + (k - startWidth) / 2, transform.position.y + (i - startHeight) / 2, transform.position.z);
        Destroy(spritefab);
    }

    void MakeSpriteObject()
    {
        spritefab = new GameObject();
        spritefab.AddComponent<SpriteRenderer>();
        spritefab.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public bool GetLadder() { return ladder; }
}
