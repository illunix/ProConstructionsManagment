using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Models;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;

namespace ProConstructionsManagment.Desktop.Views.AddPosition
{
    public class AddPositionViewModel : ViewModelBase
    {
        private readonly IPositionsService _positionsService;
        private readonly IShellManager _shellManager;

        private string _positionName;
        private string _positionJobDescription;

        public AddPositionViewModel(IPositionsService positionsService, IShellManager shellManager)
        {
            _positionsService = positionsService;
            _shellManager = shellManager;
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

        private ValidationResult BuildValidation()
        {
            if (string.IsNullOrWhiteSpace(PositionName) || string.IsNullOrWhiteSpace(PositionJobDescription))
            {
                return new ValidationResult(false);
            }

            return new ValidationResult(true);
        }

        public ICommand AddPositionCommand => new AsyncRelayCommand(AddPosition);

        private async Task AddPosition()
        {
            if (BuildValidation().IsSuccessful)
            {
                try
                {
                    _shellManager.SetLoadingData(true);

                    var data = new Models.Position
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = PositionName,
                        JobDescription = PositionJobDescription
                    };

                    var result = await Task.Run(() => _positionsService.AddPosition(data));
                    if (result.IsSuccessful)
                    {
                        Log.Information($"Successfully added position ({data.Id})");

                        MessageBox.Show("Pomyślnie zapisano zmiany");
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed adding position");

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
