using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial Dialog", menuName = "Tutorial Dialog/Create Tutorial Dialog")]
public class TutorialDialogs : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] DialogStageOne;
    [TextArea(3, 10)]
    public string[] DialogStageTwo;
    [TextArea(3, 10)]
    public string[] DialogStageThree;
    [TextArea(3, 10)]
    public string[] DialogStageFour;
    [TextArea(3, 10)]
    public string[] DialogStageFive;
    [TextArea(3, 10)]
    public string[] DialogStageSix;
    [TextArea(3, 10)]
    public string[] DialogStageSeven;
}
