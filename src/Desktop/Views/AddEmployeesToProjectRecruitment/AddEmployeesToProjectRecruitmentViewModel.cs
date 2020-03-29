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

namespace ProConstructionsManagment.Desktop.Views.AddEmployeesToProjectRecruitment
{
    public class AddEmployeesToProjectRecruitmentViewModel : ViewModelBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly IShellManager _shellManager;
        private readonly IMessengerService _messengerService;

        private string _projectId;
        private string _positionId;
        private string _projectRecruitmentId;

        private ObservableCollection<Models.Employee> _employeesWithPositionAbleToRecruit;

        private bool _addEmployee;

        public AddEmployeesToProjectRecruitmentViewModel(IEmployeesService employeesService, IShellManager shellManager, IMessengerService messengerService)
        {
            _employeesService = employeesService;
            _shellManager = shellManager;
            _messengerService = messengerService;

            messengerService.Register<PositionIdMessage>(this, msg => PositionId = msg.PositionId);
            messengerService.Register<ProjectIdMessage>(this, msg => ProjectId = msg.ProjectId);
            messengerService.Register<ProjectRecruitmentIdMessage>(this, msg => ProjectRecruitmentId = msg.ProjectRecruitmentId);
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

        public ObservableCollection<Models.Employee> EmployeesWithPositionAbleToRecruit
        {
            get => _employeesWithPositionAbleToRecruit;
            set => Set(ref _employeesWithPositionAbleToRecruit, value);
        }

        public string ProjectRecruitmentId
        {
            get => _projectRecruitmentId;
            set => Set(ref _projectRecruitmentId, value);
        }

        public bool AddEmployee
        {
            get => _addEmployee;
            set => Set(ref _addEmployee, value);
        }

        public async Task Initialize()
        {
            EmployeesWithPositionAbleToRecruit = await _employeesService.GetAllEmployeesByPositionAbleToRecruit(PositionId);
        }

        private bool MessageShownAlready { get; set; }

        public ICommand RecruitEmployeesCommand => new AsyncRelayCommand(RecruitEmployees);

        public async Task RecruitEmployees()
        {
            try
            {
                var employeesToRecruit = EmployeesWithPositionAbleToRecruit.Where(x => x.IsChecked).ToList();
                if (employeesToRecruit.Count > 0)
                {
                    var messageBoxResult = MessageBox.Show("Czy jesteś pewien/pewna że chcesz dodać tych pracowników do tej rekrutacji?", "", MessageBoxButton.YesNo);

                    switch (messageBoxResult)
                    {
                        case MessageBoxResult.Yes:
                            _shellManager.SetLoadingData(true);

                            foreach (var employeeToRecruit in employeesToRecruit)
                            {
                                var data = new Models.Employee
                                {
                                    Id = employeeToRecruit.Id,
                                    PositionId = employeeToRecruit.PositionId,
                                    ProjectId = ProjectId,
                                    Name = employeeToRecruit.Name,
                                    SecondName = employeeToRecruit.SecondName,
                                    LastName = employeeToRecruit.LastName,
                                    DateOfBirth = employeeToRecruit.DateOfBirth,
                                    Nationality = employeeToRecruit.Nationality,
                                    IsForeman = employeeToRecruit.IsForeman,
                                    ReadDrawings = employeeToRecruit.ReadDrawings,
                                    Status = employeeToRecruit.Status
                                };

                                var result = _employeesService.UpdateEmployee(data, employeeToRecruit.Id);
                                if (result.IsSuccessful)
                                {
                                    if (employeesToRecruit.IndexOf(employeeToRecruit) == employeesToRecruit.Count - 1)
                                    {
                                        Log.Information($"Successfully updated employee ({data.Id})");

                                        MessageBox.Show("Pomyślnie dodano pracowników do rekrutacji");

                                        _shellManager.SetLoadingData(false);

                                        _messengerService.Send(new ChangeViewMessage(ViewTypes.ProjectRecruitment));
                                        _messengerService.Send(new ProjectIdMessage(ProjectId));
                                        _messengerService.Send(new ProjectRecruitmentIdMessage(ProjectRecruitmentId));
                                    }
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