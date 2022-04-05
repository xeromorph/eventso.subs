using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eventso.Subscription.Observing.DeadLetter
{
    public interface IPoisonEventInbox<TEvent>
        where TEvent : IEvent
    {
        Task Add(IReadOnlyCollection<PoisonEvent<TEvent>> events, CancellationToken cancellationToken);

        Task<bool> IsStreamPoisoned(TEvent @event, CancellationToken cancellationToken);
        Task<IReadOnlySet<TEvent>> GetPoisonStreamsEvents(IReadOnlyCollection<TEvent> events, CancellationToken cancellationToken);
    }
}