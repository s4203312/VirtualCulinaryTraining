using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;

public class MouseClickEvent : Unity.Services.Analytics.Event
{
    //Creating the event and linking it to game services
    public MouseClickEvent() : base(name:"TestEvent")
    {
    }

    //How to add paramater value to event
    public int Count { set { SetParameter(name: "Count", value); } }

}
