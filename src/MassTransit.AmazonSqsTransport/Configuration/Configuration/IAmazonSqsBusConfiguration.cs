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
namespace MassTransit.AmazonSqsTransport.Configuration.Configuration
{
    using System;
    using MassTransit.Configuration;
    using Topology.Settings;


    public interface IAmazonSqsBusConfiguration :
        IBusConfiguration
    {
        new IAmazonSqsTopologyConfiguration Topology { get; }

        /// <summary>
        /// If true, only the broker topology will be deployed
        /// </summary>
        bool DeployTopologyOnly { get; set; }

        /// <summary>
        /// Return the host that supports the specified address
        /// </summary>
        /// <param name="address">The address to match</param>
        /// <param name="hostConfiguration">The matching host, if present</param>
        /// <returns>True if the host was found, otherwise false</returns>
        bool TryGetHost(Uri address, out IAmazonSqsHostConfiguration hostConfiguration);

        /// <summary>
        /// Return the host if it is part of the configuration
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        /// <param name="hostConfiguration"></param>
        bool TryGetHost(IAmazonSqsHost host, out IAmazonSqsHostConfiguration hostConfiguration);

        /// <summary>
        /// Create an endpoint configuration on the bus, which can later be turned into a receive endpoint
        /// </summary>
        /// <returns></returns>
        IAmazonSqsEndpointConfiguration CreateEndpointConfiguration();

        /// <summary>
        /// Create a host configuration, by adding a host to the bus
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="hostTopology"></param>
        /// <returns></returns>
        IAmazonSqsHostConfiguration CreateHostConfiguration(AmazonSqsHostSettings settings);

        /// <summary>
        /// Create a receive endpoint configuration for the default host
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="endpointConfiguration"></param>
        /// <returns></returns>
        IAmazonSqsReceiveEndpointConfiguration CreateReceiveEndpointConfiguration(string queueName, IAmazonSqsEndpointConfiguration endpointConfiguration);

        /// <summary>
        /// Create a receive endpoint configuration for the default host
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="endpointConfiguration"></param>
        /// <returns></returns>
        IAmazonSqsReceiveEndpointConfiguration CreateReceiveEndpointConfiguration(QueueReceiveSettings settings,
            IAmazonSqsEndpointConfiguration endpointConfiguration);
    }
}