using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : INode
{
    List<INode> children;

    public SelectorNode() { children = new List<INode>(); }

    public void Add(INode node) { children.Add(node); }

    public INode.STATE Evaluate()
    {
        foreach (INode child in children)
        {
            INode.STATE state = child.Evaluate();
            switch (state) // chlid의 state가 하나라도 success일 경우 성공을 반환
            {
                case INode.STATE.SUCCESS:
                    return INode.STATE.SUCCESS;
                case INode.STATE.RUNNING:
                    return INode.STATE.RUNNING;
            }
        }

        return INode.STATE.FAILURE;
    }
}