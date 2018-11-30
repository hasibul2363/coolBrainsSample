using System;
using Serilog;
using SuitSupply.Infrastructure.Logger.Contracts;

namespace SuitSupply.Infrastructure.Logger.Serilog
{
    public class SuitLogUsingSerilog : ISuitLog
    {
        public void Debug(string message) => Log.Logger.Debug(message);

        public void Debug(string message, params object[] propertyValues) => Log.Logger.Debug(message, propertyValues);

        public void Error(string message) =>
            Log.Logger.Error(message);

        public void Error(string message, params object[] propertyValues) =>
            Log.Logger.Error(message, propertyValues);

        public void Error(Exception exception, string message, params object[] propertyValues) =>
            Log.Logger.Error(exception, message, propertyValues);


        public void Fatal(string message) =>
            Log.Logger.Fatal(message);

        public void Fatal(string message, params object[] propertyValues) =>
            Log.Logger.Fatal(message, propertyValues);

        public void Fatal(Exception exception, string message, params object[] propertyValues) =>
            Log.Logger.Fatal(exception, message, propertyValues);

        public void Information(string message) => Log.Logger.Information(message);

        public void Information(string message, params object[] propertyValues) =>
            Log.Logger.Information(message, propertyValues);


    }
}
