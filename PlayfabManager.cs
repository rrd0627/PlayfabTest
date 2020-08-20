using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using GooglePlayGames;
public class PlayfabManager : MonoBehaviour
{
    public Text LogText;

    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        GoogleLogin();
    }

    public void GoogleLogin()
    {
        Social.localUser.Authenticate((Success) =>
        {
            if (Success) { LogText.text = "구글 로그인 성공"; PlayFabLogin(); }
            else LogText.text = "구글 로그인 실패";
        }
        );
    }

    public void GoogleLogout()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        LogText.text = "구글 로그아웃";
    }
    public void PlayFabLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = Social.localUser.id + "@gogi.com", Password = Social.localUser.id };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => LogText.text = "로그인 성공\n" + Social.localUser.userName, (error) => PlayFabRegister());

    }

    public void PlayFabRegister()
    {
        var request = new RegisterPlayFabUserRequest { Email = Social.localUser.id + "@gogi.com", Password = Social.localUser.id, Username = Social.localUser.userName };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result)=> { LogText.text = "회원가입 성공";PlayFabLogin(); }, (error) => LogText.text  = "회원가입 실패");
    }




    private void OnLoginSuccess(LoginResult result) => print("로그인 성공");

    private void OnLoginFailure(PlayFabError error) => print("로그인 실패");


    private void OnRegisterSuccess(RegisterPlayFabUserResult result) => print("로그인 성공");

    private void OnRegisterFailure(PlayFabError error) => print("로그인 실패");

}
