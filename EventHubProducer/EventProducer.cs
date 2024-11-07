using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHubShared;
using System.Text;

namespace EventHubProducer
{
    internal class EventProducer
    {
        public async Task SendAsync()
        {
            // Create a producer client that you can use to send events to an event hub
            await using var producerClient = new EventHubProducerClient(Constants.ConnectionString, Constants.EventHubName);

            int index = 1;
            while (true)
            {
                // Create a batch of events
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                int count = index + 9;

                // Add events to the batch. An event is a represented by a collection of bytes and metadata.
                Enumerable
                    .Range(index, count)
                    .ToList()
                    .ForEach(x => eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event number {x}"))));

                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                
                Console.WriteLine("A batch of 10 events has been published.");
                index = count;

                Task.Delay(1000).Wait();
            }
        }
    }
}
