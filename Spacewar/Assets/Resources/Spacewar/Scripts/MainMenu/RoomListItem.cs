using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    public string _roomName;
    public TextMeshProUGUI _roomInfo;

    //클릭되었을때 호출되는 함수
    public Action<string> onDelegate;

    public void SetInfo(string roomName, int currPlayer, int maxPlayer){
        _roomName = roomName;
        _roomInfo.text = roomName + '(' + currPlayer + '/' + maxPlayer +')';
    }
    public void OnClick() {
        NetworkManager.Instance().JoinRoom(_roomName);
        ////InputRoomName 찾아오기
        //GameObject go =GameObject.Find("InputRoomName");
        ////찾아온 게임오브젝트에서 InputField 컴포넌트 가져오기
        //InputField inputField = go.GetComponent<InputField>();
        ////가져온 컴포넌트에서 text 값을 나의 이름으로 셋팅하기
        //inputField.text = name;
    }
}