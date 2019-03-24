namespace Web.Application.Controllers.indicators.Forms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ViewModels;


    public class IndicatorsListForm : IForm
    {
        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        public IEnumerable<DayIndicatorViewModel> DayIndicators { get; set; }
    }
}