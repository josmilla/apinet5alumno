using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Net5Crud.Clientes.HealthChecks.Responses
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<IndividualHealthCheckResponse> HealthChecks { get; set; }
        public TimeSpan HealthCheckDuration { get; set; }
    }
}
