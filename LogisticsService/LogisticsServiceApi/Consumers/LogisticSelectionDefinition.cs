using MassTransit;

namespace LogisticsServiceApi.Consumers
{
    public class LogisticSelectionDefinition : ConsumerDefinition<LogisticSelectionConsumer>
    {
        public LogisticSelectionDefinition()
        {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "logistic-selection-queue";
        }
    }
}
