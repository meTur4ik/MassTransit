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
namespace MassTransit.HttpTransport.Configuration
{
    using Hosting;
    using MassTransit.Configuration;
    using Topology;


    public class HttpBusConfiguration :
        HttpEndpointConfiguration,
        IHttpBusConfiguration
    {
        readonly IHttpTopologyConfiguration _topologyConfiguration;
        readonly IHostCollection<IHttpHostConfiguration> _hosts;

        public HttpBusConfiguration(IHttpTopologyConfiguration topologyConfiguration)
            : base(topologyConfiguration)
        {
            _topologyConfiguration = topologyConfiguration;

            _hosts = new HostCollection<IHttpHostConfiguration>();
        }

        public IReadOnlyHostCollection<IHttpHostConfiguration> Hosts => _hosts;

        public IHttpHostConfiguration CreateHostConfiguration(HttpHostSettings settings)
        {
            var hostTopology = new HttpHostTopology(_topologyConfiguration);

            var hostConfiguration = new HttpHostConfiguration(this, settings, hostTopology);

            _hosts.Add(hostConfiguration);

            return hostConfiguration;
        }

        public IHttpReceiveEndpointConfiguration CreateReceiveEndpointConfiguration(string queueName, IHttpEndpointConfiguration endpointConfiguration)
        {
            if (_hosts.Count == 0)
                throw new ConfigurationException("At least one host must be configured");

            return new HttpReceiveEndpointConfiguration(_hosts[0], queueName, endpointConfiguration);
        }

        IReadOnlyHostCollection IBusConfiguration.Hosts => _hosts;
    }
}