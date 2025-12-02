using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public string[] mainQuestList = {"Chapter 1: Forgotten???", "Chapter 2: A Helping Fin!", "Chapter 3: The Soaring Whale!" };
    public enum mainQuestStates {Chapter_1, Chapter_2, Chapter_3}
    public mainQuestStates questState;

    public bool questCompleted_01;
    public int gems_Collected_Count = 0;

    public bool questCompleted_02;
    public int number_Of_NPC_Saved = 0;

    public bool questCompleted_03;
    public int memories_Collected = 0;

    public string[] sideQuestList = { "Side Quest 01: Fish Master", "Side Quest 02: Trouble in the water" };
    public enum sideQuestStates { Chapter_1, Chapter_2, Chapter_3 }
    public sideQuestStates sideQuestState;
}
