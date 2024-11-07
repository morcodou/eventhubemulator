// See https://aka.ms/new-console-template for more information

using EventHubProducer;

Console.WriteLine("Hello, EventProducer! ");

var producer = new EventProducer();
await producer.SendAsync();

Console.WriteLine("Hello, EventProducer! ");
