using System;
using Prism.Events;

namespace ConsultApp.Helpers.Events
{
    public class PassDayAvailable : PubSubEvent<DayOfWeek>
    {
    }
}
