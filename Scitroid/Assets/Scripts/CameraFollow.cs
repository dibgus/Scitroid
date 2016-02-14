using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    //Camera cam;
    float minY=0, minX=0, maxY=0, maxX=0;
	// Use this for initialization
	void Start () {
        GameObject[] allObjects = (GameObject [])GameObject.FindObjectsOfType(typeof(GameObject));
        foreach(GameObject g in allObjects)
        {
            if (g.GetComponent<SpriteRenderer>() == null) continue;
            float right = g.transform.position.x + g.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2;
            float left = g.transform.position.x - g.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2;
            float top = g.transform.position.y + g.GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
            float bottom = g.transform.position.y - g.GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
            maxX = right > maxX ? right : maxX;
            minX = left < minX ? left : minX;
            maxY = top > maxY ? top : maxY;
            minY = bottom < minY ? bottom : minY;
            print(g.name + " :::: " + maxX + ", " + minX + ", " + maxY + ", " + minY);
        }
        //cam = GetComponent<Camera>();
        print(maxX + ", " + minX + ", " + maxY + ", " + minY);
    }
	
	// Update is called once per frame
	void Update () {
        float boundsY = Camera.main.orthographicSize;
        float boundsX = Camera.main.orthographicSize * Screen.width / Screen.height;
        //float SetX = player.transform.position.x + boundsX < maxX && player.transform.position.x-boundsX > minX ? player.transform.position.x : transform.position.x;
        //float SetY = player.transform.position.y + boundsY < maxY && player.transform.position.y-boundsY > minY ? player.transform.position.y : transform.position.y;
        float SetX;
        float SetY;
        if (player.transform.position.x + boundsX < maxX && player.transform.position.x - boundsX > minX) SetX = player.transform.position.x;
        else
        {
            if (player.transform.position.x + boundsX >= maxX) SetX = maxX - boundsX;
            else SetX = minX + boundsX;
        }
        if (player.transform.position.y + boundsY < maxY && player.transform.position.y - boundsY > minY) SetY = player.transform.position.y;
        else
        {
            if (player.transform.position.y + boundsY >= maxY) SetY = maxY - boundsY;
            else SetY = minY + boundsY;
        }
        //print(boundsX + "  ::  " + maxX +" , " + minX + "  ::  " + (player.transform.position.x + boundsX) + " , " + (player.transform.position.x-boundsX));
        transform.position = new Vector3(SetX, SetY, -10);
    }
}
