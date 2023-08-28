using Elastic.Apm;

namespace Pedidos.API.Application.Metrics
{
    public static class ApmMetrics
    {
        public static void RecordRequestMetrics(string name, long duration)
        {
            var transaction = Agent.Tracer.CurrentTransaction;
            if (transaction != null)
            {
                var span = transaction.StartSpan(name, "request");
                span.Duration = TimeSpan.FromMilliseconds(duration).TotalMilliseconds;
                span.End();
            }
        }

        public static void RecordError()
        {
            var transaction = Agent.Tracer.CurrentTransaction;
            if (transaction != null)
            {
                transaction.CaptureException(new Exception("Error occurred"));
            }
        }
    }

}
