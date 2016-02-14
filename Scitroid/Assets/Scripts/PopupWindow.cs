using UnityEngine;
using System.Collections;

public class PopupWindow : MonoBehaviour {

    public int timer = 500;
    public GUISkin skin;
    public bool isFollowing = true;
    public bool isTemporary = true;
    public bool draggable = false;
    public float width = 10f;
    public float height = 10f;
    public string title = "Dank memes";
    public string message = "dank memes don't melt steel beams";
    public int charWidth = 1; //charwidth will override setting width and height, -1 or 0 disables charwidth

    //public float x = 0f, y = 0f;
    public GameObject follower;
    public Rect window = new Rect(3, 3, 10f, 10f);
    void OnGUI()
    {
        if (charWidth != -1 && charWidth != 0)
        { 
            height = (message.Length * GUI.skin.font.fontSize) / (charWidth * GUI.skin.font.fontSize);
            width = (message.Length * GUI.skin.font.fontSize) / (height);
        }
        if (isFollowing) //TODO fix
        {
            window.position = Camera.main.WorldToScreenPoint(follower.transform.position);
            window.position = new Vector3(window.position.x - follower.GetComponent<SpriteRenderer>().sprite.rect.width / 2, Screen.height - window.position.y - follower.GetComponent<SpriteRenderer>().sprite.rect.height * 2 - height * 3);
        }

        GUILayout.Window(0, window, doFade, title, GUILayout.Width(width), GUILayout.Height(height));
    }

    void doFade(int windowID)
    {
        if (draggable)
            GUI.DragWindow();
        GUI.skin = skin;
        GUILayout.Label(message);
        //print(timer);
        if (isTemporary && timer <= 0)
            Destroy(this);
        else if(isTemporary)
            timer--;
    }
}
