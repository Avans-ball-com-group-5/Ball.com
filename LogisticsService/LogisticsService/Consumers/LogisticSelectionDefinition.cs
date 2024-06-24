using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsService.Consumers
{
    public class LogisticSelectionDefinition : ConsumerDefinition<LogisticSelectionConsumer>
    {
        public LogisticSelectionDefinition() {
            // The endpoint name is the name of the service that will be accepting the message
            EndpointName = "logistic-selection-queue";
        }
    }
}
