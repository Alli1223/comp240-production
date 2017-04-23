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

        public Dropdown UnitDropdown;


        public void onClickMapOne()
        {

            //Change map to "FireLock
            map = "Firelock";
            //lobbyManager.ServerChangeScene("map");
            //Continue();
        }
        public void onClickMapTwo()
        {
            // Change map to othermap
            map = "map";
            //lobbyManager.ServerChangeScene("Firelock");
            //Continue();
        }

        public void Continue()
        {
            if(map!= null)
            {
                if(map == "Firelock")
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
