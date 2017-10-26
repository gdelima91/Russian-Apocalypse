using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class GameUIPr : Singleton<GameUIPr> {

	protected GameUIPr(){}

    public System.Action<Image>        Adapter_Avatar_CA;    //Character A Avatar Function Delegate
    public System.Action<float, float> Adapter_Healthbar_CA; //Character A Health Bar Func Delegate
    public System.Action<float, float> Adapter_Manabar_CA;   //Character A Mana Bar Func Delegate
    //......CB......
    //......CC......   A player may control multiple Characters. Each character have it's own UI Health bar and Mana bar....
    //......CD......

    
    public System.Action<Image>        Adapter_Avatar_PopUp;
    public System.Action<float, float> Adapter_Healthbar_PopUp;
    public System.Action<float, float> Adapter_Manabar_PopUp;

    [HideInInspector]
    public GameObject mainManueObj;    //This is the main Manue Object----Press Icon Botton or press Esc key
    GameObject settingPanelObj; //Panel contains the Music sound ..... Button
    GameObject LoadSaveObj;     //Load the save data Panel
    GameObject SaveGameObj;
    GameObject loadingPanelObj;
    GameObject LevelTransitionPanelObj;
    GameObject inventoryPanelObj;
    GameObject itemInfoObj;

    List<GameObject> subMainPanelObjs = new List<GameObject>();
    
    private void Start()
    {
        // Debug.Log("GameCentalPr Start");
        
        //mainManue Panel
        if (mainManueObj == null)
        {
            mainManueObj = GetComponentInChildren<MainManue>().gameObject;
        }

        //Setting Panel
        if (settingPanelObj == null)
        {
            settingPanelObj = GetComponentInChildren<SettingPanel>().gameObject;
            if (settingPanelObj != null)
            {
                subMainPanelObjs.Add(settingPanelObj);
                settingPanelObj.SetActive(false);
            }
        }    

        //LoadSave Panel
        if (LoadSaveObj == null)
        {
            LoadSaveObj = GetComponentInChildren<LoadSavePanel>().gameObject;
            if (LoadSaveObj != null)
            {
                subMainPanelObjs.Add(LoadSaveObj);
                LoadSaveObj.SetActive(false);
            }   
        }
       
        //SaveGamePanel;
        if (SaveGameObj == null)
        {
            SaveGameObj = GetComponentInChildren<SaveGamePanel>().gameObject;
            if (SaveGameObj != null)
            {
                subMainPanelObjs.Add(SaveGameObj);
                SaveGameObj.SetActive(false);
            }
            
        }
        
        //LevelPanel
        if (LevelTransitionPanelObj == null)
        {
            LevelTransitionPanelObj = GetComponentInChildren<LevelTransitionPanel>().gameObject;
            if (LevelTransitionPanelObj != null)
            {    
                subMainPanelObjs.Add(LevelTransitionPanelObj);
                LevelTransitionPanelObj.SetActive(false);
            }
        }

        //Inventory Panel
        if (inventoryPanelObj == null)
        {
            inventoryPanelObj = GetComponentInChildren<InventoryPanel>().gameObject;
            if (inventoryPanelObj != null)
            {
                inventoryPanelObj.SetActive(false);
            }
        }


        if (itemInfoObj == null)
        {
            itemInfoObj = GetComponentInChildren<ItemInfo>().gameObject;
        }

        loadingPanelObj = GetComponentInChildren<StartbackGround>().gameObject;
    }

    public void UpdateHealthBar(float current,float max)
    {
        if (Adapter_Healthbar_CA != null) { Adapter_Healthbar_CA(current,max); }
    }

    public void UpdateManaBar(float current, float max)
    {
        if (Adapter_Manabar_CA != null) { Adapter_Manabar_CA(current, max); }
    }

    public void MailBox_SYS_UI_Event(SYS_UI_Event sysUIEvent)
	{
		
	}

    public void SwitchActivePanel(PanelType target)
    {
        //Enable Target
        if (target == PanelType.MainMenue)
        {
            if(mainManueObj!= null)
                mainManueObj.SetActive(true);
        }
        else if (target == PanelType.Setting)
        {
            if (settingPanelObj != null)
                settingPanelObj.SetActive(true);
        }
        else if (target == PanelType.LoadSave)
        {
            if (LoadSaveObj != null)
                LoadSaveObj.SetActive(true);
        }
        else if (target == PanelType.SaveGame)
        {
            if (SaveGameObj != null)
                SaveGameObj.SetActive(true);
        }

    }

    public void OpenLevelPanel()
    {
        if (LevelTransitionPanelObj != null)
        {
            LevelTransitionPanelObj.SetActive(true);
        }
    }

    public void LoadLPlayerDataFromLastIndex()
    {
        if (loadingPanelObj != null)
            loadingPanelObj.SetActive(false);     
    }

    public void MainUIMenueEvent()
    {
        if (mainManueObj != null)
        {
            //----------------------------------------------------
            //Make sure all of the sub-Mainpanel are not active
            //----------------------------------------------------
            bool subActive = false;
            foreach (GameObject obj in subMainPanelObjs)
            {
                if (obj.activeSelf) {
                    subActive = true;
                    break;
                }
            }
            if (subActive) return;
            //----------------------------------------------------
            mainManueObj.SetActive(!mainManueObj.activeSelf);
            if (mainManueObj.activeSelf)
            {
                mainManueObj.transform.SetAsLastSibling();
            }
            GameCentalPr.Instance.PauseResumeEvent(mainManueObj.activeSelf);
        }
    }

    public void InventoryPanelEvent()
    {
        if (inventoryPanelObj != null)
        {
            inventoryPanelObj.SetActive(!inventoryPanelObj.activeSelf);
            if (inventoryPanelObj.activeSelf)
            {
                inventoryPanelObj.transform.localPosition = Vector3.zero;
            }
            else {
                itemInfoObj.GetComponent<ItemInfo>().DeactiveItemInfo();
            }
        }
    }

    public void AddItemToInventory(Item item)
    {
        if (inventoryPanelObj != null)
        {
            if (!inventoryPanelObj.activeSelf)
            {
                inventoryPanelObj.SetActive(true);
                inventoryPanelObj.transform.localPosition = new Vector3(999, 999, 0);
                inventoryPanelObj.GetComponent<InventoryPanel>().AddItem(item);
                inventoryPanelObj.SetActive(false);
            }
            else
            {
                inventoryPanelObj.GetComponent<InventoryPanel>().AddItem(item);
            }
        }
    }

    public void ResumeFromeSave()
    {
        SaveGameObj.SetActive(false);
        GameCentalPr.Instance.PauseResumeEvent(false);
    }

    public void StartNewGame()
    {
        mainManueObj.SetActive(false);
        Time.timeScale = 1;
        GameCentalPr.Instance.StartNewGame();
    }

    public void OpenSavePanel()
    {
        if (SaveGameObj != null)
        {
            SaveGameObj.SetActive(!SaveGameObj.activeSelf);
            if (SaveGameObj.activeSelf)
            {
                SaveGameObj.transform.SetAsLastSibling();
            }
            GameCentalPr.Instance.PauseResumeEvent(SaveGameObj.activeSelf);
        }
    }

    public void LoadSave(int index)
    {
        Time.timeScale = 1;
        

        loadingPanelObj.SetActive(true);
        LoadSaveObj.SetActive(false);

        GameCentalPr.Instance.LoadSave_by_Index(index);

    }

    public void SaveGame(int index)
    {
        ResumeFromeSave();
        GameCentalPr.Instance.SaveGame(index);
    }

    public void CloseLoadingPanel()
    {
        if (loadingPanelObj != null)
            loadingPanelObj.SetActive(false);
    }

    public void NextLevel()
    {
        LevelTransitionPanelObj.SetActive(false);
        loadingPanelObj.SetActive(true);
        GameCentalPr.Instance.NextLevel();
    }

    public enum PanelType
    {
        MainMenue,
        Setting,
        LoadSave,
        SaveGame
    }
}


