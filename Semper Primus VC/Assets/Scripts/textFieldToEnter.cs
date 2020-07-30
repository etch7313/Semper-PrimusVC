//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Photon.Pun.UtilityScripts;

public class textFieldToEnter : MonoBehaviour
{
   [SerializeField] private InputField nameInputFiled = null;
   [SerializeField] private Button continueButton = null;

   private const string PlayerPrefsNameKey = "PlayerName";

   private void start() => SetUpInputField();

   private void SetUpInputField()
   {
      if (!PlayerPrefs.HasKey(PlayerPrefsNameKey))
      {return;}

      string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
      nameInputFiled.text = defaultName;
      
      SetPlayerName(defaultName);
   }

   public void SetPlayerName(string name)
   {
      continueButton.interactable = !string.IsNullOrEmpty(name);
   }

   public void SavePlayerName()
   {
      string playerName = nameInputFiled.text;

     // PhotonNetwork.NickName = playerName;
      PlayerPrefs.SetString(PlayerPrefsNameKey,playerName);
   }
}
