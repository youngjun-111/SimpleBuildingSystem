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
                if (hit.collider.gameObject.CompareTag("Object"))//������ ������Ʈ �±� ����
                {
                    Select(hit.collider.gameObject);//�� �Լ��� �ؾ��� ��
                    //�ƿ����� ������ ���� �����տ� �ƿ����� ��ũ��Ʈ�� �������� �ʰ� ����
                    //ó�� �����ؼ� �̵����϶��� �ƿ������� ������� �ʴٰ� ��ġ�� ���ĺ��� �ƿ����� �����ϰ��Ϸ���
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

        //�̹� ���õ� ������Ʈ�� �ٽ� �����Ϸ��� �ϸ� �׳� ����������
        if(obj == selectedObject[0])
        {
            return;
        }
        //���� ������Ʈ�� �ƴ� �ٸ� ������Ʈ�� �����ߴٸ� ������ ���õǾ� ����ִ� ������Ʈ�� �����Ұ��̴�(null)
        if(selectedObject != null)
        {
            Deselect();
        }

        Outline outline = obj.GetComponent<Outline>();

        if(outline == null)
        {
            //ó������ ��Ų �������� �ƿ������� �ʿ���ٰ�
            //Ŭ���ؼ� ��ġ�������� ����
            obj.AddComponent<Outline>();
        }
        else//���ĺ��ʹ� �ƿ����� ���۳�Ʈ�� �ֱ� ������ ���ڵ�� ����
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
