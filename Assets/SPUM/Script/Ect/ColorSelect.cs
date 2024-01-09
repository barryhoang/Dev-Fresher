using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelect : MonoBehaviour
{
    #if UNITY_EDITOR
    public int index;
    public Image _savedColor;

    public void SetColorSave()
    {
        SoonsoonData.Instance.SpumManager._nowSelectColorNum = index;
        if(_savedColor.gameObject.activeInHierarchy)
        {
            SoonsoonData.Instance.SpumManager._nowColor = _savedColor.color;
            SoonsoonData.Instance.SpumManager._nowColorShow.color = _savedColor.color;
            SoonsoonData.Instance.SpumManager._hexColorText.text = SoonsoonData.Instance.SpumManager.ColorToStr(_savedColor.color);
            SoonsoonData.Instance.SpumManager.SetObjColor();

            SoonsoonData.Instance.SpumManager.ToastOn("Loaded Color");
            SoonsoonData.Instance.SpumManager._nowSelectColor.SetActive(true);
            SoonsoonData.Instance.SpumManager._nowSelectColor.transform.position = transform.position;
            //set the color
        }
        else
        {
            //saved the color
            _savedColor.gameObject.SetActive(true);
            _savedColor.color = SoonsoonData.Instance.SpumManager._nowColor;
            SoonsoonData.Instance.SpumManager.ToastOn("Saved Color");

            string tSTR = SoonsoonData.Instance.SpumManager.ColorToStr(_savedColor.color);
            SoonsoonData.Instance._soonData2._savedColorList[index] = tSTR;
            SoonsoonData.Instance.SaveData();
        }
    }
    #endif
}
