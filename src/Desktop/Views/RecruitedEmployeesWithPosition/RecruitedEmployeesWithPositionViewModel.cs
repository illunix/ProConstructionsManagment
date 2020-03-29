using ProConstructionsManagment.Desktop.Commands;
using ProConstructionsManagment.Desktop.Enums;
using ProConstructionsManagment.Desktop.Managers;
using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;
using ProConstructionsManagment.Desktop.Views.Base;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProConstructionsManagment.Desktop.Views.RecruitedEmployeesWithPosition
{
    public class RecruitedEmployeesWithPositionViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IMessengerService _messengerService;
        private readonly IShellManager _shellManager;

        private ObservableCollection<Models.Employee> _recruitedEmployees;

        private string _projectId;
        private string _positionId;
        private string _projectRecruitmentId;

        public RecruitedEmployeesWithPositionViewModel(IEmployeesService employeesService, IMessengerService messengerService, IShellManager shellManager)
        {
            _employeesService = employeesService;
            _messengerService = messengerService;
            _shellManager = shellManager;

            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
            messengerService.Register<PositionIdMessage>(this, msg => PositionId = msg.PositionId);
            messengerService.Register<ProjectRecruitmentIdMessage>(this, msg => ProjectRecruitmentId = msg.ProjectRecruitmentId);
        }

        public ObservableCollection<Models.Employee> RecruitedEmployees
        {
            get => _recruitedEmployees;
            set => Set(ref _recruitedEmployees, value);
        }

        public string ProjectId
        {
            get => _projectId;
            set => Set(ref _projectId, value);
        }

        public string PositionId
        {
            get => _positionId;
            set => Set(ref _positionId, value);
        }

        public string ProjectRecruitmentId
        {
            get => _projectRecruitmentId;
            set => Set(ref _projectRecruitmentId, value);
        }

        public async Task Initialize()
        {
            try
            {
                var employees = await _employeesService.GetAllEmployeesByProjectIdAndPositionId(ProjectId, PositionId);

                foreach (var employee in employees)
                {
                    if (employee.ProjectId != Guid.Empty.ToString())
                    {
                        employee.IsChecked = true;
                    }

                    RecruitedEmployees = employees;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed loading recruited employees with position view");

                MessageBox.Show("Coś poszło nie tak podczas pobierania danych");
            }
            finally
            {
                _shellManager.SetLoadingData(false);
            }
        }

        public ICommand UpdateRecruitedEmployeesCommand => new AsyncRelayCommand<object>(UpdateRecruitedEmployees);

        private async Task UpdateRecruitedEmployees(object obj)
        {
            try
            {
                var recruitedEmployees = RecruitedEmployees.Where(x => x.IsChecked == false).ToList();
                if (recruitedEmployees.Count > 0)
                {
                    var messageBoxResult = MessageBox.Show("Czy jesteś pewien/pewna że chcesz zapisać zmiany w tej rekrutacji?", "", MessageBoxButton.YesNo);

                    switch (messageBoxResult)
                    {
                        case MessageBoxResult.Yes:
                            _shellManager.SetLoadingData(true);

                            foreach (var recruitedEmployee in recruitedEmployees)
                            {
                                var data = new Models.Employee
                                {
                                    Id = recruitedEmployee.Id,
                                    PositionId = recruitedEmployee.PositionId,
                                    ProjectId = Guid.Empty.ToString(),
                                    Name = recruitedEmployee.Name,
                                    SecondName = recruitedEmployee.SecondName,
                                    LastName = recruitedEmployee.LastName,
                                    DateOfBirth = recruitedEmployee.DateOfBirth,
                                    Nationality = recruitedEmployee.Nationality,
                                    IsForeman = recruitedEmployee.IsForeman,
                                    ReadDrawings = recruitedEmployee.ReadDrawings,
                                    Status = recruitedEmployee.Status
                                };

                                var result = _employeesService.UpdateEmployee(data, recruitedEmployee.Id);
                                if (recruitedEmployees.IndexOf(recruitedEmployee) == recruitedEmployees.Count - 1)
                                {
                                    Log.Information($"Successfully updated employee ({data.Id})");

                                    MessageBox.Show("Pomyślnie zapisano zmiany w rekrutacji");

                                    _shellManager.SetLoadingData(false);

                                    _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitment));
                                    _messengerService.Send(new ProjectIdMessage(ProjectId));
                                    _messengerService.Send(new ProjectRecruitmentIdMessage(ProjectRecruitmentId));
                                }
                            }
                            break;

                        case MessageBoxResult.No:
                            return;
                    }
                }
                else
                {
                    MessageBox.Show("Nie podano żadnych pracowników do dodania.");
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed updating employee");

                MessageBox.Show(
                    "Coś poszło nie tak podczas zapisywania zmian, proszę spróbować jeszcze raz. Jeśli problem nadal występuje, skontakuj się z administratorem oprogramowania");
            }
        }
    }
}