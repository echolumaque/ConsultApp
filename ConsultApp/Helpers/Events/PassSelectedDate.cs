using Prism.Events;
using System;

namespace ConsultApp.Helpers.Events
{
    public class PassSelectedDate : PubSubEvent<DateTime>
    {
    }
}
