using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class SceneChange : MonoBehaviour {

    public string LevelName;
    bool Triggered = false;

    static int[] UsedRooms = new int[0];

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!Triggered && col.gameObject.GetComponent<Player>()!= null)
        {
            int levelFound = -1;
            try 
            {
                
                int.TryParse(LevelName.Substring(5), out levelFound);
                
            }
            catch(Exception e)
            {
                print("FAIL!");
            }
            if (ArrayContains(levelFound)) return;
            AddToArray(levelFound);
            SceneManager.LoadScene(LevelName, LoadSceneMode.Additive);
            Player player = col.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.MustReload = true;
                player.SceneSwitch = true;
                //player.SwitchTimer = 0.2f;
            }
            Triggered = true;
        }
    }

    void AddToArray(int added)
    {
        int[] BetterInts = new int[UsedRooms.Length + 1];
        for(int i = 0; i < UsedRooms.Length; i++)BetterInts[i] = UsedRooms[i];
        BetterInts[BetterInts.Length - 1] = added;
        UsedRooms = BetterInts;
    }

    bool ArrayContains(int num)
    {
        for (int i = 0; i < UsedRooms.Length; i++) if (UsedRooms[i] == num) return true;
        return false;
    }
}
