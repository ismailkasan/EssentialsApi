using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.IO;

namespace Essentials.Data
{
    public class CustomExecutionStrategy : ExecutionStrategy
    {
        EssentialsDbContext context;
        public CustomExecutionStrategy(EssentialsDbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
        {
            this.context = context;
        }
        public CustomExecutionStrategy(ExecutionStrategyDependencies dependencies, int maxRetryCount, TimeSpan maxRetryDelay) : base(dependencies, maxRetryCount, maxRetryDelay)
        {
        }
        protected override bool ShouldRetryOn(Exception exception)
        {

            /*
             * Bağlantı sağlanması denenirken uygulamanın ihtiyaçları doğrultusunda  Loglama,Email vs gibi işlemleri gerçekleştirebilirsiniz.
             * 
             */

            // file to write to.
            var message = $"Message: {exception.Message}: StackTrace: {exception.StackTrace}";
            File.WriteAllText("./log.txt", message);

            return true;
        }
    }
}
