using UnityEngine;
using UnityEngine.EventSystems;

public class UIDebug : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Click");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, 100f * ray.direction, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name + " on Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
        }
        else
        {
            Debug.Log("No UI Element Clicked.");
        }
    }

    private void Update()
    {
        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(ray.origin, 100f * ray.direction, Color.blue);


        //if (Physics.Raycast(ray, out hit))
        //{
        //    Debug.Log("Hit Object: " + hit.collider.gameObject.name + " on Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
        //}
    }

    //private void OnMouseDown()
    //{
    //    Debug.Log("Mouse Click");

    //    RaycastHit hit;
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Debug.DrawRay(ray.origin, 100f * ray.direction, Color.red);

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        Debug.Log("Hit Object: " + hit.collider.gameObject.name + " on Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
    //    }
    //    else
    //    {
    //        Debug.Log("No UI Element Clicked.");
    //    }
    //}
}
