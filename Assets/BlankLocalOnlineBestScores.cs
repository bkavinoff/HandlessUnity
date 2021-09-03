using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankLocalOnlineBestScores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Blanqueo el record online de los niveles (localmente)
        BlankOnlineRecordsOnLocal();
        //BlankLocalRecord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlankOnlineRecordsOnLocal()
    {
        PlayerPrefs.DeleteKey("MaxOnlineRecordAlias_lvl_1");
        PlayerPrefs.DeleteKey("MaxOnlineRecordTime_lvl_1");
        PlayerPrefs.DeleteKey("MaxOnlineRecordAlias_lvl_2");
        PlayerPrefs.DeleteKey("MaxOnlineRecordTime_lvl_2");
        PlayerPrefs.DeleteKey("MaxOnlineRecordAlias_lvl_3");
        PlayerPrefs.DeleteKey("MaxOnlineRecordTime_lvl_3");
        Debug.Log("Menu1 - BlankOnlineRecordsOnLocal - Reseteados los Records Online guardados en local");
    }

    public void BlankLocalRecord()
    {
        PlayerPrefs.DeleteKey("LocalRecordTime_lvl_1");
        PlayerPrefs.DeleteKey("LocalRecordTime_lvl_2");
        PlayerPrefs.DeleteKey("LocalRecordTime_lvl_3");
        Debug.Log("API - BlankLocalRecord - Reseteados los records locales del usuario");
    }
}
