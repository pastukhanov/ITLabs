using System;
using System.Threading;
using System.Threading.Tasks;

namespace CandyFactory.Models
{
    public class Loader : ITechnics
    {
        public event EventHandler<string> LoaderStateChanged;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;

        public void Start()
        {
            if (_isRunning) return;
            _isRunning = true;

            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() => RunLoader(_cancellationTokenSource.Token));
            LoaderStateChanged?.Invoke(this, "Запуск погрузчика...");
        }

        public void Stop()
        {
             if (!_isRunning) return;
             _isRunning = false;

            _cancellationTokenSource.Cancel();
        }

        private async Task RunLoader(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    LoaderStateChanged?.Invoke(this, "Получение конфет с фабрики...");
                    await Task.Delay(2000); // Этап получения конфет
                    CheckCancellation(token);

                    LoaderStateChanged?.Invoke(this, "Загрузка конфет на транспортное средство...");
                    await Task.Delay(2000); // Этап загрузки
                    CheckCancellation(token);

                    LoaderStateChanged?.Invoke(this, "Доставка конфет в магазин...");
                    await Task.Delay(4000); // Этап доставки
                    CheckCancellation(token);

                    LoaderStateChanged?.Invoke(this, "Возвращение на фабрику...");
                    await Task.Delay(2000); // Этап возвращения
                    CheckCancellation(token);

                    LoaderStateChanged?.Invoke(this, "Погрузчик завершил цикл, готов к новому циклу.");
                    await Task.Delay(2000); // Пауза перед началом нового цикла
                }
                catch (TaskCanceledException)
                {
                    // Процесс был отменен
                    LoaderStateChanged?.Invoke(this, "Остановлено");
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
