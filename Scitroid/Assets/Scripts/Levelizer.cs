using UnityEngine;
using System.Collections;

public class Levelizer : MonoBehaviour
{

    public Sprite Background, FloorSprite, WallSprite, LadderSprite;
    public bool TopDoorLeft, TopDoorCenter, TopDoorRight, RightDoorTop, RightDoorCenter, RightDoorBottom, BottomDoorRight, BottomDoorCenter, BottomDoorLeft, LeftDoorBottom, LeftDoorCenter, LeftDoorTop;
    public string TopDoorLeftLv, TopDoorCenterLv, TopDoorRightLv, RightDoorTopLv, RightDoorCenterLv, RightDoorBottomLv, BottomDoorRightLv, BottomDoorCenterLv, BottomDoorLeftLv, LeftDoorBottomLv, LeftDoorCenterLv, LeftDoorTopLv;
    public GameObject[] Enemies;
    public int minEnemies, maxEnemies;

    public bool MultiScene = true;

    int wallNum = 2;
    int rightDoors = 0, leftDoors = 0, topDoors = 0, bottomDoors = 0;
    int floorNum = 2;

    float[] WallHeights;
    float[] FloorLengths;

    float[] WallOffHeights;
    float[] FloorOffLengths;

    ArrayList LadderSpots = new ArrayList();

    // Use this for initialization
    void Start()
    {
        CountDoors();
        MeasureWalls();
        MeasureFloors();
        RecalculateDoors();
        BuildBorder();
        BuildBasePlatforms();
        //GenerateIslands();
        GenerateEnemies();
        foreach (Player p in FindObjectsOfType<Player>()) p.MustReload = true;
    }

    void CountDoors()
    {
        if (TopDoorLeft) { floorNum++; topDoors++; }
        if (TopDoorCenter) { floorNum++; topDoors++; }
        if (TopDoorRight) { floorNum++; topDoors++; }
        if (BottomDoorRight) { floorNum++; bottomDoors++; }
        if (BottomDoorCenter) { floorNum++; bottomDoors++; }
        if (BottomDoorLeft) { floorNum++; bottomDoors++; }
        if (RightDoorTop) { wallNum++; rightDoors++; }
        if (RightDoorCenter) { wallNum++; rightDoors++; }
        if (RightDoorBottom) { wallNum++; rightDoors++; }
        if (LeftDoorBottom) { wallNum++; leftDoors++; }
        if (LeftDoorCenter) { wallNum++; leftDoors++; }
        if (LeftDoorTop) { wallNum++; leftDoors++; }
    }

    void MeasureWalls()
    {
        WallHeights = new float[wallNum];
        WallOffHeights = new float[wallNum];
        if (RightDoorTop)
        {
            WallHeights[0] = 80;
            WallOffHeights[0] = 216;
            if (RightDoorCenter)
            {
                WallHeights[1] = 80;
                WallOffHeights[1] = 72;
                if (RightDoorBottom)
                {
                    WallHeights[2] = 80;
                    WallHeights[3] = 80;
                    WallOffHeights[2] = -72;
                    WallOffHeights[3] = -216;
                }
                else {
                    WallHeights[2] = 224;
                    WallOffHeights[2] = -144;
                }
            }
            else if (RightDoorBottom)
            {
                WallHeights[1] = 224;
                WallHeights[2] = 80;
                WallOffHeights[1] = 0;
                WallOffHeights[2] = -216;
            }
            else {
                WallHeights[1] = 368;
                WallOffHeights[1] = -72;
            }
        }
        else if (RightDoorCenter)
        {
            WallHeights[0] = 224;
            WallOffHeights[0] = 144;
            if (RightDoorBottom)
            {
                WallHeights[1] = 80;
                WallHeights[2] = 80;
                WallOffHeights[1] = -72;
                WallOffHeights[2] = -216;
            }
            else {
                WallHeights[1] = 224;
                WallOffHeights[1] = -144;
            }
        }
        else if (RightDoorBottom)
        {
            WallHeights[0] = 368;
            WallHeights[1] = 80;
            WallOffHeights[0] = 72;
            WallOffHeights[1] = -216;
        }
        else WallHeights[0] = 512;

        if (LeftDoorTop)
        {
            WallHeights[1 + rightDoors] = 80;
            WallOffHeights[1 + rightDoors] = 216;
            if (LeftDoorCenter)
            {
                WallHeights[2 + rightDoors] = 80;
                WallOffHeights[2 + rightDoors] = 72;
                if (LeftDoorBottom)
                {
                    WallHeights[3 + rightDoors] = 80;
                    WallHeights[4 + rightDoors] = 80;
                    WallOffHeights[3 + rightDoors] = -72;
                    WallOffHeights[4 + rightDoors] = -216;
                }
                else {
                    WallHeights[3 + rightDoors] = 224;
                    WallOffHeights[3 + rightDoors] = -144;
                }
            }
            else if (LeftDoorBottom)
            {
                WallHeights[2 + rightDoors] = 224;
                WallHeights[3 + rightDoors] = 80;
                WallOffHeights[2 + rightDoors] = 0;
                WallOffHeights[3 + rightDoors] = -216;
            }
            else {
                WallHeights[2 + rightDoors] = 368;
                WallOffHeights[2 + rightDoors] = -72;
            }
        }
        else if (LeftDoorCenter)
        {
            WallHeights[1 + rightDoors] = 224;
            WallOffHeights[1 + rightDoors] = 144;
            if (LeftDoorBottom)
            {
                WallHeights[2 + rightDoors] = 80;
                WallHeights[3 + rightDoors] = 80;
                WallOffHeights[2 + rightDoors] = -72;
                WallOffHeights[3 + rightDoors] = -216;
            }
            else {
                WallHeights[2 + rightDoors] = 224;
                WallOffHeights[2 + rightDoors] = -144;
            }
        }
        else if (LeftDoorBottom)
        {
            WallHeights[1 + rightDoors] = 368;
            WallHeights[2 + rightDoors] = 80;
            WallOffHeights[1 + rightDoors] = 72;
            WallOffHeights[2 + rightDoors] = -216;
        }
        else WallHeights[1 + rightDoors] = 512;
    }

    void MeasureFloors()
    {
        FloorLengths = new float[floorNum];
        FloorOffLengths = new float[floorNum];
        if (TopDoorRight)
        {
            FloorLengths[0] = 64;
            FloorOffLengths[0] = 208;
            if (TopDoorCenter)
            {
                FloorLengths[1] = 80;
                FloorOffLengths[1] = 72;
                if (TopDoorLeft)
                {
                    FloorLengths[2] = 80;
                    FloorLengths[3] = 64;
                    FloorOffLengths[2] = -72;
                    FloorOffLengths[3] = -208;
                }
                else {
                    FloorLengths[2] = 208;
                    FloorOffLengths[2] = -136;
                }
            }
            else if (TopDoorLeft)
            {
                FloorLengths[1] = 224;
                FloorLengths[2] = 64;
                FloorOffLengths[1] = 0;
                FloorOffLengths[2] = -208;
            }
            else {
                FloorLengths[1] = 352;
                FloorOffLengths[1] = -64;
            }
        }
        else if (TopDoorCenter)
        {
            FloorLengths[0] = 208;
            FloorOffLengths[0] = 136;
            if (TopDoorLeft)
            {
                FloorLengths[1] = 80;
                FloorLengths[2] = 64;
                FloorOffLengths[1] = -72;
                FloorOffLengths[2] = -208;
            }
            else {
                FloorLengths[1] = 208;
                FloorOffLengths[1] = -136;
            }
        }
        else if (TopDoorLeft)
        {
            FloorLengths[0] = 352;
            FloorLengths[1] = 64;
            FloorOffLengths[0] = 64;
            FloorOffLengths[1] = -208;
        }
        else FloorLengths[0] = 480;

        if (BottomDoorRight)
        {
            FloorLengths[1 + topDoors] = 64;
            FloorOffLengths[1 + topDoors] = 208;
            if (BottomDoorCenter)
            {
                FloorLengths[2 + topDoors] = 80;
                FloorOffLengths[2 + topDoors] = 72;
                if (BottomDoorLeft)
                {
                    FloorLengths[3 + topDoors] = 80;
                    FloorLengths[4 + topDoors] = 64;
                    FloorOffLengths[3 + topDoors] = -72;
                    FloorOffLengths[4 + topDoors] = -208;
                }
                else {
                    FloorLengths[3 + topDoors] = 208;
                    FloorOffLengths[3 + topDoors] = -136;
                }
            }
            else if (BottomDoorLeft)
            {
                FloorLengths[2 + topDoors] = 224;
                FloorLengths[3 + topDoors] = 64;
                FloorOffLengths[2 + topDoors] = 0;
                FloorOffLengths[3 + topDoors] = -208;
            }
            else {
                FloorLengths[2 + topDoors] = 352;
                FloorOffLengths[2 + topDoors] = -64;
            }
        }
        else if (BottomDoorCenter)
        {
            FloorLengths[1 + topDoors] = 208;
            FloorOffLengths[1 + topDoors] = 136;
            if (BottomDoorLeft)
            {
                FloorLengths[2 + topDoors] = 80;
                FloorLengths[3 + topDoors] = 64;
                FloorOffLengths[2 + topDoors] = -72;
                FloorOffLengths[3 + topDoors] = -208;
            }
            else {
                FloorLengths[2 + topDoors] = 208;
                FloorOffLengths[2 + topDoors] = -136;
            }
        }
        else if (BottomDoorLeft)
        {
            FloorLengths[1 +topDoors] = 352;
            FloorLengths[2 + topDoors] = 64;
            FloorOffLengths[1 + topDoors] = 64;
            FloorOffLengths[2 + topDoors] = -208;
        }
        else FloorLengths[1 + topDoors] = 480;
    }

    void RecalculateDoors()
    {
        for (int i = 0; i < wallNum; i++)
        {
            WallHeights[i] *= transform.localScale.y;
            WallOffHeights[i] *= transform.localScale.y;
        }
        for (int i = 0; i < floorNum; i++)
        {
            FloorLengths[i] *= transform.localScale.x;
            FloorOffLengths[i] *= transform.localScale.x;
        }
    }

    void BuildBorder()
    {
        for (int i = 0; i < wallNum; i++)
        {
            GameObject waller = new GameObject();
            waller.transform.parent = transform;
            waller.tag = "Wall";
            waller.name = "Wall " + i;
            BoxCollider2D collider = waller.AddComponent<BoxCollider2D>();
            TiledCollider tile = waller.AddComponent<TiledCollider>();
            tile.sprite = WallSprite;
            collider.size = new Vector2(WallSprite.bounds.size.x, WallHeights[i] / 100f);
            if (i <= rightDoors) waller.transform.position = new Vector3((transform.position.x + Background.bounds.size.x / 2 - WallSprite.bounds.size.x / 2) * transform.localScale.x - (WallSprite.bounds.size.x / 2) * (transform.localScale.x - 1), WallOffHeights[i] / 100f + transform.position.y);
            else waller.transform.position = new Vector3((transform.position.x - Background.bounds.size.x / 2 + WallSprite.bounds.size.x / 2) * transform.localScale.x + (WallSprite.bounds.size.x / 2) * (transform.localScale.x - 1), WallOffHeights[i] / 100f + transform.position.y);
        }

        for (int i = 0; i < floorNum; i++)
        {
            GameObject floorer = new GameObject();
            floorer.transform.parent = transform;
            floorer.layer = 10;
            floorer.name = "Floor " + i;
            BoxCollider2D collider = floorer.AddComponent<BoxCollider2D>();
            TiledCollider tile = floorer.AddComponent<TiledCollider>();
            tile.sprite = FloorSprite;
            collider.size = new Vector2(FloorLengths[i] / 100f, FloorSprite.bounds.size.y);
            if (i <= topDoors) floorer.transform.position = new Vector3(FloorOffLengths[i] / 100f + transform.position.x, (transform.position.y + Background.bounds.size.y / 2 - FloorSprite.bounds.size.y / 2) * transform.localScale.y + (WallSprite.bounds.size.y / 2) * (transform.localScale.y - 1));
            else floorer.transform.position = new Vector3(FloorOffLengths[i] / 100f + transform.position.x, (transform.position.y - Background.bounds.size.y / 2 + FloorSprite.bounds.size.y / 2) * transform.localScale.y - (WallSprite.bounds.size.y / 2) * (transform.localScale.y - 1));
        }
    }

    void BuildBasePlatforms()
    {
        if (rightDoors > 0)
        {
            for (int i = 0; i < rightDoors; i++)
            {
                int[] ActiveDoors = new int[3];
                string[] doorNames = { RightDoorTopLv, RightDoorCenterLv, RightDoorBottomLv };
                if (RightDoorTop) ActiveDoors[0] = 1;
                if (RightDoorCenter) ActiveDoors[1] = 1;
                if (RightDoorBottom) ActiveDoors[2] = 1;
                int doorCounter = 0;
                GameObject waller = new GameObject();
                waller.transform.parent = transform;
                waller.name = "Right Base " + i;
                BoxCollider2D collider = waller.AddComponent<BoxCollider2D>();
                TiledCollider tile = waller.AddComponent<TiledCollider>();
                tile.sprite = WallSprite;
                int width = Random.Range(1, 6);
                collider.size = new Vector2(width * WallSprite.bounds.size.x, WallSprite.bounds.size.y);
                waller.transform.position = new Vector3((transform.position.x + Background.bounds.size.x / 2 - WallSprite.bounds.size.x / 2) * transform.localScale.x - (WallSprite.bounds.size.x / 2) * (transform.localScale.x - 1) - width * WallSprite.bounds.size.x / 2 - WallSprite.bounds.size.x / 2, WallOffHeights[i + 1] / 100f + WallHeights[i + 1] / 200f - WallSprite.bounds.size.y / 2 + transform.position.y);
                LadderSpots.Add(new Vector3(collider.bounds.min.x - LadderSprite.bounds.size.x / 2, collider.bounds.max.y));
                float difference = Mathf.Abs((((WallOffHeights[i] - WallHeights[i] / 2)) - ((WallOffHeights[i + 1] + WallHeights[i + 1] / 2))) / 100f);
                float avg = (((WallOffHeights[i] - WallHeights[i] / 2)) + ((WallOffHeights[i + 1] + WallHeights[i + 1] / 2))) / 200f;
                if (MultiScene) {
                    GameObject door = new GameObject();
                    door.transform.parent = transform;
                    door.name = "Level Door";
                    BoxCollider2D doorCollider = door.AddComponent<BoxCollider2D>();
                    SceneChange scenerizer = door.AddComponent<SceneChange>();
                    doorCollider.isTrigger = true;
                    doorCollider.size = new Vector3(FloorSprite.bounds.size.x, difference);
                    door.transform.position = new Vector3((transform.position.x + Background.bounds.size.x / 2 - FloorSprite.bounds.size.x / 2) * transform.localScale.x + (WallSprite.bounds.size.x / 2) * (transform.localScale.x - 1), avg);
                    if (ActiveDoors[i + doorCounter] == 1)
                    {
                        scenerizer.LevelName = doorNames[i + doorCounter];
                        ActiveDoors[i + doorCounter] = 0;
                    }
                    else doorCounter++;
                }
            }
        }
        if (leftDoors > 0)
        {
            for (int i = 0; i < leftDoors; i++)
            {
                GameObject waller = new GameObject();
                waller.transform.parent = transform;
                waller.name = "Left Base " + i;
                BoxCollider2D collider = waller.AddComponent<BoxCollider2D>();
                TiledCollider tile = waller.AddComponent<TiledCollider>();
                tile.sprite = WallSprite;
                int width = Random.Range(1, 6);
                collider.size = new Vector2(width * WallSprite.bounds.size.x, WallSprite.bounds.size.y);
                waller.transform.position = new Vector3((transform.position.x - Background.bounds.size.x / 2 + WallSprite.bounds.size.x / 2) * transform.localScale.x + (WallSprite.bounds.size.x / 2) * (transform.localScale.x - 1) + width * WallSprite.bounds.size.x / 2 + WallSprite.bounds.size.x / 2, WallOffHeights[i+rightDoors+2] / 100f + WallHeights[i+rightDoors+2] / 200f - WallSprite.bounds.size.y / 2 + transform.position.y);
                LadderSpots.Add(new Vector3(collider.bounds.max.x + LadderSprite.bounds.size.x / 2, collider.bounds.max.y));
            }
        }
        if (topDoors > 0)
        {
            int[] ActiveDoors = new int[3];
            string[] doorNames = { TopDoorLeftLv, TopDoorCenterLv, TopDoorRightLv };
            if (TopDoorLeft) ActiveDoors[0] = 1;
            if (TopDoorCenter) ActiveDoors[1] = 1;
            if (TopDoorRight) ActiveDoors[2] = 1;
            int doorCounter = 0;
            for (int i = 0; i < topDoors; i++)
            {
                GameObject floorer = new GameObject();
                floorer.transform.parent = transform;
                floorer.name = "Top Base " + i;
                BoxCollider2D collider = floorer.AddComponent<BoxCollider2D>();
                TiledCollider tile = floorer.AddComponent<TiledCollider>();
                tile.sprite = FloorSprite;
                float difference = Mathf.Abs((((FloorOffLengths[i] - FloorLengths[i] / 2)) - ((FloorOffLengths[i + 1] + FloorLengths[i + 1] / 2))) / 100f);
                float avg = (((FloorOffLengths[i] - FloorLengths[i] / 2)) + ((FloorOffLengths[i + 1] + FloorLengths[i + 1] / 2))) / 200f;
                collider.size = new Vector2(difference, FloorSprite.bounds.size.y);
                floorer.transform.position = new Vector3(avg + transform.position.x, (transform.position.y + Background.bounds.size.y / 2 - FloorSprite.bounds.size.y / 2) * transform.localScale.y + (WallSprite.bounds.size.y / 2) * (transform.localScale.y - 1) - 4 * FloorSprite.bounds.size.y);
                if (MultiScene)
                {
                    GameObject door = new GameObject();
                    door.transform.parent = transform;
                    door.name = "Level Door";
                    BoxCollider2D doorCollider = door.AddComponent<BoxCollider2D>();
                    SceneChange scenerizer = door.AddComponent<SceneChange>();
                    doorCollider.isTrigger = true;
                    doorCollider.size = new Vector3(difference, FloorSprite.bounds.size.y);
                    door.transform.position = new Vector3(avg + transform.position.x, (transform.position.y + Background.bounds.size.y / 2 - FloorSprite.bounds.size.y / 2) * transform.localScale.y + (WallSprite.bounds.size.y / 2) * (transform.localScale.y - 1));
                    if (ActiveDoors[i + doorCounter] == 1)
                    {
                        scenerizer.LevelName = doorNames[i + doorCounter];
                        ActiveDoors[i + doorCounter] = 0;
                    }
                    else doorCounter++;
                }
            }
        }
        for (int i = 0; i < LadderSpots.Count; i++)
        {
            GameObject ladderer = new GameObject();
            ladderer.transform.parent = transform;
            ladderer.name = "Ladder " + i;
            BoxCollider2D collider = ladderer.AddComponent<BoxCollider2D>();
            TiledCollider tile = ladderer.AddComponent<TiledCollider>();
            float dist = Background.bounds.size.y / 2 + (((Vector3)LadderSpots[i]).y - transform.position.y + LadderSprite.bounds.size.y/2);
            RaycastHit2D hit = Physics2D.Raycast((Vector3)LadderSpots[i], Vector2.down);
            //if(hit.collider == null)collider.size = new Vector2(LadderSprite.bounds.size.x, hit.distance);
            //collider.size = new Vector2(LadderSprite.bounds.size.x, dist - LadderSprite.bounds.size.y);//LadderSprite.bounds.size.y * (int)(hit.distance / LadderSprite.bounds.size.y));
            collider.size = new Vector2(LadderSprite.bounds.size.x, LadderSprite.bounds.size.y * (int)(hit.distance / LadderSprite.bounds.size.y));
            collider.isTrigger = true;
            tile.sprite = LadderSprite;
            ladderer.transform.position = (Vector3)LadderSpots[i] - new Vector3(0, (collider.size.y) / 2);
        }
    }

    void GenerateIslands()
    {
        int max = Random.Range(1, 4);
        for (int i = 0; i < max; i++)
        {
            GameObject floorer = new GameObject();
            floorer.transform.parent = transform;
            floorer.name = "Island " + i;
            BoxCollider2D collider = floorer.AddComponent<BoxCollider2D>();
            TiledCollider tile = floorer.AddComponent<TiledCollider>();
            RaycastHit2D hit = Physics2D.Raycast((Vector3)LadderSpots[i], Vector2.down);
            collider.size = new Vector2(LadderSprite.bounds.size.x, LadderSprite.bounds.size.y * (int)(hit.distance / LadderSprite.bounds.size.y));
            collider.isTrigger = true;
            tile.sprite = LadderSprite;
            floorer.transform.position = (Vector3)LadderSpots[i] - new Vector3(0, collider.size.y / 2);
        }
    }

    void GenerateEnemies()
    {
        if (Enemies.Length > 0) {
            int numEnemies = Random.Range(minEnemies, maxEnemies);
            for(int i = 0; i < numEnemies; i++)
            {
                int index = 0;
                if (Enemies.Length > 1) index = Random.Range(0, Enemies.Length);
                float vertRange = Random.Range(transform.position.y - Background.bounds.size.y + FloorSprite.bounds.size.y + Enemies[index].GetComponent<SpriteRenderer>().bounds.size.y*2, transform.position.y + Background.bounds.size.y - FloorSprite.bounds.size.y -Enemies[index].GetComponent<SpriteRenderer>().bounds.size.y*2);
                float horizRange = Random.Range(transform.position.x - Background.bounds.size.x + WallSprite.bounds.size.x + Enemies[index].GetComponent<SpriteRenderer>().bounds.size.x*2, transform.position.x + Background.bounds.size.x - WallSprite.bounds.size.x - Enemies[index].GetComponent<SpriteRenderer>().bounds.size.x*2);
                RaycastHit2D hit = Physics2D.Raycast(new Vector2(horizRange, vertRange), Vector2.down);
                Instantiate(Enemies[index], hit.point + new Vector2(0,Enemies[index].GetComponent<SpriteRenderer>().bounds.size.y / 2), Quaternion.identity);
            }
        }
    }
}