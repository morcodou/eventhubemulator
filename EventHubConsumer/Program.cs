// See https://aka.ms/new-console-template for more information
using EventHubConsumer;

var useBlobStorage = false;

if (useBlobStorage)
{
    Console.WriteLine("Hello, EventBlobConsumer!");

    var eventBlobConsumer = new EventBlobConsumer();
    await eventBlobConsumer.ReadEventsAsync();

    Console.WriteLine("Hello, EventBlobConsumer!");
}
else
{
    Console.WriteLine("Hello, EventConsumer!");

    var consumer = new EventConsumer();
    await consumer.ReadEventsAsync();

    Console.WriteLine("Hello, EventConsumer!");
}
