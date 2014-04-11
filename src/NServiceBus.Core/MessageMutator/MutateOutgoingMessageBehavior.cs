﻿namespace NServiceBus.MessageMutator
{
    using System;
    using System.ComponentModel;
    using Pipeline;
    using Pipeline.Contexts;


    [Obsolete("This is a prototype API. May change in minor version releases.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class MutateOutgoingMessageBehavior : IBehavior<SendLogicalMessageContext>
    {
        public void Invoke(SendLogicalMessageContext context, Action next)
        {
            var currentMessageToSend = context.MessageToSend.Instance;

            foreach (var mutator in context.Builder.BuildAll<IMutateOutgoingMessages>())
            {
                currentMessageToSend = mutator.MutateOutgoing(currentMessageToSend);
                context.MessageToSend.UpdateMessageInstance(currentMessageToSend);
            }

            next();
        }
    }
}