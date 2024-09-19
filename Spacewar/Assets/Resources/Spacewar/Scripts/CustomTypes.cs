using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomTypes
{
    public class Coordniate2D{
        int x;
        int y;
        Coordniate2D(int x, int y){
            this.x = x;
            this.y = y;
        }
    }

    // 전기기계의 작동상태를 지정하는 enum 입니다.
    public enum ElectricState{
        OFF,
        IDLE,
        ACTIVE
    }
}
