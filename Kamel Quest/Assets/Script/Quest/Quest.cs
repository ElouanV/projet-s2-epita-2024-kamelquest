using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    NONE,
    ACCEPTED,
    DECLINED,
    STARTED,
    COMPLETED,
    ENDED
}

public enum QuestType
{
    Meeting, // Go to meet someone
    Killing, // Go to kill an ennemy
    Finding, // find and Add an object to his inventory
    Bringing // Bring an object to the PNG
}

public class Quest : MonoBehaviour
{
    public QuestType type;
    public QuestState State;
    public GameObject Target;

    public string title;
    public string desc;
    public int exp;
    public Items reward;

    public bool completed;

    // This willupdate the state of the quest if it's needed
    public void UpdateState()
    {
        if (State == QuestState.NONE)
        {
            State = QuestState.ACCEPTED;
            transform.GetComponent<ShowsText>().OnEnable();
        }

        else if (State == QuestState.ACCEPTED)
        {
            if (Input.GetKeyUp("y"))
            {
                // To add: Add quest to progression
                State = QuestState.STARTED;
                transform.GetComponent<ShowsText>().OnEnable();
                StartQuest(type);
            }
            else if (Input.GetKeyUp("n"))
            {
                State = QuestState.DECLINED;
                transform.GetComponent<ShowsText>().OnEnable();
            }
        }

        else if (State == QuestState.STARTED)
        {
            if (completed)
            {
                completed = false;
                State = QuestState.COMPLETED;
                if (transform.parent.parent.GetComponent<ShowsBubbleText>().is_trigger) transform.GetComponent<ShowsText>().OnEnable();
            }
            else gameObject.SetActive(false);
        }

        else if (State == QuestState.DECLINED)
        {
            gameObject.SetActive(false);
            State = QuestState.NONE;
        }

        else if (State == QuestState.COMPLETED)
        {
            // To add: Give the reward

            State = QuestState.ENDED;
            gameObject.SetActive(false);
        }

        else if (State == QuestState.ENDED) gameObject.SetActive(false);
    }

    // This will active the target of the quest depending of the type
    public void StartQuest(QuestType type)
    {
        switch (type)
        {
            case QuestType.Meeting:
                Target.GetComponent<MeetingPNG>().is_active = true;
                break;
            case QuestType.Finding:
                if (completed) UpdateState();
                break;
            //case QuestType.Finding:
        }
    }

    // This function is call if the quest is completed
    public void CompletedQuest()
    {
        completed = true;
        if (State == QuestState.STARTED) UpdateState();
    }
}
 