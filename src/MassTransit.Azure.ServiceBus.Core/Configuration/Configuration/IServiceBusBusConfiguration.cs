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
namespace MassTransit.Azure.ServiceBus.Core.Configuration
{
    using MassTransit.Configuration;
    using Settings;


    public interface IServiceBusBusConfiguration :
        IBusConfiguration,
        IServiceBusEndpointConfiguration
    {
        new IReadOnlyHostCollection<IServiceBusHostConfiguration> Hosts { get; }

        new IServiceBusTopologyConfiguration Topology { get; }

        /// <summary>
        /// If true, only the broker topology will be deployed
        /// </summary>
        bool DeployTopologyOnly { get; set; }

        /// <summary>
        /// Create a host configuration, by adding a host to the bus
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        IServiceBusHostConfiguration CreateHostConfiguration(ServiceBusHostSettings settings);

        /// <summary>
        /// Create a receive endpoint configuration for the default host
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        IServiceBusReceiveEndpointConfiguration CreateReceiveEndpointConfiguration(string queueName);

        /// <summary>
        /// Create a receive endpoint configuration for the default host
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="endpointConfiguration"></param>
        /// <returns></returns>
        IServiceBusReceiveEndpointConfiguration CreateReceiveEndpointConfiguration(ReceiveEndpointSettings settings,
            IServiceBusEndpointConfiguration endpointConfiguration);

        /// <summary>
        /// Create a subscription endpoint configuration for the default host
        /// </summary>
        /// <param name="subscriptionName"></param>
        /// <param name="topicPath"></param>
        /// <returns></returns>
        IServiceBusSubscriptionEndpointConfiguration CreateSubscriptionEndpointConfiguration(string topicPath, string subscriptionName);
    }
}