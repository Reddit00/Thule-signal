using System;
using System.Threading;
using ThuleSignal.Domain.Exceptions;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class RetryPolicyService
    {
        public void ExecuteWithRetry(Action operation, int maxRetries = 3, int initialDelayMs = 500)
        {
            int attempt = 0;

            while (true)
            {
                try
                {
                    attempt++;
                    operation(); 
                    return;  
                }
                catch (NetworkStreamingException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[Retry Policy] Спроба {attempt} із {maxRetries} провалилася. Причина: {ex.Message}");
                    Console.ResetColor();

                    if (attempt >= maxRetries)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[Retry Policy] Усі спроби вичерпано. Операція завершилась критичною помилкою.");
                        Console.ResetColor();
                        throw; 
                    }

                    int delay = initialDelayMs * (int)Math.Pow(2, attempt - 1);
                    Console.WriteLine($"[Retry Policy] Очікування {delay} ms перед наступною спробою...");
                    Thread.Sleep(delay); 
                }
            }
        }
    }
}