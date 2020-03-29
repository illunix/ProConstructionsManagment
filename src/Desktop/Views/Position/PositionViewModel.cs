using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.Position
{
    public class PositionViewModel : ViewModelBase
    {
        private readonly IPositionsService _positionsService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private string _positionId;
        private string _positionName;
        private string _positionJobDescription;

        public PositionViewModel(IPositionsService positionsService, IMessengerService messengerService, IShellManager shellManager)
        {
            _positionsService = positionsService;
            _messengerService = messengerService;
            _shellManager = shellManager;

            messengerService.Register<PositionIdMessage>(this, msg => PositionId = msg.PositionId);
        }

        public string PositionId
        {
            get => _positionId;
            set => Set(ref _positionId, value);
        }

        public string PositionName
        {
            get => _positionName;
            set => Set(ref _positionName, value);
        }

        public string PositionJobDescription
        {
            get => _positionJobDescription;
            set => Set(ref _positionJobDescription, value);
        }

        public async Task Initialize()
        {
            try
            {
                _shellManager.SetLoadingData(true);

                var position = await _positionsService.GetPositionById(PositionId);

                PositionName = position.Name;
                PositionJobDescription = position.JobDescription;
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading position view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(PositionName) || string.IsNullOrWhiteSpace(PositionJobDescription))
            {
                return new ValidationResult(false);
            }

            return new ValidationResult(true);
        }

        public ICommand UpdatePositionCommand => new AsyncRelayCommand(UpdatePosition);

        private async Task UpdatePosition()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.Position
                    {
                        Id = PositionId,
                        Name = PositionName,
                        JobDescription = PositionJobDescription
                    };

                    var result = await Task.Run(() => _positionsService.UpdatePosition(data, PositionId));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully updated position ({data.Id})");

                        MessageBox.Show("Pomyślnie zapisano zmiany");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed updating position");

                    MessageBox.Show(
                        "Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
                }
                finally
                {
                    _shellManager.SetLoadingData(false);
                }
            }
            else
            {
                MessageBox.Show("Uzupełnij wymagane pola");
            }
        }
    }
}