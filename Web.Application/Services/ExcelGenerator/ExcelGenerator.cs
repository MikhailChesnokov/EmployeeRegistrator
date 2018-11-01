namespace Web.Application.Services.ExcelGenerator
{
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Controllers.Registration.Enums;
    using Controllers.Registration.Forms;
    using Controllers.Registration.ViewModels;
    using Domain.Entities.Registration;
    using Domain.Infrastructure.Extensions;
    using OfficeOpenXml;



    public sealed class ExcelGenerator : IExcelGenerator
    {
        private readonly ExcelSettings _settings;
        
        
        
        public ExcelGenerator(ExcelSettings settings)
        {
            _settings = settings;
        }

        
        
        public async Task<Stream> GenerateAsync(RegistrationsViewModel registrationsViewModel)
        {
            string tempFileName = Path.GetTempFileName();
            
            File.Copy(_settings.ReportTemplatePath, tempFileName, overwrite: true);
            
            using (var package = new ExcelPackage(new FileInfo(tempFileName)))
            {
                FillContent(package, registrationsViewModel);
                
                package.Save();
            }

            var file = File.Open(tempFileName, FileMode.Open, FileAccess.Read);
            
            return await Task.FromResult(file);
        }

        
        private void FillContent(ExcelPackage package, RegistrationsViewModel registrationsViewModel)
        {
            var columns = new
            {
                Date = "A",
                Employee = "B",
                EventTime = "C",
                Event = "D",
                WorkTime = "E",
                DayWorkTime = "F",
                Lateness = "G"
            };

            
            
            Filter(registrationsViewModel.FilterForm);
            Table();



            void Filter(ReportFilterForm filter)
            {
                package
                    .Write(filter.EmployeeId.HasValue ? filter.Employees.FirstOrDefault(x => x.Selected)?.Text : "Все сотрудники")
                    .To("B", 3);
                
                package
                    .Write(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToShortDateString() : "Не указана")
                    .To("D", 3);
                
                package
                    .Write(filter.DateTo.HasValue ? filter.DateTo.Value.ToShortDateString() : "Не указана")
                    .To("D", 5);
                
                package
                    .Write(filter.Lateness.HasValue ? filter.LatenessSelectListItems.FirstOrDefault(x => x.Selected)?.Text : "Не имеет значения")
                    .To("F", 3);
                
                package
                    .Write(filter.StrictSchedule.HasValue ? filter.StrictScheduleSelecrListItems.FirstOrDefault(x => x.Selected)?.Text : "Не имеет значения")
                    .To("F", 5);
            }
            
            void Table()
            {
                int dayRowIndex = 11;

                foreach (var dayRegistrations in registrationsViewModel.DayRegistrations)
                {
                    var dayRegistrationsCount = dayRegistrations.DayEmployeeRegistrationsCount;

                    package
                        .Write(dayRegistrations.Day.ToShortDateString())
                        .From(columns.Date, dayRowIndex)
                        .To(columns.Date, dayRowIndex + dayRegistrationsCount - 1);

                    int dayEmployeeRowIndex = dayRowIndex;

                    foreach (var dayEmployeeRegistrations in dayRegistrations.DayEmployeeRegistraions)
                    {
                        var dayEmployeeRegistrationsCount = dayEmployeeRegistrations.TotalDayEmployeeRegistrationsCount;

                        package
                            .Write(dayEmployeeRegistrations.Employee)
                            .From(columns.Employee, dayEmployeeRowIndex)
                            .To(columns.Employee, dayEmployeeRowIndex + dayEmployeeRegistrationsCount - 1);

                        package
                            .Write(dayEmployeeRegistrations.TotalWorkDayTimeInterval.ToString("hh\\:mm"))
                            .From(columns.DayWorkTime, dayEmployeeRowIndex)
                            .To(columns.DayWorkTime, dayEmployeeRowIndex + dayEmployeeRegistrationsCount - 1);

                        package
                            .Write(dayEmployeeRegistrations.LatenessTimeInterval.ToString("hh\\:mm"))
                            .Fill(dayEmployeeRegistrations.EmployeeWasLate ? Color.Coral : Color.White)
                            .From(columns.Lateness, dayEmployeeRowIndex)
                            .To(columns.Lateness, dayEmployeeRowIndex + dayEmployeeRegistrationsCount - 1);

                        int registrationRowIndex = dayEmployeeRowIndex;

                        foreach (var registration in dayEmployeeRegistrations.RegistrationRows)
                        {
                            package
                                .Write(registration.Time.ToString("hh\\:mm"))
                                .To(columns.EventTime, registrationRowIndex);

                            package
                                .Write(registration.Event.DisplayName())
                                .To(columns.Event, registrationRowIndex);

                            if (registration.Event is RegistrationEventType.Coming &&
                                registration.CheckResult is RegistrationCheckResult.Ok)
                            {
                                    package
                                        .Write(registration.WorkTimeInterval.ToString("hh\\:mm"))
                                        .From(columns.WorkTime, registrationRowIndex)
                                        .To(columns.WorkTime, registrationRowIndex + 1);
                            }
                            else if (registration.CheckResult == RegistrationCheckResult.Violation)
                            {
                                package
                                    .Write("-")
                                    .Fill(Color.Orchid)
                                    .To(columns.WorkTime, registrationRowIndex);
                            }

                            registrationRowIndex++;
                        }

                        dayEmployeeRowIndex += dayEmployeeRegistrationsCount;
                    }

                    dayRowIndex += dayRegistrationsCount;
                }
            }
        }
    }
}