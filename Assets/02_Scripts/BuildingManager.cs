using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;//배열에 담아둠
    public GameObject pendingObject;
    Vector3 pos;

    RaycastHit hit;

    [SerializeField] LayerMask layerMask;

    public float gridSize;
    bool gridOn = true;
    [SerializeField] Toggle gridToggle;

    public float rotateAmount = 45f;
    void Start()
    {

    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
    }

    public void Update()
    {
        if(pendingObject != null)
        {
            if (gridOn)
            {

                pendingObject.transform.position = new Vector3
                    (RoundToNearesGrid(pos.x), RoundToNearesGrid(pos.y), RoundToNearesGrid(pos.z));
            }
            else
            {
                pendingObject.transform.position = pos;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }

            if (Input.GetKey(KeyCode.R))
            {
                RotateObject();
            }

        }
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }else
        {
            gridOn = false;
        }
    }

    float RoundToNearesGrid(float pos)
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }

        return pos;
    }
    void PlaceObject()
    {
        pendingObject = null;
    }

    void RotateObject()
    {
        pendingObject.transform.Rotate(Vector3.up, rotateAmount);
    }
}
