using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{ 
    public enum STATE
    { RUNNING, SUCCESS, FAILURE } // 실행 중, 성공, 실패

    public INode.STATE Evaluate(); // 판단하여 상태 리턴
}
