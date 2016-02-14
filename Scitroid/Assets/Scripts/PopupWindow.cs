using UnityEngine;
using System.Collections;

public class PopupWindow : MonoBehaviour {

    public static int id = 0;
    public bool visible = false;
    public bool isDialog = true;
    public string[] dialogTag = { "Player", "Enemy" };
    public int timer = 500;
    public GUISkin skin;
    public bool isFollowing = true;
    public bool isTemporary = true;
    public bool draggable = false;
    public float width = 10f;
    public float height = 10f;
    public string title = "Dank memes";
    public string[] messages = { "dank memes don't melt steel beams", "My mom once told me to be awoosome", "Howdy", "We done did a game", "I'm just a regular copied asset!", "...", "Get outta my way" };
    public string[] altMessages = { "Sup brutha", "Look at those chumps...", "We have the world to ourselves" };
    private int currentMessage = -1;
    private int triggeredTag = -1;
    private int messageDelay = 0;
    public bool randomMessages = true;
    public int charWidth = 1; //charwidth will override setting width and height, -1 or 0 disables charwidth
    //public float x = 0f, y = 0f;
    public GameObject follower;
    public Rect window = new Rect(3, 3, 10f, 10f);
    void OnGUI()
    {
        if (visible)
        {
            if (charWidth != -1 && charWidth != 0)
            {
                //height = (message.Length * GUI.skin.font.fontSize * 2) / (charWidth * GUI.skin.font.fon)tSize;
                //width = (message.Length * GUI.skin.font.fontSize) / (height);
                height = 50f;
                width = 100f;
            }
            if (isFollowing) //TODO fix
            {
                window.position = Camera.main.WorldToScreenPoint(follower.transform.position);
                window.position = new Vector3(window.position.x - follower.GetComponent<SpriteRenderer>().sprite.rect.width, Screen.height - window.position.y - follower.GetComponent<SpriteRenderer>().sprite.rect.height * 2 - height);
            }

            GUILayout.Window(id++, window, doFade, title, GUILayout.Width(width), GUILayout.Height(height));
        }
    }

    void doFade(int windowID)
    {
        if (messageDelay > 0)
            messageDelay--;
        if (draggable)
            GUI.DragWindow();
        GUI.skin = skin;
        if (currentMessage == -1)
            currentMessage = (int)Mathf.Round((randomMessages) ? Random.Range(0f, triggeredTag == 0 || triggeredTag == -1 ? messages.Length - 1 : altMessages.Length - 1) : 0);
        GUILayout.Label(triggeredTag == 0 || triggeredTag == -1 ? messages[currentMessage] : altMessages[currentMessage]);
        //print(timer);
        if (isTemporary && timer <= 0)
        {
            if (isDialog)
            {
                currentMessage = -1;
                visible = false;
                triggeredTag = -1;
            }
            else
                Destroy(this);
        }
        else if (isTemporary)
            timer--;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        print("TOUCHDOWN: " + col.collider.tag);
        for (int i = 0; i < dialogTag.Length; i++)
            if (col.collider.tag == dialogTag[i] && isDialog)
            {
                if (messageDelay <= 0)
                { 
                    currentMessage = -1;
                    messageDelay = 10;
                }
                triggeredTag = i;
            isTemporary = true;
            timer = 400;
            visible = true;
        }
    }
}
