using System;
using System.Threading;
using System.Threading.Tasks;

namespace CandyFactory.Models
{
    public class SugarEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class AccidentEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class Factory : ITechnics
    {
        public event EventHandler<SugarEventArgs> SugarEnded;
        public event EventHandler<AccidentEventArgs> AccidentOccurred;
        public event EventHandler<string> FactoryStateChanged;

        private CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;

        public void Start()
        {
            if (_isRunning) return;
            _isRunning = true;

            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => RunFactory(_cancellationTokenSource.Token));
            FactoryStateChanged?.Invoke(this, "Запуск производства...");
        }

        public void Stop()
        {
            if (!_isRunning) return;
            _isRunning = false;
            _cancellationTokenSource.Cancel();
        }

        public void AddSugar()
        {
            if (_isRunning) return;
            FactoryStateChanged?.Invoke(this, "Сахар добавлен. Продолжаем производство...");
            Start();
        }

        public void FixFactory()
        {
            if (_isRunning) return;
            FactoryStateChanged?.Invoke(this, "Фабрика починена. Продолжаем производство...");
            Start();
        }

        private async Task RunFactory(CancellationToken token)
        {
            Random random = new Random();
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(1000);
                    FactoryStateChanged?.Invoke(this, "Смешивание ингредиентов...");
                    await Task.Delay(2000); // Этап смешивания
                    CheckCancellation(token);


                    if (random.NextDouble() < 0.15)
                    {
                        _isRunning = false;
                        SugarEnded?.Invoke(this, new SugarEventArgs { Message = "Сахар закончился!" });
                        FactoryStateChanged?.Invoke(this, "Нет сахара");
                        break;
                    }

                    FactoryStateChanged?.Invoke(this, "Формовка конфет...");
                    await Task.Delay(2000); // Этап формовки
                    CheckCancellation(token);


                    if (random.NextDouble() < 0.05)
                    {
                        _isRunning = false;
                        AccidentOccurred?.Invoke(this, new AccidentEventArgs { Message = "Произошла авария!" });
                        FactoryStateChanged?.Invoke(this, "Авария");
                        break;
                    }

                    FactoryStateChanged?.Invoke(this, "Охлаждение конфет...");
                    await Task.Delay(2000); // Этап охлаждения
                    CheckCancellation(token);


                    FactoryStateChanged?.Invoke(this, "Упаковка конфет...");
                    await Task.Delay(2000); // Этап упаковки
                    CheckCancellation(token);


                    FactoryStateChanged?.Invoke(this, "Производство завершено, конфеты готовы.");
                    await Task.Delay(2000); // Пауза перед началом нового цикла

                }
                catch (TaskCanceledException)
                {
                    FactoryStateChanged?.Invoke(this, "Остановлено");
                }
            }
        }
        private void CheckCancellation(CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }
        }
    }
}
