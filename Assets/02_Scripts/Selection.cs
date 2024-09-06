using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{

    public GameObject[] selectedObject;

    public Text objNameText;

    BuildingManager buildingManager;

    public GameObject objUi;
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Object"))//프리팹 오브젝트 태그 설정
                {
                    Select(hit.collider.gameObject);//이 함수가 해야할 일
                    //아웃라인 연출을 위해 프리팹에 아웃라인 스크립트를 적용하지 않고 시작
                    //처음 생성해서 이동중일때는 아웃라인이 적용되지 않다가 위치된 이후부터 아웃라인 적용하게하려고
                    //Oulice outline = obj.Getcomponent<Outline>();
                    //obj.AddComponent<Outline>();
                }
            }
        }
       if (Input.GetMouseButtonDown(1) && selectedObject[0] != null)
        {
            Deselect();
        }

        
    }
    
    void Select(GameObject obj)
    {
        selectedObject[0].GetComponent<Outline>().enabled = true;

        //이미 선택된 오브젝트를 다시 선택하려고 하면 그냥 빠져나오게
        if(obj == selectedObject[0])
        {
            return;
        }
        //같은 오브젝트가 아닌 다른 오브젝트를 선택했다면 기존에 선택되어 담겨있는 오브젝트를 해제할것이다(null)
        if(selectedObject != null)
        {
            Deselect();
        }

        Outline outline = obj.GetComponent<Outline>();

        if(outline == null)
        {
            //처음생성 시킨 시점에는 아웃라인이 필요없다가
            //클릭해서 위치시켯을때 적용
            obj.AddComponent<Outline>();
        }
        else//이후부터는 아웃라인 컴퍼넌트가 있기 때문에 이코드로 적용
        {
            outline.enabled = true;
        }

        objNameText.text = obj.name;
        objUi.SetActive(true);
        selectedObject[0] = obj;
    }

    void Deselect()
    {
        objUi.SetActive(false);
        selectedObject[0].GetComponent<Outline>().enabled = false;
        selectedObject = null;
    }

    public void Move()
    {
        buildingManager.pendingObject = selectedObject[0];
    }

    public void Delete()
    {
        GameObject objToDestroy = selectedObject[0];
        Deselect();
        Destroy(objToDestroy);
    }

}
