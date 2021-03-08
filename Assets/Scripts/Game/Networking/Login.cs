using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

public class Login : MonoBehaviour
{

     [Header ("REFERENCES")]
     public InputField userName;
     public InputField mail;
     public InputField password;

     public Text displayText;

     public void Start () {
          userName.text = PlayerPrefs.GetString ("UserName");
          password.text = PlayerPrefs.GetString ("Password");
     }

     public void RegisterAccount () {

          var request = new RegisterPlayFabUserRequest {
               Username = userName.text,
               DisplayName = userName.name,
               Email = mail.text,
               Password = password.text,       
          };

          PlayFabClientAPI.RegisterPlayFabUser (request,
               (RegisterPlayFabUserResult result)=> {
                    displayText.text = "Logged as " + userName.text;
                    PlayerPrefs.SetString ("UserName",userName.text);
                    PlayerPrefs.SetString ("Password",password.text);
                    PlayerPrefs.Save ();
               },
               (PlayFabError error) => {
                    Debug.Log (error.GenerateErrorReport ());
               }
          );

          displayText.text = "Trying to register...";
     }

     public void LoginAccount () {

          var request = new LoginWithPlayFabRequest {
               Username = userName.text,
               Password = password.text               
          };

          PlayFabClientAPI.LoginWithPlayFab (request, 
               (LoginResult result) => {
                    displayText.text = "Logged as " + userName.text;
                    PlayerPrefs.SetString ("UserName",userName.text);
                    PlayerPrefs.SetString ("Password",password.text);
                    PlayerPrefs.Save ();
               },
               (PlayFabError error) => {
                    Debug.Log (error.GenerateErrorReport ());
               }
          );

          displayText.text = "Trying to login...";
     }
}