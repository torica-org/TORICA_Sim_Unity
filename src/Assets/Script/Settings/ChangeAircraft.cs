using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeAircraft : MonoBehaviour
{
    private bool s=false;
    private bool sameLoad;

    // Start is called before the first frame update
    public void OnEnables()
    {
        Dropdown AircraftDdtmp; /*  */
        List<string> AircraftList = new List<string>();

        //Optionsに表示する文字列をリストに追加
        AircraftList.Add("Tatsumi");
        AircraftList.Add("Ray");
        AircraftList.Add("Mio");
        AircraftList.Add("QX-18");
        AircraftList.Add("QX-19");
        AircraftList.Add("QX-20");
        AircraftList.Add("ARG-2");
        AircraftList.Add("ORCA18");
        AircraftList.Add("UL01B");
        AircraftList.Add("ORCA18");
        AircraftList.Add("ORCA22");
        AircraftList.Add("Gardenia");
        AircraftList.Add("Aria");
        AircraftList.Add("Camellia");

        //DropdownコンポーネントのOptionsという項目にOptionsのリストがありました
        //それを編集するためにDropdownコンポーネントを取得
        AircraftDdtmp = GetComponent<Dropdown>();

        //一度すべてのOptionsをクリア
        AircraftDdtmp.ClearOptions();

        //リストを追加
        AircraftDdtmp.AddOptions(AircraftList);
        s=false;

        //if(GameManager.instance.PlaneName == null){
        //    GameManager.instance.PlaneName=DefaultPlane;
        //}
        if(GameManager.instance.PlaneName == GameManager.instance.DefaultPlane){
            sameLoad=true;
        }

        AircraftDdtmp.value = AircraftList.IndexOf(GameManager.instance.PlaneName);
    }
    
    public void OnSelected()
    {
        if(s || GameManager.instance.FirstLoad || sameLoad){
            Dropdown AircraftDdtmp;

            //DropdownコンポーネントをGet
            AircraftDdtmp = GetComponent<Dropdown>();

            //Dropdownコンポーネントから選択されている文字を取得
            string selectedvalue = AircraftDdtmp.options[AircraftDdtmp.value].text;

            GameManager.instance.PlaneName = selectedvalue;

            Time.timeScale=1f;
            SceneManager.LoadScene("FlightScene");
        }else{
            s=true;
        }

    }
}
