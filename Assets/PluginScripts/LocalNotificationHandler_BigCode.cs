using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalNotificationHandler_BigCode : MonoBehaviour {


    public static LocalNotificationHandler_BigCode Instance;


    public string[] notificationTexts;
    private  string notificationString;
    private  int notificationId;
    private  List<int> allNotifications = new List<int>();
    private  int currentNotification = 0;

    // Use this for initialization
    void Awake () {

        Instance = this;
	}



    public  void CancelNotifications()
    {
        notificationString = PlayerPrefs.GetString(GameConstants_BigCode.NOTIFICATION_IDS, "");

        string[] notificationIDs = notificationString.Split(new string[] { "#" }, System.StringSplitOptions.None);

        for (int i = 0; i < notificationIDs.Length; i++)
        {
            int.TryParse(notificationIDs[i], out notificationId);
            if (notificationId > 0)
            {
                BigCodeLibHandler_BigCode.Instance.CancelNotification(notificationId);
            }
        }

        PlayerPrefs.SetString(GameConstants_BigCode.NOTIFICATION_IDS, "");
    }

    //public  void CancelNotification(int id)
    //{
    //    BigCodeLibHandler.Instance.CancelNotification(id);
    //}

    void ScheduleNotifications()
    {
        for (int i = 1; i <= notificationTexts.Length; i++)
        {
            notificationId = 1000 + i;
            notificationString = PlayerPrefs.GetString(GameConstants_BigCode.NOTIFICATION_IDS, "") + notificationId + "#";
            PlayerPrefs.SetString(GameConstants_BigCode.NOTIFICATION_IDS, notificationString);
            BigCodeLibHandler_BigCode.Instance.SetLocalNotification(notificationId, DaysToMilliSeconds(i), notificationTexts[Random.Range(0, notificationTexts.Length)]);
        }
    }

    //public  int ScheduleNotification(long timeInSeconds, string notificationText)
    //{
    //    currentNotification++;
    //    notificationString = PlayerPrefs.GetString(GameConstants_BigCode.NOTIFICATION_IDS, "") + currentNotification + "#";
    //    PlayerPrefs.SetString(GameConstants_BigCode.NOTIFICATION_IDS, notificationString);
    //    BigCodeLibHandler_BigCode.Instance.SetLocalNotification(currentNotification, 1000, notificationText);
    //    return currentNotification;
    //}

    public void ScheduleNotification(int day, string notificationText)
    {
        BigCodeLibHandler_BigCode.Instance.SetLocalNotification(currentNotification, DaysToMilliSeconds(day), notificationText);
    }


    void OnApplicationPause(bool isPaused)
    {
        if(BigCodeLibHandler_BigCode.Instance && BigCodeLibHandler_BigCode.Instance.nativeFunctionalities != null)
        {
            if (isPaused)
            {
                ScheduleNotifications();
            }
            else
            {
                if (BigCodeLibHandler_BigCode.Instance.nativeFunctionalities != null)
                    CancelNotifications();
            }
        }


    }


    public  long SecondsToMilliSeconds(long seconds)
    {
        return seconds * 1000;
    }

    public  long MinutesToSeconds(long minutes)
    {
        return minutes * 60;
    }

    public  long HoursToMinutes(long hours)
    {
        return hours * 60;
    }
    public  long DaysToHours(long days)
    {
        return days * 24;
    }

    public  long HoursToMilliSeconds(long hours)
    {
        return SecondsToMilliSeconds(MinutesToSeconds(HoursToMinutes(hours)));
    }

    public  long DaysToMilliSeconds(long days)
    {
        return SecondsToMilliSeconds(MinutesToSeconds(HoursToMinutes(DaysToHours(days))));
    }


}
