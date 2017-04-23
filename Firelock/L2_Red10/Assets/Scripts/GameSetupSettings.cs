using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// AUTHOR: ALASTAIR RAYNER
/// </summary>
namespace Prototype.NetworkLobby
{
    public class GameSetupSettings : MonoBehaviour
    {
        public LobbyManager lobbyManager;

        //public RectTransform lobbyServerList;
        //public RectTransform lobbyPanel;

        private string map;
        int selectedUnit;

        public Dropdown UnitDropdown;


        public void onClickMapOne()
        {

            //Change map to "FireLock
            map = "Firelock";
        }
        public void onClickMapTwo()
        {
            // Change map to othermap
            map = "map";
        }

        public void Continue()
        {
            //Used for getting the players selected unit from dropdown
            selectedUnit = UnitDropdown.value;

            if (map != null)
            {
                if (map == "Firelock")
                    lobbyManager.ServerChangeScene(map);

                else if (map == "map")
                    lobbyManager.ServerChangeScene(map);
            }
            //close pannel
            lobbyManager.gameSettingsPannel.gameObject.SetActive(false);
        }

        
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
