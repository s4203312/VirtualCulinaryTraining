using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public static class AnalyticActions
{
    //Settting up the action that will be performed
    public static void MouseClicked(int Count)
    {
        var MouseClicked = new MouseClickEvent 
        { 
            Count = Count 
        };
        
        AnalyticsService.Instance.RecordEvent(MouseClicked);    //Recording the information
    }
}
