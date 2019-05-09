using Serilog.Events;
using Serilog.Formatting;
using System;
using System.IO;

namespace Ardas.AspNetCore.Logging.Formatters
{
    public class KibanaLogsFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            FormatContent(logEvent, output);
            output.WriteLine();
        }

        private static void FormatContent(LogEvent logEvent, TextWriter output)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            if (output == null) throw new ArgumentNullException(nameof(output));

            WriteBlock(output, logEvent.Timestamp.ToString("o"));
            WriteBlock(output, logEvent.Level.ToString().ToUpper());
            output.Write("['message':'");
            output.Write(logEvent.MessageTemplate.Render(logEvent.Properties));
            output.Write("']");
        }

        private static void WriteBlock(TextWriter output, string value)
        {
            output.Write('[');
            output.Write(value);
            output.Write(']');
        }
    }
}
