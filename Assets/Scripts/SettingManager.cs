using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initializedata()    {
        ES3.DeleteKey("ListOXdata");
        ES3.DeleteKey("ListSAdata");
        ES3.DeleteKey("ListSOdata");
        ES3.DeleteKey("ListSMdata");
        PlayerPrefs.DeleteAll();

    }
}
