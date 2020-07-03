using GBank.Domain.Core.Bus;
using GBank.Domain.Core.Commands;
using GBank.Domain.Core.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBank.Infra.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {

        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connect = factory.CreateConnection())
            {
                using (var channel = connect.CreateModel())
                {
                    var eventName = @event.GetType().Name;
                    channel.QueueDeclare(eventName, false, false, false, null);

                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", eventName, null, body);
                }
            }
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventname = typeof(T).Name;

            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
            if (!_handlers.ContainsKey(eventname))
            {
                _handlers.Add(eventname, new List<Type>());
            }
            if (_handlers[eventname].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException(
                    $"HandlerType {handlerType.Name} already is registered for {eventname}", nameof(handlerType));
            }

            _handlers[eventname].Add(handlerType);

            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var eventName = typeof(T).Name;

            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };

            var connect = factory.CreateConnection();
            var channel = connect.CreateModel();

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Recived;

            channel.BasicConsume(eventName, true, consumer);

        }

        private async Task Consumer_Recived(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subscriptions = _handlers[eventName];

                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ServiceProvider.GetService(subscription);

                        if (handler == null) continue;

                        var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType);

                        var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                        await (Task)conreteType.GetMethod("Handler").Invoke(handler, new object[] { @event });
                    } 
                }
            }
        }
    }
}
