namespace Eventso.Subscription.Hosting;

public sealed class TopicSubscriptionConfiguration
{
    private TopicSubscriptionConfiguration(
        string topic,
        IMessageDeserializer serializer,
        HandlerConfiguration? handlerConfig = default,
        bool skipUnknownMessages = true,
        bool enableDeadLetterQueue = false)
    {
        if (string.IsNullOrWhiteSpace(topic))
            throw new ArgumentException("Topic name is not specified.");

        Topic = topic;
        Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        SkipUnknownMessages = skipUnknownMessages;
        HandlerConfig = handlerConfig ?? new HandlerConfiguration();
        EnableDeadLetterQueue = enableDeadLetterQueue;
    }

    public TopicSubscriptionConfiguration(
        string topic,
        IMessageDeserializer serializer,
        HandlerConfiguration? handlerConfig = default,
        DeferredAckConfiguration? deferredAckConfiguration = default,
        bool skipUnknownMessages = true,
        bool enableDeadLetterQueue = false,
        int bufferSize = 0)
        : this(
            topic,
            serializer,
            handlerConfig,
            skipUnknownMessages,
            enableDeadLetterQueue)
    {
        if (bufferSize < 0) throw new ArgumentOutOfRangeException(nameof(bufferSize));

        BufferSize = bufferSize;
        DeferredAckConfiguration = deferredAckConfiguration ?? DeferredAckConfiguration.Disabled;
    }

    public TopicSubscriptionConfiguration(
        string topic,
        BatchConfiguration batchConfiguration,
        IMessageDeserializer serializer,
        HandlerConfiguration? handlerConfig = default,
        bool skipUnknownMessages = true,
        bool enableDeadLetterQueue = false)
        : this(
            topic,
            serializer,
            handlerConfig,
            skipUnknownMessages,
            enableDeadLetterQueue)
    {
        batchConfiguration.Validate();

        BatchConfiguration = batchConfiguration;
        BatchProcessingRequired = true;
    }

    public bool BatchProcessingRequired { get; }

    public bool SkipUnknownMessages { get; }

    public string Topic { get; set; }

    public TimeSpan? ObservingDelay { get; init; }

    public IMessageDeserializer Serializer { get; }

    public HandlerConfiguration HandlerConfig { get; }

    public BatchConfiguration? BatchConfiguration { get; }

    public DeferredAckConfiguration? DeferredAckConfiguration { get; }

    public bool EnableDeadLetterQueue { get; }

    public int BufferSize { get; }
}