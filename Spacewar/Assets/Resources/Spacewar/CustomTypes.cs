using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CustomTypes
{
    [System.Serializable]
    public class Coordniate2D{
        int x;
        int y;
        Coordniate2D(int x, int y){
            this.x = x;
            this.y = y;
        }
    }
    [System.Serializable]
    public class ItemData{
        
        public string ItemName;
        public int ItemType;
        public Sprite ThumbnailSprite;
        public void ClearItemData(){
            ItemName = "";
            ItemType = 0;
            ThumbnailSprite = null;
        }
        public ItemData(string ItemName, int ItemType, Sprite ThumbnailSprite){
            this.ItemName = ItemName;
            this.ItemType = ItemType;
            this.ThumbnailSprite = ThumbnailSprite;
        }
    }

    // 전기기계의 작동상태를 지정하는 enum 입니다.
    public enum ElectricState{
        OFF,
        IDLE,
        ACTIVE
    }

    public class MathExt{
        public static bool Approximately(float a, float b){
        return Mathf.Abs(b - a) < Mathf.Max(0.001f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), 0.01f);
        }
    }
    
}
