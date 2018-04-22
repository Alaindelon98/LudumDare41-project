using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StoreManager : MonoBehaviour {


    public bool placingAction;
    private bool canBePlaced;
    bool placeableTop, placeableRight, placeableLeft;

    public GameObject JumpPrefab;

    public Tilemap levelTiles;

    private Vector3 mousePos;

    private SpriteRenderer newActionRenderer;
    private BoxCollider2D newActionCollider;
    private GameObject newAction;

	void Start () {
        placingAction = false;
        newAction = null;
	}
	
	void Update () {
        //Debug.Log(placingAction);
		if (!placingAction)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Place new Action");
                NewAction();
                placingAction = true;
            }
           
        }

        else if (placingAction && newAction)
        {
            FindOrPlace();
        }



	}
    void FindOrPlace()
    {
        canBePlaced = false;
        placeableRight = false;
        placeableLeft = false;
        placeableTop = false;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);

        Vector3Int mousePosInt = new Vector3Int((int)Mathf.Floor(mousePos.x), (int)Mathf.Floor(mousePos.y), (int)Mathf.Floor(mousePos.z));

        Vector3Int selectedCellPos = levelTiles.WorldToCell(mousePosInt);

        if (levelTiles.GetTile(selectedCellPos) != null)
        {
            if (levelTiles.GetTile(new Vector3Int(selectedCellPos.x, selectedCellPos.y + 1, 0)) == null && levelTiles.GetTile(new Vector3Int(selectedCellPos.x, selectedCellPos.y + 2, 0)) == null)
            {
                //Debug.Log("CAN BE PLACED TOP");
                placeableTop = true;
            }
            else if (levelTiles.GetTile(new Vector3Int(selectedCellPos.x + 1, selectedCellPos.y, 0)) == null && levelTiles.GetTile(new Vector3Int(selectedCellPos.x + 2, selectedCellPos.y, 0)) == null)
            {
                //Debug.Log("CAN BE PLACED RIGHT");
                placeableRight = true;
            }
            else if (levelTiles.GetTile(new Vector3Int(selectedCellPos.x - 1, selectedCellPos.y, 0)) == null && levelTiles.GetTile(new Vector3Int(selectedCellPos.x - 2, selectedCellPos.y, 0)) == null)
            {
                //Debug.Log("CAN BE PLACED LEFT");
                placeableLeft = true;
            }
        }

        if (placeableTop || placeableRight || placeableLeft)
        {
            canBePlaced = true;
            ChangeColor("canPlace");
        }
        else
        {
            canBePlaced = false;
            ChangeColor("cannotPlace");
        }


        if (!canBePlaced)
        {
            newAction.transform.position = mousePos;
        }

        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //Debug.Log("Placed action and can be placed: " + canBePlaced);
                placingAction = false;
                ChangeColor("placed");
                newActionCollider.enabled = true;
            }
            newAction.transform.position = levelTiles.GetCellCenterWorld(selectedCellPos);
        }
    }
    void ChangeColor(string type)
    {
        Color tmp = newActionRenderer.color;

        switch (type)
        {
            case "cannotPlace":
                tmp.r = 1f;
                tmp.g = 0f;
                tmp.b = 0f;
                tmp.a = 75f / 255;
                break;

            case "canPlace":
                tmp.r = 0f;
                tmp.g = 1f;
                tmp.b = 0f;
                tmp.a = 75f / 255;
                break;

            case "placed":
                tmp.r = 1f;
                tmp.g = 1f;
                tmp.b = 1f;
                tmp.a = 1f;
                break;
        }

        newActionRenderer.color = tmp;

    }

    void NewAction()
    {
        newAction = Instantiate(JumpPrefab, mousePos, Quaternion.identity);
        newActionRenderer = newAction.GetComponent<SpriteRenderer>();
        newActionCollider = newAction.GetComponent<BoxCollider2D>();

        newActionCollider.enabled = false;

       
        canBePlaced = false;
    }
}
