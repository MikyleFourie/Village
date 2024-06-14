using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    [SerializeField] private GameObject panel1; //Main button panel
    [SerializeField] private GameObject panel2; //Main Features panel
    [SerializeField] private GameObject panel3; //Credits

    public void ShowPanel1()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }
    public void ShowPanel2()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
    }
    public void ShowPanel3()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
    }

}
