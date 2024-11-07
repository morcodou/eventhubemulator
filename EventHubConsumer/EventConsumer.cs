using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using EventHubShared;

namespace EventHubConsumer
{
    internal class EventConsumer
    {
        public async Task ReadEventsAsync()
        {
            // Read from the default consumer group: $Default
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            await using (var consumer = new EventHubConsumerClient(consumerGroup, Constants.ConnectionString, Constants.EventHubName))
            {
                using var cancellationSource = new CancellationTokenSource();
                cancellationSource.CancelAfter(TimeSpan.FromSeconds(45));

                await foreach (PartitionEvent receivedEvent in consumer.ReadEventsAsync(cancellationSource.Token))
                {
                    var data = Encoding.UTF8.GetString(receivedEvent.Data.EventBody.ToArray());
                    Console.WriteLine($"events [{data}] has been received.");

                    // At this point, the loop will wait for events to be available in the Event Hub.  When an event
                    // is available, the loop will iterate with the event that was received.  Because we did not
                    // specify a maximum wait time, the loop will wait forever unless cancellation is requested using
                    // the cancellation token.
                }
            }
        }
    }
}


