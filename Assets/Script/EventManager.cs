using System;
using System.Collections.Generic;

public class EventManager
{
    public static Action<Information> UserClickEvent;
    public static Action<List<Operations>> OperationClickEvent;
    public static Action<List<Levels>> LevelClickEvent;
    public static Action<Levels> TopicClickEvent;
    public static Action<bool> ActivateScreen1Event;
    public static Action<bool> ActivateScreen2Event;
    public static Action<string, int> ChangeTitleEvent;

    public static void OnUserClick(Information info)
    {
        if (UserClickEvent != null)
        {
            UserClickEvent(info);
        }
    }

    public static void OnOperationClick(List<Operations> operation)
    {
        if (OperationClickEvent != null)
        {
            OperationClickEvent(operation);
        }
    }

    public static void OnLevelClick(List<Levels> level)
    {
        if (LevelClickEvent != null)
        {
            LevelClickEvent(level);
        }
    }

    public static void OnTopicClick(Levels level)
    {
        if (TopicClickEvent != null)
        {
            TopicClickEvent(level);
        }
    }

    public static void ActivateScreen1(bool flag)
    {
        if (ActivateScreen1Event != null)
        {
            ActivateScreen1Event(flag);
        }
    }

    public static void ActivateScreen2(bool flag)
    {
        if (ActivateScreen2Event != null)
        {
            ActivateScreen2Event(flag);
        }
    }

    public static void OnChangeTitle(string name, int size)
    {
        if (ChangeTitleEvent != null)
        {
            ChangeTitleEvent(name, size);
        }
    }
}