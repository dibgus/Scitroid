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
    public string message = "du";
    public int charWidth = 1; //charwidth will override setting width and height, -1 or 0 disables charwidth

    //public float x = 0f, y = 0f;
    public GameObject follower;
    public Rect window = new Rect(3, 3, 10f, 10f);
    void OnGUI()
    {
        //print("Created a dialog");
        //print(x + " " + y);
        print(Screen.width + " " + Screen.height); //748 * 292
        if (charWidth != -1 && charWidth != 0)
        { 
            height = (message.Length * GUI.skin.font.fontSize) / (charWidth * GUI.skin.font.fontSize);
            width = (message.Length * GUI.skin.font.fontSize) / (height);
        }
        if (isFollowing) //TODO fix
            window.position = new Vector3(follower.transform.position.x * ( 1000f / 4.2f) + (Screen.width * 350 / 750), -follower.transform.position.y * (Screen.height * 130 / 280) + Screen.height * 105 / 185);
        GUILayout.Window(0, window, doFade, "Popup", GUILayout.Width(width), GUILayout.Height(height));
    }

    void doFade(int windowID)
    {
        GUI.skin = skin;
        GUILayout.Label(message);
        //print(timer);
        if (isTemporary && timer <= 0)
            Destroy(this);
        else
            timer--;
        if (draggable)
            GUI.DragWindow();
    }
}
