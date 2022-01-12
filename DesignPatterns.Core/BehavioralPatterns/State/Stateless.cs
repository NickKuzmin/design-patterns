using System;
using System.Collections.Generic;
using System.Text;
/*using Stateless;*/

namespace DesignPatterns.Core.BehavioralPatterns.State
{
    class StatelessProgram
    {
        enum Trigger
        {
            On, Off
        }

        public static void Main(string[] args)
        {
            // false = off, true = on

            /*var light = new StateMachine<bool, Trigger>(false);

            light.Configure(false) // if the light is off...
                .Permit(Trigger.On, true)  // we can turn it on
                .OnEntry(transition =>
                {
                    if (transition.IsReentry)
                        WriteLine("Light is already off!");
                    else
                        WriteLine("Switching light off");
                })
                .PermitReentry(Trigger.Off);
            // .Ignore(Trigger.Off) // but if it's already off we do nothing

            // same for when the light is on
            light.Configure(true)
                .Permit(Trigger.Off, false)
                .OnEntry(() => WriteLine("Turning light on"))
                .Ignore(Trigger.On);

            light.Fire(Trigger.On);  // Turning light on
            light.Fire(Trigger.Off); // Turning light off
            light.Fire(Trigger.Off); // Light is already off!
            */
        }
    }
}
