﻿// Copyright 2007-2018 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit
{
    using System;
    using Azure.ServiceBus.Core;
    using Azure.ServiceBus.Core.Configuration;
    using Azure.ServiceBus.Core.Settings;
    using Azure.ServiceBus.Core.Topology.Configuration.Configurators;
    using Microsoft.Azure.WebJobs;
    using WebJobs.EventHubsIntegration;
    using WebJobs.EventHubsIntegration.Configuration;


    public static class AzureFunctionEventHubsBusExtensions
    {
        public static IEventDataReceiver CreateEventDataReceiver(this IBusFactorySelector selector, IBinder binder,
            Action<IWebJobReceiverConfigurator> configure)
        {
            if (binder == null)
                throw new ArgumentNullException(nameof(binder));

            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            var topologyConfiguration = new ServiceBusTopologyConfiguration(AzureBusFactory.MessageTopology);
            var busConfiguration = new ServiceBusBusConfiguration(topologyConfiguration);

            var queueConfigurator = new QueueConfigurator("no-host-configured")
            {
                AutoDeleteOnIdle = Defaults.TemporaryAutoDeleteOnIdle,
            };

            var settings = new ReceiveEndpointSettings("no-host-configured", queueConfigurator);

            var busEndpointConfiguration = busConfiguration.CreateReceiveEndpointConfiguration(settings, busConfiguration);

            var configurator = new WebJobEventDataReceiverSpecification(binder, busEndpointConfiguration);

            configure(configurator);

            return configurator.Build();
        }
    }
}