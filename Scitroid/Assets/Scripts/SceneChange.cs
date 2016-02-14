using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public string LevelName;

    void OnTriggerEnter2D(Collider2D col)
    {
        SceneManager.LoadScene(LevelName, LoadSceneMode.Additive);
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null) player.MustReload = true;
    }
}
