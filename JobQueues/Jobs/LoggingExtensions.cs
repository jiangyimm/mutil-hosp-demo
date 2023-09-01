using Microsoft.Extensions.Logging;

namespace FastEndpoints.Messaging.Jobs;

internal static partial class LoggingExtensions
{
    //[LoggerMessage(1, LogLevel.Error, "Job storage 'get-next-batch' error for [queue-id:{queueID}]({tCommand}): {msg}. Retrying in 5 seconds...")]
    public static void StorageRetrieveError(this ILogger l, string queueID, string tCommand, string msg)
    {
        l.LogError(1, "Queue ID: {queueID} Command: {tCommand} Message: {msg}", queueID, tCommand, msg);

    }

    //[LoggerMessage(2, LogLevel.Critical, "Job [{tCommand}] 'execution' error: [{msg}]")]
    public static void CommandExecutionCritical(this ILogger l, string tCommand, string msg)
    {
        l.LogCritical(2, "Command: {tCommand} Message: {msg}", tCommand, msg);

    }

    //[LoggerMessage(3, LogLevel.Error, "Job storage 'on-execution-failure' error for [queue-id:{queueID}]({tCommand}): {msg}. Retrying in 5 seconds...")]
    public static void StorageOnExecutionFailureError(this ILogger l, string queueID, string tCommand, string msg)
    {
        l.LogError(3, "Queue ID: {queueID} Command: {tCommand} Message: {msg}", queueID, tCommand, msg);

    }

    //[LoggerMessage(4, LogLevel.Error, "Job storage 'mark-as-complete' error for [queue-id:{queueID}]({tCommand}): {msg}. Retrying in 5 seconds...")]
    public static void StorageMarkAsCompleteError(this ILogger l, string queueID, string tCommand, string msg)
    {
        l.LogError(4, "Queue ID: {queueID} Command: {tCommand} Message: {msg}", queueID, tCommand, msg);
    }
}