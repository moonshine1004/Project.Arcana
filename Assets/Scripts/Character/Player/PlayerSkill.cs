using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerView _controller;

    private void Start()
    {
        
    }

    private void Update()
    {
        //int nowSkillIndex = _controller.index;
    }

    private void SkillIndexToSkill(int skillIndex)
    {
        switch (skillIndex)
        {
            case 0:
                skill1();
                break;
            case 2:
                skill2();
                break;

        }
    }

    private void skill1()
    {

    }
    private void skill2()
    {

    }
}
